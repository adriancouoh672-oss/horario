using System.Collections.ObjectModel;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Models;

namespace TiempoEnOrden.App.ViewModels;

public sealed class ClientsViewModel : BaseViewModel
{
    private readonly ILocalDataRepository _repository;
    private readonly AppointmentDraft _draft;
    private readonly INavigationService _navigation;
    private string _searchText = "";
    private bool _showArchived;
    private bool _isContextMode;
    private string _newName = "";
    private string _newPhone = "";
    private string _newNotes = "";

    public ClientsViewModel(ILocalDataRepository repository, AppointmentDraft draft, INavigationService navigation)
    {
        _repository = repository;
        _draft = draft;
        _navigation = navigation;
        AddCommand = new AsyncRelayCommand(AddAsync);
        BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync());
    }

    public ObservableCollection<ClientListItemViewModel> VisibleClients { get; } = [];
    public string SearchText { get => _searchText; set { if (SetProperty(ref _searchText, value)) _ = LoadAsync(); } }
    public bool ShowArchived { get => _showArchived; set { if (SetProperty(ref _showArchived, value)) _ = LoadAsync(); } }
    public bool IsContextMode { get => _isContextMode; set => SetProperty(ref _isContextMode, value); }
    public string NewName { get => _newName; set => SetProperty(ref _newName, value); }
    public string NewPhone { get => _newPhone; set => SetProperty(ref _newPhone, value); }
    public string NewNotes { get => _newNotes; set => SetProperty(ref _newNotes, value); }
    public ICommand AddCommand { get; }
    public ICommand BackCommand { get; }

    public override async Task LoadAsync()
    {
        var clients = await _repository.GetClientsAsync(ShowArchived);
        VisibleClients.Clear();
        foreach (var client in clients.Where(item => string.IsNullOrWhiteSpace(SearchText) || item.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)))
        {
            VisibleClients.Add(new ClientListItemViewModel(client));
        }
    }

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            Message = "Indica el nombre del cliente.";
            return;
        }

        var client = new ClientRecord(Guid.NewGuid(), NewName.Trim(), NewPhone.Trim(), NewNotes.Trim());
        await _repository.SaveClientAsync(client);
        _draft.Client = client;
        NewName = NewPhone = NewNotes = "";
        await LoadAsync();
        Message = "Cliente guardado localmente.";
        if (IsContextMode)
        {
            await _navigation.GoBackAsync();
        }
    }
}

