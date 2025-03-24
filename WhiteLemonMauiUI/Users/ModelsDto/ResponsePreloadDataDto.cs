namespace WhiteLemonMauiUI.Users.ModelsDto
{
    /// <summary>
    /// Κατασκευαστής για το ResponsePreloadDataDto.
    /// </summary>
    /// <param name="topRatedUsers">Λίστα τυχαίων χρηστών.</param>
    /// <param name="suggestedUsers">Λίστα με τους πιο πρόσφατους χρήστες.</param>
    public record ResponsePreloadDataDto(List<PreloadUserLimit> topRatedUsers, List<PreloadUserLimit> suggestedUsers);
}
