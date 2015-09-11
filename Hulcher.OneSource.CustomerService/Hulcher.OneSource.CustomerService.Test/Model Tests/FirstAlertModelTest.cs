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
    public class FirstAlertModelTest
    {
        [TestMethod]
        public void TestGenerateFirstAlertNumber()
        {
            //Arrange
            FirstAlertModel model = new FirstAlertModel(new FakeUnitOfWork());
            CS_FirstAlert firstAlert = new CS_FirstAlert();
            //Act
            model.GenerateFirstAlertNumber(firstAlert);
            //Assert
            Assert.AreEqual("0002", firstAlert.Number);
        }

        [TestMethod]
        public void TestListFirstAlertPersonByFirstAlertID()
        {
            //Arrange
            FakeObjectSet<CS_FirstAlertPerson> fakePersonList = new FakeObjectSet<CS_FirstAlertPerson>();
            fakePersonList.AddObject(new CS_FirstAlertPerson() { Active = true, FirstAlertID = 1 });
            fakePersonList.AddObject(new CS_FirstAlertPerson() { Active = false, FirstAlertID = 1 });
            fakePersonList.AddObject(new CS_FirstAlertPerson() { Active = true, FirstAlertID = 2 });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_FirstAlertPerson>()).Returns(fakePersonList);
            FirstAlertModel model = new FirstAlertModel(mockUnitOfWork.Object);
            //Act
            IList<CS_FirstAlertPerson> results = model.ListFirstAlertPersonByFirstAlertID(1);
            //Assert
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void TestListAllFirstAlert()
        {
            //Arrange
            FakeObjectSet<CS_FirstAlert> fakeFirstAlert = new FakeObjectSet<CS_FirstAlert>();
            fakeFirstAlert.AddObject(new CS_FirstAlert() { 
                Active = true, 
                Number = "123", 
                JobID = 1, 
                CustomerID = 1, 
                Details = "aaAaA",
                Date = new DateTime(2011, 7, 12, 5, 0, 0), 
                HasPoliceReport = true, 
                CreatedBy = "dcecilia", 
                CreationDate = new DateTime(2011, 7, 12, 5, 0, 0),  
                ModifiedBy = "dcecilia", 
                ModificationDate = new DateTime(2011, 7, 12, 5, 0, 0)
            });
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

            mock.Setup(w => w.CreateObjectSet<CS_FirstAlert>()).Returns(fakeFirstAlert);

            FirstAlertModel model = new FirstAlertModel(mock.Object);

            //Act
            IList<CS_FirstAlert> results = model.ListAllFirstAlert();

            //Assert
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void TestListFilteredFirstAlert()
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

            fakeFirstAlert.AddObject(new CS_FirstAlert()
            {
                Active = true,
                Number = "123",
                JobID = 1,
                CustomerID = 1,
                Details = "aaAaA",
                Date = new DateTime(2011, 7, 12, 5, 0, 0),
                HasPoliceReport = true,
                CreatedBy = "dcecilia",
                CreationDate = new DateTime(2011, 7, 12, 5, 0, 0),
                ModifiedBy = "dcecilia",
                ModificationDate = new DateTime(2011, 7, 12, 5, 0, 0),
                CS_FirstAlertFirstAlertType = entityCollectionFirstAlertFirstAlertType,
            });

            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();

            mock.Setup(w => w.CreateObjectSet<CS_FirstAlert>()).Returns(fakeFirstAlert);

            FirstAlertModel model = new FirstAlertModel(mock.Object);

            //Act
            IList<CS_FirstAlert> results = model.ListFilteredFirstAlert(Globals.FirstAlert.FirstAlertFilters.IncidentType, "injury" );

            //Assert
            Assert.AreEqual(1, results.Count);

        }
    }
}
