using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ResourceAllocationPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Resource Allocation View Interface
        /// </summary>
        private IResourceAllocationView _view;

        /// <summary>
        /// Instance of the Resource Allocation ViewModel
        /// </summary>
        private ResourceAllocationViewModel _viewModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CustomerInfo View Interface</param>
        public ResourceAllocationPresenter(IResourceAllocationView view)
        {
            _view = view;
            _viewModel = new ResourceAllocationViewModel(view);
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Loads the initial page state
        /// </summary>
        public void LoadPage()
        {
            try
            {
                // Gets the Page Title
                _viewModel.UpdateTitle();

                // Gets the Resource Allocation Notes
                _viewModel.GetResourceAllocationNotes();

                _view.CallDate = DateTime.Now;

                if (_view.ResourceConversion)
                {
                    _view.SetPageResourceConversion();
                    
                    // Binds the Transfer Shopping Cart Grid
                    _viewModel.ListTransferShoppingCart();
                    // Binds the Reserved Grid
                    _viewModel.ListReservedResourceConversionList();
                    //List Reserved Equipments and Employees                    
                    _viewModel.ListAllEquipmentAddByReserves();
                    _viewModel.ListAllEmployeeAddByReserves();
                }
                else
                {
                    // Verify if the Add Listing needs to be displayed
                    _viewModel.VerifyJobStatus();

                    // Default filter for Add (based on divisions related to Job Record)
                    if (_view.DisplayAddResource)
                    {
                        _viewModel.ListAllEquipmentAddByDivision();
                        _viewModel.ListAllEmployeeAddByDivision();
                    }

                    // Fill the Dropdown filters for Reserved Equipments
                    _viewModel.ListAllEquipmentType();

                    // Defaul filter for Reserve (based on divisions related to Job Record)
                    _viewModel.ListAllEquipmentReserveByDivision();
                    _viewModel.ListAllEmployeeReserveByDivision();

                    // Binds the Shopping Cart Grid
                    _viewModel.ListTransferShoppingCart();
                }
                _viewModel.FillShoppingCart();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the page information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to load the page.", false);
            }
        }

        #endregion

        #region [ Add Resource - Equipment ]

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        public void ListFilteredEquipmentGridAdd()
        {
            try
            {
                _viewModel.ListFilteredEquipmentGridAdd();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to filter the Information.", false);
            }
        }

        /// <summary>
        ///  Goes through the rows of the DataTable and updates the 'Enabled' property, according to parameters
        /// </summary>
        public void RebindEquipmentGridAdd()
        {
            try
            {
                _viewModel.BindEquipmentGridAdd();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to update the add equipment gridview!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An internal error has ocurred while trying to update the grid information.", false);
            }
        }

        /// <summary>
        /// Fill all fields of the Equipment Grid Row
        /// </summary>
        public void FillEquipmentGridAddRow()
        {
            try
            {
                _viewModel.SetEquipmentsAddRow();
                _viewModel.GetEquipmentComboRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to update the add equipment gridview row!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An internal error has ocurred while trying to update the grid information.", false);
            }
        }

        /// <summary>
        /// Fill all fields of the Equipment inside a combo Row
        /// </summary>
        public void FillEquipmentGridAddRowCombo()
        {
            try
            {
                _viewModel.FillEquipmentGridAddRowCombo();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to update the add equipment gridview combo row!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An internal error has ocurred while trying to update the grid information.", false);
            }
        }

        /// <summary>
        /// Add the selected equipments (Add Grid) to the shopping cart
        /// </summary>
        public void AddEquipmentToShoppingCart()
        {
            try
            {
                _viewModel.AddEquipmentToShoppingCart();
                _viewModel.BindEquipmentGridAdd();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to add the selected equipments to the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An internal error has ocurred while trying to add the selected equipments to the resources.", false);
            }
        }

        #endregion

        #region [ Add Resource - Employee ]

        /// <summary>
        /// Insert Employee Info List to a DataTable
        /// </summary>
        public void ListFilteredEmployeeGridAdd()
        {
            try
            {
                _viewModel.ListFilteredEmployeeGridAdd();
                _viewModel.UpdateEmployeeGridAddButton();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to filter the Information.", false);
            }
        }

        /// <summary>
        /// Adds the Selected Employees (Add Grid) to the Shopping Cart
        /// </summary>
        public void AddEmployeeToShoppingCart()
        {
            try
            {
                _viewModel.AddEmployeeToShoppingCart();
                _viewModel.UpdateEmployeeGridAddButton();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to insert the selected employees into the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to insert the selected employees into the resources.", false);
            }
        }

        #endregion

        #region [ Reserve Resource - Equipment ]

        /// <summary>
        /// Returns a list of equipment type based on filters
        /// </summary>
        public void ListFilteredEquipmentReserve()
        {
            try
            {
                _viewModel.ListFilteredEquipmentGridReserve();
                _viewModel.BuildReserveEquipmentLocalCount();
                _view.BindReserveEquipmentGrid();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the list of all filtered equipment\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the list of all filtered equipment. Please try again.", false);
            }
        }

        /// <summary>
        /// List of all jobs related to the equipment type and division informed
        /// </summary>
        public void ListAllJobsByEquipmentTypeAndDivision()
        {
            try
            {
                _viewModel.ListAllJobsByEquipmentTypeAndDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Job information.", false);
            }
        }

        /// <summary>
        /// Inserts the selected Equipment Types inside the Shopping Cart
        /// </summary>
        public void ReserveEquipmentTypeToShoppingCart()
        {
            try
            {
                _viewModel.ReserveEquipmentTypeToShoppingCart();
                _view.BindReserveEquipmentGrid();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to insert the selected items into the resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to insert the selected items into the resources.. Please try again.", false);
            }

        }

        #endregion

        #region [ Reserve Resource - Employee ]

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        public void ListFilteredEmployeeReserve()
        {
            try
            {
                _viewModel.ListFilteredEmployeeGridReserve();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to filter the Information.", false);
            }
        }

        /// <summary>
        /// Inserts the selected Employees inside the Shopping Cart
        /// </summary>
        public void ReserveEmployeeToShoppingCart()
        {
            try
            {
                _viewModel.ReserveEmployeeToShoppingCart();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to insert the selected items into the resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to insert the selected items into the resources.. Please try again.", false);
            }
        }

        #endregion

        #region [ Shopping Cart ]

        /// <summary>
        /// Clears shoping cart data table
        /// </summary>
        public void ClearShoppingCart()
        {
            try
            {
                _viewModel.ListTransferShoppingCart();

                _viewModel.ResetEquipmentAddGridCheckboxes();
                _viewModel.ResetEmployeeAddGridCheckboxes();

                _viewModel.BuildReserveEquipmentLocalCount();                
                _view.BindReserveEquipmentGrid();

                _view.ShoppingCart.Rows.Clear();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to clean the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to clean the resources.", false);
            }
        }

        /// <summary>
        /// Removes a list of rows from the shopping cart datatable
        /// </summary>
        public void RemoveItemsFromShoppingCart()
        {
            try
            {
                _viewModel.RemoveItemsFromShoppingCart();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to remove the selected items from the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to remove the selected items from the resources.", false);
            }
        }

        /// <summary>
        /// Removes a list of rows from the shopping cart datatable
        /// </summary>
        public void TransferResourcesFromShoppingCart()
        {
            try
            {
                _viewModel.TransferResourcesFromShoppingCart();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to transfer the selected items from the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to transfer the selected items from the resources.", false);
            }
        }

        /// <summary>
        /// Executes the Saving function for the Resource Allocation (Shopping Cart)
        /// </summary>
        public void SaveShoppingCart()
        {
            try
            {
                if (_viewModel.ValidateShoppingCartBeforeSave())
                {
                    _viewModel.SaveResourceAllocation();

                    if (_view.ResourceConversion)
                    {
                        _viewModel.ClearReservedResources();
                    }

                    _view.SavedSuccessfully = true;
                    _view.DisplayMessage("Resources added successfully.", true);
                }
                else
                {
                    _view.SavedSuccessfully = false;
                    _view.DisplayMessage("It´s necessary to select an equipment and at least one employee prior to save the Resource Allocation.", false);
                }
            }
            catch (Exception ex)
            {
                _view.SavedSuccessfully = false;

                if (null == ex.InnerException)
                    Logger.Write(string.Format("There was an error while trying to save the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                else
                    Logger.Write(string.Format("There was an error while trying to save the resources!\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                _view.DisplayMessage(string.Format("There was an error while trying to save the resources.\n{0}", ex.Message), false);
            }
        }        

        #endregion

        #endregion
    }
}
