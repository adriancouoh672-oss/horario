using SQLite;

namespace TiempoEnOrden.Infrastructure.Entities;

[Table("Clients")]
internal sealed class ClientEntity
{
    [PrimaryKey] public string Id { get; set; } = "";
    [Indexed] public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Notes { get; set; } = "";
    public bool IsArchived { get; set; }
}

[Table("Services")]
internal sealed class ServiceEntity
{
    [PrimaryKey] public string Id { get; set; } = "";
    [Indexed] public string Name { get; set; } = "";
    public int DurationMinutes { get; set; }
    public int PreparationMinutes { get; set; }
    public int CleanupMinutes { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "MXN";
    public bool IsArchived { get; set; }
}

[Table("Appointments")]
internal sealed class AppointmentEntity
{
    [PrimaryKey] public string Id { get; set; } = "";
    [Indexed] public string? ClientId { get; set; }
    public string ClientName { get; set; } = "";
    [Indexed] public string ServiceId { get; set; } = "";
    public string ServiceName { get; set; } = "";
    [Indexed] public long StartTicks { get; set; }
    public int DurationMinutes { get; set; }
    public int PreparationMinutes { get; set; }
    public int CleanupMinutes { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "MXN";
    public string Notes { get; set; } = "";
    [Indexed] public int Status { get; set; }
}

[Table("PersonalTimes")]
internal sealed class PersonalTimeEntity
{
    [PrimaryKey] public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public int Kind { get; set; }
    [Indexed] public long StartTicks { get; set; }
    [Indexed] public long EndTicks { get; set; }
}

[Table("WorkSchedule")]
internal sealed class WorkScheduleEntity
{
    [PrimaryKey] public int Id { get; set; } = 1;
    public long WorkStartTicks { get; set; }
    public long WorkEndTicks { get; set; }
    public long BreakStartTicks { get; set; }
    public long BreakEndTicks { get; set; }
    public int WorkingDaysMask { get; set; }
}

