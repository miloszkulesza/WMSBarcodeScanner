using System.Drawing;

namespace WMSBarcodeScanner.Services
{
    public interface IStatusBarPlatformSpecific
    {
        void SetStatusBarColor(Color color);
    }
}
