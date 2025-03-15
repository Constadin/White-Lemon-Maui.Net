using Microsoft.EntityFrameworkCore;
using WhiteLemon.Domain.Entities;

namespace WhiteLemon.Infrastructure.Data
{
    /// <summary>
    /// Represents the application's database context, providing access to the data models.
    /// Αντιπροσωπεύει το context της βάσης δεδομένων της εφαρμογής, παρέχοντας πρόσβαση στα μοντέλα δεδομένων.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext with the provided options.
        /// Αρχικοποιεί μια νέα περίπτωση του ApplicationDbContext με τις παρασχεθείσες επιλογές.
        /// </summary>
        /// <param name="options">The options to configure the database context. 
        /// Οι επιλογές για τη διαμόρφωση του context της βάσης δεδομένων.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSet properties for each entity (table in the database)
        public DbSet<User> Users { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        /// <summary>
        /// Configures the entity models and relationships for the database.
        /// Διαμορφώνει τα μοντέλα οντοτήτων και τις σχέσεις για τη βάση δεδομένων.
        /// </summary>
        /// <param name="modelBuilder">The model builder to configure the model properties. 
        /// Ο δημιουργός μοντέλου για τη διαμόρφωση των ιδιοτήτων του μοντέλου.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for the Friendship entity
            // Composite Key για την οντότητα Friendship
            modelBuilder.Entity<Friendship>()
                .HasKey(f => new { f.RequesterId, f.RequestedId });


            // Ensure unique entries for the same friendship
            // Εξασφαλίζει ότι δεν θα υπάρχουν διπλές εγγραφές για την ίδια φιλία
            modelBuilder.Entity<Friendship>()
                .HasIndex(f => new { f.RequesterId, f.RequestedId })
                .IsUnique();

            // Optionally, add reverse uniqueness
            // Επιπλέον, μπορείς να προσθέσεις μια αντίστροφη μοναδικότητα
            modelBuilder.Entity<Friendship>()
                .HasIndex(f => new { f.RequestedId, f.RequesterId })
                .IsUnique(); 

            // 🔥 When a User is deleted, all related data will be deleted automatically

            // Delete the Posts of the user
            // Διαγραφή των Posts του χρήστη
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Friendships of the user
            // Διαγραφή των Friendships του χρήστη
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)  // First user (Requester)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)  // Second user (Requested)
                .WithMany()  // The second user does not need a reverse relationship
                .HasForeignKey(f => f.RequestedId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Comments of the user
            // Διαγραφή των Comments του χρήστη
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Likes of the user
            // Διαγραφή των Likes του χρήστη
            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Bookmarks of the user
            // Διαγραφή των Bookmarks του χρήστη
            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Notifications of the user
            // Διαγραφή των Notifications του χρήστη
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete the Messages of the user
            // Διαγραφή των Messages του χρήστη
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany() 
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 When a Post is deleted, all related entries will be cleared


            // Comments - Post (Cascade Delete)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // Likes - Post (Cascade Delete)
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // PostImages - Post (Cascade Delete)
            modelBuilder.Entity<PostImage>()
                .HasOne(pi => pi.Post)
                .WithMany(p => p.PostImages)
                .HasForeignKey(pi => pi.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // Bookmarks - Post (Cascade Delete)
            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.Post)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            // Notifications - Post (Cascade Delete)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Post)
                .WithMany()
                .HasForeignKey(n => n.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
