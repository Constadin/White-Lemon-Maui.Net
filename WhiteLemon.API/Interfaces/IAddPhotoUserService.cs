namespace WhiteLemon.API.Interfaces
{
    /// <summary>
    /// Interface για την υπηρεσία αποθήκευσης φωτογραφιών χρηστών.
    /// </summary>
    public interface IAddPhotoUserService
    {
        /// <summary>
        /// Αποθηκεύει τη φωτογραφία στον server.
        /// Δημιουργεί τον φάκελο αν δεν υπάρχει, αποθηκεύει τη φωτογραφία και επιστρέφει το πλήρες μονοπάτι και το URL της φωτογραφίας.
        /// </summary>
        /// <param name="photo">Η φωτογραφία που θέλουμε να αποθηκεύσουμε σε μορφή byte[].</param>
        /// <param name="folderPaths">Οι φάκελοι στους οποίους θέλουμε να αποθηκεύσουμε τη φωτογραφία.</param>
        /// <returns>Ένα tuple που περιλαμβάνει το πλήρες μονοπάτι και το URL της φωτογραφίας.</returns>
        Task<(string PhotoPath, string PhotoUrl)?> SavePhotoAsync(byte[]? photoBytes, params string[] folderPaths);
    }
}
