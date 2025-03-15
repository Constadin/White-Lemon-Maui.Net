using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents the table for comments in the database.
    /// Αντιπροσωπεύει τον πίνακα για τα σχόλια στη βάση δεδομένων.
    /// </summary>
    [Table("Comments")]
    public class Comment
    {
        /// <summary>
        /// Unique identifier for the comment.
        /// Μοναδικός αναγνωριστικός αριθμός για το σχόλιο.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Creates a unique GUID for each comment

        /// <summary>
        /// Foreign key to associate the comment with a specific post.
        /// Κλειδί ξένο για να συσχετίσει το σχόλιο με ένα συγκεκριμένο post.
        /// </summary>
        [Required]
        public Guid PostId { get; set; } // Foreign Key to the Post

        /// <summary>
        /// Foreign key to associate the comment with the user who created it.
        /// Κλειδί ξένο για να συσχετίσει το σχόλιο με τον χρήστη που το δημιούργησε.
        /// </summary>
        [Required]
        public Guid UserId { get; set; } // Foreign Key to the User

        /// <summary>
        /// The content of the comment.
        /// Το περιεχόμενο του σχόλιου.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the comment was created.
        /// Η ημερομηνία και ώρα δημιουργίας του σχόλιου.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time when the comment was last modified (optional).
        /// Η ημερομηνία και ώρα τελευταίας τροποποίησης του σχόλιου (προαιρετικό).
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        // 🔗 Relationships 

        /// <summary>
        /// Relationship with the Post entity.
        /// Σχέση με την οντότητα Post.
        /// </summary>
        public Post Post { get; set; } = null!;  // A comment belongs to a post

        /// <summary>
        /// Relationship with the User entity.
        /// Σχέση με την οντότητα User.
        /// </summary>
        public User User { get; set; } = null!;  // A comment belongs to a user
    }
}
