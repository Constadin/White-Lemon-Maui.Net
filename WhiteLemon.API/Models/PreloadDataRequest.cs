namespace WhiteLemon.API.Models
{
    /// <summary>
    /// Represents a request to preload data for a specific user.
    /// </summary>
    public record PreloadDataRequest(Guid UserId, int Limit);
}
