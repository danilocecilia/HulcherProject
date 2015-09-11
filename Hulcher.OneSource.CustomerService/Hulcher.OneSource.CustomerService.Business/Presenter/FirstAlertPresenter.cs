using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// First Alert Presenter
    /// </summary>
    public class FirstAlertPresenter
    {
        #region [ Attributes ]

        private IFirstAlertView _view;

        private FirstAlertViewModel _viewModel;

        private FirstAlertModel _model;

        private DivisionModel _divisionModel;

        private JobModel _jobModel;

        private AZManager _manager;

        #endregion

        #region [ Constructor ]

        public FirstAlertPresenter(IFirstAlertView view)
        {
            _view = view;
            _model = new FirstAlertModel();
            _divisionModel = new DivisionModel();
            _jobModel = new JobModel();
            _viewModel = new FirstAlertViewModel(view);
        }

        public FirstAlertPresenter(IFirstAlertView view, FirstAlertModel model)
        {
            _view = view;
            _model = model;
            _viewModel = new FirstAlertViewModel(view);
        }

        public FirstAlertPresenter(IFirstAlertView view, AZManager manager)
        {
            _view = view;
            _viewModel = new FirstAlertViewModel(view);
            _manager = manager;
        }

        #endregion

        #region [ Methods ]

        #region [ First Alert ]

        /// <summary>
        /// Page Load
        /// </summary>
        public void LoadPage()
        {
            try
            {
                VerifyAccess();
                ListDivisions();
                LoadFirstAlertType();
                if (_view.JobIDFromExternalSource.HasValue)
                {
                    _view.EditMode = true;
                    ResetFirstAlert();
                    FillJobFields();
                }
                else
                    LoadFirstAlert();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the page.", false);
            }
        }

        /// <summary>
        /// Load First alert Type
        /// </summary>
        private void LoadFirstAlertType()
        {
            _view.FirstAlertType = _model.ListFirstAlertType();
        }

        /// <summary>
        /// Verify if user has access to the page
        /// </summary>
        public void VerifyAccess()
        {
            try
            {
                if (_manager == null)
                    _manager = new AZManager();

                AZOperation[] azOP = _manager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.FirstAlert });

                if (!azOP[0].Result)
                {
                    _view.DisplayMessage("The user does not have access to this functionality", true);
                    _view.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to verify Permissions!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to verify Permissions.", false);
            }
        }

        public void VerifyDeletePermission()
        {
            if (_manager == null)
                _manager = new AZManager();
            
            AZOperation[] azOP = _manager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.FirstAlertDelete });
            if (azOP.Length > 0)
            {
                _view.DeletePermission = azOP[0].Result;
            }
        }

        /// <summary>
        /// Load first bind the dashboard view for the first alert
        /// </summary>
        public void LoadFirstAlert()
        {
            try
            {
                if (!_view.ReadOnly)
                {
                    if (_view.CallLogID.HasValue)
                    {
                        _viewModel.LoadExistingFirstAlert();
                    }
                    else
                    {
                        _viewModel.LoadFirstAlertList();
                    }

                    VerifyDeletePermission();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load First Alert Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load First Alert Information.", false);
            }
        }

        /// <summary>
        /// List all divisions to fill the Dropdown
        /// </summary>
        public void ListDivisions()
        {
            try
            {
                _view.DivisionList = _divisionModel.ListAllDivision().OrderBy(e => e.Name).ToList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to List Divisions!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load First Alert Information.", false);
            }
        }

        /// <summary>
        /// Fill job related fields when a job record is selected
        /// </summary>
        public void FillJobFields()
        {
            try
            {
                if (_view.JobID > 0)
                    _view.JobRelatedInformation = _jobModel.GetJobById(_view.JobID);
                else
                    _view.JobRelatedInformation = null;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to get Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to get Job Information.", false);
            }
        }

        /// <summary>
        /// Resets the fields of the First Alert
        /// </summary>
        public void ResetFirstAlert()
        {
            try
            {
                _view.FirstAlertEntity = null;
                ResetAddVehicle();
                ResetPersonAdd();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to clean first alert information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to clean first alert information.", false);
            }
        }

        /// <summary>
        /// Fill the fields with the First Alert selected
        /// </summary>
        public void FillFirstAlertHeaderFields()
        {
            _view.EditMode = true;
            CS_FirstAlert firstAlert = _model.GetFirstAlertById(_view.FirstAlertID);
            _view.FirstAlertEntity = firstAlert;

            IList<CS_FirstAlertVehicle> vehicles = firstAlert.CS_FirstAlertVehicle.Where(a => a.Active).ToList();
            IList<FirstAlertPersonVO> person = ConvertFirstPersonAlert(firstAlert.CS_FirstAlertPerson.Where(a => a.Active).ToList());
            IList<CS_FirstAlertContactPersonal> contactPersonal = firstAlert.CS_FirstAlertContactPersonal.Where(a => a.Active).ToList();

            for (int i = 0; i < vehicles.Count; i++)
            {
                vehicles[i].TemporaryID = _view.NewVehicleTempID();

                if (vehicles[i].IsHulcherVehicle)
                    vehicles[i].UnitNumber = vehicles[i].CS_Equipment.Name;

                List<FirstAlertPersonVO> personVOList = person.Where(e => e.FirstAlertVehicleID == vehicles[i].ID).ToList();

                for (int j = 0; j < personVOList.Count; j++)
                {
                    int personVOAux = person.IndexOf(personVOList[j]);

                    personVOList[j].FirstAlertVehicleIndex = vehicles[i].TemporaryID;

                    person[personVOAux] = personVOList[j];
                }
            }

            _view.FirstAlertVehicleList = vehicles;
            _view.FirstAlertPersonList = person;
            _view.FirstAlertDivisionList = firstAlert.CS_FirstAlertDivision.Where(a => a.Active).ToList();
            _view.FirstAlertFirstAlertTypeList = firstAlert.CS_FirstAlertFirstAlertType.Where(a => a.Active).ToList();
            _view.FirstAlertContactPersonalList = contactPersonal;
            _viewModel.BindPersonVehiclesList();
        }

        private IList<FirstAlertPersonVO> ConvertFirstPersonAlert(IList<CS_FirstAlertPerson> list)
        {
            List<FirstAlertPersonVO> returnList = new List<FirstAlertPersonVO>();

            for (int i = 0; i < list.Count; i++)
            {
                returnList.Add(list[i].FirstAlertPersonVO);    
            }

            return returnList;
        }

        /// <summary>
        /// Save/Update first alert information
        /// </summary>
        public void SaveUpdateFirstAlert()
        {
            try
            {
                _viewModel.SaveUpdateFirstAlert();
                _view.SavedSuccessfully = true;

                _view.EditMode = false;
                if (_view.FirstAlertFilter.HasValue && !string.IsNullOrEmpty(_view.FilterValue))
                    _viewModel.LoadFilteredFirstAlert();
                else
                    _viewModel.LoadFirstAlertList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save First Alert Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save First Alert Information.", false);
                _view.SavedSuccessfully = false;
            }
        }

        /// <summary>
        /// Delete first alert logicaly
        /// </summary>
        public void DeleteFirstAlert()
        {
            try
            {
                _viewModel.DeleteFirstAlert();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to delete First Alert Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to delete First Alert Information.", false);
            }
        }

        /// <summary>
        /// Set all  first alert fields to the gridview 
        /// </summary>
        public void SetDetailedFirstAlertRowData()
        {
            try
            {
                _viewModel.SetDetailedFirstAlertRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to set the filds on the FirstAlertGridView!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to set the filds on the FirstAlertGridView.", false);
            }
        }

        /// <summary>
        /// Load filtered information to bind the dashboard grid
        /// </summary>
        public void LoadFilteredFirstAlert()
        {
            try
            {
                if (_view.FirstAlertFilter.HasValue && !string.IsNullOrEmpty(_view.FilterValue))
                    _viewModel.LoadFilteredFirstAlert();
                else
                    _viewModel.LoadFirstAlertList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load filtered data do bind the dashboard grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Load filtered data do bind the dashboard grid.", false);
            }
        }

        #endregion

        #region [ Vehicle ]

        /// <summary>
        /// Add Vehicles to the list
        /// </summary>
        public void AddVehicles()
        {
            _viewModel.AddVehicles();
            _viewModel.BindPersonVehiclesList();
        }

        /// <summary>
        /// Gets a vehicle by equipmentId
        /// </summary>
        public void GetVehicleByEquipmentID()
        {
            _viewModel.GetVehicleByEquipmentID();
        }

        /// <summary>
        /// Resets the Add Vehicle Form
        /// </summary>
        public void ResetAddVehicle()
        {
            _view.CurrentFirstAlertVehicleEquipmentID = 0;
            _view.CurrentFirstAlertVehicleIndex = -1;
            _view.CurrentFirstAlertVehicleID = 0;
            _view.FilteredEquipmentsDataSource = new List<CS_View_EquipmentInfo>();
            _view.FilterVehiclesByJobID = (_view.JobID != 0);
            _view.EquipmentFilter = null;
            _view.EquipmentFilterValue = string.Empty;
            _view.OtherVehicle = null;
        }

        /// <summary>
        /// Bind JobRelated Equipments to the Hulcher Vehicles Grid
        /// </summary>
        public void BindEquipment()
        {
            try
            {
                _viewModel.BindEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load filtered data to bind the Vehicles grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Load filtered data to bind the Vehicles grid.", false);
            }
        }

        /// <summary>
        /// Removes a first alert vehicle from the list
        /// </summary>
        public void DeleteFirstAlertVehicle()
        {
            _viewModel.DeleteFirstAlertVehicle();
            _viewModel.BindPersonVehiclesList();
        }

        /// <summary>
        /// RowData bound for gvFilteredEquipments
        /// </summary>
        public void FilteredEquipmentsRowDataBound()
        {
            _viewModel.FilteredEquipmentsRowDataBound();
        }

        /// <summary>
        /// Shows VehicleForm
        /// </summary>
        public void ShowVehiclesForm()
        {
            _view.VehiclesFormVisible = true;
            _view.VehiclesListVisible = false;
            _view.HulcherVehicleHeaderEnabled = (!_view.IsVehicleListEdit);
        }

        /// <summary>
        /// Shows HulcherVehicles
        /// </summary>
        public void ShowHulcherVehicles()
        {
            _view.HulcherVehiclesVisible = true;
        }

        /// <summary>
        /// Shows OtherVehicles
        /// </summary>
        public void ShowOtherVehicles()
        {
            _view.OtherVehiclesVisible = true;
        }

        /// <summary>
        /// Hides VehicleForm
        /// </summary>
        public void HideVehiclesForm()
        {
            _view.VehiclesFormVisible = false;
            _view.VehiclesListVisible = true;
        }

        public void ClearValidationFields()
        {
            _view.DivisionValue = string.Empty;
            _view.FirstAlertTypeValue = string.Empty;
        }

        /// <summary>
        /// Hides HulcherVehicles
        /// </summary>
        public void HideHulcherVehicles()
        {
            _view.HulcherVehiclesVisible = false;
        }

        /// <summary>
        /// Hides OtherVehicles
        /// </summary>
        public void HideOtherVehicles()
        {
            _view.OtherVehiclesVisible = false;
        }

        /// <summary>
        /// gvVehiclesList RowCommand
        /// </summary>
        public void VehiclesListRowCommand()
        {
            if (_view.VehiclesListCommandName == "VehicleEdit")
            {
                ResetAddVehicle();
                _view.CurrentFirstAlertVehicleIndex = _view.VehiclesListCommandArgument;
                ShowVehiclesForm();
            }

            if (_view.VehiclesListCommandName == "VehicleDelete")
            {
                
                _view.CurrentFirstAlertVehicleIndex = _view.VehiclesListCommandArgument;
                List<FirstAlertPersonVO> person = _view.FirstAlertPersonList.Where(e => e.FirstAlertVehicleIndex.HasValue && _view.FirstAlertVehicleList[_view.CurrentFirstAlertVehicleIndex].TemporaryID == e.FirstAlertVehicleIndex.Value).ToList();

                if (person.Count == 0)
                {
                    DeleteFirstAlertVehicle();
                    ResetAddVehicle();
                }
                else
                {
                    string message = @"The vehicle can not be removed because it is associated with the following person(s):\n{0}";
                    List<string> names = new List<string>();

                    for (int i = 0; i < person.Count; i++)
                        names.Add(string.Format(@"{0}, {1}",person[i].LastName, person[i].FirstName));

                    message = string.Format(message, string.Join(@"\n", names));

                    _view.DisplayMessage(message, false);
                }
            }
        }

        public void SetVehiclesListItems()
        {
            _view.FirstAlertVehicleListUnitNumber = _view.FirstAlertVehicleListDataItem.UnitNumber;
            _view.FirstAlertVehicleListMake = _view.FirstAlertVehicleListDataItem.Make;
            _view.FirstAlertVehicleListModel = _view.FirstAlertVehicleListDataItem.Model;
            if (_view.FirstAlertVehicleListDataItem.Year.HasValue)
                _view.FirstAlertVehicleListYear = _view.FirstAlertVehicleListDataItem.Year.Value.ToString();
            _view.FirstAlertVehicleListDamage = _view.FirstAlertVehicleListDataItem.Damage;

            if (_view.FirstAlertVehicleListDataItem.IsHulcherVehicle)
                _view.FirstAlertVehicleListHulcher = "Yes";
            else
                _view.FirstAlertVehicleListHulcher = "No";
        }

        #endregion

        #region [ Person ]

        public void ResetPersonAdd()
        {
            _view.CurrentFirstAlertPersonIndex = -1;
            _view.EmployeeDataSource = new List<CS_Employee>();
            _view.FilterEmployeeByJobID = (_view.JobID != 0);
            _view.EmployeeFilter = null;
            _view.EmployeeFilterValue = string.Empty;
            _view.NewFirstAlertPerson = null;
            _view.UnblockPeopleFilterForEdit();
        }

        public void HidePersonForm()
        {
            _view.PeopleFormVisible = false;
            _view.PeopleListVisible = true;
        }

        public void ShowPersonForm()
        {
            _view.PeopleFormVisible = true;
            _view.PeopleListVisible = false;
        }

        public void AddPersonToList()
        {
            try
            {
                _viewModel.AddPersonToList();
                ResetPersonAdd();
                HidePersonForm();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save Person Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save Person Information.", false);
            }
        }

        public void RemovePersonFromList()
        {
            _viewModel.RemovePersonFromList();
        }

        public void BindEmployee()
        {
            _viewModel.BindEmployee();
        }

        public void BindEmployeeByID()
        {
            _viewModel.BindEmployeeByID();
        }

        public void AddEmployeeListDetails()
        {
            _viewModel.AddEmployeeListDetails();
        }

        public void AddPeopleListDetails()
        {
            _viewModel.AddPeopleListDetails();
        }

        #endregion

        #region [ Contact Personal ]

        public void FillContactPersonalRow()
        {
            try
            {
                _viewModel.FillContactPersonalRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to fill the Contact Personal row!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to to fill the Contact Personal row.", false);
            }
        }

        #endregion

        #endregion
    }
}
