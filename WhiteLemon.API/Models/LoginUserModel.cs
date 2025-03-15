namespace WhiteLemon.API.Models
{
    /// <summary>
    /// Model for user login, containing the necessary fields: email and password.
    /// Μοντέλο για την είσοδο χρήστη, το οποίο περιέχει τα απαιτούμενα πεδία: email και κωδικό πρόσβασης.
    /// </summary>
    public record LoginUserModel(string Email, string Password);
}
