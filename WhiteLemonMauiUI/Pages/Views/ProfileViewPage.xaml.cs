namespace WhiteLemonMauiUI.Pages.Views;

public partial class ProfileViewPage : ContentPage
{
    public ProfileViewPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
    }

    private async void GotoSettingsPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SettingsViewPage), animate: true);
        });

    }

}