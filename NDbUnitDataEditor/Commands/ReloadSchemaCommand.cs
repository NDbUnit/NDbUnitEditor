using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NDbUnitDataEditor.Commands
{
	public class ReloadSchemaCommandException : Exception
	{
		public ReloadSchemaCommandException(string message)
			: base(message)
		{
		}

	}

	public class ReloadSchemaCommand : ICommand
	{

		private IDataEditorView _dataEditor;
		private IDataSetProvider _datasetProvider;
		private IMessageCreator _messageCreator;
		private IFileService _fileService;
		/// <summary>
		/// Initializes a new instance of the ReloadSchemaCommand class.
		/// </summary>
		public ReloadSchemaCommand(IDataEditorView dataEditor, IDataSetProvider datasetProvider, IMessageCreator messageCreator, IFileService fileService)
		{
			_fileService = fileService;
            _messageCreator = messageCreator;
            _datasetProvider = datasetProvider;
            _dataEditor = dataEditor;

		}

		public void Execute()
		{
				string schemaFileName = _dataEditor.SchemaFileName;

				if (!_fileService.FileExists(schemaFileName))
					throw new ReloadSchemaCommandException("Unable to find schema file.");
				_datasetProvider.ReadSchemaFromFile(schemaFileName);
				_dataEditor.BindTableTree(_datasetProvider.DataSetName, _datasetProvider.GetTableNames());
				_dataEditor.EnableSave();
		}
	}
}
