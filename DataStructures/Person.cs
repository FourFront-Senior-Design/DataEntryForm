using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Person
    {
        public string FirstName{ get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Location { get; set; }
        public List<string> RankList { get; set; }
        public List<string> AwardList { get; set; }
        public string AwardCustom { get; set; }
        public List<string> WarList { get; set; }
        public List<string> BranchList { get; set; }
        public string BranchUnitCustom { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Inscription { get; set; }

        public Person()
        {
            RankList = new List<string>();
            AwardList = new List<string>();
            BranchList = new List<string>();
            WarList = new List<string>();
        }

        public bool containsData()
        {
            if (FirstName == "" &&
                MiddleName == "" &&
                LastName == "" &&
                Suffix == "" &&
                Location == "" &&
                RankList.Count == 0 &&
                AwardList.Count == 0 &&
                AwardList.Count == 0 &&
                AwardCustom == "" &&
                WarList.Count == 0 &&
                BranchList.Count == 0 &&
                BranchUnitCustom == "" &&
                BranchUnitCustom == "" &&
                //BirthDate == "" &&
                //DeathDate == "" &&
                Inscription == "")
            {
                return false;
            }

            return true;
        }

        public void clearPerson()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Suffix = "";
            Location = "";
            RankList.Clear();
            AwardList.Clear();
            AwardList.Clear();
            AwardCustom = "";
            WarList.Clear();
            BranchList.Clear();
            BranchUnitCustom = "";
            BranchUnitCustom = "";
            BirthDate = "";
            DeathDate = "";
            Inscription = "";
        }
    }
}
