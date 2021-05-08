using WMSBarcodeScanner.Models;
using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventDetailsPage : ContentPage
    {
        public InventDetailsPage(Inventory inventory)
        {
            InitializeComponent();
            (BindingContext as InventDetailsViewModel).Inventory = inventory;
        }
    }
}