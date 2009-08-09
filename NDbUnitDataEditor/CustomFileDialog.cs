using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor.UI
{
    public class CustomFileDialog : IFileDialog
    {
        FileDialog _dialog = null;

        /// <summary>
        /// Initializes a new instance of the FileDialog class.
        /// </summary>
        public CustomFileDialog(FileDialogType dialogType)
        {
            if (dialogType == FileDialogType.OpenFileDialog)
                _dialog = new OpenFileDialog();
            else
            {
                _dialog = new SaveFileDialog();
                _dialog.AddExtension = true;
            }
        }

        public string FileName
        {
            get
            {
                return _dialog.FileName;
            }
        }

        public string Filter
        {
            get { return _dialog.Filter; }
            set
            {
                _dialog.Filter = value;
            }
        }

        public FileDialogResult Show()
        {
            DialogResult dlgResult = _dialog.ShowDialog();
            if (dlgResult == DialogResult.OK)
                return FileDialogResult.OK;
            else
                return FileDialogResult.Cancel;
        }

    }
}
