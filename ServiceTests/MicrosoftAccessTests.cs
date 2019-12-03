using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace ServiceTests
{
    [TestClass]
    public class MicrosoftAccessTests
    {
        [TestMethod]
        public void CanAccessDB()
        { 
            MicrosoftAccess databaseService = new MicrosoftAccess();
            
            databaseService.InitDBConnection(@"C:\Users\7405148\Desktop\Sha\2019_Fall\Senior_design\Database");


            Headstone headstone = databaseService.GetHeadstone(1);
        }

        [TestMethod]
        public void CanReadTableColHeaders()
        {
            Regex reg = new Regex(@".*_be.accdb");

            var Dbfiles = Directory.GetFiles(@"C:\Users\7405148\Desktop\Sha\2019_Fall\Senior_design\Database")
                .Where(path => reg.IsMatch(path))
                .ToList();

            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Dbfiles[0];

            using (var con = new OleDbConnection(conStr))
            {
                con.Open();
                using (var cmd = new OleDbCommand("select * from Master where AccessUniqueID = 1", con))
                using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    DataTable table = reader.GetSchemaTable();
                    DataColumn nameCol = table.Columns["ColumnName"];
                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine(row[nameCol]);
                        var temp = row[nameCol];
                    }
                }
            }
        }

        public void GetAllCemeteryNames()
        {
            
        }
    }
}
