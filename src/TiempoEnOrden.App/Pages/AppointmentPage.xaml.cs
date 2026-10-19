using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class AppointmentPage : ContentPage
{
    private AppointmentViewModel ViewModel => (AppointmentViewModel)BindingContext;

    public AppointmentPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<AppointmentViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Refresh();
    }
}
