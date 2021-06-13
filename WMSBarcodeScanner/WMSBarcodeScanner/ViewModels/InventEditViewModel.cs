using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using Xamarin.Forms;
using WMSBarcodeScanner.Infrastructure.Consts;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventEditViewModel : BaseViewModel
    {
        #region private members
        private readonly IInventoryRepository inventoryRepo = DependencyService.Get<IInventoryRepository>();
        private readonly IAlertService alertService = DependencyService.Get<IAlertService>();
        #endregion

        #region properties
        public BaseViewModel ViewModel { get; set; }

        private Inventory inventory;
        public Inventory Inventory
        {
            get { return inventory; }
            set { SetProperty(ref inventory, value); }
        }

        private bool newInventory;
        public bool NewInventory
        {
            get { return newInventory; }
            set { SetProperty(ref newInventory, value); }
        }
        #endregion

        #region command
        public ICommand SaveInventoryCommand => new Command(async () => await OnSaveInventory());
        #endregion

        #region ctor
        public InventEditViewModel()
        {

        }
        #endregion

        #region private methods
        private async Task OnSaveInventory()
        {
            bool result = false;
            try
            {
                if(NewInventory)
                    result = await inventoryRepo.AddInventoryAsync(Inventory);
                else
                    result = await inventoryRepo.UpdateInventoryAsync(Inventory);
            }
            catch(Exception e)
            {
                await alertService.ShowAsync("Błąd", $"W trakcie zapisu towaru wystąpił błąd. Szczegóły:{e.InnerException.Message}", "Zamknij");
            }

            await Page.Navigation.PopAsync();
            
            if(result && ViewModel is InventListViewModel)
                (ViewModel as InventListViewModel).RefreshListCommand.Execute(null);   
            else if(NewInventory)
                await alertService.ShowAsync("Dodano nowy towar", $"Dodano nowy towar: {Inventory.Name}", "OK");
        }
        #endregion

        #region public methods
        public void SetPageTitle()
        {
            if (NewInventory)
                Title = $"{ViewTitles.InventAddPage} - {Inventory.Barcode}";
            else
                Title = $"{ViewTitles.InventEditPage} - {Inventory.Barcode}";
        }
        #endregion
    }
}
