using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Data;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class EmployeeInfoDaoTest
    {
        private CS_Country country;
        private CS_State state;
        private CS_Division division;
        private CS_Employee employee1;
        private CS_Employee employee2;
        private CS_JobStatus jobStatus;
        private CS_PriceType priceType;
        private CS_JobCategory jobCategory;
        private CS_JobType jobType;
        private CS_JobAction jobAction;
        private CS_Job job;
        private CS_JobInfo jobInfo;
        private CS_Resource resource;

        [TestInitialize]
        public void Initialize()
        {
            // Step 1 - Clear Tables
            JobInfoDao.Singleton.ClearAll();
            ResourceDao.Singleton.ClearAll();
            EmployeeDao.Singleton.ClearAll();

            // Step 2 - Adding controlled data
            country = CountryDao.Singleton.Add(
                new CS_Country()
                {
                    Active = true,
                    Name = "USA",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            state = StateDao.Singleton.Add(
                new CS_State()
                {
                    Active = true,
                    Acronym = "TX",
                    Name = "Texas",
                    CountryID = country.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            division = DivisionDao.Singleton.Add(
                new CS_Division()
                {
                    Active = true,
                    CountryID = country.ID,
                    Description = "DIV1",
                    StateID = state.ID,
                    Name = "001",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            employee1 = EmployeeDao.Singleton.Add(
                new CS_Employee()
                {
                    Active = true,
                    FirstName = "a",
                    Name = "b",
                    DivisionID = division.ID,
                    BusinessCardTitle = "Laborer",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            employee2 = EmployeeDao.Singleton.Add(
                new CS_Employee()
                {
                    Active = true,
                    FirstName = "c",
                    Name = "d",
                    DivisionID = division.ID,
                    BusinessCardTitle = "Regional Vice President",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            jobStatus = JobStatusDao.Singleton.Add(
                new CS_JobStatus()
                {
                    Active = true,
                    Description = "Active",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            priceType = PriceTypeDao.Singleton.Add(
                new CS_PriceType()
                {
                    Active = true,
                    Acronym = "X",
                    Description = "X",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            jobCategory = JobCategoryDao.Singleton.Add(
                new CS_JobCategory()
                {
                    Active = true,
                    Description = "X",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            jobType = JobTypeDao.Singleton.Add(
                new CS_JobType()
                {
                    Active = true,
                    Description = "X",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            jobAction = JobActionDao.Singleton.Add(
                new CS_JobAction()
                {
                    Active = true,
                    Description = "X",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            job = JobDao.Singleton.Add(
                new CS_Job()
                {
                    Active = true,
                    Number = "000001",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
            jobInfo = JobInfoDao.Singleton.Add(
                new CS_JobInfo()
                {
                    Active = true,
                    JobID = job.ID,
                    JobStatusID = jobStatus.ID,
                    PriceTypeID = priceType.ID,
                    JobCategoryID = jobCategory.ID,
                    JobTypeID = jobType.ID,
                    JobActionID = jobAction.ID,
                    InitialCallDate = DateTime.Now,
                    InitialCallTime = new TimeSpan(0, 1, 10, 0, 0),
                    InterimBill = false,
                    ProjectManager = employee2.ID,
                    EmployeeID = employee2.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });

            resource = ResourceDao.Singleton.Add(
                new CS_Resource()
                {
                    Active = true,
                    EmployeeID = employee1.ID,
                    JobID = job.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now
                });
        }

        [TestMethod]
        public void TestListAll()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListAll();
            Assert.AreEqual(2, returnList.Count);
            if (returnList[0].EmployeeId.Equals(employee1.ID))
            {
                Assert.AreEqual("Assigned", returnList[0].Assigned);
                Assert.AreEqual("Available", returnList[1].Assigned);
            }
            else
            {
                Assert.AreEqual("Available", returnList[0].Assigned);
                Assert.AreEqual("Assigned", returnList[1].Assigned);
            }
        }

        [TestMethod]
        public void TestListFilteredByDivision()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByDivision(new string[] { "001" });
            Assert.AreEqual(2, returnList.Count);
            if (returnList[0].EmployeeId.Equals(employee1.ID))
            {
                Assert.AreEqual("Assigned", returnList[0].Assigned);
                Assert.AreEqual("Available", returnList[1].Assigned);
            }
            else
            {
                Assert.AreEqual("Available", returnList[0].Assigned);
                Assert.AreEqual("Assigned", returnList[1].Assigned);
            }
        }

        [TestMethod]
        public void TestListFilteredByDivisionState()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByDivisionState(new string[] { "Texas" });
            Assert.AreEqual(2, returnList.Count);
            if (returnList[0].EmployeeId.Equals(employee1.ID))
            {
                Assert.AreEqual("Assigned", returnList[0].Assigned);
                Assert.AreEqual("Available", returnList[1].Assigned);
            }
            else
            {
                Assert.AreEqual("Available", returnList[0].Assigned);
                Assert.AreEqual("Assigned", returnList[1].Assigned);
            }
        }

        [TestMethod]
        public void TestListFilteredByStatus()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByStatus(new string[] { "Assigned" });
            Assert.AreEqual(1, returnList.Count);
            Assert.AreEqual("Assigned", returnList[0].Assigned);
        }

        [TestMethod]
        public void TestFilteredByJobNumber()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByJobNumber(new string[] { "000001" });
            Assert.AreEqual(1, returnList.Count);
            Assert.AreEqual("Assigned", returnList[0].Assigned);
        }

        [TestMethod]
        public void TestFilteredByPosition()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByPosition(new string[] { "Laborer" });
            Assert.AreEqual(1, returnList.Count);
            Assert.AreEqual("Assigned", returnList[0].Assigned);
        }

        [TestMethod]
        public void TestFilteredByEmployee()
        {
            // Step 3 - Testing the view
            IList<CS_View_EmployeeInfo> returnList = EmployeeInfoDao.Singleton.ListFilteredByEmployee(new string[] { "a b" });
            Assert.AreEqual(1, returnList.Count);
            Assert.AreEqual("Assigned", returnList[0].Assigned);
        }
    }
}
