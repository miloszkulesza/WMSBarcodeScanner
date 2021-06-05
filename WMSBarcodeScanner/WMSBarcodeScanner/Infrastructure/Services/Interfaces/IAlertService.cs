using System.Threading.Tasks;

namespace WMSBarcodeScanner.Services.Interfaces
{
    public interface IAlertService
    {
        Task ShowAsync(string title, string message, string buttonText);
    }
}
