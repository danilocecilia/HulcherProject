using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class CallEntryPresenterTest
    {
        [TestMethod]
        public void ShouldMakePanelVisibleWhenPrimaryCallTypeIsResourceUpdate()
        {
            //Arrange
            Mock<ICallEntryView> mock = new Mock<ICallEntryView>();
            mock.Setup(ce => ce.SelectedPrimaryCallType).Returns(new DataContext.CS_PrimaryCallType() { ID = 4, Active = true });
            mock.SetupProperty(c => c.ResourceAssignedVisibility, false);
            CallEntryPresenter presenter = new CallEntryPresenter(mock.Object);

            //Act
            presenter.ValidatePrimaryCallType(true);

            //Assert
            Assert.IsTrue(mock.Object.ResourceAssignedVisibility);
        }

        [TestMethod]
        public void LoadingInitialAdviseInformationShouldMakeOtherPanelsInvisible()
        {
            Mock<ICallEntryView> mock = new Mock<ICallEntryView>();            
            mock.SetupProperty(c => c.InitialAdviseVisibility, false);
            mock.SetupProperty(c => c.ResourceAssignedVisibility , true);
            mock.SetupProperty(c => c.PersonsAdviseVisibility, true);
            mock.SetupProperty(c => c.CallLogHistoryPanelVisibility, true);
            mock.SetupProperty(c => c.CallLogInitialAdviseHistoryVisibility, false);
            CallEntryPresenter presenter = new CallEntryPresenter(mock.Object);

            presenter.SetInitialAdvisePanels();

            Assert.IsTrue(mock.Object.InitialAdviseVisibility);
            Assert.IsFalse(mock.Object.ResourceAssignedVisibility);
            Assert.IsFalse(mock.Object.PersonsAdviseVisibility);
            Assert.IsFalse(mock.Object.CallLogHistoryPanelVisibility);
            Assert.IsTrue(mock.Object.CallLogInitialAdviseHistoryVisibility);
        }
    }
}
