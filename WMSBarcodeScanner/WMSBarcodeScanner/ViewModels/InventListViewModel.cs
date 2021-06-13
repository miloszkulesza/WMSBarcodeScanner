using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Infrastructure.Consts;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using WMSBarcodeScanner.Infrastructure.Events;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventListViewModel : BaseViewModel
    {
        #region services
        private readonly IInventoryRepository inventoryRepo = DependencyService.Get<IInventoryRepository>();
        private readonly IAlertService alertService = DependencyService.Get<IAlertService>();
        #endregion

        #region properties
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

        private Inventory selectedItem;
        public Inventory SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                OnSelectedItemChanged();
            }
        }

        private bool resetList;

        public bool ResetList
        {
            get { return resetList; }
            set { SetProperty(ref resetList, value); }
        }

        private ZXingScannerPage scannerPage;

        public ZXingScannerPage ScannerPage
        {
            get { return scannerPage; }
            set { SetProperty(ref scannerPage, value); }
        }
        #endregion

        #region event delegates
        
        #endregion

        #region commands
        public ICommand EditInventoryCommand
        {
            get
            {
                return new Command(async (param) => await OnEditInventory(param));
            }
        }

        public ICommand RemoveInventoryCommand
        {
            get
            {
                return new Command(async (param) => await OnRemoveInventory(param));
            }
        }

        public ICommand SearchByBarcodeCommand
        {
            get
            {
                return new Command(() => OnSearchByBarcode());
            }
        }

        public ICommand RefreshListCommand
        {
            get
            {
                return new Command(async () => await OnRefreshList());
            }
        }
        #endregion

        #region construct
        public InventListViewModel()
        {
            Title = ViewTitles.InventListPage;
            InventoryList = new ObservableCollection<Inventory>(inventoryRepo.GetInventoryAsync().Result);
            ResetList = false;
            RegisterEvents();
        }
        #endregion

        #region private methods
        private async Task OnSearchChanged()
        {
            if (SearchText == string.Empty)
                InventoryList = new ObservableCollection<Inventory>(await inventoryRepo.GetInventoryAsync());
            else
                InventoryList = new ObservableCollection<Inventory>(await inventoryRepo.SearchForInventory(SearchText));
        }

        private async Task OnSelectedItemChanged()
        {
            var detailsPage = new InventDetailsPage(SelectedItem);
            await Page.Navigation.PushAsync(detailsPage);
            SelectedItem = null;
        }

        private async Task OnEditInventory(object param)
        {
            if (param is Inventory)
            {
                var editPage = new InventEditPage(param as Inventory, this);
                await Page.Navigation.PushAsync(editPage);
            }
        }

        private async Task OnRemoveInventory(object param)
        {
            if (param is Inventory)
            {
                await inventoryRepo.DeleteInventoryAsync((param as Inventory).Id);
                InventoryList.Remove(InventoryList.FirstOrDefault(x => x.Id == (param as Inventory).Id));
                await alertService.ShowAsync("Usuwanie towaru", $"Usunięto towar {(param as Inventory).Name}", "Zamknij");
            }
        }

        private void OnSearchByBarcode()
        {
            if (ScannerPage == null)
            {
                ScannerPage = new ZXingScannerPage();
                ScannerPage.Title = ViewTitles.SearchByBarcodeScanningPage;
                ScannerPage.IsScanning = true;
                ScannerPage.IsAnalyzing = true;
                ScannerPage.OnScanResult += OnSearchScannedBarcode;
            }
            else
                ScannerPage.IsAnalyzing = true;
            
            Device.BeginInvokeOnMainThread(async () => await Page.Navigation.PushAsync(ScannerPage));      
        }

        private void OnSearchScannedBarcode(Result result)
        {
            ScannerPage.IsAnalyzing = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Page.Navigation.PopAsync();
                var searchedInventory = await inventoryRepo.SearchByBarcode(result.Text);
                if (searchedInventory.Count() == 0)
                    await alertService.ShowAsync("Nie znaleziono towaru", $"Nie znaleziono zeskanowanego towaru o kodzie kreskowym {result.Text}", "Zamknij");
                else
                {
                    InventoryList = new ObservableCollection<Inventory>(searchedInventory);
                    ResetList = true;
                }
            });
        }

        private async Task OnRefreshList()
        {
            InventoryList = new ObservableCollection<Inventory>(await inventoryRepo.GetInventoryAsync());
            ResetList = false;
        }

        private void RegisterEvents()
        {
            inventoryRepo.InventoryAdd += InventoryAddEventHandler;
        }
        #endregion

        #region event handlers
        private void InventoryAddEventHandler(object sender, InventoryAddEventArgs args)
        {
            InventoryList.Add(args.Inventory);
        }
        #endregion
    }
}
