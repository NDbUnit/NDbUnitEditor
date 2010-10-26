using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor.UI
{
    public interface IDialogFactory
    {
        IMessageCreator CreateMessageDialog();
        IFileDialog CreateFileDialog(FileDialogType dialogType, string filter);
    }
}
