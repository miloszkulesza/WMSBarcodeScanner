using WMSBarcodeScanner.Services.DataAccess.Repositories;
using WMSBarcodeScanner.Services.Interfaces;
using WMSBarcodeScanner.Services.Services;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;

namespace WMSBarcodeScanner
{
    public partial class App : Application
    {

        public App()
        {
            DependencyService.Register<InventoryMockRepository>();
            DependencyService.Register<IAlertService, AlertService>();

            InitializeComponent();
            
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }        
    }
}
