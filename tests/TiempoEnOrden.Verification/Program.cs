using TiempoEnOrden.Core.Models;
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
    await thirdRepository.CloseAsync();

    Console.WriteLine("BLOQUE-1 APROBADO: SQLite conserva datos después de reabrir la conexión.");
    Console.WriteLine("BLOQUE-2 APROBADO: clientes y servicios se guardan y consultan desde SQLite.");
    Console.WriteLine("PRIMER HITO APROBADO: cliente, servicio y cita permanecen tras reabrir; la cita se editó y canceló.");
    Console.WriteLine("BLOQUE-3 APROBADO: altas, consulta, edición y cancelación de citas usan SQLite.");
    Console.WriteLine("BLOQUE-4 APROBADO: tiempo personal, horario laboral y descanso permanecen tras reabrir SQLite.");
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

