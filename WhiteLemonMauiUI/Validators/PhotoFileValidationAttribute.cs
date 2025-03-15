using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WhiteLemonMauiUI.Validators
{
    public class PhotoFileValidationAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions;
        private readonly long _maxFileSize;

        public PhotoFileValidationAttribute(string[] validExtensions, long maxFileSize)
        {
            this._validExtensions = validExtensions;
            this._maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Αν το value είναι null, δεν κάνουμε έλεγχο (π.χ. αν είναι optional)
            if (value == null)
                return ValidationResult.Success;

            var file = value as string;

            // Έλεγχος αν υπάρχει αρχείο και αν είναι έγκυρο
            if (file == null)
            {
                return new ValidationResult(FormatErrorMessage("File not found"));
            }

            // Έλεγχος επέκτασης αρχείου
            var fileExtension = ".jpg";  // ή ".png"
            var validExtensions = new[] { ".jpg", ".png", ".jpeg" };

            if (!validExtensions.Contains(fileExtension))
            {
                return new ValidationResult(FormatErrorMessage("Invalid file format. Only .jpg and .png are allowed."));
            }

            // Έλεγχος μεγέθους αρχείου
            if (file.Length > this._maxFileSize)
            {
                return new ValidationResult(FormatErrorMessage("File size"));
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            // Ανάλογα με το σφάλμα, επιστρέφουμε το μήνυμα που είναι σχετικό
            return name switch
            {
                "File not found" => "File not found",
                "Invalid file format. Only .jpg and .png are allowed." => "Invalid file format. Only .jpg and .png are allowed.",
                "File size" => $"File size exceeds the limit of {this._maxFileSize / 1024 / 1024} MB.",
                _ => $"Error in the file. {name}"  // Γενικό μήνυμα σφάλματος για άλλες περιπτώσεις
            };
        }
    }
}
