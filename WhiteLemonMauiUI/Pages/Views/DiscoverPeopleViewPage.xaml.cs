using System.ComponentModel;
using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

[QueryProperty(nameof(ExplicitNav), "explicitNav")]

public partial class DiscoverPeopleViewPage : BaseView, INotifyPropertyChanged
{
    private string _explicitNav = string.Empty;
    private DiscoverPeopleViewModel _discoverPeopleViewModel;

    public new event PropertyChangedEventHandler? PropertyChanged;

    public DiscoverPeopleViewPage(DiscoverPeopleViewModel discoverPeopleViewModel)
    {
        InitializeComponent();
        this._discoverPeopleViewModel = discoverPeopleViewModel;
        this.BindingContext = this._discoverPeopleViewModel;
    }

    public string ExplicitNav
    {
        get => this._explicitNav;
        set
        {
            this._explicitNav = value;
            OnPropertyChanged(nameof(ExplicitNav));
            this.HandleNavigation();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        this._discoverPeopleViewModel.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this._discoverPeopleViewModel.OnDisappearing();
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

    protected override void OnPropertyChanged(string? propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
