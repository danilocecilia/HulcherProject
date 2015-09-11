using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Moq;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class JobRecordPresenterTest
    {
        //[TestMethod]
        //public void WhenJobCloningUsesTheAlreadyCreatedMethodToLoadContactInfoFields()
        //{
        //    //Arrange
        //    Mock<ICustomerInfoView> mockCustomerView = new Mock<ICustomerInfoView>();
        //    mockCustomerView.SetupProperty(e => e.CloningId, 1);
        //    Mock<JobModel> mockJobModel = new Mock<JobModel>();
        //    CustomerInfoPresenter presenter = new CustomerInfoPresenter(mockCustomerView.Object, mockJobModel.Object);
        //    //Act
        //    presenter.LoadCustomerInfoCloningData();
        //    //Assert
        //    mockJobModel.Verify(e => e.GetCustomerInfoByJobId(1), Times.Once());                                  
        //}

        //[TestMethod]
        //public void MustFillContactInfoFieldsWhenCloning()
        //{
        //    //Arrange
        //    Mock<ICustomerInfoView> mockCustomerView = new Mock<ICustomerInfoView>();
        //    mockCustomerView.SetupProperty(e => e.CloningId, 1);
        //    mockCustomerView.SetupProperty(e => e.CustomerInfoEntity, null);
        //    Mock<JobModel> mockJobModel = new Mock<JobModel>();
        //    mockJobModel.Setup(e => e.GetCustomerInfoByJobId(1)).Returns(new CS_CustomerInfo() { JobId = 1 });
                
        //    CustomerInfoPresenter presenter = new CustomerInfoPresenter(mockCustomerView.Object, mockJobModel.Object);
        //    //Act
        //    presenter.LoadCustomerInfoCloningData();
        //    //Assert
        //    Assert.IsNotNull(mockCustomerView.Object.CustomerInfoEntity);
        //}

        [TestMethod]
        public void WhenJobCloningUsesTheAlreadyCreatedMethodToLoadLocationInfoFields()
        {
            //Arrange
            Mock<ILocationInfoView> mockLocationView = new Mock<ILocationInfoView>();
            mockLocationView.SetupProperty(e => e.CloningId, 1);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            LocationInfoPresenter presenter = new LocationInfoPresenter(mockLocationView.Object,mockJobModel.Object);
            //Act
            //presenter.LoadLocationInfoCloningData();
            //Assert
            mockJobModel.Verify(e => e.GetLocationInfoByJobId(1), Times.Once());
        }

        [TestMethod]
        public void MustFillLocationInfoFieldsWhenCloning()
        {
            //Arrange
            Mock<ILocationInfoView> mockLocationView = new Mock<ILocationInfoView>();
            mockLocationView.SetupProperty(e => e.CloningId, 1);
            mockLocationView.SetupProperty(e => e.LocationInfoEntity, null);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetLocationInfoByJobId(1)).Returns(new CS_LocationInfo() { JobID = 1 });

            LocationInfoPresenter presenter = new LocationInfoPresenter(mockLocationView.Object, mockJobModel.Object);
            //Act
            //presenter.LoadLocationInfoCloningData();
            //Assert
            Assert.IsNotNull(mockLocationView.Object.LocationInfoEntity);
        }

        [TestMethod]
        public void WhenJobCloningUsesTheAlreadyCreatedMethodToLoadJobInfoFieldsInModel()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.JobInfoEntity, null);
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);            
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetJobInfoWithAppend(1)).Returns(new CS_JobInfo() { JobID = 1, Active = true });

            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object, mockJobModel.Object);
            //Act
            presenter.LoadJobInfoCloningData();
            //Assert
            mockJobModel.Verify(e => e.GetJobInfoWithAppend(1), Times.Once());
        }

        [TestMethod]
        public void WhenCloningJobInfoCallDateAndTimeMustBeSetToCurrent()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.JobInfoEntity, null);
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetJobInfoWithAppend(1)).Returns(new CS_JobInfo() { JobID = 1, InitialCallDate = new DateTime(2000, 11, 05), InitialCallTime = new TimeSpan(1, 20, 5) });
            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object,mockJobModel.Object);
            //Act
            presenter.LoadJobInfoCloningData();
            //Assert
            Assert.AreEqual(mockJobInfoView.Object.JobInfoEntity.InitialCallDate.Year, DateTime.Now.Year);
            Assert.AreEqual(mockJobInfoView.Object.JobInfoEntity.InitialCallDate.Month, DateTime.Now.Month);
            Assert.AreEqual(mockJobInfoView.Object.JobInfoEntity.InitialCallDate.Day, DateTime.Now.Day);
            Assert.AreEqual(mockJobInfoView.Object.JobInfoEntity.InitialCallTime.Hours, DateTime.Now.TimeOfDay.Hours);
            Assert.AreEqual(mockJobInfoView.Object.JobInfoEntity.InitialCallTime.Minutes, DateTime.Now.TimeOfDay.Minutes);
        }

        [TestMethod]
        public void WhenCloningJobInfoDescriptiveActionMustBeBlank()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.JobInfoEntity, null);
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetJobInfoWithAppend(1)).Returns(new CS_JobInfo()
            {
                JobID = 1,
                CS_JobAction = new CS_JobAction()
                    {
                        Description = "Action Description"
                    }
            });
            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object, mockJobModel.Object);
            //Act
            presenter.LoadJobInfoCloningData();
            //Assert
            Assert.IsNotNull(mockJobInfoView.Object.JobInfoEntity.CS_JobAction);
        }

        [TestMethod]
        public void WhenCloningJobInfoPresetInfoMustBeBlank()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.JobInfoEntity, null);
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetPresetInfo(1)).Returns(new CS_PresetInfo()
            {
                 JobId = 1                
            });                
            JobInfoPresenter  presenter = new JobInfoPresenter(mockJobInfoView.Object, mockJobModel.Object);
            //Act
            presenter.LoadJobInfoCloningData();
            //Assert
            Assert.IsNull(mockJobInfoView.Object.PresetInfoEntity);
        }

        [TestMethod]
        public void WhenCloningJobInfoCustomerSpecificInfoMustBeBlank()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.JobInfoEntity, null);
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
             Mock<JobModel> mockJobModel = new Mock<JobModel>();
             mockJobModel.Setup(e => e.GetJobInfoWithAppend(1)).Returns(new CS_JobInfo()
            {
                JobID = 1,
                CustomerSpecificInfo = "customer specific info"
            });      
            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object,mockJobModel.Object);
            //Act
            presenter.LoadJobInfoCloningData();
            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(mockJobInfoView.Object.JobInfoEntity.CustomerSpecificInfo));
        }

        [TestMethod]
        public void WhenCloningPermittingInfoExcludeOnlySingleTripPermitType()
        {
            //Arrange
            Mock<IPermitInfoView> mockPermitInfoView = new Mock<IPermitInfoView>();    
            mockPermitInfoView.SetupProperty(e => e.CloningId, 1);
            mockPermitInfoView.SetupProperty(e => e.PermitInfoEntityList, null);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetPermitInfoByJob(1)).Returns(
                new List<CS_JobPermit>()
                {
                    new CS_JobPermit() { CS_JobPermitType = new CS_JobPermitType() { ID=1, Description = "Single-trip"}},                    
                    new CS_JobPermit() { CS_JobPermitType = new CS_JobPermitType() { ID=2, Description="Anual"}},
                    new CS_JobPermit() { CS_JobPermitType = new CS_JobPermitType() { ID=3, Description="Quartely"}},
                    new CS_JobPermit() { CS_JobPermitType = new CS_JobPermitType() { ID=4, Description = "Single-trip"}}
                });
            PermitInfoPresenter presenter = new PermitInfoPresenter(mockPermitInfoView.Object,mockJobModel.Object);
            //Act
            presenter.ListPermitInfoCloning();
            //Assert
            Assert.AreEqual(mockPermitInfoView.Object.PermitInfoEntityList.Count,2);
        }

        [TestMethod]
        public void MustFillSpecialPricingInfoWhenCloningJobInfo()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
            mockJobInfoView.SetupProperty(e => e.SpecialPricingEntity, null);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.GetSpecialPricing(1)).Returns(new CS_SpecialPricing() { JobId = 1 });

            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object, mockJobModel.Object);
            //Act
            presenter.LoadSpecialPricingInfoCloning();
            //Assert
            Assert.IsNotNull(mockJobInfoView.Object.SpecialPricingEntity);
        }

        [TestMethod]
        public void WhenCloningJobInfoMustExcludeAllDivisionsExceptPrimaryDivision()
        {
            //Arrange
            Mock<IJobInfoView> mockJobInfoView = new Mock<IJobInfoView>();
            mockJobInfoView.SetupProperty(e => e.CloningId, 1);
            mockJobInfoView.SetupProperty(e => e.JobDivisionEntityList, null);
            Mock<JobModel> mockJobModel = new Mock<JobModel>();
            mockJobModel.Setup(e => e.ListJobDivision(1)).Returns(
                    new List<CS_JobDivision>()
                    {
                        new CS_JobDivision(){ ID=1, Active = true, PrimaryDivision = false},
                        new CS_JobDivision(){ ID=2, Active = true, PrimaryDivision = false},
                        new CS_JobDivision(){ ID=3, Active = true, PrimaryDivision = true}
                    }
                );

            JobInfoPresenter presenter = new JobInfoPresenter(mockJobInfoView.Object, mockJobModel.Object);
            //Act
            presenter.LoadJobDivisionCloning();
            //Assert
            Assert.IsNotNull(mockJobInfoView.Object.JobDivisionEntityList);
            Assert.AreEqual(1, mockJobInfoView.Object.JobDivisionEntityList.Count);
        }
    }
}
