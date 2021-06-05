using Android.OS;
using Plugin.CurrentActivity;
using WMSBarcodeScanner.Infrastructure.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(WMSBarcodeScanner.Droid.CustomRenderers.Statusbar))]
namespace WMSBarcodeScanner.Droid.CustomRenderers
{
    public class Statusbar : IStatusBarPlatformSpecific
    {        
        public void SetStatusBarColor(System.Drawing.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.AddLuminosity(-0.1f).ToPlatformColor();
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
            }
        }
    }
}