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

namespace CoordCalc.View
{
    /// <summary>
    /// Interaction logic for SomeText.xaml
    /// </summary>
    public partial class SomeText : UserControl
    {
        public SomeText()
        {
            InitializeComponent();
        }

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty);  }
            set { SetValue(MyTextProperty, value);  }
        }

        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register("MyText", typeof(string), typeof(SomeText), new PropertyMetadata(string.Empty));
    }
}
