using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Infrastructure.Consts;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Models;
using Acr.UserDialogs;
using Xamarin.Forms;
using ZXing;
using System;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;

namespace WMSBarcodeScanner.ViewModels
{
    public class PickViewModel : BaseViewModel
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
        public ICommand QRScanResultCommand { get; set; }
        #endregion

        #region construct
        public PickViewModel()
        {
            IsScanning = true;            
            Title = ViewTitles.PickScanningPage;
            ScannedBarcode = new Result(string.Empty, new byte[] { }, new ResultPoint[] { }, BarcodeFormat.QR_CODE);
            QRScanResultCommand = new Command(async () => await OnQRScanResult());
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
                UserDialogs.Instance.Toast($"Nie odnaleziono towaru o kodzie kreskowym {ScannedBarcode.Text}");
            else
            {
                PromptResult result = await UserDialogs.Instance.PromptAsync("Podaj ilość towaru do wydania", "Wydanie towaru", "Wydaj", "Anuluj", "Ilość", InputType.Number);
                if (result.Ok)
                {
                    int quantity = Convert.ToInt32(result.Value);

                    if (quantity <= 0)
                        alertService.Show("Błąd", $"Nie można wydać towaru w ilości mniejsze lub równej 0", "Zamknij");
                    else if (quantity > inventory.Quantity)
                        alertService.Show("Błąd", $"Nie można wydać więcej towaru niż jest na magazynie", "Zamknij");
                    else
                    {
                        inventory.Quantity -= quantity;
                        bool updateResult = false;
                        try
                        {
                            updateResult = await inventRepo.UpdateInventoryAsync(inventory);
                            if (updateResult)
                                UserDialogs.Instance.Toast($"Wydano towar {inventory.Name} w ilości {quantity}", TimeSpan.FromSeconds(3));
                            else
                                UserDialogs.Instance.Toast($"Nie udało się wydać towaru {inventory.Name} w ilości {quantity}", TimeSpan.FromSeconds(3));
                        }
                        catch (Exception e)
                        {
                            alertService.Show("Błąd", $"W czasie wydawania towaru {inventory.Name} wystąpił błąd. Szczegóły: {e.Message}", "Zamknij");
                        }

                        if (inventory.Quantity == 0)
                        {
                            bool confirmResult = await UserDialogs.Instance.ConfirmAsync("Brak towaru w magazynie. Czy usunąć towar?", "Brak towaru", "Usuń", "Zamknij");
                            if (confirmResult)
                            {
                                try
                                {
                                    bool deleteResult = await inventRepo.DeleteInventoryAsync(inventory.Id);
                                    if (deleteResult)
                                        UserDialogs.Instance.Toast($"Usunięto towar {inventory.Name}", TimeSpan.FromSeconds(3));
                                    else
                                        UserDialogs.Instance.Toast($"Nie udało się usunąć towaru {inventory.Name}", TimeSpan.FromSeconds(3));
                                }
                                catch (Exception e)
                                {
                                    alertService.Show("Błąd", $"W czasie usuwania towaru wystąpił błąd. Szczegóły: {e.Message}", "Zamknij");
                                }
                            }
                        }
                    }
                }
            }

            IsAnalyzing = true;
        }
        #endregion

        #region public methods
        public void SetPageApeearingEvent()
        {
            Page.Appearing += OnPageAppearing;
        }
        #endregion
    }
}
