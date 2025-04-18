using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.View
{
    /// <summary>
    /// Interaction logic for MatrixInput.xaml
    /// </summary>
    public partial class MatrixInput : UserControl
    {
        public MatrixInput()
        {
            InitializeComponent();
        }
        public MatrixDisplayModel Matrix
        {
            get { return (MatrixDisplayModel)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static readonly DependencyProperty MatrixProperty =
            DependencyProperty.Register(nameof(Matrix), typeof(MatrixDisplayModel),
                typeof(MatrixInput), new PropertyMetadata(null));


        private void TextBox_SelectAllTextOnFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            { 
                textBox.SelectAll();
            }
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !textBox.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                textBox.Focus();
                textBox.SelectAll();
            }
        }
    }
}
