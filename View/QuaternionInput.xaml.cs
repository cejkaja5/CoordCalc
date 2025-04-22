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
    /// Interaction logic for QuaternionInput.xaml
    /// </summary>
    public partial class QuaternionInput : UserControl
    {
        public QuaternionInput()
        {
            InitializeComponent();
        }
        public QuaternionDisplayModel Quat
        {
            get { return (QuaternionDisplayModel)GetValue(QuatProperty); }
            set { SetValue(QuatProperty, value); }
        }

        public static readonly DependencyProperty QuatProperty =
            DependencyProperty.Register(nameof(Quat), typeof(QuaternionDisplayModel),
                typeof(QuaternionInput), new PropertyMetadata(null));


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
