using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WMSBarcodeScanner.Services
{
    public class AlertService : IAlertService
    {
        public async Task ShowAsync(string title, string message, string buttonText)
        {
            await App.Current.MainPage.DisplayAlert(title, message, buttonText);
        }
    }
}
