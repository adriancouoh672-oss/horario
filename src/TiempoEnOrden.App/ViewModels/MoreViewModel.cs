using System.Windows.Input;
using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Services;

namespace TiempoEnOrden.App.ViewModels;

public sealed class MoreViewModel
{
    public MoreViewModel(INavigationService navigation)
    {
        OpenClientsCommand = new AsyncRelayCommand(() => navigation.GoToAsync("more-clients"));
        OpenServicesCommand = new AsyncRelayCommand(() => navigation.GoToAsync("more-services"));
        OpenSettingsCommand = new AsyncRelayCommand(() => navigation.GoToAsync("more-settings"));
        OpenPersonalTimeCommand = new AsyncRelayCommand(() => navigation.GoToAsync("personal-time"));
    }

    public ICommand OpenClientsCommand { get; }
    public ICommand OpenServicesCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand OpenPersonalTimeCommand { get; }
}
