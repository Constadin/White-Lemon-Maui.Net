using System.Windows.Input;

namespace WhiteLemonMauiUI.Pages.ComponetViewModels
{
    public class ButtomSheetComponetViewModel
    {
        public event EventHandler<(byte[], string)>? TakePhotoBytesSingle;
        public event EventHandler<(string, string)>? SelectedBase64EncodedSingle;

        public event EventHandler<(List<byte[]>, string)>? ListPhotoBytes;
        public event EventHandler<(List<string>, string)>? ListBase64EncodedImages;


        public ICommand PickPhotoCommand { get; }
        public ICommand TakePhotoCommand { get; }
        public ICommand CloseModalCommand { get; }

        private string? _callerId;

        public ButtomSheetComponetViewModel(string callerId)
        {
            this._callerId = callerId;
            this.PickPhotoCommand = new Command(async () => await PickPhotoAsync());
            this.TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            this.CloseModalCommand = new Command(async () => await CloseModalAsync());
        }
        public async Task PickPhotoAsync()
        {
            // User selects multiple photos
            // Επιλογή πολλαπλών φωτογραφιών από τον χρήστη
            var photos = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Select up to 4 images", // Μήνυμα για τον χρήστη
                FileTypes = FilePickerFileType.Images // Επιλογή μόνο εικόνων
            });

            if (photos == null || !photos.Any())
                return;

            // Limit images to a maximum of 4
            // Περιορισμός των εικόνων σε μέγιστο 4
            var selectedPhotos = photos.Take(4).ToList();

            var base64Images = new List<string>();    // List to store the Base64 images // Λίστα για να αποθηκεύσουμε τις Base64 εικόνες
            var photoBytesList = new List<byte[]>(); // List of image bytes // Λίστα για τα bytes των εικόνων

            foreach (var photo in selectedPhotos)
            {
                using Stream stream = await photo.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                byte[] photoBytes = memoryStream.ToArray();
                string base64Photo = Convert.ToBase64String(photoBytes);

                // Add the Base64 images to the list
                // Προσθήκη των Base64 εικόνων στη λίστα
                base64Images.Add(base64Photo);
                photoBytesList.Add(photoBytes);
            }

            if (this._callerId == "RegisterViewModel")
            {
                TakePhotoBytesSingle?.Invoke(this, (photoBytesList.First(), this._callerId ?? string.Empty));
                SelectedBase64EncodedSingle?.Invoke(this, (base64Images.First(), this._callerId ?? string.Empty));
            }
            else if (_callerId == "AddPostViewModel")
            {
                // Send all images (as byte arrays)
                // Στέλνουμε όλες τις εικόνες (ως byte arrays)
                ListPhotoBytes?.Invoke(this, (photoBytesList, this._callerId ?? string.Empty));

                // Send all Base64 images
                // Στέλνουμε όλες τις Base64 εικόνες
                ListBase64EncodedImages?.Invoke(this, (base64Images, this._callerId ?? string.Empty));
            }
        }


        public async Task TakePhotoAsync()
        {

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Read the photo as a byte array
                    // Διαβάζουμε τη φωτογραφία ως byte array

                    List<byte[]> photoBytesList = new List<byte[]>();
                    List<string> base64Images = new List<string>();

                    var stream = await photo.OpenReadAsync();

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);

                        // Encode it in Base64
                        // Την κωδικοποιήσουμε σε Base64

                        byte[] photoBytes = memoryStream.ToArray();
                        string base64Photo = Convert.ToBase64String(photoBytes);

                        // Add the Base64 images to the list
                        // Προσθήκη των Base64 εικόνων στη λίστα
                        base64Images.Add(base64Photo);
                        photoBytesList.Add(photoBytes);

                    }

                    if (_callerId == "RegisterViewModel")
                    {
                        TakePhotoBytesSingle?.Invoke(this, (photoBytesList.First(), this._callerId ?? string.Empty));
                        SelectedBase64EncodedSingle?.Invoke(this, (base64Images.First(), this._callerId ?? string.Empty));
                    }
                    else if (_callerId == "AddPostViewModel")
                    {
                        // Send all images (as byte arrays)
                        // Στέλνουμε όλες τις εικόνες (ως byte arrays)
                        ListPhotoBytes?.Invoke(this, (photoBytesList, this._callerId ?? string.Empty));

                        // We send all Base64 images
                        // Στέλνουμε όλες τις Base64 εικόνες
                        ListBase64EncodedImages?.Invoke(this, (base64Images, this._callerId ?? string.Empty));
                    }

                }
            }

        }

        private async Task CloseModalAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
