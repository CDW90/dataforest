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
        public TabModel()
        {
            this.Name = "Neuer Tab";
            this.Data = new DataTable();
        }

        RelayCommand CloseTabCommand;
        RelayCommand SaveCommand;
        RelayCommand SaveAsCommand;

        public string Name { get; set; }
        public DataTable Data { get; set; }
        
    }
}
