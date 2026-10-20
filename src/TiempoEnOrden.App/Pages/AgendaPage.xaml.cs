using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;
using System.Windows.Input;

namespace TiempoEnOrden.App.Pages;

public partial class AgendaPage : ContentPage
{
    private AgendaViewModel ViewModel => (AgendaViewModel)BindingContext;
    public ICommand SelectFilterCommand => ViewModel.SelectFilterCommand;
    public ICommand SelectDayCommand => ViewModel.SelectDayCommand;
    public ICommand EditAppointmentCommand => ViewModel.EditAppointmentCommand;
    public ICommand EditPersonalTimeCommand => ViewModel.EditPersonalTimeCommand;

    public AgendaPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<AgendaViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = ViewModel.LoadAsync();
    }
}
