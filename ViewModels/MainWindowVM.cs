using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ViewModelInterfaces;
using System.ComponentModel;

namespace ViewModels
{
    public class MainWindowVM : IMainWindowVM, INotifyPropertyChanged
    {
        IImageLoader _imageLoader;
        IImageProcessor _imageProcessor;
        List<string> _images;
        private string fileLocation;

        public string FileLocation
        {
            get
            {
                return fileLocation;
            }
            set
            {
                fileLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(fileLocation)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool LoadImages()
        {
            _images = _imageLoader.GetImages(FileLocation);
            return _images.Count() != 0;
        }

        public bool ProcessImages()
        {
            if (_images.Count() == 0)
                return false;

            return _imageProcessor.ProcessImages(_images);
        }

        public MainWindowVM(IImageLoader imageLoader, IImageProcessor imageProcessor)
        {
            _imageLoader = imageLoader;
            _imageProcessor = imageProcessor;
        }
    }
}
