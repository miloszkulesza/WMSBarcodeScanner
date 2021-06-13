using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Markup;
using WMSBarcodeScanner.Models;
using Xamarin.Forms;

namespace WMSBarcodeScanner.Infrastructure.Extensions
{
    public class InventoryUnitConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DisplayAttribute attribute = typeof(InventoryUnit).GetRuntimeField(value.ToString()).GetCustomAttributes<DisplayAttribute>(false).SingleOrDefault();
            return attribute.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
