using DataStructures;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FileDatabase : IDatabase
    {
        public bool Insert(FieldData data)
        {
            return true;
        }
    }
}
