using Microsoft.EntityFrameworkCore;
using WhiteLemon.Domain.Entities;
using WhiteLemon.Domain.Interfaces;
using WhiteLemon.Infrastructure.Data;


namespace WhiteLemon.Infrastructure.Repositories
{
    /// <summary>
    /// Provides methods to interact with the User data in the database.
    /// Παρέχει μεθόδους για την αλληλεπίδραση με τα δεδομένα του χρήστη στη βάση δεδομένων.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the UserRepository with the provided database context.
        /// Αρχικοποιεί μια νέα περίπτωση του UserRepository με το παρεχόμενο context της βάσης δεδομένων.
        /// </summary>
        /// <param name="context">The database context used to interact with the data.
        /// Το context της βάσης δεδομένων που χρησιμοποιείται για την αλληλεπίδραση με τα δεδομένα.</param>
        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Finds a user by their email address.
        /// Βρίσκει τον χρήστη με τη διεύθυνση email του.
        /// </summary>
        /// <param name="email">The email address of the user.
        /// Η διεύθυνση email του χρήστη.</param>
        /// <returns>The user if found, otherwise null.
        /// Ο χρήστης αν βρεθεί, διαφορετικά null.</returns>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await this._context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Finds a user by their unique identifier (Id).
        /// Βρίσκει τον χρήστη με το μοναδικό αναγνωριστικό (Id).
        /// </summary>
        /// <param name="userId">The unique identifier of the user.
        /// Το μοναδικό αναγνωριστικό του χρήστη.</param>
        /// <returns>The user if found, otherwise null.
        /// Ο χρήστης αν βρεθεί, διαφορετικά null.</returns>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await this._context.Set<User>().FindAsync(userId);
        }

        /// <summary>
        /// Adds a new user to the database.
        /// Προσθέτει έναν νέο χρήστη στη βάση δεδομένων.
        /// </summary>
        /// <param name="user">The user to be added.
        /// Ο χρήστης που θα προστεθεί.</param>
        public async Task AddRegisterAsync(User user)
        {
            await this._context.Set<User>().AddAsync(user);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user's information in the database.
        /// Ενημερώνει τις πληροφορίες ενός υπαρκτού χρήστη στη βάση δεδομένων.
        /// </summary>
        /// <param name="user">The user whose information will be updated.
        /// Ο χρήστης των πληροφοριών του οποίου θα ενημερωθούν.</param>
        public async Task UpdateAsync(User user)
        {
            this._context.Set<User>().Update(user);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user from the database.
        /// Διαγράφει έναν χρήστη από τη βάση δεδομένων.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.
        /// Το μοναδικό αναγνωριστικό του χρήστη που θα διαγραφεί.</param>
        public async Task DeleteAsync(Guid userId)
        {
            var user = await this._context.Set<User>().FindAsync(userId);
            if (user != null)
            {
                this._context.Set<User>().Remove(user);
                await this._context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a list of users along with the ID of their most liked post.
        /// Επιστρέφει μια λίστα χρηστών μαζί με το ID του πιο δημοφιλούς τους post.
        /// </summary>
        /// <param name="limit">The maximum number of users to return.</param>
        /// <returns>A list of tuples containing a user and the ID of their most liked post (if any), limited by the specified 'limit'.</returns>
        public async Task<List<(User, Guid?)>> GetUsersWithTopPostAsync(int limit)
        {
            var topUsers = await _context.Set<User>()
                .Select(user => new
                {
                    User = user, // Αποθηκεύουμε το αντικείμενο του χρήστη

                    // Βρίσκουμε το ID του post του χρήστη που έχει τα περισσότερα likes
                    MostLikedPostId = (Guid?)user.Posts
                        .OrderByDescending(post => post.Likes.Count()) // Ταξινομούμε τα posts κατά φθίνουσα σειρά likes
                        .Select(post => post.Id) // Επιλέγουμε μόνο το ID του post
                        .FirstOrDefault() // Παίρνουμε το πρώτο στοιχείο της λίστας (το post με τα περισσότερα likes)
                })
                .OrderByDescending(userWithPost =>
                    userWithPost.User.Posts.Sum(post => post.Likes.Count())) // Ταξινομούμε τους χρήστες κατά συνολικό αριθμό likes στα posts τους
                        .Take(limit) // Περιορίζουμε το αποτέλεσμα σε 'limit' χρήστες
                        .ToListAsync(); // Εκτελούμε το query στη βάση δεδομένων

            // Μετατρέπουμε τη λίστα ώστε να επιστρέψουμε ένα Tuple (User, MostLikedPostId)
            return topUsers
                    .Select(u => (u.User, u.MostLikedPostId == Guid.Empty ? (Guid?)null : u.MostLikedPostId))
                    .ToList();


        }

        /// <summary>
        /// Gets a list of users who are not friends with the current user.
        /// Επιστρέφει μια λίστα χρηστών που δεν είναι φίλοι με τον τρέχοντα χρήστη.
        /// </summary>
        /// <param name="currentUserId">Το ID του χρήστη που έχει κάνει login.</param>
        /// <param name="limit">Πόσους χρήστες να επιστρέψει.</param>
        /// <returns>Λίστα χρηστών που δεν είναι φίλοι με τον logged-in χρήστη.</returns>
        public async Task<List<User>> GetUsersNotFriendsAsync(Guid currentUserId, int limit)
        {
            return await _context.Set<User>()
                .Where(u => u.Id != currentUserId) // Εξαιρούμε τον εαυτό του
                .Where(u => !_context.Friendships.Any(f =>
                    (f.RequesterId == currentUserId && f.RequestedId == u.Id) ||
                    (f.RequestedId == currentUserId && f.RequesterId == u.Id))) // Εξαιρούμε όσους είναι ήδη φίλοι
                .OrderByDescending(u => u.CreatedAt) // Ταξινόμηση με βάση την ημερομηνία εγγραφής
                .Take(limit) // Περιορισμός στον αριθμό των χρηστών
                .ToListAsync();

            //limit = limit / 2;

            //return await _context.Set<User>()
            //  .Where(u => u.Id != currentUserId)
            //  .OrderBy(u => u.CreatedAt)  // Βρίσκουμε τους πιο πρόσφατους χρήστες
            //  .Take(limit)
            //  .ToListAsync();
        }


    }
}
