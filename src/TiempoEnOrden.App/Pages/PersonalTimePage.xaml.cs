using TiempoEnOrden.App.Infrastructure;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Pages;

public partial class PersonalTimePage : ContentPage, IQueryAttributable
{
    private PersonalTimeViewModel ViewModel => (PersonalTimeViewModel)BindingContext;
    private bool _prepared;
    public PersonalTimePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetRequiredService<PersonalTimeViewModel>();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _prepared = true;
        var id = query.TryGetValue("personalTimeId", out var value) && Guid.TryParse(value?.ToString(), out var parsed) ? parsed : (Guid?)null;
        await ViewModel.BeginAsync(id);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_prepared) { _prepared = true; await ViewModel.BeginAsync(null); }
    }
}
