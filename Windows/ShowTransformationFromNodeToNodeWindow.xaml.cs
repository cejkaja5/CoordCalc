using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoordCalc.ClassLib;
using System.Numerics;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for ShowTransformationFromNodeToNodeWindow.xaml
    /// </summary>
    public partial class ShowTransformationFromNodeToNodeWindow : Window, INotifyPropertyChanged
    {
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ShowTransformationFromNodeToNodeWindow(CoordSystem origin, CoordSystem destination, CoordSystemsTree systemsTree)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Origin = origin;
            Destination = destination;
            CoordSystemsTree = systemsTree;
            DataContext = this;
            InitializeComponent();
            FromToText = $"Transformation from {origin.Name} to {destination.Name}";
            this.Title = FromToText;
            CalculateAndDisplayTransformation();
            btnCalculate_Click(this, new RoutedEventArgs());
        }

        private void CalculateAndDisplayTransformation()
        {
            try
            {
                TransformationsList = CoordSystemsTree.GetTransformations(Origin, Destination);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            FullTransformationText = CoordSystemsTree.TransformationToStringOnlyNames(TransformationsList);
            Matrix = CoordSystemsTree.TransformationToMatrix(TransformationsList);
            if (Matrix4x4.Decompose(Matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation))
            {
                TranslationVectorText = "Translation vector: " + translation.ToCustomString(
                    GlobalSettings.FloatPrecisionTranslationVector);
                ScaleVectorText = "Scale vector: " + scale.ToCustomString(
                    GlobalSettings.FloatPrecisionScaleVector);
                RotationText = "Quaternion: " + rotation.ToCustomString(
                    GlobalSettings.FloatPrecisionQuaternion);
                EulerAnglesText = "Yaw pitch roll (in deg): " + rotation.ToEulerAngles().ToCustomString(
                    GlobalSettings.FloatPrecisionEulerAnglesDeg);
                EulerAnglesRadText = "Yaw pitch roll (in rad): " + rotation.ToEulerAngles().DegreesToRadians().ToCustomString(
                    GlobalSettings.FloatPrecisionEulerAnglesRad);

            }
            else
            {
                MatrixHelper.CheckIfMatrixIsDecomposable(Matrix, out string message);
                MessageBox.Show($"Matrix decomposition failed. Matrix validity status: {message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TranslationVectorText = "Translation vector: Error";
                ScaleVectorText = "Scale vector: Error";
                RotationText = "Quaternion: Error";
                EulerAnglesText = "Yaw pitch roll (in deg): Error";
                EulerAnglesRadText = "Yaw pitch roll (in rad): Error";
            }
        }

        List<CoordSystemsTree.Transformation> _transformationsList = new List<CoordSystemsTree.Transformation>();

        public List<CoordSystemsTree.Transformation> TransformationsList
        {
            get { return _transformationsList; }
            set
            {
                if (_transformationsList != value)
                {
                    _transformationsList = value;
                    OnPropertyChanged(nameof(TransformationsList));
                }
            }
        }

        private string _fromToText = string.Empty;

        public string FromToText
        {
            get { return _fromToText; }
            set 
            {
                if (_fromToText != value)
                {
                    _fromToText = value;  
                    OnPropertyChanged(nameof(FromToText));
                }
            }
        }

        private string _fullTransformationText = string.Empty;

        public string FullTransformationText
        {
            get { return _fullTransformationText; }
            set
            {
                if (_fullTransformationText != value)
                {
                    _fullTransformationText = value;
                    OnPropertyChanged(nameof(FullTransformationText));
                }
            }
        }

        private string _translationVectorText = string.Empty;

        public string TranslationVectorText
        {
            get { return _translationVectorText; }
            set
            {
                if (_translationVectorText != value)
                {
                    _translationVectorText = value;
                    OnPropertyChanged(nameof(TranslationVectorText));
                }
            }
        }

        private string _scaleVectorText = string.Empty;

        public string ScaleVectorText
        {
            get { return _scaleVectorText; }
            set
            {
                if (_scaleVectorText != value)
                {
                    _scaleVectorText = value;
                    OnPropertyChanged(nameof(ScaleVectorText));
                }
            }
        }

        private string _rotationText = string.Empty;

        public string RotationText
        {
            get { return _rotationText; }
            set
            {
                if (_rotationText != value)
                {
                    _rotationText = value;
                    OnPropertyChanged(nameof(RotationText));
                }
            }
        }

        private string _eulerAnglesText = string.Empty;

        public string EulerAnglesText
        {
            get { return _eulerAnglesText; }
            set
            {
                if (_eulerAnglesText != value)
                {
                    _eulerAnglesText = value;
                    OnPropertyChanged(nameof(EulerAnglesText)); 
                }
            }
        }

        private string _eulerAnglesRadText;

        public string EulerAnglesRadText
        {
            get { return _eulerAnglesRadText; }
            set
            {
                if (_eulerAnglesRadText != value)
                {
                    _eulerAnglesRadText = value;
                    OnPropertyChanged(nameof(EulerAnglesRadText));
                }
            }  
        }


        private Matrix4x4 _matrix = Matrix4x4.Identity;

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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly List<CoordSystemsTree.Transformation> _transformations;

        public List<CoordSystemsTree.Transformation> Transformations
        {
            get { return _transformations; }
            init { _transformations = value; }
        }

        private readonly CoordSystem _origin;

        public CoordSystem Origin
        {
            get { return _origin; }
            init { _origin = value; } 
        }

        private readonly CoordSystem _destination;

        public CoordSystem Destination
        {
            get { return _destination; }
            init { _destination = value; }
        }

        private readonly CoordSystemsTree _coordSystemsTree;

        public CoordSystemsTree CoordSystemsTree
        {
            get { return _coordSystemsTree; }
            init { _coordSystemsTree = value; }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Vector3DisplayModel _vectorToBeTransformed = new Vector3DisplayModel(new Vector3(0,0,0));

        public Vector3DisplayModel VectorToBeTransformed
        {
            get { return _vectorToBeTransformed; }
            set
            {
                if (_vectorToBeTransformed != value)
                {
                    _vectorToBeTransformed = value;
                    OnPropertyChanged(nameof(VectorToBeTransformed));
                } 
            }
        }

        private string _transformedVectorText = string.Empty;
        public string TransformedVectorText
        {
            get
            {
                return _transformedVectorText;
            }
            set
            {
                if (_transformedVectorText != value)
                {
                    _transformedVectorText = value;
                    OnPropertyChanged(nameof(TransformedVectorText));
                }
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Vector4 v = VectorToBeTransformed.ToVector4();
            Vector4 result = v.TransformAsRowVector(Matrix);
            TransformedVectorText = (new Vector3(result.X, result.Y, result.Z)).ToCustomString(
                GlobalSettings.FloatPrecisionDefault);
        }
    } 
}
