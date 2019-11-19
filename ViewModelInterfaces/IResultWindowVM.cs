using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelInterfaces
{
    public interface IResultWindowVM
    {
        ResultsPageData Data { get; set; }
        void SetResultsPageData();
        List<FieldData> GetLowConfImages();
        List<FieldData> GetHighConfImages();
        bool SaveToDatabase(List<FieldData> data);
    }
}
