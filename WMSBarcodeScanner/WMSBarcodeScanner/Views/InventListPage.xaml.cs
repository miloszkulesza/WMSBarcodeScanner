using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventListPage : ContentPage
    {

        public InventListPage()
        {
            InitializeComponent();
            (BindingContext as InventListViewModel).Page = this;
        }
    }
}
