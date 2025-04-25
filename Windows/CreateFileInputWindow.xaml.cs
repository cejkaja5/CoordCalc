using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for CreateFileInputWindow.xaml
    /// </summary>
    public partial class CreateFileInputWindow : Window, INotifyPropertyChanged
    {
        public CreateFileInputWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _success = false;
        public bool Success
        { 
            get { return _success; }
            set 
            { 
                if (_success != value)
                {
                    _success = value;
                    OnPropertyChanged(nameof(Success));
                }
            }
        }

        public string FilePath
        {
            get 
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderPath))
                {
                    Success = false;
                    return string.Empty;
                }
                Success = true;
                return System.IO.Path.Combine(FolderPath, FileName + ".coordcalc"); 
            }
        }

        private string _filaName = string.Empty;

        public string FileName
        {
            get { return _filaName; }
            set 
            {
                if (_filaName != value)
                {
                    _filaName = value;
                    OnPropertyChanged(nameof(FileName));
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }

        private string _folderPath = string.Empty;
        public string FolderPath
        {
            get { return _folderPath; }
            set 
            { 
                if (_folderPath != value)
                {
                    _folderPath = value; 
                    OnPropertyChanged(nameof(FolderPath));
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }

        private void btnPickFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Select a folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName; // Selected folder path
            }

            this.Activate();
            this.Focus();
        }

        private void tbFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FileName = ((TextBox)sender).Text;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Success = false;
            Close();
        }
    }
}
