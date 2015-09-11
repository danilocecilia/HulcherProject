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
    public class DossierIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TestGetAllEquipments()
        {
            try
            {
                IDataReader reader = DossierIntegration.Singleton.ListAllEquipments();
                int _innerCount = 0;

                while (reader.Read())
                {
                    _innerCount++;
                }

                Assert.IsTrue(_innerCount > 0);
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
