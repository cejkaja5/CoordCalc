using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public static class Vector3Extensions
    {
        public static string ToCustomString(this Vector3 v)
        {
            return $"[{v.X:F3}  {v.Y:F3}  {v.Z:F3}]";
        }

        public static string ToEulerAnglesString(this Vector3 v)
        {
            return $"[pitch  yaw  roll] = [{v.X:F2}  {v.Y:F2}  {v.Z:F2}]";
        }
    }
}
