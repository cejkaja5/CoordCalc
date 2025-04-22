using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public static class QuaternionExtensions
    {
        public static string ToCustomString(this Quaternion q)
        {
            return $"[{q.X:F3}  {q.Y:F3}  {q.Z:F3}  {q.W:F3}]";
        }

        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            q = Quaternion.Normalize(q);

            float x = q.X;
            float y = q.Y;
            float z = q.Z;
            float w = q.W;

            float yaw, pitch, roll;
            // Yaw (Y-axis rotation)
            float sinPitch = -2.0f * (y * z - w * x);


            // Clamp to avoid NaNs due to slight overflows
            sinPitch = Math.Clamp(sinPitch, -1.0f, 1.0f);

            bool gimbalLock = MathF.Abs(sinPitch) >= 0.9999f;


            if (gimbalLock)
            {
                // Gimbal lock: set roll = 0
                yaw = MathF.Atan2(-2.0f * (x * z - w * y), 1.0f - 2.0f * (y * y + z * z));
                pitch = (MathF.PI/2) * sinPitch;
                roll = 0;
            }
            else
            {
                // Regular case
                pitch = MathF.Asin(sinPitch);
                yaw = MathF.Atan2(2.0f * (x * z + y * w), 1.0f - 2.0f * (x * x + y * y));
                roll = MathF.Atan2(2.0f * (x * y + w * z), 1.0f - 2.0f * (x * x + z * z));
            }

            // Convert radians to degrees
            pitch *= 180.0f / MathF.PI;
            yaw *= 180.0f / MathF.PI;
            roll *= 180.0f / MathF.PI;

            // Clamp yaw and roll into (-180, 180]
            yaw = WrapAngle(yaw);
            roll = WrapAngle(roll);

            return new Vector3(yaw, pitch, roll);
        }

        // Utility: Wrap angle to (-180, 180]
        private static float WrapAngle(float angle)
        {
            angle %= 360.0f;
            if (angle <= -180.0f) angle += 360.0f;
            else if (angle > 180.0f) angle -= 360.0f;
            return angle;
        }
    }
}
