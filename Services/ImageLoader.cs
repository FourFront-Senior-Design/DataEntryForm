using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Services
{
    public class ImageLoader : IImageLoader
    {
        public List<String> GetImages(string filePath)
        {
            string[] pathToImage = Directory.GetFiles(filePath, "*.jpg", SearchOption.TopDirectoryOnly);

            if(pathToImage.Length == 0)
            {
                return new List<String>();
            }

            List<String> images = new List<String>();
            foreach(string path in pathToImage)
            {
                images.Add(path.ToString());
            }
            return images;
        }
    }
}
