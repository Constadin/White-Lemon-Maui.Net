using Microsoft.AspNetCore.Mvc;
using WhiteLemon.API.Interfaces;
using WhiteLemon.API.Models;
using WhiteLemon.Application.DTOs;
using WhiteLemon.Application.Interfaces;

namespace WhiteLemon.API.Controllers
{
    /// <summary>
    /// Controller for user-related actions such as registration and login.
    /// Ελεγκτής για ενέργειες που σχετίζονται με τον χρήστη, όπως η εγγραφή και η σύνδεση.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAddPhotoUserService _photoUploadService;

        /// <summary>
        /// Constructor to initialize the UserController with the IUserService dependency.
        /// Κατασκευαστής για την αρχικοποίηση του UserController με την εξάρτηση IUserService.
        /// </summary>
        public UserController(IUserService userService, IAddPhotoUserService photoUploadService)
        {
            this._userService = userService;
            this._photoUploadService = photoUploadService;
        }

        /// <summary>
        /// Endpoint for registering a new user.
        /// Σημείο πρόσβασης για την εγγραφή ενός νέου χρήστη.
        /// </summary>
        /// <param name="model">The user data model for registration.</paramxreiazome ena byte>
        /// <returns>A response with success or failure message.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
        {
            if (model == null)
            {
                return ErrorResponseFactory.CreateErrorResponse(this, "Invalid user data.");
            }

            if (string.IsNullOrEmpty(model.photoUser))
            {
                return ErrorResponseFactory.CreateErrorResponse(this, "Photo data is required.");
            }


            byte[] photoBytes;
            try
            {
                photoBytes = Convert.FromBase64String(model.photoUser);
            }
            catch (FormatException)
            {
                return ErrorResponseFactory.CreateErrorResponse(this, "Invalid photo data format.");
            }

            var result = await this._photoUploadService.SavePhotoAsync(photoBytes, "Uploads");

            string photoPath = result.Value.PhotoPath;

            string photoUrl = result.Value.PhotoUrl;

            var registrationResult = await this._userService.RegisterUserAsync(new RegisterUserDto(model.Name, model.Email, model.Password, photoUrl, photoPath));


            return ErrorResponseFactory.CreateErrorResponse(this, registrationResult);
        }

        /// <summary>
        /// Endpoint for logging in an existing user.
        /// Σημείο πρόσβασης για την είσοδο ενός υπάρχοντος χρήστη.
        /// </summary>
        /// <param name="model">The login credentials for the user.</param>
        /// <returns>A response with a token if login is successful, otherwise an error message.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserModel model)
        {
            if (model == null)
            {
                return ErrorResponseFactory.CreateErrorResponse(this, "Invalid login credentials.");
            }


            // Calling LoginUserAsync to check the user data
            // Κλήση της LoginUserAsync για να ελέγξουμε τα δεδομένα του χρήστη

            var registrationResul = await this._userService.LoginUserAsync(new LoginUserDto(model.Email, model.Password));
   


            //If there is an error, we return Unauthorized and the error message
            // Αν υπάρχει σφάλμα, επιστρέφουμε Unauthorized και το μήνυμα σφάλματος
            return ErrorResponseFactory.CreateErrorResponse(this, registrationResul);
        }


        /// <summary>
        /// Endpoint for preloading data.  GET /api/users/preloadData
        /// Εντολή για προφόρτωση δεδομένων.  GET /api/users/preloadData
        /// </summary>
        /// <returns></returns>
        [HttpPost("preload-data")]
        public async Task<IActionResult> GetPreloadData([FromBody] PreloadDataRequest request)
        {
            try
            {
                Guid currentUserId = request.UserId;

                int preloadLimit = request.Limit;

                var preloadResponse = await this._userService.PreloadDataAsync(currentUserId, preloadLimit);

                return ErrorResponseFactory.CreateErrorResponse(this, preloadResponse);
            }
            catch (Exception ex)
            {

                return ErrorResponseFactory.CreateErrorResponse(this, ex);
            }
        }

        /// <summary>
        /// Endpoint for adding a new post. 
        /// Εντολή για προσθήκη νέας ανάρτησης.  
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-post")]
        public async Task<IActionResult> AddPost([FromBody] PostModel model)
        {
            if (model == null)
            {
                return ErrorResponseFactory.CreateErrorResponse(this, "Invalid post data.");
            }

            List<PostImageModel> postImages = new List<PostImageModel>();

            List<PostImageDto> postImageDtos = new List<PostImageDto>();

            if (model.PostImages != null && model.PostImages.Any())
            {

                foreach (var image in model.PostImages)
                {
                    byte[] photoBytes;

                    try
                    {
                        photoBytes = Convert.FromBase64String(image.ImageUrl);
                    }
                    catch (FormatException)
                    {
                        return ErrorResponseFactory.CreateErrorResponse(this, "Invalid photo data format.");
                    }

                    var result = await this._photoUploadService.SavePhotoAsync(photoBytes, "Posts");

                    string photoPath = result.Value.PhotoPath;

                    string photoUrl = result.Value.PhotoUrl;

                    postImages.Add(new PostImageModel(Guid.NewGuid(), model.Id, photoUrl));
                }

                postImageDtos.AddRange(postImages.Select(psI => new PostImageDto(psI.Id, psI.PostId, psI.ImageUrl)).ToList());
            }

            var postedResult = await this._userService.AddPostAsync(new PostDto(model.Id, model.UserId, model.Title, model.Content, model.CreatedAt, model.ModifiedOn, postImageDtos));

            return ErrorResponseFactory.CreateErrorResponse(this, postedResult);
        }

    }
}
