using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace DataForest.ViewModel
{
    public class TreeTabModel : TabModel
    {
        public TreeTabModel(string name) : base (name)
        {
            
           
        }
        private ObservableCollection<Shape> tree;
    }
}
