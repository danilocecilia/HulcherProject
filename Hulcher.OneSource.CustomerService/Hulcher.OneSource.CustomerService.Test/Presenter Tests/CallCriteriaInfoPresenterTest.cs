using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class CallCriteriaInfoPresenterTest
    {
        [TestMethod]
        public void TestIfPrimaryCallTypesFieldsAreBeingFilled()
        {
            // Arrange
            EntityCollection<CS_PrimaryCallType_CallType> primaryCallTypeReference = new EntityCollection<CS_PrimaryCallType_CallType>();
            primaryCallTypeReference.Add(
                new CS_PrimaryCallType_CallType()
                {
                    ID = 1,
                    PrimaryCallTypeID = 1,
                    CallTypeID = 1,
                    CS_CallType = new CS_CallType()
                    {
                        ID = 1,
                        Description = "Call Type 1",
                        Active = true
                    }
                });
            primaryCallTypeReference.Add(
                new CS_PrimaryCallType_CallType()
                {
                    ID = 2,
                    PrimaryCallTypeID = 1,
                    CallTypeID = 2,
                    CS_CallType = new CS_CallType()
                    {
                        ID = 2,
                        Description = "Call Type 2",
                        Active = true
                    }
                });
            CS_PrimaryCallType primaryCallTypeRepeaterDataItem = new CS_PrimaryCallType()
            {
                ID = 1,
                Type = "Primary Call Type",
                Active = true,
                CS_PrimaryCallType_CallType = primaryCallTypeReference
            };
            
            Mock<ICallCriteriaInfoView> view = new Mock<ICallCriteriaInfoView>();
            view.SetupProperty(m => m.PrimaryCallTypeRepeaterDataItem, primaryCallTypeRepeaterDataItem);
            view.SetupProperty(m => m.PrimaryCallTypeRepeaterRowDescription, string.Empty);
            view.SetupProperty(m => m.PrimaryCallTypeRepeaterRowCallTypeList, new List<CS_PrimaryCallType_CallType>());

            // Act
            CallCriteriaInfoPresenter presenter = new CallCriteriaInfoPresenter(view.Object);
            presenter.FillPrimaryCallTypeRow();

            // Assert
            Assert.AreEqual("Primary Call Type", view.Object.PrimaryCallTypeRepeaterRowDescription);
            Assert.AreEqual(2, view.Object.PrimaryCallTypeRepeaterRowCallTypeList.Count);
        }
    }
}
