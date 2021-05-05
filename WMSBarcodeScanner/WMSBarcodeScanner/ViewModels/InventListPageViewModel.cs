using WMSBarcodeScanner.Consts;
using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.Services;
using Xamarin.Forms;

namespace WMSBarcodeScanner.ViewModels
{
    public class InventListPageViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public InventListPageViewModel()
        {
            Title = ViewTitles.InventListPage;
        }
    }
}
