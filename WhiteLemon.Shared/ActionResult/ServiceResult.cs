/// <summary>
/// Represents the result of a service operation, including success status, data, and error messages.
/// Αντιπροσωπεύει το αποτέλεσμα μιας λειτουργίας υπηρεσίας, περιλαμβάνοντας την κατάσταση επιτυχίας, τα δεδομένα και τα μηνύματα σφάλματος.
/// </summary>
/// <typeparam name="T">The type of the data returned by the service operation.</typeparam>
public class ServiceResult<T>
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// Δηλώνει αν η λειτουργία ήταν επιτυχής.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Contains the data returned by the operation in case of success.
    /// Περιέχει το αντικείμενο που επιστρέφει η λειτουργία σε περίπτωση επιτυχίας.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Contains the error message in case of failure.
    /// Περιέχει το μήνυμα σφάλματος σε περίπτωση αποτυχίας.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// The success message returned with the operation result.
    /// Μήνυμα επιτυχίας που επιστρέφεται με το αποτέλεσμα της λειτουργίας.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// An optional token that might be returned with the result.
    /// Ένα προαιρετικό token που μπορεί να επιστραφεί με το αποτέλεσμα.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Creates a successful result without any data.
    /// Δημιουργία επιτυχίας χωρίς δεδομένα.
    /// </summary>
    /// <param name="message">Optional success message.</param>
    /// <returns>A <see cref="ServiceResult{T}"/> indicating success.</returns>
    public static ServiceResult<T> SuccessResult(string message = "")
    {
        return new ServiceResult<T> { Success = true, Message = message };
    }

    /// <summary>
    /// Creates a failure result without any data.
    /// Δημιουργία αποτυχίας χωρίς δεδομένα.
    /// </summary>
    /// <param name="errorMessage">The error message indicating the reason for failure.</param>
    /// <returns>A <see cref="ServiceResult{T}"/> indicating failure.</returns>
    public static ServiceResult<T> FailureResult(string errorMessage)
    {
        return new ServiceResult<T> { Success = false, ErrorMessage = errorMessage };
    }

    /// <summary>
    /// Creates a successful result with data.
    /// Δημιουργία επιτυχίας με δεδομένα.
    /// </summary>
    /// <param name="data">The data to be returned in case of success.</param>
    /// <param name="message">Optional success message.</param>
    /// <returns>A <see cref="ServiceResult{T}"/> indicating success with data.</returns>
    public static ServiceResult<T> SuccessResult(T? data, string message = "")
    {
        return new ServiceResult<T> { Success = true, Data = data, Message = message };
    }

    /// <summary>
    /// Creates a failure result with data.
    /// Δημιουργία αποτυχίας με δεδομένα.
    /// </summary>
    /// <param name="data">The data to be returned in case of failure.</param>
    /// <param name="errorMessage">The error message indicating the reason for failure.</param>
    /// <returns>A <see cref="ServiceResult{T}"/> indicating failure with data.</returns>
    public static ServiceResult<T> FailureResult(T? data, string errorMessage)
    {
        return new ServiceResult<T> { Success = false, Data = data, ErrorMessage = errorMessage };
    }
}
