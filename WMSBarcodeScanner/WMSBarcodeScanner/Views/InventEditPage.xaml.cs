using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventEditPage : ContentPage
    {
        public InventEditPage(Inventory inventory)
        {
            InitializeComponent();
            (BindingContext as InventEditViewModel).Inventory = inventory;
        }
    }
}