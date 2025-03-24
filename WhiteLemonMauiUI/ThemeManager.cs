using CommunityToolkit.Maui.Core;
using WhiteLemonMauiUI.Pages.Views;
using Themes = WhiteLemonMauiUI.Resources.Themes;

namespace WhiteLemonMauiUI
{
    /// <summary>
    /// Manages the application's themes and facilitates the switching between light and dark themes.
    /// Διαχειρίζεται τα θέματα της εφαρμογής και διευκολύνει την εναλλαγή μεταξύ του φωτεινού και του σκοτεινού θέματος.
    /// </summary>
    public static class ThemeManager
    {
        // The key for the current topic.
        // Το κλειδί για το τρέχον θέμα.
        private const string ThemeKey = "theme";

        // Το κλειδί για το προηγούμενο θέμα.
        // Το κλειδί για το προηγούμενο θέμα.
        private const string PrevThemeKey = "previous-theme";


        // Dictionary that associates topic names with their ResourceDictionaries.
        // Λεξικό που συσχετίζει ονόματα θεμάτων με τα ResourceDictionaries τους.
        private static readonly IDictionary<string, ResourceDictionary> _themesMap =
            new Dictionary<string, ResourceDictionary>
            {
                [nameof(Themes.Light)] = new Themes.Light(), // Add  Default Theme.
                [nameof(Themes.Dark)] = new Themes.Dark()   // Προσθήκη του Dark θέματος.
            };

    
        static ThemeManager()
        {

            // Subscribe to the event that notifies when the device theme changes
            // Εγγραφή στο γεγονός που ειδοποιεί όταν αλλάζει το θέμα της συσκευής.
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }
        }

        /// <summary>
        /// Initializes the theme of the application based on saved preferences or system settings.
        /// Αρχικοποιεί το θέμα της εφαρμογής με βάση τις αποθηκευμένες προτιμήσεις ή τις ρυθμίσεις του συστήματος.
        /// </summary>
        public static void Initialize()
        {
            // Gets the saved theme from preferences.
            // Παίρνει το αποθηκευμένο θέμα από τις προτιμήσεις.
            var selectedTheme = Preferences.Default.Get<string>(ThemeKey, string.Empty);

            // If no theme is set and the device is in Dark Mode, selects Dark.
            // Αν δεν έχει οριστεί θέμα και η συσκευή είναι σε Dark Mode, επιλέγει το Dark.
            if (string.IsNullOrEmpty(selectedTheme) || Application.Current?
                .PlatformAppTheme == AppTheme.Dark)
            {
                selectedTheme = nameof(Themes.Dark);
            }
            else
            {
                selectedTheme = nameof(Themes.Light);
            }

            // Applies the selected theme or Default if none exists.
            // Εφαρμόζει το θέμα που επιλέχθηκε ή το Default αν δεν υπάρχει.
            SetTheme(selectedTheme ?? nameof(Themes.Light));

            // Update the status bar color based on the theme
            UpdateStatusBarColor(selectedTheme ?? nameof(Themes.Light));
        }

        // Event that is triggered when the selected theme changes.
        // Γεγονός που ενεργοποιείται όταν αλλάζει το επιλεγμένο θέμα.
        public static event EventHandler? SelectedThemeChanged;

        // Επιστρέφει τα διαθέσιμα ονόματα θεμάτων.
        public static string[] ThemeNames => _themesMap.Keys.ToArray();

        // Επιστρέφει τα διαθέσιμα ονόματα θεμάτων.
        // Αποθηκεύει το τρέχον επιλεγμένο θέμα.
        public static string SelectedTheme { get; set; } = nameof(Themes.Light);

        /// <summary>
        /// Handles system-wide theme changes (e.g., switching to Dark Mode).
        /// Διαχειρίζεται τις αλλαγές θέματος του συστήματος (π.χ., μετάβαση σε Dark Mode).
        /// </summary>
        private static void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            // If the device switches to Dark Mode..
            // Αν η συσκευή αλλάξει σε Dark Mode.   
            if (e.RequestedTheme == AppTheme.Dark)
            {  
                // If the current theme is not already Dark, it saves the previous theme.
                // Αν το τρέχον θέμα δεν είναι ήδη το Dark, αποθηκεύει το προηγούμενο θέμα.
                if (SelectedTheme != nameof(Themes.Dark))
                {
                    Preferences.Default.Set(PrevThemeKey, SelectedTheme);
                }

                // Sets Dark Mode.
                // Θέτει το Dark Mode.
                SetTheme(nameof(Themes.Dark));
            }
            else
            {
                //// Gets the previous topic or Default if it doesn't exist.
                // Παίρνει το προηγούμενο θέμα ή το Default αν δεν υπάρχει.
                var prevTheme = Preferences.Default.Get<string>(PrevThemeKey, nameof(Themes.Light));

                // Raises the previous topic.
                // Θέτει το προηγούμενο θέμα.
                SetTheme(prevTheme);
            }
        }

        /// <summary>
        /// Sets the specified theme and applies it across the application.
        /// Θέτει το επιλεγμένο θέμα και το εφαρμόζει σε όλη την εφαρμογή.
        /// </summary>
        /// <param name="themeName">The name of the theme to be applied.</param>
        public static void SetTheme(string themeName)
        {
            // If the selected theme is already the same, it does nothing.
            // Αν το επιλεγμένο θέμα είναι ήδη το ίδιο, δεν κάνει τίποτα.
            if (SelectedTheme == themeName)
                return;


            // Gets the ResourceDictionary of the theme to be applied.
            // Παίρνει το ResourceDictionary του θέματος που πρέπει να εφαρμοστεί.
            var themeToBeApplied = _themesMap[themeName];


            // Clears the existing ResourceDictionaries and adds the new one.
            // Καθαρίζει τα υπάρχοντα ResourceDictionaries και προσθέτει το νέο.
            Application.Current?.Resources.MergedDictionaries.Clear();
            Application.Current?.Resources.MergedDictionaries.Add(themeToBeApplied);

            // Ενημερώνει το επιλεγμένο θέμα.
            SelectedTheme = themeName;

            // Triggers the event that notifies about the topic change.
            // Ενεργοποιεί το γεγονός που ενημερώνει για την αλλαγή θέματος.
            SelectedThemeChanged?.Invoke(null, EventArgs.Empty);

            // Saves the selected theme to preferences.
            // Αποθηκεύει το επιλεγμένο θέμα στις προτιμήσεις.
            Preferences.Default.Set<string>(ThemeKey, themeName);

            // Updates the status bar color based on the theme.
            // Ενημερώνει το χρώμα της γραμμής κατάστασης με βάση το θέμα.
            UpdateStatusBarColor(themeName);
        }

        /// <summary>
        /// Updates the status bar color based on the selected theme.
        /// Ενημερώνει το χρώμα της γραμμής κατάστασης με βάση το επιλεγμένο θέμα.
        /// </summary>
        /// <param name="themeName">The name of the theme being applied.</param>
        private static void UpdateStatusBarColor(string themeName)
        {
            var currentSystemTheme = Application.Current?.RequestedTheme.ToString();

            if (currentSystemTheme != null && themeName != currentSystemTheme)
            {
                themeName = currentSystemTheme;
            }

            var currentPage = Shell.Current.CurrentPage;

            if (currentPage is WelcomeViewPage || currentPage is LoginViewPage)
            {
#if IOS || ANDROID
                if (OperatingSystem.IsAndroidVersionAtLeast(23) || OperatingSystem.IsIOSVersionAtLeast(15))
                {
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Color.FromArgb("#9307f7"));
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.LightContent);
                }
#endif
            }
            else if (themeName == nameof(Themes.Dark))
            {
#if IOS || ANDROID
                if (OperatingSystem.IsAndroidVersionAtLeast(23) || OperatingSystem.IsIOSVersionAtLeast(15))
                {
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Colors.Black);
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.LightContent);
                }
#endif
            }
            else if (themeName == nameof(Themes.Light))
            {
#if IOS || ANDROID
                if (OperatingSystem.IsAndroidVersionAtLeast(23) || OperatingSystem.IsIOSVersionAtLeast(15))
                {
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Colors.White);
                    CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.DarkContent);
                }
#endif
            }
        }
    }
}
