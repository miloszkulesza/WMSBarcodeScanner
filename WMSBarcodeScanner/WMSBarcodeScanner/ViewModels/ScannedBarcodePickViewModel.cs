using WMSBarcodeScanner.Consts;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class ScannedBarcodePickViewModel : BaseViewModel
    {
        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { SetProperty(ref barcode, value); }
        }

        public ScannedBarcodePickViewModel(string barcode)
        {
            Barcode = barcode;
            Title = $"{ViewTitles.PickPage} - {Barcode}";
        }
    }
}
