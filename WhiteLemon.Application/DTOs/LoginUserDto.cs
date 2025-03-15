namespace WhiteLemon.API.Models
{
    /// <summary>
    /// Data Transfer Object (DTO) for user login.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) για την σύνδεση του χρήστη.
    /// </summary>
    public record LoginUserDto(string Email, string Password);
}

