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
    private readonly IUserDialogService _dialogs;
    private Guid? _editingId;
    private string _searchText = "", _newName = "", _newPhone = "", _newNotes = "";
    private bool _showArchived, _isContextMode;

    public ClientsViewModel(ILocalDataRepository repository, AppointmentDraft draft, INavigationService navigation, IUserDialogService dialogs)
    {
        _repository = repository; _draft = draft; _navigation = navigation; _dialogs = dialogs;
        SaveCommand = new AsyncRelayCommand(SaveAsync);
        EditCommand = new AsyncRelayCommand<Guid>(BeginEditAsync);
        ArchiveCommand = new AsyncRelayCommand<Guid>(ArchiveAsync);
        ResetCommand = new RelayCommand(ResetForm);
        BackCommand = new AsyncRelayCommand(() => _navigation.GoBackAsync());
    }

    public ObservableCollection<ClientListItemViewModel> VisibleClients { get; } = [];
    public string SearchText { get => _searchText; set { if (SetProperty(ref _searchText, value)) _ = LoadAsync(); } }
    public bool ShowArchived { get => _showArchived; set { if (SetProperty(ref _showArchived, value)) _ = LoadAsync(); } }
    public bool IsContextMode { get => _isContextMode; set => SetProperty(ref _isContextMode, value); }
    public string NewName { get => _newName; set => SetProperty(ref _newName, value); }
    public string NewPhone { get => _newPhone; set => SetProperty(ref _newPhone, value); }
    public string NewNotes { get => _newNotes; set => SetProperty(ref _newNotes, value); }
    public bool IsEditing => _editingId.HasValue;
    public string FormTitle => IsEditing ? "Editar cliente" : "Agregar cliente";
    public string SaveText => IsEditing ? "Guardar cambios" : "Guardar cliente";
    public ICommand SaveCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand ArchiveCommand { get; }
    public ICommand ResetCommand { get; }
    public ICommand BackCommand { get; }

    public override async Task LoadAsync()
    {
        var clients = await _repository.GetClientsAsync(ShowArchived);
        VisibleClients.Clear();
        foreach (var client in clients.Where(item => string.IsNullOrWhiteSpace(SearchText) || item.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase))) VisibleClients.Add(new(client));
    }

    private async Task BeginEditAsync(Guid id)
    {
        if (id == Guid.Empty) return;
        var client = await _repository.GetClientAsync(id);
        if (client is null) { Message = "No se encontró el cliente."; return; }
        _editingId = client.Id; NewName = client.Name; NewPhone = client.Phone; NewNotes = client.Notes;
        Message = "Edita los datos y guarda los cambios."; NotifyForm();
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(NewName)) { Message = "Indica el nombre del cliente."; return; }
        var existing = _editingId.HasValue ? await _repository.GetClientAsync(_editingId.Value) : null;
        var client = new ClientRecord(_editingId ?? Guid.NewGuid(), NewName.Trim(), NewPhone.Trim(), NewNotes.Trim(), existing?.IsArchived ?? false);
        await _repository.SaveClientAsync(client); _draft.Client = client;
        var wasEditing = IsEditing; ResetForm(); await LoadAsync();
        Message = wasEditing ? "Cliente actualizado localmente." : "Cliente guardado localmente.";
        if (IsContextMode && !wasEditing) await _navigation.GoBackAsync();
    }

    private async Task ArchiveAsync(Guid id)
    {
        if (id == Guid.Empty) return;
        var client = await _repository.GetClientAsync(id);
        if (client is null) return;
        var archive = !client.IsArchived;
        if (!await _dialogs.ConfirmAsync(archive ? "¿Archivar cliente?" : "¿Reactivar cliente?", archive ? "Conservará el historial y dejará de ofrecerse para nuevas citas." : "Volverá a estar disponible para nuevas citas.", archive ? "Archivar" : "Reactivar", "Volver")) return;
        await _repository.SaveClientAsync(client with { IsArchived = archive });
        if (_editingId == id) ResetForm();
        await LoadAsync(); Message = archive ? "Cliente archivado; su historial se conserva." : "Cliente reactivado.";
    }

    private void ResetForm() { _editingId = null; NewName = NewPhone = NewNotes = ""; NotifyForm(); }
    private void NotifyForm() { OnPropertyChanged(nameof(IsEditing)); OnPropertyChanged(nameof(FormTitle)); OnPropertyChanged(nameof(SaveText)); }
}
