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


        static RoutedCommand CloseCommand = new RoutedCommand();
        static RoutedCommand Open = new RoutedCommand();
        static RoutedCommand NewCommand = new RoutedCommand();
        static RoutedCommand CreateOptimalTree = new RoutedCommand();
        static RoutedCommand CreateInteractiveTree = new RoutedCommand();
        
        public ObservableCollection<TabModel> Tabs { get; set; }


        


        public EventHandler RequestClose { get; set; }


    }
}
