using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Data.Schema.UnitTesting;
using Hulcher.OneSource.CustomerService.Data;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class CustomerDaoTest
    {
        private const int CONSULTING_ID = 1;
        private const int NUMBER_ROWS_GENERATED = 2;
        private CS_Customer _foundCustomer;

        /// <summary>
        /// Prepare the database generating sample data
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //DatabaseTestClass.TestService.DeployDatabaseProject();
            //DatabaseTestClass.TestService.GenerateData();
        }

        /// <summary>
        /// Test the Get Method from DAO
        /// </summary>
        //[TestMethod] TODO:Fix Customer table in OneSourceTest to run this test
        public void TestGetCustomerMethodReturn()
        {
            _foundCustomer = CustomerDao.Singleton.Get(1);
            Assert.AreEqual(CONSULTING_ID, _foundCustomer.ID);
        }

        //[TestMethod] TODO:Fix Customer table in OneSourceTest to run this test
        public void TestListAllCustomerMethodReturn()
        {            
            var customerList = CustomerDao.Singleton.ListAll();
            Assert.AreEqual(NUMBER_ROWS_GENERATED, customerList.Count());
        }

        [TestMethod]
        public void TestAddNewCustomer()
        {
            CS_Country countryAdded = CountryDao.Singleton.Add(new CS_Country 
            {
                Name = "Country1",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load",
                Active = true
            }
            ); 

            CS_Customer customerAdded = CustomerDao.Singleton.Add(new CS_Customer 
            { 
                CustomerNumber = null,
                Name = null,
                Address1 = null,
                Address2 = null,
                State = null,
                City = null,
                Country = null,
                Zip = null,
                Phone = null,
                Fax = null,
                Email = null,
                BillName = null,
                BillAddress1 = null,
                BillAddress2 = null,
                BillAttn = null,
                BillState = null,
                BillCity = null,
                BillCountry = null,
                BillPhone = null,
                BillFax = null,
                BillSalutation = null,
                BillThruProject =null,
                BillZip = null,
                CountryID = countryAdded.ID,
                Xml = null,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true
            }
            );
            CS_Customer customerSelected = CustomerDao.Singleton.Get(customerAdded.ID);
            Assert.AreEqual(customerAdded.ID, customerSelected.ID);
        }

        [TestMethod]
        public void TestUpdateCustomer()
        {
            CS_Country countryAdded = CountryDao.Singleton.Add(new CS_Country
            {
                Name = "Country1",
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = "Load",
                Active = true
            }
           );

            CS_Customer customerAdded = CustomerDao.Singleton.Add(new CS_Customer
            {
                CustomerNumber = null,
                Name = null,
                Address1 = null,
                Address2 = null,
                State = null,
                City = null,
                Country = null,
                Zip = null,
                Phone = null,
                Fax = null,
                Email = null,
                BillName = null,
                BillAddress1 = null,
                BillAddress2 = null,
                BillAttn = null,
                BillState = null,
                BillCity = null,
                BillCountry = null,
                BillPhone = null,
                BillFax = null,
                BillSalutation = null,
                BillThruProject = null,
                BillZip = null,
                CountryID = countryAdded.ID,
                Xml = null,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true
            }
            );

            Guid randomValue = Guid.NewGuid();

            CS_Customer customerUpdated = new CS_Customer()
            {
                ID=customerAdded.ID,
                CustomerNumber = randomValue.ToString(),
                Name = null,
                Address1 = null,
                Address2 = null,
                State = null,
                City = null,
                Country = null,
                Zip = null,
                Phone = null,
                Fax = null,
                Email = null,
                BillName = null,
                BillAddress1 = null,
                BillAddress2 = null,
                BillAttn = null,
                BillState = null,
                BillCity = null,
                BillCountry = null,
                BillPhone = null,
                BillFax = null,
                BillSalutation = null,
                BillThruProject = null,
                BillZip = null,
                CountryID = countryAdded.ID,
                Xml = null,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true
            };

            CS_Customer customerAfterUpdate = CustomerDao.Singleton.Update(customerUpdated);
            Assert.AreEqual(randomValue.ToString(), customerAfterUpdate.CustomerNumber);
        }
    }
}
