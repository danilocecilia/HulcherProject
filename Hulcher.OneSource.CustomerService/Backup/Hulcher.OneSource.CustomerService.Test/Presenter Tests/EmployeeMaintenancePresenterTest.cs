using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class EmployeeMaintenancePresenterTest
    {
        [TestMethod]
        public void MustReturnEmployeeInfo()
        {
            //Arrange
            Mock<IEmployeeMaintenanceView> mockView = new Mock<IEmployeeMaintenanceView>();
            Mock<EmployeeModel> mockModel = new Mock<EmployeeModel>();
            System.Data.Objects.DataClasses.EntityCollection<CS_EmployeeEmergencyContact> employeeContacts = new System.Data.Objects.DataClasses.EntityCollection<CS_EmployeeEmergencyContact>();
            employeeContacts.Add(new CS_EmployeeEmergencyContact() { Active = true, EmployeeID = 1, FirstName = "Margie", LastName = "Simpson", HomeAreaCode = "11", HomePhone = "89431267" });
            CS_Employee employeeStub = new CS_Employee()
            {
                ID = 1,
                PersonID = "123",
                FirstName = "Danilo",
                Name = "Ruziska",
                HireDate = new DateTime(2011, 8, 31),
                Address = "Elm Street, 123",
                Address2 = "Third Floor",
                City = "Houston",
                StateProvinceCode = "TX",
                CountryCode = "USA",
                PostalCode = "12345",
                BusinessCardTitle = "Software Developer",
                CS_Division = new CS_Division() { ID = 1, Active = true, Name = "001" },
                PassportNumber = "12345abc",
                CS_EmployeeEmergencyContact = employeeContacts,
                HomeAreaCode = "11",
                HomePhone = "123456",
                MobileAreaCode = "12",
                MobilePhone = "34567",
                OtherPhoneAreaCode = "13",
                OtherPhone = "98765",
                IsDentonPersonal = true,
                DriversLicenseNumber = "123",
                DriversLicenseClass = "A",
                DriversLicenseStateProvinceCode = "TX",
                DriversLicenseExpireDate = new DateTime(2012, 12, 21)
            };

            mockView.SetupProperty(e => e.EmployeeId, 1);
            mockView.SetupProperty(e => e.EmployeeName, "");
            mockView.SetupProperty(e => e.HireDate, null);
            mockView.SetupProperty(e => e.PersonID, "");
            mockView.SetupProperty(e => e.Address, "");
            mockView.SetupProperty(e => e.Address2, "");
            mockView.SetupProperty(e => e.City, "");
            mockView.SetupProperty(e => e.State, "");
            mockView.SetupProperty(e => e.Country, "");
            mockView.SetupProperty(e => e.PostalCode, "");
            mockView.SetupProperty(e => e.Position, "");
            mockView.SetupProperty(e => e.EmployeeDivision, "");
            mockView.SetupProperty(e => e.PassportNumber, "");
            mockView.SetupProperty(e => e.EmployeeContacts, null);
            mockView.SetupProperty(e => e.HomePhone, "");
            mockView.SetupProperty(e => e.HomeAreaCode, "");
            mockView.SetupProperty(e => e.MobileAreaCode, "");
            mockView.SetupProperty(e => e.MobilePhone, "");
            mockView.SetupProperty(e => e.OtherPhoneAreaCode, "");
            mockView.SetupProperty(e => e.OtherPhone, "");
            mockView.SetupProperty(e => e.IsDentonPersonal, false);
            mockView.SetupProperty(e => e.DriversLicenseNumber, "");
            mockView.SetupProperty(e => e.DriversLicenseClass, "");
            mockView.SetupProperty(e => e.DriversLicenseState, "");
            mockView.SetupProperty(e => e.DriversLicenseExpireDate, null);
            mockModel.Setup(e => e.GetEmployee(1)).Returns(employeeStub);

            EmployeeMaintenancePresenter presenter = new EmployeeMaintenancePresenter(mockView.Object, mockModel.Object);
            //Act
            presenter.LoadEmployeeInfo();
            //Assert
            Assert.AreEqual("Danilo,Ruziska", mockView.Object.EmployeeName);
            Assert.AreEqual(new DateTime(2011, 8, 31), mockView.Object.HireDate);
            Assert.AreEqual("123", mockView.Object.PersonID);
            Assert.AreEqual("Software Developer", mockView.Object.Position);
            Assert.AreEqual("001", mockView.Object.EmployeeDivision);
            Assert.AreEqual("12345abc", mockView.Object.PassportNumber);
            Assert.AreEqual(1, mockView.Object.EmployeeContacts.Count);
            Assert.AreEqual("Elm Street, 123", mockView.Object.Address);
            Assert.AreEqual("Third Floor", mockView.Object.Address2);
            Assert.AreEqual("Houston", mockView.Object.City);
            Assert.AreEqual("TX", mockView.Object.State);
            Assert.AreEqual("USA", mockView.Object.Country);
            Assert.AreEqual("12345", mockView.Object.PostalCode);
            Assert.AreEqual("11", mockView.Object.HomeAreaCode);
            Assert.AreEqual("123456", mockView.Object.HomePhone);
            Assert.AreEqual("12", mockView.Object.MobileAreaCode);
            Assert.AreEqual("34567", mockView.Object.MobilePhone);
            Assert.AreEqual("13", mockView.Object.OtherPhoneAreaCode);
            Assert.AreEqual("98765", mockView.Object.OtherPhone);
            Assert.IsTrue(mockView.Object.IsDentonPersonal);
            Assert.AreEqual("123", mockView.Object.DriversLicenseNumber);
            Assert.AreEqual("A", mockView.Object.DriversLicenseClass);
            Assert.AreEqual("TX", mockView.Object.DriversLicenseState);
            Assert.AreEqual(new DateTime(2012, 12, 21), mockView.Object.DriversLicenseExpireDate);
        }

        //[TestMethod]
        //public void MustReturnOffCallInformation()
        //{
        //    //Arrange
        //    Mock<IEmployeeMaintenanceView> mockView = new Mock<IEmployeeMaintenanceView>();
        //    mockView.SetupProperty(e => e.OffCallStartDate, new DateTime());
        //    mockView.SetupProperty(e => e.OffCallEndDate, new DateTime());
        //    mockView.SetupProperty(e => e.OffCallReturnTime, new TimeSpan());
        //    mockView.SetupProperty(e => e.ProxyEmployeeId, 0);
        //    mockView.SetupProperty(e => e.EmployeeId, 1);
        //    Mock<EmployeeModel> mockModel = new Mock<EmployeeModel>();
        //    CS_EmployeeOffCallHistory activeOffCall = new CS_EmployeeOffCallHistory()
        //    {
        //        EmployeeID = 1,
        //        ProxyEmployeeID = 3,
        //        CS_Employee_Proxy = new CS_Employee() { ID = 3, FirstName = "Emp", Name = "Proxy" },
        //        Active = true,
        //        OffCallStartDate = new DateTime(2011, 08, 20),
        //        OffCallEndDate = new DateTime(2011, 08, 25),
        //        OffCallReturnTime = new TimeSpan(11, 30, 0)
        //    };
        //    mockModel.Setup(e => e.GetActiveOffCallEmployeeById(1)).Returns(activeOffCall);
        //    EmployeeMaintenancePresenter _presenter = new EmployeeMaintenancePresenter(mockView.Object, mockModel.Object);
        //    //Act
        //    _presenter.LoadEmployeeOffCallInformation();
        //    //Assert            
        //    Assert.AreEqual(new DateTime(2011, 08, 20).Date, mockView.Object.OffCallStartDate.Value.Date);
        //    Assert.AreEqual(new DateTime(2011, 08, 25).Date, mockView.Object.OffCallEndDate.Value.Date);
        //    Assert.AreEqual(new TimeSpan(11, 30, 0), mockView.Object.OffCallReturnTime.Value);
        //    Assert.AreEqual(3, mockView.Object.ProxyEmployeeId.Value);
        //}

        //[TestMethod]
        //public void MustReturnOffCallHistory()
        //{
        //    //Arrange
        //    Mock<IEmployeeMaintenanceView> mockView = new Mock<IEmployeeMaintenanceView>();
        //    Mock<EmployeeModel> mockModel = new Mock<EmployeeModel>();
        //    IList<CS_EmployeeOffCallHistory> offCallHistory = new List<CS_EmployeeOffCallHistory>();
        //    offCallHistory.Add(new CS_EmployeeOffCallHistory()
        //    {
        //        Active = false,
        //        CS_Employee_Proxy =
        //            new CS_Employee() { FirstName = "Danilo", Name = "Ruziska" },
        //        OffCallStartDate = new DateTime(2011, 8, 15),
        //        OffCallEndDate = new DateTime(2011, 8, 25),
        //        OffCallReturnTime = new TimeSpan(11, 11, 0)
        //    });
        //    offCallHistory.Add(new CS_EmployeeOffCallHistory()
        //    {
        //        Active = false,
        //        CS_Employee_Proxy =
        //            new CS_Employee() { FirstName = "Bruce", Name = "Wayne" },
        //        OffCallStartDate = new DateTime(2011, 8, 15),
        //        OffCallEndDate = new DateTime(2011, 8, 25),
        //        OffCallReturnTime = new TimeSpan(11, 11, 0)
        //    });
        //    offCallHistory.Add(new CS_EmployeeOffCallHistory()
        //    {
        //        Active = false,
        //        CS_Employee_Proxy =
        //            new CS_Employee() { FirstName = "Peter", Name = "Parker" },
        //        OffCallStartDate = new DateTime(2011, 8, 15),
        //        OffCallEndDate = new DateTime(2011, 8, 25),
        //        OffCallReturnTime = new TimeSpan(11, 11, 0)
        //    });
        //    mockView.SetupProperty(e => e.EmployeeId, 1);
        //    mockView.SetupProperty(e => e.OffCallHistoryList, null);
        //    mockModel.Setup(e => e.ListEmployeeOffCallHistory(1)).Returns(offCallHistory);
        //    EmployeeMaintenancePresenter _presenter = new EmployeeMaintenancePresenter(mockView.Object, mockModel.Object);
        //    //Act
        //    _presenter.LoadEmployeeOffCallHistory();
        //    //Assert
        //    Assert.AreEqual(3, mockView.Object.OffCallHistoryList.Count);
        //}

        //[TestMethod]
        //public void MustReturnEmployeeCoverageInformation()
        //{
        //    //Arrange
        //    Mock<IEmployeeMaintenanceView> mockView = new Mock<IEmployeeMaintenanceView>();
        //    mockView.SetupProperty(e => e.ActualEmployeeDivision, "");
        //    mockView.SetupProperty(e => e.CoverageDivisionID, null);
        //    mockView.SetupProperty(e => e.CoverageStartDate, new DateTime());
        //    mockView.SetupProperty(e => e.CoverageStartTime, null);
        //    mockView.SetupProperty(e => e.CoverageEndDate, new DateTime());
        //    mockView.SetupProperty(e => e.CoverageDuration, null);
        //    mockView.SetupProperty(e => e.EmployeeId, 1);
        //    Mock<EmployeeModel> mockModel = new Mock<EmployeeModel>();
        //    CS_EmployeeCoverage activeCoverage = new CS_EmployeeCoverage()
        //    {
        //        EmployeeID = 1,
        //        DivisionID = 2,
        //        CS_Division = new CS_Division() { ID=2, Name = "002"},
        //        CoverageStartDate = new DateTime(2011, 8, 10, 12, 45, 0),
        //        CoverageEndDate = new DateTime(2011, 8, 25),
        //        Duration = 15
        //    };
        //    mockModel.Setup(e => e.GetEmployeeCoverageById(1)).Returns(activeCoverage);
        //    EmployeeMaintenancePresenter _presenter = new EmployeeMaintenancePresenter(mockView.Object, mockModel.Object);
        //    //Act
        //    _presenter.LoadEmployeeCoverageInfo();
        //    //Assert
        //    Assert.AreEqual(2, mockView.Object.CoverageDivisionID.Value);
        //    Assert.AreEqual(new DateTime(2011, 08, 10, 12, 45, 0), mockView.Object.CoverageStartDate.Value);
        //    Assert.AreEqual(new TimeSpan(12, 45, 0), mockView.Object.CoverageStartTime);
        //    Assert.AreEqual(new DateTime(2011, 08, 25), mockView.Object.CoverageEndDate.Value);
        //    Assert.AreEqual(15, mockView.Object.CoverageDuration.Value);
        //}

        //[TestMethod]
        //public void MustReturnEmployeeCoverageHistory()
        //{
        //     Mock<IEmployeeMaintenanceView> mockView = new Mock<IEmployeeMaintenanceView>();
        //     Mock<EmployeeModel> mockModel = new Mock<EmployeeModel>();
        //     IList<CS_EmployeeCoverage> employeeCoverageHistoryList = new List<CS_EmployeeCoverage>();
        //    employeeCoverageHistoryList.Add(new CS_EmployeeCoverage()
        //    {
        //        EmployeeID = 1,
        //        DivisionID = 2,
        //        CS_Division = new CS_Division() { ID=2, Name = "002"},
        //        CoverageStartDate = new DateTime(2011, 8, 10),
        //        CoverageEndDate = new DateTime(2011, 8, 25),
        //        Duration = 15
        //    });
        //    employeeCoverageHistoryList.Add(new CS_EmployeeCoverage()
        //    {
        //        EmployeeID = 1,
        //        DivisionID = 3,
        //        CS_Division = new CS_Division() { ID=3, Name = "003"},
        //        CoverageStartDate = new DateTime(2011, 7, 10),
        //        CoverageEndDate = new DateTime(2011, 7, 20),
        //        Duration = 8
        //    });
        //     mockView.SetupProperty(e => e.EmployeeId, 1);
        //     mockView.SetupProperty(e => e.CoverageHistoryList, null);
        //     mockModel.Setup(e => e.ListEmployeeCoverageHistory(1)).Returns(employeeCoverageHistoryList);
        //     EmployeeMaintenancePresenter _presenter = new EmployeeMaintenancePresenter(mockView.Object, mockModel.Object);
        //    //Act
        //     _presenter.ListEmployeeCoverageHistory();
        //    //Assert
        //     Assert.AreEqual(2, mockView.Object.CoverageHistoryList.Count);
        //}
    }
}
