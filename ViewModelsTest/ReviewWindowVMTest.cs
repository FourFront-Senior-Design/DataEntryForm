using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;
using ServicesInterfaces;
using Services;
using ViewModelInterfaces;
using System.Collections.Generic;
using DataStructures;

namespace ViewModelsTest
{
    [TestClass]
    public class ReviewWindowVMTest
    {
        [TestMethod]
        public void testFirstRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.FirstRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void testLastRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.LastRecord();
            Assert.IsTrue(reviewWindow.PageIndex == database.TotalItems);
        }

        [TestMethod]
        public void testGoToRecord_NegativeInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            Assert.IsTrue(reviewWindow.GoToRecord("-1") == false);
        }

        [TestMethod]
        public void testGoToRecord_ZeroRecordNumberInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("0") == false);
        }

        [TestMethod]
        public void testGoToRecord_LastRecordNumberInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            string input = database.TotalItems.ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(input) == true);
        }

        [TestMethod]
        public void testGoToRecord_MoreThanNumberOfRecordsInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            string input = (database.TotalItems + 1).ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(input) == false);
        }

        [TestMethod]
        public void testGoToRecord_lettersInInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("abc5") == false);
        }

        [TestMethod]
        public void testGoToRecord_EmptyInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("") == false);
            
        }

        [TestMethod]
        public void testGoToRecord_FirstRecordInput()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            
            Assert.IsTrue(reviewWindow.GoToRecord("1") == true);
        }
        
        [TestMethod]
        public void testNextRecord_LastRecordBehavior()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord(database.TotalItems.ToString());
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == database.TotalItems);
        }

        [TestMethod]
        public void testNextRecord_AnyRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            
            reviewWindow.GoToRecord("1");
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 2);
        }

        [TestMethod]
        public void testNextRecord_SaveCemeteryName_NextEmpty()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveCemeteryName_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveSectionNumber()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveSectionNumber_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveMarkerType()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveMarkerType_NextFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testNextRecord_SaveMarkerType_SomeFilled()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testPreviousRecord_FirstRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord("1");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void testPreviousRecord_AnyRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord("2");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void testMandatoryInfo_GravestoneNumber()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testMandatoryInfo_PrimaryName()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
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
        public void testMandatoryInfo_OthersDecedentList()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;
            Headstone save = reviewWindow.CurrentPageData;
            
            for (int i = 0; i < 2; i++)
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
