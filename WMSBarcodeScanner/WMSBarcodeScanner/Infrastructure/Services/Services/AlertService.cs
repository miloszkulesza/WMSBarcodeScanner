using System.Threading.Tasks;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;

namespace WMSBarcodeScanner.Infrastructure.Services.Services
{
    public class AlertService : IAlertService
    {
        public async Task ShowAsync(string title, string message, string buttonText)
        {
            await App.Current.MainPage.DisplayAlert(title, message, buttonText);
        }
    }
}
