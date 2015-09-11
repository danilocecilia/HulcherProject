using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class ResourceAllocationModelTest
    {
        [TestMethod]
        public void TestGetResource()
        {
            //Arrange
            ResourceAllocationModel model = new ResourceAllocationModel(new FakeUnitOfWork());
            //Act
            CS_Resource resource = model.GetResource(1);
            //Assert
            Assert.IsNotNull(resource);
        }

        [TestMethod]
        public void TestListAllFilteredResourcesInfoByJob()
        {
            //Arrange
            ResourceAllocationModel model = new ResourceAllocationModel(new FakeUnitOfWork());
            //Act
            IList<CS_View_Resource_CallLogInfo> resultList = model.ListFilteredResourcesCallLogInfoByJob(5, true,true,null, null);
            //Assert
            Assert.AreEqual(2, resultList.Count);
        }

        [TestMethod]
        public void TestUpdateResourceAllocation()
        {
            //Arrange
            IList<CS_Resource> resourceList = new List<CS_Resource>(){
                new CS_Resource() { 
                    Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "DescriptionTest", 
                    Duration=1, EmployeeID = 1, EquipmentID =1, JobID = 1, ModificationDate = DateTime.Now, 
                    ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1, 
                    CS_Equipment = new CS_Equipment(){
                        Name ="Sideboom", 
                        CS_Division = new CS_Division() { ID = 1, Name = "no name", Active = true }
                    },
                    CS_Employee = new CS_Employee() { Name="Ruziska", FirstName="Danilo" }
                },
                new CS_Resource() { 
                    Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "DescriptionTest2", 
                    Duration=1, EmployeeID = 1, EquipmentID =1, JobID = 1, ModificationDate = DateTime.Now, 
                    ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1, 
                    CS_Equipment = new CS_Equipment(){ 
                        Name ="Sideboom", 
                        CS_Division = new CS_Division() { ID = 1, Name = "no name", Active = true }
                    },
                    CS_Employee = new CS_Employee() { Name="Ruziska", FirstName="Danilo" }
                }
            };

            IList<CS_Reserve> reserveList = new List<CS_Reserve>(){
                new CS_Reserve() { 
                    Active = true, CreateBy = "Load", CreationDate = DateTime.Now, DivisionID = 1, Duration = 1, 
                    EmployeeID = 1, EquipmentTypeID = 1, JobID = 1, ModificationDate = DateTime.Now, ModifiedBy = "Load", 
                    StartDateTime = DateTime.Now, Type = 1, 
                    CS_EquipmentType = new CS_EquipmentType() { ID = 1, Name = "no name", Active = true }, 
                    CS_Division = new CS_Division() { ID = 1, Name = "no name", Active = true }
                },
                new CS_Reserve() { 
                    Active = true, CreateBy = "Load", CreationDate = DateTime.Now, DivisionID = 1, Duration = 1, 
                    EmployeeID = 1, EquipmentTypeID = null, JobID = 1, ModificationDate = DateTime.Now, ModifiedBy = "Load", 
                    StartDateTime = DateTime.Now, Type = 1, 
                    CS_Employee = new CS_Employee() { 
                        ID = 1, Name = "no name", Active = true, 
                        CS_Division = new CS_Division() { ID = 1, Name = "no name", Active = true }
                    }
                }
            };

            IList<int> lstDivisions = new List<int>();
            lstDivisions.Add(2);

            FakeObjectSet<CS_Resource> fakeResourceList = new FakeObjectSet<CS_Resource>();
            FakeObjectSet<CS_Reserve> fakeReserveList = new FakeObjectSet<CS_Reserve>();
            FakeObjectSet<CS_EquipmentPermit> fakePermitList = new FakeObjectSet<CS_EquipmentPermit>();
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            FakeObjectSet<CS_CallLogResource> fakeCallLogResourceList = new FakeObjectSet<CS_CallLogResource>();
            FakeObjectSet<CS_CallType> fakeCallTypeList = new FakeObjectSet<CS_CallType>();
            fakeCallTypeList.AddObject(new CS_CallType() { ID = 1, Active = true });

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Resource>()).Returns(fakeResourceList);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Reserve>()).Returns(fakeReserveList);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_EquipmentPermit>()).Returns(fakePermitList);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLogResource>()).Returns(fakeCallLogResourceList);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallType>()).Returns(fakeCallTypeList);

            //Act
            ResourceAllocationModel model = new ResourceAllocationModel(mockUnitOfWork.Object);
            model.SaveOrUpdateResourceAllocation(1, reserveList, resourceList, "druziska", lstDivisions, string.Empty, false, DateTime.Now, false, string.Empty, string.Empty);

            //Assert
            Assert.IsNotNull(model.ResourceSaved);
            Assert.IsNotNull(model.ReserveSaved);
        }

        [TestMethod]
        public void TestSaveResourceAllocation()
        {
            //Arrange
            IList<CS_Resource> resourceList = new List<CS_Resource>(){
                new CS_Resource() { ID=1, Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "DescriptionTest", Duration=1, EmployeeID = 1, EquipmentID =1, 
                    JobID = 1, ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1 },    
                new CS_Resource() { ID = 1, Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "DescriptionTest2", Duration=1, EmployeeID = 1, EquipmentID =1, 
                JobID = 1, ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1 }
            };

            IList<CS_Reserve> reserveList = new List<CS_Reserve>(){
                new CS_Reserve() { ID = 1, Active =true, CreateBy = "Load", CreationDate=DateTime.Now, DivisionID = 1, Duration = 1, EmployeeID = 1, EquipmentTypeID = 1, JobID = 1, 
                    ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1  },
                new CS_Reserve() { ID = 1, Active =true, CreateBy = "Load", CreationDate=DateTime.Now, DivisionID = 1, Duration = 1, EmployeeID = 1, EquipmentTypeID = 1, JobID = 1, 
                ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1  }            
            };

            IList<int> lstDivisions = new List<int>();
            lstDivisions.Add(2);

            //Act
            ResourceAllocationModel model = new ResourceAllocationModel(new FakeUnitOfWork());
            model.SaveOrUpdateResourceAllocation(1, reserveList, resourceList, "druziska", lstDivisions, string.Empty, false, DateTime.Now, false, string.Empty,string.Empty);

            //Assert
            Assert.IsNotNull(model.ResourceSaved);
            Assert.IsNotNull(model.ReserveSaved);
        }

        [TestMethod]
        public void TestClearReservesByJobId()
        {
            //Arrange
            FakeObjectSet<CS_Reserve> fakeReserveList = new FakeObjectSet<CS_Reserve>();
            fakeReserveList.AddObject
            (
                new CS_Reserve()
                    {
                        ID = 1,
                        Active = true,
                        CreateBy = "Load",
                        CreationDate = DateTime.Now,
                        DivisionID = 1,
                        Duration = 1,
                        EmployeeID = 1,
                        EquipmentTypeID = 1,
                        JobID = 1,
                        ModificationDate = DateTime.Now,
                        ModifiedBy = "Load",
                        StartDateTime = DateTime.Now,
                        Type = 1
                    }
            );
            fakeReserveList.AddObject
            (
                new CS_Reserve()
                {
                    ID = 2,
                    Active = true,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    DivisionID = 1,
                    Duration = 1,
                    EquipmentTypeID = 1,
                    JobID = 1,
                    ModificationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    StartDateTime = DateTime.Now,
                    Type = 1
                }
            );
            

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Reserve>()).Returns(fakeReserveList);

            //Act
            ResourceAllocationModel model = new ResourceAllocationModel(mockUnitOfWork.Object);

            IList<CS_Reserve> reserveList = model.ClearReservesByJobId(1, "rbrandao");

            //Assert
            for (int i = 0; i < reserveList.Count; i++)
            {
                Assert.AreEqual(reserveList[i].Active, false);
            }
        }
    }
}
