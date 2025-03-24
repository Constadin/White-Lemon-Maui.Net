using System.ComponentModel.DataAnnotations;
using WhiteLemonMauiUI.Validators;

namespace WhiteLemonMauiUI.Users.ModelsDto
{
    /// <summary>
    /// Data Transfer Object (DTO) that represents the Login data  Request for a user.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) που αντιπροσωπεύει τα δεδομένα απάντησης για έναν χρήστη.
    /// </summary>
    public record class LoginUserRequest
    {
        /// <summary>
        /// The email address of the user.
        /// Η διεύθυνση email του χρήστη.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailFormatValidation]
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// The password of the user.
        /// Ο κωδικός πρόσβασης του χρήστη.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; init; } = string.Empty;
    }
}
