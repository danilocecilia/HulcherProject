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
    public class PhotoReportPresenterTest
    {
        private int LIST_COUNT_TO_TEST = 1;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [DeploymentItem(@"Logo.png")]
        public void TestFileUploadOK()
        {
            FileInfo fileInfo = new FileInfo(Path.GetFullPath("Logo.png"));
            Mock<IPhotoReportView> view = new Mock<IPhotoReportView>();
            view.Setup(viewSetup => viewSetup.FileName).Returns("Logo.png");
            view.Setup(viewSetup => viewSetup.UploadedFile).Returns(fileInfo.OpenRead());
            view.Setup(viewSetup => viewSetup.UploadFolder).Returns(Path.GetTempPath());
            view.SetupProperty(viewSetup => viewSetup.FullFilePath);

            PhotoReportPresenter presenter = new PhotoReportPresenter(view.Object);
            presenter.SaveFile();

            Assert.IsTrue(System.IO.File.Exists(view.Object.FullFilePath));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Only files with extension .png, .bmp, .jpg, .tiff or .gif are allowed")]
        public void TestFileUploadError()
        {
            FileInfo fileInfo = new FileInfo(Path.GetFullPath("UploadSample.txt"));

            Mock<IPhotoReportView> view = new Mock<IPhotoReportView>();
            view.Setup(viewSetup => viewSetup.FileName).Returns("UploadSample.txt");
            view.Setup(viewSetup => viewSetup.UploadedFile).Returns(fileInfo.OpenRead());
            view.Setup(viewSetup => viewSetup.UploadFolder).Returns(Path.GetTempPath());
            view.SetupProperty(viewSetup => viewSetup.FullFilePath);

            PhotoReportPresenter presenter = new PhotoReportPresenter(view.Object);
            presenter.SaveFile();
        }

        [TestMethod]
        public void TestAddPhotoReport()
        {
            Mock<IPhotoReportView> view = new Mock<IPhotoReportView>();
            view.Setup(viewSetup => viewSetup.PhotoReportEntity).Returns(
                new DataContext.CS_JobPhotoReport()
                {
                    Description = "Unit Test",
                    CreationDate = DateTime.Now,
                    CreatedBy = "ksantos",
                    FileName = "Logo.png",
                    Path = "/Uploads/Permits/20101208190800_Logo.png",
                    Active = true,
                    ID = 1
                }
            );
            view.Setup(viewSetup => viewSetup.PhotoReportList).Returns(new List<DataContext.CS_JobPhotoReport>());

            PhotoReportPresenter presenter = new PhotoReportPresenter(view.Object);
            presenter.AddPhotoReport();

            Assert.AreEqual(LIST_COUNT_TO_TEST, view.Object.PhotoReportList.Count);
        }

        [TestMethod]
        public void TestRemovePhotoReport()
        {
            Mock<IPhotoReportView> view = new Mock<IPhotoReportView>();
            view.Setup(viewSetup => viewSetup.RemoveIndex).Returns(0);
            view.Setup(viewSetup => viewSetup.PhotoReportList).Returns(new List<DataContext.CS_JobPhotoReport>()
                {
                    new DataContext.CS_JobPhotoReport() { ID = 1 },
                    new DataContext.CS_JobPhotoReport() { ID = 2 }
                }
            );

            PhotoReportPresenter presenter = new PhotoReportPresenter(view.Object);
            presenter.RemovePhotoReport();

            Assert.AreEqual(LIST_COUNT_TO_TEST, view.Object.PhotoReportList.Count);
        }
    }
}
