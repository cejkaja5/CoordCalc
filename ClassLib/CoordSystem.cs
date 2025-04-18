using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace CoordCalc.ClassLib
{
    public class CoordSystem : INotifyPropertyChanged
    {
		private string _name;

		public string Name
		{
			get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }

            }
        }

		private Matrix4x4 _matrix;

		public Matrix4x4 Matrix
        {
			get { return _matrix; }
			set 
			{
				if (_matrix != value)
				{ 
					_matrix = value; 
					OnPropertyChanged(nameof(Matrix));
				}
				
			}
		}

		private readonly CoordSystem? _parent;

		public CoordSystem Parent
		{
			get { return _parent!; }
		}

		private List<CoordSystem> _children;

        public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        public List<CoordSystem> Children
		{
			get { return _children; }
		}

		public void AddChild(CoordSystem child)
		{
			Children.Add(child);
			OnPropertyChanged(nameof(Children));
		}

		public CoordSystem(Matrix4x4 matrix, string name, CoordSystem? parent) 
		{
			_matrix = matrix;
			_name = name;
			_parent = parent;
			_children = new List<CoordSystem>();
		}

		public static CoordSystem GetRootCoordSystem()
		{
			return new CoordSystem(Matrix4x4.Identity, "0", null);
		}

		public bool IsRoot()
		{
			return (bool)(_name == "0");
		}

		public override string ToString() 
		{
			return _name;
		}

		public static bool IsNameValid(string name)
		{
			if (string.IsNullOrEmpty(name) || name == "0")
			{
				return false;
			}
			else
			{
				return name.All(c => char.IsLetterOrDigit(c) || c == '_');
			}
        }
    }
}
