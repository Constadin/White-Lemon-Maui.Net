using System.Windows.Input;
using WhiteLemon.Shared.Constants;
using WhiteLemonMauiUI.Api.ApiConfigMaui;
using WhiteLemonMauiUI.Converters;
using WhiteLemonMauiUI.Helpers.Viewmodels;
using WhiteLemonMauiUI.Mappers;
using WhiteLemonMauiUI.Pages.Views;
using WhiteLemonMauiUI.Users.Interfaces;

namespace WhiteLemonMauiUI.Pages.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private string? _email;
        private string? _userName;
        private string? _password;
        private string? _message;
        private byte[]? _photoUser;
        private readonly string? _defaultUserImage = Const.DefaultUserImage;
        private string? _photoUrl;
        private string? _selectedImage;
        private bool _isPasswordHidden = true;
        #endregion

        public RegisterViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this.RegisterUserIcommand = new Command(async () => await RegisterAsync());
            this.TakePhotoCommand = new Command(async () => await TakePhotoAsync());
        }

        #region Properties

        public ICommand RegisterUserIcommand { get; }
        public ICommand TakePhotoCommand { get; }
        public bool IsPasswordHidden
        {
            get => this._isPasswordHidden;
            set => SetPropertyValue(ref this._isPasswordHidden, value);
        }
        public string? UserName
        {
            get => this._userName;
            set => this.SetPropertyValue(ref this._userName, value);
        }

        public string? Email
        {
            get => this._email;
            set => SetPropertyValue(ref this._email, value?.ToLower());
        }

        public string? Password
        {
            get => this._password;
            set => SetPropertyValue(ref this._password, value);
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
            get => this._selectedImage;
            set => SetPropertyValue(ref this._selectedImage, value);
        }

        public string? PhotoUrl => string.IsNullOrEmpty(SelectedImage) ? _defaultUserImage : SelectedImage;
        #endregion

        #region Lifecycle Methods

        public void OnAppearing()
        {
            //this.PhotoUrl = $"{ApiConfig.BaseAddress}/Uploads/cbe92c82-17c0-4ca0-ab83-f6942ee875f9.png" ?? null;
        }

        public void OnDisappearing()
        {
            this.ClearViewModel();
            //this.PhotoUrl = $"{ApiConfig.BaseAddress}/Uploads/cbe92c82-17c0-4ca0-ab83-f6942ee875f9.png" ?? null;
        }
        public void ClearViewModel()
        {
            UserName = null;
            Email = null;
            Password = null;
            Message = null;
            PhotoUser = null;
            SelectedImage = null;
        }

        //string imageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.6jWd6_OzRDY5fXlzN0tcxAHaJ4%26pid%3DApi&f=1&ipt=1237bf924f7aacae9eb65af90179bba1da4470bd997e096ecc7e3bab49dce7e2&ipo=images";
        public async Task TakePhotoAsync()
        {

            // _= await SetPhotoFromUrlAsync(imageUrl);

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
                        this.PhotoUser = memoryStream.ToArray();

                        // Encode it in Base64
                        // Την κωδικοποιήσουμε σε Base64

                        var base64Photo = Convert.ToBase64String(this.PhotoUser);

                        this.SelectedImage = base64Photo;
                    }
                }
            }
        }

        public async Task RegisterAsync()
        {
            try
            {
                this.ShowActivityIndicatorPopup();

                await Task.Yield();

                string? finalDefaultUserImage = this.PhotoUrl;

                if (this.PhotoUrl == Const.DefaultUserImage)
                {
                    // If it's the default image, convert it to Base64
                    // Αν είναι η προεπιλεγμένη εικόνα, το μετατρέπουμε σε Base64

                    finalDefaultUserImage = await ConvertImage.ConvertDefaultUserImageUrlToBase64(Const.DefaultUserImageUrl);
                }

                var registerUserRequest = UserMapper.ToRegisterUserRequest(UserName, Email, Password, finalDefaultUserImage);

                var result = await this._userService.RegisterUserAsync(registerUserRequest);

                if (result.Success)
                {
                    this.HidActivityIndicatorPopup();

                    if (result.Data != null)
                    {
                        this.Message = result.Message;
                        this.UserName = result.Data.Name;
                        this.SelectedImage = $"{ApiConfig.BaseAddress}{result.Data.PhotoUrl}" ?? Const.DefaultUserImage;

                        OnPropertyChanged(nameof(PhotoUrl));

                        this.ShowDialog("Success", Message, "success");

                        await Task.Yield();

                        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
                        {
                            await Task.Delay(4000);

                            await Shell.Current.GoToAsync($"///{nameof(LoginViewPage)}", animate: true);
                        });
                    }
                }
                else
                {
                    this.Message = result.ErrorMessage;
                    this.ShowDialog("Error", Message, "error");
                    this.HidActivityIndicatorPopup();

                }
            }
            catch (Exception ex)
            {
                //serilog
            }
            finally
            {
                this.HidActivityIndicatorPopup();

            }
        }
        #endregion


        //public async Task<bool> SetPhotoFromUrlAsync(string imageUrl)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(imageUrl))
        //            return false;

        //        // Κατεβάζουμε την εικόνα από το URL
        //        using (HttpClient client = new HttpClient())
        //        {
        //            var response = await client.GetAsync(imageUrl);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var imageBytes = await response.Content.ReadAsByteArrayAsync();

        //                // Αποθηκεύουμε την εικόνα ως byte array
        //                this.PhotoUser = imageBytes;

        //                // Κωδικοποιούμε την εικόνα σε Base64
        //                this.PhotoUrl = Convert.ToBase64String(this.PhotoUser);

        //                return true; // Επιστρέφει true αν η διαδικασία ολοκληρώθηκε επιτυχώς
        //            }
        //            else
        //            {
        //                // Αν το URL δεν είναι έγκυρο ή το αίτημα απέτυχε
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Χειρισμός σφαλμάτων
        //        Console.WriteLine($"Σφάλμα κατά την λήψη της φωτογραφίας: {ex.Message}");
        //        return false;
        //    }
        //}

    }
}
