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
using System.Collections.Specialized;
using System.ComponentModel;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for ProjectSelectorWindow.xaml
    /// </summary>
    public partial class ProjectSelectorWindow : Window, INotifyPropertyChanged
    {
        public ProjectSelectorWindow()
        {
            _filePath = String.Empty;
            InitializeComponent();
            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set 
            { 
                _filePath = value;
                if (_filePath != String.Empty) 
                {
                    OkBtnEnabled = true;
                    OnPropertyChanged(nameof(FilePath));
                }
                else
                {
                    OkBtnEnabled = false;
                }
            }
        }

        private bool _okBtnEnabled = false;

        public bool OkBtnEnabled
        {
            get { return _okBtnEnabled; }
            set
            { 
                if (value != _okBtnEnabled)
                {
                    _okBtnEnabled = value; 
                    OnPropertyChanged(nameof(OkBtnEnabled));
                }
            }
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
            }
        }

        private void createProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateFileInputWindow window = new CreateFileInputWindow();
            window.ShowDialog();

            if (window.Success)
            {
                FilePath = window.FilePath;
                OkBtnEnabled = true;
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(FilePath))
            {
                try 
                {
                    using (File.Create(FilePath))
                    {
                        // Blank file created
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating file: {ex.Message}");
                    return;
                }
            }
            
            CoordSystemsTree coordSystemsTree = new CoordSystemsTree(FilePath);
            MainWindow mainWindow = new MainWindow(coordSystemsTree, this);
            mainWindow.Show();
            
        }
    }
}
