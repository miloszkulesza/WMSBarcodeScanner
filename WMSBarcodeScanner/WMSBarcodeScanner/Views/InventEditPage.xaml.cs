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
                              InventListViewModel viewModel)
        {
            InitializeComponent();
            (BindingContext as InventEditViewModel).Inventory = inventory;
            (BindingContext as BaseViewModel).Page = this;
            (BindingContext as InventEditViewModel).InventListViewModel = viewModel;
        }
    }
}