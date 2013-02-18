using DataForest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DataForest.ViewModel
{
    class OpenFileDialogVM : BaseViewModel
    {
        public static RelayCommand OpenCommand { get; set; }

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                OnPropertyChanged("SelectedPath");
            }
        }

        private string _defaultPath;

        public OpenFileDialogVM()
        {
            RegisterCommands();
        }

        public OpenFileDialogVM(string defaultPath)
        {
            _defaultPath = defaultPath;
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(param => ExecuteOpenFileDialog());
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
        }
    }
}
