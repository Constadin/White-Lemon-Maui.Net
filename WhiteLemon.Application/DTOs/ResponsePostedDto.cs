namespace WhiteLemon.Application.DTOs
{
    /// <summary>
    /// Represents a response for a posted post with associated details.
    /// Αντιπροσωπεύει μια απόκριση για μια δημοσίευση με τις σχετικές λεπτομέρειες.
    /// </summary>
    public record ResponsePostedDto(
        /// <summary>
        /// The ID of the user who created the post.
        /// Το ID του χρήστη που δημιούργησε τη δημοσίευση.
        /// </summary>
        Guid UserId,

        /// <summary>
        /// The ID of the post.
        /// Το ID της δημοσίευσης.
        /// </summary>
        Guid PostId,

        /// <summary>
        /// The username of the user who created the post.
        /// Το όνομα χρήστη του χρήστη που δημιούργησε τη δημοσίευση.
        /// </summary>
        string Username,

        /// <summary>
        /// The title of the post.
        /// Ο τίτλος της δημοσίευσης.
        /// </summary>
        string Title,

        /// <summary>
        /// The creation date and time of the post.
        /// Η ημερομηνία και ώρα δημιουργίας της δημοσίευσης.
        /// </summary>
        DateTime Createdat,

        /// <summary>
        /// A list of post images associated with the post.
        /// Μια λίστα με εικόνες δημοσίευσης που σχετίζονται με τη δημοσίευση.
        /// </summary>
        List<PostImageDto> PostImages);

}
