using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace DataForest.ViewModel
{
    public abstract class TabModel
    {
        RoutedCommand CloseTabCommand = new RoutedCommand();
        RoutedCommand SaveCommand = new RoutedCommand();
        RoutedCommand SaveAsCommand = new RoutedCommand();

        public UserControl Content { get; set; }
        public string Name { get; set; }
        public DataTable data { get; set; }
        
    }
}
