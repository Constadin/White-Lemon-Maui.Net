using Microsoft.AspNetCore.Identity;
using WhiteLemon.API.Models;
using WhiteLemon.Application.DTOs;
using WhiteLemon.Application.Interfaces;
using WhiteLemon.Domain.Entities;
using WhiteLemon.Domain.Interfaces;

namespace WhiteLemon.Application.Services
{
    /// <summary>
    /// Provides user-related services, including registration and login functionality.
    /// Παρέχει υπηρεσίες που σχετίζονται με τον χρήστη, όπως η εγγραφή και η σύνδεση.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="UserService"/>.
        /// </summary>
        public UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            this._passwordHasher = passwordHasher;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user in the system asynchronously.
        /// Εγγράφει έναν νέο χρήστη στο σύστημα ασύγχρονα.
        /// </summary>
        /// <param name="userDto">The data transfer object containing the user's registration details.</param>
        /// <returns>A service result indicating whether the registration was successful or not.</returns>
        public async Task<ServiceResult<ResponseRegisterUserDto>> RegisterUserAsync(RegisterUserDto userDto)
        {
            // Check if fields are empty or contain whitespace.
            // Έλεγχος αν τα πεδία είναι άδεια ή λευκές αλυσίδες χαρακτήρων.
            if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return ServiceResult<ResponseRegisterUserDto>.FailureResult("Invalid user data.");
            }

            var user = await this._userRepository.GetUserByEmailAsync(userDto.Email);

            // If the user already exists, return a failure result.
            // Αν ο χρήστης υπάρχει ήδη, επιστρέφουμε αποτυχία.
            if (user?.Email == userDto.Email)
            {
                return ServiceResult<ResponseRegisterUserDto>.FailureResult("This user already exists.");
            }

            // Create a new user.
            // Δημιουργία νέου χρήστη.
            user = new User
            {
                Name = userDto.Name ?? string.Empty,
                Email = userDto.Email ?? string.Empty,
                PhotoUrl = userDto.photoUrl ?? null,
                PhotoPath = userDto.PhotoPath ?? string.Empty,
                CreatedAt = DateTime.UtcNow.AddMilliseconds(-DateTime.UtcNow.Millisecond)
            };

            // Hash the password and assign it to the user.
            // Δημιουργία του κωδικού πρόσβασης με hashing.
            user.PasswordHash = this._passwordHasher.HashPassword(user, userDto.Password);

            // Add the user to the database.
            // Προσθήκη του χρήστη στη βάση δεδομένων.
            await this._userRepository.AddRegisterAsync(user);

            // Create and return the ResponseUserDto.
            // Δημιουργία και επιστροφή του ResponseUserDto.
            var userDtoResponse = new ResponseRegisterUserDto(user.Id, user.Name, user.Email, user.PhotoUrl);

            return ServiceResult<ResponseRegisterUserDto>.SuccessResult(userDtoResponse, "User registered successfully.");
        }

        /// <summary>
        /// Authenticates a user based on their email and password asynchronously.
        /// Αυθεντικοποιεί έναν χρήστη με βάση το email και τον κωδικό πρόσβασης ασύγχρονα.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A service result containing the user's details and token if authentication is successful.</returns>
        public async Task<ServiceResult<ResponseLoginUserDto>> LoginUserAsync(LoginUserDto userDto)
        {
            // Find the user from the database.
            // Βρίσκουμε τον χρήστη από τη βάση δεδομένων.
            var user = await this._userRepository.GetUserByEmailAsync(userDto.Email!);

            if (user == null) // If the user doesn't exist, return a failure result.
            // Αν ο χρήστης δεν υπάρχει, επιστρέφουμε αποτυχία.
            {
                return ServiceResult<ResponseLoginUserDto>.FailureResult("Invalid user data.");
            }

            // Verify the password using hashing and verification.
            // Έλεγχος του κωδικού πρόσβασης με hashing και verification.
            var passwordVerificationResult = this._passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password!);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return ServiceResult<ResponseLoginUserDto>.FailureResult("Invalid user password.");
            }

            // Return the user's details if authentication is successful.
            // Επιστροφή των στοιχείων του χρήστη σε περίπτωση επιτυχίας.
            var responseUserDto = new ResponseLoginUserDto(user.Id, user.Email, user.Name, user.PhotoUrl);

            return ServiceResult<ResponseLoginUserDto>.SuccessResult(responseUserDto, "User login successfully.");
        }

        /// <summary>
        /// Loads user data to preload the home page.
        /// Φορτώνει δεδομένα χρηστών για την προφόρτωση της αρχικής σελίδας.
        /// </summary>
        /// <param name="preloadLimit">The number of users to be preloaded.</param>
        /// <returns>A service result containing the preloaded user data.</returns>
        public async Task<ServiceResult<ResponsePreloadDataDto>> PreloadDataAsync(Guid currentUserId, int preloadLimit)
        {
            try
            {
                // Get random users.
                // Λήψη τυχαίων χρηστών.
                var topRatedUsers = await this._userRepository.GetUsersWithTopPostAsync(preloadLimit);

                // Get the newest users.
                // Λήψη των πιο πρόσφατων χρηστών.
                var suggestedUsers = await this._userRepository.GetUsersNotFriendsAsync(currentUserId, preloadLimit);

                // Create the DTO with both lists of users.
                // Δημιουργία του DTO με τις δύο λίστες χρηστών.
                var responsePreloadData = new ResponsePreloadDataDto(
                    topRatedUsers.Select(u => new PreloadUserLimit(u.Item1.Id, u.Item1.Name, u.Item1.PhotoUrl, u.Item2)).ToList(),
                    suggestedUsers.Select(u => new PreloadUserLimit(u.Id, u.Name, u.PhotoUrl, null)).ToList()
                );

                // Return the result with success.
                // Επιστροφή του αποτελέσματος με επιτυχία.
                return ServiceResult<ResponsePreloadDataDto>.SuccessResult(responsePreloadData, "Data loaded successfully.");
            }
            catch (Exception ex)
            {
                // Return an error result in case of failure.
                // Επιστροφή σφάλματος σε περίπτωση αποτυχίας.
                return ServiceResult<ResponsePreloadDataDto>.FailureResult($"Error loading data: {ex.Message}");
            }
        }

        public async Task<ServiceResult<ResponsePostedDto>> AddPostAsync(PostDto post)
        {
            try
            {

                if (post.UserId == Guid.Empty)
                {
                    return ServiceResult<ResponsePostedDto>.FailureResult("Invalid user data.");
                }

                Post postEntity = new Post
                {
                    Id = post.Id,
                    UserId = post.UserId,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    ModifiedOn = post.ModifiedOn,
                    PostImages = post.PostImages?.Select(pi => new PostImage
                    {
                        Id = Guid.NewGuid(),
                        PostId = post.Id,
                        ImageUrl = pi.ImageUrl
                    }).ToList() ?? new List<PostImage>()
                };


                // Add the post to the database
                // Προσθήκη του post στη βάση δεδομένων
                var savedPost = await _userRepository.UserPostAsync(postEntity);

                if (savedPost == null)
                {
                    return ServiceResult<ResponsePostedDto>.FailureResult("Failed to retrieve post after save.");
                }

                // Prepare ResponsePostedDto with the necessary data
                // Προετοιμασία του ResponsePostedDto με τα απαραίτητα δεδομένα
                var responsePostedDto = new ResponsePostedDto
                (
                    savedPost.UserId,
                    savedPost.Id,
                    savedPost.User.Name,
                    savedPost.Title ?? string.Empty,
                    savedPost.CreatedAt,
                    savedPost.PostImages.Select(pi => new PostImageDto
                    (
                        pi.Id,
                        pi.PostId,
                        pi.ImageUrl ?? string.Empty
                    )).ToList()
                );

                return ServiceResult<ResponsePostedDto>.SuccessResult(responsePostedDto, "Posted successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult<ResponsePostedDto>.FailureResult($"An error occurred: {ex.Message}");
            }
        }

    }
}
