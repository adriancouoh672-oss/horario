using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class HomeViewModel : BaseViewModel
{
    private readonly IDemoScheduleStore _store;
    private readonly INavigationService _navigation;

    public HomeViewModel(IDemoScheduleStore store, INavigationService navigation)
    {
        _store = store;
        _navigation = navigation;
        OpenAppointmentCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("new-appointment"));
        OpenPersonalTimeCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("personal-time"));
        OpenAgendaCommand = new AsyncRelayCommand(() => _navigation.GoToAsync(NavigationRoutes.Agenda));
        Refresh();
    }

    public string Greeting => "Buenos días";
    public string DemoDateText => DemoScheduleStore.DemoNow.ToString("dddd d 'de' MMMM 'de' yyyy · HH:mm", new CultureInfo("es-MX"));
    public string DemoNotice => "Datos ficticios · Los cambios de esta demostración se borran al reiniciar.";
    public string UpcomingCountText { get; private set; } = "";
    public HomeCommitmentViewModel? NextAppointment { get; private set; }
    public ObservableCollection<HomeCommitmentViewModel> TodayItems { get; } = [];
    public ObservableCollection<string> Tasks { get; } = [];
    public ICommand OpenAppointmentCommand { get; }
    public ICommand OpenPersonalTimeCommand { get; }
    public ICommand OpenAgendaCommand { get; }

    public override void Refresh()
    {
        var today = _store.Commitments
            .Where(item => item.OccupationStart.Date == DemoScheduleStore.DemoNow.Date && !item.IsCancelled)
            .OrderBy(item => item.OccupationStart)
            .ToList();

        TodayItems.Clear();
        foreach (var item in today)
        {
            TodayItems.Add(DemoPresentation.ToHome(item, DemoScheduleStore.DemoNow));
        }

        var upcoming = today.Where(item => item.OccupationEnd > DemoScheduleStore.DemoNow).ToList();
        UpcomingCountText = $"{upcoming.Count} compromisos pendientes";
        var next = upcoming.FirstOrDefault(item => item.Kind == DemoCommitmentKind.Business);
        NextAppointment = next is null ? null : DemoPresentation.ToHome(next, DemoScheduleStore.DemoNow);

        Tasks.Clear();
        foreach (var task in _store.Tasks)
        {
            Tasks.Add(task);
        }

        OnPropertyChanged(nameof(UpcomingCountText));
        OnPropertyChanged(nameof(NextAppointment));
    }
}
