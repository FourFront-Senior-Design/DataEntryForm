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
        bool Insert(FieldData data);
    }
}
