using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class EmailModelTest
    {
        [TestMethod]
        public void SendNotificationForEstimationTeamTest()
        {
            //Arrange
            EmailModel model = new EmailModel(new FakeUnitOfWork());

            #region Body
            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Non-job#:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 000000025INT");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Division:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 005");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("JobType:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" A");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("JobAction:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Environmental Work, General - Undefined Scope of Work");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Scope Of Work:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("xxcxcxc");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job start date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("14/02/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Employee:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dcecilia, Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Engines:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Loads:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 2");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Empties:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            #endregion

            string receipts = "dcecilia@hulcher.com;rbrandao@hulcher.com";

            string subject = "Status Changed - Bid => Active";

            string username = "testing";

            int employeeId = 1;

            IList<CS_Email> savedEmail = null;

            //Act
            savedEmail = model.SaveEmailList(receipts, subject, sb.ToString(), username, employeeId);

            //Assert
            Assert.IsNotNull(savedEmail);
            Assert.AreEqual(savedEmail.Count, 2);
            Assert.AreEqual("dcecilia@hulcher.com", savedEmail[0].Receipts, "Error on Receipts field");
            Assert.AreEqual("rbrandao@hulcher.com", savedEmail[1].Receipts, "Error on Receipts field");
            Assert.AreEqual(subject, savedEmail[0].Subject, "Error on Subject field");
            Assert.AreEqual(sb.ToString(), savedEmail[0].Body, "Error on Body field");
            Assert.AreEqual(username, savedEmail[0].CreatedBy, "Error on CreatedBy field");
            Assert.AreEqual(employeeId, savedEmail[0].CreationID, "Error on CreationID field");
            Assert.AreEqual((short)Globals.EmailService.Status.Pending, savedEmail[0].Status, "Error on Status Field");
        }

        [TestMethod]
        public void SendNotificationForInvoicingTeamTest()
        {
            //Arrange
            EmailModel model = new EmailModel(new FakeUnitOfWork());

            #region Body
            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job#:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 243");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Customer:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" American Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Customer Contact:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" danilo");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Bill to:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" cecilia, danilo");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Call date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 14/02/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Initial Call time:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 10:11:59");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Price Type:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" description test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Action:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Environmental Work, General - Undefined Scope of Work");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Category:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" B");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job Type:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" A");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Division:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 005");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Interim Bill:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Yes");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Requested By:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dcecilia, Test");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Frequency:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" D");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Country:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" USA");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("State:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Texas");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("City:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" Dalton");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Engines:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Loads:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 2");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Number Empties:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Scope Of Work:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("xxcxcxc");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job start date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 14/02/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Job end date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append(" 14/02/2011 00:00:00");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            #endregion

            string receipts = "dcecilia@hulcher.com;rbrandao@hulcher.com";

            string subject = "Status Changed - Cancel => Active";

            string username = "testing";

            int employeeId = 1;

            IList<CS_Email> savedEmail = null;

            //Act
            savedEmail = model.SaveEmailList(receipts, subject, sb.ToString(), username, employeeId);

            //Assert
            Assert.IsNotNull(savedEmail);
            Assert.AreEqual(savedEmail.Count, 2);
            Assert.AreEqual("dcecilia@hulcher.com", savedEmail[0].Receipts, "Error on Receipts field");
            Assert.AreEqual("rbrandao@hulcher.com", savedEmail[1].Receipts, "Error on Receipts field");
            Assert.AreEqual(subject, savedEmail[0].Subject, "Error on Subject field");
            Assert.AreEqual(sb.ToString(), savedEmail[0].Body, "Error on Body field");
            Assert.AreEqual(username, savedEmail[0].CreatedBy, "Error on CreatedBy field");
            Assert.AreEqual(employeeId, savedEmail[0].CreationID, "Error on CreationID field");
            Assert.AreEqual((short)Globals.EmailService.Status.Pending, savedEmail[0].Status, "Error on Status Field");
        }

        [TestMethod]
        public void TestIfModelIsSavingEmailList()
        {
            // Arrange
            EmailModel model = new EmailModel(new FakeUnitOfWork());
            string receipts = "user1@hulcher.com;user2@hulcher.com;user3@hulcher.com";
            string subject = "testing email save";
            string body = "this is a test";
            string username = "testing";
            int employeeId = 1;
            IList<CS_Email> savedEmail = null;

            // Act
            savedEmail = model.SaveEmailList(receipts, subject, body, username, employeeId);

            // Assert
            Assert.IsNotNull(savedEmail);
            Assert.AreEqual(savedEmail.Count, 3);
            Assert.AreEqual("user1@hulcher.com", savedEmail[0].Receipts, "Error on Receipts field");
            Assert.AreEqual("user2@hulcher.com", savedEmail[1].Receipts, "Error on Receipts field");
            Assert.AreEqual("user3@hulcher.com", savedEmail[2].Receipts, "Error on Receipts field");
            Assert.AreEqual(subject, savedEmail[0].Subject, "Error on Subject field");
            Assert.AreEqual(body, savedEmail[0].Body, "Error on Body field");
            Assert.AreEqual(username, savedEmail[0].CreatedBy, "Error on CreatedBy field");
            Assert.AreEqual(employeeId, savedEmail[0].CreationID, "Error on CreationID field");
            Assert.AreEqual((short)Globals.EmailService.Status.Pending, savedEmail[0].Status, "Error on Status Field");
        }

        [TestMethod]
        public void TestIfModelIsSavingEmail()
        {
            // Arrange
            EmailModel model = new EmailModel(new FakeUnitOfWork());
            string receipts = "user1@hulcher.com";
            string subject = "testing email save";
            string body = "this is a test";
            string username = "testing";
            int employeeId = 1;
            CS_Email savedEmail = null;

            // Act
            savedEmail = model.SaveEmail(receipts, subject, body, username, employeeId);

            // Assert
            Assert.IsNotNull(savedEmail);
            Assert.AreEqual("user1@hulcher.com", savedEmail.Receipts, "Error on Receipts field");
            Assert.AreEqual(subject, savedEmail.Subject, "Error on Subject field");
            Assert.AreEqual(body, savedEmail.Body, "Error on Body field");
            Assert.AreEqual(username, savedEmail.CreatedBy, "Error on CreatedBy field");
            Assert.AreEqual(employeeId, savedEmail.CreationID, "Error on CreationID field");
            Assert.AreEqual((short)Globals.EmailService.Status.Pending, savedEmail.Status, "Error on Status Field");
        }

        [TestMethod]
        public void TestIfModelIsUpdatingEmailStatus()
        {
            // Arrange
            CS_Email email = new CS_Email()
            {
                ID = 1,
                Active = true,
                Status = (short)Globals.EmailService.Status.Pending
            };
            EmailModel model = new EmailModel(new FakeUnitOfWork());
            CS_Email updatedEmail = null;
            
            // Act
            updatedEmail = model.UpdateStatusEmail(email, Globals.EmailService.Status.Sent);

            // Assert
            Assert.IsNotNull(updatedEmail);
            Assert.AreEqual((short)Globals.EmailService.Status.Sent, updatedEmail.Status, "Error on Status field");
        }

        [TestMethod]
        public void TestIfModelIsReturningPendingEmails()
        {
            // Arrange
            FakeObjectSet<CS_Email> fakeEmailList = new FakeObjectSet<CS_Email>();
            fakeEmailList.AddObject(new CS_Email() { ID = 1, Status = (short)Globals.EmailService.Status.Pending, Active = true });
            fakeEmailList.AddObject(new CS_Email() { ID = 2, Status = (short)Globals.EmailService.Status.Pending, Active = true });
            fakeEmailList.AddObject(new CS_Email() { ID = 3, Status = (short)Globals.EmailService.Status.Sent, Active = true });
            fakeEmailList.AddObject(new CS_Email() { ID = 4, Status = (short)Globals.EmailService.Status.Error, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Email>()).Returns(fakeEmailList);
            EmailModel model = new EmailModel(mockUnitOfWork.Object);

            // Act
            IList<CS_Email> pendingEmails = model.ListAllPendingEmails();

            // Assert
            Assert.IsNotNull(pendingEmails);
            Assert.AreEqual(2, pendingEmails.Count);
        }
    }
}
