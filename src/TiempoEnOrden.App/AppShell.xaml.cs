namespace TiempoEnOrden.App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("new-appointment", typeof(Pages.AppointmentPage));
		Routing.RegisterRoute("personal-time", typeof(Pages.PersonalTimePage));
		Routing.RegisterRoute("client-context", typeof(Pages.ClientsPage));
		Routing.RegisterRoute("service-context", typeof(Pages.ServicesPage));
		Routing.RegisterRoute("more-clients", typeof(Pages.ClientsPage));
		Routing.RegisterRoute("more-services", typeof(Pages.ServicesPage));
		Routing.RegisterRoute("more-settings", typeof(Pages.SettingsPage));
	}

	private async void OnNewAppointmentClicked(object? sender, EventArgs e) =>
		await GoToAsync("new-appointment");
}
