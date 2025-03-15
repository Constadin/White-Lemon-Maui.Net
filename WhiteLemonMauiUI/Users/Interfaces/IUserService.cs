using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Users.Interfaces
{
    /// <summary>
    /// Interface that defines the contract for user-related operations in the application.
    /// Διεπαφή που ορίζει τη σύμβαση για τις λειτουργίες που αφορούν τον χρήστη στην εφαρμογή.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user asynchronously by validating the input and calling the API.
        /// Εγγράφει έναν νέο χρήστη ασύγχρονα, επικυρώνοντας τα δεδομένα εισόδου και κάνοντας κλήση στο API.
        /// </summary>
        /// <param name="model">Το μοντέλο εγγραφής χρήστη που περιέχει τα δεδομένα του χρήστη.</param>
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponseUserDto}"/> που περιέχει τα αποτελέσματα της εγγραφής.</returns>
        Task<ServiceResult<ResponseUserDto>> RegisterUserAsync(RegisterUserRequest model);

        Task<ServiceResult<ResponseUserDto>> LoginUserAsync(LoginUserRequest model);
    }
}
