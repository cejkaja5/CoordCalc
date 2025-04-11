using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CoordCalc.ClassLib
{
    public class CoordSystem
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private Matrix4x4 _matrix;

		public Matrix4x4 Matrix
        {
			get { return _matrix; }
			set { _matrix = value; }
		}

		private readonly CoordSystem? _parent;

		public CoordSystem Parent
		{
			get { return _parent!; }
		}

		private List<CoordSystem> _children;

		public List<CoordSystem> Children
		{
			get { return _children; }
		}

		public void AddChild(CoordSystem child)
		{
			Children.Add(child);
		}

		public CoordSystem(Matrix4x4 matrix, string name, CoordSystem? parent) 
		{
			_matrix = matrix;
			_name = name;
			_parent = parent;
			_children = new List<CoordSystem>();
		}

		public static CoordSystem GetGlobalCoordSystem()
		{
			return new CoordSystem(Matrix4x4.Identity, "0", null);
		}
    }
}
