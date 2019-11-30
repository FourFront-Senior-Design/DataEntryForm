using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        bool EnableExtract { get; set; }
        string FileLocation { get; set; }
        int LoadData();
    }
}
