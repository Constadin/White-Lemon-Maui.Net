using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WhiteLemonMauiUI.Validators
{
    // This class validates if the email address matches the correct format.
    // Αυτή η κλάση ελέγχει αν η διεύθυνση email είναι σε σωστή μορφή.
    public class EmailFormatValidationAttribute : ValidationAttribute
    {
        // A regular expression pattern to match a valid email format.
        // Ένα κανονικό μοτίβο έκφρασης για να αντιστοιχεί σε έγκυρη μορφή email.
        private static readonly Regex EmailRegex = new Regex(
       @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       RegexOptions.Compiled);

        // This method checks if the value matches the email format.
        // Αυτή η μέθοδος ελέγχει αν η τιμή ταιριάζει με τη μορφή email.
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            var email = value.ToString();
            return email != null && EmailRegex.IsMatch(email);
        }

        // This method formats the error message to show when the email format is invalid.
        // Αυτή η μέθοδος μορφοποιεί το μήνυμα σφάλματος που εμφανίζεται όταν η μορφή email είναι λανθασμένη.
        public override string FormatErrorMessage(string name)
        {
            return "Please provide a valid email address in the correct format (e.g., example@example.com).";
        }
    }
}
