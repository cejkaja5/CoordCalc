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
            // Convert to rotation matrix
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);

            float pitch, yaw, roll;

            // Extract Euler angles from rotation matrix
            if (m.M31 < 1)
            {
                if (m.M31 > -1)
                {
                    pitch = MathF.Asin(-m.M31);
                    yaw = MathF.Atan2(m.M32, m.M33);
                    roll = MathF.Atan2(m.M21, m.M11);
                }
                else
                {
                    // m.M31 == -1
                    pitch = MathF.PI / 2;
                    yaw = -MathF.Atan2(-m.M12, m.M22);
                    roll = 0;
                }
            }
            else
            {
                // m.M31 == +1
                pitch = -MathF.PI / 2;
                yaw = MathF.Atan2(-m.M12, m.M22);
                roll = 0;
            }

            // Convert to degrees
            Vector3 anglesDeg = new Vector3(
                pitch * 180f / MathF.PI,
                yaw * 180f / MathF.PI,
                roll * 180f / MathF.PI
            );

            // Normalize to [0, 360)
            anglesDeg.X = NormalizeAngle(anglesDeg.X);
            anglesDeg.Y = NormalizeAngle(anglesDeg.Y);
            anglesDeg.Z = NormalizeAngle(anglesDeg.Z);

            return anglesDeg;
        }

        private static float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0)
                angle += 360f;
            return angle;
        }
    }
}
