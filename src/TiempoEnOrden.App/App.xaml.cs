using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

namespace TiempoEnOrden.App;

public partial class App : Application
{
	private readonly AppShell _shell;

	public App(AppShell shell)
	{
		var presentationCulture = CultureInfo.GetCultureInfo("es-MX");
		CultureInfo.DefaultThreadCurrentCulture = presentationCulture;
		CultureInfo.DefaultThreadCurrentUICulture = presentationCulture;
		InitializeComponent();
		_shell = shell;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(_shell);
	}
}
