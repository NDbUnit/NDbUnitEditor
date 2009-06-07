using System;
using System.Collections.Generic;
using System.Text;

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

        private DatabaseClientType _databaseType;

        IDataSetFromDatabaseView _dataSetFromDatabase;

        /// <summary>
        /// Initializes a new instance of the DataSetFromDatabasePresenter class.
        /// </summary>
        /// <param name="dataSetFromDatabase"></param>
        public DataSetFromDatabasePresenter(IDataSetFromDatabaseView dataSetFromDatabase)
        {
            _dataSetFromDatabase = dataSetFromDatabase;
            _dataSetFromDatabase.FillDataSetFromDatabase += FillDataSetFromDatabase;
            _dataSetFromDatabase.TestDatabaseConnection += TestDatabaseConnection;
            _dataSetFromDatabase.SelectDatabaseType += SetDatabaseType;
            FillPresenterWithSupportedDatabaseTypesList();
        }

        public void Start()
        {
            _dataSetFromDatabase.Run();
        }

        private void FillDataSetFromDatabase()
        {
            //TODO: code this so that the dataset is filled from the database in this method!
            throw new NotImplementedException("method not implemented!");
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

        private System.Data.IDbConnection GetConnection()
        {
            try
            {
                switch (_databaseType)
                {
                    case DatabaseClientType.MySqlClient:
                        return new MySql.Data.MySqlClient.MySqlConnection(_dataSetFromDatabase.DatabaseConnectionString);
                    case DatabaseClientType.OleDBClient:
                        return new System.Data.OleDb.OleDbConnection(_dataSetFromDatabase.DatabaseConnectionString);
                    case DatabaseClientType.OracleClient:
                        throw new InvalidOperationException("Oracle Client is not yet supported by NDbUnit!");
                    case DatabaseClientType.SqlCeClient:
                        return new System.Data.SqlServerCe.SqlCeConnection(_dataSetFromDatabase.DatabaseConnectionString);
                    case DatabaseClientType.SqlClient:
                        return new System.Data.SqlClient.SqlConnection(_dataSetFromDatabase.DatabaseConnectionString);
                    case DatabaseClientType.SqliteClient:
                        return new System.Data.SQLite.SQLiteConnection(_dataSetFromDatabase.DatabaseConnectionString);
                    default:
                        throw new InvalidOperationException("you have selected an invalid database type!");
                }
            }
            catch (Exception)
            {
                //if anything went wrong with the attempt to construct the IDbConnection, just return NULL b/c we don't care why
                return null;
            }

        }

        private void SetDatabaseType()
        {
            _databaseType = _dataSetFromDatabase.SelectedDatabaseConnectionType;
        }

        private void TestDatabaseConnection()
        {
            using (System.Data.IDbConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    conn.Close();
                    _dataSetFromDatabase.ConnectionTestResult = true;
                }
                catch (Exception)
                {
                    _dataSetFromDatabase.ConnectionTestResult = false;
                }
            }

        }

    }
}
