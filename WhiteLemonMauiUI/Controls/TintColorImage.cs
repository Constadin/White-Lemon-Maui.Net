using CommunityToolkit.Maui.Behaviors;

namespace WhiteLemonMauiUI.Controls
{
    /// <summary>
    /// Custom <see cref="Image"/> control that automatically adjusts the tint color based on the application's theme (Dark/Light).
    /// Προσαρμοσμένο control <see cref="Image"/> που προσαρμόζει αυτόματα το χρώμα της εικόνας ανάλογα με το θέμα της εφαρμογής (Dark/Light).
    /// </summary>
    public class TintColorImage : Image
    {
        // Behavior object for changing image color
        // Αντικείμενο συμπεριφοράς για την αλλαγή χρώματος εικόνας
        private IconTintColorBehavior? _tintColorBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="TintColorImage"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="TintColorImage"/>.
        /// </summary>
        public TintColorImage()
        {
            // Checks if the behavior has not already been initialized
            // Ελέγχει αν η συμπεριφορά δεν έχει ήδη αρχικοποιηθεί
            if (this._tintColorBehavior == null)
            {
                // Creates the behavior for changing the image color
                // Δημιουργεί τη συμπεριφορά για την αλλαγή του χρώματος εικόνας
                this._tintColorBehavior = new IconTintColorBehavior
                {
                    TintColor = GetTintColor() // Sets the initial color depending on the theme // Ορίζει το αρχικό χρώμα ανάλογα με το θέμα
                };

                // Adds the behavior to the image
                // Προσθέτει τη συμπεριφορά στην εικόνα
                Behaviors.Add(this._tintColorBehavior);

                // If the application is not null, it registers for the topic change event
                // Αν η εφαρμογή δεν είναι null, εγγράφεται στο γεγονός αλλαγής θέματος
                if (Application.Current != null)
                {
                    Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
                }
            }
        }

        /// <summary>
        /// Event handler for when the requested theme changes (Dark/Light).
        /// Διαχειριστής συμβάντος όταν αλλάζει το θέμα της εφαρμογής (Dark/Light).
        /// </summary>
        private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            // If the behavior exists, updates the image color according to the new theme
            // Αν η συμπεριφορά υπάρχει, ενημερώνει το χρώμα της εικόνας σύμφωνα με το νέο θέμα
            if (this._tintColorBehavior != null)
            {
                this._tintColorBehavior.TintColor = GetTintColor();
            }
        }

        /// <summary>
        /// Returns the tint color based on the current theme.
        /// Επιστρέφει το χρώμα τόνου ανάλογα με το τρέχον θέμα.
        /// </summary>
        private Color GetTintColor()
        {
            // If the theme is Dark, return white color, otherwise return black
            // Αν το θέμα είναι Dark, επιστρέφει λευκό χρώμα, διαφορετικά επιστρέφει μαύρο
            return Application.Current?.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        }

        /// <summary>
        /// Destructor for the <see cref="TintColorImage"/> class.
        /// Καταστροφέας για την κλάση <see cref="TintColorImage"/>.
        /// </summary>
        ~TintColorImage()
        {
            // Unregisters from the theme change event when the image is destroyed
            // Αποεγγράφεται από το γεγονός αλλαγής θέματος όταν η εικόνα καταστρέφεται
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            }
        }
    }
}
