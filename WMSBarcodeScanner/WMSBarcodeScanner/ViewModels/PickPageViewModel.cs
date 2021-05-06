using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;
using ZXing;

namespace WMSBarcodeScanner.ViewModels
{
    public class PickPageViewModel : BaseViewModel
    {
        public Page Page { get; set; }

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

        public ICommand QRScanResultCommand { get; set; }

        public PickPageViewModel(Page page)
        {
            Page = page;
            IsScanning = true;
            Page.Appearing += OnPageAppearing;
            Title = ViewTitles.PickScanningPage;
            QRScanResultCommand = new Command(() => OnQRScanResult());
        }

        private void OnPageAppearing(object sender, System.EventArgs e)
        {
            IsAnalyzing = true;
        }

        private void OnQRScanResult()
        {
            IsAnalyzing = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Page.Navigation.PushAsync(new ScannedBarcodePickPage(ScannedBarcode.Text));
            });
        }
    }
}
