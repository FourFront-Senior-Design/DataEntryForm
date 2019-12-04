using DataStructures;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FileDatabase : IDatabaseService
    {
        public string SectionFilePath => throw new NotImplementedException();

        int IDatabaseService.TotalItems => throw new NotImplementedException();

        public List<CemeteryNameData> CemeteryNames => throw new NotImplementedException();

        public List<EmblemData> EmblemNames => throw new NotImplementedException();

        public List<LocationData> LocationNames => throw new NotImplementedException();

        public List<BranchData> BranchNames => throw new NotImplementedException();

        public List<WarData> WarNames => throw new NotImplementedException();

        public List<AwardData> AwardNames => throw new NotImplementedException();

        Headstone IDatabaseService.GetHeadstone(int index)
        {
            throw new NotImplementedException();
        }

        bool IDatabaseService.InitDBConnection(string sectionFilePath)
        {
            throw new NotImplementedException();
        }

        void IDatabaseService.SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
       
    }
}
