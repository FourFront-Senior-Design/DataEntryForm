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
        public Headstone GetHeadstone(int index)
        {
            throw new NotImplementedException();
        }

        public void InitDBConnection(string sectionFilePath)
        {
            throw new NotImplementedException();
        }

        public bool Insert(FieldData data)
        {
            return true;
        }

        public void SetHeadstone(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
    }
}
