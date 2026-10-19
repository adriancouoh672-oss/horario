using TiempoEnOrden.App.Models;

namespace TiempoEnOrden.App.Services;

public interface IDemoScheduleStore
{
    IReadOnlyList<DemoCommitment> Commitments { get; }
    IReadOnlyList<DemoClient> Clients { get; }
    IReadOnlyList<DemoService> Services { get; }
    IReadOnlyList<string> Tasks { get; }

    event EventHandler? Changed;

    void AddClient(DemoClient client);
    void AddService(DemoService service);
    void AddPersonalTime(DemoCommitment commitment);
}
