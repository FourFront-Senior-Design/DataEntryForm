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
        public int TotalItems => throw new NotImplementedException();

        public Headstone Get(int index)
        {
            throw new NotImplementedException();
        }

        public void InitDatabaseService(string sectionFilePath)
        {
            throw new NotImplementedException();
        }

        public bool Insert(FieldData data)
        {
            return true;
        }

        public void Insert(int index, Headstone headstone)
        {
            throw new NotImplementedException();
        }
    }
}
