using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
{
    public class NDbUnitManager
    {
        public void BuildNDbUnitInstance(string databaseConnectionString, DatabaseClientType databaseType, out IDbConnection databaseConnection, out INDbUnitTest database)
        {
            switch (databaseType)
            {
                case DatabaseClientType.MySqlClient:
                    {
                        databaseConnection = new MySql.Data.MySqlClient.MySqlConnection(databaseConnectionString);
                        database = new NDbUnit.Core.MySqlClient.MySqlDbUnitTest(databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.OleDBClient:
                    {
                        databaseConnection = new System.Data.OleDb.OleDbConnection(databaseConnectionString);
                        database = new NDbUnit.Core.OleDb.OleDbUnitTest(databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.OracleClient:
                    throw new InvalidOperationException("Oracle Client is not yet supported by NDbUnit!");
                case DatabaseClientType.SqlCeClient:
                    {
                        databaseConnection = new System.Data.SqlServerCe.SqlCeConnection(databaseConnectionString);
                        database = new NDbUnit.Core.SqlServerCe.SqlCeUnitTest(databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.SqlClient:
                    {
                        databaseConnection = new System.Data.SqlClient.SqlConnection(databaseConnectionString);
                        database = new NDbUnit.Core.SqlClient.SqlDbUnitTest(databaseConnectionString);
                        break;
                    }
                case DatabaseClientType.SqliteClient:
                    {
                        databaseConnection = new System.Data.SQLite.SQLiteConnection(databaseConnectionString);
                        database = new NDbUnit.Core.SqlLite.SqlLiteUnitTest(databaseConnectionString);
                        break;
                    }
                default:
                    throw new InvalidOperationException("you have selected an invalid database type!");
            }

        }

        public IList<DatabaseClientType> GetSupportedClientTypesList()
        {
            return new List<DatabaseClientType>()
             {
                 DatabaseClientType.MySqlClient,
                 DatabaseClientType.OleDBClient,
                 DatabaseClientType.OracleClient,
                 DatabaseClientType.SqlCeClient,
                 DatabaseClientType.SqlClient,
                 DatabaseClientType.SqliteClient
             };
        }

    }
}
