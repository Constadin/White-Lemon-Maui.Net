namespace WhiteLemonMauiUI.Pages.Views;

public partial class SettingsViewPage : ContentPage
{
    public SettingsViewPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        Shell.SetTabBarIsVisible(this, false);
    }

    private async void GotoHelpPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(HelpViewPage), animate: true);
        });
    }

    private async void GotoAboutPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(AboutViewPage), animate: true);
        });
    }

    private async void GotoSupportPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SupportViewPage), animate: true);
        });
    }
    
    private async void GotoPrivacyPolicyPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(PrivacyPolicyViewPage), animate: true);
        });
    }
    private async void GoToLoginPage(object sender, TappedEventArgs e)
    {       

        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            
            await Shell.Current.GoToAsync($"///{nameof(LoginViewPage)}", true);
            
        });

    }
}