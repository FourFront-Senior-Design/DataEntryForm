using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Person
    {
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string Suffix;
        public string Location;
        public List<string> RankList;
        public List<string> AwardList;
        public string AwardCustom;
        public List<string> WarList;
        public List<string> BranchList;
        public string BranchUnitCustom;
        public DateTime BirthDate;
        public DateTime DeathDate;
        public string Inscription;

        public Person()
        {
            RankList = new List<string>();
            AwardList = new List<string>();
            WarList = new List<string>();
            BranchList = new List<string>();
        }
    }
}
