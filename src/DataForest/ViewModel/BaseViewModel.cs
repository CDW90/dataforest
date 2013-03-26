using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using DataForest.ViewModel.Dialogs;

namespace DataForest.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BaseDialogModel dialog;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public BaseDialogModel Dialog
        {
            get
            {
                return dialog;
            }
            set
            {
                if (value != dialog)
                {
                    if (dialog != null)
                    {
                        dialog.PropertyChanged -= new PropertyChangedEventHandler(Dialog_PropertyChanged);
                    }
                    dialog = value;
                    dialog.PropertyChanged += new PropertyChangedEventHandler(Dialog_PropertyChanged);
                    OnPropertyChanged("DialogVisibility");
                    OnPropertyChanged("Dialog");
                }
            }
        }

        

        public void ShowDialog()
        {
            Dialog.MyVisibility = Visibility.Visible;
        }

        public void ShowDialog(Dialogs.BaseDialogModel dlg)
        {
            Dialog = dlg;
            Dialog.MyVisibility = Visibility.Visible;
        }

        public void ShowMessageDialog(string title, string message)
        {
            Dialog = new Dialogs.MessageDialogModel(title, message);
            Dialog.MyVisibility = Visibility.Visible;
            
        }

        public void ShowFileDialog(string title)
        {
            Dialog = new Dialogs.FileDialogModel();
            Dialog.MyVisibility = Visibility.Visible;
        }

        public Visibility DialogVisibility
        {
            get
            {
                if (Dialog != null)
                {
                    return Dialog.MyVisibility;
                }
                return Visibility.Hidden;
            }
            set
            {
                if (Dialog != null)
                {
                    Dialog.MyVisibility = value;
                    OnPropertyChanged("DialogVisibility");
                }

            }
        }

        void Dialog_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MyVisibility")
            {
                OnPropertyChanged("DialogVisibility");
            }
        }



    }
}
