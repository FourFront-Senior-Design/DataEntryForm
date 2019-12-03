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
        Headstone GetHeadstone(int index);

        void SetHeadstone(int index, Headstone headstone);

        bool InitDBConnection(string sectionFilePath);

        int TotalItems { get; }
    }
}
