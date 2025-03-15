namespace WhiteLemonMauiUI.Pages.Views;

public partial class AboutViewPage : ContentPage
{
	public AboutViewPage()
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