using TiempoEnOrden.App.Infrastructure;

namespace TiempoEnOrden.App.ViewModels;

public abstract class BaseViewModel : ObservableObject
{
    private string _message = "";

    public string Message
    {
        get => _message;
        protected set
        {
            if (SetProperty(ref _message, value))
            {
                OnPropertyChanged(nameof(HasMessage));
            }
        }
    }

    public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    public virtual void Refresh()
    {
    }
}
