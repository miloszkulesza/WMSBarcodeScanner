using System.Drawing;

namespace WMSBarcodeScanner.Infrastructure.Services.Interfaces
{
    public interface IStatusBarPlatformSpecific
    {
        void SetStatusBarColor(Color color);
    }
}
