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
    await firstRepository.CloseAsync();

    var reopenedRepository = new SqliteLocalDataRepository(databasePath);
    var loaded = await reopenedRepository.GetClientAsync(client.Id);
    Assert(loaded == client, "El registro debe permanecer después de cerrar y reabrir SQLite.");
    Assert(await reopenedRepository.GetServiceAsync(service.Id) == service, "El servicio debe permanecer después de reabrir SQLite.");
    Assert((await reopenedRepository.GetWorkScheduleAsync()).WorkingDays.Contains(DayOfWeek.Monday), "Debe existir el horario inicial persistente.");
    await reopenedRepository.CloseAsync();

    Console.WriteLine("BLOQUE-1 APROBADO: SQLite conserva datos después de reabrir la conexión.");
    Console.WriteLine("BLOQUE-2 APROBADO: clientes y servicios se guardan y consultan desde SQLite.");
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

