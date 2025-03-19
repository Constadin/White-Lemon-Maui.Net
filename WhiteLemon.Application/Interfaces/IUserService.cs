using WhiteLemon.API.Models;
using WhiteLemon.Application.DTOs;

namespace WhiteLemon.Application.Interfaces
{
    /// <summary>
    /// Interface for user-related operations such as registration and login.
    /// Διεπαφή για λειτουργίες που σχετίζονται με τον χρήστη, όπως η εγγραφή και η σύνδεση.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user and returns a result with the user data.
        /// Καταχωρεί έναν νέο χρήστη και επιστρέφει το αποτέλεσμα με τα δεδομένα του χρήστη.
        /// </summary>
        /// <param name="userDto">The user data for registration.</param>
        /// <returns>A <see cref="ServiceResult{ResponseRegisterUserDto}"/> containing the user data or error message.</returns>
        Task<ServiceResult<ResponseRegisterUserDto>> RegisterUserAsync(RegisterUserDto userDto);

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// Συνδέει έναν χρήστη με τα δεδομένα σύνδεσης που παρέχονται.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="ServiceResult{ResponseLoginUserDto}"/> containing the user data and authentication token or error message.</returns>
        Task<ServiceResult<ResponseLoginUserDto>> LoginUserAsync(LoginUserDto userDto);

        /// <summary>
        /// Loads user data to preload the home page with random and newest users.
        /// Φορτώνει δεδομένα χρηστών για την προφόρτωση της αρχικής σελίδας με τυχαίους και πιο πρόσφατους χρήστες.
        /// </summary>
        /// <param name="preloadLimit">The number of users to be preloaded.</param>
        /// <returns>A <see cref="ServiceResult{ResponsePreloadDataDto}"/> containing the preloaded user data.</returns>
        Task<ServiceResult<ResponsePreloadDataDto>> PreloadDataAsync(Guid currentUserId, int preloadLimit);
    }
}
