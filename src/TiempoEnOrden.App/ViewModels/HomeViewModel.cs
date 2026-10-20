using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class HomeViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private readonly INavigationService _navigation;

    public HomeViewModel(ILocalDataRepository repository, INavigationService navigation)
    {
        _repository = repository;
        _navigation = navigation;
        OpenAppointmentCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("new-appointment"));
        OpenPersonalTimeCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("personal-time"));
        OpenAgendaCommand = new AsyncRelayCommand(() => _navigation.GoToAsync(NavigationRoutes.Agenda));
    }

    public string Greeting => DateTime.Now.Hour < 12 ? "Buenos días" : DateTime.Now.Hour < 19 ? "Buenas tardes" : "Buenas noches";
    public string CurrentDateText => DateTime.Now.ToString("dddd d 'de' MMMM 'de' yyyy · HH:mm", CultureInfo.GetCultureInfo("es-MX"));
    public string UpcomingCountText { get; private set; } = "0 compromisos pendientes";
    public HomeCommitmentViewModel? NextAppointment { get; private set; }
    public bool HasNextAppointment => NextAppointment is not null;
    public bool HasNoAppointments => NextAppointment is null;
    public ObservableCollection<HomeCommitmentViewModel> TodayItems { get; } = [];
    public ObservableCollection<string> Tasks { get; } = [];
    public ICommand OpenAppointmentCommand { get; }
    public ICommand OpenPersonalTimeCommand { get; }
    public ICommand OpenAgendaCommand { get; }

    public override async Task LoadAsync()
    {
        var now = DateTime.Now;
        var appointments = await _repository.GetAppointmentsAsync(now.Date, now.Date.AddDays(1));
        var personalTimes = await _repository.GetPersonalTimesAsync(now.Date, now.Date.AddDays(1));
        var schedule = await _repository.GetWorkScheduleAsync();
        var commitments = appointments.Select(ToCommitment)
            .Concat(personalTimes.Select(ToCommitment))
            .ToList();
        if (schedule.WorkingDays.Contains(now.DayOfWeek))
        {
            commitments.Add(new DemoCommitment(
                $"break-{now:yyyyMMdd}", "Descanso", "Horario laboral",
                now.Date.Add(schedule.BreakStart), now.Date.Add(schedule.BreakEnd), DemoCommitmentKind.Break));
        }
        commitments = commitments.OrderBy(item => item.OccupationStart).ToList();

        TodayItems.Clear();
        foreach (var item in commitments) TodayItems.Add(DemoPresentation.ToHome(item, now));

        var upcoming = commitments.Where(item => item.OccupationEnd > now).ToList();
        UpcomingCountText = upcoming.Count == 1 ? "1 compromiso pendiente" : $"{upcoming.Count} compromisos pendientes";
        var next = upcoming.FirstOrDefault(item => item.Kind == DemoCommitmentKind.Business);
        NextAppointment = next is null ? null : DemoPresentation.ToHome(next, now);

        OnPropertyChanged(nameof(Greeting));
        OnPropertyChanged(nameof(CurrentDateText));
        OnPropertyChanged(nameof(UpcomingCountText));
        OnPropertyChanged(nameof(NextAppointment));
        OnPropertyChanged(nameof(HasNextAppointment));
        OnPropertyChanged(nameof(HasNoAppointments));
    }

    private static DemoCommitment ToCommitment(AppointmentRecord item) => new(
        item.Id.ToString("D"),
        item.ClientName == "Sin cliente" ? item.ServiceName : $"{item.ServiceName} con {item.ClientName}",
        $"{item.ServiceName} · Negocio",
        item.OccupiedStart,
        item.OccupiedEnd,
        DemoCommitmentKind.Business,
        item.Start,
        item.ServiceEnd);

    private static DemoCommitment ToCommitment(PersonalTimeRecord item) => new(
        item.Id.ToString("D"), item.Title,
        item.Kind == PersonalTimeKind.Block ? "Bloqueo personal" : "Actividad personal",
        item.Start, item.End, DemoCommitmentKind.Personal);
}
