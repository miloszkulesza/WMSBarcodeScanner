using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Services;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventEditViewModel : BaseViewModel
    {
        private readonly IInventoryRepository inventoryRepo = DependencyService.Get<IInventoryRepository>();
        private readonly IAlertService alertService = DependencyService.Get<IAlertService>();
        public InventListViewModel InventListViewModel { get; set; }

        private Inventory inventory;
        public Inventory Inventory
        {
            get { return inventory; }
            set 
            { 
                SetProperty(ref inventory, value);
                Title = inventory.Name;
            }
        }

        public ICommand SaveInventoryCommand => new Command(async () => await OnSaveInventory());

        public InventEditViewModel()
        {
            
        }

        private async Task OnSaveInventory()
        {
            bool result = false;
            try
            {
                result = await inventoryRepo.UpdateInventoryAsync(Inventory);
            }
            catch(Exception e)
            {
                await alertService.ShowAsync("Błąd", $"W trakcie zapisu towaru wystąpił błąd. Szczegóły:{e.InnerException.Message}", "Zamknij");
            }

            await Page.Navigation.PopAsync();
            
            if(result)
                InventListViewModel.RefreshListCommand.Execute(null);
        }
    }
}
