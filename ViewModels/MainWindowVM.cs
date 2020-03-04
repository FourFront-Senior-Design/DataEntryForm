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
        private bool _reviewButtonEnabled;
        private string _version;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowVM(IDatabaseService database)
        {
            _database = database;
            _version = System.Reflection.AssemblyName.GetAssemblyName("DataEntryForm.exe").Version.ToString();
            ResetWindow();
        }

        public string Copyright
        {
            get
            {
                string copyrightSymbol = "\u00a9";
                return $"Senior Design Data Extraction Project {copyrightSymbol} 2019. Version {_version}";
            }
        }

        public string Title
        {
            get
            {
                return $"Data Entry Form (Verison {_version})";
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            private set
            {
                if (_message != value)
                {
                    _message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
                }
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
                if (_fileLocation != value)
                {
                    _fileLocation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileLocation)));
                }
            }
        }

        public bool ReviewButtonEnabled
        {
            get
            {
                return _reviewButtonEnabled;
            }
            private set
            {
                if(_reviewButtonEnabled != value)
                {
                    _reviewButtonEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReviewButtonEnabled)));
                }
            }
        }

        public bool LoadData()
        {
            if (_database.InitDBConnection(_fileLocation) == false)
            {
                Message = "Invalid Path. Try Again.";
                return false;
            }

            int count = _database.TotalItems;
            if (count == 0)
            {
               Message = "No records found in the Database. Try Again.";
               return false;
            }
            else
            {
               Message = "Successfully loaded " + count.ToString() + " records from the Database.";
               ReviewButtonEnabled = true;
            }
            return true;
        }

        public void SetFilePath(string selectedPath)
        {
            if (selectedPath != string.Empty)
            {
                Message = "";
                FileLocation = selectedPath;
            }
        }

        public void ResetWindow()
        {
            Message = "";
            ReviewButtonEnabled = false;
        }
    }
}

