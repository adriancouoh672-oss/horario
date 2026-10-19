using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class PersonalTimeViewModel : BaseViewModel
{
    private readonly IDemoScheduleStore _store;
    private readonly INavigationService _navigation;
    private string _title = "Cita médica";
    private string _type = "Actividad personal";
    private DateTime _date = DemoScheduleStore.DemoNow.Date.AddDays(1);
    private TimeSpan _startTime = new(12, 0, 0);
    private TimeSpan _endTime = new(13, 0, 0);

    public PersonalTimeViewModel(IDemoScheduleStore store, INavigationService navigation)
    {
        _store = store;
        _navigation = navigation;
        AddCommand = new RelayCommand(AddToDemo);
        BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync());
    }

    public IReadOnlyList<string> Types { get; } = ["Actividad personal", "Bloqueo"];
    public string Title { get => _title; set => SetProperty(ref _title, value); }
    public string Type { get => _type; set => SetProperty(ref _type, value); }
    public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
    public TimeSpan StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }
    public TimeSpan EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }
    public ICommand AddCommand { get; }
    public ICommand BackCommand { get; }

    private void AddToDemo()
    {
        if (string.IsNullOrWhiteSpace(Title) || EndTime <= StartTime)
        {
            Message = "Revisa el título y asegúrate de que el fin sea posterior al inicio.";
            return;
        }

        var start = Date.Date.Add(StartTime);
        var end = Date.Date.Add(EndTime);
        _store.AddPersonalTime(new DemoCommitment($"demo-{Guid.NewGuid():N}", Title.Trim(), $"{Type} · Demostración", start, end, DemoCommitmentKind.Personal));
        Message = "Agregado solo a los datos ficticios en memoria. Se perderá al reiniciar.";
    }
}
