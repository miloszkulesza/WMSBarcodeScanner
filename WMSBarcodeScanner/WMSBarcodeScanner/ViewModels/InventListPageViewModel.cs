using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Services;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventListPageViewModel : BaseViewModel
    {
        public readonly IInventoryRepository inventoryRepo = DependencyService.Get<IInventoryRepository>();

        private ObservableCollection<Inventory> inventoryList;
        public ObservableCollection<Inventory> InventoryList
        {
            get { return inventoryList; }
            set { SetProperty(ref inventoryList, value); }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            { 
                SetProperty(ref searchText, value);
                OnSearchChanged();
            }
        }


        private async Task OnSearchChanged()
        {
            if (SearchText == string.Empty)
                InventoryList = new ObservableCollection<Inventory>(await inventoryRepo.GetInventoryAsync());
            else
                InventoryList = new ObservableCollection<Inventory>(await inventoryRepo.SearchForInventory(SearchText));
        }

        public InventListPageViewModel()
        {
            Title = ViewTitles.InventListPage;
            InventoryList = new ObservableCollection<Inventory>(inventoryRepo.GetInventoryAsync().Result);
        }
    }
}
