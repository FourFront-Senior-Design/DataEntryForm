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
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public string Inscription { get; set; }

        public Person()
        {
            RankList = new List<string>();
            for (int i = 0; i < 3; i++)
                RankList.Add("");

            AwardList = new List<string>();
            for (int i = 0; i < 7; i++)
                RankList.Add("");

            WarList = new List<string>();
            for (int i = 0; i < 4; i++)
                RankList.Add("");

            BranchList = new List<string>();
            for (int i = 0; i < 3; i++)
                RankList.Add("");
        }
    }
}
