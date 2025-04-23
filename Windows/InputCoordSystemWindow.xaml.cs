using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        public InputCoordSystemWindow(Matrix4x4 matrix, CoordSystem parent, Collection<string> takenNames, string name)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ParentSystem = parent;
            Matrix = new MatrixDisplayModel(matrix);
            DataContext = this;
            Name = name;
            TakenNames = takenNames;
            InitializeComponent();
            tbEnterName.Text = name;
            cbAsMatrix.IsChecked = true;
            btnCheckValidity_Click(this, new RoutedEventArgs());
        }

        public InputCoordSystemWindow(CoordSystem parent, Collection<string> takenNames) : this(Matrix4x4.Identity, parent, takenNames, string.Empty)
        {
        }

        private MatrixDisplayModel _matrix = new MatrixDisplayModel(Matrix4x4.Identity);

        private Collection<string> _takenNames;

        public Collection<string> TakenNames
        {
            get { return _takenNames; }
            init 
            {
                 _takenNames = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MatrixDisplayModel Matrix
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

        public bool OKbtnIsEnabled
        {
            get 
            { 
                return CoordSystem.IsNameValid(SystemName, TakenNames, out string message) && IsMatrixValid; 
            }
        }
         

        private string _systemName = String.Empty;
         
        public string SystemName
        {
            get { return _systemName ; }
            set 
            {
                if (_systemName != value) 
                { 
                    _systemName = value;
                    OnPropertyChanged(nameof(OKbtnIsEnabled));
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            btnCheckValidity_Click(sender, e);
            if (OKbtnIsEnabled == false)
            {
                return;
            }
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
            if (CoordSystem.IsNameValid(tbEnterName.Text, TakenNames, out string message))
            {
                SystemName = tbEnterName.Text;
                btnCheckValidity_Click(this, new RoutedEventArgs());                
            }
            else
            {
                ValidationMessage = message;
                SystemName = string.Empty;
            }
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
        }

        private Vector3DisplayModel _translationVector;

        public Vector3DisplayModel TranslationVector
        {
            get { return _translationVector; }
            set 
            {
                if (_translationVector != value)
                {
                    _translationVector = value;
                    OnPropertyChanged(nameof(TranslationVector));
                }
            }
        }

        private Vector3DisplayModel _scaleVector;

        public Vector3DisplayModel ScaleVector
        {
            get { return _scaleVector; }
            set 
            {
                if (_scaleVector != value)
                {
                    _scaleVector = value;
                    OnPropertyChanged(nameof(ScaleVector));
                } 
            }
        }

        private Vector3DisplayModel _eulerAnglesVector;

        public Vector3DisplayModel EulerAnglesVector
        {
            get { return _eulerAnglesVector; }
            set
            {
                if (_eulerAnglesVector != value)
                {
                    _eulerAnglesVector = value;
                    OnPropertyChanged(nameof(EulerAnglesVector));
                }
            }
        }


        private QuaternionDisplayModel _rotationQuaternion;

        public QuaternionDisplayModel RotationQuaternion
        {
            get { return _rotationQuaternion; }
            set 
            {
                if (_rotationQuaternion != value)
                {
                    _rotationQuaternion = value;
                    OnPropertyChanged(nameof(RotationQuaternion));
                }
            }
        }

        private void UpdateMatrixFromScaleTranslationQuaternion()
        {
            if (cbEulerAngles.IsChecked == true)
            {
                var (yaw, pitch, roll) = EulerAnglesVector.ToEulerAngles();
                System.Numerics.Quaternion rotation = System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
                RotationQuaternion = new QuaternionDisplayModel(rotation);
            }

            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(ScaleVector.ToVector3());
            Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(RotationQuaternion.ToQuaternion());
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(TranslationVector.ToVector3());
            Matrix4x4 result =  scaleMatrix * rotationMatrix * translationMatrix;
            Matrix = new MatrixDisplayModel(result);
        }

        private void btnCheckValidity_Click(object sender, RoutedEventArgs e)
        {
            bool nameValid = false;

            if (CoordSystem.IsNameValid(tbEnterName.Text, TakenNames, out string message))
            {
                nameValid = true;
                SystemName = tbEnterName.Text;
            }
            else
            {
                ValidationMessage = message;
                SystemName = string.Empty;
            }

            if (cbAsMatrix.IsChecked == true)
            {
                tbEnterMatrixAsVector.Text = CoordSystemsTree.MatrixToString(OutputMatrix);
                if (Matrix4x4.Decompose(OutputMatrix, out Vector3 scale, out System.Numerics.Quaternion rotation, out Vector3 translation))
                {
                    ScaleVector = new Vector3DisplayModel(scale);
                    RotationQuaternion = new QuaternionDisplayModel(rotation);
                    TranslationVector = new Vector3DisplayModel(translation);
                    EulerAnglesVector = new Vector3DisplayModel(rotation.ToEulerAngles());
                }
                else
                {
                    ScaleVector = new Vector3DisplayModel(new Vector3(1, 1, 1));
                    TranslationVector = new Vector3DisplayModel(new Vector3(0, 0, 0));
                    RotationQuaternion = new QuaternionDisplayModel(new System.Numerics.Quaternion(0, 0, 0, 1));
                    EulerAnglesVector = new Vector3DisplayModel(new Vector3(0, 0, 0));
                }
            }
            else if (cbAsVector.IsChecked == true) 
            {
                try
                {
                    Matrix4x4 matrix = CoordSystemsTree.StringToMatrix(tbEnterMatrixAsVector.Text);
                    Matrix = new MatrixDisplayModel(matrix);
                    if (Matrix4x4.Decompose(OutputMatrix, out Vector3 scale, out System.Numerics.Quaternion rotation, out Vector3 translation))
                    {
                        ScaleVector = new Vector3DisplayModel(scale);
                        RotationQuaternion = new QuaternionDisplayModel(rotation);
                        TranslationVector = new Vector3DisplayModel(translation);
                        EulerAnglesVector = new Vector3DisplayModel(rotation.ToEulerAngles());
                    }
                    else
                    {
                        ScaleVector = new Vector3DisplayModel(new Vector3(1, 1, 1));
                        TranslationVector = new Vector3DisplayModel(new Vector3(0, 0, 0));
                        RotationQuaternion = new QuaternionDisplayModel(new System.Numerics.Quaternion(0, 0, 0, 1));
                        EulerAnglesVector = new Vector3DisplayModel(new Vector3(0, 0, 0));
                    }
                }
                catch
                {
                    ;
                }
            }
            else if (cbAsAnglesAndTranslation.IsChecked == true)
            {
                UpdateMatrixFromScaleTranslationQuaternion();
                tbEnterMatrixAsVector.Text = CoordSystemsTree.MatrixToString(OutputMatrix); 
            }

            IsMatrixValid = MatrixHelper.CheckIfMatrixIsDecomposable(OutputMatrix, out message);

            if (nameValid) //do not override name error message
            {
                ValidationMessage = message;
            }
        }

        private bool _isMatrixValid;

        public bool IsMatrixValid
        {
            get { return _isMatrixValid; }
            set 
            { 
                _isMatrixValid = value;
                OnPropertyChanged(nameof(OKbtnIsEnabled));
            }
        }


        private string _validationMessage = String.Empty;

        public string ValidationMessage
        {
            get { return _validationMessage; }
            set
            { 
                if (value != _validationMessage)
                {
                    _validationMessage = value; 
                    OnPropertyChanged(nameof(ValidationMessage));
                }
            }
        }

        private void btnNormalizeAngles_Click(object sender, RoutedEventArgs e)
        {
            System.Numerics.Quaternion quaternion;
            if (cbEulerAngles.IsChecked == true)
            {
                quaternion = EulerAnglesVector.ToQuaternionFromYawPitchRoll();
            }
            else if (cbQuaternion.IsChecked == true)
            {
                quaternion = RotationQuaternion.ToQuaternion();
            }
            else
            {
                return;
            }            
            quaternion = System.Numerics.Quaternion.Normalize(quaternion);
            EulerAnglesVector = new Vector3DisplayModel(quaternion.ToEulerAngles());
            RotationQuaternion = new QuaternionDisplayModel(quaternion);
        }

        private void cbEulerAngles_Checked(object sender, RoutedEventArgs e)
        {
            eulerAnglesInputField.Visibility = Visibility.Visible;
            cbQuaternion.IsChecked = false;
        }

        private void cbEulerAngles_Unchecked(object sender, RoutedEventArgs e)
        {
            eulerAnglesInputField.Visibility = Visibility.Collapsed;
        }

        private void cbQuaternion_Checked(object sender, RoutedEventArgs e)
        {
            quaternionInputField.Visibility = Visibility.Visible;
            cbEulerAngles.IsChecked = false;
        }

        private void cbQuaternion_Unchecked(object sender, RoutedEventArgs e)
        {
            quaternionInputField.Visibility = Visibility.Collapsed;
        }
    }
}
