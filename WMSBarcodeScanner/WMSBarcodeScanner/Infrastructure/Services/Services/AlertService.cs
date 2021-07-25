using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using Xamarin.Forms;

namespace WMSBarcodeScanner.Infrastructure.Services.Services
{
    public class AlertService : IAlertService
    {
        public void Show(string title, string message, string buttonText)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(title, message, buttonText);
            });            
        }
    }
}
