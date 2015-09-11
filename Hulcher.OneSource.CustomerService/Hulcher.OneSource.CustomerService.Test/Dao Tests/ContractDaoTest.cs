using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Data.Schema.UnitTesting;
using Hulcher.OneSource.CustomerService.Data;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class ContractDaoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CustomerServiceModelContainer ctx = new CustomerServiceModelContainer();

        //    EmployeeDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_STATE");
        //    EmployeeDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_COUNTRY");
        //    EmployeeDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_CUSTOMER");
        //    EmployeeDao.Singleton.ExecuteSql(ctx, "DELETE FROM CS_CUSTOMERCONTRACT");
        }
        [TestMethod]
        public void TestAddNewContract()
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

            CS_CustomerContract customerContractAdded = CustomerContractDao.Singleton.Add(new CS_CustomerContract
            {
                ContractNumber = "12345",
                Description = "Description",
                AdditionalDetails = "Details",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CustomerID = customerAdded.ID,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,                
                Active = true
            });

            CS_CustomerContract customerContractSelected = CustomerContractDao.Singleton.Get(customerContractAdded.ID);
            Assert.AreEqual(customerContractAdded.ID, customerContractSelected.ID);
        }

        [TestMethod]
        public void TestUpdateContract()
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

            CS_CustomerContract customerContractAdded = CustomerContractDao.Singleton.Add(new CS_CustomerContract
            {
                ContractNumber = "12345",
                Description = "Description",
                AdditionalDetails = "Details",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CustomerID = customerAdded.ID,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true
            });

            string randomString = StringManipulation.GenerateRandomString(5);

            CS_CustomerContract customerContractUpdated = new CS_CustomerContract
            {
                ID=customerContractAdded.ID,
                ContractNumber = randomString,
                Description = "Description",
                AdditionalDetails = "Details",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CustomerID = customerAdded.ID,
                CreatedBy = "Load",
                CreationDate = DateTime.Now,
                ModifiedBy = "Load",
                ModificationDate = DateTime.Now,
                Active = true
            };

            CS_CustomerContract contractAfterUpdate = CustomerContractDao.Singleton.Update(customerContractUpdated);
            Assert.AreEqual(randomString, contractAfterUpdate.ContractNumber);
        }

        [TestMethod]
        public void TestListAllContractsByCustomer()
        {
            // Clear Contracts Table
            CustomerContractDao.Singleton.ClearAll();

            CS_Country countryAdded = CountryDao.Singleton.Add(new CS_Country
                {
                    Name = "Country1",
                    CreatedBy = "Load",
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    ModifiedBy = "Load",
                    Active = true
                });

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
                });

            CS_Customer customerAdded2 = CustomerDao.Singleton.Add(new CS_Customer
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
            });

            // Insert 5 itens (3 for Customer 1 and 2 for Customer 2)
            // For those 3 for Customer 1, only 2 will be available
            for (int i = 0; i < 5; i++)
            {
                CS_CustomerContract customerContractAdded = CustomerContractDao.Singleton.Add(new CS_CustomerContract
                    {
                        ContractNumber = i.ToString(),
                        Description = "Description",
                        AdditionalDetails = "Details",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        CustomerID = (i < 3 ? customerAdded.ID : customerAdded2.ID),
                        CreatedBy = "Load",
                        CreationDate = DateTime.Now,
                        ModifiedBy = "Load",
                        ModificationDate = DateTime.Now,
                        Active = (i % 2 == 0 ? true : false)
                    });
            }

            IList<CS_CustomerContract> returnList = CustomerContractDao.Singleton.ListAllByCustomer(customerAdded.ID);
            Assert.AreEqual(returnList.Count, 2); // So, test 2 active items for customer 1
        }
    }
}
