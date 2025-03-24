using System.Windows.Input;
using WhiteLemon.Shared.Constants;
using WhiteLemonMauiUI.Converters;
using WhiteLemonMauiUI.Helpers.Viewmodels;
using WhiteLemonMauiUI.Users.Interfaces;

namespace WhiteLemonMauiUI.Pages.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IDataSourceService _dataSourceServices;
        private string? _email;
        private string? _userName;
        private string? _message;
        private byte[]? _photoUser;
        private readonly string? _defaultUserImage = Const.DefaultUserImage;
        private string? _selectedImage;
        private bool _isPasswordHidden = true;
        #endregion

        public ProfileViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this._dataSourceServices = serviceProvider.GetRequiredService<IDataSourceService>();
            this.UpdateUserIcommand = new Command(async () => await UpdateUserAsync());
            this.ChangePhotoCommand = new Command(async () => await TakePhotoAsync());
        }

        #region Properties

        public ICommand UpdateUserIcommand { get; }
        public ICommand ChangePhotoCommand { get; }
        public Guid CurrentUserId => _dataSourceServices.CurrentUserId;

        public bool IsPasswordHidden
        {
            get => this._isPasswordHidden;
            set => SetPropertyValue(ref this._isPasswordHidden, value);
        }
        public string? UserName
        {
            get => this._dataSourceServices.CurrentUserName;
            set => SetPropertyValue(ref this._userName, value);
        }

        public string? Email
        {
            get => this._dataSourceServices.CurentUserEmail;
            set => SetPropertyValue(ref this._email, value?.ToLower());
        }


        public string? Message
        {
            get => this._message;
            set => SetPropertyValue(ref this._message, value);
        }
        public byte[]? PhotoUser
        {
            get => this._photoUser;
            set => SetPropertyValue(ref this._photoUser, value);
        }


        public string? SelectedImage
        {
            get => this._dataSourceServices.CurrentProfilePhoto;
            set => SetPropertyValue(ref this._selectedImage, value);
        }

        public string? PhotoUrl => string.IsNullOrEmpty(this.SelectedImage) ? this._defaultUserImage : this.SelectedImage;
        #endregion

        #region Lifecycle Methods

        public void OnAppearing()
        {

        }

        public void OnDisappearing()
        {
            //this.ClearViewModel();
        }

        public void ClearViewModel()
        {
            this.UserName = null;
            this.Email = null;
            this.Message = null;
            this.PhotoUser = null;
            this.SelectedImage = null;
        }

        public async Task TakePhotoAsync()
        {

            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Take a photo
                // Λήψη φωτογραφίας

                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Read the photo as a byte array
                    // Διαβάζουμε τη φωτογραφία ως byte array

                    var stream = await photo.OpenReadAsync();

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        PhotoUser = memoryStream.ToArray();

                        // Encode it in Base64
                        // Την κωδικοποιήσουμε σε Base64

                        var base64Photo = Convert.ToBase64String(PhotoUser);

                        this.SelectedImage = base64Photo;
                    }
                }
            }
        }


        public async Task UpdateUserAsync()
        {

            try
            {
                ShowActivityIndicatorPopup();

                await Task.Yield();

                string? finalDefaultUserImage = PhotoUrl;

                if (this.PhotoUrl == Const.DefaultUserImage)
                {
                    // Αν είναι η προεπιλεγμένη εικόνα, το μετατρέπουμε σε Base64
                    finalDefaultUserImage = await ConvertImage.ConvertDefaultUserImageUrlToBase64(Const.DefaultUserImageUrl);
                }




            }
            catch (Exception)
            {
                //serilog
            }
            finally
            {
                HidActivityIndicatorPopup();

            }
        }
        #endregion
    }
}
