using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class AgendaViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private DateTime _selectedDate = DateTime.Today;
    private bool _isWeek;
    private string _selectedFilter = "Todo";
    private bool _showCancelled;

    public AgendaViewModel(ILocalDataRepository repository, INavigationService navigation)
    {
        _repository = repository;
        ShowDayCommand = new RelayCommand(() => IsWeek = false);
        ShowWeekCommand = new RelayCommand(() => IsWeek = true);
        PreviousCommand = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(IsWeek ? -7 : -1));
        NextCommand = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(IsWeek ? 7 : 1));
        TodayCommand = new RelayCommand(() => SelectedDate = DateTime.Today);
        SelectDayCommand = new RelayCommand<DateTime>(date => SelectedDate = date);
        SelectFilterCommand = new RelayCommand<string>(filter => SelectedFilter = filter ?? "Todo");
        OpenAppointmentCommand = new AsyncRelayCommand(() => navigation.GoToAsync("new-appointment"));
        EditAppointmentCommand = new AsyncRelayCommand<string>(id => navigation.GoToAsync($"new-appointment?appointmentId={Uri.EscapeDataString(id ?? "")}"));
        OpenPersonalTimeCommand = new AsyncRelayCommand(() => navigation.GoToAsync("personal-time"));
    }

    public ObservableCollection<AgendaEntryViewModel> VisibleEntries { get; } = [];
    public ObservableCollection<AgendaEntryViewModel> WeekEntries { get; } = [];
    public ObservableCollection<WeekDayViewModel> WeekDays { get; } = [];
    public IReadOnlyList<string> Filters { get; } = ["Todo", "Negocio", "Personal"];

    public DateTime SelectedDate { get => _selectedDate; set { if (SetProperty(ref _selectedDate, value.Date)) _ = LoadAsync(); } }
    public bool IsWeek { get => _isWeek; set { if (SetProperty(ref _isWeek, value)) { OnPropertyChanged(nameof(IsDay)); OnPropertyChanged(nameof(DateHeader)); _ = LoadAsync(); } } }
    public bool IsDay => !IsWeek;
    public string SelectedFilter { get => _selectedFilter; set { if (SetProperty(ref _selectedFilter, value)) _ = LoadAsync(); } }
    public bool ShowCancelled { get => _showCancelled; set { if (SetProperty(ref _showCancelled, value)) _ = LoadAsync(); } }

    public string DateHeader
    {
        get
        {
            var culture = CultureInfo.GetCultureInfo("es-MX");
            return IsWeek
                ? $"Semana del {StartOfWeek(SelectedDate).ToString("d 'de' MMMM", culture)}"
                : SelectedDate.ToString("dddd d 'de' MMMM", culture);
        }
    }

    public string EntryCountText
    {
        get
        {
            var entries = IsWeek ? WeekEntries : VisibleEntries;
            var count = entries.Select(item => item.Id).Distinct().Count();
            return $"{count} {(count == 1 ? "compromiso" : "compromisos")}";
        }
    }
    public string SelectedDayDetailText => $"Detalle del {SelectedDate.ToString("dddd d 'de' MMMM", CultureInfo.GetCultureInfo("es-MX"))}";
    public DateTime WeekStart => StartOfWeek(SelectedDate);
    public ICommand ShowDayCommand { get; }
    public ICommand ShowWeekCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand TodayCommand { get; }
    public ICommand SelectDayCommand { get; }
    public ICommand SelectFilterCommand { get; }
    public ICommand OpenAppointmentCommand { get; }
    public ICommand EditAppointmentCommand { get; }
    public ICommand OpenPersonalTimeCommand { get; }

    public override async Task LoadAsync()
    {
        var weekStart = StartOfWeek(SelectedDate);
        var appointments = await _repository.GetAppointmentsAsync(weekStart, weekStart.AddDays(7), true);
        var personalTimes = await _repository.GetPersonalTimesAsync(weekStart, weekStart.AddDays(7));
        var schedule = await _repository.GetWorkScheduleAsync();
        var commitments = appointments.Select(ToCommitment)
            .Concat(personalTimes.Select(ToCommitment))
            .Concat(CreateBreaks(schedule, weekStart))
            .ToList();

        WeekDays.Clear();
        for (var index = 0; index < 7; index++)
        {
            var date = weekStart.AddDays(index);
            WeekDays.Add(new(date, date.ToString("ddd", CultureInfo.GetCultureInfo("es-MX")), date.Day.ToString(CultureInfo.InvariantCulture)));
        }

        WeekEntries.Clear();
        foreach (var entry in Filter(commitments).SelectMany(DemoPresentation.Expand).OrderBy(item => item.Start)) WeekEntries.Add(entry);

        VisibleEntries.Clear();
        foreach (var entry in Filter(commitments.Where(item => item.OccupationStart.Date == SelectedDate)).SelectMany(DemoPresentation.Expand).OrderBy(item => item.Start)) VisibleEntries.Add(entry);

        OnPropertyChanged(nameof(DateHeader));
        OnPropertyChanged(nameof(EntryCountText));
        OnPropertyChanged(nameof(SelectedDayDetailText));
        OnPropertyChanged(nameof(WeekStart));
    }

    private IEnumerable<DemoCommitment> Filter(IEnumerable<DemoCommitment> source) => source
        .Where(item => ShowCancelled || !item.IsCancelled)
        .Where(item => SelectedFilter switch
        {
            "Negocio" => item.Kind == DemoCommitmentKind.Business,
            "Personal" => item.Kind is DemoCommitmentKind.Personal or DemoCommitmentKind.Break,
            _ => true
        });

    private static DemoCommitment ToCommitment(AppointmentRecord item) => new(
        item.Id.ToString("D"),
        item.ClientName == "Sin cliente" ? item.ServiceName : $"{item.ServiceName} con {item.ClientName}",
        $"{item.ServiceName} · {item.Currency} {item.Price:0.##} · {(item.Status == AppointmentStatus.Cancelled ? "Cancelada" : "Negocio")}",
        item.OccupiedStart,
        item.OccupiedEnd,
        DemoCommitmentKind.Business,
        item.Start,
        item.ServiceEnd,
        item.Status == AppointmentStatus.Cancelled);

    private static DemoCommitment ToCommitment(PersonalTimeRecord item) => new(
        item.Id.ToString("D"), item.Title,
        item.Kind == PersonalTimeKind.Block ? "Bloqueo personal" : "Actividad personal",
        item.Start, item.End, DemoCommitmentKind.Personal);

    private static IEnumerable<DemoCommitment> CreateBreaks(WorkScheduleSettings settings, DateTime weekStart)
    {
        for (var offset = 0; offset < 7; offset++)
        {
            var date = weekStart.AddDays(offset);
            if (!settings.WorkingDays.Contains(date.DayOfWeek)) continue;
            yield return new DemoCommitment(
                $"break-{date:yyyyMMdd}", "Descanso", "Horario laboral",
                date.Add(settings.BreakStart), date.Add(settings.BreakEnd), DemoCommitmentKind.Break);
        }
    }

    private static DateTime StartOfWeek(DateTime date)
    {
        var difference = ((int)date.DayOfWeek + 6) % 7;
        return date.Date.AddDays(-difference);
    }
}
