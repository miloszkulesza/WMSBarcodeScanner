using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventEditPage : ContentPage
    {
        public InventEditPage(Inventory inventory,
                              BaseViewModel viewModel,
                              bool newInventory = false)
        {
            InitializeComponent();
            (BindingContext as InventEditViewModel).Inventory = inventory;
            (BindingContext as BaseViewModel).Page = this;
            (BindingContext as InventEditViewModel).ViewModel = viewModel;
            if(newInventory)
                (BindingContext as InventEditViewModel).NewInventory = newInventory;
            (BindingContext as InventEditViewModel).SetPageTitle();
        }
    }
}