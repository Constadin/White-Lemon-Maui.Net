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
    }
}
