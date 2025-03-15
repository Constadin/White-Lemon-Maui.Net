using UIKit;

namespace WhiteLemonMauiUI
{
    public class Program
    {
        // This is the main entry point of the application. //Αυτό είναι το κύριο σημείο εισόδου της εφαρμογής.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    }
}
