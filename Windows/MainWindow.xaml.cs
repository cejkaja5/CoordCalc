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

namespace CoordCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoordSystemsTree _coordSystemTree;

        public Collection<CoordSystem> CoordSystems
        {
            get { return _coordSystemTree.Nodes; }
        }
        public MainWindow(CoordSystemsTree system, ProjectSelectorWindow opener)
        {
            _coordSystemTree = system;
            DataContext = this;
            InitializeComponent();
            opener.Close();
        }
    }
}