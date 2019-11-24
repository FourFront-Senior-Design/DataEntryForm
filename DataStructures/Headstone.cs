using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Headstone
    {
        public string SequenceID { get; set; }
        string CemeteryName;
        string BurialSectionNumber;
        string WallID;
        string RowNum;
        string GavestoneNumber;
        string MarkerType;
        string Emblem1;
        string Emblem2;

        Person PrimaryDecedent;
        List<Person> OthersDecedentList;

        string Image1FilePath;
        string Image2FilePath;
    }
}
