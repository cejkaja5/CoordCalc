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
using System.Windows.Shapes;
using CoordCalc.ClassLib;

namespace CoordCalc.Windows
{
    /// <summary>
    /// Interaction logic for ShowTransformationFromNodeToNodeWindow.xaml
    /// </summary>
    public partial class ShowTransformationFromNodeToNodeWindow : Window
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ShowTransformationFromNodeToNodeWindow(CoordSystem origin, CoordSystem destination, CoordSystemsTree systemsTree)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            Origin = origin;
            Destination = destination;
            CoordSystemsTree = systemsTree;
            InitializeComponent(); 
            this.Title = $"Transformation from {origin.Name} to {destination.Name}";
        }

        private readonly CoordSystem _origin;

        public CoordSystem Origin
        {
            get { return _origin; }
            init { _origin = value; } 
        }

        private readonly CoordSystem _destination;

        public CoordSystem Destination
        {
            get { return _destination; }
            init { _destination = value; }
        }

        private readonly CoordSystemsTree _coordSystemsTree;

        public CoordSystemsTree CoordSystemsTree
        {
            get { return _coordSystemsTree; }
            init { _coordSystemsTree = value; }
        }
    } 
}
