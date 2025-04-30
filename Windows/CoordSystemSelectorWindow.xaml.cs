using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for CoordSystemSelector.xaml
    /// </summary>
    public partial class CoordSystemSelectorWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public CoordSystemSelectorWindow(CoordSystemsTree systemsTree)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            _systemsTree = systemsTree;
            DataContext = this;
            InitializeComponent();
            Root = new ObservableCollection<CoordSystem> {
                systemsTree.GetRootSystem()
            };
            tvCoordSystemSelectItem(Root[0]);

            //SelectedCoordSystem = systemsTree.GetRootSystem();
            //lvCoordsSystems.SelectedItem = SelectedCoordSystem;
        }
        private readonly CoordSystemsTree _systemsTree;

        private ObservableCollection<CoordSystem> _root;
        public ObservableCollection<CoordSystem> Root
        {
            get { return _root; }
            init
            {
                _root = value;
                OnPropertyChanged(nameof(Root));
            }
        }
        public CoordSystemsTree SystemsTree
        {
            init { 
                _systemsTree = value;
                OnPropertyChanged(nameof(SystemsTree));
                OnPropertyChanged(nameof(CoordSystems));
            }
            get { return _systemsTree; }
        }

        public Collection<CoordSystem> CoordSystems
        {
            get { return SystemsTree.Nodes; }
        }

        private bool _success = false;
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
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
                }
            }
        }

        //private void lvCoordsSystems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    SelectedCoordSystem = (CoordSystem)lvCoordsSystems.SelectedItem;
        //}

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Success = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Success = false;
            Close();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = e.NewValue as CoordSystem;
            if (selectedItem != null)
            {
                SelectedCoordSystem = selectedItem;
            }
        }

        private TreeViewItem? GetTreeViewItem(ItemsControl container, object item)
        {
            if (container == null) return null;

            for (int i = 0; i < container.Items.Count; i++)
            {
                var currentItem = container.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                if (currentItem == null) continue;

                if (currentItem.DataContext == item)
                    return currentItem;

                // Expand node and recurse
                if (currentItem.Items.Count > 0)
                {
                    currentItem.IsExpanded = true; // make sure child containers are generated
                    var child = GetTreeViewItem(currentItem, item);
                    if (child != null)
                        return child;
                }
            }
            return null;
        }

        private void tvCoordSystemSelectItem(object item)
        {
            if (item == null) return;
            var treeViewItem = GetTreeViewItem(tvCoordSystems, item);
            if (treeViewItem != null)
            {
                treeViewItem.BringIntoView();
                treeViewItem.Focus();
                treeViewItem.IsSelected = true;
            }
        }

    }
}
