using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{

	public class FileDialogResult
	{
		public bool Accepted { get; set; }
		public string SelectedFileName { get; set; }
	}

    public interface IFileDialogCreator
    {
        FileDialogResult ShowFileSave(string filter);
		FileDialogResult ShowFileOpen(string filter);
    }
}
