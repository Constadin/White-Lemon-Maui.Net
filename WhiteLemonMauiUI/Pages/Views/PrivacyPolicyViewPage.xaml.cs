namespace WhiteLemonMauiUI.Pages.Views;

public partial class PrivacyPolicyViewPage : ContentPage
{
    public PrivacyPolicyViewPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        Shell.SetTabBarIsVisible(this, false);
    }
}