using System;
using System.Collections.Generic;
using WMSBarcodeScanner.ViewModels;
using WMSBarcodeScanner.Views;
using Xamarin.Forms;

namespace WMSBarcodeScanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(InventListPage), typeof(InventListPage));
            Routing.RegisterRoute(nameof(PickPage), typeof(PickPage));
            Routing.RegisterRoute(nameof(ReceiptPage), typeof(ReceiptPage));
            Routing.RegisterRoute(nameof(PickPage), typeof(PickPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

    }
}
