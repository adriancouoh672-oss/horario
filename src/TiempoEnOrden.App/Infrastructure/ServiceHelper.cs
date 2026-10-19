using Microsoft.Extensions.DependencyInjection;

namespace TiempoEnOrden.App.Infrastructure;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; set; } = null!;

    public static T GetRequiredService<T>() where T : notnull => Services.GetRequiredService<T>();
}
