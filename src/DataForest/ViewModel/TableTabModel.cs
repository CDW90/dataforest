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
            this.Name = "Neue Tabelle";
            Data.Columns.Add("Col1", typeof(string));
            Data.Columns.Add("Col2", typeof(string));
            Data.Columns.Add("Col3", typeof(string));
            Data.Rows.Add(new object[] { "hallo", "welt", "1" });
            Data.Rows.Add(new object[] { "hallo", "computer", "2" });
        }
        
    }
}
