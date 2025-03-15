using CommunityToolkit.Maui.Views;
using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

public partial class RegisterViewPage : BaseView
{
    private RegisterViewModel _registerViewModel;

    public RegisterViewPage(RegisterViewModel registerViewModel)
    {
        InitializeComponent();

        this._registerViewModel = registerViewModel;
        this.BindingContext = this._registerViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Yield();
        ThemeManager.Initialize();

        _ = this._registerViewModel.OnAppearing();
    }

    // Unsubscribe from the event when the page is disappearing
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}
