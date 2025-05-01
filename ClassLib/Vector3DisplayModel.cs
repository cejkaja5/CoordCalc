using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CoordCalc.ClassLib
{
    public class Vector3DisplayModel
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector3DisplayModel(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(X, Y, Z, 1);
        }

        public (float yaw, float pitch, float roll) ToEulerAngles()
        {
            float ToRadians(float degrees) => MathF.PI* degrees / 180f;

            float yaw = ToRadians(X);
            float pitch = ToRadians(Y);
            float roll = ToRadians(Z);
            return (yaw, pitch, roll);
        }

        public static (float yaw, float pitch, float roll) ToEulerAngles(Vector3 eulerDegrees)
        {
            Vector3DisplayModel euler = new Vector3DisplayModel(eulerDegrees);
            return euler.ToEulerAngles();
        }

        public Quaternion ToQuaternionFromYawPitchRoll()
        {
            Vector3 vector = ToVector3();
            return vector.ToQuaternionFromYawPitchRoll();
        }

        public void NormalizeAngles()
        {
            Vector3 vector = ToVector3();
            vector.NormalizeAngles();
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override string ToString()
        {
            return ToString(3);
        }

        public string ToString(int precision)
        {
            string format = $"F{precision}";
            return $"[{X.ToString(format)}  {Y.ToString(format)}  {Z.ToString(format)}]";
        }

        public Vector3DisplayModel RadiansToDegrees()
        {
            return new Vector3DisplayModel(new Vector3(
                ToDegrees(X),
                ToDegrees(Y),
                ToDegrees(Z)));
        }

        public Vector3DisplayModel DegreesToRadians()
        {
            return new Vector3DisplayModel(new Vector3(
                ToRadians(X),
                ToRadians(Y),
                ToRadians(Z)));
        }

        private float ToDegrees(float radians)
        {
            return radians * 180 / MathF.PI;
        }

        private float ToRadians(float degrees)
        {
            return degrees * MathF.PI / 180;
        }
    }       
}
