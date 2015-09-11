using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class DashboardPrintPresenterTest
    {
        //[TestMethod]
        //public void TestLoadSearchFilterPanel()
        //{
        //    // Arrange
        //    Mock<IDashboardPrintView> mockView = new Mock<IDashboardPrintView>();
        //    mockView.SetupProperty(e => e.ContactFilter, "None");
        //    mockView.SetupProperty(e => e.ContactFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.JobFilter, "None");
        //    mockView.SetupProperty(e => e.JobFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.LocationFilter, "None");
        //    mockView.SetupProperty(e => e.LocationFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.JobDescriptionFilter, "None");
        //    mockView.SetupProperty(e => e.JobDescriptionFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.EquipmentTypeFilter, "None");
        //    mockView.SetupProperty(e => e.EquipmentTypeFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.ResourceFilter, "None");
        //    mockView.SetupProperty(e => e.ResourceFilterValue, string.Empty);
        //    mockView.SetupProperty(e => e.DateRangeBeginValue, new DateTime(2011, 6, 12));
        //    mockView.SetupProperty(e => e.DateRangeEndValue, new DateTime(2011, 6, 17, 23, 59, 59));
        //    mockView.SetupProperty(e => e.SearchContactInfoLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchJobInfoLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchLocationInfoLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchJobDescriptionLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchEquipmentTypeLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchResourceLabel, string.Empty);
        //    mockView.SetupProperty(e => e.SearchDateRangeLabel, string.Empty);

        //    // Act
        //    DashboardPrintPresenter presenter = new DashboardPrintPresenter(mockView.Object);
        //    presenter.LoadSearchFilterPanel();

        //    // Assert
        //    Assert.AreEqual("Contact Info: None", mockView.Object.SearchContactInfoLabel);
        //    Assert.AreEqual("Job Info: None", mockView.Object.SearchJobInfoLabel);
        //    Assert.AreEqual("Location Info: None", mockView.Object.SearchLocationInfoLabel);
        //    Assert.AreEqual("Job Description: None", mockView.Object.SearchJobDescriptionLabel);
        //    Assert.AreEqual("Equipment Type: None", mockView.Object.SearchEquipmentTypeLabel);
        //    Assert.AreEqual("Resource: None", mockView.Object.SearchResourceLabel);
        //    Assert.AreEqual(
        //        string.Format("Date Range: from {0} to {1}",
        //            new DateTime(2011, 6, 12).ToString("MM/dd/yyyy"),
        //            new DateTime(2011, 6, 17, 23, 59, 59).ToString("MM/dd/yyyy")), 
        //        mockView.Object.SearchDateRangeLabel); 
        //}
    }
}
