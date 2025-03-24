namespace WhiteLemonMauiUI.Pages.Views;

public partial class WelcomeViewPage : ContentPage
{

    public WelcomeViewPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
    }

    private async void GoToLoginPage(object sender, EventArgs e)
    {

        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginViewPage)}", true);
        });

    }
}