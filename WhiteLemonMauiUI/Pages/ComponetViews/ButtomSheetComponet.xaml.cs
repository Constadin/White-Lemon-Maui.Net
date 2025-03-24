using WhiteLemonMauiUI.Pages.ComponetViewModels;

namespace WhiteLemonMauiUI.Pages.ComponetViews;

public partial class ButtomSheetComponet : ContentPage
{
    private ButtomSheetComponetViewModel _buttomSheetComponetViewModel;

    public ButtomSheetComponet(string callerId)
    {
        InitializeComponent();

        // Here we pass the callerId to use in the ViewModel
        // Εδώ περνάμε το callerId για να το χρησιμοποιήσουμε στο ViewModel
        this._buttomSheetComponetViewModel = new ButtomSheetComponetViewModel(callerId);
        this.BindingContext = this._buttomSheetComponetViewModel;
    }
}
