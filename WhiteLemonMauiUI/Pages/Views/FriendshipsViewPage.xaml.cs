using System.Diagnostics;

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
        var stopwatch = Stopwatch.StartNew();

        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync($"{nameof(DiscoverPeopleViewPage)}?explicitNav=true", false);
        });

        stopwatch.Stop();
        Console.WriteLine($"Navigation to EmptyPage took: {stopwatch.ElapsedMilliseconds}ms");
    }
}