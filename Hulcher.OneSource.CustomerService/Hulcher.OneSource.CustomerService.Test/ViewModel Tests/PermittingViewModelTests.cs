using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Test.ViewModel_Tests
{
    /// <summary>
    /// Summary description for PermittingViewModelTests
    /// </summary>
    [TestClass]
    public class PermittingViewModelTests
    {
        [TestMethod]
        public void PermittingViewHierarchicalPrimaryRepeaterBind()
        {
            List<CS_View_EquipmentInfo> lstCombo = new List<CS_View_EquipmentInfo>();
            CS_View_EquipmentInfo dataItem = new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 1,
                    EquipmentStatus = "Up",
                    IsPrimary = 1,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberOne"
                };

            lstCombo.Add(dataItem);

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 2,
                    EquipmentStatus = "Down",
                    IsPrimary = 0,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberTwo"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 3,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberThree"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 4,
                    EquipmentStatus = "Up",
                    IsPrimary = 1,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberFour"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 5,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberFive"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 6,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberSix"
                });

            //TODO
            Mock<IPermittingView> mock = new Mock<IPermittingView>();
            mock.SetupProperty(c => c.EquipmentInfoListData, lstCombo);
            mock.SetupProperty(c => c.FirstTierDataSource, new List<CS_View_EquipmentInfo>());

            PermittingViewModel viewModel = new PermittingViewModel(mock.Object);
            viewModel.GetFirstTierEquipmentList();

            Assert.AreEqual(2, mock.Object.FirstTierDataSource.Count);
        }

        [TestMethod]
        public void PermittingViewHierarchicalPrimaryRepeaterItemsSetValue()
        {
            CS_View_EquipmentInfo dataItem = new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 1,
                    EquipmentStatus = "Up",
                    IsPrimary = 1,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberOne"
                };

            //TODO
            Mock<IPermittingView> mock = new Mock<IPermittingView>();
            mock.SetupProperty(c => c.FirstTierDataItem, dataItem);
            mock.SetupProperty(c => c.FirstTierComboId, 0);
            mock.SetupProperty(c => c.FirstTierComboName, "");
            mock.SetupProperty(c => c.FirstTierJobId, 0);
            mock.SetupProperty(c => c.FirstTierJobNumber, "");
            mock.SetupProperty(c => c.FirstTierDivisionNumber, "");
            mock.SetupProperty(c => c.FirstTierDivisionState, "");

            PermittingViewModel viewModel = new PermittingViewModel(mock.Object);
            viewModel.SetEquipmentComboRowData();

            Assert.AreEqual(1, mock.Object.FirstTierComboId);
            Assert.AreEqual("FTW-1", mock.Object.FirstTierComboName);
            Assert.AreEqual(1, mock.Object.FirstTierJobId);
            Assert.AreEqual("000001", mock.Object.FirstTierJobNumber);
            Assert.AreEqual("One", mock.Object.FirstTierDivisionNumber);
            Assert.AreEqual("TX", mock.Object.FirstTierDivisionState);
        }

        [TestMethod]
        public void PermittingViewHierarchicalSecondaryRepeaterBind()
        {
            List<CS_View_EquipmentInfo> lstCombo = new List<CS_View_EquipmentInfo>();
            CS_View_EquipmentInfo dataItem = new CS_View_EquipmentInfo()
            {
                Active = true,
                ComboID = 1,
                ComboName = "FTW-1",
                JobStatus = "Combo1",
                Descriptor = null,
                DivisionID = 1,
                DivisionName = "One",
                DivisionState = "TX",
                EquipmentID = 1,
                EquipmentStatus = "Up",
                IsPrimary = 1,
                JobID = 1,
                JobLocation = "TX",
                JobNumber = "000001",
                Status = "Reserved",
                Type = "Type1",
                UnitNumber = "NumberOne"
            };

            lstCombo.Add(dataItem);

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 2,
                    EquipmentStatus = "Down",
                    IsPrimary = 0,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberTwo"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 3,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberThree"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 4,
                    EquipmentStatus = "Up",
                    IsPrimary = 1,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberFour"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 5,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberFive"
                });

            lstCombo.Add(
                new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 2,
                    ComboName = "FTW-2",
                    JobStatus = "Combo2",
                    Descriptor = null,
                    DivisionID = 2,
                    DivisionName = "Two",
                    DivisionState = "MX",
                    EquipmentID = 6,
                    EquipmentStatus = "Up",
                    IsPrimary = 0,
                    JobID = 2,
                    JobLocation = "MX",
                    JobNumber = "000002",
                    Status = "Reserved",
                    Type = "Type2",
                    UnitNumber = "NumberSix"
                });

            //TODO
            Mock<IPermittingView> mock = new Mock<IPermittingView>();
            mock.SetupProperty(c => c.EquipmentInfoListData, lstCombo);
            mock.SetupProperty(c => c.FirstTierDataItem, dataItem);
            mock.SetupProperty(c => c.SecondTierDataSource, new List<CS_View_EquipmentInfo>());

            PermittingViewModel viewModel = new PermittingViewModel(mock.Object);
            viewModel.GetSecondTierEquipmentList();

            Assert.AreEqual(3, mock.Object.SecondTierDataSource.Count);
        }

        [TestMethod]
        public void PermittingViewHierarchicalSecondaryRepeaterItemsSetValue()
        {
            CS_View_EquipmentInfo dataItem = new CS_View_EquipmentInfo()
            {
                Active = true,
                ComboID = 1,
                ComboName = "FTW-1",
                JobStatus = "Combo1",
                Descriptor = null,
                DivisionID = 1,
                DivisionName = "One",
                DivisionState = "TX",
                EquipmentID = 1,
                EquipmentStatus = "Up",
                IsPrimary = 1,
                JobID = 1,
                JobLocation = "TX",
                JobNumber = "000001",
                Status = "Reserved",
                Type = "Type1",
                UnitNumber = "NumberOne"
            };

            CS_View_EquipmentInfo dataItem2 = new CS_View_EquipmentInfo()
                {
                    Active = true,
                    ComboID = 1,
                    ComboName = "FTW-1",
                    JobStatus = "Combo1",
                    Descriptor = null,
                    DivisionID = 1,
                    DivisionName = "One",
                    DivisionState = "TX",
                    EquipmentID = 2,
                    EquipmentStatus = "Down",
                    IsPrimary = 0,
                    JobID = 1,
                    JobLocation = "TX",
                    JobNumber = "000001",
                    Status = "Reserved",
                    Type = "Type1",
                    UnitNumber = "NumberTwo"
                };

            //TODO
            Mock<IPermittingView> mock = new Mock<IPermittingView>();
            mock.SetupProperty(c => c.FirstTierDataItem, dataItem);
            mock.SetupProperty(c => c.SecondTierDataItem, dataItem2);
            mock.SetupProperty(c => c.SecondTierUnitNumber, "");
            mock.SetupProperty(c => c.SecondTierJobId, 0);
            mock.SetupProperty(c => c.SecondTierJobNumber, "");
            mock.SetupProperty(c => c.SecondTierDivisionNumber, "");
            mock.SetupProperty(c => c.SecondTierDivisionState, "");

            PermittingViewModel viewModel = new PermittingViewModel(mock.Object);
            viewModel.SetDetailedEquipmentComboRowData();

            Assert.AreEqual("NumberTwo", mock.Object.SecondTierUnitNumber);
            Assert.AreEqual(1, mock.Object.SecondTierJobId);
            Assert.AreEqual("000001", mock.Object.SecondTierJobNumber);
            Assert.AreEqual("One", mock.Object.SecondTierDivisionNumber);
            Assert.AreEqual("TX", mock.Object.SecondTierDivisionState);
        }

        [TestMethod]
        public void RemoveEquipmentFromShoppingCart()
        {
            List<EquipmentComboVO> equipmentList = new List<EquipmentComboVO>();

            EquipmentComboVO dataItem = new EquipmentComboVO()
            {
                Descriptor = null,
                DivisionNumber = "One",
                EquipmentId = 1,
                IsPrimary = true,
                UnitNumber = "NumberOne"
            };

            equipmentList.Add(dataItem);

            EquipmentComboVO dataItem2 = new EquipmentComboVO()
                {
                    Descriptor = null,
                    DivisionNumber = "One",
                    EquipmentId = 2,
                    IsPrimary = false,
                    UnitNumber = "NumberTwo"
                };

            equipmentList.Add(dataItem2);

            List<int> selectedEquipments = new List<int>();

            selectedEquipments.Add(2);

            Mock<IPermittingView> mock = new Mock<IPermittingView>();
            mock.SetupProperty(c => c.EquipmentInfoShoppingCartDataSource, equipmentList);
            mock.SetupProperty(c => c.RemovedEquipments, selectedEquipments);

            PermittingViewModel viewModel = new PermittingViewModel(mock.Object);
            viewModel.RemoveEquipmentFromShoppingCart();

            Assert.AreEqual(1, mock.Object.EquipmentInfoShoppingCartDataSource.Count);
            
        }
    }
}
