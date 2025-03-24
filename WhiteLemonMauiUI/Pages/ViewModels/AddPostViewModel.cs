using System.Collections.ObjectModel;
using System.Windows.Input;
using WhiteLemon.Shared.Constants;
using WhiteLemonMauiUI.Api.ApiConfigMaui;
using WhiteLemonMauiUI.Helpers.Viewmodels;
using WhiteLemonMauiUI.Mappers;
using WhiteLemonMauiUI.Pages.ComponetViewModels;
using WhiteLemonMauiUI.Pages.ComponetViews;
using WhiteLemonMauiUI.Users.Interfaces;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Pages.ViewModels
{
    public class AddPostViewModel : BaseViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IDataSourceService _dataSourceServices;
        private List<byte[]>? _photoPost;
        private readonly string? _defaultPostImage = Const.DefaultPostImage;
        private ObservableCollection<string> _selectedPostImages = new();
        private ObservableCollection<string> _photoUrlImages = new();
        private string? _message;
        private string? _postTitle;
        private string? _postContent;
        private string? _postMessage;
        private string? _selectedMode;
        private bool _isEntryEnabled = false;
        #endregion

        public AddPostViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this._userService = serviceProvider.GetRequiredService<IUserService>();
            this._dataSourceServices = serviceProvider.GetRequiredService<IDataSourceService>();
            this.ChoosePhotoCommand = new Command(async () => await OpenBottomSheetAsync());
            this.DeletePhotoCommand = new Command(async () => await DeletePostImageAsync());
            this.AddPostICommand = new Command(async () => await AddPostAsync());
            this.ResetCommand = new Command(() => Resetform());
            this.SetTitleCommand = new Command(() => SelectMode("Title"));
            this.SetContentCommand = new Command(() => SelectMode("Content"));
            UpdatePhotoUrl();
        }

        #region Properties
        public ICommand ChoosePhotoCommand { get; }
        public ICommand DeletePhotoCommand { get; }
        public ICommand AddPostICommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand SetTitleCommand { get; }
        public ICommand SetContentCommand { get; }
        public ObservableCollection<PreloadUserLimit> RecentUseImages => _dataSourceServices.RecentUseImages;
        public Guid CurrentUserId => _dataSourceServices.CurrentUserId;

        public string? PostTitle
        {
            get => this._postTitle;
            set => SetPropertyValue(ref this._postTitle, value);
        }
        public string? PostContent
        {
            get => this._postContent;
            set => SetPropertyValue(ref this._postContent, value);
        }

        public string? PostMessage
        {
            get => this._postMessage;
            set
            {
                if (SetPropertyValue(ref this._postMessage, value) && !string.IsNullOrWhiteSpace(value))
                {
                    if (SelectedMode == "Title")
                    {
                        PostTitle = value;
                    }
                    else if (SelectedMode == "Content")
                    {
                        PostContent = value;
                    }
                }
            }
        }

        private string? SelectedMode
        {
            get => this._selectedMode;
            set => SetPropertyValue(ref this._selectedMode, value);
        }

        public bool IsEntryEnabled
        {
            get => this._isEntryEnabled;
            set => SetPropertyValue(ref this._isEntryEnabled, value);
        }

        public string? Message
        {
            get => this._message;
            set => SetPropertyValue(ref this._message, value);
        }

        public List<byte[]>? PhotoPost
        {
            get => this._photoPost;
            set => SetPropertyValue(ref this._photoPost, value);
        }
        public ObservableCollection<string> SelectedPostImages
        {
            get => this._selectedPostImages;
            set
            {
                if (SetPropertyValue(ref this._selectedPostImages, value))
                {
                    UpdatePhotoUrl();
                }
            }
        }

        public ObservableCollection<string> PhotoUrlImages
        {
            get => this._photoUrlImages;
            set => SetPropertyValue(ref this._photoUrlImages, value);
        }
        #endregion

        #region Lifecycle Methods
        public void OnAppearing() { }

        public void OnDisappearing()
        {
            if (Shell.Current.Navigation.ModalStack.LastOrDefault() is ButtomSheetComponet bottomSheetPage &&
                bottomSheetPage.BindingContext is ButtomSheetComponetViewModel viewModel)
            {
                viewModel.ListPhotoBytes -= ListOnPhotoTaken;
                viewModel.ListBase64EncodedImages -= ListOnBase64Encoded;
            }
        }

        public void Resetform()
        {
            this.PostContent = null;
            this.PostTitle = null;
            this.PostMessage = null;
        }

        public void ClearViewModel()
        {
            this.Message = null;
            this.PostTitle = null;
            this.PostContent = null;
            this.PostMessage = null;
            this.PhotoPost = null;
            this.PostMessage = null;
            this.PhotoUrlImages.Clear();
        }
        #endregion

        #region Methods

        private void SelectMode(string mode)
        {
            this.SelectedMode = mode;
            this.IsEntryEnabled = true;
            this.PostMessage = string.Empty;
        }

        /// <summary>
        /// Opens the bottom sheet to select a photo.
        /// Ανοίγει το κάτω φύλλο επιλογής για επιλογή φωτογραφίας.
        /// </summary>
        private async Task OpenBottomSheetAsync()
        {
            var bottomSheetPage = new ButtomSheetComponet("AddPostViewModel");
            await Shell.Current.Navigation.PushModalAsync(bottomSheetPage);

            if (bottomSheetPage.BindingContext is ButtomSheetComponetViewModel viewModel)
            {
                viewModel.ListPhotoBytes += ListOnPhotoTaken;
                viewModel.ListBase64EncodedImages += ListOnBase64Encoded;
            }
        }

        private void ListOnPhotoTaken(object? sender, (List<byte[]>, string) data)
        {
            var (photoBytesList, callerId) = data;

            if (callerId == "AddPostViewModel")
            {
                PhotoPost = photoBytesList;
            }
        }

        private void ListOnBase64Encoded(object? sender, (List<string>, string) data)
        {
            var (base64List, callerId) = data;

            if (callerId == "AddPostViewModel")
            {
                SelectedPostImages.Clear();
                foreach (var image in base64List)
                {
                    SelectedPostImages.Add(image);
                }
            }
        }

        private async Task DeletePostImageAsync()
        {
            ClearViewModel();
            await Task.Delay(200);
        }


        private void UpdatePhotoUrl()
        {
            this._photoUrlImages.Clear();
            if (this._selectedPostImages.Count == 0)
            {
                _photoUrlImages.Add(this._defaultPostImage ?? string.Empty);
            }
            else
            {
                foreach (var img in this._selectedPostImages)
                {

                    this._photoUrlImages.Add(img);
                }
            }
            OnPropertyChanged(nameof(PhotoUrlImages));
        }

        /// <summary>
        /// Adds a new post asynchronously.
        /// Προσθέτει μια νέα δημοσίευση ασύγχρονα.
        /// </summary>
        /// If there are no selected images, send a placeholder image or an empty string.
        // This ensures that the post has a valid structure even without images.
        // Alternatively, you can use a placeholder URL, e.g.:
        // postImages.Add(new PostImageDto(Guid.NewGuid(), postId, "https://picsum.photos/150"));

        /// <summary>
        /// Adds a placeholder image or an empty string if no images are selected.
        /// Ensures post structure remains valid even without images.
        /// </summary>
        /// <summary>
        /// Προσθέτει μια placeholder εικόνα ή ένα κενό string αν δεν υπάρχουν εικόνες.
        /// Διασφαλίζει τη σωστή δομή της ανάρτησης, ακόμα και χωρίς εικόνες.
        /// </summary>
        public async Task AddPostAsync()
        {
            try
            {
                ShowActivityIndicatorPopup();
                await Task.Yield();

                var postId = Guid.NewGuid();

                List<PostImageDto> postImages = new();


                if (this._selectedPostImages != null && this._selectedPostImages.Count > 0)
                {
                    foreach (var photo in this._selectedPostImages)
                    {

                        postImages.Add(new PostImageDto(Guid.NewGuid(), postId, photo));
                    }
                }    
 
                var newPost = UserMapper.ToAddPostUserRequest(
                    postId,
                    CurrentUserId,
                    PostTitle ?? string.Empty,
                    PostContent ?? string.Empty,
                    DateTime.UtcNow,
                    null,
                    postImages
                );

                var result = await this._userService.AddPostUserAsync(newPost);

                if (result.Success)
                {
                    HidActivityIndicatorPopup();

                    if (result.Data != null)
                    {
                        Message = result.Message;
                        PostTitle = result.Data.Title;
                        //PostContent = result.Data.Content;

                        SelectedPostImages.Clear();

                        foreach (var imageUrl in result.Data.PostImages.Select(x => $"{ApiConfig.BaseAddress}{x.ImageUrl}"))
                        {
                            SelectedPostImages.Add(imageUrl);
                        }

                        ShowDialog("Success", Message, "success");

                        UpdatePhotoUrl();
                        await Task.Yield();
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
                // Log 
            }
            finally
            {
                HidActivityIndicatorPopup();
                Resetform();
            }
        }

        #endregion
    }
}
