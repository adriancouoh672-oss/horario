using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class ClientsPage : ContentPage, IQueryAttributable
{
    private ClientsViewModel ViewModel => (ClientsViewModel)BindingContext;

    public ClientsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<ClientsViewModel>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        ViewModel.IsContextMode = query.TryGetValue("context", out var value) && value?.ToString() == "appointment";

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Refresh();
    }
}
