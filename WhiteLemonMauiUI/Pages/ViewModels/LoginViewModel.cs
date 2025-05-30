﻿using System.Windows.Input;
using WhiteLemon.Shared.Constants;
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
        private readonly IDataSourceService _dataSourceServices;
        private string? _email;
        private string? _password;
        private string? _message;
        private bool _isPasswordHidden = true;
        #endregion

        public LoginViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this._dataSourceServices = serviceProvider.GetRequiredService<IDataSourceService>();
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

        }

        public void OnDisappearing()
        {
            ClearViewModel();
        }
        public void ClearViewModel()
        {
            this.Email = null;
            this.Password = null;
            this.Message = null;
        }
        private async Task LoginAsync()
        {
            try
            {
                ShowActivityIndicatorPopup();

                await Task.Yield();

                var loginUserRequest = UserMapper.ToLoginUserRequest(Email, Password);

                var result = await this._userService.LoginUserAsync(loginUserRequest);

                if (result.Success)
                {
                    HidActivityIndicatorPopup();

                    if (result.Data != null)
                    {
                        Message = result.Message;

                        ShowDialog("Success", result.Message, "success");

                        var userName = result.Data.Name ?? string.Empty;
                        var userEmail = result.Data.Email ?? string.Empty;

                        await this._dataSourceServices.PreLoadDataAsync(result.Data.UserId, Const.PreloadLimit, result.Data.PhotoUrl, userName, userEmail);

                        await NavigationGuard.SafeNavigateAsyncGuard(async () =>
                        {
                            await Shell.Current.GoToAsync($"///{nameof(HomeViewPage)}", true);
                        });
                    }
                }
                else
                {
                    Message = result.ErrorMessage;
                    ShowDialog("Error", Message, "error");
                    HidActivityIndicatorPopup();
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