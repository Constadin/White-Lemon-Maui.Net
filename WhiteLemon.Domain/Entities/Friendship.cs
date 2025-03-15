using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a friendship between two users.
    /// Αντιπροσωπεύει μια φιλία μεταξύ δύο χρηστών.
    /// </summary>
    [Table("Friendship")]
    public class Friendship
    {
        /// <summary>
        /// Unique identifier for the friendship.
        /// Μοναδικό αναγνωριστικό για τη φιλία.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The ID of the user who requested the friendship.
        /// Το αναγνωριστικό του χρήστη που ζήτησε τη φιλία.
        /// </summary>
        [Required]
        public Guid RequesterId { get; set; }

        /// <summary>
        /// The ID of the user who was requested for the friendship.
        /// Το αναγνωριστικό του χρήστη που έλαβε την πρόταση φιλίας.
        /// </summary>
        [Required]
        public Guid RequestedId { get; set; }

        /// <summary>
        /// The date and time when the friendship request was created.
        /// Η ημερομηνία και ώρα που δημιουργήθηκε το αίτημα φιλίας.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Relationships 

        /// <summary>
        /// Reference to the first user in the friendship.
        /// Αναφορά στον πρώτο χρήστη της φιλίας.
        /// </summary>
        public User User1 { get; set; } = null!;

        /// <summary>
        /// Reference to the second user in the friendship.
        /// Αναφορά στον δεύτερο χρήστη της φιλίας.
        /// </summary>
        public User User2 { get; set; } = null!;

        // Public properties to easily reference the Composite Key fields

        /// <summary>
        /// The ID of the first user in the friendship (Requester).
        /// Το αναγνωριστικό του πρώτου χρήστη στη φιλία (Requester).
        /// </summary>
        public Guid User1Id => RequesterId;

        /// <summary>
        /// The ID of the second user in the friendship (Requested).
        /// Το αναγνωριστικό του δεύτερου χρήστη στη φιλία (Requested).
        /// </summary>
        public Guid User2Id => RequestedId;
    }
}
