using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a User entity in the system.
    /// Αντιπροσωπεύει μια οντότητα Χρήστη στο σύστημα.
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// Μοναδικό αναγνωριστικό για τον χρήστη.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the user.
        /// Το όνομα του χρήστη.
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// Διεύθυνση email του χρήστη.
        /// </summary>
        [Required, MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password hash of the user.
        /// Hash του κωδικού πρόσβασης του χρήστη.
        /// </summary>
        [Required, MaxLength(250)]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the user was created.
        /// Η ημερομηνία και ώρα που δημιουργήθηκε ο χρήστης.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// URL of the user's profile picture.
        /// Η διεύθυνση URL της φωτογραφίας προφίλ του χρήστη.
        /// </summary>
        public string? PhotoUrl { get; set; }

        /// <summary>
        /// Path to the user's profile picture on the server.
        /// Η διαδρομή της φωτογραφίας προφίλ του χρήστη στον εξυπηρετητή.
        /// </summary>
        public string? PhotoPath { get; set; }

        // 🔗 Relationships 
        /// <summary>
        /// Collection of posts written by the user.
        /// Συλλογή των δημοσιεύσεων που έχουν γραφτεί από τον χρήστη.
        /// </summary>
        public ICollection<Post> Posts { get; set; } = new List<Post>();          // Ένας χρήστης έχει πολλά posts

        /// <summary>
        /// Collection of friendships the user is involved in.
        /// Συλλογή των φιλιών στις οποίες συμμετέχει ο χρήστης.
        /// </summary>
        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>(); // Ένας χρήστης έχει πολλές φιλίες

        /// <summary>
        /// Collection of comments made by the user.
        /// Συλλογή των σχολίων που έχουν γίνει από τον χρήστη.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();    // Ένας χρήστης έχει πολλά comments

        /// <summary>
        /// Collection of likes made by the user.
        /// Συλλογή των likes που έχουν γίνει από τον χρήστη.
        /// </summary>
        public ICollection<Like> Likes { get; set; } = new List<Like>();          // Ένας χρήστης έχει πολλά likes
    }
}
