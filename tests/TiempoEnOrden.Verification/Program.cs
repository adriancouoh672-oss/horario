using TiempoEnOrden.Core.Models;
using TiempoEnOrden.Core.Services;
using TiempoEnOrden.Infrastructure;

var verificationRoot = Path.Combine(Path.GetTempPath(), "TiempoEnOrdenVerification", Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(verificationRoot);
var databasePath = Path.Combine(verificationRoot, "verification.db3");

try
{
    var client = new ClientRecord(Guid.NewGuid(), "Cliente persistente", "5550101000", "Prueba local");
    var service = new ServiceRecord(Guid.NewGuid(), "Servicio persistente", 45, 10, 5, 250m, "MXN");
    var firstRepository = new SqliteLocalDataRepository(databasePath);
    await firstRepository.SaveClientAsync(client);
    await firstRepository.SaveServiceAsync(service);
    var appointment = new AppointmentRecord(
        Guid.NewGuid(), client.Id, client.Name, service.Id, service.Name,
        new DateTime(2026, 10, 20, 10, 0, 0), service.DurationMinutes,
        service.PreparationMinutes, service.CleanupMinutes, service.Price,
        service.Currency, "Primera cita", AppointmentStatus.Active);
    await firstRepository.SaveAppointmentAsync(appointment);
    await firstRepository.CloseAsync();

    var reopenedRepository = new SqliteLocalDataRepository(databasePath);
    var loaded = await reopenedRepository.GetClientAsync(client.Id);
    Assert(loaded == client, "El registro debe permanecer después de cerrar y reabrir SQLite.");
    Assert(await reopenedRepository.GetServiceAsync(service.Id) == service, "El servicio debe permanecer después de reabrir SQLite.");
    Assert(await reopenedRepository.GetAppointmentAsync(appointment.Id) == appointment, "La cita debe permanecer después de reabrir SQLite.");
    Assert((await reopenedRepository.GetWorkScheduleAsync()).WorkingDays.Contains(DayOfWeek.Monday), "Debe existir el horario inicial persistente.");

    var editedAppointment = appointment with
    {
        Start = appointment.Start.AddHours(1),
        Notes = "Cita editada"
    };
    await reopenedRepository.SaveAppointmentAsync(editedAppointment);
    Assert(await reopenedRepository.GetAppointmentAsync(appointment.Id) == editedAppointment, "La edición debe conservar el identificador y actualizar la cita.");

    var changedService = service with { DurationMinutes = 60, Price = 300m };
    await reopenedRepository.SaveServiceAsync(changedService);
    var appointmentAfterServiceChange = await reopenedRepository.GetAppointmentAsync(appointment.Id);
    Assert(appointmentAfterServiceChange?.DurationMinutes == 45 && appointmentAfterServiceChange.Price == 250m,
        "Cambiar el servicio no debe alterar la copia histórica de una cita existente.");

    var cancelledAppointment = editedAppointment with { Status = AppointmentStatus.Cancelled };
    await reopenedRepository.SaveAppointmentAsync(cancelledAppointment);
    Assert((await reopenedRepository.GetAppointmentsAsync()).All(item => item.Id != appointment.Id),
        "Una cita cancelada no debe aparecer por defecto.");
    Assert((await reopenedRepository.GetAppointmentsAsync(includeCancelled: true)).Single(item => item.Id == appointment.Id).Status == AppointmentStatus.Cancelled,
        "Una cita cancelada debe conservarse en el historial.");

    var personalTime = new PersonalTimeRecord(
        Guid.NewGuid(), "Actividad persistente", PersonalTimeKind.Activity,
        new DateTime(2026, 10, 20, 12, 0, 0), new DateTime(2026, 10, 20, 13, 0, 0));
    var schedule = new WorkScheduleSettings(
        new TimeSpan(8, 30, 0), new TimeSpan(17, 30, 0),
        new TimeSpan(13, 30, 0), new TimeSpan(14, 15, 0),
        new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday });
    await reopenedRepository.SavePersonalTimeAsync(personalTime);
    await reopenedRepository.SaveWorkScheduleAsync(schedule);
    await reopenedRepository.CloseAsync();

    var thirdRepository = new SqliteLocalDataRepository(databasePath);
    Assert((await thirdRepository.GetPersonalTimesAsync()).Single() == personalTime,
        "El tiempo personal debe permanecer después de reabrir SQLite.");
    var loadedSchedule = await thirdRepository.GetWorkScheduleAsync();
    Assert(loadedSchedule.WorkStart == schedule.WorkStart && loadedSchedule.WorkEnd == schedule.WorkEnd &&
           loadedSchedule.BreakStart == schedule.BreakStart && loadedSchedule.BreakEnd == schedule.BreakEnd &&
           loadedSchedule.WorkingDays.SetEquals(schedule.WorkingDays),
        "El horario laboral y el descanso deben permanecer después de reabrir SQLite.");
    var backupPath = Path.Combine(verificationRoot, "respaldos", "verificacion.db3");
    await thirdRepository.CreateBackupAsync(backupPath);
    Assert(File.Exists(backupPath), "El respaldo debe crear un archivo de base de datos.");
    var recordAfterBackup = new ClientRecord(Guid.NewGuid(), "Cliente posterior", "", "");
    await thirdRepository.SaveClientAsync(recordAfterBackup);
    Assert(await thirdRepository.GetClientAsync(recordAfterBackup.Id) is not null,
        "El cambio posterior debe existir antes de restaurar.");
    await thirdRepository.RestoreBackupAsync(backupPath);
    Assert(await thirdRepository.GetClientAsync(recordAfterBackup.Id) is null,
        "Restaurar debe descartar los cambios posteriores al respaldo.");
    Assert(await thirdRepository.GetClientAsync(client.Id) == client,
        "Restaurar debe recuperar los datos existentes al crear el respaldo.");
    await thirdRepository.CloseAsync();

    var availability = new AvailabilityService();
    var activeAppointment = editedAppointment with { Status = AppointmentStatus.Active };
    var marginConflict = availability.Check(
        new AvailabilityRequest(new DateTime(2026, 10, 20, 11, 55, 0), 30, 10, 0),
        schedule, [activeAppointment], []);
    Assert(marginConflict.Conflicts.Any(item => item.Kind == AvailabilityConflictKind.Appointment),
        "La preparación de la cita candidata debe detectar la limpieza u ocupación de otra cita.");

    var selfEdit = availability.Check(
        new AvailabilityRequest(activeAppointment.Start, activeAppointment.DurationMinutes,
            activeAppointment.PreparationMinutes, activeAppointment.CleanupMinutes, activeAppointment.Id),
        schedule, [activeAppointment], []);
    Assert(selfEdit.IsAvailable, "Editar una cita no debe compararla consigo misma.");

    var cancelledIsFree = availability.Check(
        new AvailabilityRequest(cancelledAppointment.Start, cancelledAppointment.DurationMinutes,
            cancelledAppointment.PreparationMinutes, cancelledAppointment.CleanupMinutes),
        schedule, [cancelledAppointment], []);
    Assert(cancelledIsFree.IsAvailable, "Una cita cancelada debe liberar disponibilidad.");

    var personalConflict = availability.Check(
        new AvailabilityRequest(new DateTime(2026, 10, 20, 12, 15, 0), 30, 0, 0),
        schedule, [], [personalTime]);
    Assert(personalConflict.Conflicts.Any(item => item.Kind == AvailabilityConflictKind.PersonalTime),
        "El tiempo personal debe proteger su intervalo.");

    var breakConflict = availability.Check(
        new AvailabilityRequest(new DateTime(2026, 10, 20, 13, 45, 0), 15, 0, 0),
        schedule, [], []);
    Assert(breakConflict.Conflicts.Any(item => item.Kind == AvailabilityConflictKind.Break),
        "El descanso laboral debe bloquear citas.");

    var outsideSchedule = availability.Check(
        new AvailabilityRequest(new DateTime(2026, 10, 20, 17, 15, 0), 30, 0, 5),
        schedule, [], []);
    Assert(outsideSchedule.Conflicts.Any(item => item.Kind == AvailabilityConflictKind.OutsideWorkSchedule),
        "La ocupación total debe caber en el horario laboral.");

    var touchingEdge = availability.Check(
        new AvailabilityRequest(new DateTime(2026, 10, 20, 12, 0, 0), 30, 10, 0),
        schedule, [activeAppointment], []);
    Assert(touchingEdge.IsAvailable, "Dos intervalos que solo se tocan en el límite no deben considerarse cruce.");

    var suggestionRequest = new AvailabilityRequest(
        new DateTime(2026, 10, 20, 11, 45, 0), 30, 10, 5);
    var suggestions = availability.Suggest(
        suggestionRequest, schedule, [activeAppointment], [personalTime]);
    Assert(suggestions.Count == 3, "La búsqueda debe devolver hasta tres alternativas disponibles.");
    Assert(suggestions.All(item => item.Minute % 15 == 0), "Las alternativas deben respetar intervalos de 15 minutos.");
    Assert(suggestions.All(item => item < suggestionRequest.ServiceStart.Date.AddDays(14)),
        "Las alternativas deben quedar dentro del horizonte de 14 días.");
    Assert(suggestions.All(item => availability.Check(
            suggestionRequest with { ServiceStart = item }, schedule, [activeAppointment], [personalTime]).IsAvailable),
        "Cada sugerencia debe cumplir exactamente las mismas reglas del guardado.");

    Console.WriteLine("BLOQUE-1 APROBADO: SQLite conserva datos después de reabrir la conexión.");
    Console.WriteLine("BLOQUE-2 APROBADO: clientes y servicios se guardan y consultan desde SQLite.");
    Console.WriteLine("PRIMER HITO APROBADO: cliente, servicio y cita permanecen tras reabrir; la cita se editó y canceló.");
    Console.WriteLine("BLOQUE-3 APROBADO: altas, consulta, edición y cancelación de citas usan SQLite.");
    Console.WriteLine("BLOQUE-4 APROBADO: tiempo personal, horario laboral y descanso permanecen tras reabrir SQLite.");
    Console.WriteLine("BLOQUE-5 APROBADO: cruces, márgenes, descanso, horario, cancelación y autoexclusión verificados en Core.");
    Console.WriteLine("BLOQUE-6 APROBADO: tres sugerencias de 15 minutos y 14 días reutilizan las reglas del guardado.");
    Console.WriteLine("BLOQUE-7 APROBADO: respaldo y restauración recuperan la base local comprobada.");
}
finally
{
    SQLite.SQLiteAsyncConnection.ResetPool();
    if (Directory.Exists(verificationRoot))
    {
        Directory.Delete(verificationRoot, true);
    }
}

static void Assert(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}

