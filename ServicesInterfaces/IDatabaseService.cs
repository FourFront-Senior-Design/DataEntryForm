﻿using DataStructures;
using System.Collections.Generic;

namespace ServicesInterfaces
{
    public interface IDatabaseService
    {
        Headstone GetHeadstone(int index);

        void SetHeadstone(int index, Headstone headstone);

        bool InitDBConnection(string sectionFilePath);

        void Close();

        int TotalItems { get; }

        List<CemeteryNameData> CemeteryNames { get; }

        List<EmblemData> EmblemNames { get;  }

        List<LocationData> LocationNames { get; }

        List<BranchData> BranchNames { get; }

        List<WarData> WarNames { get; }

        List<AwardData> AwardNames { get; }

        string SectionFilePath { get; }
    }
}
