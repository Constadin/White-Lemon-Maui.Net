using Microsoft.AspNetCore.Http;

namespace WhiteLemonMauiUI.Users.ModelsDto
{
    /// <summary>
    /// Data Transfer Object (DTO) that represents the response data for a user.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) που αντιπροσωπεύει τα δεδομένα απάντησης για έναν χρήστη.
    /// </summary>
    public record ResponseUserDto(Guid UserId, string? Name, string? Email, string? photoUrl);
}
