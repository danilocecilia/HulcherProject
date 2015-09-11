using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Moq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Test.Presenter_Tests
{
    [TestClass]
    public class PermittingPresenterTest
    {
        [TestMethod]
        public void ShouldFillComboInfoWhenLoading()
        {
            //Arrange
            Mock<IPermittingView> view = new  Mock<IPermittingView>();

            EquipmentComboVO equipment = new EquipmentComboVO()
            {
                EquipmentId = 1,
                IsPrimary = true,
                DivisionNumber = "084",                
                UnitNumber = "01",
                Descriptor = "Dozer"
            };

            view.SetupProperty(v => v.EquipmentInfoItem, equipment);
            view.SetupProperty(v => v.IsPrimaryObjectSelected, false);
            view.SetupProperty(v => v.DivisionSelected, string.Empty);
            view.SetupProperty(v => v.UnitNumberSelected, string.Empty);
            view.SetupProperty(v => v.DescriptorSelected, string.Empty);
            //Act
            PermittingPresenter presenter = new PermittingPresenter(view.Object);
            presenter.LoadShoppingCartRow();

            //Assert
            Assert.IsTrue(view.Object.IsPrimaryObjectSelected);
            Assert.AreEqual<string>(view.Object.DivisionSelected, "084");
            Assert.AreEqual<string>(view.Object.UnitNumberSelected, "01");
            Assert.AreEqual<string>(view.Object.DescriptorSelected, "Dozer");          
        }

        [TestMethod]
        public void IsEquipmentInfoLoaded()
        {
              Mock<IPermittingView> view = new  Mock<IPermittingView>();

              EquipmentComboVO equipment = null;

              view.SetupProperty(v => v.EquipmentInfoItem, equipment);
              view.SetupProperty(v => v.IsPrimaryObjectSelected, false);
              view.SetupProperty(v => v.DivisionSelected, string.Empty);
              view.SetupProperty(v => v.UnitNumberSelected, string.Empty);
              view.SetupProperty(v => v.DescriptorSelected, string.Empty);
              
              PermittingPresenter presenter = new PermittingPresenter(view.Object);
              presenter.LoadShoppingCartRow();
        }

        [TestMethod]
        public void ShouldFillDataSourceComboInShoppingCart()
        {
            //Arrange
            Mock<IPermittingView> view = new Mock<IPermittingView>();
            view.SetupProperty(v => v.EquipmentComboId, 1);
            view.SetupProperty(v => v.ComboName, string.Empty);
            view.SetupProperty(v => v.ComboType, string.Empty);
            view.SetupProperty(v => v.EquipmentInfoShoppingCartDataSource, null);

            Mock<EquipmentModel> model = new Mock<EquipmentModel>();
            model.Setup(m => m.GetCombo(1)).Returns(new CS_EquipmentCombo() { Name = "comboName", ComboType = "comboType" });
            model.Setup(m => m.ListEquipmentsOfACombo(1)).Returns(
                new List<EquipmentComboVO>() { 
                    new EquipmentComboVO() { Descriptor = null, DivisionNumber = "Division", EquipmentId = 1, IsPrimary = true, UnitNumber = "1" },
                    new EquipmentComboVO() { Descriptor = null, DivisionNumber = "Division", EquipmentId = 2, IsPrimary = false, UnitNumber = "2" }
                });
            
            PermittingPresenter presenter = new PermittingPresenter(view.Object, model.Object);            

            //Act            
            presenter.LoadCombo();

            //Assert
            Assert.AreEqual("comboName", view.Object.ComboName, "Failed in ComboName Property");
            Assert.AreEqual("comboType", view.Object.ComboType, "Failed in ComboType Property");
            Assert.IsNotNull(view.Object.EquipmentInfoShoppingCartDataSource, "Failed in DataSource Property (NULL)");
            Assert.AreEqual(2, view.Object.EquipmentInfoShoppingCartDataSource.Count, "Failed in DataSource Property (COUNT)");
        }
    }    
}
