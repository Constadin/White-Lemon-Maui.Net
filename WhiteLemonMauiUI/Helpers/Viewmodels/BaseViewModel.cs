using CommunityToolkit.Maui.Views;
using WhiteLemonMauiUI.Helpers.Classes;
using WhiteLemonMauiUI.Helpers.Views;

namespace WhiteLemonMauiUI.Helpers.Viewmodels
{
    /// <summary>
    /// Base class for ViewModels that handles common functionality such as showing dialogs and activity indicators.
    /// Βασική κλάση για τα ViewModels που χειρίζεται κοινές λειτουργίες, όπως η εμφάνιση διαλόγων και activity indicators.
    /// </summary>
    public class BaseViewModel : NotifyPropertyChangedClass
    {
        protected readonly IServiceProvider ServiceProvider;
        private Popup? _currentPopup;

        /// <summary>
        /// Event that is triggered to show a dialog popup.
        /// Συμβάν που ενεργοποιείται για να εμφανιστεί το παράθυρο διαλόγου.
        /// </summary>
        public event EventHandler<DialogPopUp>? ShowAlertDialogEvent;

        /// <summary>
        /// Event that is triggered to show an activity indicator popup.
        /// Συμβάν που ενεργοποιείται για να εμφανιστεί το popup του activity indicator.
        /// </summary>
        public event EventHandler<Popup>? ShowActivityIndicatorEvent;

        /// <summary>
        /// Event that is triggered to hide an activity indicator popup.
        /// Συμβάν που ενεργοποιείται για να κρυφτεί το popup του activity indicator.
        /// </summary>
        public event EventHandler<Popup>? HideActivityIndicatorEvent;

        /// <summary>
        /// Initializes the BaseViewModel with the service provider for dependency injection.
        /// Αρχικοποιεί το BaseViewModel με τον provider υπηρεσιών για το dependency injection.
        /// </summary>
        /// <param name="serviceProvider">The service provider for dependency injection.</param>
        public BaseViewModel(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Displays a dialog with a title, message, and type (warning, error, success).
        /// Εμφανίζει ένα παράθυρο διαλόγου με τίτλο, μήνυμα και τύπο (προειδοποίηση, σφάλμα, επιτυχία).
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="message">The message to display in the dialog.</param>
        /// <param name="popupType">The type of the dialog (e.g., warning, error, success).</param>
        protected void ShowDialog(string? title, string? message, string popupType)
        {
            // Δημιουργούμε το dialog
            var dialog = new DialogPopUp(title??string.Empty, message??string.Empty, popupType);

            // Ειδοποιούμε ότι πρέπει να εμφανιστεί το dialog
            ShowAlertDialogEvent?.Invoke(this, dialog);
        }

        /// <summary>
        /// Requests the display of an activity indicator popup.
        /// Ζητάει την εμφάνιση ενός popup activity indicator.
        /// </summary>
        protected void ShowActivityIndicatorPopup()
        {
            var popup = new ActivityIndicatorPopUp();
            this._currentPopup = popup;
            // Ειδοποιούμε ότι πρέπει να εμφανιστεί το activity indicator
            ShowActivityIndicatorEvent?.Invoke(this, this._currentPopup);
        }

        /// <summary>
        /// Requests the hiding of an activity indicator popup.
        /// Ζητάει την απόκρυψη του popup του activity indicator.
        /// </summary>
        protected void HidActivityIndicatorPopup()
        {
            if (this._currentPopup != null)
            {
                // Ειδοποιούμε ότι πρέπει να κρυφτεί το activity indicator
                HideActivityIndicatorEvent?.Invoke(this, this._currentPopup);

                // Clear the reference to the popup
                this._currentPopup = null;
            }
        }
    }
}
