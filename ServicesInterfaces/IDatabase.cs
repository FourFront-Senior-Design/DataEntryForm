using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces
{
    public interface IDatabase
    {
        void InitDatabase(string FileName);
        Headstone GetHeadstone(int index);
        void SetHeadstone(int index, Headstone headstoneData);
    }
}
