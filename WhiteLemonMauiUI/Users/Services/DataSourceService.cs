using System.Collections.ObjectModel;
using WhiteLemonMauiUI.Api.ApiConfigMaui;
using WhiteLemonMauiUI.Users.Interfaces;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Users.Services
{
    public class DataSourceService : IDataSourceService
    {
        public Guid CurrentUserId { get; private set; }
        public string CurrentProfilePhoto { get; private set; } = string.Empty;
        public string CurrentUserName { get; private set; } = string.Empty;
        public string CurentUserEmail { get; private set; } = string.Empty;

        public ObservableCollection<PreloadUserLimit> TopRatedUsers { get; private set; } = new ObservableCollection<PreloadUserLimit>();
        public ObservableCollection<PreloadUserLimit> SuggestedUsers { get; private set; } = new ObservableCollection<PreloadUserLimit>();

        public ObservableCollection<PreloadUserLimit> DiscoverPeopleUsers { get; private set; } = new ObservableCollection<PreloadUserLimit>();

        public ObservableCollection<PreloadUserLimit> RecentUseImages { get; private set; } = new ObservableCollection<PreloadUserLimit>();

        private readonly IUserService _userService;

        public DataSourceService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task PreLoadDataAsync(Guid currentUserId, int preloadLimit, string currentProfilePhoto, string currentUserName, string currentUserEmail)
        {
            CurrentUserId = currentUserId;
            CurrentProfilePhoto = $"{ApiConfig.BaseAddress}{currentProfilePhoto}";
            CurrentUserName = currentUserName;
            CurentUserEmail = currentUserEmail;

            try
            {
                var result = await _userService.PreloadDataUserAsync(currentUserId, preloadLimit);

                if (result.Success && result.Data != null)
                {
                    TopRatedUsers.Clear();
                    foreach (var user in result.Data.topRatedUsers)
                    {
                        TopRatedUsers.Add(new PreloadUserLimit(user.UserId, user.Name, user.FullPhotoUrl, user.MostLikedPostId));
                    }

                    await Task.Delay(1000);

                    DiscoverPeopleUsers.Clear();
                    foreach (var user in result.Data.topRatedUsers)
                    {
                        DiscoverPeopleUsers.Add(new PreloadUserLimit(user.UserId, user.Name, user.FullPhotoUrl, null));
                    }

                    await Task.Delay(1000);

                    RecentUseImages.Clear();
                    foreach (var user in result.Data.topRatedUsers.Take(12))
                    {
                        RecentUseImages.Add(new PreloadUserLimit(user.UserId, user.Name, user.FullPhotoUrl, null));
                    }

                    await Task.Delay(1000);

                    SuggestedUsers.Clear();
                    foreach (var user in result.Data.suggestedUsers.Take(5))
                    {
                        SuggestedUsers.Add(new PreloadUserLimit(user.UserId, user.Name, user.FullPhotoUrl, null));
                    }

                }

            }
            catch (Exception ex)
            {
                // 🛠️ Logging
                Console.WriteLine($"[ERROR] {ex.Message}");

            }
        }
    }
}
