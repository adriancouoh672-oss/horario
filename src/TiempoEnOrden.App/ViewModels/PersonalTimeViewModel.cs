using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class PersonalTimeViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private readonly INavigationService _navigation;
    private string _title = "Cita médica";
    private string _type = "Actividad personal";
    private DateTime _date = DateTime.Today;
    private TimeSpan _startTime = new(12, 0, 0);
    private TimeSpan _endTime = new(13, 0, 0);

    public PersonalTimeViewModel(ILocalDataRepository repository, INavigationService navigation)
    {
        _repository = repository;
        _navigation = navigation;
        AddCommand = new AsyncRelayCommand(AddAsync);
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

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(Title) || EndTime <= StartTime)
        {
            Message = "Revisa el título y asegúrate de que el fin sea posterior al inicio.";
            return;
        }

        var record = new PersonalTimeRecord(
            Guid.NewGuid(), Title.Trim(),
            Type == "Bloqueo" ? PersonalTimeKind.Block : PersonalTimeKind.Activity,
            Date.Date.Add(StartTime), Date.Date.Add(EndTime));
        await _repository.SavePersonalTimeAsync(record);
        Message = "Tiempo personal guardado localmente.";
    }
}
