using System;
using DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace ServicesTests
{
    [TestClass]
    public class DataBaseServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            DatabaseService databaseService = new DatabaseService();
            databaseService.InitDatabaseService(@"C:\Users\7295201\csc464_465_senior_design\SectionA");


            Headstone headstone = databaseService.Get(1);
        }
    }
}
