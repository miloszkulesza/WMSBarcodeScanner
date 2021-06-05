using WMSBarcodeScanner.Infrastructure.DataAccess.Interfaces;
using WMSBarcodeScanner.Infrastructure.DataAccess.Repositories;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using WMSBarcodeScanner.Infrastructure.Services.Services;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;

namespace WMSBarcodeScanner
{
    public partial class App : Application
    {

        public App()
        {
            DependencyService.Register<IInventoryRepository, InventoryMockRepository>();
            DependencyService.Register<IAlertService, AlertService>();
            DependencyService.Register<IUserRepository, UserMockRepository>();

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
