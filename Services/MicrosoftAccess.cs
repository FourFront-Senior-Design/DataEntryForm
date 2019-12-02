using Common;
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

        public bool InitDBConnection(string sectionFilePath)
        {
            try
            {

                Regex reg = new Regex(@".*_be.accdb");

                var Dbfiles = Directory.GetFiles(sectionFilePath)
                    .Where(path => reg.IsMatch(path))
                    .ToList();

                // set the connection string
                _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Dbfiles[0];

                // create the db connection
                using (OleDbConnection connection = new OleDbConnection(_connectionString))
                // using to ensure connection is closed when we are done
                {
                    try
                    {
                        connection.Open(); // try to open the connection
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

                _rowIndex = 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Headstone GetHeadstone(int index)
        {
            string sqlQuery = "SELECT * FROM Master WHERE AccessUniqueID = " + index.ToString();
            Headstone headstone = new Headstone();
            
            var dataRow = GetDataRow(sqlQuery);


            headstone.SequenceID = dataRow[(int)MasterTableCols.SequenceID].ToString();
            headstone.CemeteryName = dataRow[(int)MasterTableCols.CemeteryName].ToString();
            headstone.BurialSectionNumber = dataRow[(int)MasterTableCols.BurialSectionNumber].ToString();
            headstone.WallID = dataRow[(int)MasterTableCols.Wall].ToString();
            headstone.RowNum = dataRow[(int)MasterTableCols.RowNumber].ToString();
            headstone.GavestoneNumber = dataRow[(int)MasterTableCols.GravesiteNumber].ToString();
            headstone.MarkerType = dataRow[(int)MasterTableCols.MarkerType].ToString();
            headstone.Emblem1 = dataRow[(int)MasterTableCols.Emblem1].ToString();
            headstone.Emblem2 = dataRow[(int)MasterTableCols.Emblem2].ToString();

            headstone.PrimaryDecedent = GetPrimaryPerson(dataRow);

            headstone.OthersDecedentList = GetAddtionalDecedents(dataRow);

            headstone.Image1FilePath = dataRow[(int)MasterTableCols.FrontFilename].ToString();
            headstone.Image1FilePath = dataRow[(int)MasterTableCols.BackFilename].ToString();

            return headstone;
        }

        private object[] GetDataRow(string sqlQuery)
        {
            OleDbCommand cmd;
            OleDbDataReader reader;
            object[] dataRow;
            

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

                reader = cmd.ExecuteReader();
                reader.Read();

                dataRow = new object[reader.FieldCount];
                reader.GetValues(dataRow);
            }

            return dataRow;
        }

        
        private Person GetPrimaryPerson(object[] dataRow)
        {
            Person primaryPerson = new Person();

            if(string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastName].ToString()))
            {
                return null;
            }

            primaryPerson.FirstName = dataRow[(int)MasterTableCols.FirstName].ToString();
            primaryPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleName].ToString();
            primaryPerson.LastName = dataRow[(int)MasterTableCols.LastName].ToString();
            primaryPerson.Suffix = dataRow[(int)MasterTableCols.Suffix].ToString();
            primaryPerson.Location = dataRow[(int)MasterTableCols.Location].ToString();

            primaryPerson.RankList.Add(dataRow[(int)MasterTableCols.Rank].ToString());
            primaryPerson.RankList.Add(dataRow[(int)MasterTableCols.Rank2].ToString());
            primaryPerson.RankList.Add(dataRow[(int)MasterTableCols.Rank3].ToString());

            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award2].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award3].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award4].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award5].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award6].ToString());
            primaryPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award7].ToString());

            primaryPerson.AwardCustom = dataRow[(int)MasterTableCols.Awards_Custom].ToString();

            primaryPerson.WarList.Add(dataRow[(int)MasterTableCols.War].ToString());
            primaryPerson.WarList.Add(dataRow[(int)MasterTableCols.War2].ToString());
            primaryPerson.WarList.Add(dataRow[(int)MasterTableCols.War3].ToString());
            primaryPerson.WarList.Add(dataRow[(int)MasterTableCols.War4].ToString());

            primaryPerson.BranchList.Add(dataRow[(int)MasterTableCols.Branch].ToString());
            primaryPerson.BranchList.Add(dataRow[(int)MasterTableCols.Branch2].ToString());
            primaryPerson.BranchList.Add(dataRow[(int)MasterTableCols.Branch3].ToString());

            primaryPerson.BranchUnitCustom = dataRow[(int)MasterTableCols.Branch_Unit_CustomV].ToString();

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDate].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDate].ToString(), out deathDate);
            primaryPerson.BirthDate = birthDate;
            primaryPerson.DeathDate = deathDate;

            primaryPerson.Inscription = dataRow[(int)MasterTableCols.Inscription].ToString();

            return primaryPerson;
        }

        private List<Person> GetAddtionalDecedents(object[] dataRow)
        {
            List<Person> additonalDecedents = new List<Person>();
            Person additionalDecedent;

            additionalDecedent = GetSecondPerson(dataRow);
            if(additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            additionalDecedent = GetThirdPerson(dataRow);
            if (additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            additionalDecedent = GetForthPerson(dataRow);
            if (additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            additionalDecedent = GetFithPerson(dataRow);
            if (additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            additionalDecedent = GetSixthPerson(dataRow);
            if (additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            additionalDecedent = GetSeventhPerson(dataRow);
            if (additionalDecedent == null)
            {
                return additonalDecedents;
            }
            else
            {
                additonalDecedents.Add(additionalDecedent);
            }

            return additonalDecedents;
        }

        private Person GetSecondPerson(object[] dataRow)
        {
            Person secondPerson = new Person();

            if(string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D].ToString()))
            {
                return null;
            }

            secondPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D].ToString();
            secondPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D].ToString();
            secondPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D].ToString();
            secondPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D].ToString();
            secondPerson.Location = dataRow[(int)MasterTableCols.LocationS_D].ToString();

            secondPerson.RankList.Add(dataRow[(int)MasterTableCols.RankS_D].ToString());
            secondPerson.RankList.Add(dataRow[(int)MasterTableCols.Rank2S_D].ToString());
            secondPerson.RankList.Add(dataRow[(int)MasterTableCols.Rank3S_D].ToString());

            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.AwardS_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award2S_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award3S_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award4S_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award5S_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award6S_D].ToString());
            secondPerson.AwardList.Add(dataRow[(int)MasterTableCols.Award7S_D].ToString());

            secondPerson.AwardCustom = dataRow[(int)MasterTableCols.Awards_CustomS_D].ToString();

            secondPerson.WarList.Add(dataRow[(int)MasterTableCols.WarS_D].ToString());
            secondPerson.WarList.Add(dataRow[(int)MasterTableCols.War2S_D].ToString());
            secondPerson.WarList.Add(dataRow[(int)MasterTableCols.War3S_D].ToString());
            secondPerson.WarList.Add(dataRow[(int)MasterTableCols.War4S_D].ToString());

            secondPerson.BranchList.Add(dataRow[(int)MasterTableCols.BranchS_D].ToString());
            secondPerson.BranchList.Add(dataRow[(int)MasterTableCols.Branch2S_D].ToString());
            secondPerson.BranchList.Add(dataRow[(int)MasterTableCols.Branch3S_D].ToString());

            secondPerson.BranchUnitCustom = dataRow[(int)MasterTableCols.Branch_Unit_CustomS_D].ToString();

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D].ToString(), out deathDate);
            secondPerson.BirthDate = birthDate;
            secondPerson.DeathDate = deathDate;

            secondPerson.Inscription = dataRow[(int)MasterTableCols.InscriptionS_D].ToString();

            return secondPerson;
        }

        private Person GetThirdPerson(object[] dataRow)
        {
            Person thirdPerson = new Person();

            if (string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D_2].ToString()))
            {
                return null;
            }

            thirdPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D_2].ToString();
            thirdPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D_2].ToString();
            thirdPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D_2].ToString();
            thirdPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D_2].ToString();
            thirdPerson.Location = dataRow[(int)MasterTableCols.LocationS_D_2].ToString();

            thirdPerson.RankList.Add(dataRow[(int)MasterTableCols.RankS_D_2].ToString());

            thirdPerson.AwardList.Add(dataRow[(int)MasterTableCols.AwardS_D_2].ToString());

            thirdPerson.WarList.Add(dataRow[(int)MasterTableCols.WarS_D_2].ToString());

            thirdPerson.BranchList.Add(dataRow[(int)MasterTableCols.BranchS_D_2].ToString());

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D_2].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D_2].ToString(), out deathDate);
            thirdPerson.BirthDate = birthDate;
            thirdPerson.DeathDate = deathDate;

            thirdPerson.Inscription = dataRow[(int)MasterTableCols.InscriptionS_D_2].ToString();

            return thirdPerson;
        }

        private Person GetForthPerson(object[] dataRow)
        {
            Person forthPerson = new Person();

            if (string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D_3].ToString()))
            {
                return null;
            }

            forthPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D_3].ToString();
            forthPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D_3].ToString();
            forthPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D_3].ToString();
            forthPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D_3].ToString();
            forthPerson.Location = dataRow[(int)MasterTableCols.LocationS_D_3].ToString();

            forthPerson.RankList.Add(dataRow[(int)MasterTableCols.RankS_D_3].ToString());

            forthPerson.AwardList.Add(dataRow[(int)MasterTableCols.AwardS_D_3].ToString());

            forthPerson.WarList.Add(dataRow[(int)MasterTableCols.WarS_D_3].ToString());

            forthPerson.BranchList.Add(dataRow[(int)MasterTableCols.BranchS_D_3].ToString());

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D_3].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D_3].ToString(), out deathDate);
            forthPerson.BirthDate = birthDate;
            forthPerson.DeathDate = deathDate;

            forthPerson.Inscription = dataRow[(int)MasterTableCols.InscriptionS_D_3].ToString();

            return forthPerson;
        }

        private Person GetFithPerson(object[] dataRow)
        {
            Person fithPerson = new Person();

            if (string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D_4].ToString()))
            {
                return null;
            }

            fithPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D_4].ToString();
            fithPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D_4].ToString();
            fithPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D_4].ToString();
            fithPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D_4].ToString();
            fithPerson.Location = dataRow[(int)MasterTableCols.LocationS_D_4].ToString();

            fithPerson.RankList.Add(dataRow[(int)MasterTableCols.RankS_D_4].ToString());

            fithPerson.AwardList.Add(dataRow[(int)MasterTableCols.AwardS_D_4].ToString());

            fithPerson.WarList.Add(dataRow[(int)MasterTableCols.WarS_D_4].ToString());

            fithPerson.BranchList.Add(dataRow[(int)MasterTableCols.BranchS_D_4].ToString());

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D_4].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D_4].ToString(), out deathDate);
            fithPerson.BirthDate = birthDate;
            fithPerson.DeathDate = deathDate;

            fithPerson.Inscription = dataRow[(int)MasterTableCols.InscriptionS_D_4].ToString();

            return fithPerson;
        }

        private Person GetSixthPerson(object[] dataRow)
        {
            Person sixthPerson = new Person();

            if (string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D_5].ToString()))
            {
                return null;
            }

            sixthPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D_5].ToString();
            sixthPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D_5].ToString();
            sixthPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D_5].ToString();
            sixthPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D_5].ToString();
            sixthPerson.Location = dataRow[(int)MasterTableCols.LocationS_D_5].ToString();

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D_5].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D_5].ToString(), out deathDate);
            sixthPerson.BirthDate = birthDate;
            sixthPerson.DeathDate = deathDate;


            return sixthPerson;
        }

        private Person GetSeventhPerson(object[] dataRow)
        {
            Person seventhPerson = new Person();

            if (string.IsNullOrEmpty(dataRow[(int)MasterTableCols.LastNameS_D_6].ToString()))
            {
                return null;
            }

            seventhPerson.FirstName = dataRow[(int)MasterTableCols.FirstNameS_D_6].ToString();
            seventhPerson.MiddleName = dataRow[(int)MasterTableCols.MiddleNameS_D_6].ToString();
            seventhPerson.LastName = dataRow[(int)MasterTableCols.LastNameS_D_6].ToString();
            seventhPerson.Suffix = dataRow[(int)MasterTableCols.SuffixS_D_6].ToString();
            seventhPerson.Location = dataRow[(int)MasterTableCols.LocationS_D_6].ToString();

            DateTime birthDate, deathDate;
            DateTime.TryParse(dataRow[(int)MasterTableCols.BirthDateS_D_6].ToString(), out birthDate);
            DateTime.TryParse(dataRow[(int)MasterTableCols.DeathDateS_D_6].ToString(), out deathDate);
            seventhPerson.BirthDate = birthDate;
            seventhPerson.DeathDate = deathDate;

            return seventhPerson;
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
    }
}
