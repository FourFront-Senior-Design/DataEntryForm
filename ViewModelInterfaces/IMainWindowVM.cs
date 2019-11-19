﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelInterfaces
{
    public interface IMainWindowVM
    {
        string FileLocation { get; set; }
        bool LoadImages();
        bool ProcessImages();
    }
}
