using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class LocationModelTest
    {
        [TestMethod]
        public void TestListCityByNameAndState()
        {
            // Arrange
            LocationModel model = new LocationModel(new FakeUnitOfWork());
            // Act
            IList<CS_City> result = model.ListCityByNameAndState(2, "City 1");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestListCityByNameWithoutState()
        {
            //Arrange 
            FakeObjectSet<CS_City> fakeCityList = new FakeObjectSet<CS_City>();
            fakeCityList.AddObject(new CS_City() { ID = 1, Active = true, Name = "City 1" });
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(e => e.CreateObjectSet<CS_City>()).Returns(fakeCityList);
            LocationModel model = new LocationModel(mock.Object);

            //Act
            IList<CS_City> city = model.ListCityByNameAndState(0, "City 1");

            //Assert
            Assert.AreEqual(1, city.Count);
            Assert.IsNotNull(city);
        }

        [TestMethod]
        public void TestListZipCodeByNameAndCity()
        {
            // Arrange
            LocationModel model = new LocationModel(new FakeUnitOfWork());
            // Act
            IList<CS_ZipCode> result = model.ListZipCodeByNameAndCity(2, "001");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestListZipCodeByNameWithoutCity()
        {
            // Arrange
            LocationModel model = new LocationModel(new FakeUnitOfWork());
            // Act
            IList<CS_ZipCode> result = model.ListZipCodeByNameAndCity(0, "001");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void ShouldReturnFullStateNameFromPrefix()
        {
            //Arrange
            bool result = true;
            LocationModel model = new LocationModel(new FakeUnitOfWork());

            //Act
            IList<CS_State> states = model.ListAllStatesByName("New");

            for (int i = 0; i < states.Count; i++)
            {
                if (!states[i].Name.ToLower().Contains("new"))
                {
                    result = false;
                    break;
                }
            }

            //Assert
            Assert.IsTrue(result);
        }
    }
}
