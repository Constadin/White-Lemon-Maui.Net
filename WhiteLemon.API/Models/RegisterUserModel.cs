using WhiteLemon.Shared.Constants;

namespace WhiteLemon.API.Models
{
    /// <summary>
    /// Model for user registration, containing the necessary fields: name, email, and password.
    /// Μοντέλο για την εγγραφή χρήστη, το οποίο περιέχει τα απαιτούμενα πεδία: όνομα, email και κωδικό πρόσβασης.
    /// </summary>
    public record RegisterUserModel(string Name, string Email, string Password, string? photoUser)
    {
    }
}
