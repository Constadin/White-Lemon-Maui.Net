namespace WhiteLemon.API.Models
{
    /// <summary>
    /// Model για την εικόνα δημοσίευσης στο API.
    /// </summary>
    public record PostImageModel
    (
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
