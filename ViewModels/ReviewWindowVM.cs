using DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelInterfaces;

namespace ViewModels
{
    public class ReviewWindowVM : IReviewWindowVM, INotifyPropertyChanged
    {
        private List<FieldData> _pageContent;
        private int _currentPageIndex;
        private FieldData _currentPageData;

        public FieldData CurrentPageData
        {
            get
            {
                return _currentPageData;
            }
            set
            {
                _currentPageData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPageData)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool SaveToDatabase()
        {
            return true;
        }

        public void SetImagesToReview(List<FieldData> images)
        {
            _pageContent = images;
            _currentPageIndex = 0;
            CurrentPageData = _pageContent[_currentPageIndex];

            Trace.WriteLine("Location: ");
            Trace.WriteLine(CurrentPageData.FileName);
            Trace.WriteLine(CurrentPageData.PageNumber);

            return;
        }

        public void NextImage()
        {
            Trace.WriteLine("Next click");
            if (_currentPageIndex == _pageContent.Count - 1)
            {
                return;
            }

            _currentPageIndex++;
            CurrentPageData = _pageContent[_currentPageIndex];


            Trace.WriteLine("Location: ");
            Trace.WriteLine(CurrentPageData.FileName);
            Trace.WriteLine(CurrentPageData.PageNumber);

        }

        public void PreviousImage()
        {
            Trace.WriteLine("Previous click");

            if (_currentPageIndex == 0)
            {
                return;
            }

            _currentPageIndex--;
            CurrentPageData = _pageContent[_currentPageIndex];


            Trace.WriteLine("Location: ");
            Trace.WriteLine(CurrentPageData.FileName);
            Trace.WriteLine(CurrentPageData.PageNumber);

        }
    }
}
