using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class PersonalTimePage : ContentPage
{
    public PersonalTimePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<PersonalTimeViewModel>();
    }
}
