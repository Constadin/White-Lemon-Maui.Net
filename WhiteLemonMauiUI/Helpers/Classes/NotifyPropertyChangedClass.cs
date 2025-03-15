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
            // Αν η νέα τιμή διαφέρει από την αποθηκευμένη τιμή
            if (!EqualityComparer<T>.Default.Equals(propertyStore, value))
            {
                propertyStore = value; // Ενημέρωση της ιδιοκτησίας
                onChanged?.Invoke(); // Εκτέλεση της προαιρετικής δράσης
                this.OnPropertyChanged(propertyName); // Ειδοποίηση της αλλαγής
                return true; // Επιστροφή αληθές, καθώς η τιμή άλλαξε
            }
            return false; // Επιστροφή ψευδές, αν η τιμή δεν άλλαξε
        }

        /// <summary>
        /// Notifies that a property has changed.
        /// Ειδοποιεί ότι μία ιδιοκτησία έχει αλλάξει.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // Ειδοποίηση του event αν υπάρχει ακροατής
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
