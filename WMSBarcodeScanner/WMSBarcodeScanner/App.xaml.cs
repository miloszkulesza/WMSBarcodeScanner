using System;
using WMSBarcodeScanner.Services;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WMSBarcodeScanner
{
    public partial class App : Application
    {

        public App()
        {
            DependencyService.Register<InventoryMockRepository>();
            DependencyService.Register<IAlertService, AlertService>();

            InitializeComponent();
            
            MainPage = new AppShell();
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
