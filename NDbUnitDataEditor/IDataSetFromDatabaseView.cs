using System;
using System.Collections.Generic;
using System.Text;

namespace NDbUnitDataEditor
{

    public delegate void DataSetFromDatabaseEvent();

    public interface IDataSetFromDatabaseView
    {
        event DataSetFromDatabaseEvent TestDatabaseConnection;
        event DataSetFromDatabaseEvent GetDataSetFromDatabase;
        event DataSetFromDatabaseEvent PutDataSetToDatabase;
        event DataSetFromDatabaseEvent SelectDatabaseType;
        void Run();
        string DatabaseConnectionString { get; set; }
        IList<DataSetFromDatabasePresenter.DatabaseClientType> DatabaseConnectionTypes { set; }
        DataSetFromDatabasePresenter.DatabaseClientType SelectedDatabaseConnectionType { get; }
        bool ConnectionTestResult { set; }
        bool PutDataSetToDatabaseResult { set; }
        bool GetDataSetFromDatabaseResult { set; }
    }
}
