using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WhiteLemonMauiUI.Validators
{
    public class EmailFormatValidationAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new Regex(
       @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       RegexOptions.Compiled);

        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            var email = value.ToString();
            return email != null && EmailRegex.IsMatch(email);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Please provide a valid email address in the correct format (e.g., example@example.com).";
        }
    }
}
