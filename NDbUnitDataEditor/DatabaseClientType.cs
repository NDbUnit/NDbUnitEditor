using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
{
    /// <summary>
    /// Types of Supported Databases
    /// </summary>
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
        /// Oracle database
        /// </summary>
        OracleClient,


        /// <summary>
        /// Postgresql database
        /// </summary>
        PostgresqlClient
    }
}
