using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedBarcodeReceiptPage : ContentPage
    {
        public ScannedBarcodeReceiptPage(string barcode)
        {
            InitializeComponent();
            (BindingContext as ScannedBarcodeReceiptViewModel).Barcode = barcode;
        }
    }
}