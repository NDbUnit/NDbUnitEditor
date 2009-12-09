using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
{
    public class DataSetFromDatabasePresenter
    {
        //TODO: condsider either refactoring this entire list into NDbUnit.Core.dll itself or (perhaps) just use it from Proteus
        public enum DatabaseClientType
        {
            /// <summary>
            /// Microsoft SQL Server database
            /// </summary>
            SqlClient,

            /// <summary>
            /// Microsoft OleDB-compatible database
            /// </summary>
            OleDBClient,

            /// <summary>
            /// Microsoft SQL Server CE database
            /// </summary>
            SqlCeClient,

            /// <summary>
            /// SQLite database
            /// </summary>
            SqliteClient,

            /// <summary>
            /// MySQL database
            /// </summary>
            MySqlClient,

            /// <summary>
            /// Oracle database (NOT YET SUPPORTED)
            /// </summary>
            /// <remarks>
            /// Oracle database support is not yet available; as of now, Oracle interaction can be achieved via the OleDB drivers for Oracle (select OleDBClient as the database type).
            /// </remarks>
            OracleClient
        }

        private INDbUnitTest _database;

        private System.Data.IDbConnection _databaseConnection;

        private string _databaseConnectionString;

        private DatabaseClientType _databaseType;

        private int _databaseTypeSelectedIndex;
        private DataSet _dataSet;

        IDataSetFromDatabaseView _dataSetFromDatabase;

        /// <summary>
        /// Initializes a new instance of the DataSetFromDatabasePresenter class.
        /// </summary>
        /// <param name="dataSetFromDatabase"></param>
        public DataSetFromDatabasePresenter(IDataSetFromDatabaseView dataSetFromDatabase)
        {
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

        private void BuildNDbUnitInstance()
        {

            _databaseConnectionString = _dataSetFromDatabase.DatabaseConnectionString;
            switch (_databaseType)
            {
                case DatabaseClientType.MySqlClient:
                    {
                        _databaseConnection = new MySql.Data.MySqlClient.MySqlConnection(_databaseConnectionString);
                        _database = new NDbUnit.Core.MySqlClient.MySqlDbUnitTest(_databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.OleDBClient:
                    {
                        _databaseConnection = new System.Data.OleDb.OleDbConnection(_databaseConnectionString);
                        _database = new NDbUnit.Core.OleDb.OleDbUnitTest(_databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.OracleClient:
                    throw new InvalidOperationException("Oracle Client is not yet supported by NDbUnit!");
                case DatabaseClientType.SqlCeClient:
                    {
                        _databaseConnection = new System.Data.SqlServerCe.SqlCeConnection(_databaseConnectionString);
                        _database = new NDbUnit.Core.SqlServerCe.SqlCeUnitTest(_databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.SqlClient:
                    {
                        _databaseConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                        _database = new NDbUnit.Core.SqlClient.SqlDbUnitTest(_databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.SqliteClient:
                    {
                        _databaseConnection = new System.Data.SQLite.SQLiteConnection(_databaseConnectionString);
                        _database = new NDbUnit.Core.SqlLite.SqlLiteUnitTest(_databaseConnectionString);
                        break;
                    }
                default:
                    throw new InvalidOperationException("you have selected an invalid database type!");
            }

        }

        private void FillPresenterWithSupportedDatabaseTypesList()
        {
            _dataSetFromDatabase.DatabaseConnectionTypes = new List<DatabaseClientType>()
             {
                 DatabaseClientType.MySqlClient,
                 DatabaseClientType.OleDBClient,
                 DatabaseClientType.OracleClient,
                 DatabaseClientType.SqlCeClient,
                 DatabaseClientType.SqlClient,
                 DatabaseClientType.SqliteClient
             };
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

                _dataSetFromDatabase.GetDataSetFromDatabaseResult = true;
            }
            catch (Exception ex)
            {
                _dataSetFromDatabase.GetDataSetFromDatabaseResult = false;
            }

        }

        private void SetDatabaseType()
        {
            _databaseType = _dataSetFromDatabase.SelectedDatabaseConnectionType;
        }

        private void SetupNDbUnitForUse()
        {
            BuildNDbUnitInstance();
        }

        private void TestDatabaseConnection()
        {
            try
            {
                DatabaseConnectionString = _dataSetFromDatabase.DatabaseConnectionString;
                DatabaseTypeSelectedIndex = _dataSetFromDatabase.DatabaseTypeSelectedIndex;
                BuildNDbUnitInstance();
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
