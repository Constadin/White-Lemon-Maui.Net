using System.Diagnostics;
using WhiteLemonMauiUI.Pages.Views;

namespace WhiteLemonMauiUI
{
    /// <summary>
    /// The class that manages the main Shell of the app, includes primary navigation and registers routes.
    /// Η κλάση που διαχειρίζεται το κύριο Shell της εφαρμογής, περιλαμβάνει τις βασικές πλοηγήσεις και καταχωρεί τα routes.
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// The constructor of the class, initializes the Shell, registers routes, and sets the BindingContext.
        /// Ο κατασκευαστής της κλάσης, αρχικοποιεί το Shell, καταχωρεί τα routes και ορίζει το BindingContext.
        /// </summary>
        public AppShell()
        {
            InitializeComponent(); // Loads the XAML file for the Shell
            RegisterRoutes(); // Registers the routes for page navigation
            BindingContext = this; // Sets the BindingContext of the page to the AppShell itself
        }

        #region Navigate Methods 
        /// <summary>
        /// Method called when the login button is clicked. It navigates to the LoginViewPage.
        /// Μέθοδος που καλείται όταν γίνεται κλικ στο κουμπί login. Πλοηγεί στην σελίδα LoginViewPage.
        /// </summary>
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navigation to the LoginViewPage
            await Shell.Current.GoToAsync($"///{nameof(LoginViewPage)}", true);
        }

        /// <summary>
        /// Called when the handler of the Shell changes. It ensures the navigation event is correctly subscribed.
        /// Καλείται όταν αλλάζει ο handler του Shell. Διασφαλίζει ότι το event πλοήγησης είναι σωστά εγγεγραμμένο.
        /// </summary>
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged(); // Calls the base method

            if (Shell.Current != null)
            {
                Shell.Current.Navigated -= OnShellNavigated; // Unsubscribe from any previous navigation events
                Shell.Current.Navigated += OnShellNavigated; // Subscribe to the new navigation event
            }
        }

        /// <summary>
        /// Handles the Shell navigation event. It removes any pages from the navigation stack when the section changes.
        /// Διαχειρίζεται το event πλοήγησης του Shell. Αφαιρεί τις σελίδες από το stack πλοήγησης όταν αλλάζει η ενότητα.
        /// </summary>
        private void OnShellNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (e.Source == ShellNavigationSource.ShellSectionChanged)
            {
                var currentTab = Shell.Current?.CurrentItem?.CurrentItem;

                if (!string.IsNullOrEmpty(currentTab?.Route))
                {
                    var stack = Shell.Current?.Navigation.NavigationStack;

                    if (stack != null)
                    {
                        foreach (var page in stack.Skip(1).ToList()) // Skips the first page (current page)
                        {
                            Shell.Current?.Navigation.RemovePage(page); // Removes the page from the navigation stack
                        }
                    }
                }
            }
        }
        #endregion

        #region Routing Methods 
        /// <summary>
        /// Registers routes for various pages in the app. Used for navigation.
        /// Καταχωρεί τα routes για διάφορες σελίδες στην εφαρμογή. Χρησιμοποιείται για την πλοήγηση.
        /// </summary>
        private static void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(RealsViewPage), typeof(RealsViewPage));
            Routing.RegisterRoute(nameof(RegisterViewPage), typeof(RegisterViewPage));
            Routing.RegisterRoute(nameof(PostDetailsViewPage), typeof(PostDetailsViewPage));
            Routing.RegisterRoute(nameof(AddPostViewPage), typeof(AddPostViewPage));
            Routing.RegisterRoute(nameof(NotificationViewPage), typeof(NotificationViewPage));
            Routing.RegisterRoute(nameof(MessagesViewPage), typeof(MessagesViewPage));
            Routing.RegisterRoute(nameof(SettingsViewPage), typeof(SettingsViewPage));
            Routing.RegisterRoute(nameof(DiscoverPeopleViewPage), typeof(DiscoverPeopleViewPage));
            Routing.RegisterRoute(nameof(FriendshipsViewPage), typeof(FriendshipsViewPage));
            Routing.RegisterRoute(nameof(HelpViewPage), typeof(HelpViewPage));
            Routing.RegisterRoute(nameof(AboutViewPage), typeof(AboutViewPage));
            Routing.RegisterRoute(nameof(SupportViewPage), typeof(SupportViewPage));
            Routing.RegisterRoute(nameof(PrivacyPolicyViewPage), typeof(PrivacyPolicyViewPage));
        }

        #endregion
    }
}
