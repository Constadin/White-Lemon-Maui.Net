namespace WhiteLemon.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for user registration.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) για την εγγραφή χρήστη.
    /// </summary>
    public record RegisterUserDto(string Name, string Email, string Password, string photoUrl, string PhotoPath);
}
