namespace WhiteLemonMauiUI.Pages.Views;

public partial class MessagesViewPage : ContentPage
{
    public MessagesViewPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        await Task.Delay(300);
        Shell.SetTabBarIsVisible(this, false);
    }

    private async void GotoFriendshipsPage(object sender, TappedEventArgs e)
    {

        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(FriendshipsViewPage), true);
        });

    }
}
