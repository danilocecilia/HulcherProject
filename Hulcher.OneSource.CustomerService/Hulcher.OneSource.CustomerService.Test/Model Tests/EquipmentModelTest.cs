using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class EquipmentModelTest
    {
        [TestMethod]
        public void TestUpdateEquipment()
        {
            //Arrange
            CS_Equipment equipment = new CS_Equipment()
                                         {
                                             Active = true,
                                             ID = 1,
                                             Name = "Xyz",
                                             Status = "Up",
                                             HeavyEquipment = false
                                         };

            FakeObjectSet<CS_Equipment> fakeEquipment = new FakeObjectSet<CS_Equipment>();
            fakeEquipment.AddObject(equipment);

            Mock<IUnitOfWork> mockUnitWork = new Mock<IUnitOfWork>();
            mockUnitWork.Setup(w => w.CreateObjectSet<CS_Equipment>()).Returns(fakeEquipment);

            EquipmentModel model = new EquipmentModel(mockUnitWork.Object);
            

            //Act
            model.UpdateMaintenanceEquipment(equipment);
            CS_Equipment csEquipment = model.GetEquipment(1);

            //Assert
            
            Assert.AreEqual(Globals.EquipmentMaintenance.Status.Up.ToString(), csEquipment.Status, "Error on Status Field");
            Assert.AreEqual(false, csEquipment.HeavyEquipment, "Error on heavyequipment field.");
        }

        //public void TestUpdateEquipmentDownHistory()
        //{
        //    DateTime dt = new DateTime(2011, 08, 01, 13, 02);

        //    //Arrange
        //    CS_EquipmentDownHistory equipmentDownHistory = new CS_EquipmentDownHistory()
        //                                                       {
        //                                                           Active = true,
        //                                                           ID = 1,
        //                                                           Duration = 1,
        //                                                           DownHistoryStartDate = DateTime.Now,
        //                                                           DownHistoryEndDate = DateTime.Now
        //                                                       };

        //    FakeObjectSet<CS_EquipmentDownHistory> fakeEquipmentDownHistory = new FakeObjectSet<CS_EquipmentDownHistory>();
        //    fakeEquipmentDownHistory.AddObject(equipmentDownHistory);

        //    Mock<IUnitOfWork> mockUnitWork = new Mock<IUnitOfWork>();
        //    mockUnitWork.Setup(w => w.CreateObjectSet<CS_EquipmentDownHistory>()).Returns(fakeEquipmentDownHistory);

        //    EquipmentModel model = new EquipmentModel(mockUnitWork.Object);


        //    //Act
        //    model.UpdateEquipmentDownHistory(equipmentDownHistory);
        //    CS_EquipmentDownHistory csEquipment = model.GetEquipmentDownHistory(1);

        //    //Assert

        //    Assert.AreEqual(, csEquipment.Status, "Error on Status Field");
        //    Assert.AreEqual(false, csEquipment.HeavyEquipment, "Error on heavyequipment field.");
        //}
    }
}
