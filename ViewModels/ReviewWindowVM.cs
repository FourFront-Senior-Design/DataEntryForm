using DataStructures;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelInterfaces;
using ServicesInterfaces;
using System.Windows.Input;
using System;

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
                Trace.WriteLine(value.PrimaryDecedent.LastName);
                if(string.IsNullOrEmpty(value.PrimaryDecedent.LastName) || string.IsNullOrWhiteSpace(value.PrimaryDecedent.LastName))
                {
                    return;
                }
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
        
        public void SetRecordsToReview()
        {
            PageIndex = 1;

            Trace.WriteLine("Set up records to review: ");
            Trace.WriteLine(CurrentPageData.PrimaryDecedent.LastName);
        }

        public void NextRecord()
        {
            //if (PageIndex == _database.TotalItems)
            //{
            //    return;
            //}

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

        public List<EmblemData> GetEmblemData
        {
            get
            {                
                List<EmblemData> emblems = new List<EmblemData>();
                emblems.Add(new EmblemData { Photo = "", Code="00", Name = "UNKNOWN" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-01.jpg", Code = "01", Name = "CHRISTIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-02.jpg", Code = "02", Name = "BUDDHIST (Wheel of Righteousness)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-03.jpg", Code = "03", Name = "JUDAISM (Star of David)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-04.jpg", Code = "04", Name = "PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-05.jpg", Code = "05", Name = "RUSSIAN ORTHODOX CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-06.jpg", Code = "06", Name = "LUTHERAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-07.jpg", Code = "07", Name = "EPISCOPAL CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-08.jpg", Code = "08", Name = "UNITARIAN CHRUCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-09.jpg", Code = "09", Name = "PRESBYTERIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-10.jpg", Code = "10", Name = "RUSSIAN ORTHODOX CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-11.jpg", Code = "11", Name = "MORMON (Angel Moroni)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-12.jpg", Code = "12", Name = "NATIVE AMERICAN CHURCH OF NORTH AMERICA" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-13.jpg", Code = "13", Name = "SERBIAN ORTHODOX" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-14.jpg", Code = "14", Name = "GREEK CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-15.jpg", Code = "15", Name = "BAHAI (9 Pointed Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-16.jpg", Code = "16", Name = "ATHEIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-17.jpg", Code = "17", Name = "MUSLIM (Crescent and Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-18.jpg", Code = "18", Name = "HINDU" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-19.jpg", Code = "19", Name = "KONKO-KYO FAITH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-20.jpg", Code = "20", Name = "COMMUNITY OF CHRIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-21.jpg", Code = "21", Name = "SUFISM REORIENTED" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-22.jpg", Code = "22", Name = "TENRIKYO CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-23.jpg", Code = "23", Name = "SEICHO-NO-IE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-24.jpg", Code = "24", Name = "CHURCH OF WORLD MESSIANITY" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-25.jpg", Code = "25", Name = "UNITED CHURCH OF RELIGIOUS SCIENCE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-26.jpg", Code = "26", Name = "CHRISTIAN REFORMED CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-27.jpg", Code = "27", Name = "UNITED MORAVIAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-28.jpg", Code = "28", Name = "ECKANKAR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-29.jpg", Code = "29", Name = "CHRISTIAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-30.jpg", Code = "30", Name = "CHRISTIAN & MISSIONARY ALLIANCE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-31.jpg", Code = "31", Name = "UNITED CHURCH OF CHRIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-32.jpg", Code = "32", Name = "HUMANIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-33.jpg", Code = "33", Name = "PRESBYTERIAN CHURCH (USA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-34.jpg", Code = "34", Name = "IZUMO TAISHAKYO MISSION OF HAWAII" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-35.jpg", Code = "35", Name = "SOKA GAKKAI INTERNATIONAL (USA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-36.jpg", Code = "36", Name = "SIKH (KHANDA)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-37.jpg", Code = "37", Name = "WICCA (Pentacle)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-38.jpg", Code = "38", Name = "LUTHERAN CHURCH MISSOURI SYNOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-39.jpg", Code = "39", Name = "NEW APOSTOLIC CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-40.jpg", Code = "40", Name = "SEVENTH DAY ADVENTIST CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-41.jpg", Code = "41", Name = "CELTIC CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-42.jpg", Code = "42", Name = "ARMENIAN CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-43.jpg", Code = "43", Name = "FAROHAR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-44.jpg", Code = "44", Name = "MESSIANIC JEWISH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-45.jpg", Code = "45", Name = "KOHEN HANDS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-46.jpg", Code = "46", Name = "CATHOLIC CELTIC CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-47.jpg", Code = "47", Name = "CHRISTIAN SCIENTIST (Cross & Crown)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-48.jpg", Code = "48", Name = "MEDICINE WHEEL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-49.jpg", Code = "49", Name = "INFINITY" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-50.jpg", Code = "50", Name = "SOUTHERN CROSS OF HONOR (Confederate States)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-51.jpg", Code = "51", Name = "LUTHER ROSE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-52.jpg", Code = "52", Name = "LANDING EAGLE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-53.jpg", Code = "53", Name = "FOUR DIRECTIONS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-54.jpg", Code = "54", Name = "CHURCH OF NAZARENE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-55.jpg", Code = "55", Name = "HAMMER OF THOR" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-56.jpg", Code = "56", Name = "UNIFICATION CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-57.jpg", Code = "57", Name = "SANDHILL CRANE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-58.jpg", Code = "58", Name = "CHURCH OF GOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-59.jpg", Code = "59", Name = "POMEGRANATE" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-60.jpg", Code = "60", Name = "MESSIANIC" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-61.jpg", Code = "61", Name = "SHINTO" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-62.jpg", Code = "62", Name = "SACRED HEART" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-63.jpg", Code = "63", Name = "AFRICAN ANCESTRAL TRADITIONALIST" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-64.jpg", Code = "64", Name = "MALTESE CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-65.jpg", Code = "65", Name = "DRUID (AWEN)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-66.jpg", Code = "66", Name = "WISCONSIN EVANGELICAL LUTHERAN SYNOD" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-67.jpg", Code = "67", Name = "POLISH NATIONAL CATHOLIC CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-68.jpg", Code = "68", Name = "GUARDIAN ANGEL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-69.jpg", Code = "69", Name = "HEART" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-70.jpg", Code = "70", Name = "SHEPHERD AND FLAG" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-71.jpg", Code = "71", Name = "AFRICAN METHODIST EPISCOPAL" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-72.jpg", Code = "72", Name = "EVANGELICAL LUTHERAN CHURCH" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-73.jpg", Code = "73", Name = "UNIVERSALIST CROSS" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-74.jpg", Code = "74", Name = "FAITH AND PRAYER" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-98.jpg", Code = "98", Name = "MUSLIM (Islamic 5-Pointed Star)" });
                emblems.Add(new EmblemData { Photo = "/ImageTextExtractor;component/Emblems/emb-99.jpg", Code = "99", Name = "NON REQUESTED" });
                return emblems;
            }

            

    }

    }
}
