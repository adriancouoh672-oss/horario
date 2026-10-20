using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class PersonalTimeViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository; private readonly INavigationService _navigation; private readonly IUserDialogService _dialogs;
    private Guid? _id; private string _title = "", _type = "Actividad personal"; private DateTime _date = DateTime.Today; private TimeSpan _startTime = new(12, 0, 0), _endTime = new(13, 0, 0);
    public PersonalTimeViewModel(ILocalDataRepository repository, INavigationService navigation, IUserDialogService dialogs)
    { _repository = repository; _navigation = navigation; _dialogs = dialogs; SaveCommand = new AsyncRelayCommand(SaveAsync); DeleteCommand = new AsyncRelayCommand(DeleteAsync); BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync()); }
    public IReadOnlyList<string> Types { get; } = ["Actividad personal", "Bloqueo"];
    public string Title { get => _title; set => SetProperty(ref _title, value); } public string Type { get => _type; set => SetProperty(ref _type, value); }
    public DateTime Date { get => _date; set => SetProperty(ref _date, value); } public TimeSpan StartTime { get => _startTime; set => SetProperty(ref _startTime, value); } public TimeSpan EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }
    public bool IsEditing => _id.HasValue; public string PageTitle => IsEditing ? "Editar tiempo personal" : "Tiempo personal"; public string SaveText => IsEditing ? "Guardar cambios" : "Guardar tiempo personal";
    public ICommand SaveCommand { get; } public ICommand DeleteCommand { get; } public ICommand BackCommand { get; }
    public async Task BeginAsync(Guid? id)
    {
        if (!id.HasValue) { _id = null; Title = ""; Type = Types[0]; Date = DateTime.Today; StartTime = new(12,0,0); EndTime = new(13,0,0); Message = ""; Notify(); return; }
        var item = await _repository.GetPersonalTimeAsync(id.Value); if (item is null) { Message = "No se encontró la actividad."; return; }
        _id = item.Id; Title = item.Title; Type = item.Kind == PersonalTimeKind.Block ? "Bloqueo" : "Actividad personal"; Date = item.Start.Date; StartTime = item.Start.TimeOfDay; EndTime = item.End.TimeOfDay; Message = ""; Notify();
    }
    private async Task SaveAsync()
    { if (string.IsNullOrWhiteSpace(Title) || EndTime <= StartTime) { Message = "Revisa el título y asegúrate de que el fin sea posterior al inicio."; return; }
      var record = new PersonalTimeRecord(_id ?? Guid.NewGuid(), Title.Trim(), Type == "Bloqueo" ? PersonalTimeKind.Block : PersonalTimeKind.Activity, Date.Date.Add(StartTime), Date.Date.Add(EndTime)); await _repository.SavePersonalTimeAsync(record); _id = record.Id; Notify(); Message = "Tiempo personal guardado localmente."; }
    private async Task DeleteAsync()
    { if (!_id.HasValue) return; if (!await _dialogs.ConfirmAsync("¿Eliminar tiempo personal?", "Esta actividad dejará de bloquear el horario.", "Eliminar", "Volver")) return; await _repository.DeletePersonalTimeAsync(_id.Value); await _navigation.GoBackAsync(); }
    private void Notify() { OnPropertyChanged(nameof(IsEditing)); OnPropertyChanged(nameof(PageTitle)); OnPropertyChanged(nameof(SaveText)); }
}
