using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Moq;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class EmployeeModelTest
    {
        [TestMethod]
        public void TestGetEmployee()
        {
            //Arrange
            EmployeeModel model = new EmployeeModel(new FakeUnitOfWork());
            //Act
            CS_Employee result = model.GetEmployee(4);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIfEmployeeIsBeingUpdated()
        {
            // Arrange
            FakeObjectSet<CS_Employee> fakeEmployeeList = new FakeObjectSet<CS_Employee>();
            FakeObjectSet<CS_CallCriteria> fakeCallCriteriaList = new FakeObjectSet<CS_CallCriteria>();
            FakeObjectSet<CS_CallCriteriaValue> fakeCallCriteriaValueList = new FakeObjectSet<CS_CallCriteriaValue>();
            FakeObjectSet<CS_EmployeeCoverage> fakeEmployeeCoverageList = new FakeObjectSet<CS_EmployeeCoverage>();
            FakeObjectSet<CS_EmployeeOffCallHistory> fakeEmployeeOffCallList = new FakeObjectSet<CS_EmployeeOffCallHistory>();
            FakeObjectSet<CS_Settings> fakeSettingsList = new FakeObjectSet<CS_Settings>();
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            FakeObjectSet<CS_CallLogResource> fakeCallLogResourceList = new FakeObjectSet<CS_CallLogResource>();
            FakeObjectSet<CS_Resource> fakeResourceList = new FakeObjectSet<CS_Resource>();
            FakeObjectSet<CS_PhoneNumber> fakePhoneList = new FakeObjectSet<CS_PhoneNumber>();

            fakeEmployeeList.AddObject(
                new CS_Employee()
                {
                    ID = 1,
                    Active = true,
                    HasAddressChanges = false,
                    HasPhoneChanges = false,
                }
            );
            fakeEmployeeList.AddObject(
                new CS_Employee()
                {
                    ID = 2,
                    Active = true,
                    HasAddressChanges = false,
                    HasPhoneChanges = false,
                }
            );
            fakeSettingsList.AddObject(
                new CS_Settings()
                {
                    ID = (int)Globals.Configuration.Settings.AddressChangeNotification,
                    Description = "ksantos@hulcher.com"
                }
            );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Employee>()).Returns(fakeEmployeeList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteria>()).Returns(fakeCallCriteriaList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakeCallCriteriaValueList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_EmployeeCoverage>()).Returns(fakeEmployeeCoverageList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_EmployeeOffCallHistory>()).Returns(fakeEmployeeOffCallList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Settings>()).Returns(fakeSettingsList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallLogResource>()).Returns(fakeCallLogResourceList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Resource>()).Returns(fakeResourceList);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_PhoneNumber>()).Returns(fakePhoneList);

            CS_Employee updateEmployee = new CS_Employee()
            {
                ID = 1,
                Address = "Testing Address"
            };

            CS_CallCriteria callCriteria = new CS_CallCriteria()
            {
                EmployeeID = 1,
                Active = true
            };

            IList<CS_CallCriteriaValue> callCriteriaValueList = new List<CS_CallCriteriaValue>();
            callCriteriaValueList.Add(new CS_CallCriteriaValue()
            {
                CallCriteriaTypeID = 1,
                Value = "testing",
                Active = true

            });

            CS_EmployeeCoverage coverage = new CS_EmployeeCoverage()
            {
                EmployeeID = 1,
                Active = true,
                CoverageStartDate = new DateTime(2011, 8, 29),
                Duration = 10,
                DivisionID = 1,
                CS_Employee = new CS_Employee() { ID = 1, Active = true, FullName = "Santos, Kleiton" },
                CS_Division = new CS_Division() { ID = 1, Active = true, Name = "001" }
            };

            CS_EmployeeOffCallHistory offCall = new CS_EmployeeOffCallHistory()
            {
                EmployeeID = 1,
                ProxyEmployeeID = 2,
                Active = true,
                OffCallStartDate = new DateTime(2011, 8, 29),
                OffCallEndDate = new DateTime(2011, 8, 31),
                OffCallReturnTime = new TimeSpan(10, 0, 0),
                CS_Employee = new CS_Employee() { ID = 1, Active = true, FullName = "Santos, Kleiton" },
                CS_Employee_Proxy = new CS_Employee() { ID = 2, Active = true, FullName = "Burton, Cynthia" }
            };

            // Act
            EmployeeModel model = new EmployeeModel(mockUnitOfWork.Object);
            model.SaveEmployee(updateEmployee, offCall, coverage, "system", true, true, new List<DataContext.VO.PhoneNumberVO>());

            // Assert
            Assert.AreEqual(1, fakeCallCriteriaList.Count());
            Assert.AreEqual(1, fakeCallCriteriaValueList.Count());
            Assert.AreEqual(1, fakeEmployeeOffCallList.Count());
            Assert.AreEqual(1, fakeEmployeeCoverageList.Count());
        }
    }
}
