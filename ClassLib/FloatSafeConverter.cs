using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoordCalc.ClassLib
{
    public class FloatSafeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float f)
                return f.ToString("F3", culture);

            return "0.000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? input = value as string;

            if (string.IsNullOrWhiteSpace(input))
                return 0f;

            input = input.Replace(',', '.');

            if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                return result;

            return 0f;
        }
    }
}
