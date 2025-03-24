using WhiteLemonMauiUI.Helpers.Views;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI.Pages.Views
{
    public partial class HomeViewPage : BaseView
    {
        private HomePageViewModel _homePageViewModel;
        public HomeViewPage(HomePageViewModel homePageViewModel)
        {

            InitializeComponent();
            this._homePageViewModel = homePageViewModel;
            this.BindingContext = this._homePageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ThemeManager.Initialize();

            this._homePageViewModel.OnAppearing();

            //await Task.Delay(1500);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this._homePageViewModel.OnDisappearing();
        }

        private async void GotoPostDetailsPage(object sender, TappedEventArgs e)
        {

            await NavigationGuard.SafeNavigateAsyncGuard(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PostDetailsViewPage), animate: true);
            });

        }

        private async void GotoNotificationsViewPAge(object sender, TappedEventArgs e)
        {
            await NavigationGuard.SafeNavigateAsyncGuard(async () =>
            {
                await Shell.Current.GoToAsync(nameof(NotificationViewPage), animate: true);
            });

        }

        private async void GotoMessagesViewPage(object sender, TappedEventArgs e)
        {
            await NavigationGuard.SafeNavigateAsyncGuard(async () =>
            {
                await Shell.Current.GoToAsync(nameof(MessagesViewPage), animate: true);
            });

        }
    }
}


