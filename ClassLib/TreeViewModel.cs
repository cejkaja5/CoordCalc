using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordCalc.ClassLib
{
    public class TreeViewModel
    {
        // Wrap the single root node in a collection for TreeView binding
        public ObservableCollection<CoordSystem> RootNodes { get; }

        public TreeViewModel(CoordSystem root)
        {
            RootNodes = new ObservableCollection<CoordSystem> { root };
        }
    }
}
