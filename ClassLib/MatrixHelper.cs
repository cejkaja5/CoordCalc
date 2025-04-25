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
            if (matrix.M14 != 0 || matrix.M24 != 0 || matrix.M34 != 0)
            {
                message = "INVALID: Matrix contains translation components in the last column.";
                return false;
            }

            if (!Matrix4x4.Invert(matrix, out _))
            {
                float determinant = matrix.GetDeterminant();
                message = $"INVALID: Matrix is not invertible, determinant is {determinant}";
                return false;
            }

            Vector3 col0 = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Vector3 col1 = new Vector3(matrix.M21, matrix.M22, matrix.M23);
            Vector3 col2 = new Vector3(matrix.M31, matrix.M32, matrix.M33);

            float epsilon = 1e-6f;
            if (col0.LengthSquared() < epsilon || col1.LengthSquared() < epsilon || col2.LengthSquared() < epsilon)
            {
                message = "INVALID: One or more scale vectors are near zero.";
                return false;
            }

            float dot01 = Vector3.Dot(Vector3.Normalize(col0), Vector3.Normalize(col1));
            float dot02 = Vector3.Dot(Vector3.Normalize(col0), Vector3.Normalize(col2));
            float dot12 = Vector3.Dot(Vector3.Normalize(col1), Vector3.Normalize(col2));

            if (Math.Abs(dot01) > 0.01f || Math.Abs(dot02) > 0.01f || Math.Abs(dot12) > 0.01f)
            {
                message = "WARNING: Matrix may contain skew (non-orthogonal basis vectors)";
                return true;
            }

            message = "Matrix is valid";
            return true;
        }

        public static Matrix4x4 CreateMatrixFromScaleTranslationRotation(Vector3 scale, Vector3 translation, Quaternion rotation)
        {
            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(translation);
            Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);
            Matrix4x4 result = scaleMatrix * rotationMatrix * translationMatrix;
            return result;
        }
    }
}
