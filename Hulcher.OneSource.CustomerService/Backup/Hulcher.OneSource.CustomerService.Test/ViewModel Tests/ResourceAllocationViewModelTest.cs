using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Test.ViewModel_Tests
{
    [TestClass]
    public class ResourceAllocationViewModelTest
    {
        [TestMethod]
        public void IsSetEquipmentsAddRowDataFillingRowProperties()
        {
            // Arrange
            CS_View_EquipmentInfo equipmentInfo = new CS_View_EquipmentInfo()
            {
                Active = true,
                CallLogID = 123,
                ComboID = 123,
                ComboName = "Combo 123",
                JobStatus = "A - Acive",
                Descriptor = "Hazmat",
                DivisionID = 1,
                DivisionName = "001",
                DivisionState = "TX",
                EquipmentID = 1,
                EquipmentStatus = "Up",
                IsPrimary = 1,
                JobID = 123,
                JobLocation = "Denton, TX",
                JobNumber = "000123",
                Status = "Available",
                Type = "ATA",
                UnitNumber = "0001",
                PrefixedNumber = "PA000123"
            };

            Mock<IResourceAllocationView> mock = new Mock<IResourceAllocationView>();
            mock.SetupProperty(c => c.EquipmentRowDataItem, equipmentInfo);
            mock.SetupProperty(c => c.SelectedEquipmentAddList, new Dictionary<string, bool>());
            mock.SetupProperty(c => c.EquipmentsAddDivision, "");
            mock.SetupProperty(c => c.EquipmentsAddDivisionState, "");
            mock.SetupProperty(c => c.EquipmentsAddComboName, "");
            mock.SetupProperty(c => c.EquipmentsAddUnitNumber, "");
            mock.SetupProperty(c => c.EquipmentsAddDescriptor, "");
            mock.SetupProperty(c => c.EquipmentsAddStatus, "");
            mock.SetupProperty(c => c.EquipmentsAddJobLocation, "");
            mock.SetupProperty(c => c.EquipmentsAddType, "");
            mock.SetupProperty(c => c.EquipmentsAddOperationStatus, "");
            mock.SetupProperty(c => c.EquipmentsAddJobNumber, "");
            mock.SetupProperty(c => c.EquipmentsAddchkEquipmentAdd, false);
            mock.SetupProperty(c => c.EquipmentsAddEquipmentId, "");
            mock.SetupProperty(c => c.EquipmentsAddIsCombo, "");
            mock.SetupProperty(c => c.EquipmentsAddIsComboUnit, "");
            mock.SetupProperty(c => c.EquipmentsAddJobNumberNavigateUrl, "");
            mock.SetupProperty(c => c.EquipmentsAddTypeNavigateUrl, "");
            mock.SetupProperty(c => c.EquipmentsAddComboID, null);

            // Act
            ResourceAllocationViewModel viewModel = new ResourceAllocationViewModel(mock.Object);
            viewModel.SetEquipmentsAddRow();

            // Assert
            Assert.AreEqual("001", mock.Object.EquipmentsAddDivision, "Failed in EquipmentsAddDivision");
            Assert.AreEqual("TX", mock.Object.EquipmentsAddDivisionState, "Failed in EquipmentsAddDivisionState");
            Assert.AreEqual("Combo 123", mock.Object.EquipmentsAddComboName, "Failed in EquipmentsAddComboName");
            Assert.AreEqual("0001", mock.Object.EquipmentsAddUnitNumber, "Failed in EquipmentsAddUnitNumber");
            Assert.AreEqual("Hazmat", mock.Object.EquipmentsAddDescriptor, "Failed in EquipmentsAddDescriptor");
            Assert.AreEqual("Available", mock.Object.EquipmentsAddStatus, "Failed in EquipmentsAddStatus");
            Assert.AreEqual("Denton, TX", mock.Object.EquipmentsAddJobLocation, "Failed in EquipmentsAddJobLocation");
            Assert.AreEqual("ATA", mock.Object.EquipmentsAddType, "Failed in EquipmentsAddType");
            Assert.AreEqual("Up", mock.Object.EquipmentsAddOperationStatus, "Failed in EquipmentsAddOperationStatus");
            Assert.AreEqual("PA000123", mock.Object.EquipmentsAddJobNumber, "Failed in EquipmentsAddJobNumber");
            Assert.AreEqual(false, mock.Object.EquipmentsAddchkEquipmentAdd, "Failed in EquipmentsAddchkEquipmentAdd");
            Assert.AreEqual("1", mock.Object.EquipmentsAddEquipmentId, "Failed in EquipmentsAddEquipmentId");
            Assert.AreEqual("True", mock.Object.EquipmentsAddIsCombo, "Failed in EquipmentsAddIsCombo");
            Assert.AreEqual("False", mock.Object.EquipmentsAddIsComboUnit, "Failed in EquipmentsAddIsComboUnit");
            Assert.AreEqual(string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", mock.Object.EquipmentRowDataItem.JobID.Value), mock.Object.EquipmentsAddJobNumberNavigateUrl, "Failed in EquipmentsAddJobNumberNavigateUrl");
            Assert.AreEqual(string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", mock.Object.EquipmentRowDataItem.JobID.Value, mock.Object.EquipmentRowDataItem.CallLogID), mock.Object.EquipmentsAddTypeNavigateUrl, "Failed in EquipmentsAddTypeNavigateUrl");
            Assert.AreEqual(123, mock.Object.EquipmentsAddComboID.Value, "Failed in EquipmentsAddComboID");
        }

        [TestMethod]
        public void IsSetEquipmentComboRowDataFillingRowProperties()
        {
            // Arrange
            CS_View_EquipmentInfo equipmentCombo = new CS_View_EquipmentInfo()
            {
                Active = true,
                CallLogID = 123,
                ComboID = 123,
                ComboName = "Combo 123",
                JobStatus = "A - Acive",
                Descriptor = "Hazmat",
                DivisionID = 1,
                DivisionName = "001",
                DivisionState = "TX",
                EquipmentID = 1,
                EquipmentStatus = "Up",
                IsPrimary = 0,
                JobID = 123,
                JobLocation = "Denton, TX",
                JobNumber = "000001",
                Status = "Available",
                Type = "ATA",
                UnitNumber = "0001",
                PrefixedNumber = "PA000001"
            };

            Mock<IResourceAllocationView> mock = new Mock<IResourceAllocationView>();
            mock.SetupProperty(c => c.EquipmentComboDataItem, equipmentCombo);
            mock.SetupProperty(c => c.SelectedEquipmentAddList, new Dictionary<string, bool>());
            mock.SetupProperty(c => c.EquipmentsComboDivision, "");
            mock.SetupProperty(c => c.EquipmentsComboDivisionState, "");
            mock.SetupProperty(c => c.EquipmentsComboComboName, "");
            mock.SetupProperty(c => c.EquipmentsComboUnitNumber, "");
            mock.SetupProperty(c => c.EquipmentsComboDescriptor, "");
            mock.SetupProperty(c => c.EquipmentsComboStatus, "");
            mock.SetupProperty(c => c.EquipmentsComboJobLocation, "");
            mock.SetupProperty(c => c.EquipmentsComboType, "");
            mock.SetupProperty(c => c.EquipmentsComboOperationStatus, "");
            mock.SetupProperty(c => c.EquipmentsComboJobNumber, "");
            mock.SetupProperty(c => c.EquipmentsCombochkEquipmentAdd, false);
            mock.SetupProperty(c => c.EquipmentsComboEquipmentId, "");
            mock.SetupProperty(c => c.EquipmentsComboIsCombo, "");
            mock.SetupProperty(c => c.EquipmentsComboIsComboUnit, "");
            mock.SetupProperty(c => c.EquipmentsComboJobNumberNavigateUrl, "");
            mock.SetupProperty(c => c.EquipmentsComboTypeNavigateUrl, "");
            mock.SetupProperty(c => c.EquipmentsComboComboID, null);

            // Act
            ResourceAllocationViewModel viewModel = new ResourceAllocationViewModel(mock.Object);
            viewModel.FillEquipmentGridAddRowCombo();

            // Assert
            Assert.AreEqual("001", mock.Object.EquipmentsComboDivision, "Failed in EquipmentsComboDivision");
            Assert.AreEqual("TX", mock.Object.EquipmentsComboDivisionState, "Failed in EquipmentsComboDivisionState");
            Assert.AreEqual("Combo 123", mock.Object.EquipmentsComboComboName, "Failed in EquipmentsComboComboName");
            Assert.AreEqual("0001", mock.Object.EquipmentsComboUnitNumber, "Failed in EquipmentsComboUnitNumber");
            Assert.AreEqual("Hazmat", mock.Object.EquipmentsComboDescriptor, "Failed in EquipmentsComboDescriptor");
            Assert.AreEqual("Available", mock.Object.EquipmentsComboStatus, "Failed in EquipmentsComboStatus");
            Assert.AreEqual("Denton, TX", mock.Object.EquipmentsComboJobLocation, "Failed in EquipmentsComboJobLocation");
            Assert.AreEqual("ATA", mock.Object.EquipmentsComboType, "Failed in EquipmentsComboType");
            Assert.AreEqual("Up", mock.Object.EquipmentsComboOperationStatus, "Failed in EquipmentsComboOperationStatus");
            Assert.AreEqual("PA000001", mock.Object.EquipmentsComboJobNumber, "Failed in EquipmentsComboJobNumber");
            Assert.AreEqual(false, mock.Object.EquipmentsCombochkEquipmentAdd, "Failed in EquipmentsCombochkEquipmentAdd");
            Assert.AreEqual("1", mock.Object.EquipmentsComboEquipmentId, "Failed in EquipmentsComboEquipmentId");
            Assert.AreEqual("False", mock.Object.EquipmentsComboIsCombo, "Failed in EquipmentsComboIsCombo");
            Assert.AreEqual("True", mock.Object.EquipmentsComboIsComboUnit, "Failed in EquipmentsComboIsComboUnit");
            Assert.AreEqual(string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", mock.Object.EquipmentComboDataItem.JobID.Value), mock.Object.EquipmentsComboJobNumberNavigateUrl, "Failed in EquipmentsComboJobNumberNavigateUrl");
            Assert.AreEqual(string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId{1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", mock.Object.EquipmentComboDataItem.JobID.Value, mock.Object.EquipmentComboDataItem.CallLogID), mock.Object.EquipmentsComboTypeNavigateUrl, "Failed in EquipmentsComboTypeNavigateUrl");
            Assert.AreEqual(123, mock.Object.EquipmentsComboComboID.Value, "Failed in EquipmentsComboComboID");
        }
    }
}
