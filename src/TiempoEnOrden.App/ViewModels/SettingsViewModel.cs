using TiempoEnOrden.App.Infrastructure;

namespace TiempoEnOrden.App.ViewModels;

public sealed class SettingsViewModel : BaseViewModel
{
    private bool _weekStartsMonday = true;
    private bool _showCancelled;
    private bool _timeZoneConfirmed = true;

    public bool WeekStartsMonday { get => _weekStartsMonday; set { if (SetProperty(ref _weekStartsMonday, value)) NotifyDemoOnly(); } }
    public bool ShowCancelled { get => _showCancelled; set { if (SetProperty(ref _showCancelled, value)) NotifyDemoOnly(); } }
    public bool TimeZoneConfirmed { get => _timeZoneConfirmed; set { if (SetProperty(ref _timeZoneConfirmed, value)) NotifyDemoOnly(); } }
    public string TimeZoneName => "Ciudad de México";
    public string TimeZoneId => "America/Mexico_City";
    public string WorkSchedule => "Lunes a sábado · 09:00–18:00";
    public string BreakSchedule => "Descanso diario · 14:00–15:00";

    private void NotifyDemoOnly() => Message = "Preferencia modificada solo para esta demostración; no se guarda al reiniciar.";
}
