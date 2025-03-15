namespace WhiteLemonMauiUI.Api.Interfaces
{
    /// <summary>
    /// Defines the contract for an API service that performs HTTP requests (GET, POST).
    /// Ορίζει τη σύμβαση για μια υπηρεσία API που εκτελεί αιτήματα HTTP (GET, POST).
    /// </summary>
    public interface IApiService
    {
        /// <summary>
        /// Sends a GET request to the specified URI and returns the result.
        /// Στέλνει ένα αίτημα GET στην καθορισμένη URI και επιστρέφει το αποτέλεσμα.
        /// </summary>
        /// <typeparam name="T">The type of the result that will be returned from the GET request.</typeparam>
        /// <param name="uri">The URI to send the GET request to.</param>
        /// <returns>A <see cref="ServiceResult{T}"/> containing the result of the GET request.</returns>
        Task<ServiceResult<T>> GetAsync<T>(string uri);

        /// <summary>
        /// Sends a POST request to the specified URI with the provided model and returns the result.
        /// Στέλνει ένα αίτημα POST στην καθορισμένη URI με το παρεχόμενο μοντέλο και επιστρέφει το αποτέλεσμα.
        /// </summary>
        /// <typeparam name="T">The type of the result that will be returned from the POST request.</typeparam>
        /// <param name="uri">The URI to send the POST request to.</param>
        /// <param name="model">The data model to be sent in the body of the POST request.</param>
        /// <returns>A <see cref="ServiceResult{T}"/> containing the result of the POST request.</returns>
        Task<ServiceResult<T>> PostAsync<T>(string uri, object model);
    }
}
