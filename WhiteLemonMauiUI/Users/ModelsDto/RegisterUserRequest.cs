using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLemonMauiUI.Validators;


namespace WhiteLemonMauiUI.Users.ModelsDto
{
    public record class RegisterUserRequest
    {
        /// <summary>
        /// The username of the user.
        /// Το όνομα χρήστη του χρήστη.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
        public string Name { get; init; } = string.Empty;

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

        /// <summary>
        /// The profile photo of the user.
        /// </summary>
        [PhotoFileValidation(new[] { ".jpg", ".png" }, 10 * 1024 * 1024)]
        public string? PhotoUser { get; init; }
    }
}

