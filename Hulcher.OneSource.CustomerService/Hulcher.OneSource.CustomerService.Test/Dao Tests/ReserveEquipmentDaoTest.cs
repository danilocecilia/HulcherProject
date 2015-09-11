using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.Data;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class ReserveEquipmentDaoTest
    {
        [TestMethod]
        public void TestListAll()
        {
            // Step 1 - Clear tables
            ReserveDao.Singleton.ClearAll();
            ResourceDao.Singleton.ClearAll();
            EquipmentDao.Singleton.ClearAll();
            EquipmentTypeDao.Singleton.ClearAll();
            
            // Step 2 - Insert Controlled data
            CS_EquipmentType equipType = EquipmentTypeDao.Singleton.Add(
                new CS_EquipmentType()
                {
                    Name = "1",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Country country = CountryDao.Singleton.Add(
                new CS_Country()
                {
                    Name = "USA",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_State state = StateDao.Singleton.Add(
                new CS_State()
                {
                    Acronym = "XY",
                    CountryID = country.ID,
                    Name = "XY",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_City city = CityDao.Singleton.Add(
                new CS_City()
                {
                    Name = "city",
                    StateID = state.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_ZipCode zipCode = ZipCodeDao.Singleton.Add(
                new CS_ZipCode()
                {
                    CityId = city.ID,
                    Name = "01234",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Division division = DivisionDao.Singleton.Add(
                new CS_Division()
                {
                    Name = "001",
                    Description = "Division 001",
                    StateID = state.ID,
                    CountryID = country.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Equipment equip1 = EquipmentDao.Singleton.Add(
                new CS_Equipment()
                {
                    Name = "Equipment 1",
                    Description = "Equipment 1",
                    EquipmentTypeID = equipType.ID,
                    DivisionID = division.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_Equipment equip2 = EquipmentDao.Singleton.Add(
                new CS_Equipment()
                {
                    Name = "Equipment 2",
                    Description = "Equipment 2",
                    EquipmentTypeID = equipType.ID,
                    DivisionID = division.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Job job = JobDao.Singleton.Add(
                new CS_Job()
                {
                    Internal_Tracking = "000000001INT",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_LocationInfo location = LocationInfoDao.Singleton.Add(
                new CS_LocationInfo()
                {
                    CountryID = country.ID,
                    StateID = state.ID,
                    CityID = city.ID,
                    ZipCodeId = zipCode.ID,
                    JobID = job.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_JobDivision jobDivision = JobDivisionDao.Singleton.Add(
                new CS_JobDivision()
                {
                    DivisionID = division.ID,
                    JobID = job.ID,
                    IsFromCustomerInfo = false,
                    PrimaryDivision = true,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Reserve reserve = ReserveDao.Singleton.Add(
                new CS_Reserve()
                {
                    JobID = job.ID,
                    Type = 1,
                    EquipmentTypeID = equipType.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Resource resource = ResourceDao.Singleton.Add(
                new CS_Resource()
                {
                    JobID = job.ID,
                    EquipmentID = equip1.ID,
                    Description = "testing",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            // Step 3 - run the query
            IList<CS_View_ReserveEquipment> returnList = ReserveEquipmentDao.Singleton.ListAll();
            Assert.AreEqual(returnList.Count, 1);
            Assert.AreEqual(equipType.ID, returnList[0].EquipmentTypeID);
            Assert.AreEqual(1, returnList[0].Assigned);
            Assert.AreEqual(2, returnList[0].Reserve);
            Assert.AreEqual(1, returnList[0].Available);
        }

        [TestMethod]
        public void TestListFiltered()
        {
            // Step 1 - Clear tables
            ReserveDao.Singleton.ClearAll();
            ResourceDao.Singleton.ClearAll();
            EquipmentDao.Singleton.ClearAll();
            EquipmentTypeDao.Singleton.ClearAll();

            // Step 2 - Insert Controlled data
            CS_EquipmentType equipType = EquipmentTypeDao.Singleton.Add(
                new CS_EquipmentType()
                {
                    Name = "1",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Country country = CountryDao.Singleton.Add(
                new CS_Country()
                {
                    Name = "USA",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_State state = StateDao.Singleton.Add(
                new CS_State()
                {
                    Acronym = "XY",
                    CountryID = country.ID,
                    Name = "XY",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_City city = CityDao.Singleton.Add(
                new CS_City()
                {
                    Name = "city",
                    StateID = state.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_ZipCode zipCode = ZipCodeDao.Singleton.Add(
                new CS_ZipCode()
                {
                    CityId = city.ID,
                    Name = "01234",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Division division = DivisionDao.Singleton.Add(
                new CS_Division()
                {
                    Name = "001",
                    Description = "Division 001",
                    StateID = state.ID,
                    CountryID = country.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Equipment equip1 = EquipmentDao.Singleton.Add(
                new CS_Equipment()
                {
                    Name = "Equipment 1",
                    Description = "Equipment 1",
                    EquipmentTypeID = equipType.ID,
                    DivisionID = division.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_Equipment equip2 = EquipmentDao.Singleton.Add(
                new CS_Equipment()
                {
                    Name = "Equipment 2",
                    Description = "Equipment 2",
                    EquipmentTypeID = equipType.ID,
                    DivisionID = division.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Job job = JobDao.Singleton.Add(
                new CS_Job()
                {
                    Internal_Tracking = "000000001INT",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_LocationInfo location = LocationInfoDao.Singleton.Add(
                new CS_LocationInfo()
                {
                    CountryID = country.ID,
                    StateID = state.ID,
                    CityID = city.ID,
                    ZipCodeId = zipCode.ID,
                    JobID = job.ID,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });
            CS_JobDivision jobDivision = JobDivisionDao.Singleton.Add(
                new CS_JobDivision()
                {
                    DivisionID = division.ID,
                    JobID = job.ID,
                    IsFromCustomerInfo = false,
                    PrimaryDivision = true,
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Reserve reserve = ReserveDao.Singleton.Add(
                new CS_Reserve()
                {
                    JobID = job.ID,
                    Type = 1,
                    EquipmentTypeID = equipType.ID,
                    CreateBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            CS_Resource resource = ResourceDao.Singleton.Add(
                new CS_Resource()
                {
                    JobID = job.ID,
                    EquipmentID = equip1.ID,
                    Description = "testing",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    ModificationDate = DateTime.Now,
                    Active = true
                });

            // Step 3 - run the query
            IList<CS_View_ReserveEquipment> returnList = ReserveEquipmentDao.Singleton.ListAllFiltered(equipType.ID, state.ID, division.ID);
            Assert.AreEqual(returnList.Count, 1);
            Assert.AreEqual(equipType.ID, returnList[0].EquipmentTypeID);
            Assert.AreEqual(1, returnList[0].Assigned);
            Assert.AreEqual(2, returnList[0].Reserve);
            Assert.AreEqual(1, returnList[0].Available);
        }
    }
}
