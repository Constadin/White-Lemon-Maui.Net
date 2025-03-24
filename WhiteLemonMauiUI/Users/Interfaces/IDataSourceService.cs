using System.Collections.ObjectModel;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Users.Interfaces
{
    /// <summary>
    /// Interface to access data sources for users, including collections for top-rated users,
    /// discoverable people, recent images, and suggested users.
    /// Διασύνδεση για πρόσβαση σε πηγές δεδομένων χρηστών, περιλαμβάνοντας συλλογές για κορυφαίους χρήστες,
    /// χρήστες που προτείνονται για ανακάλυψη, πρόσφατες εικόνες και προτεινόμενους χρήστες.
    /// </summary>
    public interface IDataSourceService
    {
        /// <summary>
        /// Gets the ID of the current user.
        /// Επιστρέφει το ID του τρέχοντος χρήστη.
        /// </summary>
        public Guid CurrentUserId { get; }

        /// <summary>
        /// Gets the profile photo URL of the current user.
        /// Επιστρέφει τη διεύθυνση URL της φωτογραφίας προφίλ του τρέχοντος χρήστη.
        /// </summary>
        public string CurrentProfilePhoto { get; }

        /// <summary>
        /// Gets the name of the current user.
        /// Επιστρέφει το όνομα του τρέχοντος χρήστη.
        /// </summary>
        public string CurrentUserName { get; }

        /// <summary>
        /// Gets the email of the current user.
        /// Επιστρέφει το email του τρέχοντος χρήστη.
        /// </summary>
        public string CurentUserEmail { get; }

        /// <summary>
        /// Gets the collection of top-rated users.
        /// Επιστρέφει τη συλλογή των κορυφαίων χρηστών.
        /// </summary>
        ObservableCollection<PreloadUserLimit> TopRatedUsers { get; }

        /// <summary>
        /// Gets the collection of users suggested for discovery.
        /// Επιστρέφει τη συλλογή των χρηστών που προτείνονται για ανακάλυψη.
        /// </summary>
        ObservableCollection<PreloadUserLimit> DiscoverPeopleUsers { get; }

        /// <summary>
        /// Gets the collection of recently used images.
        /// Επιστρέφει τη συλλογή των πρόσφατα χρησιμοποιημένων εικόνων.
        /// </summary>
        ObservableCollection<PreloadUserLimit> RecentUseImages { get; }

        /// <summary>
        /// Gets the collection of suggested users.
        /// Επιστρέφει τη συλλογή των προτεινόμενων χρηστών.
        /// </summary>
        ObservableCollection<PreloadUserLimit> SuggestedUsers { get; }

        /// <summary>
        /// Preloads data from the API, including the current user's info and data limits.
        /// Προφορτώνει δεδομένα από το API, περιλαμβάνοντας τα στοιχεία του τρέχοντος χρήστη και τα όρια των δεδομένων.
        /// </summary>
        /// <param name="currentUserId">The current user's ID.</param>
        /// <param name="preloadLimit">The limit for preloading data.</param>
        /// <param name="currentProfilePhoto">The current user's profile photo URL.</param>
        /// <param name="currentUserName">The current user's name.</param>
        /// <param name="currentUserEmail">The current user's email.</param>
        Task PreLoadDataAsync(Guid currentUserId, int preloadLimit, string currentProfilePhoto, string currentUserName, string currentUserEmail);
    }
}
