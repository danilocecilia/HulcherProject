using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class JobCallLogPresenterTest
    {
        [TestMethod]
        public void WhenCallLogTypeIsAutomaticProcessDisableUpdateAndDelete()
        {
            ////Arrange
            //JobCallLogViewFake fake = new JobCallLogViewFake();
            //CS_CallLog callLog = new CS_CallLog() { CS_CallType = new CS_CallType() { IsAutomaticProcess = true } };
            //JobCallLogPresenter presenter = new JobCallLogPresenter(fake);

            ////Act
            //presenter.ValidateCallLogGridLinkButtonVisibility(callLog);

            ////Assert
            //Assert.IsFalse(fake.GetCallLogGridViewHyperLinkVisibility("hlUpdate"));
            //Assert.IsFalse(fake.GetCallLogGridViewHyperLinkVisibility("hlDelete"));
        }
    }
}
