using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Integration;
using Hulcher.OneSource.CustomerService.Integration.Entities;
using System.Data;

namespace Hulcher.OneSource.CustomerService.Test.DynamicsIntegrationTests
{
    [TestClass]
    public class DynamicsIntegrationTest
    {
        DateTime? _LastUpdateCustomer;
        DateTime? _LastUpdateContacts;
        DateTime? _LastUpdateContracts;

        [TestInitialize]
        public void Initialize()
        {
            _LastUpdateCustomer = new DateTime(2010, 1, 1);
            _LastUpdateContacts = new DateTime(2010, 1, 1);
            _LastUpdateContracts = new DateTime(2010, 1, 1);
        }

        [TestMethod]
        public void TestGetAllCustomers()
        {
            try
            {
                List<IDataReader> readerList = DynamicsIntegration.Singleton.ListAllCustomers(_LastUpdateCustomer);

                //Assert.AreEqual(3, customers.Count);
            }
            catch
            {
                Assert.Fail("Transaction error.");
            }
        }

        [TestMethod]
        public void TestGetAllContacts()
        {
            try
            {
                List<IDataReader> readerList = DynamicsIntegration.Singleton.ListAllContacts(_LastUpdateContacts);

                //Assert.AreEqual(2, contacts.Count);
            }
            catch
            {
                Assert.Fail("Transaction error.");
            }
        }
    }
}
