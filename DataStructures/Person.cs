using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Person
    {
        public string FirstName { get; set; }
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
            if (String.IsNullOrEmpty(FirstName) &&
                String.IsNullOrEmpty(MiddleName) &&
                String.IsNullOrEmpty(LastName) &&
                String.IsNullOrEmpty(Suffix) &&
                String.IsNullOrEmpty(Location) &&
                String.IsNullOrEmpty(AwardCustom) &&
                String.IsNullOrEmpty(BranchUnitCustom) &&
                String.IsNullOrEmpty(BranchUnitCustom) &&
                String.IsNullOrEmpty(BirthDate) &&
                String.IsNullOrEmpty(DeathDate) &&
                String.IsNullOrEmpty(Inscription))
            {

            }
            else
            {
                return true;
            }
            
            foreach (string rank in RankList)
            {
                if(!string.IsNullOrEmpty(rank))
                {
                    return true;
                }
            }

            foreach (string award in AwardList)
            {
                if(!string.IsNullOrEmpty(award))
                {
                    return true;
                }
            }

            foreach (string branch in BranchList)
            {
                if(!string.IsNullOrEmpty(branch))
                {
                    return true;
                }
            }

            foreach (string war in WarList)
            {
                if(!string.IsNullOrEmpty(war))
                {
                    return true;
                }
            }

            return false;
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
