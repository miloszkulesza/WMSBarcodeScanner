using WMSBarcodeScanner.Models;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventEditViewModel : BaseViewModel
    {
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

        public InventEditViewModel()
        {
            
        }
    }
}
