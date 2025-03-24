using WhiteLemon.Domain.Entities;

namespace WhiteLemon.Domain.Interfaces
{
    /// <summary>
    /// Interface that defines the methods for interacting with user data in the repository.
    /// Διεπαφή που ορίζει τις μεθόδους για την αλληλεπίδραση με τα δεδομένα του χρήστη στο αποθετήριο.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique identifier (ID).
        /// Βρίσκει έναν χρήστη με το μοναδικό αναγνωριστικό του (ID).
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation, with the user as the result.</returns>
        Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Retrieves a user by their email address.
        /// Βρίσκει έναν χρήστη με τη διεύθυνση email του.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation, with the user as the result.</returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Registers a new user in the system.
        /// Προσθέτει έναν νέο χρήστη στο σύστημα.
        /// </summary>
        /// <param name="user">The user to be registered.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddRegisterAsync(User user);

        /// <summary>
        /// Updates an existing user's information.
        /// Ενημερώνει τις πληροφορίες ενός υπάρχοντος χρήστη.
        /// </summary>
        /// <param name="user">The user whose information is to be updated.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Deletes a user from the system by their unique identifier (ID).
        /// Διαγράφει έναν χρήστη από το σύστημα με το μοναδικό αναγνωριστικό του (ID).
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Guid userId);


        /// <summary>
        /// Gets a list of users along with the ID of their most liked post.
        /// Επιστρέφει μια λίστα χρηστών μαζί με το ID του πιο δημοφιλούς τους post.
        /// </summary>
        /// <param name="limit">The maximum number of users to return.</param>
        /// <returns>A list of tuples containing a user and the ID of their most liked post (if any), limited by the specified 'limit'.</returns>
        Task<List<(User, Guid?)>> GetUsersWithTopPostAsync(int limit);


        /// <summary>
        /// Gets a list of users who are not friends with the current user.
        /// Επιστρέφει μια λίστα χρηστών που δεν είναι φίλοι με τον τρέχοντα χρήστη.
        /// </summary>
        /// <param name="currentUserId">The ID of the current user.</param>
        /// <param name="limit">The maximum number of users to return.</param>
        /// <returns>A list of users who are not friends with the current user, limited by the specified 'limit'.</returns>
        Task<List<User>> GetUsersNotFriendsAsync(Guid currentUserId, int limit);


        /// <summary>
        /// Adds a new Post to the database.
        /// Προσθέτει έναν νέο Post στη βάση δεδομένων.
        /// </summary>
        /// <param name="post">The post to be added.
        /// Post που θα προστεθεί.</param>
        Task<Post?> UserPostAsync(Post post);
    }
}
