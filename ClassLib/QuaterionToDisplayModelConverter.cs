using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoordCalc.ClassLib
{
    public class QuaternionToDisplayModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Quaternion quaternion)
                return new QuaternionDisplayModel(quaternion);
            throw new ArgumentException("Trying to convert something that is not Quaternion");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is QuaternionDisplayModel model)
            {
                return new Quaternion(
                    model.X, model.Y, model.Z, model.W
                );
            }
            throw new ArgumentException("Trying to convert something that is not QuaternionDisplayModel");
        }
    }
}
