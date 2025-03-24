using WhiteLemonMauiUI.Api.ApiConfigMaui;

namespace WhiteLemonMauiUI.Users.ModelsDto
{
    public record PreloadUserLimit(Guid UserId, string? Name, string? PhotoUrl, Guid? MostLikedPostId)
    {
        public string? FullPhotoUrl =>
     Uri.IsWellFormedUriString(PhotoUrl, UriKind.Absolute) ? PhotoUrl : $"{ApiConfig.BaseAddress}{PhotoUrl}";

    }
}
