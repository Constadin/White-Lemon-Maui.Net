using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a message entity in the system.
    /// Αντιπροσωπεύει μια οντότητα μηνύματος στο σύστημα.
    /// </summary>
    [Table("Messages")]
    public class Message
    {
        /// <summary>
        /// Unique identifier for the message.
        /// Μοναδικό αναγνωριστικό για το μήνυμα.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the user who sends the message.
        /// Το αναγνωριστικό του χρήστη που στέλνει το μήνυμα.
        /// </summary>
        [Required]
        public Guid SenderId { get; set; }

        /// <summary>
        /// The identifier of the user who receives the message.
        /// Το αναγνωριστικό του χρήστη που λαμβάνει το μήνυμα.
        /// </summary>
        [Required]
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// The content of the message.
        /// Το περιεχόμενο του μηνύματος.
        /// </summary>
        [Required, MaxLength(3500)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the message was sent.
        /// Η ημερομηνία και ώρα αποστολής του μηνύματος.
        /// </summary>
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time when the message was read, if applicable.
        /// Η ημερομηνία και ώρα που διαβάστηκε το μήνυμα, αν ισχύει.
        /// </summary>
        public DateTime? ReadAt { get; set; }

        // 🔗 Relationships 

        /// <summary>
        /// The user who sent the message.
        /// Ο χρήστης που έστειλε το μήνυμα.
        /// </summary>
        public User Sender { get; set; } = null!;

        /// <summary>
        /// The user who received the message.
        /// Ο χρήστης που έλαβε το μήνυμα.
        /// </summary>
        public User Receiver { get; set; } = null!;

        /// <summary>
        /// Indicates whether the message has been read by the receiver.
        /// Δείχνει αν το μήνυμα έχει διαβαστεί από τον παραλήπτη.
        /// </summary>
        public bool IsRead => ReadAt.HasValue; // If ReadAt has a value, it means the message has been read
    }
}
