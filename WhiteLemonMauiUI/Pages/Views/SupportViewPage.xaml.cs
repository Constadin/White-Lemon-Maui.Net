namespace WhiteLemonMauiUI.Pages.Views;

public partial class SupportViewPage : ContentPage
{
	public SupportViewPage()
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