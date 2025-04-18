using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoordCalc.ClassLib;
using CoordCalc.Windows;
using System.Numerics;
using System.ComponentModel;

namespace CoordCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private CoordSystemsTree _coordSystemTree;

        public Collection<CoordSystem> CoordSystems
        {
            get { return _coordSystemTree.Nodes; }
        }
        public MainWindow(CoordSystemsTree tree, ProjectSelectorWindow opener)
        {
            _coordSystemTree = tree;
            _selectedCoordSystem = tree.GetRootSystem();
            DataContext = this;
            InitializeComponent();
            lvCoordsSystems.SelectedItem = _selectedCoordSystem;
            opener.Close();
        }

        public Matrix4x4 IdeMatrix { get; set; } = Matrix4x4.CreateRotationY(0.25f);


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChangedAllMatrixProperties()
        {
            OnPropertyChanged(nameof(SelectedCoordSystem));
            OnPropertyChanged(nameof(SelectedCoordSystemNameText));
            OnPropertyChanged(nameof(SelectedCoordSystemParentText));
            OnPropertyChanged(nameof(SelectedCoordSystemTransformationHeader));
            OnPropertyChanged(nameof(SelectedCoordSystemTranslationVectorText));
            OnPropertyChanged(nameof(SelectedCoordSystemRotationText));
            OnPropertyChanged(nameof(SelectedCoordSystemScaleVectorText));
            OnPropertyChanged(nameof(SelectedCoordSystemEulerAngles));
            OnPropertyChanged(nameof(SelectedCoordSystemEulerAnglesText));
            OnPropertyChanged(nameof(BtnGoToParentContent));
        }

        private CoordSystem _selectedCoordSystem;
        public CoordSystem SelectedCoordSystem
        {
            get { return _selectedCoordSystem; }
            set 
            {
                if (_selectedCoordSystem != value) 
                {
                    _selectedCoordSystem = value;
                    btnGoToParent.Visibility = Visibility.Visible;

                    if (_selectedCoordSystem.IsRoot())
                    {
                        SelectedCoordSystemRotation = null;
                        SelectedCoordSystemScaleVector = null;
                        SelectedCoordSystemTranslationVector = null;
                        SelectedCoordSystemEulerAngles = null;
                        btnGoToParent.Visibility = Visibility.Collapsed;
                    }

                    else if (Matrix4x4.Decompose(SelectedCoordSystem.Matrix, out Vector3 scaleVector,
                        out Quaternion rotation, out Vector3 translationVector))
                    {
                        SelectedCoordSystemRotation = rotation;
                        SelectedCoordSystemScaleVector = scaleVector;
                        SelectedCoordSystemTranslationVector = translationVector;
                        SelectedCoordSystemEulerAngles = rotation.ToEulerAngles();
                    }

                    else

                    {
                        MatrixHelper.CheckIfMatrixIsDecomposable(SelectedCoordSystem.Matrix, out string message);
                        MessageBox.Show($"Decomposing transformation matrix failed. Reason: {message}", "Warning",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        SelectedCoordSystemRotation = null;
                        SelectedCoordSystemScaleVector = null;
                        SelectedCoordSystemTranslationVector = null;
                        SelectedCoordSystemEulerAngles = null;
                    }

                    OnPropertyChangedAllMatrixProperties();
                }
            }
        }

        public string SelectedCoordSystemNameText
        {
            get { return $"Name: {SelectedCoordSystem.Name}"; }
        }

        public string SelectedCoordSystemParentText
        {
            get
            {
                if (SelectedCoordSystem.IsRoot())
                {
                    return "Parent: None";
                }
                else
                { 
                    return $"Parent: {SelectedCoordSystem.Parent.Name}";
                }
            }
        }

        public string SelectedCoordSystemTransformationHeader
        {
            get
            {
                if (SelectedCoordSystem.IsRoot())
                {
                    return "No child->parent transforamtion for root system";
                }
                else
                {
                    return $"{SelectedCoordSystem.Name}->{SelectedCoordSystem.Parent.Name} Transformation:";
                }
            }
        }

        private Vector3? _selectedCoordSystemTranslationVector;

        public Vector3? SelectedCoordSystemTranslationVector
        {
            get { return _selectedCoordSystemTranslationVector; }
            set { _selectedCoordSystemTranslationVector = value; }
        }

        public string SelectedCoordSystemTranslationVectorText
        {
            get 
            {
                if (SelectedCoordSystemTranslationVector != null)
                {
                    return $"Translation vector: {((Vector3)SelectedCoordSystemTranslationVector).ToCustomString()}";
                }
                else
                {
                    return "Translation vector: None";
                }
            }
        }

        private Vector3? _selectedCoordSystemScaleVector;

        public Vector3? SelectedCoordSystemScaleVector
        {
            get { return _selectedCoordSystemScaleVector; }
            set { _selectedCoordSystemScaleVector = value; }
        }

        public string SelectedCoordSystemScaleVectorText
        {
            get 
            {
                if (SelectedCoordSystemScaleVector != null)
                {
                    return $"Scale vector: {((Vector3)SelectedCoordSystemScaleVector).ToCustomString()}";
                }
                else
                {
                    return "Scale vector: None";
                }
            } 
        }              
        
        private Quaternion? _selectedCoordSystemRotation;

        public Quaternion? SelectedCoordSystemRotation
        {
            get { return _selectedCoordSystemRotation; }
            set { _selectedCoordSystemRotation = value; }
        }

        private Vector3? _electedCoordSystemEulerAngles;

        public Vector3? SelectedCoordSystemEulerAngles
        {
            get { return _electedCoordSystemEulerAngles; }
            set { _electedCoordSystemEulerAngles = value; }
        }

        public string SelectedCoordSystemEulerAnglesText
        {
            get
            {
                if (SelectedCoordSystemEulerAngles != null)
                {
                    return $"Euler angles in degrees: {((Vector3)SelectedCoordSystemEulerAngles).ToEulerAnglesString()}";
                }
                else
                {
                    return "Euler angles: None";
                }
            }
        }

        public string SelectedCoordSystemRotationText
        {
            get 
            { 
                if (SelectedCoordSystemRotation != null)
                {
                    return $"Quaternion: {((Quaternion)SelectedCoordSystemRotation).ToCustomString()}";
                }
                else
                {
                    return "Quaternion: None";
                }
            }
        }

        private void lvCoordsSystems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCoordSystem = (CoordSystem)lvCoordsSystems.SelectedItem;
        }

        private void btnGoToParent_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCoordSystem.IsRoot()) return;

            lvCoordsSystems.SelectedItem = _selectedCoordSystem.Parent;
        }

        public string BtnGoToParentContent
        {
            get 
            {
                if (!_selectedCoordSystem.IsRoot())
                {
                    return $"Go to parent ({SelectedCoordSystem.Parent.Name})";
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {
            InputCoordSystemWindow window = new InputCoordSystemWindow(SelectedCoordSystem);
            window.ShowDialog();
            
            if (window.Success) 
            {
                CoordSystem child = new CoordSystem(window.OutputMatrix, window.SystemName, SelectedCoordSystem);
                _coordSystemTree.AddNode(child);
                OnPropertyChanged(nameof(CoordSystems));
                lvCoordsSystems.SelectedItem = child;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}