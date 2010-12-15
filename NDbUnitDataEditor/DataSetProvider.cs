using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace NDbUnitDataEditor
{
	public class DataTableInfo
	{
		public int NumberOfRows { get; set; }
	}

	public interface IDataSetProvider
	{
		DataTableInfo GetTableInfo(string tableName);
		string DataSetName { get; }
		DataTable GetFirstTable();
		DataTable GetTable(string tableName);
		bool IsDirty();
		bool DataSetLoadedFromDatabase { get; set; }
		void ReplaceData(DataSet dataSet);
		IEnumerable<string> GetTableNames();
		void SaveDataToFile(string fileName);
		void ReadDataFromFile(string fileName);
		void ResetSchema();
		bool HasTableChanged(string tableName);
		void ReadSchemaFromFile(string fileName);
		DataSet Data { get; }
	}

    public class DataSetProvider : IDataSetProvider
	{
		bool _dataSetLoadedFromDatabase = false;

		DataSet _dataSet = new DataSet();
		public void ReadSchemaFromFile(string fileName)
		{
			_dataSet.ReadXmlSchema(fileName);
		}

		public void ReadDataFromFile(string fileName)
		{
			_dataSet.Clear();
			_dataSet.ReadXml(fileName);
			_dataSet.AcceptChanges();
		}

		public void SaveDataToFile(string fileName)
		{
			_dataSet.WriteXml(fileName);
		}

		public void ReplaceData(DataSet dataSet)
		{
			_dataSet = dataSet;
		}

		public DataSet Data
		{
			get { return _dataSet; }
		}

		public bool HasTableChanged(string tableName)
		{
			DataTable table = _dataSet.Tables[tableName];
			var changes = table.GetChanges();
			if (changes == null)
				return false;
			return true;
		}

		public string DataSetName
		{
			get
			{
				return _dataSet.DataSetName;
			}
		}

		public IEnumerable<string> GetTableNames()
		{
			if (_dataSet.Tables == null)
				return Enumerable.Empty<string>();
			return _dataSet.Tables.Cast<DataTable>().Select(t => t.TableName);
		}

		public DataTable GetFirstTable()
		{
			return _dataSet.Tables.Cast<DataTable>().FirstOrDefault();
		}

		public DataTable GetTable(string tableName)
		{
			return _dataSet.Tables
				.Cast<DataTable>()
				.Where(t => t.TableName == tableName)
				.FirstOrDefault();
		}

		public DataTableInfo GetTableInfo(string tableName)
		{
			var table = GetTable(tableName);
			var info = new DataTableInfo { NumberOfRows = table.Rows.Count };
			return info;
		}

		public bool IsDirty()
		{
			return _dataSet.HasChanges() || _dataSetLoadedFromDatabase ;
		}

		public void ResetSchema()
		{
			_dataSet.Clear();
			_dataSet.Dispose();
			_dataSet = new DataSet();
		}


		public bool DataSetLoadedFromDatabase
		{
			get
			{
				return _dataSetLoadedFromDatabase;
			}
			set
			{
				_dataSetLoadedFromDatabase = value;
			}
		}
	}
}
