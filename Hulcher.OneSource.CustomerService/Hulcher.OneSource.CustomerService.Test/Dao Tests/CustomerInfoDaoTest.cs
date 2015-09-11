using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Dao_Tests
{
    [TestClass]
    public class CustomerInfoDaoTest
    {
        CS_CustomerInfo _customerInfo;

        [TestInitialize]
        public void Initialize()
        {
            //DatabaseTestClass.TestService.GenerateData();

            _customerInfo = new CS_CustomerInfo()
            {
                
            };
        }

        [TestMethod]
        public void TestSaveEntity()
        {

        }
    }
}
