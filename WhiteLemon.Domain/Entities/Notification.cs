using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a notification entity in the system.
    /// Αντιπροσωπεύει μια οντότητα ειδοποίησης στο σύστημα.
    /// </summary>
    [Table("Notifications")]
    public class Notification
    {
        /// <summary>
        /// Unique identifier for the notification.
        /// Μοναδικό αναγνωριστικό για την ειδοποίηση.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the user to whom the notification is sent.
        /// Το αναγνωριστικό του χρήστη στον οποίο αποστέλλεται η ειδοποίηση.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// The message content of the notification.
        /// Το μήνυμα της ειδοποίησης.
        /// </summary>
        [Required, MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the notification was created.
        /// Η ημερομηνία και ώρα δημιουργίας της ειδοποίησης.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates whether the notification has been read by the user.
        /// Δηλώνει αν η ειδοποίηση έχει διαβαστεί από τον χρήστη.
        /// </summary>
        public bool IsRead { get; set; } = false;

        // 🔗 Relationships 

        /// <summary>
        /// The user associated with the notification.
        /// Ο χρήστης που σχετίζεται με την ειδοποίηση.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The optional post associated with the notification.
        /// Το προαιρετικό post που σχετίζεται με την ειδοποίηση.
        /// </summary>
        public Guid? PostId { get; set; }

        /// <summary>
        /// The post entity that the notification may refer to (optional).
        /// Η οντότητα του post στην οποία μπορεί να αναφέρεται η ειδοποίηση (προαιρετικό).
        /// </summary>
        public Post? Post { get; set; } = null!;
    }
}
