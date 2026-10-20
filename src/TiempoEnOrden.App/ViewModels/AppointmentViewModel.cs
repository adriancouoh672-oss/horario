using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class AppointmentViewModel : BaseViewModel
{
    private readonly IDemoScheduleStore _store;
    private readonly AppointmentDraft _draft;
    private readonly INavigationService _navigation;
    private readonly IUserDialogService _dialogs;
    private bool _hasChanges;
    private bool _isInitialized;

    public AppointmentViewModel(IDemoScheduleStore store, AppointmentDraft draft, INavigationService navigation, IUserDialogService dialogs)
    {
        _store = store;
        _draft = draft;
        _navigation = navigation;
        _dialogs = dialogs;
        _draft.PropertyChanged += DraftChanged;
        OpenClientCreationCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("client-context?context=appointment"));
        OpenServiceCreationCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("service-context?context=appointment"));
        ReviewDraftCommand = new RelayCommand(ReviewDraft);
        DiscardCommand = new AsyncRelayCommand(DiscardAsync);
        Refresh();
    }

    public ObservableCollection<string> Clients { get; } = [];
    public ObservableCollection<DemoService> Services { get; } = [];

    public string? SelectedClient
    {
        get => _draft.ClientName;
        set
        {
            _draft.ClientName = value;
            OnPropertyChanged();
        }
    }

    public DemoService? SelectedService
    {
        get => _draft.Service;
        set
        {
            _draft.Service = value;
            OnPropertyChanged();
        }
    }

    public DateTime StartDate
    {
        get => _draft.Date;
        set
        {
            _draft.Date = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan StartTime
    {
        get => _draft.Time;
        set
        {
            _draft.Time = value;
            OnPropertyChanged();
        }
    }

    public string Notes
    {
        get => _draft.Notes;
        set
        {
            _draft.Notes = value;
            OnPropertyChanged();
        }
    }

    public string EndText
    {
        get
        {
            if (SelectedService is null)
            {
                return "Selecciona un servicio para calcular el fin.";
            }

            var end = StartDate.Date.Add(StartTime).AddMinutes(SelectedService.DurationMinutes);
            return $"Fin del servicio: {end:dddd d MMM · HH:mm}";
        }
    }

    public string OccupationText
    {
        get
        {
            if (SelectedService is null)
            {
                return "La ocupación mostrará preparación, servicio y limpieza.";
            }

            var start = StartDate.Date.Add(StartTime);
            var occupiedStart = start.AddMinutes(-SelectedService.PreparationMinutes);
            var occupiedEnd = start.AddMinutes(SelectedService.DurationMinutes + SelectedService.CleanupMinutes);
            return $"Preparación {occupiedStart:HH:mm}–{start:HH:mm} · Servicio {start:HH:mm}–{start.AddMinutes(SelectedService.DurationMinutes):HH:mm} · Limpieza {start.AddMinutes(SelectedService.DurationMinutes):HH:mm}–{occupiedEnd:HH:mm}";
        }
    }

    public string PersistenceNotice => "Borrador de demostración en memoria. No se guarda al cerrar la aplicación.";
    public ICommand OpenClientCreationCommand { get; }
    public ICommand OpenServiceCreationCommand { get; }
    public ICommand ReviewDraftCommand { get; }
    public ICommand DiscardCommand { get; }

    public override void Refresh()
    {
        var selectedClient = _draft.ClientName;
        var selectedServiceName = _draft.Service?.Name ?? "Corte clásico";

        Clients.Clear();
        foreach (var client in _store.Clients.Where(item => !item.IsArchived))
        {
            Clients.Add(client.Name);
        }

        Services.Clear();
        foreach (var service in _store.Services.Where(item => !item.IsArchived))
        {
            Services.Add(service);
        }

        _draft.ClientName = selectedClient is not null && Clients.Contains(selectedClient) ? selectedClient : Clients.FirstOrDefault();
        _draft.Service = Services.FirstOrDefault(item => item.Name == selectedServiceName) ?? Services.FirstOrDefault();
        OnPropertyChanged(nameof(SelectedClient));
        OnPropertyChanged(nameof(SelectedService));
        NotifyCalculated();
        if (!_isInitialized)
        {
            _hasChanges = false;
            _isInitialized = true;
        }
    }

    private void ReviewDraft()
    {
        Message = "Vista previa actualizada. Esta acción no guardó una cita ni comprobó conflictos.";
    }

    private async Task DiscardAsync()
    {
        if (_hasChanges && !await _dialogs.ConfirmAsync("¿Descartar los cambios?", "El borrador de demostración volverá a sus valores iniciales.", "Descartar cambios", "Seguir editando"))
        {
            return;
        }

        _draft.ClientName = "Ana López";
        _draft.Service = _store.Services.FirstOrDefault(item => item.Name == "Corte clásico");
        _draft.Date = DemoScheduleStore.DemoNow.Date;
        _draft.Time = new TimeSpan(10, 0, 0);
        _draft.Notes = "";
        Message = "";
        _hasChanges = false;
        await _navigation.GoBackAsync();
    }

    private void DraftChanged(object? sender, PropertyChangedEventArgs e)
    {
        _hasChanges = true;
        OnPropertyChanged(e.PropertyName);
        NotifyCalculated();
    }

    private void NotifyCalculated()
    {
        OnPropertyChanged(nameof(EndText));
        OnPropertyChanged(nameof(OccupationText));
    }
}
