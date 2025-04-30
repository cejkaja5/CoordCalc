using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;
using System.Collections.ObjectModel;

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


        public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private ObservableCollection<CoordSystem> _children;
        public ObservableCollection<CoordSystem> Children
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
			_children = new ObservableCollection<CoordSystem>();
			if (parent != null)
            {
                parent.AddChild(this);
            }
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

		public static bool IsNameValid(string name, Collection<string> takenNames, out string message)
		{
			if (string.IsNullOrEmpty(name))
			{
                message = "INVALID: Name cannot be empty";
                return false;
			}
			else if (name == "0")
			{
				message = "INVALID: Name '0' is reserved for root system";
				return false;
            }
			else if (takenNames.Contains(name))
            {
                message = $"INVALID: Name '{name}' is already taken";
                return false;
            }
            else if (name.Length > 20)
            {
                message = "INVALID: Name is too long (max 20 characters)";
                return false;
            }
            else if (!name.All(c => char.IsLetterOrDigit(c) || c == '_'))
			{
				message = "INVALID: Name can only contain letters, digits, and underscores";
                return false;
			}
			else
			{
                message = "VALID";
                return true;
            }
        }
	}
}
