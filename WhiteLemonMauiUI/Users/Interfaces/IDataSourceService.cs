using System.Collections.ObjectModel;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Users.Interfaces
{
    public interface IDataSourceService
    {

        // Provides access to the two user collections
        // Δίνει πρόσβαση στις δύο συλλογές χρηστών 

        public Guid CurrentUserId { get; }
        public string CurrentProfilePhoto { get; }
        public string CurrentUserName { get; }
        public string CurentUserEmail { get; }
        ObservableCollection<PreloadUserLimit> TopRatedUsers { get; }
        ObservableCollection<PreloadUserLimit> DiscoverPeopleUsers { get; }
        ObservableCollection<PreloadUserLimit> SuggestedUsers { get; }

        // Method to Preload data from the API
        // Μέθοδος για να προφορτώσει τα δεδομένα από το API
        Task PreLoadDataAsync(Guid currentUserId, int preloadLimit, string currentProfilePhoto, string currentUserName, string currentUserEmail);
    }
}
