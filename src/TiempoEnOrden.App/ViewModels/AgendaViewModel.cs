using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class AgendaViewModel : BaseViewModel
{
    private readonly IDemoScheduleStore _store;
    private DateTime _selectedDate = DemoScheduleStore.DemoNow.Date;
    private bool _isWeek;
    private string _selectedFilter = "Todo";
    private bool _showCancelled;

    public AgendaViewModel(IDemoScheduleStore store, INavigationService navigation)
    {
        _store = store;
        ShowDayCommand = new RelayCommand(() => IsWeek = false);
        ShowWeekCommand = new RelayCommand(() => IsWeek = true);
        PreviousCommand = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(IsWeek ? -7 : -1));
        NextCommand = new RelayCommand(() => SelectedDate = SelectedDate.AddDays(IsWeek ? 7 : 1));
        TodayCommand = new RelayCommand(() => SelectedDate = DemoScheduleStore.DemoNow.Date);
        SelectDayCommand = new RelayCommand<DateTime>(date => SelectedDate = date);
        SelectFilterCommand = new RelayCommand<string>(filter => SelectedFilter = filter ?? "Todo");
        OpenAppointmentCommand = new AsyncRelayCommand(() => navigation.GoToAsync("new-appointment"));
        OpenPersonalTimeCommand = new AsyncRelayCommand(() => navigation.GoToAsync("personal-time"));
        Refresh();
    }

    public ObservableCollection<AgendaEntryViewModel> VisibleEntries { get; } = [];
    public ObservableCollection<AgendaEntryViewModel> WeekEntries { get; } = [];
    public ObservableCollection<WeekDayViewModel> WeekDays { get; } = [];
    public IReadOnlyList<string> Filters { get; } = ["Todo", "Negocio", "Personal"];

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value.Date))
            {
                Refresh();
            }
        }
    }

    public bool IsWeek
    {
        get => _isWeek;
        set
        {
            if (SetProperty(ref _isWeek, value))
            {
                OnPropertyChanged(nameof(IsDay));
                OnPropertyChanged(nameof(DateHeader));
                Refresh();
            }
        }
    }

    public bool IsDay => !IsWeek;

    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (SetProperty(ref _selectedFilter, value))
            {
                Refresh();
            }
        }
    }

    public bool ShowCancelled
    {
        get => _showCancelled;
        set
        {
            if (SetProperty(ref _showCancelled, value))
            {
                Refresh();
            }
        }
    }

    public string DateHeader
    {
        get
        {
            var culture = new CultureInfo("es-MX");
            if (!IsWeek)
            {
                return SelectedDate.ToString("dddd d 'de' MMMM", culture);
            }

            var start = StartOfWeek(SelectedDate);
            return $"Semana del {start.ToString("d 'de' MMMM", culture)}";
        }
    }

    public string EntryCountText => $"{VisibleEntries.Select(item => item.Id).Distinct().Count()} compromisos";
    public DateTime WeekStart => StartOfWeek(SelectedDate);
    public ICommand ShowDayCommand { get; }
    public ICommand ShowWeekCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand TodayCommand { get; }
    public ICommand SelectDayCommand { get; }
    public ICommand SelectFilterCommand { get; }
    public ICommand OpenAppointmentCommand { get; }
    public ICommand OpenPersonalTimeCommand { get; }

    public override void Refresh()
    {
        var start = StartOfWeek(SelectedDate);
        WeekDays.Clear();
        for (var index = 0; index < 7; index++)
        {
            var date = start.AddDays(index);
            WeekDays.Add(new(date, date.ToString("ddd", new CultureInfo("es-MX")), date.Day.ToString(CultureInfo.InvariantCulture)));
        }

        WeekEntries.Clear();
        foreach (var entry in Filter(_store.Commitments.Where(item => item.OccupationStart.Date >= start && item.OccupationStart.Date < start.AddDays(7))).SelectMany(DemoPresentation.Expand).OrderBy(item => item.Start))
        {
            WeekEntries.Add(entry);
        }

        VisibleEntries.Clear();
        foreach (var entry in Filter(_store.Commitments.Where(item => item.OccupationStart.Date == SelectedDate)).SelectMany(DemoPresentation.Expand).OrderBy(item => item.Start))
        {
            VisibleEntries.Add(entry);
        }

        OnPropertyChanged(nameof(DateHeader));
        OnPropertyChanged(nameof(EntryCountText));
        OnPropertyChanged(nameof(WeekStart));
    }

    private IEnumerable<DemoCommitment> Filter(IEnumerable<DemoCommitment> source) => source
        .Where(item => ShowCancelled || !item.IsCancelled)
        .Where(item => SelectedFilter switch
        {
            "Negocio" => item.Kind == DemoCommitmentKind.Business,
            "Personal" => item.Kind == DemoCommitmentKind.Personal,
            _ => true
        });

    private static DateTime StartOfWeek(DateTime date)
    {
        var difference = ((int)date.DayOfWeek + 6) % 7;
        return date.Date.AddDays(-difference);
    }
}
