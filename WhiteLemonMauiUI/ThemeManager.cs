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
        private const string ThemeKey = "theme"; // Το κλειδί για το τρέχον θέμα.
        private const string PrevThemeKey = "previous-theme"; // Το κλειδί για το προηγούμενο θέμα.

        // Λεξικό που συσχετίζει ονόματα θεμάτων με τα ResourceDictionaries τους.
        private static readonly IDictionary<string, ResourceDictionary> _themesMap =
            new Dictionary<string, ResourceDictionary>
            {
                [nameof(Themes.Light)] = new Themes.Light(), // Προσθήκη του Default θέματος.
                [nameof(Themes.Dark)] = new Themes.Dark()   // Προσθήκη του Dark θέματος.
            };

        // Στατικός κατασκευαστής, εκτελείται όταν φορτώνεται η κλάση.
        static ThemeManager()
        {
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
            // Παίρνει το αποθηκευμένο θέμα από τις προτιμήσεις.
            var selectedTheme = Preferences.Default.Get<string>(ThemeKey, string.Empty);

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

            // Εφαρμόζει το θέμα που επιλέχθηκε ή το Default αν δεν υπάρχει.
            SetTheme(selectedTheme ?? nameof(Themes.Light));

            // Update the status bar color based on the theme
            UpdateStatusBarColor(selectedTheme ?? nameof(Themes.Light));
        }

        // Γεγονός που ενεργοποιείται όταν αλλάζει το επιλεγμένο θέμα.
        public static event EventHandler? SelectedThemeChanged;

        // Επιστρέφει τα διαθέσιμα ονόματα θεμάτων.
        public static string[] ThemeNames => _themesMap.Keys.ToArray();

        // Αποθηκεύει το τρέχον επιλεγμένο θέμα.
        public static string SelectedTheme { get; set; } = nameof(Themes.Light);

        /// <summary>
        /// Handles system-wide theme changes (e.g., switching to Dark Mode).
        /// Διαχειρίζεται τις αλλαγές θέματος του συστήματος (π.χ., μετάβαση σε Dark Mode).
        /// </summary>
        private static void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            // Αν η συσκευή αλλάξει σε Dark Mode.
            if (e.RequestedTheme == AppTheme.Dark)
            {
                // Αν το τρέχον θέμα δεν είναι ήδη το Dark, αποθηκεύει το προηγούμενο θέμα.
                if (SelectedTheme != nameof(Themes.Dark))
                {
                    Preferences.Default.Set(PrevThemeKey, SelectedTheme);
                }
                // Θέτει το Dark Mode.
                SetTheme(nameof(Themes.Dark));
            }
            else
            {
                // Παίρνει το προηγούμενο θέμα ή το Default αν δεν υπάρχει.
                var prevTheme = Preferences.Default.Get<string>(PrevThemeKey, nameof(Themes.Light));

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
            // Αν το επιλεγμένο θέμα είναι ήδη το ίδιο, δεν κάνει τίποτα.
            if (SelectedTheme == themeName)
                return;

            // Παίρνει το ResourceDictionary του θέματος που πρέπει να εφαρμοστεί.
            var themeToBeApplied = _themesMap[themeName];

            // Καθαρίζει τα υπάρχοντα ResourceDictionaries και προσθέτει το νέο.
            Application.Current?.Resources.MergedDictionaries.Clear();
            Application.Current?.Resources.MergedDictionaries.Add(themeToBeApplied);

            // Ενημερώνει το επιλεγμένο θέμα.
            SelectedTheme = themeName;

            // Ενεργοποιεί το γεγονός που ενημερώνει για την αλλαγή θέματος.
            SelectedThemeChanged?.Invoke(null, EventArgs.Empty);

            // Αποθηκεύει το επιλεγμένο θέμα στις προτιμήσεις.
            Preferences.Default.Set<string>(ThemeKey, themeName);

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
