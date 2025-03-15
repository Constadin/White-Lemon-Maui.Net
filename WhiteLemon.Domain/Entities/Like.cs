using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a like entity for a post.
    /// Αντιπροσωπεύει μια οντότητα like για ένα post.
    /// </summary>
    [Table("Likes")]
    public class Like
    {
        /// <summary>
        /// Unique identifier for the like.
        /// Μοναδικό αναγνωριστικό για το like.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the post that is liked.
        /// Το αναγνωριστικό του post που λαμβάνει το like.
        /// </summary>
        [Required]
        public Guid PostId { get; set; }

        /// <summary>
        /// The identifier of the user who liked the post.
        /// Το αναγνωριστικό του χρήστη που έκανε like στο post.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// The date and time when the like was created.
        /// Η ημερομηνία και ώρα που δημιουργήθηκε το like.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time when the like was last modified, if applicable.
        /// Η ημερομηνία και ώρα που τροποποιήθηκε το like, αν ισχύει.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        // 🔗 Relationships 

        /// <summary>
        /// The post that the like belongs to.
        /// Το post στο οποίο ανήκει το like.
        /// </summary>
        public Post Post { get; set; } = null!;

        /// <summary>
        /// The user who liked the post.
        /// Ο χρήστης που έκανε like στο post.
        /// </summary>
        public User User { get; set; } = null!;
    }
}
