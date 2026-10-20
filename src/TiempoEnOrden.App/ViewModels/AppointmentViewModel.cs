using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;
using TiempoEnOrden.Core.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class AppointmentViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private readonly AppointmentDraft _draft;
    private readonly INavigationService _navigation;
    private readonly IUserDialogService _dialogs;
    private readonly AvailabilityService _availability;
    private bool _hasChanges;
    private bool _loading;

    public AppointmentViewModel(ILocalDataRepository repository, AppointmentDraft draft, INavigationService navigation, IUserDialogService dialogs, AvailabilityService availability)
    {
        _repository = repository;
        _draft = draft;
        _navigation = navigation;
        _dialogs = dialogs;
        _availability = availability;
        _draft.PropertyChanged += DraftChanged;
        OpenClientCreationCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("client-context?context=appointment"));
        OpenServiceCreationCommand = new AsyncRelayCommand(() => _navigation.GoToAsync("service-context?context=appointment"));
        SaveCommand = new AsyncRelayCommand(SaveAsync);
        CancelAppointmentCommand = new AsyncRelayCommand(CancelAppointmentAsync);
        DiscardCommand = new AsyncRelayCommand(DiscardAsync);
        ApplySuggestionCommand = new RelayCommand<ScheduleSuggestionViewModel>(ApplySuggestion);
    }

    public ObservableCollection<ClientRecord> Clients { get; } = [];
    public ObservableCollection<ServiceRecord> Services { get; } = [];
    public ObservableCollection<ScheduleSuggestionViewModel> Suggestions { get; } = [];

    public ClientRecord? SelectedClient
    {
        get => _draft.Client;
        set { _draft.Client = value; OnPropertyChanged(); }
    }

    public ServiceRecord? SelectedService
    {
        get => _draft.Service;
        set { _draft.Service = value; OnPropertyChanged(); }
    }

    public DateTime StartDate
    {
        get => _draft.Date;
        set { _draft.Date = value; OnPropertyChanged(); }
    }

    public TimeSpan StartTime
    {
        get => _draft.Time;
        set { _draft.Time = value; OnPropertyChanged(); }
    }

    public string Notes
    {
        get => _draft.Notes;
        set { _draft.Notes = value; OnPropertyChanged(); }
    }

    public bool IsEditing => _draft.AppointmentId.HasValue;
    public bool HasSuggestions => Suggestions.Count > 0;
    public string PageTitle => IsEditing ? "Editar cita" : "Nueva cita";
    public string SaveButtonText => IsEditing ? "Guardar cambios" : "Guardar cita";

    public string EndText
    {
        get
        {
            if (SelectedService is null) return "Selecciona un servicio para calcular el fin.";
            var end = StartDate.Date.Add(StartTime).AddMinutes(SelectedService.DurationMinutes);
            return $"Fin del servicio: {end.ToString("dddd d MMM · HH:mm", CultureInfo.GetCultureInfo("es-MX"))}";
        }
    }

    public string OccupationText
    {
        get
        {
            if (SelectedService is null) return "La ocupación mostrará preparación, servicio y limpieza.";
            var start = StartDate.Date.Add(StartTime);
            var occupiedStart = start.AddMinutes(-SelectedService.PreparationMinutes);
            var serviceEnd = start.AddMinutes(SelectedService.DurationMinutes);
            var occupiedEnd = serviceEnd.AddMinutes(SelectedService.CleanupMinutes);
            return $"Preparación {occupiedStart:HH:mm}–{start:HH:mm} · Servicio {start:HH:mm}–{serviceEnd:HH:mm} · Limpieza {serviceEnd:HH:mm}–{occupiedEnd:HH:mm}";
        }
    }

    public ICommand OpenClientCreationCommand { get; }
    public ICommand OpenServiceCreationCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelAppointmentCommand { get; }
    public ICommand DiscardCommand { get; }
    public ICommand ApplySuggestionCommand { get; }

    public async Task BeginAsync(Guid? appointmentId)
    {
        _loading = true;
        try
        {
            await LoadCatalogsAsync();
            if (appointmentId.HasValue)
            {
                var appointment = await _repository.GetAppointmentAsync(appointmentId.Value);
                if (appointment is null)
                {
                    Message = "No se encontró la cita.";
                    return;
                }

                _draft.AppointmentId = appointment.Id;
                _draft.Client = appointment.ClientId.HasValue ? Clients.FirstOrDefault(item => item.Id == appointment.ClientId) : null;
                _draft.Service = Services.FirstOrDefault(item => item.Id == appointment.ServiceId)
                    ?? new ServiceRecord(appointment.ServiceId, appointment.ServiceName, appointment.DurationMinutes,
                        appointment.PreparationMinutes, appointment.CleanupMinutes, appointment.Price, appointment.Currency);
                _draft.Date = appointment.Start.Date;
                _draft.Time = appointment.Start.TimeOfDay;
                _draft.Notes = appointment.Notes;
            }
            else
            {
                ResetDraft();
            }

            Message = "";
            ClearSuggestions();
            _hasChanges = false;
            NotifyAll();
        }
        finally
        {
            _loading = false;
        }
    }

    public override async Task LoadAsync()
    {
        _loading = true;
        try
        {
            var clientId = _draft.Client?.Id;
            var serviceId = _draft.Service?.Id;
            await LoadCatalogsAsync();
            _draft.Client = clientId.HasValue ? Clients.FirstOrDefault(item => item.Id == clientId) : _draft.Client;
            _draft.Service = serviceId.HasValue ? Services.FirstOrDefault(item => item.Id == serviceId) ?? _draft.Service : _draft.Service;
            NotifyAll();
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task LoadCatalogsAsync()
    {
        Clients.Clear();
        foreach (var client in await _repository.GetClientsAsync()) Clients.Add(client);
        Services.Clear();
        foreach (var service in await _repository.GetServicesAsync()) Services.Add(service);
    }

    private async Task SaveAsync()
    {
        if (SelectedService is null)
        {
            Message = "Selecciona un servicio.";
            return;
        }

        var wasEditing = IsEditing;
        var service = SelectedService;
        var appointment = new AppointmentRecord(
            _draft.AppointmentId ?? Guid.NewGuid(),
            SelectedClient?.Id,
            SelectedClient?.Name ?? "Sin cliente",
            service.Id,
            service.Name,
            StartDate.Date.Add(StartTime),
            service.DurationMinutes,
            service.PreparationMinutes,
            service.CleanupMinutes,
            service.Price,
            service.Currency,
            Notes.Trim(),
            AppointmentStatus.Active);

        var request = new AvailabilityRequest(
            appointment.Start, appointment.DurationMinutes, appointment.PreparationMinutes,
            appointment.CleanupMinutes, appointment.Id);
        var schedule = await _repository.GetWorkScheduleAsync();
        var appointments = await _repository.GetAppointmentsAsync(request.OccupiedStart, request.OccupiedEnd);
        var personalTimes = await _repository.GetPersonalTimesAsync(request.OccupiedStart, request.OccupiedEnd);
        var availability = _availability.Check(request, schedule, appointments, personalTimes);
        if (!availability.IsAvailable)
        {
            var conflict = availability.Conflicts[0];
            var horizonStart = request.ServiceStart.Date;
            var horizonEnd = horizonStart.AddDays(15);
            var horizonAppointments = await _repository.GetAppointmentsAsync(horizonStart, horizonEnd);
            var horizonPersonalTimes = await _repository.GetPersonalTimesAsync(horizonStart, horizonEnd);
            var suggestions = _availability.Suggest(request, schedule, horizonAppointments, horizonPersonalTimes);
            Suggestions.Clear();
            foreach (var suggestion in suggestions) Suggestions.Add(new ScheduleSuggestionViewModel(suggestion));
            OnPropertyChanged(nameof(HasSuggestions));
            Message = suggestions.Count == 0
                ? $"No se guardó: {conflict.Description} No se encontraron alternativas en 14 días."
                : $"No se guardó: {conflict.Description} Elige una alternativa para cambiar el borrador.";
            return;
        }

        await _repository.SaveAppointmentAsync(appointment);
        ClearSuggestions();
        _draft.AppointmentId = appointment.Id;
        _hasChanges = false;
        Message = wasEditing ? "Cita actualizada localmente." : "Cita creada localmente.";
        NotifyAll();
    }

    private async Task CancelAppointmentAsync()
    {
        if (!_draft.AppointmentId.HasValue) return;
        if (!await _dialogs.ConfirmAsync("¿Cancelar la cita?", "La cita conservará su historial y liberará el horario.", "Cancelar cita", "Volver")) return;
        var current = await _repository.GetAppointmentAsync(_draft.AppointmentId.Value);
        if (current is null) return;
        await _repository.SaveAppointmentAsync(current with { Status = AppointmentStatus.Cancelled });
        _hasChanges = false;
        Message = "La cita fue cancelada y el horario volvió a estar disponible.";
    }

    private async Task DiscardAsync()
    {
        if (_hasChanges && !await _dialogs.ConfirmAsync("¿Descartar los cambios?", "Los cambios no guardados se perderán.", "Descartar cambios", "Seguir editando")) return;
        ResetDraft();
        _hasChanges = false;
        await _navigation.GoBackAsync();
    }

    private void ResetDraft()
    {
        _draft.AppointmentId = null;
        _draft.Client = null;
        _draft.Service = Services.FirstOrDefault();
        _draft.Date = DateTime.Today;
        _draft.Time = new TimeSpan(10, 0, 0);
        _draft.Notes = "";
        ClearSuggestions();
    }

    private void ApplySuggestion(ScheduleSuggestionViewModel? suggestion)
    {
        if (suggestion is null) return;
        StartDate = suggestion.Start.Date;
        StartTime = suggestion.Start.TimeOfDay;
        ClearSuggestions();
        Message = "Alternativa aplicada al borrador. Revisa y guarda la cita.";
    }

    private void ClearSuggestions()
    {
        Suggestions.Clear();
        OnPropertyChanged(nameof(HasSuggestions));
    }

    private void DraftChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (!_loading) _hasChanges = true;
        OnPropertyChanged(e.PropertyName);
        NotifyCalculated();
    }

    private void NotifyAll()
    {
        OnPropertyChanged(nameof(SelectedClient));
        OnPropertyChanged(nameof(SelectedService));
        OnPropertyChanged(nameof(StartDate));
        OnPropertyChanged(nameof(StartTime));
        OnPropertyChanged(nameof(Notes));
        OnPropertyChanged(nameof(IsEditing));
        OnPropertyChanged(nameof(PageTitle));
        OnPropertyChanged(nameof(SaveButtonText));
        NotifyCalculated();
    }

    private void NotifyCalculated()
    {
        OnPropertyChanged(nameof(EndText));
        OnPropertyChanged(nameof(OccupationText));
    }
}
