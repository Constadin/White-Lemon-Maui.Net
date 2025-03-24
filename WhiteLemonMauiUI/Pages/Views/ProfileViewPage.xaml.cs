using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

public partial class ProfileViewPage : BaseView
{
    private ProfileViewModel _profileViewModel;
    public ProfileViewPage(ProfileViewModel profileViewModel)
    {
        InitializeComponent();
        this._profileViewModel = profileViewModel;
        this.BindingContext = this._profileViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();

        this._profileViewModel.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this._profileViewModel.OnDisappearing();
    }
    private async void GotoSettingsPage(object sender, TappedEventArgs e)
    {
        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SettingsViewPage), animate: true);
        });

    }

}