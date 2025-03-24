
namespace WhiteLemonMauiUI.Api.ApiConfigMaui
{
    /// <summary>
    /// This class provides the API base address based on the platform (Android, iOS, or others).
    /// Αυτή η κλάση παρέχει τη βασική διεύθυνση του API, ανάλογα με την πλατφόρμα (Android, iOS ή άλλες).
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// Gets the base API address depending on the platform.
        /// Επιστρέφει τη βασική διεύθυνση του API, ανάλογα με την πλατφόρμα.
        /// </summary>
        public static string BaseAddress
        {
            get
            {
                var platform = DeviceInfo.Platform;
                var model = DeviceInfo.Model;
                var deviceType = DeviceInfo.DeviceType;

                if (platform == DevicePlatform.Android)
                {
                    // Check if it is an emulator
                    // Έλεγχος αν είναι emulator
                    if (deviceType == DeviceType.Virtual)
                    {
                        return "http://10.0.2.2:5241"; // Address for Android Emulator // Διεύθυνση για Android Emulator
                    }
                    else
                    {
                        return "http://192.168.1.3:5241"; // Replace with your PC's IP // Αντικατέστησε με την IP του PC σου
                    }
                }
                else if (platform == DevicePlatform.iOS)
                {
                    return "https://localhost:5241"; // Address for iOS Simulator // Διεύθυνση για iOS Simulator
                }

                return "https://localhost:5241"; // Default for Windows, Mac, etc. // Default για Windows, Mac, κτλ.
            }
        }
    }
}
