using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Integration;
using System.Data;

namespace Hulcher.OneSource.CustomerService.Test.IVantageIntegrationTests
{
    [TestClass]
    public class IVantageIntegrationTest
    {
        const int EXPECTEDEMPLOYEECOUNT = 3;
        const int EXPECTEDEMERGENCYCONTACTSCOUNT = 0;
        const int EXPECTEDDIVISIONCOUNT = 3;

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TestGetAllEmployees()
        {
            try
            {
                IDataReader reader = IVantageIntegration.Singleton.ListAllEmployees();
                int _innerCount = 0;

                while (reader.Read())
                {
                    _innerCount++;
                }

                Assert.AreEqual(EXPECTEDEMPLOYEECOUNT, _innerCount);
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGetAllEmergencyContacts()
        {
            try
            {
                IDataReader reader = IVantageIntegration.Singleton.ListAllEmergencyContacts();
                int _innerCount = 0;

                while (reader.Read())
                {
                    _innerCount++;
                }

                Assert.AreEqual(EXPECTEDEMERGENCYCONTACTSCOUNT, _innerCount);
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGetAllDivisions()
        {
            try
            {
                IDataReader reader = IVantageIntegration.Singleton.ListAllDivisions();
                int _innerCount = 0;

                while (reader.Read())
                {
                    _innerCount++;
                }

                Assert.AreEqual(EXPECTEDDIVISIONCOUNT, _innerCount);
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
