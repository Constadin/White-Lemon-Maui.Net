using System.ComponentModel.DataAnnotations;
using WhiteLemonMauiUI.Api.ApiConfigMaui;
using WhiteLemonMauiUI.Api.Interfaces;
using WhiteLemonMauiUI.Users.Interfaces;
using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Users.Services
{
    /// <summary>
    /// Service class that handles user-related operations such as registration.
    /// Κλάση υπηρεσίας που χειρίζεται τις λειτουργίες που σχετίζονται με τον χρήστη, όπως η εγγραφή.
    /// </summary>
    public class UserService : IUserService
    {
        // Αναφορά στο API service για κλήσεις API
        private readonly IApiService _apiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="UserService"/>.
        /// </summary>
        /// <param name="apiService">Η υπηρεσία API που χρησιμοποιείται για κλήσεις στο backend.</param>
        public UserService(IApiService apiService)
        {
            // Αν η υπηρεσία API είναι null, ρίχνουμε εξαίρεση
            this._apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        /// <summary>
        /// Registers a new user asynchronously by validating the input and calling the API.
        /// Εγγράφει έναν νέο χρήστη ασύγχρονα, επικυρώνοντας τα δεδομένα εισόδου και κάνοντας κλήση στο API.
        /// </summary>
        /// <param name="model">Το μοντέλο εγγραφής χρήστη που περιέχει τα δεδομένα του χρήστη.</param>
        /// <returns>Αποτέλεσμα τύπου <see cref="ServiceResult{ResponseUserDto}"/> που περιέχει τα αποτελέσματα της εγγραφής.</returns>
        public async Task<ServiceResult<ResponseRegisterUserVMDto>> RegisterUserAsync(RegisterUserRequest model)
        {

            try
            {
                // Λίστα για τα αποτελέσματα επικύρωσης
                var validationResults = new List<ValidationResult>();

                // Προσπάθεια επικύρωσης του μοντέλου
                bool isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

                // Αν το μοντέλο δεν είναι έγκυρο, επιστρέφουμε τα σφάλματα επικύρωσης
                if (!isValid)
                {
                    // Συγκέντρωση μηνυμάτων σφάλματος επικύρωσης
                    var errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                    return ServiceResult<ResponseRegisterUserVMDto>.FailureResult(default, errorMessages);
                }

                  
                // Κλήση στο API με το MultipartFormDataContent
                var response = await _apiService.PostAsync<ServiceResult<ResponseRegisterUserVMDto>>(
                    $"{ApiConfig.BaseAddress}/api/user/register", model);

                // Έλεγχος της απάντησης
                if (!response.Success || response.Data == null)
                {
                    return ServiceResult<ResponseRegisterUserVMDto>.FailureResult(default, response.ErrorMessage);
                }

                // Επιστροφή των δεδομένων του χρήστη από την απάντηση
                return response.Data;

            }
            catch (HttpRequestException ex)
            {
                // Αν παρουσιαστεί σφάλμα κατά τη διάρκεια της κλήσης του API
                return ServiceResult<ResponseRegisterUserVMDto>.FailureResult(
                    default, $"Network error occurred. Unable to connect to the server.{ex.Message}");
            }
            catch (Exception ex)
            {
                // Γενικότερο σφάλμα, επιστρέφουμε μήνυμα σφάλματος
                return ServiceResult<ResponseRegisterUserVMDto>.FailureResult(
                    default, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult<ResponseLoginUserVMDto>> LoginUserAsync(LoginUserRequest model)
        {
            try
            {
                // Λίστα για τα αποτελέσματα επικύρωσης
                var validationResults = new List<ValidationResult>();

                // Προσπάθεια επικύρωσης του μοντέλου
                bool isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

                // Αν το μοντέλο δεν είναι έγκυρο, επιστρέφουμε τα σφάλματα επικύρωσης
                if (!isValid)
                {
                    // Συγκέντρωση μηνυμάτων σφάλματος επικύρωσης
                    var errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                    return ServiceResult<ResponseLoginUserVMDto>.FailureResult(default, errorMessages);
                }

                // Κλήση στο API για Login χρήστη
                var result = await this._apiService.PostAsync< ServiceResult<ResponseLoginUserVMDto >> (
                    $"{ApiConfig.BaseAddress}/api/user/login", model);

                // Έλεγχος αν το πρώτο ServiceResult είναι επιτυχές και αν το Data δεν είναι null
                if (!result.Success || result.Data == null)
                {
                    return ServiceResult<ResponseLoginUserVMDto>.FailureResult(default, result.ErrorMessage);
                }

                // Επιστρέφουμε τα δεδομένα του χρήστη από το ServiceResult
                return result.Data;
            }
            catch (HttpRequestException ex)
            {
                // Αν παρουσιαστεί σφάλμα κατά τη διάρκεια της κλήσης του API
                return ServiceResult<ResponseLoginUserVMDto>.FailureResult(
                    default, $"Network error occurred. Unable to connect to the server.{ex.Message}");
            }
            catch (Exception ex)
            {
                // Γενικότερο σφάλμα, επιστρέφουμε μήνυμα σφάλματος
                return ServiceResult<ResponseLoginUserVMDto>.FailureResult(
                    default, $"An error occurred: {ex.Message}");
            }

        }
    }
}
