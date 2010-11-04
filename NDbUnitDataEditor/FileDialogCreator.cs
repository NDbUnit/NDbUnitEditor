using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor.UI
{

    public class FileDialogCreator : IFileDialogCreator
    {
        public FileDialogResult ShowFileOpen(string filter)
        {
			var dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = filter;
            DialogResult dlgResult = dialog.ShowDialog();
			return GetResult(dlgResult, dialog);
        }

		public FileDialogResult ShowFileSave(string filter)
		{
			var dialog = new SaveFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = filter;
			DialogResult dlgResult = dialog.ShowDialog();
			return GetResult(dlgResult, dialog);
		}

		private FileDialogResult GetResult(DialogResult dialogResult, FileDialog dialog)
		{
			var result = new FileDialogResult();
			if (dialogResult == DialogResult.OK)
			{
				result.Accepted = true;
				result.SelectedFileName = dialog.FileName;
				return result;
			}
			else
				return result;

		}



    }
}
