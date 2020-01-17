using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;
using ServicesInterfaces;
using Services;
using ViewModelInterfaces;
using System.Collections.Generic;
using DataStructures;
using System.IO;

namespace ViewModelsTest
{
    [TestClass]
    public class ReviewWindowVMTest
    {
        private static string exePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\";
        private string sectionPath = Path.Combine(exePath, "TestDatabases\\Section0000P");

        [TestMethod]
        public void FirstRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.FirstRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void LastRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.LastRecord();
            Assert.IsTrue(reviewWindow.PageIndex == database.TotalItems);
        }

        [TestMethod]
        public void GoToRecord_NegativeInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            Assert.IsTrue(reviewWindow.GoToRecord("-1") == false);
        }

        [TestMethod]
        public void GoToRecord_ZeroRecordNumberInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("0") == false);
        }

        [TestMethod]
        public void GoToRecord_LastRecordNumberInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            string input = database.TotalItems.ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(input) == true);
        }

        [TestMethod]
        public void GoToRecord_MoreThanTotalRecordsInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            string input = (database.TotalItems + 1).ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(input) == false);
        }

        [TestMethod]
        public void GoToRecord_lettersInInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("abc5") == false);
        }

        [TestMethod]
        public void GoToRecord_EmptyInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("") == false);
            
        }

        [TestMethod]
        public void GoToRecord_FirstRecordInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("1") == true);
        }
        
        [TestMethod]
        public void NextRecord_LastRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord(database.TotalItems.ToString());
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == database.TotalItems);
        }

        [TestMethod]
        public void NextRecord_AnyRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            
            reviewWindow.GoToRecord("1");
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 2);
        }

        [TestMethod]
        public void NextRecord_SaveCemeteryName_NextEmpty()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.CemeteryName = "";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.CemeteryName = "TEST";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.CemeteryName == "TEST");
        }

        [TestMethod]
        public void NextRecord_SaveCemeteryName_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.CemeteryName = "ABC";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.CemeteryName = "TEST";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.CemeteryName == "ABC");
        }

        [TestMethod]
        public void NextRecord_SaveSectionNumber()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.BurialSectionNumber = "";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.BurialSectionNumber = "TEST";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.BurialSectionNumber == "TEST");
        }

        [TestMethod]
        public void NextRecord_SaveSectionNumber_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.BurialSectionNumber = "ABC";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.BurialSectionNumber = "TEST";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.BurialSectionNumber == "ABC");
        }

        [TestMethod]
        public void NextRecord_SaveMarkerType()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.MarkerType = "";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.MarkerType = "Flat Marker";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.MarkerType == "Flat Marker");
        }

        [TestMethod]
        public void NextRecord_SaveMarkerType_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.MarkerType = "Upright Headstone";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.MarkerType = "Flat Marker";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.MarkerType == "Upright Headstone");
        }

        [TestMethod]
        public void NextRecord_SaveMarkerType_SomeFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            reviewWindow.GoToRecord("2");
            reviewWindow.CurrentPageData.MarkerType = "Upright Headstone";
            reviewWindow.CurrentPageData.CemeteryName = "";

            reviewWindow.GoToRecord("1");
            reviewWindow.CurrentPageData.MarkerType = "Flat Marker";
            reviewWindow.CurrentPageData.CemeteryName = "TEST";

            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.CurrentPageData.MarkerType == "Upright Headstone");
            Assert.IsTrue(reviewWindow.CurrentPageData.CemeteryName == "TEST");
        }

        [TestMethod]
        public void PreviousRecord_FirstRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord("1");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void PreviousRecord_AnyRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord("2");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void MandatoryInfo_GravestoneNumber()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            Headstone save = reviewWindow.CurrentPageData;
            
            reviewWindow.CurrentPageData.GavestoneNumber = "";
            List<bool> filledInfo = reviewWindow.CheckMandatoryFields();
            Assert.IsTrue(filledInfo[0] == false);

            reviewWindow.CurrentPageData.GavestoneNumber = "12";
            filledInfo = reviewWindow.CheckMandatoryFields();
            Assert.IsTrue(filledInfo[0] == true);

            reviewWindow.CurrentPageData = save;
        }

        [TestMethod]
        public void MandatoryInfo_PrimaryName()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            Headstone save = reviewWindow.CurrentPageData;

            
            reviewWindow.CurrentPageData.PrimaryDecedent.FirstName = "ABC";
            reviewWindow.CurrentPageData.PrimaryDecedent.LastName = "";
            List<bool>  filledInfo = reviewWindow.CheckMandatoryFields();
            Assert.IsTrue(filledInfo[1] == false);

            reviewWindow.CurrentPageData.PrimaryDecedent.LastName = "LAST NAME";
            filledInfo = reviewWindow.CheckMandatoryFields();
            Assert.IsTrue(filledInfo[1] == true);

            reviewWindow.CurrentPageData.PrimaryDecedent.clearPerson();
            Assert.IsTrue(filledInfo[1] == true);

            reviewWindow.CurrentPageData = save;
        }

        [TestMethod]
        public void MandatoryInfo_OthersDecedentList()
        {
            IDatabaseService database = new MicrosoftAccess();
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            Headstone save = reviewWindow.CurrentPageData;
            
            for (int i = 0; i < reviewWindow.CurrentPageData.OthersDecedentList.Count; i++)
            {
                reviewWindow.CurrentPageData.OthersDecedentList[i].FirstName = "ABC";
                reviewWindow.CurrentPageData.OthersDecedentList[i].LastName = "";
                List<bool> filledInfo = reviewWindow.CheckMandatoryFields();
                Assert.IsTrue(filledInfo[i + 2] == false);
                reviewWindow.CurrentPageData.OthersDecedentList[i].LastName = "LAST NAME";
                filledInfo = reviewWindow.CheckMandatoryFields();
                Assert.IsTrue(filledInfo[i + 2] == true);
                reviewWindow.CurrentPageData.OthersDecedentList[i].clearPerson();
                Assert.IsTrue(filledInfo[i + 2] == true);
            }

            reviewWindow.CurrentPageData = save;
        }
    }
}
