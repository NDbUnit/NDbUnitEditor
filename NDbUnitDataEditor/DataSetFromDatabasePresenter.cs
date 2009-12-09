using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
{

    public class DataSetFromDatabasePresenter
    {
        private INDbUnitTest _database;

        private IDbConnection _databaseConnection;

        private string _databaseConnectionString;

        private DatabaseClientType _databaseType;

        private int _databaseTypeSelectedIndex;

        private DataSet _dataSet;

        IDataSetFromDatabaseView _dataSetFromDatabase;

        private NDbUnitManager _nDbUnitManager;

        /// <summary>
        /// Initializes a new instance of the DataSetFromDatabasePresenter class.
        /// </summary>
        /// <param name="nDbUnitManager"></param>
        /// <param name="dataSetFromDatabase"></param>
        public DataSetFromDatabasePresenter(IDataSetFromDatabaseView dataSetFromDatabase, NDbUnitManager nDbUnitManager)
        {
            _nDbUnitManager = nDbUnitManager;
            _dataSetFromDatabase = dataSetFromDatabase;
            _dataSetFromDatabase.GetDataSetFromDatabase += GetDataSetFromDatabase;
            _dataSetFromDatabase.PutDataSetToDatabase += PutDataSetToDatabase;
            _dataSetFromDatabase.TestDatabaseConnection += TestDatabaseConnection;
            _dataSetFromDatabase.SelectDatabaseType += SetDatabaseType;
            FillPresenterWithSupportedDatabaseTypesList();
        }

        public string DatabaseConnectionString
        {
            get
            {
                return _databaseConnectionString;
            }
            set
            {
                _databaseConnectionString = value;
            }
        }

        public DatabaseClientType DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;
            }
        }

        public int DatabaseTypeSelectedIndex
        {
            get
            {
                return _databaseTypeSelectedIndex;
            }
            set
            {
                _databaseTypeSelectedIndex = value;
            }
        }

        public DataSet DataSet
        {
            get
            {
                return _dataSet;
            }
        }

        public bool DataSetFromDatabaseResult { get; set; }

        public string XmlFilePath { get; set; }

        public string XsdFilePath { get; set; }

        public void Start()
        {
            _dataSetFromDatabase.DatabaseConnectionString = DatabaseConnectionString;
            _dataSetFromDatabase.DatabaseTypeSelectedIndex = DatabaseTypeSelectedIndex;
            _dataSetFromDatabase.Run();
        }

        private void FillPresenterWithSupportedDatabaseTypesList()
        {
            _dataSetFromDatabase.DatabaseConnectionTypes = _nDbUnitManager.GetSupportedClientTypesList();
        }

        private void GetDataSetFromDatabase()
        {
            try
            {
                SetupNDbUnitForUse();

                _database.ReadXmlSchema(XsdFilePath);
                _dataSet = _database.GetDataSetFromDb();
                _dataSetFromDatabase.GetDataSetFromDatabaseResult = true;
                DataSetFromDatabaseResult = true;
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.GetDataSetFromDatabaseResult = false;
                DataSetFromDatabaseResult = false;
            }

        }

        private void PutDataSetToDatabase()
        {
            try
            {
                SetupNDbUnitForUse();

                _database.ReadXmlSchema(XsdFilePath);
                _database.ReadXml(XmlFilePath);
                _database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);

                _dataSetFromDatabase.PutDataSetToDatabaseResult = true;
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.PutDataSetToDatabaseResult = false;
            }

        }

        private void SetDatabaseType()
        {
            _databaseType = _dataSetFromDatabase.SelectedDatabaseConnectionType;
        }

        private void SetupNDbUnitForUse()
        {
            _nDbUnitManager.BuildNDbUnitInstance(DatabaseConnectionString, DatabaseType, out _databaseConnection, out _database);
        }

        private void TestDatabaseConnection()
        {
            try
            {
                DatabaseConnectionString = _dataSetFromDatabase.DatabaseConnectionString;
                DatabaseTypeSelectedIndex = _dataSetFromDatabase.DatabaseTypeSelectedIndex;
                SetupNDbUnitForUse();
                _databaseConnection.Open();
                _databaseConnection.Close();
                _dataSetFromDatabase.ConnectionTestResult = true;
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.ConnectionTestResult = false;
            }

        }

    }
}
