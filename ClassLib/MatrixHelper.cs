using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public static class MatrixHelper
    {
        public static bool CheckIfMatrixIsDecomposable(Matrix4x4 matrix, out string message)
        {
            if (!Matrix4x4.Invert(matrix, out _))
            {
                float determinant = matrix.GetDeterminant();
                message = $"Matrix is not invertible, determinant is {determinant}";
                return false;
            }

            Vector3 col0 = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Vector3 col1 = new Vector3(matrix.M21, matrix.M22, matrix.M23);
            Vector3 col2 = new Vector3(matrix.M31, matrix.M32, matrix.M33);

            float epsilon = 1e-6f;
            if (col0.LengthSquared() < epsilon || col1.LengthSquared() < epsilon || col2.LengthSquared() < epsilon)
            {
                message = "One or more scale vectors are near zero.";
                return false;
            }

            float dot01 = Vector3.Dot(Vector3.Normalize(col0), Vector3.Normalize(col1));
            float dot02 = Vector3.Dot(Vector3.Normalize(col0), Vector3.Normalize(col2));
            float dot12 = Vector3.Dot(Vector3.Normalize(col1), Vector3.Normalize(col2));

            if (Math.Abs(dot01) > 0.01f || Math.Abs(dot02) > 0.01f || Math.Abs(dot12) > 0.01f)
            {
                message = "Matrix may contain skew (non-orthogonal basis vectors)";
                return false;
            }

            message = "Matrix succesfully passed decomposition check";
            return true;
        }
    }
}
