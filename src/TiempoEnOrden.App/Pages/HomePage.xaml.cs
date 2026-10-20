using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;
using System.Windows.Input;

namespace TiempoEnOrden.App.Pages;

public partial class HomePage : ContentPage
{
    private HomeViewModel ViewModel => (HomeViewModel)BindingContext;
    public ICommand OpenAgendaCommand => ViewModel.OpenAgendaCommand;

    public HomePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<HomeViewModel>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.LoadAsync();
    }
}
