using CommunityToolkit.Maui.Behaviors;

namespace WhiteLemonMauiUI.Controls
{
    /// <summary>
    /// Custom <see cref="Image"/> control that automatically adjusts the tint color based on the application's theme (Dark/Light).
    /// Προσαρμοσμένο control <see cref="Image"/> που προσαρμόζει αυτόματα το χρώμα της εικόνας ανάλογα με το θέμα της εφαρμογής (Dark/Light).
    /// </summary>
    public class TintColorImage : Image
    {
        // Αντικείμενο συμπεριφοράς για την αλλαγή χρώματος εικόνας
        private IconTintColorBehavior? _tintColorBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="TintColorImage"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="TintColorImage"/>.
        /// </summary>
        public TintColorImage()
        {
            // Ελέγχει αν η συμπεριφορά δεν έχει ήδη αρχικοποιηθεί
            if (this._tintColorBehavior == null)
            {
                // Δημιουργεί τη συμπεριφορά για την αλλαγή του χρώματος εικόνας
                this._tintColorBehavior = new IconTintColorBehavior
                {
                    TintColor = GetTintColor() // Ορίζει το αρχικό χρώμα ανάλογα με το θέμα
                };

                // Προσθέτει τη συμπεριφορά στην εικόνα
                Behaviors.Add(this._tintColorBehavior);

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
            // Αν το θέμα είναι Dark, επιστρέφει λευκό χρώμα, διαφορετικά επιστρέφει μαύρο
            return Application.Current?.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        }

        /// <summary>
        /// Destructor for the <see cref="TintColorImage"/> class.
        /// Καταστροφέας για την κλάση <see cref="TintColorImage"/>.
        /// </summary>
        ~TintColorImage()
        {
            // Αποεγγράφεται από το γεγονός αλλαγής θέματος όταν η εικόνα καταστρέφεται
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            }
        }
    }
}
