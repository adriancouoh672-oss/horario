using System.Collections.ObjectModel;
using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class ClientsViewModel : BaseViewModel
{
    private readonly IDemoScheduleStore _store;
    private readonly AppointmentDraft _draft;
    private readonly INavigationService _navigation;
    private string _searchText = "";
    private bool _showArchived;
    private bool _isContextMode;
    private string _newName = "";
    private string _newPhone = "";
    private string _newNotes = "";

    public ClientsViewModel(IDemoScheduleStore store, AppointmentDraft draft, INavigationService navigation)
    {
        _store = store;
        _draft = draft;
        _navigation = navigation;
        AddCommand = new AsyncRelayCommand(AddAsync);
        BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync());
        Refresh();
    }

    public ObservableCollection<DemoClient> VisibleClients { get; } = [];
    public string SearchText { get => _searchText; set { if (SetProperty(ref _searchText, value)) Refresh(); } }
    public bool ShowArchived { get => _showArchived; set { if (SetProperty(ref _showArchived, value)) Refresh(); } }
    public bool IsContextMode { get => _isContextMode; set => SetProperty(ref _isContextMode, value); }
    public string NewName { get => _newName; set => SetProperty(ref _newName, value); }
    public string NewPhone { get => _newPhone; set => SetProperty(ref _newPhone, value); }
    public string NewNotes { get => _newNotes; set => SetProperty(ref _newNotes, value); }
    public ICommand AddCommand { get; }
    public ICommand BackCommand { get; }

    public override void Refresh()
    {
        VisibleClients.Clear();
        foreach (var client in _store.Clients
                     .Where(item => ShowArchived || !item.IsArchived)
                     .Where(item => string.IsNullOrWhiteSpace(SearchText) || item.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)))
        {
            VisibleClients.Add(client);
        }
    }

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            Message = "Indica el nombre del cliente.";
            return;
        }

        var client = new DemoClient(NewName.Trim(), NewPhone.Trim(), NewNotes.Trim());
        _store.AddClient(client);
        _draft.ClientName = client.Name;
        NewName = NewPhone = NewNotes = "";
        Refresh();
        Message = "Cliente agregado solo a la demostración en memoria.";
        if (IsContextMode)
        {
            await _navigation.GoBackAsync();
        }
    }
}
