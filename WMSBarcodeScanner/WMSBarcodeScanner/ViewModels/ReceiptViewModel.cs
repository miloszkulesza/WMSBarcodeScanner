using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;
using ZXing;

namespace WMSBarcodeScanner.ViewModels
{
    public class ReceiptViewModel : BaseViewModel
    {
        #region properties
        private Page page;
        public Page Page
        {
            get { return page; }
            set 
            { 
                page = value;
                page.Appearing += OnPageAppearing;
            }
        }


        private Result scannedBarcode;
        public Result ScannedBarcode
        {
            get { return scannedBarcode; }
            set { SetProperty(ref scannedBarcode, value); }
        }

        private bool isScanning;
        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }

        private bool isAnalyzing;
        public bool IsAnalyzing
        {
            get { return isAnalyzing; }
            set { SetProperty(ref isAnalyzing, value); }
        }
        #endregion

        #region commands
        public ICommand QRScanResultCommand
        {
            get
            {
                return new Command(() => OnQRScanResult());
            }
        }
        #endregion

        #region construct
        public ReceiptViewModel()
        {
            IsScanning = true;            
            Title = ViewTitles.ReceiptScanningPage;
        }
        #endregion

        #region private methods
        private void OnPageAppearing(object sender, System.EventArgs e)
        {
            IsAnalyzing = true;
        }

        private void OnQRScanResult()
        {
            IsAnalyzing = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Page.Navigation.PushAsync(new ScannedBarcodeReceiptPage(ScannedBarcode.Text));
            });
        }
        #endregion
    }
}
