using CommunityToolkit.Maui.Views;

namespace WhiteLemonMauiUI.Helpers.Views
{
    /// <summary>
    /// A custom popup dialog that displays messages with different types of icons (warning, error, success, etc.)
    /// Προσαρμοσμένο popup διάλογο που εμφανίζει μηνύματα με διάφορους τύπους εικονιδίων (προειδοποίηση, σφάλμα, επιτυχία κ.λπ.)
    /// </summary>
    public partial class DialogPopUp : Popup
    {
        /// <summary>
        /// Title of the popup dialog.
        /// Τίτλος του popup διαλόγου.
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// Message displayed in the popup dialog.
        /// Μήνυμα που εμφανίζεται στο popup διάλογο.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Type of the popup (e.g., "warning", "error", "success").
        /// Τύπος του popup (π.χ. "warning", "error", "success").
        /// </summary>
        public string PopupType { get; set; }

        /// <summary>
        /// Command for the "OK" button that closes the popup.
        /// Εντολή για το κουμπί "OK" που κλείνει το popup.
        /// </summary>
        public Command OkCommand { get; }

        /// <summary>
        /// Constructor for the DialogPopUp. Initializes title, message, and popup type.
        /// Κατασκευαστής για το DialogPopUp. Αρχικοποιεί τον τίτλο, το μήνυμα και τον τύπο του popup.
        /// </summary>
        /// <param name="title">The title of the popup dialog.</param>
        /// <param name="message">The message to display in the popup dialog.</param>
        /// <param name="popupType">The type of the popup (e.g., "warning", "error", "success").</param>
        public DialogPopUp(string title, string message, string popupType)
        {
            InitializeComponent();

    
            this.TitleText = title;
            this.MessageText = message;
            this.PopupType = popupType;

    
            TitleLabel.Text = TitleText;
            MessageLabel.Text = MessageText;

     
            SetPopupIcon();

     
            OkCommand = new Command(() => Close());

            this.BindingContext = this;
        }

        /// <summary>
        /// Sets the appropriate icon based on the popup type (warning, error, success).
        /// Ορίζει το κατάλληλο εικονίδιο με βάση τον τύπο του popup (προειδοποίηση, σφάλμα, επιτυχία).
        /// </summary>
        private void SetPopupIcon()
        {
            string iconSource = string.Empty;

         
            switch (PopupType.ToLower())
            {
                case "warning":
                    iconSource = "warning_regular_24.svg"; 
                    break;
                case "error":
                    iconSource = "error_regular_24.svg";
                    break;
                case "success":
                    iconSource = "success_regular_24.svg"; 
                    break;
                case "notification":
                    iconSource = "notification_regular_24.svg"; 
                    break;
                default:
                    iconSource = ""; 
                    break;
            }

            // Ορίζουμε το εικονίδιο στο Image του popup
            PopupIcon.Source = iconSource;
        }
    }
}
