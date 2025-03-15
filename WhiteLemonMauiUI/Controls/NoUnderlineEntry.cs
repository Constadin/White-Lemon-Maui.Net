using Microsoft.Maui.Handlers;

namespace WhiteLemonMauiUI.Controls
{
    /// <summary>
    /// Custom <see cref="Entry"/> control that removes the underline style in Android and iOS.
    /// Προσαρμοσμένο control <see cref="Entry"/> που αφαιρεί την υπογράμμιση στο Android και iOS.
    /// </summary>
    public class NoUnderlineEntry : Entry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoUnderlineEntry"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="NoUnderlineEntry"/>.
        /// </summary>
        public NoUnderlineEntry()
        {
            // We apply the mapping when the control is created
            // Εφαρμόζουμε το mapping όταν δημιουργείται το control
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                // Removes the underline in Android
                // Αφαιρεί την υπογράμμιση στο Android
                handler.PlatformView.Background = null;
#elif IOS
                // Removes the underline in iOS
                // Αφαιρεί την υπογράμμιση στο iOS
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });
        }
    }
}
