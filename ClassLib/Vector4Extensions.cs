using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CoordCalc.ClassLib
{
    public static class Vector4Extensions
    {
        public static Vector4 TransformAsRowVector(this Vector4 v, Matrix4x4 matrix)
        {
            return new Vector4(
                v.X * matrix.M11 + v.Y * matrix.M21 + v.Z * matrix.M31 + v.W * matrix.M41,
                v.X * matrix.M12 + v.Y * matrix.M22 + v.Z * matrix.M32 + v.W * matrix.M42,
                v.X * matrix.M13 + v.Y * matrix.M23 + v.Z * matrix.M33 + v.W * matrix.M43,
                v.X * matrix.M14 + v.Y * matrix.M24 + v.Z * matrix.M34 + v.W * matrix.M44
            );
        }
    }
}
