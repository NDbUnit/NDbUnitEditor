using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NDbUnitDataEditor.Commands
{

	public class ReloadDataCommand : ICommand
	{
		private IMessageCreator _messageCreator;
		private IDataEditorView _dataEditor;
		private IDataSetProvider _datasetProvider;
        /// <summary>
		/// Initializes a new instance of the ReloadDataCommand class.
		/// </summary>
		public ReloadDataCommand(IMessageCreator messageCreator, IDataEditorView dataEditor, IDataSetProvider datasetProvider)
		{
			_datasetProvider = datasetProvider;
            _dataEditor = dataEditor;
            _messageCreator = messageCreator;
			
		}
		public void Execute()
		{
			try
			{
				string dataFileName = _dataEditor.DataFileName;

				string errorMessage = ValidateInputBeforeReload(dataFileName);
				if (errorMessage != string.Empty)
				{
					_messageCreator.ShowError(errorMessage, "Error loading DataSet");
					return;
				}

				_dataEditor.CloseAllTabs();
				_datasetProvider.ReadDataFromFile(dataFileName);
			}
			catch (Exception ex)
			{
				_messageCreator.ShowError(String.Format("Unspecified error occured. Exception: {0}", ex.Message));
				//TODO: add some sort of error logging here
			}
		}

		private string ValidateInputBeforeReload(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
			{
				return "Cannot load data. Please select data file name first";
			}

			if (!File.Exists(fileName))
			{
				return "Specified file does not exists.";
			}

			return string.Empty;
		}
	}
}
