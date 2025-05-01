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
            {
                int precision = GlobalSettings.FloatPrecisionDefault;

                if (parameter is string paramStr)
                {
                    switch (paramStr.Trim().ToLower())
                    {
                        case "translationvector":
                            precision = GlobalSettings.FloatPrecisionTranslationVector;
                            break;
                        case "scalevector":
                            precision = GlobalSettings.FloatPrecisionScaleVector;
                            break;
                        case "euleranglesdeg":
                            precision = GlobalSettings.FloatPrecisionEulerAnglesDeg;
                            break;
                        case "euleranglesrad":
                            precision = GlobalSettings.FloatPrecisionEulerAnglesRad;
                            break;
                        case "quaternion":
                            precision = GlobalSettings.FloatPrecisionQuaternion;
                            break;
                        case "matrix":
                            precision = GlobalSettings.FloatPrecisionMatrix;
                            break;
                        case "default":
                        default:
                            if (int.TryParse(paramStr, out int parsedPrecision))
                                precision = parsedPrecision;
                            break;
                    }
                }

                string format = $"F{precision}";
                return f.ToString(format, culture);
            }

            return 0f.ToString($"F{GlobalSettings.FloatPrecisionDefault}", culture);
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
