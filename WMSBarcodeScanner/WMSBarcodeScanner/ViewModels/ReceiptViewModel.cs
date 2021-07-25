using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Infrastructure.Consts;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;
using ZXing;

namespace WMSBarcodeScanner.ViewModels
{
    public class ReceiptViewModel : BaseViewModel
    {
        #region private members
        private readonly IInventoryRepository inventRepo = DependencyService.Get<IInventoryRepository>();
        private readonly IAlertService alertService = DependencyService.Get<IAlertService>();
        #endregion

        #region properties
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
                return new Command(async () => await OnQRScanResult());
            }
        }
        #endregion

        #region construct
        public ReceiptViewModel()
        {
            IsScanning = true;
            Title = ViewTitles.ReceiptScanningPage;
            ScannedBarcode = new Result(string.Empty, new byte[] { }, new ResultPoint[] { }, BarcodeFormat.QR_CODE);
        }
        #endregion

        #region private methods
        private void OnPageAppearing(object sender, System.EventArgs e)
        {
            IsAnalyzing = true;
        }

        private async Task OnQRScanResult()
        {
            IsAnalyzing = false;
            Inventory inventory;
            
            inventory = await inventRepo.GetInventoryByBarcodeAsync(ScannedBarcode.Text);
            if (inventory == null)
            {
                inventory = new Inventory
                {
                    Barcode = ScannedBarcode.Text,
                    Quantity = 1
                };
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Page.Navigation.PushAsync(new InventEditPage(inventory, this, true));
                });
            }
            else
            {
                PromptResult result = await UserDialogs.Instance.PromptAsync("Podaj ilość towaru do przyjęcia", "Przyjęcie towaru", "Przyjmij", "Anuluj", "Ilość", InputType.Number);
                if(result.Ok)
                {
                    int quantity = Convert.ToInt32(result.Value);
                    if (quantity <= 0)
                        alertService.Show("Błąd", $"Nie można przyjąć towaru w ilości mniejsze lub równej 0", "Zamknij");
                    else
                    {
                        inventory.Quantity += quantity;
                        bool updateResult = false;
                        try
                        {
                            updateResult = await inventRepo.UpdateInventoryAsync(inventory);
                            if (updateResult)
                                UserDialogs.Instance.Toast($"Przyjęto towar {inventory.Name} w ilości {quantity}", TimeSpan.FromSeconds(3));
                            else
                                UserDialogs.Instance.Toast($"Nie udało się przyjąć towaru {inventory.Name} w ilości {quantity}", TimeSpan.FromSeconds(3));
                        }
                        catch (Exception e)
                        {
                            alertService.Show("Błąd", $"W czasie przyjmowania towaru {inventory.Name} wystąpił błąd. Szczegóły: {e.Message}", "Zamknij");
                        }
                    }
                }
            }
        }
        #endregion

        #region public methods
        public void SetPageAppearingEvent()
        {
            Page.Appearing += OnPageAppearing;
        }
        #endregion
    }
}
