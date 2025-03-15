using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a Post entity in the system.
    /// Αντιπροσωπεύει μια οντότητα Δημοσίευσης στο σύστημα.
    /// </summary>
    [Table("Posts")]
    public class Post
    {
        /// <summary>
        /// Unique identifier for the post.
        /// Μοναδικό αναγνωριστικό για την δημοσίευση.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the user who created the post.
        /// Το αναγνωριστικό του χρήστη που δημιούργησε τη δημοσίευση.
        /// </summary>
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Title of the post.
        /// Ο τίτλος της δημοσίευσης.
        /// </summary>
        [MaxLength(400)]
        public string? Title { get; set; }

        /// <summary>
        /// Content of the post.
        /// Το περιεχόμενο της δημοσίευσης.
        /// </summary>
        [MaxLength(1500)]
        public string? Content { get; set; }

        /// <summary>
        /// The date and time when the post was created.
        /// Η ημερομηνία και ώρα που δημιουργήθηκε η δημοσίευση.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date and time when the post was last modified.
        /// Η ημερομηνία και ώρα που τροποποιήθηκε τελευταία η δημοσίευση.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        // 🔗 Relationships
        /// <summary>
        /// The user who created the post.
        /// Ο χρήστης που δημιούργησε τη δημοσίευση.
        /// </summary>
        public User User { get; set; } = null!;      // Ένα post ανήκει σε έναν χρήστη

        /// <summary>
        /// Collection of comments made on the post.
        /// Συλλογή των σχολίων που έγιναν στη δημοσίευση.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Ένα post έχει πολλά comments

        /// <summary>
        /// Collection of likes made on the post.
        /// Συλλογή των likes που έγιναν στη δημοσίευση.
        /// </summary>
        public ICollection<Like> Likes { get; set; } = new List<Like>();       // Ένα post έχει πολλά likes

        /// <summary>
        /// Collection of images associated with the post.
        /// Συλλογή εικόνων που σχετίζονται με τη δημοσίευση.
        /// </summary>
        public ICollection<PostImage> PostImages { get; set; } = new List<PostImage>(); // Ένα post μπορεί να έχει πολλές εικόνες

        /// <summary>
        /// Collection of bookmarks related to the post.
        /// Συλλογή των σελιδοδεικτών που σχετίζονται με τη δημοσίευση.
        /// </summary>
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    }
}
