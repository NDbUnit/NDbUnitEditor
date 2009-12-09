using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NDbUnit.Core;

namespace NDbUnitDataEditor
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
}
