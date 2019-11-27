using ServicesInterfaces;
using ViewModelInterfaces;
using System.ComponentModel;

namespace ViewModels
{
    public class MainWindowVM : IMainWindowVM, INotifyPropertyChanged
    {
        IDatabaseService _database;
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

        public int LoadData()
        {
            //call database service
            return 0;
        }
      
        public MainWindowVM(IDatabaseService database)
        {
            _database = database;
        }
    }
}
