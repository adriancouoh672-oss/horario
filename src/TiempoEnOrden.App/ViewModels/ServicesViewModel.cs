using System.Collections.ObjectModel;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class ServicesViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private readonly AppointmentDraft _draft;
    private readonly INavigationService _navigation;
    private bool _showArchived;
    private bool _isContextMode;
    private string _newName = "";
    private int _duration = 45;
    private int _preparation = 10;
    private int _cleanup = 5;
    private decimal _price = 250;

    public ServicesViewModel(ILocalDataRepository repository, AppointmentDraft draft, INavigationService navigation)
    {
        _repository = repository;
        _draft = draft;
        _navigation = navigation;
        AddCommand = new AsyncRelayCommand(AddAsync);
        BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync());
    }

    public ObservableCollection<ServiceListItemViewModel> VisibleServices { get; } = [];
    public bool ShowArchived { get => _showArchived; set { if (SetProperty(ref _showArchived, value)) _ = LoadAsync(); } }
    public bool IsContextMode { get => _isContextMode; set => SetProperty(ref _isContextMode, value); }
    public string NewName { get => _newName; set => SetProperty(ref _newName, value); }
    public int Duration { get => _duration; set { if (SetProperty(ref _duration, value)) OnPropertyChanged(nameof(Preview)); } }
    public int Preparation { get => _preparation; set { if (SetProperty(ref _preparation, value)) OnPropertyChanged(nameof(Preview)); } }
    public int Cleanup { get => _cleanup; set { if (SetProperty(ref _cleanup, value)) OnPropertyChanged(nameof(Preview)); } }
    public decimal Price { get => _price; set => SetProperty(ref _price, value); }
    public string Preview => $"Ocupación: {Preparation} + {Duration} + {Cleanup} = {Preparation + Duration + Cleanup} minutos";
    public ICommand AddCommand { get; }
    public ICommand BackCommand { get; }

    public override async Task LoadAsync()
    {
        var services = await _repository.GetServicesAsync(ShowArchived);
        VisibleServices.Clear();
        foreach (var service in services)
        {
            VisibleServices.Add(new ServiceListItemViewModel(service));
        }
    }

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName) || Duration <= 0 || Preparation < 0 || Cleanup < 0 || Price < 0)
        {
            Message = "Revisa nombre, duración, márgenes y precio.";
            return;
        }

        var service = new ServiceRecord(Guid.NewGuid(), NewName.Trim(), Duration, Preparation, Cleanup, Price, "MXN");
        await _repository.SaveServiceAsync(service);
        _draft.Service = new DemoService(service.Name, service.DurationMinutes, service.PreparationMinutes, service.CleanupMinutes, service.Price, service.Currency);
        NewName = "";
        await LoadAsync();
        Message = "Servicio guardado localmente.";
        if (IsContextMode)
        {
            await _navigation.GoBackAsync();
        }
    }
}

