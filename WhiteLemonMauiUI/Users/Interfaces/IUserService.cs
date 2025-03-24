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
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponseRegisterUserDto}"/> που περιέχει τα αποτελέσματα της εγγραφής.</returns>
        Task<ServiceResult<ResponseRegisterUserDto>> RegisterUserAsync(RegisterUserRequest model);

        /// <summary>
        /// Authenticates a user asynchronously by validating the credentials and calling the API.
        /// Πραγματοποιεί έλεγχο ταυτότητας χρήστη ασύγχρονα, επικυρώνοντας τα διαπιστευτήρια και κάνοντας κλήση στο API.
        /// </summary>
        /// <param name="model">Το μοντέλο εισόδου που περιέχει το email και τον κωδικό χρήστη.</param>
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponseLoginUserDto}"/> που περιέχει το token σύνδεσης και τα δεδομένα χρήστη.</returns>
        Task<ServiceResult<ResponseLoginUserDto>> LoginUserAsync(LoginUserRequest model);

        /// <summary>
        /// Preloads user-related data asynchronously based on the provided parameters.
        /// Φορτώνει προκαταρκτικά δεδομένα χρήστη ασύγχρονα βάσει των παραμέτρων που δίνονται.
        /// </summary>
        /// <param name="currentUser">Το αναγνωριστικό του τρέχοντος χρήστη.</param>
        /// <param name="preloadLimit">Το όριο των δεδομένων που θα προφορτωθούν.</param>
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponsePreloadDataDto}"/> με τα προκαταρκτικά δεδομένα.</returns>
        Task<ServiceResult<ResponsePreloadDataDto>> PreloadDataUserAsync(Guid currentUser, int preloadLimit);

        /// <summary>
        /// Adds a new post for the user asynchronously.
        /// Προσθέτει μια νέα ανάρτηση για τον χρήστη ασύγχρονα.
        /// </summary>
        /// <param name="model">Το μοντέλο της ανάρτησης που περιέχει το περιεχόμενο και τις λεπτομέρειες.</param>
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponsePostedDto}"/> που περιέχει πληροφορίες για την ανάρτηση.</returns>
        Task<ServiceResult<ResponsePostedDto>> AddPostUserAsync(PostDto model);
    }
}
