using WMSBarcodeScanner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            (this.BindingContext as SettingsViewModel).SaveCommmand.Execute(string.Empty);
        }
    }
}