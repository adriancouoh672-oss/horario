namespace TiempoEnOrden.Core.Models;

public sealed record ClientRecord(Guid Id, string Name, string Phone, string Notes, bool IsArchived = false);

public sealed record ServiceRecord(
    Guid Id,
    string Name,
    int DurationMinutes,
    int PreparationMinutes,
    int CleanupMinutes,
    decimal Price,
    string Currency,
    bool IsArchived = false);

public enum AppointmentStatus
{
    Active,
    Cancelled
}

public sealed record AppointmentRecord(
    Guid Id,
    Guid? ClientId,
    string ClientName,
    Guid ServiceId,
    string ServiceName,
    DateTime Start,
    int DurationMinutes,
    int PreparationMinutes,
    int CleanupMinutes,
    decimal Price,
    string Currency,
    string Notes,
    AppointmentStatus Status)
{
    public DateTime ServiceEnd => Start.AddMinutes(DurationMinutes);
    public DateTime OccupiedStart => Start.AddMinutes(-PreparationMinutes);
    public DateTime OccupiedEnd => ServiceEnd.AddMinutes(CleanupMinutes);
}

public enum PersonalTimeKind
{
    Activity,
    Block
}

public sealed record PersonalTimeRecord(
    Guid Id,
    string Title,
    PersonalTimeKind Kind,
    DateTime Start,
    DateTime End);

public sealed record WorkScheduleSettings(
    TimeSpan WorkStart,
    TimeSpan WorkEnd,
    TimeSpan BreakStart,
    TimeSpan BreakEnd,
    IReadOnlySet<DayOfWeek> WorkingDays)
{
    public static WorkScheduleSettings Default { get; } = new(
        new TimeSpan(9, 0, 0),
        new TimeSpan(18, 0, 0),
        new TimeSpan(14, 0, 0),
        new TimeSpan(15, 0, 0),
        new HashSet<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday
        });
}

