using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickPage : ContentPage
    {
        public PickPage()
        {
            InitializeComponent();
            (BindingContext as BaseViewModel).Page = this;
        }
    }
}