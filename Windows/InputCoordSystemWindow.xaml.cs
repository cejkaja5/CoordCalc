using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for InputCoordSystemWindow.xaml
    /// </summary>
    public partial class InputCoordSystemWindow : Window, INotifyPropertyChanged
    {
        public InputCoordSystemWindow(Matrix4x4 matrix, CoordSystem parent)
        {
            ParentSystem = parent;
            Matrix = new MatrixDisplayModel(matrix);
            InitializeComponent();
        }

        public InputCoordSystemWindow(CoordSystem parent) : this(Matrix4x4.Identity, parent)
        {
        }

        private MatrixDisplayModel _matrix = new MatrixDisplayModel(Matrix4x4.Identity);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MatrixDisplayModel Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        public Matrix4x4  OutputMatrix
        {
            get 
            {
                return new Matrix4x4(
                    Matrix.M11, Matrix.M12, Matrix.M13, Matrix.M14,
                    Matrix.M21, Matrix.M22, Matrix.M23, Matrix.M24,
                    Matrix.M31, Matrix.M32, Matrix.M33, Matrix.M34,
                    Matrix.M41, Matrix.M42, Matrix.M43, Matrix.M44
                );
            }
        }
        private readonly CoordSystem _parentSystem;

        public CoordSystem ParentSystem
        {
            get { return _parentSystem; }
            init { _parentSystem = value; }
        }

        private bool _success = false;

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        private string _systemName = "K";

        public string SystemName
        {
            get { return _systemName ; }
            set { _systemName = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Success = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbAsMatrix_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
