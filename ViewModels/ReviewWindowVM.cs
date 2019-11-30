using DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelInterfaces;
using System.Globalization;
using System.Reflection;
using System;
using ServicesInterfaces;

namespace ViewModels
{
    public class ReviewWindowVM : IReviewWindowVM, INotifyPropertyChanged
    {
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
                _currentPageIndex = value;
                CurrentPageData = _database.GetHeadstone(_currentPageIndex);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageIndex)));
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        public ReviewWindowVM(IDatabaseService database)
        {
            _database = database;
        }
        
        public void SetImagesToReview()
        {
            PageIndex = 1;

            Trace.WriteLine("Location: ");
            Trace.WriteLine(CurrentPageData.Image1FilePath);
            Trace.WriteLine(CurrentPageData.CemeteryName);
            Trace.WriteLine(CurrentPageData.WallID);
            Trace.WriteLine(CurrentPageData.Emblem1);

            return;
        }

        public void NextImage()
        {
            //if (_currentPageIndex == _database.TotalItems)
            //{
            //    return;
            //}

            PageIndex++;


            Trace.WriteLine("Next click");
            Trace.WriteLine(CurrentPageData.Image1FilePath);
            Trace.WriteLine(CurrentPageData.CemeteryName);
            Trace.WriteLine(CurrentPageData.WallID);
            Trace.WriteLine(CurrentPageData.Emblem1);
        }

        public void PreviousImage()
        {
            //if (_currentPageIndex == 1)
            //{
            //    return;
            //}

            PageIndex--;


            Trace.WriteLine("Previous click: ");
            Trace.WriteLine(CurrentPageData.Image1FilePath);
            Trace.WriteLine(CurrentPageData.CemeteryName);
            Trace.WriteLine(CurrentPageData.WallID);
            Trace.WriteLine(CurrentPageData.Emblem1);
        }

        public List<LocationData> GetLocation
        {
            get
            {
                List<LocationData> locations = new List<LocationData>();
                
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
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-02.jpg", Name = "02 - BUDDHIST (Wheel of Righteousness)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-03.jpg", Name = "03 - JUDAISM (Star of David)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-04.jpg", Name = "04 - PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-05.jpg", Name = "05 - RUSSIAN ORTHODOX CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-06.jpg", Name = "06 - LUTHERAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-07.jpg", Name = "07 - EPISCOPAL CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-08.jpg", Name = "08 - UNITARIAN CHRUCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-09.jpg", Name = "09 - PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-10.jpg", Name = "10 - RUSSIAN ORTHODOX CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-11.jpg", Name = "11 - MORMON (Angel Moroni)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-12.jpg", Name = "12 - NATIVE AMERICAN CHURCH OF NORTH AMERICA" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-13.jpg", Name = "13 - SERBIAN ORTHODOX" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-14.jpg", Name = "14 - GREEK CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-15.jpg", Name = "15 - BAHAI (9 Pointed Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-16.jpg", Name = "16 - ATHEIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-17.jpg", Name = "17 - MUSLIM (Crescent and Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-18.jpg", Name = "18 - HINDU" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-19.jpg", Name = "19 - KONKO-KYO FAITH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-20.jpg", Name = "20 - COMMUNITY OF CHRIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-21.jpg", Name = "21 - SUFISM REORIENTED" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-22.jpg", Name = "22 - TENRIKYO CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-23.jpg", Name = "23 - SEICHO-NO-IE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-24.jpg", Name = "24 - CHURCH OF WORLD MESSIANITY" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-25.jpg", Name = "25 - UNITED CHURCH OF RELIGIOUS SCIENCE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-26.jpg", Name = "26 - CHRISTIAN REFORMED CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-27.jpg", Name = "27 - UNITED MORAVIAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-28.jpg", Name = "28 - ECKANKAR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-29.jpg", Name = "29 - CHRISTIAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-30.jpg", Name = "30 - CHRISTIAN & MISSIONARY ALLIANCE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-31.jpg", Name = "31 - UNITED CHURCH OF CHRIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-32.jpg", Name = "32 - HUMANIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-33.jpg", Name = "33 - PRESBYTERIAN CHURCH (USA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-34.jpg", Name = "34 - IZUMO TAISHAKYO MISSION OF HAWAII" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-35.jpg", Name = "35 - SOKA GAKKAI INTERNATIONAL (USA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-36.jpg", Name = "36 - SIKH (KHANDA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-37.jpg", Name = "37 - WICCA (Pentacle)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-38.jpg", Name = "38 - LUTHERAN CHURCH MISSOURI SYNOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-39.jpg", Name = "39 - NEW APOSTOLIC CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-40.jpg", Name = "40 - SEVENTH DAY ADVENTIST CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-41.jpg", Name = "41 - CELTIC CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-42.jpg", Name = "42 - ARMENIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-43.jpg", Name = "43 - FAROHAR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-44.jpg", Name = "44 - MESSIANIC JEWISH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-45.jpg", Name = "45 - KOHEN HANDS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-46.jpg", Name = "46 - CATHOLIC CELTIC CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-47.jpg", Name = "47 - CHRISTIAN SCIENTIST (Cross & Crown)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-48.jpg", Name = "48 - MEDICINE WHEEL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-49.jpg", Name = "49 - INFINITY" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-50.jpg", Name = "50 - SOUTHERN CROSS OF HONOR (Confederate States)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-51.jpg", Name = "51 - LUTHER ROSE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-52.jpg", Name = "52 - LANDING EAGLE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-53.jpg", Name = "53 - FOUR DIRECTIONS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-54.jpg", Name = "54 - CHURCH OF NAZARENE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-55.jpg", Name = "55 - HAMMER OF THOR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-56.jpg", Name = "56 - UNIFICATION CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-57.jpg", Name = "57 - SANDHILL CRANE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-58.jpg", Name = "58 - CHURCH OF GOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-59.jpg", Name = "59 - POMEGRANATE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-60.jpg", Name = "60 - MESSIANIC" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-61.jpg", Name = "61 - SHINTO" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-62.jpg", Name = "62 - SACRED HEART" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-63.jpg", Name = "63 - AFRICAN ANCESTRAL TRADITIONALIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-64.jpg", Name = "64 - MALTESE CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-65.jpg", Name = "65 - DRUID (AWEN)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-66.jpg", Name = "66 - WISCONSIN EVANGELICAL LUTHERAN SYNOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-67.jpg", Name = "67 - POLISH NATIONAL CATHOLIC CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-68.jpg", Name = "68 - GUARDIAN ANGEL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-69.jpg", Name = "69 - HEART" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-70.jpg", Name = "70 - SHEPHERD AND FLAG" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-71.jpg", Name = "71 - AFRICAN METHODIST EPISCOPAL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-72.jpg", Name = "72 - EVANGELICAL LUTHERAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-73.jpg", Name = "73 - UNIVERSALIST CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-74.jpg", Name = "74 - FAITH AND PRAYER" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-98.jpg", Name = "98 - MUSLIM (Islamic 5-Pointed Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-99.jpg", Name = "99 - NON REQUESTED" });
                return emblems;
            }


        }

    }
}
