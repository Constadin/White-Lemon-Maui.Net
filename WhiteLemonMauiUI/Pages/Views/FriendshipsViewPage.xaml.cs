namespace WhiteLemonMauiUI.Pages.Views;

public partial class FriendshipsViewPage : ContentPage
{
    public FriendshipsViewPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        Shell.SetTabBarIsVisible(this, false);
    }

    private async void GotoDiscoverPeoplePage(object sender, TappedEventArgs e)
    {

        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync($"{nameof(DiscoverPeopleViewPage)}?explicitNav=true", true);
        });

    }
}