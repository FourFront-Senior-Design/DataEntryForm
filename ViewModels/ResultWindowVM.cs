using ViewModelInterfaces;
using ServicesInterfaces;
using DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace ViewModels
{
    public class ResultWindowVM: IResultWindowVM, INotifyPropertyChanged
    {
        IImageProcessor _imageProcessor;
        IDatabase _database;
        ResultsPageData _data;

        public ResultsPageData Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
     

        public ResultWindowVM(IImageProcessor imageProcessor, IDatabase database)
        {
            _imageProcessor = imageProcessor;
            _database = database;
        }

        public void SetResultsPageData()
        {
            Data = _imageProcessor.GetSummary();
            Trace.WriteLine("Data length (Review Window VM)");
            Trace.WriteLine(Data.Total);
        }

        public List<FieldData> GetHighConfImages()
        {
            return _imageProcessor.GetHighConfImages();
        }

        public List<FieldData> GetLowConfImages()
        {
            return _imageProcessor.GetLowConfImages();
        }

        public bool SaveToDatabase(List<FieldData> fieldData)
        {
            foreach (FieldData data in fieldData)
            {
                if (_database.Insert(data) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
