using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;
using System.Windows.Data;

namespace CoordCalc.ClassLib
{
    public class Vector3ToDisplayModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vector3 vector)
                return new Vector3DisplayModel(vector);
            throw new ArgumentException("Trying to convert something that is not Vector3");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Vector3DisplayModel model)
            {
                return new Vector3(
                    model.X, model.Y, model.Z
                );
            }
            throw new ArgumentException("Trying to convert something that is not Vector3DisplayModel");
        }
    }
}
