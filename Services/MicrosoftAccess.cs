using DataStructures;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
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
        private OleDbConnection DbConnection;
        private int index;


        public int TotalItems
        {
            get { return 1; }
        }

        public Headstone GetHeadstone(int index)
        {
            string sql = "select * from Master where AccessUniqueID = " + index.ToString();

            OleDbDataReader DbReader;
            OleDbCommand cmd = new OleDbCommand(sql, DbConnection);
            DbReader = cmd.ExecuteReader();

            DbReader.Read();
            Headstone headstone = new Headstone();

            headstone.SequenceID = DbReader.GetString(1);

            return headstone;
        }


        public void InitDBConnection(string sectionFilePath)
        {
            Regex reg = new Regex(@".*_be.accdb");

            var Dbfiles = Directory.GetFiles(sectionFilePath)
                .Where(path => reg.IsMatch(path))
                .ToList();

            DbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Dbfiles[0]);

            DbConnection.Open();
            index = 1;
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
    }
}
