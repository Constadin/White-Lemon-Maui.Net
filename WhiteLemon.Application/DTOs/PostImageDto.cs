namespace WhiteLemon.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for a post image.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) για μία εικόνα δημοσίευσης.
    /// </summary>
    public record PostImageDto(
        /// <summary>
        /// Unique identifier for the post image.
        /// Μοναδικό αναγνωριστικό για την εικόνα δημοσίευσης.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The identifier of the associated post.
        /// Το αναγνωριστικό της συνδεδεμένης δημοσίευσης.
        /// </summary>
        Guid PostId,

        /// <summary>
        /// URL of the image associated with the post.
        /// Η διεύθυνση URL της εικόνας που σχετίζεται με την δημοσίευση.
        /// </summary>
        string ImageUrl
    );

}
