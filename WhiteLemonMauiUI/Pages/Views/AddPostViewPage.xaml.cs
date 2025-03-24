using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views;

public partial class AddPostViewPage : BaseView
{
    private AddPostViewModel _addPostViewModel;
    public AddPostViewPage(AddPostViewModel addPostViewModel)
    {
        InitializeComponent();
        this._addPostViewModel = addPostViewModel;
        this.BindingContext = this._addPostViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ThemeManager.Initialize();
        this._addPostViewModel.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        this._addPostViewModel.OnDisappearing();
    }

}