namespace WhiteLemonMauiUI.Users.ModelsDto
{
    /// <summary>
    /// Data Transfer Object (DTO) for a post.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) για μία δημοσίευση.
    /// </summary>
    public record PostDto(
        /// <summary>
        /// Unique identifier for the post.
        /// Μοναδικό αναγνωριστικό για τη δημοσίευση.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The identifier of the user who created the post.
        /// Το αναγνωριστικό του χρήστη που δημιούργησε τη δημοσίευση.
        /// </summary>
        Guid UserId,

        /// <summary>
        /// Title of the post.
        /// Ο τίτλος της δημοσίευσης.
        /// </summary>
        string Title,

        /// <summary>
        /// Content of the post.
        /// Το περιεχόμενο της δημοσίευσης.
        /// </summary>
        string Content,

        /// <summary>
        /// The date and time when the post was created.
        /// Η ημερομηνία και ώρα που δημιουργήθηκε η δημοσίευση.
        /// </summary>
        DateTime CreatedAt,

        /// <summary>
        /// The date and time when the post was last modified.
        /// Η ημερομηνία και ώρα που τροποποιήθηκε τελευταία η δημοσίευση.
        /// </summary>
        DateTime? ModifiedOn,

        /// <summary>
        /// Collection of images associated with the post.
        /// Συλλογή εικόνων που σχετίζονται με τη δημοσίευση.
        /// </summary>
        List<PostImageDto> PostImages
    );
}
