using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NDbUnitDataEditor.Commands
{
	public class GetDataFromDatabaseCommand : ICommand
	{
		private DataSetFromDatabasePresenter _presenter;
		private IDataEditorView _dataEditor;
		private IDataSetProvider _datasetProvider;
		private IEventAggregator _events;
		/// <summary>
		/// Initializes a new instance of the GetDataFromDatabaseCommand class.
		/// </summary>
		public GetDataFromDatabaseCommand(IEventAggregator events, DataSetFromDatabasePresenter presenter, IDataEditorView dataEditor, IDataSetProvider datasetProvider)
		{
			_events = events;
			_datasetProvider = datasetProvider;
			_dataEditor = dataEditor;
			_presenter = presenter;

		}
		public void Execute()
		{
			_presenter.SchemaFilePath = _dataEditor.SchemaFileName;
			_presenter.DataFilePath = _dataEditor.DataFileName;
			_presenter.DatabaseConnectionString = _dataEditor.DatabaseConnectionString;
			_presenter.SetDatabaseType(_dataEditor.DatabaseClientType);

			_presenter.Start();

			if (_presenter.OperationResult)
			{
				if (_presenter.DataFileHasChanged)
					_dataEditor.DataFileName = _presenter.DataFilePath;

				if (_presenter.SchemaFileHasChanged)
					_dataEditor.SchemaFileName = _presenter.SchemaFilePath;
				_datasetProvider.DataSetLoadedFromDatabase = true;
				_dataEditor.CloseAllTabs();

				_datasetProvider.ResetSchema();
				_events.Publish(new ReinitializeMainViewRequested());
				//ReInitializeView();
			}

			_dataEditor.DatabaseConnectionString = _presenter.DatabaseConnectionString;
			_dataEditor.DatabaseClientType = _presenter.DatabaseTypeName;
			//TODO: retreive selected database client type the same way

			_dataEditor.DatabaseClientType = _presenter.DatabaseType.ToString();
		}
	}
}
