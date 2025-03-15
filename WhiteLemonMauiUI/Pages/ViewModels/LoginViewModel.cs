using System.Windows.Input;
using WhiteLemonMauiUI.Helpers.Viewmodels;
using WhiteLemonMauiUI.Mappers;
using WhiteLemonMauiUI.Pages.Views;
using WhiteLemonMauiUI.Users.Interfaces;

namespace WhiteLemonMauiUI.Pages.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private string? _email;
        private string? _password;
        private string? _message;
        private bool _isPasswordHidden = true;
        #endregion

        public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this.LoginUserIcommand = new Command(async () => await LoginAsync());
            this.ToRegisterPageIcommand = new Command(async () => await ToRegisterPageAsync());
        }

        #region Properties
        public ICommand LoginUserIcommand { get; }
        public ICommand ToRegisterPageIcommand { get; }

        public bool IsPasswordHidden
        {
            get => this._isPasswordHidden;
            set => SetPropertyValue(ref this._isPasswordHidden, value);
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

        #endregion

        #region Lifecycle Methods
        public void OnAppearing()
        {
            ClearViewModel();
        }

        public void ClearViewModel()
        {
            Email = null;
            Password = null;
            Message = null;
        }

        #endregion

        #region Methods
        private async Task LoginAsync()
        {
            try
            {
                this.ShowActivityIndicatorPopup();

                await Task.Yield();

                var loginUserRequest = UserMapper.ToLoginUserRequest(Email, Password);

                var result = await this._userService.LoginUserAsync(loginUserRequest);

                if (result.Success)
                {
                    this.HidActivityIndicatorPopup();

                    if (result.Data != null)
                    {
                        this.Message = result.Message;

                        this.ShowDialog("Success", result.Message, "success");

                        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
                        {
                            //await Shell.Current.GoToAsync($"///{nameof(HomeViewPage)}", true);

                            await Shell.Current.GoToAsync(nameof(RegisterViewPage));
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

        private async Task ToRegisterPageAsync()
        {
            await NavigationGuard.SafeNavigateAsyncGuard(async () =>
            {
                await Shell.Current.GoToAsync(nameof(RegisterViewPage));
            });
        }
        #endregion
    }

}