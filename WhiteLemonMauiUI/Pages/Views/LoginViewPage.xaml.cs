using CommunityToolkit.Maui.Views;
using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

public partial class LoginViewPage : BaseView
{
    LoginViewModel _loginViewModel; 
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

        this._loginViewModel.ClearViewModel();
    }


    // Unsubscribe from the event when the page is disappearing
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
       
    }
}