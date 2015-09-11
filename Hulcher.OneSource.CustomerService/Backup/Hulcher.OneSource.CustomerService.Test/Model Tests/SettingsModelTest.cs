using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class SettingsModelTest
    {
        SettingsModel settingsModel;

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TestUpdateJobNumber()
        {
            using (settingsModel = new SettingsModel(new FakeUnitOfWork()))
            {
                int lastJobNumber = settingsModel.GetLastJobNumber();
                settingsModel.UpdateLastJobNumber(lastJobNumber + 1);
                Assert.AreEqual<int>(settingsModel.GetLastJobNumber(), lastJobNumber + 1);
            }
        }

        [TestMethod]
        public void TestUpdateNonJobNumber()
        {
            using (settingsModel = new SettingsModel(new FakeUnitOfWork()))
            {
                int lastNonJobNumber = settingsModel.GetLastNonJobNumber();
                settingsModel.UpdateLastNonJobNumber(lastNonJobNumber + 1);
                Assert.AreEqual<int>(settingsModel.GetLastNonJobNumber(), lastNonJobNumber + 1);
            }
        }
    }
}
