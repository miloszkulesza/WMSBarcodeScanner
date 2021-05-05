using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Services;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class PickPageViewModel : BaseViewModel
    {
        public Page Page { get; }
        public ZXingScannerPage ScanPage { get; set; }
        public IAlertService alertService { get; } = DependencyService.Get<IAlertService>();
        public ICommand ScanCommand { get { return new Command(async () => await OnScan(), () => true); } }

        public PickPageViewModel(Page page)
        {
            Page = page;
            Title = ViewTitles.PickPage;
        }

        private async Task OnScan()
        {
            ScanPage = new ZXingScannerPage();
            ScanPage.OnScanResult += OnScannedResult;
            await Page.Navigation.PushAsync(ScanPage);
        }

        private void OnScannedResult(Result result)
        {
            ScanPage.IsScanning = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Page.Navigation.PopAsync();
                await alertService.ShowAsync("Scanned Barcode", result.Text, "OK");
            });
        }
    }
}
