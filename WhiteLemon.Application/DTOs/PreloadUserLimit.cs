namespace WhiteLemon.Application.DTOs
{
    public record PreloadUserLimit(Guid UserId, string? Name, string? PhotoUrl, Guid? MostLikedPostId);
}
