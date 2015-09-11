using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class FirstAlertPresenterTest
    {
        [TestMethod]
        public void MustEnableDeleteLinkIfUserHasPermissionToDeleteFirstAlert()
        {
            //Arrange
            Mock<IFirstAlertView> mockView = new Mock<IFirstAlertView>();
            Mock<AZManager> mockAzManager = new Mock<AZManager>();   
            Globals.Security.Operations[] operations = new Globals.Security.Operations[1];
            operations[0] = Globals.Security.Operations.FirstAlertDelete;            
            mockView.SetupProperty(e => e.DeletePermission, false);            
            mockAzManager.Setup(e => e.CheckAccessById(null, null, operations)).Returns(new AZOperation[] { new AZOperation() { ID = 8, Name = "First Alert Delete", Result = true } });            
            //Act
            FirstAlertPresenter presenter = new FirstAlertPresenter(mockView.Object, mockAzManager.Object);
            presenter.VerifyDeletePermission();
            //Assert
            Assert.IsTrue(mockView.Object.DeletePermission);
        }

        [TestMethod]
        public void MustFillFirstAlertHeaderWithEntityObject()
        {
            //Arrange
            Mock<IFirstAlertView> mockView = new Mock<IFirstAlertView>();
            Mock<FirstAlertModel>  mockModel = new Mock<FirstAlertModel>();
            System.Data.Objects.DataClasses.EntityCollection<CS_FirstAlertDivision> divisions = new System.Data.Objects.DataClasses.EntityCollection<CS_FirstAlertDivision>();
            divisions.Add(new CS_FirstAlertDivision() { CS_Division = new CS_Division() {ID=1, Name="001"}});
            divisions.Add(new CS_FirstAlertDivision() { CS_Division = new CS_Division() {ID=1, Name="002"}});
            CS_FirstAlert firstAlertStub =  new CS_FirstAlert()
                { 
                    ID=1,
                   CS_Job = new CS_Job() { ID=1, Number = "1234"},
                    CS_Customer = new CS_Customer() {ID=2, Name = "Customer 1" },
                    CS_Employee_InCharge = new CS_Contact() { ID = 2, Name = "Peter", LastName = "Parker", Active = true},
                    CS_FirstAlertDivision = divisions,
                    Date = new DateTime(2011, 7, 12, 5, 0, 0),
                    CS_Country = new CS_Country() { ID = 1, Name = "USA" },
                    CS_State = new CS_State() { ID = 1, Name = "Florida" },
                    CS_City = new CS_City() { ID = 1, Name = "Miami" },
                    ReportedBy = "danilo",
                    CS_Employee_CompletedBy = new CS_Employee() { ID = 1, FirstName = "Danilo", Name = "Ruziska" },
                    Details = "details",
                    HasPoliceReport = true,
                    PoliceAgency = "agency",
                    PoliceReportNumber = "1234"
                } ;
            mockView.SetupProperty(e => e.FirstAlertID, 1);
            mockView.SetupProperty(e => e.FirstAlertEntity, null);
            mockModel.Setup(e => e.GetFirstAlertById(1)).Returns(firstAlertStub);
            FirstAlertPresenter presenter = new FirstAlertPresenter(mockView.Object, mockModel.Object);
            //Act
            presenter.FillFirstAlertHeaderFields();
            //Assert
            Assert.AreEqual(firstAlertStub.CS_Job.Number,  mockView.Object.FirstAlertEntity.CS_Job.Number);
            Assert.AreEqual(firstAlertStub.CS_Customer.ID, mockView.Object.FirstAlertEntity.CS_Customer.ID);
            Assert.AreEqual(firstAlertStub.CS_Employee_InCharge.ID,  mockView.Object.FirstAlertEntity.CS_Employee_InCharge.ID);
            Assert.AreEqual(firstAlertStub.CS_FirstAlertDivision.Count,  mockView.Object.FirstAlertEntity.CS_FirstAlertDivision.Count);
            Assert.AreEqual(firstAlertStub.Date, mockView.Object.FirstAlertEntity.Date);
            Assert.AreEqual(firstAlertStub.CS_Country.ID,  mockView.Object.FirstAlertEntity.CS_Country.ID);
            Assert.AreEqual(firstAlertStub.CS_City.ID, mockView.Object.FirstAlertEntity.CS_City.ID);
            Assert.AreEqual(firstAlertStub.CS_State.ID, mockView.Object.FirstAlertEntity.CS_State.ID);
            Assert.AreEqual(firstAlertStub.ReportedBy, mockView.Object.FirstAlertEntity.ReportedBy);
            Assert.AreEqual(firstAlertStub.CS_Employee_CompletedBy.ID, mockView.Object.FirstAlertEntity.CS_Employee_CompletedBy.ID);
            Assert.AreEqual(firstAlertStub.ReportedBy, mockView.Object.FirstAlertEntity.ReportedBy);
            Assert.AreEqual(firstAlertStub.Details, mockView.Object.FirstAlertEntity.Details);
            Assert.AreEqual(firstAlertStub.HasPoliceReport, mockView.Object.FirstAlertEntity.HasPoliceReport);
            Assert.AreEqual(firstAlertStub.PoliceAgency,  mockView.Object.FirstAlertEntity.PoliceAgency);
            Assert.AreEqual(firstAlertStub.PoliceReportNumber, mockView.Object.FirstAlertEntity.PoliceReportNumber);
        }
    }
}
