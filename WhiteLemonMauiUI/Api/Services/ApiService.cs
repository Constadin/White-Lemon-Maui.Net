using System.Net.Http.Json;
using WhiteLemonMauiUI.Api.Interfaces;

namespace WhiteLemonMauiUI.Api.Services
{
    /// <summary>
    /// Provides methods for making API requests (GET, POST) to the backend service.
    /// Παρέχει μεθόδους για να κάνετε αιτήματα API (GET, POST) στο backend service.
    /// </summary>
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiService"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="ApiService"/>.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making requests.</param>
        public ApiService(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Makes an asynchronous GET request to the specified URI and returns the result.
        /// Κάνει ένα ασύγχρονο GET αίτημα στη συγκεκριμένη URI και επιστρέφει το αποτέλεσμα.
        /// </summary>
        /// <typeparam name="T">The type of the result that will be returned.</typeparam>
        /// <param name="uri">The URI to send the GET request to.</param>
        /// <returns>A <see cref="ServiceResult{T}"/> containing the result or an error message.</returns>
        public async Task<ServiceResult<T>> GetAsync<T>(string uri)
        {
            try
            {
                // Στέλνει το GET αίτημα στην καθορισμένη URI
                var response = await _httpClient.GetAsync(uri);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return ServiceResult<T>.FailureResult(default, error);
                }

                var result = await response.Content.ReadFromJsonAsync<T>();

                if (result == null)
                {
                    return ServiceResult<T>.FailureResult(default, "No data found.");
                }
                return ServiceResult<T>.SuccessResult(result);
            }
            catch (HttpRequestException httpEx)
            {
                // Επιστρέφει αποτυχία σε περίπτωση HTTP εξαίρεσης
                return ServiceResult<T>.FailureResult(default, $"HTTP Error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Επιστρέφει αποτυχία αν προκύψει εξαίρεση κατά τη διάρκεια της αίτησης
                return ServiceResult<T>.FailureResult(default, $"Error fetching data: {ex.Message}");
            }
        }

        /// <summary>
        /// Makes an asynchronous POST request to the specified URI with a model and returns the result.
        /// Κάνει ένα ασύγχρονο POST αίτημα στη συγκεκριμένη URI με το μοντέλο και επιστρέφει το αποτέλεσμα.
        /// </summary>
        /// <typeparam name="T">The type of the result that will be returned.</typeparam>
        /// <param name="uri">The URI to send the POST request to.</param>
        /// <param name="model">The model to be sent in the POST request body.</param>
        /// <returns>A <see cref="ServiceResult{T}"/> containing the result or an error message.</returns>
        public async Task<ServiceResult<T>> PostAsync<T>(string uri, object model)
        {
            try
            {
                // Στέλνει το POST αίτημα στην καθορισμένη URI με το μοντέλο ως σώμα
                var response = await _httpClient.PostAsJsonAsync(uri, model);


                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return ServiceResult<T>.FailureResult(default, error);
                }

                var result = await response.Content.ReadFromJsonAsync<T>();

                if (result == null)
                {
                    return ServiceResult<T>.FailureResult(default, "No data found.");
                }
                return ServiceResult<T>.SuccessResult(result);
            }
            catch (HttpRequestException httpEx)
            {
                // Επιστρέφει αποτυχία σε περίπτωση HTTP εξαίρεσης
                return ServiceResult<T>.FailureResult(default, $"HTTP Error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Επιστρέφει αποτυχία σε περίπτωση οποιασδήποτε άλλης εξαίρεσης
                return ServiceResult<T>.FailureResult(default, $"Error posting data: {ex.Message}");
            }
        }
    }
}
