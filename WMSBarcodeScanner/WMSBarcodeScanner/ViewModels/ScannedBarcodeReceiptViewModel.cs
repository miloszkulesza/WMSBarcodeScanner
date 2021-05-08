using WMSBarcodeScanner.Consts;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class ScannedBarcodeReceiptViewModel : BaseViewModel
    {
        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set 
            { 
                SetProperty(ref barcode, value);
                Title = $"{ViewTitles.ReceiptPage} - {Barcode}";
            }
        }

        public ScannedBarcodeReceiptViewModel()
        {

        }
    }
}
