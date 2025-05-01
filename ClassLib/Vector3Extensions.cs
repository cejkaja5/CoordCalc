using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public static class Vector3Extensions
    {
        public static string ToCustomString(this Vector3 v)
        {
            return ToCustomString(v, GlobalSettings.FloatPrecisionDefault);
        }

        public static string ToCustomString(this Vector3 v, int precision)
        {
            string format = $"F{precision}";
            return $"[{v.X.ToString(format)}  {v.Y.ToString(format)}  {v.Z.ToString(format)}]";
        }

        public static string ToEulerAnglesString(this Vector3 v)
        {
            return $"[yaw  pitch  roll] = [{v.X:F2}  {v.Y:F2}  {v.Z:F2}]";
        }

        public static void NormalizeAngles(this Vector3 v)
        {
            v.X = NormalizeAngle(v.X); // Yaw is in (-180, 180]
            v.Y = NormalizeAngle(v.Y); // Pitch is in [-90, 90]
            v.Z = NormalizeAngle(v.Z); // Roll is in (-180, 180]
        }

        private static float NormalizeAngle(float angle)
        {
            angle %= 360.0f;
            if (angle <= -180.0f) angle += 360.0f;
            else if (angle > 180.0f) angle -= 360.0f;
            return angle;
        }

        public static Quaternion ToQuaternionFromYawPitchRoll(this Vector3 v)
        {
            float yaw = MathF.PI * v.X / 180f;
            float pitch = MathF.PI * v.Y / 180f;
            float roll = MathF.PI * v.Z / 180f;
            return Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        public static Vector3 DegreesToRadians(this Vector3 v)
        {
            return new Vector3(
                ToRadians(v.X),
                ToRadians(v.Y),
                ToRadians(v.Z));
        }

        private static float ToRadians(float degrees)
        {
            return MathF.PI * degrees / 180f;
        }
    }
}
