using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class ResourceAllocationPresenterTest
    {
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void TestPartialCriteriaInEquipmentDivisionNumberFilter()
        {


            //EquipmentModel equipmentModel = new EquipmentModel();
            //equipmentModel.ListFilteredEquipmentInfo(Core.Globals.ResourceAllocation.EquipmentFilters.Division, "08");

            //Mock<CustomerServiceModelContainer> csModelContainer = new Mock<CustomerServiceModelContainer>();
            //csModelContainer.Setup(container => container.CS_View_EquipmentInfo).Returns(
            //ResourceAllocationPresenter resourceAllocationPresenter = new ResourceAllocationPresenter(view.Object);
            //resourceAllocationPresenter.ListFilteredEquipmentAdd();

            //Assert.AreEqual(2, view.Object.EquipmentDataTable.Rows.Count);
        }

        //[TestMethod]
        //public void TestIfShoppingCartIsCleared()
        //{
        //    //Arrange
        //    DataTable dtShoppingCart = CreateSampleDataTableForShoppingCartClearTest(3);
        //    Mock<IResourceAllocationView> mockView = new Mock<IResourceAllocationView>();
        //    mockView.SetupProperty(e => e.ShoppingCart, dtShoppingCart);

        //    //Act
        //    ResourceAllocationPresenter presenter = new ResourceAllocationPresenter(mockView.Object);
        //    presenter.ClearShoppingCart();

        //    //Assert
        //    Assert.AreEqual(0, mockView.Object.ShoppingCart.Rows.Count);
        //}

        private DataTable CreateSampleDataTableForShoppingCartClearTest(int count)
        {

            DataTable dtShoppingCart = new DataTable();
            dtShoppingCart.Columns.AddRange(
            new DataColumn[] { 
                    new DataColumn("Id", typeof(int)), 
                    new DataColumn("EquipmentId", typeof(int)), 
                    new DataColumn("EmployeeId", typeof(int)), 
                    new DataColumn("EquipmentTypeId", typeof(int)), 
                    new DataColumn("DivisionId", typeof(int)),
                    new DataColumn("JobId", typeof(int)),
                    new DataColumn("Type", typeof(short)), 
                    new DataColumn("AssignmentType", typeof(string)),
                    new DataColumn("Name", typeof(string)), 
                    new DataColumn("Duration", typeof(int)),
                    new DataColumn("StartDateTime", typeof(DateTime))
                });
            for (int i = 0; i < count; i++)
            {
                DataRow row = dtShoppingCart.NewRow();
                row["Id"] = 1;
                row["EquipmentId"] = 1;
                row["EmployeeId"] = 1;
                row["EquipmentTypeId"] = 1;
                row["DivisionId"] = 1;
                row["JobId"] = 1;
                row["Type"] = 1;
                row["AssignmentType"] = "AssignmentTypeTest";
                row["Name"] = "NameTest";
                row["Duration"] = 1;
                row["StartDateTime"] = DateTime.Now;
                dtShoppingCart.Rows.Add(row);
            }
            return dtShoppingCart;
        }
    }
}
