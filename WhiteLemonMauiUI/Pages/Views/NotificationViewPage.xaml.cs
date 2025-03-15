namespace WhiteLemonMauiUI.Pages.Views;

public partial class NotificationViewPage : ContentPage
{
    private const int MaxCharLimit = 30;

    private bool IsExpanded1 = false;
    private bool IsExpanded2 = false;

    private string FullText1 = "hcjhdasd,nc,znxc,znxc,nzx,cnzx h,nzx,cnzxhcjhdanc,znxc,znxc,nz hcjhdasd,nc,znxc,znxc,nzx,cnzx h,nzx,cnzxhcjhdanc,znxc,znxc,nzdasd,nc,znxc,znxc,nzx,cnzx h,nzx,cnzxhcjhdanc,znxc,znxc,nz";
    private string FullText2 = "hcjhdasd,nc,znxc,znxc,nzx,cnzx h,nzx,cnzxhcjhdanc,znxc,znxc,nz hcjhdasd,n";

    public NotificationViewPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        await Task.Delay(500);
        UpdateText();
    }

    private void ShowExpandedText1(object sender, TappedEventArgs e)
    {
        IsExpanded1 = !IsExpanded1;
        UpdateText();
    }

    private void ShowExpandedText2(object sender, TappedEventArgs e)
    {
        IsExpanded2 = !IsExpanded2;
        UpdateText();
    }

    private void UpdateText()
    {
        if (TextLabel != null)
        {
            TextLabel.Text = IsExpanded1
                ? FullText1 + "\n...Close"
                : (FullText1.Length > MaxCharLimit ? FullText1.Substring(0, MaxCharLimit) + "\n... More" : FullText1);
        }

        if (TextLabel2 != null)
        {
            TextLabel2.Text = IsExpanded2
                ? FullText2 + "\n...Close"
                : (FullText2.Length > MaxCharLimit ? FullText2.Substring(0, MaxCharLimit) + "\n... More" : FullText2);
        }
    }
}
