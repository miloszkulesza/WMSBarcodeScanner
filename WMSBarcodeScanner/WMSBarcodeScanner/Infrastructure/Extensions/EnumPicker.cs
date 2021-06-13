using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace WMSBarcodeScanner.Infrastructure.Extensions
{
    public class EnumPicker<T> : Picker where T : struct
    {
        public EnumPicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
            foreach (var value in Enum.GetValues(typeof(T)))
                Items.Add(GetEnumName(value));
            Title = Items[0].ToString();
        }

        public static new BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(T), typeof(EnumPicker<T>), default(T), propertyChanged: OnSelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);

        public new T SelectedItem
        {
            get { return (T)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
                SelectedItem = default(T);
            else
            {
                T match;
                if (!Enum.TryParse<T>(Items[SelectedIndex], out match))
                    match = GetEnumByName(Items[SelectedIndex]);
                SelectedItem = (T)Enum.Parse(typeof(T), match.ToString());
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as EnumPicker<T>;
            if (newvalue != null)
                picker.SelectedIndex = picker.Items.IndexOf(GetEnumName(newvalue));
        }

        private static string GetEnumName(object value)
        {
            string result = value.ToString();
            DisplayAttribute attribute = typeof(T).GetRuntimeField(value.ToString()).GetCustomAttributes<DisplayAttribute>(false).SingleOrDefault();

            if (attribute != null)
                result = attribute.Name;

            return result;
        }

        private T GetEnumByName(string Name)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(x => string.Equals(GetEnumName(x), Name));
        }
    }
}
