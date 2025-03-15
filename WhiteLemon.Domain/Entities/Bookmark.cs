using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents the table for bookmarks in the database.
    /// Αντιπροσωπεύει τον πίνακα για τα bookmarks στη βάση δεδομένων.
    /// </summary>
    [Table("Bookmarks")]
    public class Bookmark
    {
        /// <summary>
        /// Unique identifier for the bookmark.
        /// Μοναδικός αναγνωριστικός αριθμός για το bookmark.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Foreign key to associate the bookmark with a specific user.
        /// Κλειδί ξένο για να συσχετίσει το bookmark με ένα συγκεκριμένο χρήστη.
        /// </summary>
        [Required]
        public Guid UserId { get; set; } // Ο χρήστης που έκανε το bookmark

        /// <summary>
        /// Foreign key to associate the bookmark with a specific post.
        /// Κλειδί ξένο για να συσχετίσει το bookmark με ένα συγκεκριμένο post.
        /// </summary>
        [Required]
        public Guid PostId { get; set; } // Το Post που έχει γίνει bookmark

        /// <summary>
        /// The date and time when the bookmark was created.
        /// Η ημερομηνία και ώρα δημιουργίας του bookmark.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Relationships 

        /// <summary>
        /// Relationship with the User entity.
        /// Σχέση με την οντότητα User.
        /// </summary>
        public User User { get; set; } = null!; // Σχέση με τον χρήστη

        /// <summary>
        /// Relationship with the Post entity.
        /// Σχέση με την οντότητα Post.
        /// </summary>
        public Post Post { get; set; } = null!; // Σχέση με το post που έχει γίνει bookmark
    }
}
