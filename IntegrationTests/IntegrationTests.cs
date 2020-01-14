using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using ServicesInterfaces;
using ViewModelInterfaces;
using ViewModels;

namespace IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private string exePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\";
        
        [TestMethod]
        public void TestMethod1()
        {
            IDatabaseService database = new MicrosoftAccess();
            string sectionPath = Path.Combine(exePath, "TestDatabases\\Section0000P");
            database.InitDBConnection(sectionPath);
            IReviewWindowVM reviewWindow = new ReviewWindowVM(database);
            reviewWindow.PageIndex = 3;

            reviewWindow.FirstRecord();
            Assert.IsTrue(reviewWindow.PageIndex == 1);
        }
    }
}
