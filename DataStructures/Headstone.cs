using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Headstone
    {
        public string SequenceID;
        public string CemeteryName;
        public string BurialSectionNumber;
        public string WallID;
        public string RowNum;
        public string GavestoneNumber;
        public string MarkerType;
        public string Emblem1;
        public string Emblem2;

        public Person PrimaryDecedent;
        public List<Person> OthersDecedentList;

        public string Image1FilePath;
        public string Image2FilePath;
    }
}
