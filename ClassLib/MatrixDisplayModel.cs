using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Numerics;
using System.ComponentModel;

namespace CoordCalc.ClassLib
{
    public class MatrixDisplayModel
    {
        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }
        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }
        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }
        public float M41 { get; set; }
        public float M42 { get; set; }
        public float M43 { get; set; }
        public float M44 { get; set; }

        public MatrixDisplayModel(Matrix4x4 matrix)
        {
            M11 = matrix.M11;
            M12 = matrix.M12;
            M13 = matrix.M13;
            M14 = matrix.M14;
            M21 = matrix.M21;    
            M22 = matrix.M22;
            M23 = matrix.M23;
            M24 = matrix.M24;
            M31 = matrix.M31;
            M32 = matrix.M32;
            M33 = matrix.M33;
            M34 = matrix.M34;
            M41 = matrix.M41;
            M42 = matrix.M42;
            M43 = matrix.M43;
            M44 = matrix.M44;
        }
    }
}
