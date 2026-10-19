using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class MorePage : ContentPage
{
    public MorePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<MoreViewModel>();
    }
}
