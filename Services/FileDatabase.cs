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

        public Headstone GetHeadstone(int index)
        {
            throw new NotImplementedException();
        }

        public bool InitDBConnection(string sectionFilePath)
        {
            throw new NotImplementedException();
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }

        List<CemeteryNameData> GetCemeteryData()
        {
            throw new NotImplementedException();
        }

        List<AwardData> GetAwardData()
        {
            throw new NotImplementedException();
        }

        List<BranchData> GetBranchData()
        {
            throw new NotImplementedException();
        }

        List<EmblemData> GetEmblemData()
        {
            throw new NotImplementedException();
        }


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

        List<CemeteryNameData> IDatabaseService.GetCemeteryData()
        {
            throw new NotImplementedException();
        }

        List<AwardData> IDatabaseService.GetAwardData()
        {
            throw new NotImplementedException();
        }

        List<BranchData> IDatabaseService.GetBranchData()
        {
            throw new NotImplementedException();
        }

        List<EmblemData> IDatabaseService.GetEmblemData()
        {
            throw new NotImplementedException();
        }
    }
}
