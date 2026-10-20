using Microsoft.Extensions.Logging;

using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.Models;
using TiempoEnOrden.App.Services;
using TiempoEnOrden.App.ViewModels;
using TiempoEnOrden.Core.Data;
using TiempoEnOrden.Core.Services;
using TiempoEnOrden.Infrastructure;

namespace TiempoEnOrden.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<ILocalDataRepository>(_ => new SqliteLocalDataRepository(
			Path.Combine(FileSystem.AppDataDirectory, "tiempo-en-orden.db3")));
		builder.Services.AddSingleton<AvailabilityService>();
		builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
		builder.Services.AddSingleton<IUserDialogService, UserDialogService>();
		builder.Services.AddSingleton<AppointmentDraft>();
		builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<AgendaViewModel>();
		builder.Services.AddSingleton<AppointmentViewModel>();
		builder.Services.AddTransient<PersonalTimeViewModel>();
		builder.Services.AddTransient<ClientsViewModel>();
		builder.Services.AddTransient<ServicesViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();
		builder.Services.AddTransient<MoreViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();
		ServiceHelper.Services = app.Services;
		return app;
	}
}
