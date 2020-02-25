using ServicesInterfaces;
using ViewModelInterfaces;
using System.ComponentModel;

namespace ViewModels
{
    public class MainWindowVM : IMainWindowVM, INotifyPropertyChanged
    {
        private IDatabaseService _database;
        private string _fileLocation;
        private string _message;
        private bool _enableExtract = false;

        public string Copyright
        {
            get
            {
                return "Senior Design Data Extraction Project" + "\u00a9" + "2019. Version 1.4";
            }
        }
        
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileLocation)));
            }
        }

        public bool EnableExtract
        {
            get
            {
                return _enableExtract;
            }
            set
            {
                _enableExtract = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnableExtract)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int LoadData()
        {
            if (_database.InitDBConnection(_fileLocation) == false)
                return -1;
            return _database.TotalItems;
        }
      
        public MainWindowVM(IDatabaseService database)
        {
            _database = database;
        }
    }
}

