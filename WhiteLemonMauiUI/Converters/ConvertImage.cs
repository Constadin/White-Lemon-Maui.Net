using System.Diagnostics;

namespace WhiteLemonMauiUI.Converters
{
    public static class ConvertImage
    {
        public static async Task<string> ConvertDefaultUserImageUrlToBase64(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return string.Empty;

                // Download the image from the URL
                // Κατεβάζουμε την εικόνα από το URL
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(imageUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Save the image as a byte array
                        // Αποθηκεύουμε την εικόνα ως byte array
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();


                        // Encode the image to Base64
                        // Κωδικοποιούμε την εικόνα σε Base64
                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error converting image to Base64: {ex.Message}");
            }
            return string.Empty;
        }

    }
}
