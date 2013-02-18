using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using DataForest.DataModel;
using DataForest.Utilities;
using System.Windows.Forms;

namespace DataForest.ViewModel
{
    public abstract class TabModel : BaseViewModel
    {
        public TabModel(string name)
        {
            this.Name = name;
            this.Data = new Table();
            
        }

        public event EventHandler TabCloseRequest;
        



        RelayCommand closeTabCommand;
        RelayCommand saveCommand;
        RelayCommand saveAsCommand;
        private Table data;

        public string Name { get; set; }
        public Table Data
        {
            get
            {
                return data;
            }
            set
            {
                if (value != data)
                {
                    data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        
        public ICommand CloseTabCommand
        {
           get
            {
                if (closeTabCommand == null)
                {
                    closeTabCommand = new RelayCommand(param => CloseTab());
                }
                return closeTabCommand;
            }
        }

        private void CloseTab()
        {
            if (TabCloseRequest != null)
            {
                TabCloseRequest(this, null);
            }
           
        }
        
        public void Save()
        {
            string save = "";
            SaveFileDialog savefileDialog = new SaveFileDialog();
            savefileDialog.Filter = "Semikolonseparierte Datei (*.csv)|*.csv";
            if (savefileDialog.ShowDialog() == DialogResult.OK)
            {
                save = savefileDialog.FileName;
                this.Data.CreateCSVFile(save,";");
            }
            
        }
        
    }
}
