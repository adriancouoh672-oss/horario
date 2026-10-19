namespace TiempoEnOrden.App.Services;

public interface INavigationService
{
    Task GoToAsync(string route);
    Task GoBackAsync();
}

public sealed class ShellNavigationService : INavigationService
{
    public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);

    public Task GoBackAsync() => Shell.Current.GoToAsync("..");
}
