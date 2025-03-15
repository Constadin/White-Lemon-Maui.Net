namespace WhiteLemonMauiUI.Pages.Views;

[QueryProperty(nameof(ExplicitNav), "explicitNav")]

public partial class DiscoverPeopleViewPage : ContentPage
{
    private string _explicitNav = string.Empty;
    public DiscoverPeopleViewPage()
    {
        InitializeComponent();
    }

    public string ExplicitNav
    {
        get => this._explicitNav;
        set
        {
            this._explicitNav = value;
            HandleNavigation();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
    }
    private void HandleNavigation()
    {
        if (ExplicitNav == "true")
        {
            Shell.SetTabBarIsVisible(this, false);
        }
        else
        {
            Shell.SetTabBarIsVisible(this, true);
        }
    }
}