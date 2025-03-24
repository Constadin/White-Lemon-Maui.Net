using System.Collections.ObjectModel;
using WhiteLemon.Shared.Constants;
using WhiteLemonMauiUI.Helpers.Viewmodels;
using WhiteLemonMauiUI.Users.Interfaces;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Pages.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IDataSourceService _dataSourceServices;
        private string? _message;

        #endregion

        public HomePageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this._dataSourceServices = serviceProvider.GetRequiredService<IDataSourceService>();
        }

        #region Properties

        public Guid CurrentUserId => _dataSourceServices.CurrentUserId;
        public ObservableCollection<PreloadUserLimit> TopRatedUsers => _dataSourceServices.TopRatedUsers;
        public ObservableCollection<PreloadUserLimit> SuggestedUsers => _dataSourceServices.SuggestedUsers;
        public string? Message
        {
            get => this._message;
            set => SetPropertyValue(ref this._message, value);
        }

        #endregion

        #region Lifecycle Methods
        public async void OnAppearing()
        {

            // Check if there is data for TopRatedUsers and SuggestedUsers
            // Ελέγχουμε αν υπάρχουν δεδομένα για TopRatedUsers και SuggestedUsers

            if (!this._dataSourceServices.TopRatedUsers.Any() || !this._dataSourceServices.SuggestedUsers.Any())
            {
                // If there is no data, we call the PreloadDataAsync method to load it
                // Αν δεν υπάρχουν δεδομένα, καλούμε τη μέθοδο PreloadDataAsync για να τα φορτώσουμε

                await this._dataSourceServices.PreLoadDataAsync(CurrentUserId, Const.PreloadLimit, string.Empty, string.Empty, string.Empty);
            }
        }

        public void OnDisappearing()
        {
            //this.ClearViewModel();
        }

        public void ClearViewModel()
        {
            this.Message = null;
            this.TopRatedUsers.Clear();
            this.SuggestedUsers.Clear();
        }

        #endregion
    }
}
