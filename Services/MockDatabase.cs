/* For testing purposes only */
using DataStructures;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MockDatabase : IDatabaseService
    {
        private List<Headstone> sampleRecords;

        public int TotalItems { get; private set; }

        public List<CemeteryNameData> CemeteryNames { get; private set; }

        public List<EmblemData> EmblemNames { get; private set; }

        public List<LocationData> LocationNames { get; private set; }

        public List<BranchData> BranchNames { get; private set; }

        public List<WarData> WarNames { get; private set; }

        public List<AwardData> AwardNames { get; private set; }

        public string SectionFilePath { get; private set; } = string.Empty;

        public Headstone GetHeadstone(int index)
        {
            return sampleRecords[index];
        }

        public void Close() { }

        public bool InitDBConnection(string sectionFilePath)
        {
            TotalItems = 10;
            sampleRecords = new List<Headstone>();

            for(int i=0; i<TotalItems+1; i++)
            {
                Headstone headstone = new Headstone();
                headstone.CemeteryName = "FT. RICHARDSON NATIONAL CEMETERY";
                headstone.BurialSectionNumber = "BurialSectionNumber";
                headstone.WallID = "2";
                headstone.RowNum = "RowNum";
                headstone.GavestoneNumber = "GravestoneNumber";
                headstone.MarkerType = "Markertype";
                headstone.Emblem1 = "2";
                headstone.Emblem2 = "1";

                Person primary = new Person();
                primary.FirstName = "P_First";
                primary.MiddleName = "P_Middle";
                primary.LastName = "P_Last";
                primary.Suffix = "P_Suffix";
                primary.Location = "P_Location";
                primary.BranchUnitCustom = "P_BranchUnitCustom";
                primary.BirthDate = "P_BirthDate";
                primary.DeathDate = "P_DeathDate";
                primary.Inscription = "P_Inscription";
                primary.AwardCustom = "P_AwardCustom";
                primary.AwardList = new List<string> { "P_A1", "P_A2", "P_A3", "P_A4", "P_A5", "P_A6", "P_A7" };
                primary.WarList = new List<string> { "P_W1", "P_W2", "P_W3", "P_W4" };
                primary.RankList = new List<string> { "P_R1", "P_R2", "P_R3" };
                primary.BranchList = new List<string> { "P_B1", "P_B2", "P_B3" };
                headstone.PrimaryDecedent = primary;

                headstone.OthersDecedentList = new List<Person>();

                Person secondary = new Person();
                secondary.FirstName = "S_First";
                secondary.MiddleName = "S_Middle";
                secondary.LastName = "S_Last";
                secondary.Suffix = "S_Suffix";
                secondary.Location = "S_Location";
                secondary.BranchUnitCustom = "S_BranchUnitCustom";
                secondary.BirthDate = "S_BirthDate";
                secondary.DeathDate = "S_DeathDate";
                secondary.Inscription = "S_Inscription";
                secondary.AwardCustom = "S_AwardCustom";
                secondary.AwardList = new List<string> { "S_A1", "S_A2", "S_A3", "S_A4", "S_A5", "S_A6", "S_A7" };
                secondary.WarList = new List<string> { "S_W1", "S_W2", "S_W3", "S_W4" };
                secondary.RankList = new List<string> { "S_R1", "S_R2", "S_R3" };
                secondary.BranchList = new List<string> { "S_B1", "S_B2", "S_B3" };
                headstone.OthersDecedentList.Add(secondary);

                Person other1 = new Person();
                other1.FirstName = "O1_First";
                other1.MiddleName = "O1_Middle";
                other1.LastName = "O1_Last";
                other1.Suffix = "O1_Suffix";
                other1.Location = "O1_Location";
                other1.BirthDate = "O1_BirthDate";
                other1.DeathDate = "O1_DeathDate";
                other1.Inscription = "O1_Inscription";
                other1.AwardList = new List<string> { "O1_A1" };
                other1.WarList = new List<string> { "O1_W1" };
                other1.RankList = new List<string> { "O1_R1" };
                other1.BranchList = new List<string> { "O1_B1" };
                headstone.OthersDecedentList.Add(other1);

                Person other2 = new Person();
                other2.FirstName = "O2_First";
                other2.MiddleName = "O2_Middle";
                other2.LastName = "O2_Last";
                other2.Suffix = "O2_Suffix";
                other2.Location = "O2_Location";
                other2.BirthDate = "O2_BirthDate";
                other2.DeathDate = "O2_DeathDate";
                other2.Inscription = "O2_Inscription";
                other2.AwardList = new List<string> { "O2_A2" };
                other2.WarList = new List<string> { "O2_W2" };
                other2.RankList = new List<string> { "O2_R2" };
                other2.BranchList = new List<string> { "O2_B2" };
                headstone.OthersDecedentList.Add(other2);

                Person other3 = new Person();
                other3.FirstName = "O3_First";
                other3.MiddleName = "O3_Middle";
                other3.LastName = "O3_Last";
                other3.Suffix = "O3_Suffix";
                other3.Location = "O3_Location";
                other3.BirthDate = "O3_BirthDate";
                other3.DeathDate = "O3_DeathDate";
                other3.Inscription = "O3_Inscription";
                other3.AwardList = new List<string> { "O3_A3" };
                other3.WarList = new List<string> { "O3_W3" };
                other3.RankList = new List<string> { "O3_R3" };
                other3.BranchList = new List<string> { "O3_B3" };
                headstone.OthersDecedentList.Add(other3);

                Person other4 = new Person();
                other4.FirstName = "O4_First";
                other4.MiddleName = "O4_Middle";
                other4.LastName = "O4_Last";
                other4.Suffix = "O4_Suffix";
                other4.Location = "O4_Location";
                other4.BirthDate = "O4_BirthDate";
                other4.DeathDate = "O4_DeathDate";
                headstone.OthersDecedentList.Add(other4);

                Person other5 = new Person();
                other5.FirstName = "O5_First";
                other5.MiddleName = "O5_Middle";
                other5.LastName = "O5_Last";
                other5.Suffix = "O5_Suffix";
                other5.Location = "O5_Location";
                other5.BirthDate = "O5_BirthDate";
                other5.DeathDate = "O5_DeathDate";
                headstone.OthersDecedentList.Add(other5);

                sampleRecords.Add(headstone);
            }
            return true;
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            sampleRecords[index] = headstone;
        }

        public string GetGraveSiteNum(int index)
        {
            return "";
        }
    }
}
