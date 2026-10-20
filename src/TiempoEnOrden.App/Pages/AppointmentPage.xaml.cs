using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class AppointmentPage : ContentPage, IQueryAttributable
{
    private AppointmentViewModel ViewModel => (AppointmentViewModel)BindingContext;
    private bool _prepared;

    public AppointmentPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<AppointmentViewModel>();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _prepared = true;
        var id = query.TryGetValue("appointmentId", out var raw) && Guid.TryParse(raw?.ToString(), out var parsed) ? parsed : (Guid?)null;
        await ViewModel.BeginAsync(id);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_prepared)
        {
            _prepared = true;
            await ViewModel.BeginAsync(null);
        }
        else
        {
            await ViewModel.LoadAsync();
        }
    }
}
