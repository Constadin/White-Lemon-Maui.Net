namespace WhiteLemonMauiUI.Pages.Views;

public partial class PostDetailsViewPage : ContentPage
{
    public PostDetailsViewPage()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        ThemeManager.Initialize();
    }

    private async void GotoHomePage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..", animate: true);
    }
}