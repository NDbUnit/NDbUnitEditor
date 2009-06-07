using System;
using System.Collections.Generic;
using System.Text;

namespace NDbUnitDataEditor
{

    public delegate void DataSetFromDatabaseEvent();

    public interface IDataSetFromDatabaseView
    {
        event DataSetFromDatabaseEvent TestDatabaseConnection;
        event DataSetFromDatabaseEvent FillDataSetFromDatabase;
        event DataSetFromDatabaseEvent SelectDatabaseType;
        void Run();
        string DatabaseConnectionString { get;}
        IList<DataSetFromDatabasePresenter.DatabaseClientType> DatabaseConnectionTypes { set; }
        DataSetFromDatabasePresenter.DatabaseClientType SelectedDatabaseConnectionType { get; }
        bool ConnectionTestResult { set; }
    }
}
