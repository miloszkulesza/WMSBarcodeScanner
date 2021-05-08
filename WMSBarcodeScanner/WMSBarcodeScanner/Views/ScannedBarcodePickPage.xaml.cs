using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedBarcodePickPage : ContentPage
    {
        public ScannedBarcodePickPage(string barcode)
        {
            InitializeComponent();
            (BindingContext as ScannedBarcodePickViewModel ).Barcode = barcode;
        }
    }
}