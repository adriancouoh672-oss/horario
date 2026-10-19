namespace TiempoEnOrden.App.Services;

public static class NavigationRoutes
{
    public static string Agenda => DeviceInfo.Platform == DevicePlatform.WinUI ? "//agenda" : "//mobile/mobile-agenda";
}
