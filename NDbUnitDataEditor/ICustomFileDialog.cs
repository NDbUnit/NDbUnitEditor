using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    public enum FileDialogType
    {
        OpenFileDialog,
        SaveFileDilaog
    }

    public enum FileDialogResult
    {
        OK,
        Cancel
    }

    public interface IFileDialog
    {
        string FileName { get; }
        FileDialogResult Show();
    }
}
