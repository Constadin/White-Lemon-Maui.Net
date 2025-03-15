using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

                // Κατεβάζουμε την εικόνα από το URL
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(imageUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Αποθηκεύουμε την εικόνα ως byte array
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

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
