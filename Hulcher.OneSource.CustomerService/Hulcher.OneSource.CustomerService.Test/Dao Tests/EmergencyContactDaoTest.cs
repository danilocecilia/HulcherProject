using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Hulcher.OneSource.CustomerService.Integration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Data;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class EmergencyContactDaoTest
    {
        [TestMethod]
        public void TestBulkCopyAllEmergencyContacts()
        {
            try
            {
                EmergencyContactDao.Singleton.BulkCopyAllEmergencyContacts(IVantageIntegration.Singleton.ListAllEmergencyContacts());
                //The following line will only be executed if the Deposit method
                //failed to throw an exception.
                Assert.Fail("Expected Exception was not thrown");
            }
            catch (Exception)
            {
                //Ok, this is an expected exception.
            }
        }
    }
}
