using System.Threading.Tasks;

namespace WMSBarcodeScanner.Services
{
    public interface IAlertService
    {
        Task ShowAsync(string title, string message, string buttonText);
    }
}
