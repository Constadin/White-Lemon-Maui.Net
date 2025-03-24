using Microsoft.AspNetCore.Mvc;

public static class ErrorResponseFactory
{
    public static IActionResult CreateErrorResponse(ControllerBase controller, string errorMessage)
    {
        // Έλεγχος αν το μήνυμα περιλαμβάνει συγκεκριμένο τύπο σφάλματος
        if (string.IsNullOrEmpty(errorMessage))
        {
            return controller.StatusCode(500, "An unknown error occurred. Please try again later."); // Default 500
        }

        if (errorMessage.Contains("Invalid user data"))
        {
            return controller.BadRequest("Invalid user data."); // 400 Bad Request
        }
        else if (errorMessage.Contains("Invalid user password"))
        {
            return controller.BadRequest("Invalid user password."); // 400 Bad Request
        }
        else if (errorMessage.Contains("This user already exists"))
        {
            return controller.Conflict("This user already exists."); // 409 Conflict
        }
        else if (errorMessage.Contains("Server error"))
        {
            return controller.StatusCode(500, "Server error occurred. Please try again later."); // 500 Internal Server Error
        }
        //If the error is not detected, we return 500 with the message
        // Αν δεν εντοπιστεί το σφάλμα, επιστρέφουμε 500 με το μήνυμα
        return controller.StatusCode(500, errorMessage);
    }

    // New method for exception case
    // Νέα μέθοδος για την περίπτωση exception
    public static IActionResult CreateErrorResponse(ControllerBase controller, Exception ex)
    {
        // Check if the exception is null or has messages
        // Έλεγχος αν το exception είναι null ή έχει μηνύματα
        if (ex == null)
        {
            return controller.StatusCode(500, "An unknown error occurred. Please try again later.");
        }

        // You can check the exception type and return an appropriate error
        // Μπορείς να ελέγξεις τον τύπο της εξαίρεσης και να επιστρέψεις ανάλογο σφάλμα
        if (ex is InvalidOperationException)
        {
            return controller.BadRequest($"Invalid operation: {ex.Message}"); // 400 Bad Request
        }
        else if (ex is UnauthorizedAccessException)
        {
            return controller.StatusCode(403, $"Unauthorized access: {ex.Message}"); // 403 Forbidden
        }
        else if (ex is ArgumentNullException)
        {
            return controller.BadRequest($"Missing argument: {ex.Message}"); // 400 Bad Request
        }

        // General handling for other exceptions
        // Γενικός χειρισμός για άλλες εξαιρέσεις
        return controller.StatusCode(500, $"An error occurred: {ex.Message}"); // 500 Internal Server Error
    }
    public static IActionResult CreateErrorResponse<T>(ControllerBase controller, ServiceResult<T> result)
    {
        // Check if result is a failure and return an appropriate error
        // Έλεγχος αν το result είναι αποτυχία και επιστροφή ανάλογου σφάλματος

        if (!result.Success)
        {
            return CreateErrorResponse(controller, result.ErrorMessage); // Χρησιμοποιούμε την ήδη υπάρχουσα μέθοδο για το μήνυμα
        }

        // If successful, return the result as OK
        // Αν είναι επιτυχία, επιστρέφουμε το αποτέλεσμα ως OK
        return controller.Ok(result); 
    }
}
