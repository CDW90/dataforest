using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DataForest.ViewModel
{
    abstract class Tab
    {
        public UserControl Content { get; set; }
        public string Name { get; set; }
        
    }
}
