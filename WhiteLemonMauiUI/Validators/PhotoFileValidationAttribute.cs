using System.ComponentModel.DataAnnotations;

namespace WhiteLemonMauiUI.Validators
{
    // This class provides custom validation for photo file uploads.
    // Αυτή η κλάση παρέχει προσαρμοσμένο έλεγχο εγκυρότητας για ανεβάσματα φωτογραφιών.
    public class PhotoFileValidationAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions;
        private readonly long _maxFileSize;

        // Constructor to initialize valid file extensions and max file size.
        // Κατασκευαστής για την αρχικοποίηση έγκυρων επεκτάσεων αρχείων και μέγιστου μεγέθους αρχείου.
        public PhotoFileValidationAttribute(string[] validExtensions, long maxFileSize)
        {
            this._validExtensions = validExtensions;
            this._maxFileSize = maxFileSize;
        }

        // This method checks if the file is valid according to the given rules.
        // Αυτή η μέθοδος ελέγχει αν το αρχείο είναι έγκυρο σύμφωνα με τους δοθέντες κανόνες.
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Αν το value είναι null, δεν κάνουμε έλεγχο (π.χ. αν είναι optional)
            // If the value is null, we skip validation (e.g. if it's optional).
            if (value == null)
                return ValidationResult.Success;

            var file = value as string;

            // Έλεγχος αν υπάρχει αρχείο και αν είναι έγκυρο
            // Check if the file exists and is valid
            if (file == null)
            {
                return new ValidationResult(FormatErrorMessage("File not found"));
            }

            // Έλεγχος επέκτασης αρχείου
            // Check the file extension
            var fileExtension = ".jpg";  // ή ".png"
            var validExtensions = new[] { ".jpg", ".png", ".jpeg" };

            if (!validExtensions.Contains(fileExtension))
            {
                return new ValidationResult(FormatErrorMessage("Invalid file format. Only .jpg and .png are allowed."));
            }

            // Έλεγχος μεγέθους αρχείου
            // Check the file size
            if (file.Length > this._maxFileSize)
            {
                return new ValidationResult(FormatErrorMessage("File size"));
            }

            return ValidationResult.Success;
        }

        // This method formats the error message based on the type of validation failure.
        // Αυτή η μέθοδος μορφοποιεί το μήνυμα σφάλματος με βάση τον τύπο της αποτυχίας επικύρωσης.
        public override string FormatErrorMessage(string name)
        {
            // Ανάλογα με το σφάλμα, επιστρέφουμε το μήνυμα που είναι σχετικό
            // Depending on the error, we return the appropriate message
            return name switch
            {
                "File not found" => "File not found",
                "Invalid file format. Only .jpg and .png are allowed." => "Invalid file format. Only .jpg and .png are allowed.",
                "File size" => $"File size exceeds the limit of {this._maxFileSize / 1024 / 1024} MB.",
                _ => $"Error in the file. {name}"  // Γενικό μήνυμα σφάλματος για άλλες περιπτώσεις
                // General error message for other cases
            };
        }
    }
}
