using System.Threading.Tasks;

namespace WMSBarcodeScanner.Infrastructure.Services.Interfaces
{
    public interface IAlertService
    {
        void Show(string title, string message, string buttonText);
    }
}
