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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public InputCoordSystemWindow(Matrix4x4 matrix, CoordSystem parent)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ParentSystem = parent;
            Matrix = new MatrixDisplayModel(matrix);
            DataContext = this;
            InitializeComponent();
            cbAsMatrix.IsChecked = true;
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

        public Matrix4x4 OutputMatrix
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

        private bool _okbtnIsEnabledValid = false;

        public bool OKbtnIsEnabledValid
        {
            get { return _okbtnIsEnabledValid; }
            set 
            {
                if (_okbtnIsEnabledValid != value)
                {
                    _okbtnIsEnabledValid = value; 
                    OnPropertyChanged(nameof(OKbtnIsEnabledValid));
                }
            }
        }
         

        private string _systemName = String.Empty;
         
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


        private void cbAsMatrix_Checked(object sender, RoutedEventArgs e)
        {
            cbAsAnglesAndTranslation.IsChecked = false;
            cbAsVector.IsChecked = false;
            mainContentMatrix.Visibility = Visibility.Visible;
        }

        private void cbAsMatrix_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContentMatrix.Visibility = Visibility.Collapsed;
        }

        private void cbAsVector_Checked(object sender, RoutedEventArgs e)
        {
            cbAsAnglesAndTranslation.IsChecked = false;
            cbAsMatrix.IsChecked = false;
            mainContentVector.Visibility = Visibility.Visible;
            MatrixInputAsVector = CoordSystemsTree.MatrixToString(OutputMatrix);
        }

        private void cbAsVector_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContentVector.Visibility = Visibility.Collapsed;
        }

        private void cbAsAnglesAndTranslation_Checked(object sender, RoutedEventArgs e)
        {
            cbAsMatrix.IsChecked = false;
            cbAsVector.IsChecked = false;
            mainContentAnglesAndTranslation.Visibility = Visibility.Visible;
        }

        private void cbAsAnglesAndTranslation_Unchecked(object sender, RoutedEventArgs e)
        {
            mainContentAnglesAndTranslation.Visibility = Visibility.Collapsed;
        }

        private void tbEnterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CoordSystem.IsNameValid(tbEnterName.Text))
            {
                SystemName = tbEnterName.Text;
                OKbtnIsEnabledValid = true;
            }
            else
            {
                SystemName = string.Empty;
                OKbtnIsEnabledValid = false;
            }
            OnPropertyChanged(nameof(OKbtnIsEnabledValid)); 
        }

        private string _matrixInputAsVector = string.Empty;

        public string MatrixInputAsVector
        {
            get { return _matrixInputAsVector ; }
            set 
            {
                if (value != _matrixInputAsVector)
                {
                    _matrixInputAsVector = value; 
                    OnPropertyChanged(nameof(MatrixInputAsVector));
                }
            }
        }

        private void tbEnterMatrixAsVector_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Matrix4x4 matrix = CoordSystemsTree.StringToMatrix(tbEnterMatrixAsVector.Text);
                Matrix = new MatrixDisplayModel(matrix);
                OnPropertyChanged(nameof(Matrix));
            }
            catch
            {
                ;
            }
        }
    }
}
