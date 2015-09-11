using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.ExtensionTests
{
    [TestClass]
    public class JobExtensionTest
    {
        [TestMethod]
        public void ShouldAddPrefixToJobNumberWhenVisualizing()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Number = "000014",
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusId=1 determine this is a job number case (not Internal tracking)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus() { ID = 1, JobID =1, JobStatusId = 1, Active = true } },                    
                    CS_PriceType = new CS_PriceType() { ID = 2, Acronym = "A" },
                    CS_JobType = new CS_JobType() { ID = 3, Description = "Z" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual("AZ000014", numberPrefixed);
        }

        [TestMethod]
        public void ShouldAddPrefixToInternalTrackingWhenVisualizing()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Internal_Tracking = "000000001INT",
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusID=2 determine this is a Internal tracking case (not job number)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus() { ID = 1, JobID = 1, JobStatusId = 2, Active = true } },
                    CS_PriceType = new CS_PriceType() { ID = 2, Acronym = "B" },
                    CS_JobType = new CS_JobType() { ID = 3, Description = "Y" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual("BY000000001INT", numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnEmptyPrefixWhenNumberIsNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Number = string.Empty,
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusID=1 determine this is a job number case (not Internal tracking)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus() { ID=1, JobID=1,JobStatusId = 1, Active = true } },
                    CS_PriceType = new CS_PriceType() { ID = 2, Acronym = "A" },
                    CS_JobType = new CS_JobType() { ID = 3, Description = "Z" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual(string.Empty, numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnEmptyPrefixWhenInternalTrackingIsNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Internal_Tracking = "",
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusID=2 determine this is a Internal tracking case (not job number)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus(){ ID = 1, JobID = 1, JobStatusId = 2, Active = true} },
                    CS_PriceType = new CS_PriceType() { ID = 2, Acronym = "B" },
                    CS_JobType = new CS_JobType() { ID = 3, Description = "Y" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual(string.Empty, numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnEmptyPrefixWhenJobInfoAndNumberAreNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,              
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual(string.Empty, numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnOnlyNumberWhenJobInfoIsNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Number = "000014"
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual("000014", numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnOnlyNumberWhenPriceTypeIsNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Internal_Tracking = "000000001INT",
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusID=2 determine this is a Internal tracking case (not job number)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() {  new CS_Job_JobStatus() { ID =1, JobID=1,JobStatusId = 2}},
                    CS_JobType = new CS_JobType() { ID = 3, Description = "Y" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual("000000001INT", numberPrefixed);
        }

        [TestMethod]
        public void ShouldReturnOnlyNumberWhenJobTypeIsNotProvided()
        {
            //Arrange
            CS_Job newJob = new CS_Job()
            {
                ID = 1,
                Number = "000014",
                CS_JobInfo = new CS_JobInfo()
                {
                    //The JobStatusID=1 determine this is a job number case (not Internal tracking)
                    CS_Job_JobStatus = new System.Data.Objects.DataClasses.EntityCollection<CS_Job_JobStatus>() {  new CS_Job_JobStatus() { ID=1, JobID=1,JobStatusId =1}},
                    CS_PriceType = new CS_PriceType() { ID = 2, Acronym = "A" }
                }
            };

            //Act
            string numberPrefixed = newJob.PrefixedNumber;

            //Assert
            Assert.AreEqual("000014", numberPrefixed);
        }        
    }
}
