using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServicesInterfaces
{
    public interface IImageProcessor
    {
        bool ProcessImages(List<string> imageLoc);
        ResultsPageData GetSummary();
        List<FieldData> GetHighConfImages();
        List<FieldData> GetLowConfImages();
    }
}
