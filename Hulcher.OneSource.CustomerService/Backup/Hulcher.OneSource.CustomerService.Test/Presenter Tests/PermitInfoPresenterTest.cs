using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class PermitInfoPresenterTest
    {
        private int LIST_COUNT_TO_TEST = 1;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void TestFileUpload()
        {
            FileInfo fileInfo = new FileInfo(Path.GetFullPath("UploadSample.txt"));

            Mock<IPermitInfoView> view = new Mock<IPermitInfoView>();
            view.Setup(viewSetup => viewSetup.FileName).Returns("UploadSample.txt");
            view.Setup(viewSetup => viewSetup.UploadedFile).Returns(fileInfo.OpenRead());
            view.Setup(viewSetup => viewSetup.UploadFolder).Returns(Path.GetTempPath());
            view.SetupProperty(viewSetup => viewSetup.FullFilePath);
            
            PermitInfoPresenter presenter = new PermitInfoPresenter(view.Object);
            presenter.SaveFile();

            Assert.IsTrue(System.IO.File.Exists(view.Object.FullFilePath));
        }

        [TestMethod]
        public void TestAddPermit()
        {
            Mock<IPermitInfoView> view = new Mock<IPermitInfoView>();
            view.Setup(viewSetup => viewSetup.PermitInfoEntity).Returns(
                new DataContext.CS_JobPermit()
                {
                    Type = 1,
                    Number = "ABC123",
                    Location = "Denton, TX",
                    CreationDate = DateTime.Now,
                    CreatedBy = "ksantos",
                    FileName = "UploadSample.txt",
                    Path = "/Uploads/Permits/20101208190800_UploadSample.txt",
                    Active = true,
                    ID = 1,
                    AgencyOperator = "Agency",
                    AgentOperatorName = "David Jones",
                    PermitDate = DateTime.Now
                }
            );
            view.Setup(viewSetup => viewSetup.PermitInfoEntityList).Returns(new List<DataContext.CS_JobPermit>());

            PermitInfoPresenter presenter = new PermitInfoPresenter(view.Object);
            presenter.AddPermit();

            Assert.AreEqual(LIST_COUNT_TO_TEST, view.Object.PermitInfoEntityList.Count);
        }

        [TestMethod]
        public void TestRemovePermit()
        {
            Mock<IPermitInfoView> view = new Mock<IPermitInfoView>();
            view.Setup(viewSetup => viewSetup.RemoveIndex).Returns(0);                
            view.Setup(viewSetup => viewSetup.PermitInfoEntityList).Returns(new List<DataContext.CS_JobPermit>()
                {
                    new DataContext.CS_JobPermit() { ID = 1 },
                    new DataContext.CS_JobPermit() { ID = 2 }
                }
            );

            PermitInfoPresenter presenter = new PermitInfoPresenter(view.Object);
            presenter.RemovePermit();

            Assert.AreEqual(LIST_COUNT_TO_TEST, view.Object.PermitInfoEntityList.Count);
        }
    }
}
