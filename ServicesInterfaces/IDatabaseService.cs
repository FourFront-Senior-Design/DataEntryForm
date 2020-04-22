using DataStructures;
using System.Collections.Generic;

namespace ServicesInterfaces
{
    public interface IDatabaseService
    {
        string SectionFilePath { get; }

        bool InitDBConnection(string sectionFilePath); //opens the connection to the database

        int TotalItems { get; }

        Headstone GetHeadstone(int index);

        void SetHeadstone(int index, Headstone headstone);

        string GetGraveSiteNum(int index);

        void Close(); //closes the connection to the database
        
        /* Gets the data from tables in the database for the dropdown menus*/
        List<CemeteryNameData> CemeteryNames { get; }

        List<EmblemData> EmblemNames { get;  }

        List<LocationData> LocationNames { get; }

        List<BranchData> BranchNames { get; }

        List<WarData> WarNames { get; }

        List<AwardData> AwardNames { get; }
    }
}
