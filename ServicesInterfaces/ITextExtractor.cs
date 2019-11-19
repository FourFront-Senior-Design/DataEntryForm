using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServicesInterfaces
{
    public interface ITextExtractor
    {
        bool ReadText(string fileLocation);
    }
}
