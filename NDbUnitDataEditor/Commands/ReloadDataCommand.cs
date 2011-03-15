using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NDbUnitDataEditor.Commands
{
	public class ReloadDataCommandException : Exception
	{
		public ReloadDataCommandException(string message)
			: base(message)
		{
		}

	}

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
				string dataFileName = _dataEditor.DataFileName;

				string errorMessage = ValidateInputBeforeReload(dataFileName);
				if (errorMessage != string.Empty)
					throw new ReloadDataCommandException(errorMessage);

				_dataEditor.CloseAllTabs();
				_datasetProvider.ReadDataFromFile(dataFileName);
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
