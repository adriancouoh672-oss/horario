using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;
using System.Windows.Input;

namespace TiempoEnOrden.App.Pages;

public partial class ClientsPage : ContentPage, IQueryAttributable
{
    private ClientsViewModel ViewModel => (ClientsViewModel)BindingContext;
    public ICommand EditCommand => ViewModel.EditCommand;
    public ICommand ArchiveCommand => ViewModel.ArchiveCommand;

    public ClientsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<ClientsViewModel>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        ViewModel.IsContextMode = query.TryGetValue("context", out var value) && value?.ToString() == "appointment";

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadAsync();
    }
}
