using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NDbUnitDataEditor.UI;

namespace NDbUnitDataEditor.Commands
{
	public class ReinitializeMainViewRequested
	{

	}

	public class SaveDataCommand : ICommand
	{
		private IDataEditorView _dataEditor;
		private IMessageCreator _messageCreator;
		private IDataSetProvider _datasetProvider;
		private IFileDialogCreator _fileDialogCreator;
		/// <summary>
		/// Initializes a new instance of the SaveDataCommand class.
		/// </summary>
		public SaveDataCommand(IDataEditorView dataEditor, IMessageCreator messageCreator, IFileDialogCreator fileDialogCreator, IDataSetProvider datasetProvider)
		{
			_fileDialogCreator = fileDialogCreator;
			_datasetProvider = datasetProvider;
			_messageCreator = messageCreator;
			_dataEditor = dataEditor;
		}

		public void Execute()
		{
			try
			{
				string fileName = _dataEditor.DataFileName;
				if (fileName == null || fileName == "")
				{
					var dlgResult = _fileDialogCreator.ShowFileSave("xml files|*.xml");
					if (dlgResult.Accepted)
						fileName = dlgResult.SelectedFileName;
					else
						return;
				}
				//verify if path is correct
				string path = Path.GetDirectoryName(fileName);
				if (!Directory.Exists(path))
				{
					_messageCreator.ShowError("Cannot save specified file");
					return;
				}
				_datasetProvider.SaveDataToFile(fileName);
				_dataEditor.DataFileName = fileName;
				_dataEditor.RemoveEditedMarksFromAllTabs();
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unable to save file. Exception: {0}", ex.Message));
			}
		}
	}
}
