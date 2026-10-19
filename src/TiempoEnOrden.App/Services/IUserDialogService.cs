namespace TiempoEnOrden.App.Services;

public interface IUserDialogService
{
    Task<bool> ConfirmAsync(string title, string message, string accept, string cancel);
}

public sealed class UserDialogService : IUserDialogService
{
    public Task<bool> ConfirmAsync(string title, string message, string accept, string cancel) =>
        Shell.Current.DisplayAlertAsync(title, message, accept, cancel);
}
