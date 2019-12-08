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

        [TestMethod]
        public void CanQuerryAllCemeteryNames()
        {
            MicrosoftAccess databaseService = new MicrosoftAccess();

            databaseService.InitDBConnection(@"C:\Users\7295201\csc464_465_senior_design\Section0000P");


            List<CemeteryNameData> Cemeterynames = databaseService.CemeteryNames;
            List<AwardData> Awardnames = databaseService.AwardNames;
            List<BranchData> Branchnames = databaseService.BranchNames;
            List<EmblemData> EmblemNames = databaseService.EmblemNames;
            List<LocationData> LocationNames = databaseService.LocationNames;
        }

        [TestMethod]
        public void CanReadIntoBranchUnit()
        {

            string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"C:\Users\7309977\Desktop\Section0000P\FRNC_SectionP_be.accdb";
            string sqlQuery = @"UPDATE Master SET ";
            Dictionary<string, string> headstoneData = new Dictionary<string, string>();

            headstoneData.Add("Branch-Unit_CustomV", "my Test Data");


            // Append all keys and values to the string
            //foreach (KeyValuePair<string, string> entry in headstoneData)
            //{
            //    sqlQuery += @"[" + entry.Key + @"] = " + @"@" + entry.Key + @", ";
            //}

            // trim the last ", " off
            //sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);

            sqlQuery += "[Branch-Unit_CustomV] = my test data ";

            // finalize update statement
            sqlQuery += @" WHERE AccessUniqueID = " + 1 + @";";

            OleDbCommand cmd;
            using (OleDbConnection connection = new OleDbConnection(_connectionString)) // using to ensure connection is closed when we are done
            {
                try
                {
                    cmd = new OleDbCommand(sqlQuery, connection);
                    connection.Open(); // try to open the connection
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error accessing Database");
                    throw e;
                }

                //foreach (KeyValuePair<string, string> entry in headstoneData)
                //{
                //    cmd.Parameters.AddWithValue("@" + entry.Key,("'" + entry.Value + "'"));
                //}

                try
                {
                    cmd.ExecuteNonQuery(); // do the update
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing to the database:");
                    Console.WriteLine(e);
                    Console.WriteLine(sqlQuery);
                }
            }
        }

    }
}
