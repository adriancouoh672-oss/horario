using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class ServicesPage : ContentPage, IQueryAttributable
{
    private ServicesViewModel ViewModel => (ServicesViewModel)BindingContext;

    public ServicesPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<ServicesViewModel>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        ViewModel.IsContextMode = query.TryGetValue("context", out var value) && value?.ToString() == "appointment";

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadAsync();
    }
}
