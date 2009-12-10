using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
{
    public class NDbUnitFacade
    {
        private IDbConnection _databaseConnection;

        private string _databaseConnectionString;

        private INDbUnitTest _database;

        private DatabaseClientType _databaseType;

        public DataSet GetDataSetFromDatabase(string xsdFilePath)
        {
            _database.ReadXmlSchema(xsdFilePath);
            return _database.GetDataSetFromDb();
        }

        public IList<DatabaseClientType> GetSupportedClientTypesList()
        {
            return new List<DatabaseClientType>()
             {
                DatabaseClientType.SqlClient,
                DatabaseClientType.OleDBClient,
                DatabaseClientType.MySqlClient,
                DatabaseClientType.SqliteClient,
                DatabaseClientType.SqlCeClient,
                DatabaseClientType.OracleClient
             };
        }

        public void PutDataSetToDatabase(string xsdFilePath, string xmlFilePath)
        {
            _database.ReadXmlSchema(xsdFilePath);
            _database.ReadXml(xmlFilePath);
            _database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        public void Setup(string databaseConnectionString, DatabaseClientType databaseType)
        {
            if (String.IsNullOrEmpty(databaseConnectionString))
                throw new ArgumentException("databaseConnectionString is null or empty.", "databaseConnectionString");

            _databaseConnectionString = databaseConnectionString;
            _databaseType = databaseType;

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

        public bool TestConnection()
        {
            EnsureSetup();
            _databaseConnection.Open();
            _databaseConnection.Close();

            return true;
        }

        private void EnsureSetup()
        {
            if (_database == null || _databaseConnection == null)
                Setup(_databaseConnectionString, _databaseType);
        }

    }
}
