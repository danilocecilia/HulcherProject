using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Test.ExtensionTests
{
    [TestClass]
    public class JobInfoExtensionTest
    {
        [TestMethod]
        public void MustReturnLastJobStatusBasedOnDate()
        {
            //Arrange
            CS_JobInfo info = new CS_JobInfo()
            {
                CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() 
                {
                    new CS_Job_JobStatus()
                    {
                        ID = 1, JobID = 1, JobStatusId = (int)Globals.JobRecord.JobStatus.Active, CreationDate = new DateTime(2011,6,14,1,0,0), ModificationDate = new DateTime(2011,6,14,2,0,0),
                        CS_JobStatus = new CS_JobStatus(){ID = (int)Globals.JobRecord.JobStatus.Active}
                    },
                    new CS_Job_JobStatus()
                    {
                        ID=2, JobID = 1, JobStatusId = (int)Globals.JobRecord.JobStatus.Closed, CreationDate = DateTime.Now, ModificationDate = DateTime.Now, Active = true,
                        CS_JobStatus = new CS_JobStatus(){ ID=(int)Globals.JobRecord.JobStatus.Closed}
                    }
                }
            };
            //Act
            CS_JobStatus status = info.LastJobStatus;
            //Assert
            Assert.AreEqual<int>((int)Globals.JobRecord.JobStatus.Closed, status.ID);
        }

        [TestMethod]
        public void MustReturnNullStatusIfJobInfoStatusIsNull()
        {
            //Arrange
            CS_JobInfo info = new CS_JobInfo()
            {
            };
            //Act
            CS_JobStatus status = info.LastJobStatus;
            //Assert
            Assert.IsNull(status);
        }
    }
}
