using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using System.Numerics;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for ProjectSelectorWindow.xaml
    /// </summary>
    public partial class ProjectSelectorWindow : Window
    {
        public ProjectSelectorWindow()
        {
            _filePath = String.Empty;
            InitializeComponent();
            DataContext = this;
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }


        private void openProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CoordCalc projects | *.coordcalc";

            bool? success = fileDialog.ShowDialog();
            if (success == true) 
            { 
                string path = fileDialog.FileName;
                FilePath = path;
                txtBlockFilePath.Text = path;
            }
        }

        private void createProjectBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FilePath != String.Empty && File.Exists(FilePath)) 
            {
                CoordSystemsTree coordSystemsTree = new CoordSystemsTree(FilePath);
                MainWindow mainWindow = new MainWindow(coordSystemsTree, this);
                mainWindow.Show();
            }
        }
    }
}
