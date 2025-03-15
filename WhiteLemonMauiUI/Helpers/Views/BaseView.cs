using CommunityToolkit.Maui.Views;
using WhiteLemonMauiUI.Helpers.Viewmodels;

namespace WhiteLemonMauiUI.Helpers.Views
{
    /// <summary>
    /// A base view class for managing common functionalities such as showing dialogs and activity indicators.
    /// Βασική κλάση για την διαχείριση κοινών λειτουργιών, όπως η εμφάνιση διαλόγων και activity indicators.
    /// </summary>
    public class BaseView : ContentPage
    {
        /// <summary>
        /// Called when the page appears. Subscribes to events of the ViewModel to handle popups and activity indicators.
        /// Καλείται όταν η σελίδα εμφανίζεται. Εγγράφεται στα events του ViewModel για την διαχείριση των popups και των activity indicators.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Check if the BindingContext is a BaseViewModel instance
            if (BindingContext is BaseViewModel viewModel)
            {
                // Subscribe to events for showing dialog and activity indicators
                viewModel.ShowAlertDialogEvent += OnShowDialogEvent;
                viewModel.ShowActivityIndicatorEvent += OnShowActivityIndicatorEvent;
                viewModel.HideActivityIndicatorEvent += OnHideActivityIndicatorEvent;
            }
        }

        /// <summary>
        /// Called when the page disappears. Unsubscribes from events of the ViewModel to prevent memory leaks.
        /// Καλείται όταν η σελίδα εξαφανίζεται. Αποεγγράφει από τα events του ViewModel για να αποφευχθούν διαρροές μνήμης.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Check if the BindingContext is a BaseViewModel instance
            if (BindingContext is BaseViewModel viewModel)
            {
                // Unsubscribe from the events to stop receiving notifications
                viewModel.ShowAlertDialogEvent -= OnShowDialogEvent;
                viewModel.ShowActivityIndicatorEvent -= OnShowActivityIndicatorEvent;
                viewModel.HideActivityIndicatorEvent -= OnHideActivityIndicatorEvent;
            }
        }

        /// <summary>
        /// Handles the event for showing a dialog. Displays the dialog popup asynchronously.
        /// Διαχειρίζεται το event για την εμφάνιση ενός διαλόγου. Εμφανίζει το παράθυρο διαλόγου ασύγχρονα.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="dialog">The dialog object containing information to display.</param>
        private async void OnShowDialogEvent(object? sender, DialogPopUp dialog)
        {
            // Show the dialog popup asynchronously
            await this.ShowPopupAsync(dialog);
        }

        /// <summary>
        /// Handles the event for showing an activity indicator. Displays the popup for the activity indicator.
        /// Διαχειρίζεται το event για την εμφάνιση ενός activity indicator. Εμφανίζει το παράθυρο popup για τον activity indicator.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="popup">The popup object for showing the activity indicator.</param>
        private void OnShowActivityIndicatorEvent(object? sender, Popup popup)
        {
            // Show the activity indicator popup
            this.ShowPopup(popup);
        }

        /// <summary>
        /// Handles the event for hiding an activity indicator. Closes the activity indicator popup.
        /// Διαχειρίζεται το event για την απόκρυψη του activity indicator. Κλείνει το παράθυρο popup του activity indicator.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="popup">The popup object to close.</param>
        private void OnHideActivityIndicatorEvent(object? sender, Popup popup)
        {
            // Close the activity indicator popup
            popup.Close();
        }
    }
}
