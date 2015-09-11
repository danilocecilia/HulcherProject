using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Data;


namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class JobPermitDaoTest
    {
        private const int ROWS_RETURNED = 2;
        private const int JOB_ID = 1;

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TestGetPermitInfoByJob()
        {
            //var newJobPermit = new CS_JobPermit()
            //                       {
            //                           JobID = 1,
            //                           Type = "Test",
            //                           Number = "Test Number",
            //                           Location = "Test Location",
            //                           FileName = "Test FileName",
            //                           Path = "Test Path",
            //                           CreatedBy = "dcecilia",
            //                           CreationDate = DateTime.Now,
            //                           ModifiedBy = "dcecilia",
            //                           ModificationDate = DateTime.Now,
            //                           Active = true
            //                       };

            //JobPermitDao.Singleton.Add(newJobPermit);

            var ListOfJobPermit = JobPermitDao.Singleton.GetPermitInfoByJob(JOB_ID);
            Assert.AreEqual(ListOfJobPermit.Count, ROWS_RETURNED);
        }
    }
}
