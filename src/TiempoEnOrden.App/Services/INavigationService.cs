namespace TiempoEnOrden.App.Services;

public interface INavigationService
{
    Task GoToAsync(string route);
    Task GoToAgendaAsync();
    Task GoBackAsync();
}

public sealed class ShellNavigationService : INavigationService
{
    public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);

    public async Task GoToAgendaAsync()
    {
        var shell = Shell.Current;
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            var agendaItem = shell.Items.FirstOrDefault(item => item.Route == "agenda");
            if (agendaItem is not null)
            {
                shell.CurrentItem = agendaItem;
                return;
            }
        }
        else
        {
            var mobileItem = shell.Items.FirstOrDefault(item => item.Route == "mobile");
            var agendaSection = mobileItem?.Items.FirstOrDefault(item => item.Route == "mobile-agenda");
            if (mobileItem is not null && agendaSection is not null)
            {
                shell.CurrentItem = mobileItem;
                mobileItem.CurrentItem = agendaSection;
                return;
            }
        }

        await shell.GoToAsync(NavigationRoutes.Agenda);
    }

    public Task GoBackAsync() => Shell.Current.GoToAsync("..");
}
