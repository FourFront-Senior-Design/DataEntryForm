using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces
{
    public interface IDatabaseService
    {
        void Insert(int index, Headstone headstone);
        Headstone Get(int index);
        int TotalItems { get; }
        void InitDatabaseService(string sectionFilePath);
    }
}
