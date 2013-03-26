using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DataForest.Utilities;

namespace DataForest.ViewModel.Dialogs
{
    public class BaseDialogModel : BaseViewModel
    {

        protected string acceptButtonContent = "OK";
        protected string cancelButtonContent = "Abbrechen";
        protected string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string AcceptButtonContent
        {
            get
            {
                return acceptButtonContent;
            }
            set
            {
                if (value != acceptButtonContent)
                {
                    acceptButtonContent = value;
                    OnPropertyChanged("AcceptButtonContent");
                }
            }
        }
        public string CancelButtonContent
        {
            get
            {
                return cancelButtonContent;
            }
            set
            {
                if (value != cancelButtonContent)
                {
                    cancelButtonContent = value;
                    OnPropertyChanged("CancelButtonContent");
                }
            }
        }


        protected ICommand acceptCommand;
        protected ICommand cancelCommand;

        public Visibility myVisibility;

        public Visibility MyVisibility
        {
            get
            {
                return myVisibility;
            }
            set
            {
                if (value != myVisibility)
                {
                    myVisibility = value;
                    OnPropertyChanged("MyVisibility");
                }
            }
        }

        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new RelayCommand(param => DialogReturned());
                }
                return acceptCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => DialogReturned());
                }
                return cancelCommand;
            }
        }

        public void ShowMe() {
            this.MyVisibility = Visibility.Visible;
        }


        protected void DialogReturned()
        {
            this.MyVisibility = Visibility.Hidden;
        }
    }
}
