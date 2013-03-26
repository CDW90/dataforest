using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DataForest.ViewModel;
using DataForest.Utilities;
using System.Windows.Forms;
using DataForest.DataModel;

namespace DataForest.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            Tabs = new ObservableCollection<TabModel>();
            NewTable();

        }

        private TabModel selectedTab;

        private RelayCommand closeCommand;
        private RelayCommand openCommand;
        private RelayCommand saveCommand;
        private RelayCommand newtableCommand;
        private RelayCommand newtreeCommand;
        private RelayCommand createOptimalTreeCommand;
        private RelayCommand createInteractiveTreeCommand;
        
        public TabModel SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                if (value != selectedTab)
                {
                    selectedTab = value;
                    OnPropertyChanged("SelectedTab");
                }
            }
        }
        public ObservableCollection<TabModel> Tabs
        {
            get;
            set;
        }

        public ICommand NewTableCommand
        {
            get
            {
                if (newtableCommand == null)
                {
                    newtableCommand = new RelayCommand(param => NewTable());
                }
                return newtableCommand;
            }
        }
        public ICommand NewTreeCommand
        {
            get
            {
                if (newtreeCommand == null)
                {
                    newtreeCommand = new RelayCommand(param => NewTree());
                }
                return newtreeCommand;
            }
        }
        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new RelayCommand(param => this.Open());
                }
                return openCommand;
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(param => this.Close());
                }
                return closeCommand;
            }
        }
        public ICommand CreateOptimalTreeCommand
        {
            get
            {
                if (createOptimalTreeCommand == null)
                {
                    createOptimalTreeCommand = new RelayCommand(param => CreateOptimalTree());
                }
                return createOptimalTreeCommand;
            }
        }
        public ICommand CreateInteractiveTreeCommand
        {
            get {
                if (createInteractiveTreeCommand == null)
                {
                    createInteractiveTreeCommand = new RelayCommand(param => this.CreateInteractiveTree());
                }
                return createInteractiveTreeCommand;
            }
            
        }
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(param => Save());
                }
                return saveCommand;
            }
        }



        public event EventHandler RequestClose;

        private void Close()
        {
            if (RequestClose != null) 
            {
                RequestClose(this, null);
            }
           
        }

        private void Save()
        {
            SelectedTab.Save();
        }

        private void Open()
        {
            List<string> error = new List<string>();
            this.ShowFileDialog("Test");
            /*
            string path = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Zeichensepariert (*.txt,*.csv)|*.txt; *.csv";
            if (fileDialog.ShowDialog() == DialogResult.OK)
                path = fileDialog.FileName;
            TabModel tab = NewTable();
            tab.Data = Table.ReadFromCSV(path, ";",out error);*/
        }

        private TabModel NewTable()
        {
            var tab = new TableTabModel("Neue Tabelle") { };
            Tabs.Add(tab);
            tab.TabCloseRequest += CloseTab;
            SelectedTab = tab;
            return tab;
        }

        private void NewTree()
        {
            var tab = new TreeTabModel ("Neuer Baum"){ };
            Tabs.Add(tab);
            tab.TabCloseRequest += CloseTab;
            SelectedTab = tab;
        }

        private void CloseTab (object sender, EventArgs e)
        {
            Tabs.Remove(sender as TabModel);
        }

        private void CreateOptimalTree()
        {
            throw new NotImplementedException();
        }

        private void CreateInteractiveTree()
        {
            throw new NotImplementedException();
        }


    }
}
