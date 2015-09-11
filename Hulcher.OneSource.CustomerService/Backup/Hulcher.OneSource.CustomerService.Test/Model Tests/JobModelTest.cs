using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Data.Schema.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class JobModelTest
    {
        [TestMethod]
        public void TestSaveOperationNotAllowed()
        {
            //Arrange
            FakeObjectSet<CS_JobStatus> fakeStatusList = new FakeObjectSet<CS_JobStatus>();
            fakeStatusList.AddObject(new CS_JobStatus() { Active = true, ID = 2, Description = "Preset" });
            FakeObjectSet<CS_Job> fakeJobList = new FakeObjectSet<CS_Job>();
            fakeJobList.AddObject(
                new CS_Job()
                { 
                    Active = true, ID = 1, 
                    CS_CustomerInfo = new CS_CustomerInfo()
                    {
                        Active = true, CustomerId = 1
                    },
                    CS_JobInfo = new CS_JobInfo() 
                    { 
                        JobID = 1, Active= true, 
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() { 
                            new CS_Job_JobStatus(){ 
                                ID = 1, JobID = 1, JobStatusId = 2, Active= true 
                            } 
                        } 
                    } 
                }
            );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_JobStatus>()).Returns(fakeStatusList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobList);
            JobModel model = new JobModel(mockUnitOfWork.Object)
            { 
                JobStatusID = 1, 
                NewJob = new CS_Job() 
                { 
                    Active = true, ID = 1,
                    CS_JobInfo = new CS_JobInfo() 
                    { 
                        JobID = 1, Active= true, 
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() { 
                            new CS_Job_JobStatus() { 
                                ID = 1, JobID = 1, JobStatusId = 1, Active= true 
                            } 
                        } 
                    } 
                },
                NewCustomer = new CS_CustomerInfo()
                {
                    Active = true,
                    CustomerId = 1
                }                    
            };

            //Act
            IDictionary<string, object> output = model.UpdateJobData(false, true);

            //Assert
            Assert.IsTrue(output.ContainsKey("OperationNotAllowed"));
            Assert.IsTrue(bool.Parse(output["OperationNotAllowed"].ToString()));
            Assert.AreEqual("Cannot update Job Status to Active, because the previous status is Preset.", output["Message"].ToString());
        }

        [TestMethod]
        public void TestGetJobInfoForCallEntry()
        {
            //Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            //Act
            CS_Job result = model.GetJobInfoForCallEntry(1);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCallEntryFilter()
        {
            //Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            //Act
            IList<CS_Job> resultList1 = model.CallEntryFilter(Globals.JobRecord.CallEntryFilter.Customer, "customer");
            IList<CS_Job> resultList2 = model.CallEntryFilter(Globals.JobRecord.CallEntryFilter.Division, "division");
            IList<CS_Job> resultList3 = model.CallEntryFilter(Globals.JobRecord.CallEntryFilter.JobNumber, "number");
            IList<CS_Job> resultList4 = model.CallEntryFilter(Globals.JobRecord.CallEntryFilter.Location, "new");
            //Assert
            Assert.AreEqual(1, resultList1.Count);
            Assert.AreEqual(1, resultList2.Count);
            Assert.AreEqual(3, resultList3.Count);
            Assert.AreEqual(1, resultList4.Count);
        }

        [TestMethod]
        public void TestListAllFilteredPriceTypesByName()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_PriceType> result = model.ListAllFilteredPriceTypesByName("A");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestListAllJobStatusByDescription()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_JobStatus> result = model.ListJobStatusByDescription("status2");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestListAllJobActionByName()
        {
            JobModel jobModel = new JobModel(new FakeUnitOfWork());

            IList<CS_JobAction> result = jobModel.ListAllJobActionByName("Tran");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestGetJobCategoryByJobAction()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            CS_JobCategory result = model.GetJobCategoryByJobAction(1);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetJobTypeByJobAction()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            CS_JobType result = model.GetJobTypeByJobAction(1);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindJobSummaryModificationDate()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, null, null, Globals.Dashboard.DateFilterType.ModificationDate, DateTime.MinValue, DateTime.Now,"");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryPresetDate()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, null, null, Globals.Dashboard.DateFilterType.PresetDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryInitialCallDate()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, null, null, Globals.Dashboard.DateFilterType.InitialCallDate, new DateTime(2011, 1, 1), new DateTime(2011, 1, 31), "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryJobStartDate()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, null, null, Globals.Dashboard.DateFilterType.JobStartDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryJobStatus()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(1, null, null, null, Globals.Dashboard.DateFilterType.ModificationDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryJobId()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, 2, null, null, Globals.Dashboard.DateFilterType.ModificationDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryDivision()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, 2, null, Globals.Dashboard.DateFilterType.ModificationDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryCustomer()
        {
            // Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());
            // Act
            IList<CS_SP_GetJobSummary_Result> result = model.FindJobSummary(null, null, null, 1, Globals.Dashboard.DateFilterType.ModificationDate, DateTime.MinValue, DateTime.Now, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void MustReturnJobWhenInternalTrackingIsFilled()
        {
            //Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());

            //Act
            CS_Job jobResult = model.GetJobByNumber("12345daniloInternalTracking");

            //Assert
            Assert.IsNotNull(jobResult);
            Assert.AreEqual(9, jobResult.ID);
        }

        [TestMethod]
        public void MustReturnJobWhenJobNumberIsFilled()
        {
            //Arrange
            JobModel model = new JobModel(new FakeUnitOfWork());

            //Act
            CS_Job jobResult = model.GetJobByNumber("12345daniloJobNumber");

            //Assert
            Assert.IsNotNull(jobResult);
            Assert.AreEqual(10, jobResult.ID);
        }

        [TestMethod]
        public void GenerateEmailBodyForInvoicingTeamTest()
        {
            DateTime dt = new DateTime(2011, 02, 14);

            TimeSpan timeSpan = new TimeSpan(10, 11, 59);


            CS_Country country = new CS_Country()
                                     {
                                         ID = 1,
                                         Active = true,
                                         Name = "USA"

                                     };

            CS_State state = new CS_State()
                                 {
                                     ID = 1,
                                     Active = true,
                                     Name = "Texas"
                                 };

            CS_City city = new CS_City()
                               {
                                   ID = 1,
                                   Active = true,
                                   Name = "Dalton"
                               };

            CS_LocationInfo locationInfo = new CS_LocationInfo()
                                            {
                                                Active = true,
                                                CountryID = 1,
                                                StateID = 1,
                                                CityID = 1,
                                                CS_Country = country,
                                                CS_State = state,
                                                CS_City = city
                                            };


            CS_Frequency frequency = new CS_Frequency()
                                         {
                                             Active = true,
                                             ID = 1,
                                             Description = "D"
                                         };

            CS_JobDescription csJobDescription = new CS_JobDescription()
            {
                Active = true,
                NumberEmpties = 1,
                NumberLoads = 2,
                NumberEngines = 1
            };


            CS_Division division = new CS_Division()
            {
                ID = 241,
                Active = true,
                Name = "005",
                Description = "White River, Ontario"
            };

            CS_JobDivision jobdivision = new CS_JobDivision()
            {
                Active = true,
                JobID = 243,
                DivisionID = 241,
                CS_Division = division
            };

            CS_Employee employee = new CS_Employee()
            {
                ID = 1,
                Active = true,
                Name = "Dcecilia",
                FirstName = "Test",
                DivisionID = 241
            };

            CS_Reserve reserve = new CS_Reserve()
            {
                Active = true,
                JobID = 243,
                Type = 2,
                CS_Employee = employee,
                DivisionID = 241
            };

            EntityCollection<CS_JobDivision> JobDivision = new EntityCollection<CS_JobDivision>();

            JobDivision.Add(jobdivision);

            CS_ScopeOfWork csScopeOfWork = new CS_ScopeOfWork()
            {
                Active = true,
                ScopeOfWork = "xxcxcxc",
                JobId = 243
            };

            EntityCollection<CS_Reserve> csReserves = new EntityCollection<CS_Reserve>();
            csReserves.Add(reserve);

            EntityCollection<CS_ScopeOfWork> scopeOfWorks = new EntityCollection<CS_ScopeOfWork>();
            scopeOfWorks.Add(csScopeOfWork);

            //Arrange
            FakeObjectSet<CS_Job> fakeJobObject = new FakeObjectSet<CS_Job>();
            fakeJobObject.AddObject
                (
                    new CS_Job()
                    {
                        ID = 243,
                        Active = true,
                        CreatedBy = "rbrandao",
                        CreationDate = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        ModifiedBy = "Load",
                        //Internal_Tracking = "000000025INT",
                        Number = "000243",
                        CS_ScopeOfWork = scopeOfWorks,
                        CS_JobDivision = JobDivision,
                        CS_Reserve = csReserves,
                        CS_CustomerInfo = new CS_CustomerInfo()
                        {
                            Active = true,
                            CS_Customer = new CS_Customer()
                            {
                                Active = true,
                                Name = "American Test"
                            },
                            CS_Division = division,
                            CS_Contact1 = new CS_Contact()
                            {
                                ID = 1,
                                Active = true,
                                Name = "danilo",
                                LastName = "cecilia",
                            },
                            CS_Contact3 = new CS_Contact()
                                             {
                                                 ID = 1,
                                                 Active = true,
                                                 Name = "danilo",
                                                 LastName = "cecilia",
                                             },
                            //IsCustomer = true,
                            InitialCustomerContactId = 1,
                            BillToContactId = 1

                        },
                        CS_JobInfo = new CS_JobInfo()
                        {
                            Active = true,
                            InterimBill = true,
                            CS_Employee = employee,
                            EmployeeID = employee.ID,
                            CS_Frequency = frequency,
                            FrequencyID = 1,
                            CS_JobAction = new CS_JobAction()
                            {
                                Active = true,
                                Description = "Environmental Work, General - Undefined Scope of Work"
                            },
                            CS_JobType = new CS_JobType()
                            {
                                Active = true,
                                Description = "A"
                            },
                            InitialCallDate = dt,
                            InitialCallTime = timeSpan,
                            CS_PriceType = new CS_PriceType()
                                               {
                                                   Active = true,
                                                   Acronym = "P",
                                                   Description = "description test"
                                               },
                            CS_JobCategory = new CS_JobCategory()
                                                {
                                                    Active = true,
                                                    Description = "B"
                                                },
                            CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                            {
                                new CS_Job_JobStatus()
                                {
                                    Active = true,
                                    JobStatusId = (int)Globals.JobRecord.JobStatus.Active,
                                    JobStartDate = new DateTime(2011,02,14),
                                    JobCloseDate = new DateTime(2011,02,14)
                                }
                            }

                        },
                        CS_LocationInfo = locationInfo,
                        CS_JobDescription = csJobDescription
                    }
            );

            //Act
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobObject);

            JobModel jobModel = new JobModel(mockUnitOfWork.Object);

            string body = jobModel.GenerateEmailBodyForInvoicingTeam(243);

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job#:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" PA000243");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Customer:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" American Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Customer Contact:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" danilo");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Bill to:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" cecilia, danilo");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Call date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 02/14/2011");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Call time:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 10:11:59");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Price Type:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" description test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Action:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Environmental Work, General - Undefined Scope of Work");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Category:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" B");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Type:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" A");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Division:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 005");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Interim Bill:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Yes");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Requested By:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dcecilia, Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Frequency:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" D");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Country:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" USA");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("State:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Texas");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("City:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dalton");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Engines:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Loads:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 2");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Empties:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Scope Of Work:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("xxcxcxc");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job start date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 02/14/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job end date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 02/14/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");

            //Assert
            Assert.AreEqual(sb.ToString(), body);
        }

        [TestMethod]
        public void GenerateEmailBodyForEstimationTeamTest()
        {
            DateTime dt = new DateTime(2011, 02, 14);

            CS_JobDescription csJobDescription = new CS_JobDescription()
                                                     {
                                                         Active = true,
                                                         NumberEmpties = 1,
                                                         NumberLoads = 2,
                                                         NumberEngines = 1
                                                     };


            CS_Division division = new CS_Division()
                                       {
                                           ID = 241,
                                           Active = true,
                                           Name = "005",
                                           Description = "White River, Ontario"
                                       };

            CS_JobDivision jobdivision = new CS_JobDivision()
                                          {
                                              Active = true,
                                              JobID = 243,
                                              DivisionID = 241,
                                              CS_Division = division
                                          };

            CS_Employee employee = new CS_Employee()
                                       {
                                           Active = true,
                                           Name = "Dcecilia",
                                           FirstName = "Test",
                                           DivisionID = 241
                                       };

            CS_Reserve reserve = new CS_Reserve()
                                     {
                                         Active = true,
                                         JobID = 243,
                                         Type = 2,
                                         CS_Employee = employee,
                                         DivisionID = 241
                                     };

            EntityCollection<CS_JobDivision> JobDivision = new EntityCollection<CS_JobDivision>();

            JobDivision.Add(jobdivision);

            CS_ScopeOfWork csScopeOfWork = new CS_ScopeOfWork()
            {
                Active = true,
                ScopeOfWork = "xxcxcxc",
                JobId = 243
            };

            EntityCollection<CS_Reserve> csReserves = new EntityCollection<CS_Reserve>();
            csReserves.Add(reserve);

            EntityCollection<CS_ScopeOfWork> scopeOfWorks = new EntityCollection<CS_ScopeOfWork>();
            scopeOfWorks.Add(csScopeOfWork);

            //Arrange
            FakeObjectSet<CS_Job> fakeJobObject = new FakeObjectSet<CS_Job>();
            fakeJobObject.AddObject
                (
                    new CS_Job()
                        {
                            ID = 243,
                            Active = true,
                            CreatedBy = "rbrandao",
                            CreationDate = DateTime.Now,
                            ModificationDate = DateTime.Now,
                            ModifiedBy = "Load",
                            Internal_Tracking = "000000025INT",
                            CS_ScopeOfWork = scopeOfWorks,
                            CS_JobDivision = JobDivision,
                            CS_Reserve = csReserves,
                            CS_CustomerInfo = new CS_CustomerInfo()
                                                  {
                                                      Active = true,
                                                      CS_Customer = new CS_Customer()
                                                                        {
                                                                            Active = true,
                                                                            Name = "Test Customer"
                                                                        },
                                                      CS_Division = division
                                                  },
                            CS_JobInfo = new CS_JobInfo()
                                             {
                                                 Active = true,
                                                 CS_JobAction = new CS_JobAction()
                                                                    {
                                                                        Active = true,
                                                                        Description = "Environmental Work, General - Undefined Scope of Work"
                                                                    },
                                                 CS_JobType = new CS_JobType()
                                                                    {
                                                                        Active = true,
                                                                        Description = "A"
                                                                    },
                                                 CS_PriceType = new CS_PriceType()
                                                 {
                                                     Active = true,
                                                     Acronym = "P"
                                                 },
                                                CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                                                                    {
                                                                        new CS_Job_JobStatus()
                                                                        {
                                                                            JobStatusId = (int)Globals.JobRecord.JobStatus.Bid,
                                                                            JobStartDate = new DateTime(2011,02,14),
                                                                            Active = true
                                                                        }
                                                                    }


                                             },
                            CS_JobDescription = csJobDescription
                        }
            );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobObject);

            JobModel jobModel = new JobModel(mockUnitOfWork.Object);

            string body = jobModel.GenerateEmailBodyForEstimationTeam(243);

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Proposal#:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" ##");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job#:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" PA000000025INT");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Customer:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Test Customer");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Division:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 005");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("JobType:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" A");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("JobAction:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Environmental Work, General - Undefined Scope of Work");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Scope Of Work:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("xxcxcxc");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job start date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("02/14/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Employee:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dcecilia, Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Engines:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Loads:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 2");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Empties:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");

            Assert.AreEqual(sb.ToString(), body);
        }

        [TestMethod]
        public void GenerateEmailSubjectEstimationTeam()
        {
            //Arrange 
            FakeObjectSet<CS_Job> fakeJobObject = new FakeObjectSet<CS_Job>();
            fakeJobObject.AddObject(
                new CS_Job
                {
                    Active = true,
                    ID = 243,
                    Number = "000123",
                    CS_CustomerInfo = new CS_CustomerInfo
                    {
                        Active = true,
                        CS_Customer = new CS_Customer()
                        {
                            Active = true,
                            Name = "Customer Test"
                        }
                    },
                    CS_JobInfo = new CS_JobInfo
                    {
                        CS_JobType = new CS_JobType() { ID = 1, Description = "A", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Acronym = "P", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus { JobStatusId = 1, JobID = 243, Active = true }
                        }
                    }
                }
                );

            //Act
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobObject);

            JobModel jobModel = new JobModel(mockUnitOfWork.Object);

            string subject = jobModel.GenerateEmailSubjectEstimationTeam(243);

            string expected = "PA000123, Proposal Number, Customer Test";

            //Assert
            Assert.AreEqual(expected, subject);
        }

        [TestMethod]
        public void GenerateEmailSubjectsInvoicingTeamTest()
        {
            //Arrange 
            FakeObjectSet<CS_Job> fakeJobObject = new FakeObjectSet<CS_Job>();
            fakeJobObject.AddObject(
                new CS_Job
                    {
                        Active = true,
                        ID = 243,
                        Number = "000123",
                        CS_CustomerInfo = new CS_CustomerInfo
                                              {
                                                  Active = true,
                                                  CS_Customer = new CS_Customer()
                                                                    {
                                                                        Active = true,
                                                                        Name = "Customer Test"
                                                                    }
                                              },
                        CS_JobInfo = new CS_JobInfo
                        {
                            CS_JobType = new CS_JobType() { ID = 1, Description = "A", Active = true },
                            CS_PriceType = new CS_PriceType() { ID = 1, Acronym = "P", Active = true },
                            CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus { JobStatusId = 1, JobID = 243, Active = true }
                        }
                        }
                    }
                );

            //Act
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobObject);

            JobModel jobModel = new JobModel(mockUnitOfWork.Object);

            string subject = jobModel.GenerateEmailSubjectsInvoicingTeam(243);

            string expected = "PA000123, Customer Test";

            //Assert
            Assert.AreEqual(expected, subject);
        }

        [TestMethod]
        public void TestListPresetNotification()
        {
            // Arrange
            FakeObjectSet<CS_Job> fakeJobObject = new FakeObjectSet<CS_Job>();
            fakeJobObject.AddObject(
                new CS_Job
                {
                    Active = true,
                    ID = 243,
                    Number = "123",
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 243,
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 243, JobStatusId = (int)Globals.JobRecord.JobStatus.Preset, Active = true }
                        }
                    },
                    CS_PresetInfo = new CS_PresetInfo()
                    {
                        JobId = 243,
                        Date = new DateTime(2011, 6, 15),
                        Time = new TimeSpan(16, 0, 0),
                        Active = true
                    }
                }
                );
            fakeJobObject.AddObject(
                new CS_Job
                {
                    Active = true,
                    ID = 244,
                    Number = "124",
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 244,
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 244, JobStatusId = (int)Globals.JobRecord.JobStatus.PresetPurchase, Active = true }
                        }
                    },
                    CS_PresetInfo = new CS_PresetInfo()
                    {
                        JobId = 244,
                        Date = new DateTime(2011, 6, 15),
                        Time = new TimeSpan(12, 0, 0),
                        Active = true
                    }
                }
                );
            fakeJobObject.AddObject(
                new CS_Job
                {
                    Active = true,
                    ID = 245,
                    Number = "125",
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 245,
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 245, JobStatusId = (int)Globals.JobRecord.JobStatus.Active, Active = true }
                        }
                    },
                    CS_PresetInfo = new CS_PresetInfo()
                }
                );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobObject);

            // Act
            JobModel jobModel = new JobModel(mockUnitOfWork.Object);
            IList<PresetNotificationVO> returnList = jobModel.ListPresetNotification(new DateTime(2011, 6, 16, 16, 32, 0));

            // Assert
            Assert.AreEqual(2, returnList.Count);
        }

        [TestMethod]
        public void TestFindJobSummaryByJobIds()
        {
            // Arrange
            FakeObjectSet<CS_SP_GetJobSummary_Result> fakeObjectSet = new FakeObjectSet<CS_SP_GetJobSummary_Result>();
            fakeObjectSet.AddObject(
                new CS_SP_GetJobSummary_Result()
                {
                    JobID = 1
                });
            fakeObjectSet.AddObject(
                new CS_SP_GetJobSummary_Result()
                {
                    JobID = 2
                });
            fakeObjectSet.AddObject(
                new CS_SP_GetJobSummary_Result()
                {
                    JobID = 3
                });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_SP_GetJobSummary_Result>()).Returns(fakeObjectSet);

            // Act
            JobModel jobModel = new JobModel(mockUnitOfWork.Object);
            IList<CS_View_JobSummary> returnList = jobModel.FindJobSummary(new List<int> { 1, 2 });

            // Assert
            Assert.AreEqual(2, returnList.Count);
        }
    }
}
