using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class SettingsViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private TimeSpan _workStart;
    private TimeSpan _workEnd;
    private TimeSpan _breakStart;
    private TimeSpan _breakEnd;
    private bool _monday;
    private bool _tuesday;
    private bool _wednesday;
    private bool _thursday;
    private bool _friday;
    private bool _saturday;
    private bool _sunday;

    public SettingsViewModel(ILocalDataRepository repository)
    {
        _repository = repository;
        SaveCommand = new AsyncRelayCommand(SaveAsync);
    }

    public TimeSpan WorkStart { get => _workStart; set => SetProperty(ref _workStart, value); }
    public TimeSpan WorkEnd { get => _workEnd; set => SetProperty(ref _workEnd, value); }
    public TimeSpan BreakStart { get => _breakStart; set => SetProperty(ref _breakStart, value); }
    public TimeSpan BreakEnd { get => _breakEnd; set => SetProperty(ref _breakEnd, value); }
    public bool Monday { get => _monday; set => SetProperty(ref _monday, value); }
    public bool Tuesday { get => _tuesday; set => SetProperty(ref _tuesday, value); }
    public bool Wednesday { get => _wednesday; set => SetProperty(ref _wednesday, value); }
    public bool Thursday { get => _thursday; set => SetProperty(ref _thursday, value); }
    public bool Friday { get => _friday; set => SetProperty(ref _friday, value); }
    public bool Saturday { get => _saturday; set => SetProperty(ref _saturday, value); }
    public bool Sunday { get => _sunday; set => SetProperty(ref _sunday, value); }
    public string TimeZoneName => "Ciudad de México";
    public string TimeZoneId => "America/Mexico_City";
    public ICommand SaveCommand { get; }

    public override async Task LoadAsync()
    {
        var settings = await _repository.GetWorkScheduleAsync();
        WorkStart = settings.WorkStart;
        WorkEnd = settings.WorkEnd;
        BreakStart = settings.BreakStart;
        BreakEnd = settings.BreakEnd;
        Monday = settings.WorkingDays.Contains(DayOfWeek.Monday);
        Tuesday = settings.WorkingDays.Contains(DayOfWeek.Tuesday);
        Wednesday = settings.WorkingDays.Contains(DayOfWeek.Wednesday);
        Thursday = settings.WorkingDays.Contains(DayOfWeek.Thursday);
        Friday = settings.WorkingDays.Contains(DayOfWeek.Friday);
        Saturday = settings.WorkingDays.Contains(DayOfWeek.Saturday);
        Sunday = settings.WorkingDays.Contains(DayOfWeek.Sunday);
    }

    private async Task SaveAsync()
    {
        var days = new HashSet<DayOfWeek>();
        if (Monday) days.Add(DayOfWeek.Monday);
        if (Tuesday) days.Add(DayOfWeek.Tuesday);
        if (Wednesday) days.Add(DayOfWeek.Wednesday);
        if (Thursday) days.Add(DayOfWeek.Thursday);
        if (Friday) days.Add(DayOfWeek.Friday);
        if (Saturday) days.Add(DayOfWeek.Saturday);
        if (Sunday) days.Add(DayOfWeek.Sunday);

        if (WorkEnd <= WorkStart || BreakEnd <= BreakStart || BreakStart < WorkStart || BreakEnd > WorkEnd || days.Count == 0)
        {
            Message = "Revisa el horario: el descanso debe quedar dentro de la jornada y debe existir al menos un día laboral.";
            return;
        }

        await _repository.SaveWorkScheduleAsync(new WorkScheduleSettings(WorkStart, WorkEnd, BreakStart, BreakEnd, days));
        Message = "Horario laboral y descanso guardados localmente.";
    }
}
