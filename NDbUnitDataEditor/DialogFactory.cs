using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor.UI
{
    public class DialogFactory : IDialogFactory
    {
        public IFileDialog CreateFileDialog(FileDialogType dialogType, string filter)
        {
            CustomFileDialog fileDialog = new CustomFileDialog(dialogType);
            fileDialog.Filter = filter;

            return fileDialog;
        }

        public IMessageCreator CreateMessageDialog()
        {
            MessageCreator dialog = new MessageCreator();
            return dialog;
        }

    }
}
