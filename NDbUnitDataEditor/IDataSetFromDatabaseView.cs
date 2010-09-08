using System;
using System.Collections.Generic;
using System.Text;

namespace NDbUnitDataEditor
{

    public delegate void DataSetDatabaseEvent();

    public interface IDataSetFromDatabaseView
    {
        string SelectFile(string initialFilename, string selectionFilter);
        event DataSetDatabaseEvent TestDatabaseConnection;
        event DataSetDatabaseEvent GetDataSetFromDatabase;
        event DataSetDatabaseEvent PutDataSetToDatabase;
        event DataSetDatabaseEvent SelectDatabaseType;
        int DatabaseTypeSelectedIndex { get; set; }
        void Run();
        string ErrorMessage { get; set; }
        string DatabaseConnectionString { get; set; }
        IList<DatabaseClientType> DatabaseConnectionTypes { set; }
        DatabaseClientType SelectedDatabaseConnectionType { get; }
        bool ConnectionTestResult { set; }
        bool PutDataSetToDatabaseResult { set; }
        bool GetDataSetFromDatabaseResult { set; }
    }
}
