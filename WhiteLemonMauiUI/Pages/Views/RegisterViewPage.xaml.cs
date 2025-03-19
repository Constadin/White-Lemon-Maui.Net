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

        this._registerViewModel.OnAppearing();
    }
        
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this._registerViewModel.OnDisappearing();
    }
}
