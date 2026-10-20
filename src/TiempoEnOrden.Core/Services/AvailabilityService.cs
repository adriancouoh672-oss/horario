using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.Core.Services;

public enum AvailabilityConflictKind
{
    OutsideWorkSchedule,
    Break,
    Appointment,
    PersonalTime
}

public sealed record AvailabilityRequest(
    DateTime ServiceStart,
    int DurationMinutes,
    int PreparationMinutes,
    int CleanupMinutes,
    Guid? AppointmentId = null)
{
    public DateTime ServiceEnd => ServiceStart.AddMinutes(DurationMinutes);
    public DateTime OccupiedStart => ServiceStart.AddMinutes(-PreparationMinutes);
    public DateTime OccupiedEnd => ServiceEnd.AddMinutes(CleanupMinutes);
}

public sealed record AvailabilityConflict(
    AvailabilityConflictKind Kind,
    string Description,
    DateTime Start,
    DateTime End);

public sealed record AvailabilityResult(IReadOnlyList<AvailabilityConflict> Conflicts)
{
    public bool IsAvailable => Conflicts.Count == 0;
}

public sealed class AvailabilityService
{
    public AvailabilityResult Check(
        AvailabilityRequest request,
        WorkScheduleSettings schedule,
        IEnumerable<AppointmentRecord> appointments,
        IEnumerable<PersonalTimeRecord> personalTimes)
    {
        ArgumentNullException.ThrowIfNull(schedule);
        ArgumentNullException.ThrowIfNull(appointments);
        ArgumentNullException.ThrowIfNull(personalTimes);
        if (request.DurationMinutes <= 0 || request.PreparationMinutes < 0 || request.CleanupMinutes < 0)
            throw new ArgumentException("La duración y los márgenes no son válidos.", nameof(request));

        var conflicts = new List<AvailabilityConflict>();
        var date = request.ServiceStart.Date;
        var workStart = date.Add(schedule.WorkStart);
        var workEnd = date.Add(schedule.WorkEnd);
        if (!schedule.WorkingDays.Contains(date.DayOfWeek) ||
            request.OccupiedStart.Date != date || request.OccupiedEnd.Date != date ||
            request.OccupiedStart < workStart || request.OccupiedEnd > workEnd)
        {
            conflicts.Add(new AvailabilityConflict(
                AvailabilityConflictKind.OutsideWorkSchedule,
                "La preparación, el servicio y la limpieza deben quedar dentro del horario laboral.",
                workStart, workEnd));
        }

        var breakStart = date.Add(schedule.BreakStart);
        var breakEnd = date.Add(schedule.BreakEnd);
        if (Overlaps(request.OccupiedStart, request.OccupiedEnd, breakStart, breakEnd))
        {
            conflicts.Add(new AvailabilityConflict(
                AvailabilityConflictKind.Break,
                "El horario se cruza con el descanso laboral.", breakStart, breakEnd));
        }

        foreach (var appointment in appointments.Where(item =>
                     item.Status == AppointmentStatus.Active && item.Id != request.AppointmentId &&
                     Overlaps(request.OccupiedStart, request.OccupiedEnd, item.OccupiedStart, item.OccupiedEnd)))
        {
            conflicts.Add(new AvailabilityConflict(
                AvailabilityConflictKind.Appointment,
                $"El horario se cruza con la cita {appointment.ServiceName}.",
                appointment.OccupiedStart, appointment.OccupiedEnd));
        }

        foreach (var personalTime in personalTimes.Where(item =>
                     Overlaps(request.OccupiedStart, request.OccupiedEnd, item.Start, item.End)))
        {
            conflicts.Add(new AvailabilityConflict(
                AvailabilityConflictKind.PersonalTime,
                $"El horario se cruza con {personalTime.Title}.",
                personalTime.Start, personalTime.End));
        }

        return new AvailabilityResult(conflicts);
    }

    public IReadOnlyList<DateTime> Suggest(
        AvailabilityRequest request,
        WorkScheduleSettings schedule,
        IEnumerable<AppointmentRecord> appointments,
        IEnumerable<PersonalTimeRecord> personalTimes,
        int maximumSuggestions = 3,
        int intervalMinutes = 15,
        int horizonDays = 14)
    {
        if (maximumSuggestions <= 0 || intervalMinutes <= 0 || horizonDays <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumSuggestions), "Los límites de búsqueda deben ser positivos.");

        var appointmentList = appointments.ToList();
        var personalTimeList = personalTimes.ToList();
        var suggestions = new List<DateTime>(maximumSuggestions);
        var candidate = RoundUp(request.ServiceStart, intervalMinutes);
        var endExclusive = request.ServiceStart.Date.AddDays(horizonDays);

        while (candidate < endExclusive && suggestions.Count < maximumSuggestions)
        {
            var candidateRequest = request with { ServiceStart = candidate };
            if (Check(candidateRequest, schedule, appointmentList, personalTimeList).IsAvailable)
                suggestions.Add(candidate);
            candidate = candidate.AddMinutes(intervalMinutes);
        }

        return suggestions;
    }

    public static bool Overlaps(DateTime firstStart, DateTime firstEnd, DateTime secondStart, DateTime secondEnd) =>
        firstStart < secondEnd && secondStart < firstEnd;

    private static DateTime RoundUp(DateTime value, int intervalMinutes)
    {
        var intervalTicks = TimeSpan.FromMinutes(intervalMinutes).Ticks;
        var roundedTicks = ((value.Ticks + intervalTicks - 1) / intervalTicks) * intervalTicks;
        return new DateTime(roundedTicks, value.Kind);
    }
}
