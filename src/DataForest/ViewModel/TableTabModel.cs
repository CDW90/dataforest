using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DataForest.ViewModel
{
    public class TableTabModel : TabModel
    {
        public TableTabModel()
        {
            this.Content = new DataForest.View.TableView();
            this.Name = "Neue Tabelle";
        }
        
    }
}
