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
    public class Matrix4x4ToDisplayModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Matrix4x4 matrix)
                return new MatrixDisplayModel(matrix);
            throw new ArgumentException("Trying to convert something that is not Matrix4x4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is MatrixDisplayModel model)
            {
                return new Matrix4x4(
                    model.M11, model.M12, model.M13, model.M14,
                    model.M21, model.M22, model.M23, model.M24,
                    model.M31, model.M32, model.M33, model.M34,
                    model.M41, model.M42, model.M43, model.M44
                );
            }
            throw new ArgumentException("Trying to convert something that is not MatrixDisplayModel");
        }
    }
}
