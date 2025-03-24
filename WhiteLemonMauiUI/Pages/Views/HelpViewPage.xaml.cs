namespace WhiteLemonMauiUI.Pages.Views;

public partial class HelpViewPage : ContentPage
{
    public HelpViewPage()
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