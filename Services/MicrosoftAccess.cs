﻿using Common;
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
            get { return _GetTotalRecords(); }
        }

        public string SectionFilePath { get; private set; } = string.Empty;

        public bool InitDBConnection(string sectionFilePath)
        {
            SectionFilePath = sectionFilePath;

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

        private int _GetTotalRecords()
        {
            string sqlQuery = "SELECT COUNT(AccessUniqueID) FROM Master";
            OleDbCommand cmd;
            OleDbDataReader reader;

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

                return reader.GetInt32(0);
            }

        }

        public Headstone GetHeadstone(int index)
        {
            string sqlQuery = "SELECT * FROM Master WHERE AccessUniqueID = " + index.ToString();
            Headstone headstone = new Headstone();
            
            var dataRow = GetDataRow(sqlQuery);


            headstone.SequenceID = dataRow[(int)MasterTableCols.SequenceID].ToString();
            headstone.PrimaryKey = dataRow[(int)MasterTableCols.PrimaryKey].ToString();
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
            headstone.Image2FilePath = dataRow[(int)MasterTableCols.BackFilename].ToString();

            headstone.Image1FileName = Path.GetFileName(headstone.Image1FilePath);
            headstone.Image2FileName = Path.GetFileName(headstone.Image2FilePath);

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

        public List<string> GetCemeteryNames()
        {
            OleDbCommand cmd;
            OleDbDataReader reader;
            List<string> CemeteryNames = new List<string>();

            string sqlQuery = "SELECT CemeteryName FROM CemeteryNames";

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

                while(reader.Read())
                {
                    CemeteryNames.Add(reader.GetString(0));
                }

                reader.Close();
            }

            return CemeteryNames;

        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            // For each field in headstone that has content, update the database
            Dictionary<string, string> headstoneData = new Dictionary<string, string>();

            SetHeader(ref headstoneData, ref headstone);
            SetPrimaryPerson(ref headstoneData, ref headstone);
            SetFirstPerson(ref headstoneData, ref headstone);
            SetSecondPerson(ref headstoneData, ref headstone);
            SetThirdPerson(ref headstoneData, ref headstone);
            SetFourthPerson(ref headstoneData, ref headstone);
            SetFifthPerson(ref headstoneData, ref headstone);
            SetSixthPerson(ref headstoneData, ref headstone);

            string sqlQuery = "UPDATE Master SET ";

            // Append all keys and values to the string
            foreach (KeyValuePair<string, string> entry in headstoneData)
            {
                sqlQuery += entry.Key + " = " + entry.Value + ", ";
            }

            // trim the last ", " off
            sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 2);

            sqlQuery += "WHERE AccessUniqueID = " + index;

            OleDbCommand cmd;
            using (OleDbConnection connection = new OleDbConnection(_connectionString)) // using to ensure connection is closed when we are done
            {
                try
                {
                    cmd = new OleDbCommand(sqlQuery, connection);
                    connection.Open(); // try to open the connection
                    cmd.ExecuteNonQuery(); // do the update
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error accsessing Database");
                    throw e;
                }
            }
        }

        private void SetHeader(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("CemetaryName", headstone.CemeteryName);
            dict.Add("BurialSection", headstone.BurialSectionNumber);
            dict.Add("Wall", headstone.WallID);
            dict.Add("Row#", headstone.RowNum);
            dict.Add("Gravesite#", headstone.GavestoneNumber);
            dict.Add("MarkerType", headstone.MarkerType);
            dict.Add("Emblem1", headstone.Emblem1);
            dict.Add("Emblem2", headstone.Emblem2);

        }

        private void SetPrimaryPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstName", headstone.PrimaryDecedent.FirstName);
            dict.Add("MiddleName", headstone.PrimaryDecedent.MiddleName);
            dict.Add("LastName", headstone.PrimaryDecedent.LastName);
            dict.Add("Suffix", headstone.PrimaryDecedent.Suffix);
            dict.Add("Location", headstone.PrimaryDecedent.Location);

            dict.Add("Rank", headstone.PrimaryDecedent.RankList[0]);
            dict.Add("Rank2", headstone.PrimaryDecedent.RankList[1]);
            dict.Add("Rank3", headstone.PrimaryDecedent.RankList[2]);

            dict.Add("Award", headstone.PrimaryDecedent.AwardList[0]);
            dict.Add("Award2", headstone.PrimaryDecedent.AwardList[1]);
            dict.Add("Award3", headstone.PrimaryDecedent.AwardList[2]);
            dict.Add("Award4", headstone.PrimaryDecedent.AwardList[3]);
            dict.Add("Award5", headstone.PrimaryDecedent.AwardList[4]);
            dict.Add("Award6", headstone.PrimaryDecedent.AwardList[5]);
            dict.Add("Award7", headstone.PrimaryDecedent.AwardList[6]);

            dict.Add("Award_Custom", headstone.PrimaryDecedent.AwardCustom);

            dict.Add("War", headstone.PrimaryDecedent.WarList[0]);
            dict.Add("War2", headstone.PrimaryDecedent.WarList[1]);
            dict.Add("War3", headstone.PrimaryDecedent.WarList[2]);
            dict.Add("War4", headstone.PrimaryDecedent.WarList[3]);

            dict.Add("Branch", headstone.PrimaryDecedent.BranchList[0]);
            dict.Add("Branch2", headstone.PrimaryDecedent.BranchList[1]);
            dict.Add("Branch3", headstone.PrimaryDecedent.BranchList[2]);
            dict.Add("Branch-Unit_CustomV", headstone.PrimaryDecedent.BranchUnitCustom);

            dict.Add("BirthDate", headstone.PrimaryDecedent.BirthDate.ToString());
            dict.Add("DeathDate", headstone.PrimaryDecedent.DeathDate.ToString());

            dict.Add("Inscription", headstone.PrimaryDecedent.Inscription);
        }

        private void SetFirstPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameSpouse/Dependent", headstone.OthersDecedentList[0].FirstName);
            dict.Add("MiddleNameSpouse/Dependent", headstone.OthersDecedentList[0].MiddleName);
            dict.Add("LastNameSpouse/Dependent", headstone.OthersDecedentList[0].LastName);
            dict.Add("SuffixSpouse/Dependent", headstone.OthersDecedentList[0].Suffix);
            dict.Add("LocationS_D", headstone.OthersDecedentList[0].Location);

            dict.Add("RankS_D", headstone.OthersDecedentList[0].RankList[0]);
            dict.Add("Rank2S_D", headstone.OthersDecedentList[0].RankList[1]);
            dict.Add("Rank3S_D", headstone.OthersDecedentList[0].RankList[2]);

            dict.Add("AwardS_D", headstone.OthersDecedentList[0].AwardList[0]);
            dict.Add("Award2S_D", headstone.OthersDecedentList[0].AwardList[1]);
            dict.Add("Award3S_D", headstone.OthersDecedentList[0].AwardList[2]);
            dict.Add("Award4S_D", headstone.OthersDecedentList[0].AwardList[3]);
            dict.Add("Award5S_D", headstone.OthersDecedentList[0].AwardList[4]);
            dict.Add("Award6S_D", headstone.OthersDecedentList[0].AwardList[5]);
            dict.Add("Award7S_D", headstone.OthersDecedentList[0].AwardList[6]);
            dict.Add("Award_CustomS_D", headstone.OthersDecedentList[0].AwardCustom);

            dict.Add("WarS_D", headstone.OthersDecedentList[0].WarList[0]);
            dict.Add("War2S_D", headstone.OthersDecedentList[0].WarList[1]);
            dict.Add("War3S_D", headstone.OthersDecedentList[0].WarList[2]);
            dict.Add("War4S_D", headstone.OthersDecedentList[0].WarList[3]);

            dict.Add("BranchS_D", headstone.OthersDecedentList[0].BranchList[0]);
            dict.Add("Branch2S_D", headstone.OthersDecedentList[0].BranchList[1]);
            dict.Add("Branch3S_D", headstone.OthersDecedentList[0].BranchList[2]);
            dict.Add("Branch-Unit_CustomS_D", headstone.OthersDecedentList[0].BranchUnitCustom);

            dict.Add("BirthDateS_D", headstone.OthersDecedentList[0].BirthDate.ToString());
            dict.Add("DeathDateS_D", headstone.OthersDecedentList[0].DeathDate.ToString());

            dict.Add("InscriptionS_D", headstone.OthersDecedentList[0].Inscription);
        }

        private void SetSecondPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameS_D_2", headstone.OthersDecedentList[1].FirstName);
            dict.Add("MiddleNameS_D_2", headstone.OthersDecedentList[1].MiddleName);
            dict.Add("LastNameS_D_2", headstone.OthersDecedentList[1].LastName);
            dict.Add("SuffixS_D_2", headstone.OthersDecedentList[1].Suffix);
            dict.Add("LocationS_D_2", headstone.OthersDecedentList[1].Location);

            dict.Add("RankS_D_2", headstone.OthersDecedentList[1].RankList[0]);
            dict.Add("AwardS_D_2", headstone.OthersDecedentList[1].AwardList[0]);
            dict.Add("WarS_D_2", headstone.OthersDecedentList[1].WarList[0]);
            dict.Add("BranchS_D_2", headstone.OthersDecedentList[1].BranchList[0]);

            dict.Add("InscriptionS_D_2", headstone.OthersDecedentList[1].Inscription);

            dict.Add("BirthDateS_D_2", headstone.OthersDecedentList[1].BirthDate.ToString());
            dict.Add("DeathDateS_D_2", headstone.OthersDecedentList[1].DeathDate.ToString());
        }

        private void SetThirdPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameS_D_3", headstone.OthersDecedentList[2].FirstName);
            dict.Add("MiddleNameS_D_3", headstone.OthersDecedentList[2].MiddleName);
            dict.Add("LastNameS_D_3", headstone.OthersDecedentList[2].LastName);
            dict.Add("SuffixS_D_3", headstone.OthersDecedentList[2].Suffix);
            dict.Add("LocationS_D_3", headstone.OthersDecedentList[2].Location);

            dict.Add("RankS_D_3", headstone.OthersDecedentList[2].RankList[0]);
            dict.Add("AwardS_D_3", headstone.OthersDecedentList[2].AwardList[0]);
            dict.Add("WarS_D_3", headstone.OthersDecedentList[2].WarList[0]);
            dict.Add("BranchS_D_3", headstone.OthersDecedentList[2].BranchList[0]);

            dict.Add("InscriptionS_D_3", headstone.OthersDecedentList[2].Inscription);

            dict.Add("BirthDateS_D_3", headstone.OthersDecedentList[2].BirthDate.ToString());
            dict.Add("DeathDateS_D_3", headstone.OthersDecedentList[2].DeathDate.ToString());
        }

        private void SetFourthPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameS_D_4", headstone.OthersDecedentList[3].FirstName);
            dict.Add("MiddleNameS_D_4", headstone.OthersDecedentList[3].MiddleName);
            dict.Add("LastNameS_D_4", headstone.OthersDecedentList[3].LastName);
            dict.Add("SuffixS_D_4", headstone.OthersDecedentList[3].Suffix);
            dict.Add("LocationS_D_4", headstone.OthersDecedentList[3].Location);

            dict.Add("RankS_D_4", headstone.OthersDecedentList[3].RankList[0]);
            dict.Add("AwardS_D_4", headstone.OthersDecedentList[3].AwardList[0]);
            dict.Add("WarS_D_4", headstone.OthersDecedentList[3].WarList[0]);
            dict.Add("BranchS_D_4", headstone.OthersDecedentList[3].BranchList[0]);

            dict.Add("InscriptionS_D_4", headstone.OthersDecedentList[3].Inscription);

            dict.Add("BirthDateS_D_4", headstone.OthersDecedentList[3].BirthDate.ToString());
            dict.Add("DeathDateS_D_4", headstone.OthersDecedentList[3].DeathDate.ToString());
        }

        private void SetFifthPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameS_D_5", headstone.OthersDecedentList[4].FirstName);
            dict.Add("MiddleNameS_D_5", headstone.OthersDecedentList[4].MiddleName);
            dict.Add("LastNameS_D_5", headstone.OthersDecedentList[4].LastName);
            dict.Add("SuffixS_D_5", headstone.OthersDecedentList[4].Suffix);
            dict.Add("LocationS_D_5", headstone.OthersDecedentList[4].Location);

            dict.Add("BirthDateS_D_5", headstone.OthersDecedentList[4].BirthDate.ToString());
            dict.Add("DeathDateS_D_5", headstone.OthersDecedentList[4].DeathDate.ToString());
        }

        private void SetSixthPerson(ref Dictionary<string, string> dict, ref Headstone headstone)
        {
            dict.Add("FirstNameS_D_6", headstone.OthersDecedentList[5].FirstName);
            dict.Add("MiddleNameS_D_6", headstone.OthersDecedentList[5].MiddleName);
            dict.Add("LastNameS_D_6", headstone.OthersDecedentList[5].LastName);
            dict.Add("SuffixS_D_6", headstone.OthersDecedentList[5].Suffix);
            dict.Add("LocationS_D_6", headstone.OthersDecedentList[5].Location);

            dict.Add("BirthDateS_D_6", headstone.OthersDecedentList[5].BirthDate.ToString());
            dict.Add("DeathDateS_D_6", headstone.OthersDecedentList[5].DeathDate.ToString());
        }
    }
}
