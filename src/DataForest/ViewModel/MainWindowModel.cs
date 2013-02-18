using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DataForest.ViewModel;

namespace DataForest.ViewModel
{
    public class MainWindowModel
    {
        public MainWindowModel()
        {
            Tabs = new ObservableCollection<TabModel>();
            Tabs.Add(new TableTabModel());
            Tabs.Add(new TreeTabModel());

        }




        private RelayCommand closeCommand;
        private RelayCommand openCommand;
        private RelayCommand newCommand;
        private RelayCommand createOptimalTreeCommand;
        private RelayCommand createInteractiveTreeCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(param => New());
                }
                return newCommand;
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

        public ObservableCollection<TabModel> Tabs { get; set; }

        public EventHandler RequestClose { get; set; }

        private void Close()
        {
            throw new NotImplementedException(); 
        }

        private void Open()
        {
            throw new NotImplementedException();
        }

        private void New()
        {
            System.Windows.MessageBox.Show("Klappt");
            //throw new NotImplementedException();
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
