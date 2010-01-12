using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMeta;
using System.Diagnostics;

namespace NDbUnitDataEditor.ZeusSchemaBuilder
{
    public class TableListBuilder
    {
        private string _connectionString;



        public TableListBuilder(string connectionString)
        {
            _connectionString = connectionString;

        }
        public IList<string> BuildTableList()
        {

            IList<string> tables = new List<string>();

            dbRoot myMeta = new dbRoot();
            myMeta.Connect(dbDriver.SQL, _connectionString);

            IDatabase db = myMeta.DefaultDatabase;

            foreach (ITable table in db.Tables)
            {
                tables.Add(table.Name);
            }

            return tables;

        }
    }

    /*

string connectionstring = @"data source=SQLiteDatabase.DB";
 
MyMeta.dbRoot myMeta = new MyMeta.dbRoot();
myMeta.Connect(MyMeta.dbDriver.SQLite, connectionstring);
 
IDatabase db = myMeta.DefaultDatabase;
 
foreach (MyMeta.ITable table in db.Tables)
{
    Console.WriteLine("{0} ({1})", table.Name, table.Columns.Count);
    Console.WriteLine("\tCOLUMNS");
 
    foreach (MyMeta.IColumn column in table.Columns)
    {
        Console.WriteLine("\t\t{0} ({1}), Nullable:{2}",
                 column.Name, column.DataTypeName, column.IsNullable);
    }
 
    Console.WriteLine("\tINDEXES");
 
    foreach (MyMeta.IIndex index in table.Indexes)
    {
        Console.WriteLine("\t\t{0}, Unique:{1}", index.Name, index.Unique);
    }
}     

     */

}
