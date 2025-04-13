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

        private CoordSystem _selectedCoordSystem;
        public CoordSystem SelectedCoordSystem
        {
            get { return _selectedCoordSystem; }
            set 
            {
                if (_selectedCoordSystem != value) 
                {
                    _selectedCoordSystem = value; 
                    OnPropertyChanged(nameof(SelectedCoordSystem));
                    OnPropertyChanged(nameof(SelectedCoordSystemNameText));
                    OnPropertyChanged(nameof(SelectedCoordSystemParentText));
                    OnPropertyChanged(nameof(SelectedCoordSystemTransformationHeader));
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
                    return "Root system doesnt have a parent system";
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
                    return "---No parent->child transforamtion for root system---";
                }
                else
                {
                    return $"{SelectedCoordSystem.Parent.Name}->{SelectedCoordSystem.Name} Transformation:";
                }
            }
        }


        private void lvCoordsSystems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedCoordSystem = (CoordSystem)lvCoordsSystems.SelectedItem;
        }
    }
}