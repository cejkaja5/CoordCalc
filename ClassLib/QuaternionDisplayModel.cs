using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public class QuaternionDisplayModel
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public QuaternionDisplayModel(Quaternion quaternion)
        {
            X = quaternion.X;
            Y = quaternion.Y;
            Z = quaternion.Z;
            W = quaternion.W;
        }


        public Quaternion ToQuaternion()
        {
            return new Quaternion(X, Y, Z, W);
        }


        public override string ToString()
        {
            return $"[{X:F3}  {Y:F3}  {Z:F3}  {W:F3}]";
        }
    }
}
