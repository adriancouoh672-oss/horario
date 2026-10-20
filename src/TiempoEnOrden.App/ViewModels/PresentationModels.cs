using TiempoEnOrden.App.Models;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed record ClientListItemViewModel(ClientRecord Record)
{
    public Guid Id => Record.Id;
    public string Name => Record.Name;
    public string Phone => Record.Phone;
    public string Notes => Record.Notes;
    public string StatusText => Record.IsArchived ? "Archivado" : "Activo";
    public override string ToString() => Name;
}

public sealed record ServiceListItemViewModel(ServiceRecord Record)
{
    public Guid Id => Record.Id;
    public string Name => Record.Name;
    public string Summary => $"{Record.DurationMinutes} min · {Record.Currency} {Record.Price:0.##}";
    public string OccupationSummary => $"{Record.PreparationMinutes} min preparación + {Record.DurationMinutes} min servicio + {Record.CleanupMinutes} min limpieza";
    public string StatusText => Record.IsArchived ? "Archivado" : "Activo";
    public override string ToString() => Name;
}

public sealed record HomeCommitmentViewModel(
    string Icon,
    string Title,
    string Subtitle,
    string TimeText,
    string StatusText,
    string BackgroundColor,
    string StrokeColor);

public sealed record WeekDayViewModel(DateTime Date, string DayName, string DayNumber)
{
    public string DisplayText => $"{DayName}\n{DayNumber}";
}

public sealed record ScheduleSuggestionViewModel(DateTime Start)
{
    public string DisplayText => Start.ToString("ddd d MMM · HH:mm", System.Globalization.CultureInfo.GetCultureInfo("es-MX"));
}

public sealed record AgendaEntryViewModel(
    string Id,
    string Icon,
    string Title,
    string Subtitle,
    string SegmentLabel,
    DateTime Start,
    DateTime End,
    DemoCommitmentKind Kind,
    bool IsCancelled,
    string BackgroundColor,
    string StrokeColor)
{
    public string TimeText => $"{Start:HH:mm}–{End:HH:mm}";
    public string AccessibleText => $"{SegmentLabel}. {Title}. {TimeText}. {Subtitle}";
    // Una cita se representa con preparación, servicio y limpieza. Solo el
    // segmento de servicio ofrece la acción para no repetirla tres veces.
    public bool IsEditable => Kind == DemoCommitmentKind.Business && !IsCancelled && SegmentLabel == "Servicio";
}

public static class DemoPresentation
{
    public static IEnumerable<AgendaEntryViewModel> Expand(DemoCommitment item)
    {
        if (item.Kind == DemoCommitmentKind.Business && item.ServiceStart is not null && item.ServiceEnd is not null)
        {
            if (item.OccupationStart < item.ServiceStart)
            {
                yield return Create(item, "Preparación", item.OccupationStart, item.ServiceStart.Value, "🧰", "#FFF3D6", "#B87900");
            }

            yield return Create(item, "Servicio", item.ServiceStart.Value, item.ServiceEnd.Value, "💼", item.IsCancelled ? "#F2F4F5" : "#DCEEFF", item.IsCancelled ? "#76838A" : "#176B87");

            if (item.ServiceEnd < item.OccupationEnd)
            {
                yield return Create(item, "Limpieza", item.ServiceEnd.Value, item.OccupationEnd, "🧹", "#E2F2F5", "#287788");
            }

            yield break;
        }

        var palette = item.Kind switch
        {
            DemoCommitmentKind.Personal => ("👤", "#F0E8FF", "#7352A3", "Personal"),
            DemoCommitmentKind.Break => ("☕", "#FFF3D6", "#9A6500", "Descanso"),
            _ => ("💼", "#DCEEFF", "#176B87", "Negocio")
        };

        yield return Create(item, palette.Item4, item.OccupationStart, item.OccupationEnd, palette.Item1, palette.Item2, palette.Item3);
    }

    public static HomeCommitmentViewModel ToHome(DemoCommitment item, DateTime demoNow)
    {
        var icon = item.Kind switch
        {
            DemoCommitmentKind.Business => "💼",
            DemoCommitmentKind.Personal => "👤",
            _ => "☕"
        };
        var background = item.Kind switch
        {
            DemoCommitmentKind.Business => "#DCEEFF",
            DemoCommitmentKind.Personal => "#F0E8FF",
            _ => "#FFF3D6"
        };
        var stroke = item.Kind switch
        {
            DemoCommitmentKind.Business => "#176B87",
            DemoCommitmentKind.Personal => "#7352A3",
            _ => "#9A6500"
        };

        return new(icon, item.Title, item.Subtitle, $"{item.OccupationStart:HH:mm}–{item.OccupationEnd:HH:mm}", item.OccupationEnd <= demoNow ? "Pasado" : "Próximo", background, stroke);
    }

    private static AgendaEntryViewModel Create(DemoCommitment source, string label, DateTime start, DateTime end, string icon, string background, string stroke) =>
        new(source.Id, icon, source.Title, source.Subtitle, label, start, end, source.Kind, source.IsCancelled, background, stroke);
}
