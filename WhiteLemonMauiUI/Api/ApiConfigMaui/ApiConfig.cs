namespace WhiteLemonMauiUI.Api.ApiConfigMaui
{
    public class ApiConfig
    {
        public static string BaseAddress
        {
            get
            {
                var platform = DeviceInfo.Platform.ToString();
                switch (platform)
                {
                    case nameof(DevicePlatform.Android):
                        return "http://10.0.2.2:5241"; // Διεύθυνση για Android Emulator
                    case nameof(DevicePlatform.iOS):
                        return "https://localhost:5241"; // Διεύθυνση για iOS Simulator
                    default:
                        return "https://localhost:5241"; // Default για Windows, Mac, κτλ.
                }
            }
        }
    }
}
