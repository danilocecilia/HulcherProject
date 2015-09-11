using System;
using System.Data.Objects.DataClasses;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Test.ViewModel_Tests
{
    [TestClass]
    public class FirstAlertViewModelTest
    {
        [TestMethod]
        public void SetCallLogViewCallEntryRowData()
        {
            //Arrange
            FakeObjectSet<CS_FirstAlert> fakeFirstAlert = new FakeObjectSet<CS_FirstAlert>();

            CS_FirstAlertType csFirstAlertType = new CS_FirstAlertType()
            {
                Active = true,
                Description = "injury",
                CreatedBy = "dcecilia",
                CreationDate = new DateTime(10, 10, 10, 5, 0, 1),
                ModifiedBy = "dcecilia",
                ModificationDate = new DateTime(10, 10, 10, 5, 0, 1),

            };

            CS_FirstAlertFirstAlertType csFirstAlertFirstAlertType = new CS_FirstAlertFirstAlertType()
            {
                Active = true,
                FirstAlertID = 1,
                FirstAlertTypeID = 1,
                CreatedBy = "dcecilia",
                CreationDate =
                    new DateTime(10, 10, 10, 5, 0, 1),
                ModifiedBy = "dcecilia",
                ModificationDate =
                    new DateTime(10, 10, 10, 5, 0, 1),
                CS_FirstAlertType = csFirstAlertType
            };

            EntityCollection<CS_FirstAlertFirstAlertType> entityCollectionFirstAlertFirstAlertType = new EntityCollection<CS_FirstAlertFirstAlertType>();
            entityCollectionFirstAlertFirstAlertType.Add(csFirstAlertFirstAlertType);

            DateTime currentDate = DateTime.Now;

            CS_Customer csCustomer = new CS_Customer()
                                         {
                                             ID = 1,
                                             Active = true,
                                             Name = "Abcd",
                                             Country = "USA",
                                             CustomerNumber = "1000"
                                         };

            CS_Job csJob = new CS_Job()
                               {
                                   ID = 1,
                                   Active = true,
                                   CreatedBy = "dcecilia",
                                   CreationDate = currentDate,
                                   ModifiedBy = "dcecilia",
                                   ModificationDate = currentDate,
                                   Number = "123"
                               };


            CS_FirstAlert csFirstAlert = new CS_FirstAlert()
                                             {
                                                 ID = 1,
                                                 Active = true,
                                                 Number = "123",
                                                 JobID = 1,
                                                 CS_Job = csJob,
                                                 CustomerID = 1,
                                                 CS_Customer = csCustomer,
                                                 
                                                 Details = "aaAaA",
                                                 Date = currentDate,
                                                 HasPoliceReport = true,
                                                 CreatedBy = "dcecilia",
                                                 CreationDate = currentDate,
                                                 ModifiedBy = "dcecilia",
                                                 ModificationDate = new DateTime(2011, 7, 12, 5, 0, 0),
                                                 CS_FirstAlertFirstAlertType = entityCollectionFirstAlertFirstAlertType,
                                             };

            CS_Division csDivision = new CS_Division()
            {
                Active = true,
                ID = 1,
                Name = "001"
            };

            CS_FirstAlertDivision csFirstAlertDivision = new CS_FirstAlertDivision()
            {
                Active = true,
                ID = 1,
                FirstAlertID = 1,
                DivisionID = 1,
                CS_Division = csDivision,
                CS_FirstAlert = csFirstAlert
            };

            Mock<IFirstAlertView> mock = new Mock<IFirstAlertView>();
            mock.SetupProperty(c => c.FirstAlertRowDataItem, csFirstAlert);
            mock.SetupProperty(c => c.FirstAlertRowAlertDateAndTime, "");
            mock.SetupProperty(c => c.FirstAlertRowAlertId, "");
            mock.SetupProperty(c => c.FirstAlertRowAlertNumber, "");
            mock.SetupProperty(c => c.FirstAlertRowCustomer, "");
            mock.SetupProperty(c => c.FirstAlertRowDivision, "");
            mock.SetupProperty(c => c.FirstAlertRowFirstAlertType, "");
            mock.SetupProperty(c => c.FirstAlertRowJobNumber, "");

           //Act
            FirstAlertViewModel viewModel = new FirstAlertViewModel(mock.Object);

            viewModel.SetDetailedFirstAlertRowData();

            // Assert
            Assert.AreEqual(currentDate.ToString("MM/dd/yyyy") + " " + currentDate.ToShortTimeString(), mock.Object.FirstAlertRowAlertDateAndTime, "Failed in FirstAlertRowAlertDateAndTime");
            Assert.AreEqual("1", mock.Object.FirstAlertRowAlertId, "Failed in FirstAlertRowAlertId");
            Assert.AreEqual("123", mock.Object.FirstAlertRowAlertNumber, "Failed in FirstAlertRowAlertNumber");
            Assert.AreEqual("Abcd - USA - 1000", mock.Object.FirstAlertRowCustomer, "Failed in FirstAlertRowCustomer");
            Assert.AreEqual("001", mock.Object.FirstAlertRowDivision, "Failed in FirstAlertRowDivision");
            Assert.AreEqual("injury", mock.Object.FirstAlertRowFirstAlertType, "Failed in FirstAlertRowFirstAlertType");
            Assert.AreEqual("123", mock.Object.FirstAlertRowJobNumber, "Failed in FirstAlertRowJobNumber");
        }

        [TestMethod]
        public void FilteredEquipmentsRowDataBound()
        {
            //Arrange
            IList<CS_FirstAlertVehicle> firstAlertVehicleList = new List<CS_FirstAlertVehicle>();

            firstAlertVehicleList.Add(new CS_FirstAlertVehicle()
            {
                Active = true,
                EquipmentID = 1,
                Damage = "TL",
                EstimatedCost = 100,
            });

            Mock<IFirstAlertView> mock = new Mock<IFirstAlertView>();
            mock.SetupProperty(c => c.FirstAlertVehicleList, firstAlertVehicleList);
            mock.SetupProperty(c => c.FilteredEquipmentsEquipmentID, 1);
            mock.SetupProperty(c => c.FilteredEquipmentsDamage, "");
            mock.SetupProperty(c => c.FilteredEquipmentsEstCost, "");
            mock.SetupProperty(c => c.FilteredEquipmentsSelect, false);

            //Act
            FirstAlertViewModel viewModel = new FirstAlertViewModel(mock.Object);

            viewModel.FilteredEquipmentsRowDataBound();

            // Assert
            Assert.AreEqual("TL", mock.Object.FilteredEquipmentsDamage, "Failed in FilteredEquipmentsDamage");
            Assert.AreEqual(string.Format("{0:0.00}", 100), mock.Object.FilteredEquipmentsEstCost, "Failed in FilteredEquipmentsEstCost");
            Assert.AreEqual(true, mock.Object.FilteredEquipmentsSelect, "Failed in FilteredEquipmentsSelect");
        }
    }
}
