using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Services;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventListViewModel : BaseViewModel
    {
        #region services
        private readonly IInventoryRepository inventoryRepo = DependencyService.Get<IInventoryRepository>();
        private readonly IAlertService alertService = DependencyService.Get<IAlertService>();
        #endregion

        #region properties
        public Page Page { get; set; }

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
        #endregion

        #region commands
        public ICommand EditInventoryCommand
        {
            get
            {
                return new Command(OnEditInventory);
            }
        }
        #endregion

        #region construct
        public InventListViewModel()
        {
            Title = ViewTitles.InventListPage;
            InventoryList = new ObservableCollection<Inventory>(inventoryRepo.GetInventoryAsync().Result);
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

        //TODO: nie działa
        private async void OnEditInventory(object param)
        {
            var editPage = new InventEditPage(param as Inventory);
            await Page.Navigation.PushAsync(editPage);
        }
        #endregion
    }
}
