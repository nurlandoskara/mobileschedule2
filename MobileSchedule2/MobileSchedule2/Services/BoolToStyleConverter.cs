using System;
using Xamarin.Forms;

namespace MobileSchedule2.Services
{
    public class BoolToStyleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Color.FromHex("#ecf0f1");

            return (bool)value ? Color.FromHex("#27ae60") : Color.FromHex("#ecf0f1");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
