using DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelInterfaces;
using ServicesInterfaces;
using Common;
using System;

namespace ViewModels
{
    public class ReviewWindowVM : IReviewWindowVM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler HeadstoneChanged;

        private int _currentPageIndex;
        private Headstone _currentPageData;
        private IDatabaseService _database;
        
        public Headstone CurrentPageData
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

        public int PageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                if (_currentPageIndex != 0)
                {
                    _database.SetHeadstone(_currentPageIndex, _currentPageData);
                }
                _currentPageIndex = value;
                displayHeadStone();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageIndex)));
            }
        }
        
        private void displayHeadStone()
        {
            CurrentPageData = _database.GetHeadstone(_currentPageIndex);
            HeadstoneChanged?.Invoke(this, new EventArgs());
        }

        public int GetDatabaseCount
        {
            get
            {
                return _database.TotalItems;
            }
        }
        
        public string ImageSource1
        {
            get
            {
                return _database.SectionFilePath + "\\" + Constants.REFERENCED_IMAGE_FOLDER_NAME + 
                    "\\" + CurrentPageData.Image1FileName;
            }
        }

        public string ImageSource2
        {
            get
            {
                return _database.SectionFilePath + "\\" + Constants.REFERENCED_IMAGE_FOLDER_NAME + 
                    "\\" + CurrentPageData.Image2FileName;
            }
        }
        
        public ReviewWindowVM(IDatabaseService database)
        {
            _database = database;
        }
        
        public void SetRecordsToReview()
        {
            PageIndex = 1;

            Trace.WriteLine("Set up records to review: ");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }

        public void NextRecord()
        {
            if (PageIndex == _database.TotalItems)
            {
                return;
            }

            PageIndex++;
            
            Trace.WriteLine("Next click");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }
        
        public void PreviousRecord()
        {
            if (PageIndex == 1)
            {
                return;
            }

            PageIndex--;
            
            Trace.WriteLine("Previous click: ");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }

        public void FirstRecord()
        {
            PageIndex = 1;

            Trace.WriteLine("First Record click: ");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }

        public void LastRecord()
        {
            PageIndex = _database.TotalItems;

            Trace.WriteLine("Last Record click: ");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }

        public bool GoToRecord(string text)
        {
            try
            {
                int page = Convert.ToInt32(text);
                Console.WriteLine("page", page, text);
                PageIndex = page;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EmblemData> GetEmblemData
        {
            get
            {
                return new EmblemData().GetEmblemData;
            }
        }  

        public List<CemeteryNameData> GetCemeteryNames
        {
            get
            {
                return _database.CemeteryNames;
            }
        }

        public List<LocationData> GetLocationData
        {
            get
            {
                return _database.LocationNames;
            }
        }

        public List<BranchData> GetBranchData
        {
            get
            {
                return _database.BranchNames;
            }
        }

        public List<WarData> GetWarData
        {
            get
            {
                return _database.WarNames;
            }
        }

        public List<AwardData> GetAwardData
        {
            get
            {
                return _database.AwardNames;
            }
        }

        public List<bool> CheckMandatoryFields()
        {
            List<bool> isValidList = new List<bool>()
            {
                true, // gravesite number
                true, // primary last name
                true, // descedent 1 last name
                true, // descedent 2 last name
                true, // descedent 3 last name
                true, // descedent 4 last name
                true, // descedent 5 last name
                true  // descedent 6 last name
            };

            if (_currentPageData.GavestoneNumber == "")
            {
                isValidList[0] = false;
            }

            if (_currentPageData.PrimaryDecedent.LastName == "")
            {
                isValidList[1] = false;
            }

            int personIndex = 2;
            foreach (Person person in _currentPageData.OthersDecedentList)
            {
                // A person with contents is valid iff they have a last name
                if (person.containsData() && person.LastName == "")
                {
                    isValidList[personIndex] = false;
                }

                personIndex += 1;
            }

            return isValidList;
        }
    }
}
