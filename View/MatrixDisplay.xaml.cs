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
using System.Numerics;
using CoordCalc.ClassLib;

namespace CoordCalc.View
{
    /// <summary>
    /// Interaction logic for MatrixDisplay.xaml
    /// </summary>
    public partial class MatrixDisplay : UserControl
    {
        public MatrixDisplay()
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
                typeof(MatrixDisplay), new PropertyMetadata(null));


    }
}
