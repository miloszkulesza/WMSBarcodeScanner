using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptPage : ContentPage
    {
        public ReceiptPage()
        {
            InitializeComponent();
            (BindingContext as BaseViewModel).Page = this;
        }
    }
}