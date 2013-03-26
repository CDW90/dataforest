using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataForest.ViewModel.Dialogs
{
    public class MessageDialogModel : BaseDialogModel
    {
        public MessageDialogModel() {}

        public MessageDialogModel(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }


        public string Message { get; set; }
    }
}
