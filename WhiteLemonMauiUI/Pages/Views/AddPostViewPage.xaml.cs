namespace WhiteLemonMauiUI.Pages.Views;

public partial class AddPostViewPage : ContentPage
{
    public AddPostViewPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
    }
}