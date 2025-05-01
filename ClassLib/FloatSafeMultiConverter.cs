using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoordCalc.ClassLib
{
    public class FloatSafeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is float f && values[1] is string vectorType)
            {
                int precision = GlobalSettings.FloatPrecisionDefault;

                switch (vectorType)
                {
                    case "TranslationVector":
                        precision = GlobalSettings.FloatPrecisionTranslationVector;
                        break;
                    case "ScaleVector":
                        precision = GlobalSettings.FloatPrecisionScaleVector;
                        break;
                    case "EulerAnglesDeg":
                        precision = GlobalSettings.FloatPrecisionEulerAnglesDeg;
                        break;
                    case "EulerAnglesRad":
                        precision = GlobalSettings.FloatPrecisionEulerAnglesRad;
                        break;
                    case "Quaternion":
                        precision = GlobalSettings.FloatPrecisionQuaternion;
                        break;
                    case "Matrix":
                        precision = GlobalSettings.FloatPrecisionMatrix;
                        break;
                }

                return f.ToString($"F{precision}", culture);
            }

            return "0.000";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string input = value as string ?? "";
            input = input.Replace(',', '.');

            if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
                return new object[] { f, Binding.DoNothing }; // Only first value is editable

            return new object[] { 0f, Binding.DoNothing };
        }
    }
}
