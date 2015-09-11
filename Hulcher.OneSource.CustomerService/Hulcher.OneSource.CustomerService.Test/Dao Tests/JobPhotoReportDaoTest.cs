using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Data;
using System.Collections.Generic;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class JobPhotoReportDaoTest
    {
        JobPhotoReportDao _jobPhotoReportDao;
        [TestInitialize]
        public void Initialize()
        {
             CustomerServiceModelContainer ctx = new CustomerServiceModelContainer(); 
            _jobPhotoReportDao = new JobPhotoReportDao();
            JobPhotoReportDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_JobPhotoReport");
        }

        [TestMethod]
        public void TestGetPhotoReportsByJobID()
        {
            CS_Job jobCreated = JobDao.Singleton.Add(new CS_Job()
            {
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load"
            });

            CS_Job jobCreated2 = JobDao.Singleton.Add(new CS_Job()
            {
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load"
            });

            JobPhotoReportDao.Singleton.Add(new CS_JobPhotoReport(){
                JobID = jobCreated.ID,
                FileName = "test.zip",
                Path = "path",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load"
            });

            JobPhotoReportDao.Singleton.Add(new CS_JobPhotoReport(){
                JobID = jobCreated.ID,
                FileName = "test.zip",
                Path = "path",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load"
            });

            JobPhotoReportDao.Singleton.Add(new CS_JobPhotoReport(){
                JobID = jobCreated2.ID,
                FileName = "test.zip",
                Path = "path",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load"
            });
            IList<CS_JobPhotoReport> photoReportlist = _jobPhotoReportDao.GetJobPhotoReportByJob(jobCreated.ID);

            Assert.AreEqual<int>(2, photoReportlist.Count);
        }
    }
}
