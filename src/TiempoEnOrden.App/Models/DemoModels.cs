using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.Models;

public enum DemoCommitmentKind
{
    Business,
    Personal,
    Break
}

public sealed record DemoCommitment(
    string Id,
    string Title,
    string Subtitle,
    DateTime OccupationStart,
    DateTime OccupationEnd,
    DemoCommitmentKind Kind,
    DateTime? ServiceStart = null,
    DateTime? ServiceEnd = null,
    bool IsCancelled = false);

public sealed record DemoClient(string Name, string Phone, string Notes = "", bool IsArchived = false)
{
    public string StatusText => IsArchived ? "Archivado" : "Activo";
}

public sealed record DemoService(
    string Name,
    int DurationMinutes,
    int PreparationMinutes,
    int CleanupMinutes,
    decimal Price,
    string Currency,
    bool IsArchived = false)
{
    public string Summary => $"{DurationMinutes} min · {Currency} {Price:0}";
    public string OccupationSummary => $"{PreparationMinutes} min preparación + {DurationMinutes} min servicio + {CleanupMinutes} min limpieza";
    public string StatusText => IsArchived ? "Archivado" : "Activo";
}

public sealed class AppointmentDraft : Infrastructure.ObservableObject
{
    private string? _clientName = "Ana López";
    private DemoService? _service;
    private DateTime _date = DemoScheduleStore.DemoNow.Date;
    private TimeSpan _time = new(10, 0, 0);
    private string _notes = "";

    public string? ClientName
    {
        get => _clientName;
        set => SetProperty(ref _clientName, value);
    }

    public DemoService? Service
    {
        get => _service;
        set => SetProperty(ref _service, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public TimeSpan Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }
}
