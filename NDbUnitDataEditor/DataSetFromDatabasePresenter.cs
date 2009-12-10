using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{

    public class DataSetFromDatabasePresenter
    {
        //private INDbUnitTest _database;

        //private IDbConnection _databaseConnection;

        private string _databaseConnectionString;

        private DatabaseClientType _databaseType;

        private int _databaseTypeSelectedIndex;

        private DataSet _dataSet;

        IDataSetFromDatabaseView _dataSetFromDatabase;

        private NDbUnitFacade _nDbUnit;

        /// <summary>
        /// Initializes a new instance of the DataSetFromDatabasePresenter class.
        /// </summary>
        /// <param name="nDbUnitManager"></param>
        /// <param name="dataSetFromDatabase"></param>
        public DataSetFromDatabasePresenter(IDataSetFromDatabaseView dataSetFromDatabase, NDbUnitFacade nDbUnitManager)
        {
            _nDbUnit = nDbUnitManager;
            _dataSetFromDatabase = dataSetFromDatabase;
            _dataSetFromDatabase.GetDataSetFromDatabase += GetDataSetFromDatabase;
            _dataSetFromDatabase.PutDataSetToDatabase += PutDataSetToDatabase;
            _dataSetFromDatabase.TestDatabaseConnection += TestDatabaseConnection;
            _dataSetFromDatabase.SelectDatabaseType += SetDatabaseType;
            _dataSetFromDatabase.GetSchemaFromDatabase += GetSchemaFromDatabase;
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
            _dataSetFromDatabase.DatabaseConnectionTypes = _nDbUnit.GetSupportedClientTypesList();
        }

        private void GetDataSetFromDatabase()
        {
            try
            {
                SetupNDbUnit();

                _dataSet = _nDbUnit.GetDataSetFromDatabase(XsdFilePath);
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

        private void GetSchemaFromDatabase()
        {
            //TODO: implement the ZEUSCMD-based script invocation class here
            MessageBox.Show("Retrieving Schema!");
        }

        private void PutDataSetToDatabase()
        {
            try
            {
                SetupNDbUnit();

                _nDbUnit.PutDataSetToDatabase(XsdFilePath, XmlFilePath);
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

        private void SetupNDbUnit()
        {
            _nDbUnit.Setup(DatabaseConnectionString, DatabaseType);
        }

        private void TestDatabaseConnection()
        {
            try
            {
                DatabaseConnectionString = _dataSetFromDatabase.DatabaseConnectionString;
                DatabaseTypeSelectedIndex = _dataSetFromDatabase.DatabaseTypeSelectedIndex;

                SetupNDbUnit();

                _dataSetFromDatabase.ConnectionTestResult = _nDbUnit.TestConnection();
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.ErrorMessage = ex.Message;
                _dataSetFromDatabase.ConnectionTestResult = false;
            }

        }

    }
}
