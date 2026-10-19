using TiempoEnOrden.App.Models;

namespace TiempoEnOrden.App.Services;

public sealed class DemoScheduleStore : IDemoScheduleStore
{
    public static readonly DateTime DemoNow = new(2026, 11, 6, 9, 20, 0, DateTimeKind.Unspecified);

    private readonly List<DemoCommitment> _commitments =
    [
        new("personal-caminata", "Caminata", "Personal", new(2026, 11, 6, 7, 30, 0), new(2026, 11, 6, 8, 0, 0), DemoCommitmentKind.Personal),
        new("cita-ana", "Corte con Ana", "Corte clásico · Negocio", new(2026, 11, 6, 9, 50, 0), new(2026, 11, 6, 10, 50, 0), DemoCommitmentKind.Business, new(2026, 11, 6, 10, 0, 0), new(2026, 11, 6, 10, 45, 0)),
        new("descanso-viernes", "Descanso", "No disponible", new(2026, 11, 6, 14, 0, 0), new(2026, 11, 6, 15, 0, 0), DemoCommitmentKind.Break),
        new("clase-viernes", "Clase nocturna", "Personal", new(2026, 11, 6, 18, 30, 0), new(2026, 11, 6, 20, 0, 0), DemoCommitmentKind.Personal),
        new("cita-luis", "Afeitado con Luis", "Afeitado · Negocio", new(2026, 11, 3, 11, 50, 0), new(2026, 11, 3, 12, 40, 0), DemoCommitmentKind.Business, new(2026, 11, 3, 12, 0, 0), new(2026, 11, 3, 12, 30, 0)),
        new("personal-gimnasio", "Gimnasio", "Personal", new(2026, 11, 4, 17, 0, 0), new(2026, 11, 4, 18, 0, 0), DemoCommitmentKind.Personal),
        new("cita-cancelada", "Corte cancelado", "Cancelada · Negocio", new(2026, 11, 5, 11, 50, 0), new(2026, 11, 5, 12, 50, 0), DemoCommitmentKind.Business, new(2026, 11, 5, 12, 0, 0), new(2026, 11, 5, 12, 45, 0), true)
    ];

    private readonly List<DemoClient> _clients =
    [
        new("Ana López", "555-010-2040", "Prefiere cita por la mañana"),
        new("Luis Pérez", "555-010-2075"),
        new("María Torres", "555-010-2090", IsArchived: true)
    ];

    private readonly List<DemoService> _services =
    [
        new("Corte clásico", 45, 10, 5, 250, "MXN"),
        new("Afeitado", 30, 10, 10, 180, "MXN"),
        new("Corte infantil", 35, 5, 5, 200, "MXN", IsArchived: true)
    ];

    private readonly List<string> _tasks = ["Comprar insumos"];

    public IReadOnlyList<DemoCommitment> Commitments => _commitments;
    public IReadOnlyList<DemoClient> Clients => _clients;
    public IReadOnlyList<DemoService> Services => _services;
    public IReadOnlyList<string> Tasks => _tasks;

    public event EventHandler? Changed;

    public void AddClient(DemoClient client)
    {
        _clients.Add(client);
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void AddService(DemoService service)
    {
        _services.Add(service);
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void AddPersonalTime(DemoCommitment commitment)
    {
        _commitments.Add(commitment);
        Changed?.Invoke(this, EventArgs.Empty);
    }
}
