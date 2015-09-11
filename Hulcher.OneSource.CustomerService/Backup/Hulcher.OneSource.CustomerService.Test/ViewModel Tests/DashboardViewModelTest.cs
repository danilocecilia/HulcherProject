using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using Moq;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.ViewModel_Tests
{
    [TestClass]
    public class DashboardViewModelTest
    {
        [TestMethod]
        public void IsLoadPageFillingProperties()
        {
            // Arrange
            Moq.Mock<IDashboardView> mockView = new Moq.Mock<IDashboardView>();
            mockView.SetupProperty(c => c.BeginDateJobSummaryValue, new DateTime());
            mockView.SetupProperty(c => c.EndDateJobSummaryValue, new DateTime());
            mockView.SetupProperty(c => c.BeginDateCallLogViewFilter, new DateTime());
            mockView.SetupProperty(c => c.EndDateCallLogViewFilter, new DateTime());
            mockView.SetupProperty(c => c.DashBoardViewType, Globals.Dashboard.ViewType.JobCallLogView);
            DashboardViewModel viewModel = new DashboardViewModel(mockView.Object, new FakeUnitOfWork());
            DateTime beginDate = DateTime.Now.AddDays(-4);
            DateTime endDate = DateTime.Now;

            // Act
            viewModel.LoadPage();

            // Assert
            Assert.AreEqual(beginDate.ToString("MM/dd/yyyy"), mockView.Object.BeginDateJobSummaryValue.ToString("MM/dd/yyyy"));
            Assert.AreEqual(beginDate.ToString("MM/dd/yyyy"), mockView.Object.BeginDateCallLogViewFilter.ToString("MM/dd/yyyy"));
            Assert.AreEqual(endDate.ToString("MM/dd/yyyy"), mockView.Object.EndDateJobSummaryValue.ToString("MM/dd/yyyy"));
            Assert.AreEqual(endDate.ToString("MM/dd/yyyy"), mockView.Object.EndDateCallLogViewFilter.ToString("MM/dd/yyyy"));
            Assert.AreEqual(Globals.Dashboard.ViewType.JobCallLogView, mockView.Object.DashBoardViewType);
        }

        [TestMethod]
        public void IsSetJobSummaryRowDataFillingRowProperties()
        {
            // Arrange
            DateTime currentDate = DateTime.Now;
            CS_SP_GetJobSummary_Result jobItem = new CS_SP_GetJobSummary_Result()
            {
                CallDate = currentDate,
                CallLogId = 1,
                Customer = "Customer1",
                CustomerId = 1,
                Division = "001",
                DivisionId = 1,
                HasResources = 1,
                IsResource = false,
                JobID = 1,
                JobNumber = "000001",
                JobStatus = "A - Active",
                JobStatusId = 1,
                LastCallDate = currentDate,
                LastCallType = "Initial Advise",
                LastModification = currentDate,
                Location = "Denton, TX",
                ModifiedBy = "user",
                PresetDate = currentDate,
                ProjectManager = "pm",
                StartDate = currentDate,
                PrefixedNumber = "PA000001"
            };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.JobSummaryRepeaterDataItem, jobItem);
            mock.SetupProperty(c => c.JobSummaryRowDivision, "");
            mock.SetupProperty(c => c.JobSummaryRowJobId, 0);
            mock.SetupProperty(c => c.JobSummaryRowHasResources, null);
            mock.SetupProperty(c => c.JobSummaryRowJobNumber, "");
            mock.SetupProperty(c => c.JobSummaryRowCustomer, "");
            mock.SetupProperty(c => c.JobSummaryRowStatus, "");
            mock.SetupProperty(c => c.JobSummaryRowLocation, "");
            mock.SetupProperty(c => c.JobSummaryRowProjectManager, "");
            mock.SetupProperty(c => c.JobSummaryRowModifiedBy, "");
            mock.SetupProperty(c => c.JobSummaryRowLastModification, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryRowCallDate, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryRowPresetDate, null);
            mock.SetupProperty(c => c.JobSummaryRowLastCallEntry, null);
            mock.SetupProperty(c => c.JobSummaryRowLastCallEntryId, null);
            mock.SetupProperty(c => c.JobSummaryRowLastCallDate, null);

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetJobSummaryRowData();

            // Assert
            Assert.AreEqual("001", mock.Object.JobSummaryRowDivision, "Failed in JobSummaryRowDivision");
            Assert.AreEqual(1, mock.Object.JobSummaryRowJobId, "Failed in JobSummaryRowJobId");
            Assert.AreEqual(1, mock.Object.JobSummaryRowHasResources, "Failed in JobSummaryRowHasResources");
            Assert.AreEqual("PA000001", mock.Object.JobSummaryRowJobNumber, "Failed in JobSummaryRowJobNumber");
            Assert.AreEqual("Customer1", mock.Object.JobSummaryRowCustomer, "Failed in JobSummaryRowCustomer");
            Assert.AreEqual("A - Active", mock.Object.JobSummaryRowStatus, "Failed in JobSummaryRowStatus");
            Assert.AreEqual("Denton, TX", mock.Object.JobSummaryRowLocation, "Failed in JobSummaryRowLocation");
            Assert.AreEqual("pm", mock.Object.JobSummaryRowProjectManager, "Failed in JobSummaryRowProjectManager");
            Assert.AreEqual("user", mock.Object.JobSummaryRowModifiedBy, "Failed in JobSummaryRowModifiedBy");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryRowLastModification, "Failed in JobSummaryRowLastModification");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryRowCallDate, "Failed in JobSummaryRowCallDate");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryRowPresetDate, "Failed in JobSummaryRowPresetDate");
            Assert.AreEqual("Initial Advise", mock.Object.JobSummaryRowLastCallEntry, "Failed in JobSummaryRowLastCallEntry");
            Assert.AreEqual(1, mock.Object.JobSummaryRowLastCallEntryId, "Failed in JobSummaryRowLastCallEntryId");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryRowLastCallDate, "Failed in JobSummaryRowLastCallDate");
        }

        [TestMethod]
        public void IsSetJobSummaryResourceRowDataFillingRowPropertiesWithEmployee()
        {
            // Arrange
            DateTime currentDate = DateTime.Now;
            CS_SP_GetJobSummary_Result resourceItem = new CS_SP_GetJobSummary_Result()
            {
                ResourCallDate = currentDate,
                CallLogId = 1,
                Division = "001",
                EmployeeName = "Employee1",
                JobID = 1,
                LastCallDate = currentDate,
                LastCallType = "Initial Advise",
                ResouceLastModification = currentDate,
                Location = "Denton, TX",
                ModifiedBy = "user"
            };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.JobSummaryResourceRepeaterDataItem, resourceItem);
            mock.SetupProperty(c => c.JobSummaryResourceRowDivision, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowResource, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowLocation, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowModifiedBy, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowLastModification, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryResourceRowCallDate, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallEntry, null);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallEntryId, null);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallDate, null);

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetJobSummaryResourceRowData();

            // Assert
            Assert.AreEqual("001", mock.Object.JobSummaryResourceRowDivision, "Failed in JobSummaryResourceRowDivision");
            Assert.AreEqual("Employee1", mock.Object.JobSummaryResourceRowResource, "Failed in JobSummaryResourceRowResource");
            Assert.AreEqual("Denton, TX", mock.Object.JobSummaryResourceRowLocation, "Failed in JobSummaryResourceRowLocation");
            Assert.AreEqual("user", mock.Object.JobSummaryResourceRowModifiedBy, "Failed in JobSummaryResourceRowModifiedBy");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowLastModification, "Failed in JobSummaryResourceRowLastModification");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowCallDate, "Failed in JobSummaryResourceRowCallDate");
            Assert.AreEqual("Initial Advise", mock.Object.JobSummaryResourceRowLastCallEntry, "Failed in JobSummaryResourceRowLastCallEntry");
            Assert.AreEqual(1, mock.Object.JobSummaryResourceRowLastCallEntryId, "Failed in JobSummaryResourceRowLastCallEntryId");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowLastCallDate, "Failed in JobSummaryResourceRowLastCallDate");
        }

        [TestMethod]
        public void IsSetJobSummaryResourceRowDataFillingRowPropertiesWithEquipment()
        {
            // Arrange
            DateTime currentDate = DateTime.Now;
            CS_SP_GetJobSummary_Result resourceItem = new CS_SP_GetJobSummary_Result()
            {
                ResourCallDate = currentDate,
                CallLogId = 1,
                Division = "001",
                EquipmentName = "Equipment1",
                JobID = 1,
                LastCallDate = currentDate,
                LastCallType = "Initial Advise",
                ResouceLastModification = currentDate,
                Location = "Denton, TX",
                ModifiedBy = "user"
            };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.JobSummaryResourceRepeaterDataItem, resourceItem);
            mock.SetupProperty(c => c.JobSummaryResourceRowDivision, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowResource, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowLocation, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowModifiedBy, "");
            mock.SetupProperty(c => c.JobSummaryResourceRowLastModification, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryResourceRowCallDate, DateTime.MinValue);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallEntry, null);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallEntryId, null);
            mock.SetupProperty(c => c.JobSummaryResourceRowLastCallDate, null);

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetJobSummaryResourceRowData();

            // Assert
            Assert.AreEqual("001", mock.Object.JobSummaryResourceRowDivision, "Failed in JobSummaryResourceRowDivision");
            Assert.AreEqual("Equipment1", mock.Object.JobSummaryResourceRowResource, "Failed in JobSummaryResourceRowResource");
            Assert.AreEqual("Denton, TX", mock.Object.JobSummaryResourceRowLocation, "Failed in JobSummaryResourceRowLocation");
            Assert.AreEqual("user", mock.Object.JobSummaryResourceRowModifiedBy, "Failed in JobSummaryResourceRowModifiedBy");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowLastModification, "Failed in JobSummaryResourceRowLastModification");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowCallDate, "Failed in JobSummaryResourceRowCallDate");
            Assert.AreEqual("Initial Advise", mock.Object.JobSummaryResourceRowLastCallEntry, "Failed in JobSummaryResourceRowLastCallEntry");
            Assert.AreEqual(1, mock.Object.JobSummaryResourceRowLastCallEntryId, "Failed in JobSummaryResourceRowLastCallEntryId");
            Assert.AreEqual(currentDate, mock.Object.JobSummaryResourceRowLastCallDate, "Failed in JobSummaryResourceRowLastCallDate");
        }

        [TestMethod]
        public void IsSetJobCallLogDivisionRowDataFillingRowProperties()
        {
            // Arrange
            CS_Division divisionItem = new CS_Division()
                {
                    ID = 1,
                    Name = "001"
                };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.DivisionRowName, "");
            mock.SetupProperty(c => c.DivisionCount, 0);
            mock.SetupProperty(c => c.DivisionRepeaterDataItem, divisionItem);

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetJobCallLogDivisionRowData();

            // Assert
            Assert.AreEqual("001", mock.Object.DivisionRowName, "Failed in DivisionRowName");
            Assert.AreEqual(1, mock.Object.DivisionCount, "Failed in DivisionCount");
        }

        [TestMethod]
        public void IsSetJobCallLogJobRowDataFillingRowProperties()
        {
            // Arrange
            CS_View_JobCallLog jobItem = new CS_View_JobCallLog()
                {
                    JobNumber = "000002",
                    Customer = "Customer1",
                    PrefixedNumber = "PA000002"
                };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.JobRowJobNumberCustomer, "");
            mock.SetupProperty(c => c.JobCount, 0);
            mock.SetupProperty(c => c.JobRepeaterDataItem, jobItem);

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetJobCallLogJobRowData();

            // Assert
            Assert.AreEqual("PA000002 - Customer1", mock.Object.JobRowJobNumberCustomer, "Failed in JobRowJobNumberCustomer");
            Assert.AreEqual(1, mock.Object.JobCount, "Failed in JobCount");
        }

        [TestMethod]
        public void IsSetCallLogViewCallEntryRowDataFillingRowProperties()
        {
            // Arrange
            DateTime currentDate = DateTime.Now;
            CS_View_JobCallLog callEntryItem = new CS_View_JobCallLog()
                {
                    CallId = 4,
                    DivisionId = 2,
                    JobId = 3,
                    JobNumber = "00003",
                    Customer = "Customer1",
                    CallType = "JobCallType1",
                    CalledInBy = "CalledInBy1",
                    CallDate = currentDate,
                    ModifiedBy = "ModifiedBy1",
                    Details = "Details1"
                };
            Mock<IDashboardView> mock = new Mock<IDashboardView>();
            mock.SetupProperty(c => c.CallLogRepeaterDataItem, callEntryItem);
            mock.SetupProperty(c => c.CallLogRowCallType, "");
            mock.SetupProperty(c => c.CallLogRowCalledInBy, "");
            mock.SetupProperty(c => c.CallLogRowCallDate, "");
            mock.SetupProperty(c => c.CallLogRowCallTime, "");
            mock.SetupProperty(c => c.CallLogRowModifiedBy, "");
            mock.SetupProperty(c => c.CallLogRowDetails, "");

            // Act
            DashboardViewModel viewModel = new DashboardViewModel(mock.Object);
            viewModel.SetCallLogViewCallEntryRowData();

            // Assert
            Assert.AreEqual("JobCallType1", mock.Object.CallLogRowCallType, "Failed in CallLogRowCallType");
            Assert.AreEqual("CalledInBy1", mock.Object.CallLogRowCalledInBy, "Failed in CallLogRowCalledInBy");
            Assert.AreEqual(currentDate.ToString("MM/dd/yyyy"), mock.Object.CallLogRowCallDate, "Failed in CallLogRowCallDate");
            Assert.AreEqual(currentDate.ToString("HH:mm"), mock.Object.CallLogRowCallTime, "Failed in CallLogRowCallTime");
            Assert.AreEqual("ModifiedBy1", mock.Object.CallLogRowModifiedBy, "Failed in CallLogRowModifiedBy");
            Assert.AreEqual("Details1", mock.Object.CallLogRowDetails, "Failed in CallLogRowDetails");
        }
    }
}
