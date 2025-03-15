namespace WhiteLemonMauiUI
{
    /// <summary>
    /// A static utility class for safely handling navigation requests to prevent multiple navigations occurring simultaneously.
    /// Μια στατική βοηθητική κλάση για την ασφαλή διαχείριση των αιτημάτων πλοήγησης, ώστε να αποφεύγεται η ταυτόχρονη πλοήγηση.
    /// </summary>
    public static class NavigationGuard
    {
        /// <summary>
        /// A flag indicating whether navigation is currently in progress.
        /// Ένα σημαία που υποδεικνύει αν η πλοήγηση είναι σε εξέλιξη.
        /// </summary>
        public static bool _IsNavigate;

        /// <summary>
        /// Executes the provided navigation task safely, ensuring that only one navigation happens at a time.
        /// Εκτελεί με ασφάλεια το παρεχόμενο έργο πλοήγησης, διασφαλίζοντας ότι μόνο μία πλοήγηση θα πραγματοποιηθεί κάθε φορά.
        /// </summary>
        /// <param name="NavigationTask">The asynchronous task that performs the navigation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task SafeNavigateAsyncGuard(Func<Task> NavigationTask)
        {
            if (_IsNavigate)
            {
                return;
            }

            _IsNavigate = true;

            try
            {
                await NavigationTask();
            }
            finally
            {
                _IsNavigate = false;
            }
        }
    }
}
