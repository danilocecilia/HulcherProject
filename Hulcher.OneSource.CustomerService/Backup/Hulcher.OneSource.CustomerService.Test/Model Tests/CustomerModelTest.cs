using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class CustomerModelTest
    {
        [TestMethod]
        public void TestListAllFilteredContacts()
        {
            //Arrange
            CustomerModel model = new CustomerModel(new FakeUnitOfWork());
            //Act
            IList<CS_Contact> resultList1 = model.ListAllFilteredContacts(1, true);
            //Assert
            Assert.AreEqual(2, resultList1.Count);
        }

        [TestMethod]
        public void TestGetContactById()
        {
            //Arrange
            CustomerModel model = new CustomerModel(new FakeUnitOfWork());
            //Act
            CS_Contact result = model.GetContactById(1);
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
