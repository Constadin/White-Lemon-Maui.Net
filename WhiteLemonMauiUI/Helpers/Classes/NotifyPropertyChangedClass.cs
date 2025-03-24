using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WhiteLemonMauiUI.Helpers.Classes
{
    /// <summary>
    /// A base class that implements INotifyPropertyChanged for property change notifications.
    /// Μία βασική κλάση που υλοποιεί το INotifyPropertyChanged για ειδοποιήσεις αλλαγής ιδιοτήτων.
    /// </summary>
    public class NotifyPropertyChangedClass : INotifyPropertyChanged
    {
        //Processing the event for property change notification
        // Επεξεργασία του event για ειδοποίηση αλλαγής ιδιοτήτων
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Safely sets the value of a property and notifies listeners if the value has changed.
        /// Ασφαλής μέθοδος για να ορίσετε την τιμή μίας ιδιοκτησίας και να ενημερώσετε τους ακροατές αν έχει αλλάξει η τιμή.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyStore">The backing field of the property.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property (automatically filled).</param>
        /// <param name="onChanged">An optional action to execute when the property changes.</param>
        /// <returns>True if the value has changed, otherwise false.</returns>
        protected bool SetPropertyValue<T>(ref T propertyStore, T value, [CallerMemberName] string? propertyName = null, Action? onChanged = null)
        {
            //// If the new value differs from the stored value
            // Αν η νέα τιμή διαφέρει από την αποθηκευμένη τιμή
            if (!EqualityComparer<T>.Default.Equals(propertyStore, value))
            {
                propertyStore = value;                   // Update the property // Ενημέρωση της ιδιοκτησίας
                onChanged?.Invoke();                    // Perform the optional action// Εκτέλεση της προαιρετικής δράσης
                OnPropertyChanged(propertyName);       // Notification of change// Ειδοποίηση της αλλαγής
                return true;                          // Return true, as the value changed// Επιστροφή αληθές, καθώς η τιμή άλλαξε
            }
            return false;                           // Return false if the value did not change// Επιστροφή ψευδές, αν η τιμή δεν άλλαξε
        }

        /// <summary>
        /// Notifies that a property has changed.
        /// Ειδοποιεί ότι μία ιδιοκτησία έχει αλλάξει.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // Notify the event if there is a listener
            // Ειδοποίηση του event αν υπάρχει ακροατής
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
