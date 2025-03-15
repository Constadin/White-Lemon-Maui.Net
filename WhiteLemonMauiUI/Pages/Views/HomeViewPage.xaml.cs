namespace WhiteLemonMauiUI.Pages.Views
{
    public partial class HomeViewPage : ContentPage
    {
        public HomeViewPage()
        {

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ThemeManager.Initialize();
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


