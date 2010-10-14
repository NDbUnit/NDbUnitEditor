using System;
using System.Data;

namespace NDbUnitDataEditor
{
	public interface IDataSetProvider
	{
		void SaveDataToFile(string fileName);
		void ReadDataFromFile(string fileName);
		void ResetSchema();
		bool HasTableChanged(string tableName);
		void ReadSchemaFromFile(string fileName);
		DataSet Data { get; }
	}
    public class DataSetProvider : IDataSetProvider
	{
		DataSet _dataSet = new DataSet();
		public void ReadSchemaFromFile(string fileName)
		{
			_dataSet.ReadXmlSchema(fileName);
		}

		public void ReadDataFromFile(string fileName)
		{
			_dataSet.Clear();
			_dataSet.ReadXml(fileName);
		}

		public void SaveDataToFile(string fileName)
		{
			_dataSet.WriteXml(fileName);
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

		public void ResetSchema()
		{
			_dataSet.Clear();
			_dataSet.Dispose();
			_dataSet = new DataSet();
		}
	}
}
