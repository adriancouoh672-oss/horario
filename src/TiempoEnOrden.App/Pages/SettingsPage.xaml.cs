using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class SettingsPage : ContentPage
{
    private SettingsViewModel ViewModel => (SettingsViewModel)BindingContext;

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<SettingsViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = ViewModel.LoadAsync();
    }
}
