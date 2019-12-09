using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;
using ServicesInterfaces;
using Services;
using ViewModelInterfaces;
using System.Collections.Generic;

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
        public void testGoToRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 1;

            string test1 = "-1";
            Assert.IsTrue(reviewWindow.GoToRecord(test1) == false);

            string test2 = "0";
            Assert.IsTrue(reviewWindow.GoToRecord(test2) == false);
            
            string test3 = database.TotalItems.ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(test3) == true);

            string test4 = (database.TotalItems + 1).ToString();
            Assert.IsTrue(reviewWindow.GoToRecord(test4) == false);

            string test5 = "abc5";
            Assert.IsTrue(reviewWindow.GoToRecord(test5) == false);

            string test6 = "";
            Assert.IsTrue(reviewWindow.GoToRecord(test6) == false);
            
            string test7 = "1";
            Assert.IsTrue(reviewWindow.GoToRecord(test7) == true);
        }

        [TestMethod]
        public void testNextRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord(database.TotalItems.ToString());
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == database.TotalItems);

            reviewWindow.GoToRecord("1");
            reviewWindow.NextRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 2);
        }

        [TestMethod]
        public void testPreviousRecord()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            reviewWindow.GoToRecord("1");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);

            reviewWindow.GoToRecord("2");
            reviewWindow.PreviousRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }

        [TestMethod]
        public void testMandatoryInfo()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = @"C:\Users\7405148\Desktop\Section0000P";
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);

            //Gravestone number
            reviewWindow.CurrentPageData.GavestoneNumber = "";
            List<bool> filledInfo = reviewWindow.CheckMandatoryFields();
            Assert.IsTrue(filledInfo[0] == false);
            //reviewWindow.CurrentPageData.GavestoneNumber = "12";
            //filledInfo = reviewWindow.CheckMandatoryFields();
            //Assert.IsTrue(filledInfo[0] == true);

            ////Primary Name
            //reviewWindow.CurrentPageData.PrimaryDecedent.FirstName = "ABC";
            //reviewWindow.CurrentPageData.PrimaryDecedent.LastName = "";
            //filledInfo = reviewWindow.CheckMandatoryFields();
            //Assert.IsTrue(filledInfo[0] == false); 
            //reviewWindow.CurrentPageData.PrimaryDecedent.LastName = "LAST NAME";
            //filledInfo = reviewWindow.CheckMandatoryFields();
            //Assert.IsTrue(filledInfo[0] == true);
            //reviewWindow.CurrentPageData.PrimaryDecedent.clearPerson();
            //Assert.IsTrue(filledInfo[0] == true);

            ////Other Decedents
            //for(int i=0; i<reviewWindow.CurrentPageData.OthersDecedentList.Count; i++)
            //{
            //    reviewWindow.CurrentPageData.OthersDecedentList[i].FirstName = "ABC";
            //    reviewWindow.CurrentPageData.OthersDecedentList[i].LastName = "";
            //    filledInfo = reviewWindow.CheckMandatoryFields();
            //    Assert.IsTrue(filledInfo[i] == false);
            //    reviewWindow.CurrentPageData.OthersDecedentList[0].LastName = "LAST NAME";
            //    filledInfo = reviewWindow.CheckMandatoryFields();
            //    Assert.IsTrue(filledInfo[i] == true);
            //    reviewWindow.CurrentPageData.OthersDecedentList[0].clearPerson();
            //    Assert.IsTrue(filledInfo[i] == true);
            //}

        }
    }
}
