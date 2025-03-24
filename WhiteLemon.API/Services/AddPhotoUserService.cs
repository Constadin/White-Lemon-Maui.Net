using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using WhiteLemon.API.Interfaces;

namespace WhiteLemon.API.Services
{

    /// <summary>
    /// Service for storing user photos.
    /// Saves the photoBytes in a specified folder on the server and returns the full file path and the URL of the photoBytes.
    /// </summary>
    /// <summary>
    /// Service για την αποθήκευση φωτογραφιών χρηστών.
    /// Αποθηκεύει τη φωτογραφία σε συγκεκριμένο φάκελο στον server και επιστρέφει το πλήρες μονοπάτι και το URL της φωτογραφίας.
    /// </summary>
    public class AddPhotoUserService : IAddPhotoUserService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for the AddPhotoUserService.
        /// Takes the WebHost environment and the application configuration for access to the file system and settings.
        /// </summary>
        /// <param name="webHostEnvironment">The WebHost environment (for accessing the server's file system).</param>
        /// <param name="configuration">The application's configuration (for reading settings like the domain).</param>
        /// <summary>
        /// Constructor για την υπηρεσία AddPhotoUserService.
        /// Λαμβάνει το περιβάλλον του WebHost και την παραμετροποίηση της εφαρμογής για πρόσβαση στο file system και στις ρυθμίσεις.
        /// </summary>
        /// <param name="webHostEnvironment">Το περιβάλλον του WebHost (για πρόσβαση στο file system του server).</param>
        /// <param name="configuration">Η παραμετροποίηση της εφαρμογής (για ανάγνωση ρυθμίσεων όπως το domain).</param>
        public AddPhotoUserService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;
        }

        /// <summary>
        /// Saves the photoBytes to the server.
        /// Creates the folder if it does not exist, stores the photoBytes, and returns the full file path and the URL of the photoBytes.
        /// </summary>
        /// <param name="photoBytes">The photoBytes we want to save.</param>
        /// <param name="folderPaths">The folders where we want to save the photoBytes.</param>
        /// <returns>A tuple containing the full file path and the URL of the photoBytes.</returns>
        /// <summary>
        /// Δημιουργεί φάκελο αν δεν υπάρχει, αποθηκεύει τη φωτογραφία και επιστρέφει το πλήρες μονοπάτι και το URL της φωτογραφίας.
        /// </summary>
        /// <param name="photoBytes">Η φωτογραφία που θέλουμε να αποθηκεύσουμε.</param>
        /// <param name="folderPaths">Οι φάκελοι στους οποίους θέλουμε να αποθηκεύσουμε τη φωτογραφία.</param>
        /// <returns>Ένα tuple που περιλαμβάνει το πλήρες μονοπάτι και το URL της φωτογραφίας.</returns>
        /// <summary>
        /// 

        public async Task<(string PhotoPath, string PhotoUrl)?> SavePhotoAsync(byte[]? photoBytes, params string[] folderPaths)
        {
            if (photoBytes == null || photoBytes.Length == 0)
            {
                return null; // Επιστροφή null αν η φωτογραφία είναι κενή ή μηδενική
            }

            // Creating the folder path where the photoBytes will be stored.
            // Δημιουργούμε το μονοπάτι του φάκελου όπου θα αποθηκευτεί η φωτογραφία.

            var targetFolderPath = Path.Combine(this._webHostEnvironment.WebRootPath, Path.Combine(folderPaths));

            // If the folder doesn't exist, we create it.
            // Αν ο φάκελος δεν υπάρχει, τον δημιουργούμε.

            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }

            string extension;

            using (var ms = new MemoryStream(photoBytes))
            {
                // This method detects the image format
                // Load the image from the memory stream
                // Αυτή η μέθοδος εντοπίζει το φορμά της εικόνας
                // Φορτώνουμε την εικόνα από το memory stream
                var format = Image.DetectFormat(ms); 

                extension = format switch
                {
                    JpegFormat _ => ".jpg",
                    PngFormat _ => ".png",
                    GifFormat _ => ".gif",
                    _ => ".jpg" 
                };
            }

            // Create a new name for the photoBytes with a random GUID and timestamp.
            // Δημιουργούμε νέο όνομα για τη φωτογραφία με τυχαίο GUID και χρόνο.
            //var newPhotoName = $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}{extension}";

            var newPhotoName = $"{Guid.NewGuid()}{extension}";

            // Combine the folder path and the new photoBytes name to create the full file path.
            // Συνδυάζουμε το μονοπάτι του φακέλου και το νέο όνομα της φωτογραφίας για να δημιουργήσουμε το πλήρες μονοπάτι.
            var fullPhotoPath = Path.Combine(targetFolderPath, newPhotoName);


            // Save the file data to the destination.
            // Αποθηκεύουμε τα δεδομένα του αρχείου στον προορισμό.
            using (var stream = new FileStream(fullPhotoPath, FileMode.Create))
            {
                await stream.WriteAsync(photoBytes);
            }

            // Read the domain setting from the configuration.
            // Διαβάζουμε τη ρύθμιση του domain από την παραμετροποίηση.
            var domainUrl = this._configuration?.GetValue<string>("Domain")?.TrimEnd('/');
            if (domainUrl == null)
            {
                // If the domain setting is not found, throw an exception.
                // Αν δεν υπάρχει ρύθμιση για το domain, ρίχνουμε εξαίρεση.
                throw new InvalidOperationException("Domain configuration is missing.");
            }

            // Δημιουργούμε το URL της φωτογραφίας για να το επιστρέψουμε.
            // Create the URL for the photoBytes to return.
            var photoUrl = $"/{string.Join('/', folderPaths)}/{newPhotoName}";

            // Return the full file path and the URL of the photoBytes.
            // Επιστρέφουμε το πλήρες μονοπάτι και το URL της φωτογραφίας.
            return (fullPhotoPath, photoUrl);
        }
    }
}
