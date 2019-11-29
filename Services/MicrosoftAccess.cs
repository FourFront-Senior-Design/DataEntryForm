using DataStructures;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class MicrosoftAccess : IDatabaseService
    {
        private string _connectionString;
        private int _rowIndex;


        public int TotalItems
        {
            get { return 1; }
        }

        public Headstone GetHeadstone(int index)
        {
            string sqlQuery = "SELECT * FROM Master WHERE AccessUniqueID = " + index.ToString();
            Headstone headstone = new Headstone();

            var dataRow = GetDataRow(sqlQuery);

            return headstone;
        }

        public Dictionary<string,object> GetDataRow(string sqlQuery)
        {
            OleDbCommand cmd;
            OleDbDataReader reader;
            Dictionary<string, object> dataRow;

            using (OleDbConnection connection = new OleDbConnection(_connectionString)) // using to ensure connection is closed when we are done
            {
                try
                {
                    cmd = new OleDbCommand(sqlQuery, connection);
                    connection.Open(); // try to open the connection

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error accsessing Database");
                    throw e;
                }

                dataRow = new Dictionary<string, object>();
                reader = cmd.ExecuteReader();

                DataTable table = reader.GetSchemaTable();
                DataColumn nameCol = table.Columns["ColumnName"];

                reader.Read();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow.Add(table.Rows[i][nameCol].ToString(), reader[i]);
                }

            }

            return dataRow;
        }


        public void InitDBConnection(string sectionFilePath)
        {
            Regex reg = new Regex(@".*_be.accdb");

            var Dbfiles = Directory.GetFiles(sectionFilePath)
                .Where(path => reg.IsMatch(path))
                .ToList();

            // set the connection string
            _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Dbfiles[0];

            // create the db connection
            using (OleDbConnection connection = new OleDbConnection(_connectionString)) // using to ensure connection is closed when we are done
            {
                try
                {
                    connection.Open(); // try to open the connection
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error accsessing Database");
                    throw e;
                }
            }

            _rowIndex = 1;
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
    }
}
