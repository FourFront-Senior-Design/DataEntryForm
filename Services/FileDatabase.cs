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

        List<string> GetCemeteryNames()
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

        List<string> IDatabaseService.GetCemeteryNames()
        {
            throw new NotImplementedException();
        }
    }
}
