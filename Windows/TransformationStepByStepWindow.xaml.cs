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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for TransformationStepByStepWindow.xaml
    /// </summary>
    public partial class TransformationStepByStepWindow : Window
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public TransformationStepByStepWindow(List<CoordSystemsTree.Transformation> transformations)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Transformations = transformations;
            InitializeComponent();
            DataContext = this;
            Title = $"{Transformations[0].From} to {Transformations[^1].To} step by step";
        }

        private List<CoordSystemsTree.Transformation> _transformations;

        public List<CoordSystemsTree.Transformation> Transformations
        {
            get { return _transformations; }
            set { _transformations = value; }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}