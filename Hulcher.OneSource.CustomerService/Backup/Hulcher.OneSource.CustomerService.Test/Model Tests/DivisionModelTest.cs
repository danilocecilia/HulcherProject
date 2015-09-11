using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class DivisionModelTest
    {
        [TestMethod]
        public void TestListAllFilteredDivisionByName()
        {
            // Arrange
            DivisionModel model = new DivisionModel(new FakeUnitOfWork());
            // Act
            IList<CS_Division> result = model.ListAllFilteredDivisionByName("1");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }
    }
}
