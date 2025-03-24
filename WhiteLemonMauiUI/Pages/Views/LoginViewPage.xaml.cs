using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

public partial class LoginViewPage : BaseView
{
    private LoginViewModel _loginViewModel;
    public LoginViewPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this._loginViewModel = loginViewModel;
        this.BindingContext = this._loginViewModel;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        ThemeManager.Initialize();

        this._loginViewModel.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this._loginViewModel.OnDisappearing();
    }
}