using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLemon.Domain.Entities
{
    /// <summary>
    /// Represents a PostImage entity in the system.
    /// Αντιπροσωπεύει μια οντότητα Εικόνας Δημοσίευσης στο σύστημα.
    /// </summary>
    [Table("PostImages")]
    public class PostImage
    {
        /// <summary>
        /// Unique identifier for the post image.
        /// Μοναδικό αναγνωριστικό για την εικόνα δημοσίευσης.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the associated post.
        /// Το αναγνωριστικό της συνδεδεμένης δημοσίευσης.
        /// </summary>
        [Required]
        public Guid PostId { get; set; }

        /// <summary>
        /// URL of the image associated with the post.
        /// Η διεύθυνση URL της εικόνας που σχετίζεται με την δημοσίευση.
        /// </summary>
        public string? ImageUrl { get; set; }

        // 🔗 Relationship 
        /// <summary>
        /// The post to which the image belongs.
        /// Η δημοσίευση στην οποία ανήκει η εικόνα.
        /// </summary>
        public Post Post { get; set; } = null!; // Κάθε εικόνα ανήκει σε ένα συγκεκριμένο Post
    }
}
