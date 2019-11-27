using DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelInterfaces;
using System.Globalization;
using System.Reflection;

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
            if (_currentPageIndex == _pageContent.Count - 1)
            {
                return;
            }

            _currentPageIndex++;
            CurrentPageData = _pageContent[_currentPageIndex];


            Trace.WriteLine("Next click");
            Trace.WriteLine(CurrentPageData.FileName);
            Trace.WriteLine(CurrentPageData.PageNumber);
        }

        public void PreviousImage()
        {
            if (_currentPageIndex == 0)
            {
                return;
            }

            _currentPageIndex--;
            CurrentPageData = _pageContent[_currentPageIndex];


            Trace.WriteLine("Previous click: ");
            Trace.WriteLine(CurrentPageData.FileName);
            Trace.WriteLine(CurrentPageData.PageNumber);
        }

        public List<LocationData> GetLocation
        {
            get
            {
                List<LocationData> locations = new List<LocationData>();

                locations.Add(new LocationData { Abbr = "AL", Name = "Alabama" });
                locations.Add(new LocationData { Abbr = "AK", Name = "Alaska" });
                locations.Add(new LocationData { Abbr = "AR", Name = "Arkansas" });
                locations.Add(new LocationData { Abbr = "AZ", Name = "Arizona" });
                locations.Add(new LocationData { Abbr = "CA", Name = "California" });
                locations.Add(new LocationData { Abbr = "CO", Name = "Colorado" });
                locations.Add(new LocationData { Abbr = "CT", Name = "Connecticut" });
                locations.Add(new LocationData { Abbr = "DC", Name = "District of Columbia" });
                locations.Add(new LocationData { Abbr = "DE", Name = "Delaware" });
                locations.Add(new LocationData { Abbr = "FL", Name = "Florida" });
                locations.Add(new LocationData { Abbr = "GA", Name = "Georgia" });
                locations.Add(new LocationData { Abbr = "HI", Name = "Hawaii" });
                locations.Add(new LocationData { Abbr = "ID", Name = "Idaho" });
                locations.Add(new LocationData { Abbr = "IL", Name = "Illinois" });
                locations.Add(new LocationData { Abbr = "IN", Name = "Indiana" });
                locations.Add(new LocationData { Abbr = "IA", Name = "Iowa" });
                locations.Add(new LocationData { Abbr = "KS", Name = "Kansas" });
                locations.Add(new LocationData { Abbr = "KY", Name = "Kentucky" });
                locations.Add(new LocationData { Abbr = "LA", Name = "Louisiana" });
                locations.Add(new LocationData { Abbr = "ME", Name = "Maine" });
                locations.Add(new LocationData { Abbr = "MD", Name = "Maryland" });
                locations.Add(new LocationData { Abbr = "MA", Name = "Massachusetts" });
                locations.Add(new LocationData { Abbr = "MI", Name = "Michigan" });
                locations.Add(new LocationData { Abbr = "MN", Name = "Minnesota" });
                locations.Add(new LocationData { Abbr = "MS", Name = "Mississippi" });
                locations.Add(new LocationData { Abbr = "MO", Name = "Missouri" });
                locations.Add(new LocationData { Abbr = "MT", Name = "Montana" });
                locations.Add(new LocationData { Abbr = "NE", Name = "Nebraska" });
                locations.Add(new LocationData { Abbr = "NH", Name = "New Hampshire" });
                locations.Add(new LocationData { Abbr = "NJ", Name = "New Jersey" });
                locations.Add(new LocationData { Abbr = "NM", Name = "New Mexico" });
                locations.Add(new LocationData { Abbr = "NY", Name = "New York" });
                locations.Add(new LocationData { Abbr = "NC", Name = "North Carolina" });
                locations.Add(new LocationData { Abbr = "NV", Name = "Nevada" });
                locations.Add(new LocationData { Abbr = "ND", Name = "North Dakota" });
                locations.Add(new LocationData { Abbr = "OH", Name = "Ohio" });
                locations.Add(new LocationData { Abbr = "OK", Name = "Oklahoma" });
                locations.Add(new LocationData { Abbr = "OR", Name = "Oregon" });
                locations.Add(new LocationData { Abbr = "PA", Name = "Pennsylvania" });
                locations.Add(new LocationData { Abbr = "RI", Name = "Rhode Island" });
                locations.Add(new LocationData { Abbr = "SC", Name = "South Carolina" });
                locations.Add(new LocationData { Abbr = "SD", Name = "South Dakota" });
                locations.Add(new LocationData { Abbr = "TN", Name = "Tennessee" });
                locations.Add(new LocationData { Abbr = "TX", Name = "Texas" });
                locations.Add(new LocationData { Abbr = "UT", Name = "Utah" });
                locations.Add(new LocationData { Abbr = "VT", Name = "Vermont" });
                locations.Add(new LocationData { Abbr = "VA", Name = "Virginia" });
                locations.Add(new LocationData { Abbr = "WA", Name = "Washington" });
                locations.Add(new LocationData { Abbr = "WV", Name = "West Virginia" });
                locations.Add(new LocationData { Abbr = "WI", Name = "Wisconsin" });
                locations.Add(new LocationData { Abbr = "WY", Name = "Wyoming" });

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                foreach (CultureInfo culture in cultures)
                {
                    RegionInfo region = new RegionInfo(culture.LCID);

                    LocationData data = new LocationData{ Name = region.EnglishName, Abbr = region.TwoLetterISORegionName };
                    if (!(locations.Contains(data)))
                    {
                        locations.Add(data);
                    }
                }

                for (int i=0; i<locations.Count; i++)
                {
                    locations[i].Abbr = locations[i].Abbr.ToUpper();
                    locations[i].Name = locations[i].Name.ToUpper();
                }
                return locations;
            }
        }

        public List<EmblemData> GetEmblemData
        {
            get
            {                
                List<EmblemData> emblems = new List<EmblemData>();
                emblems.Add(new EmblemData { Photo = "", Name = "00 - UNKNOWN" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-01.jpg", Name = "01 - CHRISTIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "\\emb-02.jpg", Name = "02 - BUDDHIST (Wheel of Righteousness)" });
                emblems.Add(new EmblemData { Photo = "\\emb-03.jpg", Name = "03 - JUDAISM (Star of David)" });
                emblems.Add(new EmblemData { Photo = "\\emb-04.jpg", Name = "04 - PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "\\emb-05.jpg", Name = "05 - RUSSIAN ORTHODOX CROSS" });


                emblems.Add(new EmblemData { Photo = "\\emb-06.jpg", Name = "06 - LUTHERAN CROSS" });
                emblems.Add(new EmblemData { Photo = "\\emb-07.jpg", Name = "07 - EPISCOPAL CROSS" });
                emblems.Add(new EmblemData { Photo = "\\emb-08.jpg", Name = "08 - UNITARIAN CHRUCH" });
                emblems.Add(new EmblemData { Photo = "\\emb-09.jpg", Name = "09 - PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "\\emb-10.jpg", Name = "10 - RUSSIAN ORTHODOX CROSS" });
                return emblems;
            }
        }


    }
}
