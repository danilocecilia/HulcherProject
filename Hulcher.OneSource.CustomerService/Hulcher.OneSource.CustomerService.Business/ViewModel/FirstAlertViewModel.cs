using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// FirstAlertViewModel Class
    /// </summary>
    public class FirstAlertViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the page view. Constains access to Page shared properties
        /// </summary>
        private IFirstAlertView _view;

        /// <summary>
        /// Access to first alert related DB objects
        /// </summary>
        private FirstAlertModel _firstAlertModel;

        /// <summary>
        /// Instance of the Equipment Model
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of the Employee Model
        /// </summary>
        private EmployeeModel _employeeModel;

        #endregion

        #region [ Constructor ]

        public FirstAlertViewModel(IFirstAlertView view)
        {
            _view = view;
            _firstAlertModel = new FirstAlertModel();
        }

        public FirstAlertViewModel(IFirstAlertView view, FirstAlertModel firstAlertModel)
        {
            _view = view;
            _firstAlertModel = firstAlertModel;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Load the fields for a existing First Alert
        /// </summary>
        public void LoadExistingFirstAlert()
        {
            _view.FirstAlertEntity = _firstAlertModel.GetFirstAlertByCallLogId(_view.CallLogID.Value);
            _view.EditMode = true;
        }

        /// <summary>
        /// List all data of first alert from db
        /// </summary>
        public void LoadFirstAlertList()
        {
            _view.FirstAlertList = _firstAlertModel.ListAllFirstAlert();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadFilteredFirstAlert()
        {
            _view.FirstAlertList = _firstAlertModel.ListFilteredFirstAlert(_view.FirstAlertFilter.Value, _view.FilterValue);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveUpdateFirstAlert()
        {
            CS_FirstAlert firstAlert = _view.FirstAlertEntity;
            bool openReport = firstAlert.ID.Equals(0);

            _firstAlertModel.UserName = _view.UserName;
            _firstAlertModel.SaveUpdateFirstAlert(firstAlert, _view.FirstAlertPersonList, _view.FirstAlertVehicleList, _view.FirstAlertDivisionList, _view.FirstAlertFirstAlertTypeList, _view.FirstAlertContactPersonalList);

            if (openReport)
                _view.OpenReport(firstAlert.ID);

            if (_view.JobIDFromExternalSource.HasValue)
                _view.DisplayMessage("First Alert saved successfully!", true);
        }

        public void DeleteFirstAlert()
        {
            _firstAlertModel.DeleteFirstAlert(_view.FirstAlertID);
        }

        /// <summary>
        /// Set the fields to theier properties to be bound on the gridview
        /// </summary>
        public void SetDetailedFirstAlertRowData()
        {
            if (null != _view.FirstAlertRowDataItem)
            {
                _view.FirstAlertRowAlertId = _view.FirstAlertRowDataItem.ID.ToString();

                _view.FirstAlertRowAlertNumber = _view.FirstAlertRowDataItem.Number.ToString();

                if (null != _view.FirstAlertRowDataItem.CS_FirstAlertFirstAlertType && _view.FirstAlertRowDataItem.CS_FirstAlertFirstAlertType.Count > 0)
                {
                    List<CS_FirstAlertFirstAlertType> lstFirstAlertType = _view.FirstAlertRowDataItem.CS_FirstAlertFirstAlertType.Where(e => e.Active).ToList();

                    string types = string.Join(", ", lstFirstAlertType.Select(e => e.CS_FirstAlertType.Description));

                    _view.FirstAlertRowFirstAlertType = types;
                }

                _view.FirstAlertRowAlertDateAndTime = _view.FirstAlertRowDataItem.Date.ToString("MM/dd/yyyy") + " " + _view.FirstAlertRowDataItem.Date.ToShortTimeString();

                _view.FirstAlertRowJobNumber = _view.FirstAlertRowDataItem.CS_Job.Number;

                if (null != _view.FirstAlertRowDataItem.CS_FirstAlertDivision && _view.FirstAlertRowDataItem.CS_FirstAlertDivision.Count > 0)
                {
                    List<CS_FirstAlertDivision> lstDivisions = _view.FirstAlertRowDataItem.CS_FirstAlertDivision.Where(a => a.Active).ToList();

                    string divisions = string.Join(", ", lstDivisions.Select(e => e.CS_Division.Name));

                    _view.FirstAlertRowDivision = divisions;
                }

                if (_view.FirstAlertRowDataItem.CustomerID.HasValue)
                    _view.FirstAlertRowCustomer = _view.FirstAlertRowDataItem.CS_Customer.FullCustomerInformation;

                _view.FirsAlertRowLocation = _view.FirstAlertRowDataItem.Location;

            }
        }

        public void BindEquipment()
        {
            int? joId = null;

            if (_view.FilterVehiclesByJobID)
                joId = _view.JobID;

            using (_equipmentModel = new EquipmentModel())
            {
                if (_view.EquipmentFilter.HasValue && !string.IsNullOrEmpty(_view.EquipmentFilterValue))
                    _view.FilteredEquipmentsDataSource = _equipmentModel.ListFilteredEquipmentInfo(_view.EquipmentFilter.Value, _view.EquipmentFilterValue, joId);
                else if (joId.HasValue)
                    _view.FilteredEquipmentsDataSource = _equipmentModel.ListAllComboByJob(joId.Value);
                else
                    _view.FilteredEquipmentsDataSource = new List<CS_View_EquipmentInfo>();
            }
        }

        public void AddVehicles()
        {
            IList<CS_FirstAlertVehicle> vehicleList = _view.SelectedVehicles;

            if (vehicleList.Count > 0)
                _view.FirstAlertVehicleList = vehicleList;
        }

        public void BindPersonVehiclesList()
        {
            _view.PersonVehicleList = _view.FirstAlertVehicleList;
        }

        public void DeleteFirstAlertVehicle()
        {
            if (_view.CurrentFirstAlertVehicleIndex != -1)
            {
                IList<CS_FirstAlertVehicle> vehicleList = _view.FirstAlertVehicleList;

                vehicleList.RemoveAt(_view.CurrentFirstAlertVehicleIndex);
                _view.FirstAlertVehicleList = vehicleList;
            }
        }

        public void GetVehicleByEquipmentID()
        {
            using (_equipmentModel = new EquipmentModel())
            {
                IList<CS_View_EquipmentInfo> list = new List<CS_View_EquipmentInfo>();
                CS_View_EquipmentInfo equipment = _equipmentModel.GetSpecificEquipmentFromView(_view.CurrentFirstAlertVehicleEquipmentID);

                if (equipment != null)
                {
                    list.Add(equipment);
                    _view.FilteredEquipmentsDataSource = list;
                }
            }
        }

        public void FilteredEquipmentsRowDataBound()
        {
            CS_FirstAlertVehicle vehicle = _view.FirstAlertVehicleList.FirstOrDefault(f => f.EquipmentID == _view.FilteredEquipmentsEquipmentID);

            if (null != vehicle)
            {
                _view.FilteredEquipmentsDamage = vehicle.Damage;
                _view.FilteredEquipmentsEstCost = string.Format("{0:0.00}", vehicle.EstimatedCost);
                _view.FilteredEquipmentsSelect = true;
            }
        }

        #region [ Person ]

        public void AddPersonToList()
        {
            FirstAlertPersonVO person = _view.NewFirstAlertPerson;
            IList<FirstAlertPersonVO> personList = _view.FirstAlertPersonList;

            if (person != null)
            {
                if (_view.CurrentFirstAlertPersonIndex > -1)
                {
                    personList[_view.CurrentFirstAlertPersonIndex] = person;
                }
                else
                {
                    personList.Add(person);
                }

                _view.FirstAlertPersonList = personList;
            }
        }

        public void RemovePersonFromList()
        {
            if (_view.CurrentFirstAlertPersonIndex != -1)
            {
                IList<FirstAlertPersonVO> personList = _view.FirstAlertPersonList;

                personList.RemoveAt(_view.CurrentFirstAlertPersonIndex);
                _view.FirstAlertPersonList = personList;
            }
        }

        public void BindEmployee()
        {
            int? jobId = null;

            if (_view.FilterEmployeeByJobID)
                jobId = _view.JobID;

            using (_employeeModel = new EmployeeModel())
            {
                _view.EmployeeDataSource = _employeeModel.ListFilteredEmployee(_view.EmployeeFilter.Value, _view.EmployeeFilterValue, jobId);
            }
        }

        public void BindEmployeeByID()
        {
            using (_employeeModel = new EmployeeModel())
            {
                List<CS_Employee> returnList = new List<CS_Employee>();

                if (_view.EditEmployeeID.HasValue)
                    returnList.Add(_employeeModel.GetEmployee(_view.EditEmployeeID.Value));

                _view.EmployeeDataSource = returnList;
            }
        }

        public void AddEmployeeListDetails()
        {
            if (null !=_view.EmployeeListRowDataItem.CS_Division)
            {
                _view.EmployeeListRowDivision = _view.EmployeeListRowDataItem.FullDivisionName;
                if (null != _view.EmployeeListRowDataItem.CS_Division.CS_State)
                    _view.EmployeeListRowLocation = _view.EmployeeListRowDataItem.CS_Division.CS_State.AcronymName;
            }

            _view.EmployeeListRowFirstName = _view.EmployeeListRowDataItem.FirstName;
            _view.EmployeeListRowLastName = _view.EmployeeListRowDataItem.Name;
            _view.EmployeeListRowID = _view.EmployeeListRowDataItem.ID.ToString();
        }

        public void AddPeopleListDetails()
        {
            _view.PeopleListRowFirstName = _view.PeopleListRowDataItem.FirstName;
            _view.PeopleListRowLastName = _view.PeopleListRowDataItem.LastName;

            if (_view.PeopleListRowDataItem.IsHulcherEmployee)
                _view.PeopleListRowHulcherEmployee = "Yes";
            else
                _view.PeopleListRowHulcherEmployee = "No";
            
            _view.PeopleListRowLocation = string.Format("{0}", _view.PeopleListRowDataItem.CityName);
        }

        #endregion

        #region [ Contact Personal ]

        public void FillContactPersonalRow()
        {
            if (null != _view.ContactPersonalRowDataItem)
            {
                _view.ContactPersonalRowID = _view.ContactPersonalRowDataItem.ID;
                if (_view.ContactPersonalRowDataItem.EmployeeID.HasValue)
                    _view.ContactPersonalRowName = _view.ContactPersonalRowDataItem.CS_Employee.FullName;
                else if (_view.ContactPersonalRowDataItem.ContactID.HasValue)
                    _view.ContactPersonalRowName = _view.ContactPersonalRowDataItem.CS_Contact.FullName;
                _view.ContactPersonalRowEmailAdvisedDate = _view.ContactPersonalRowDataItem.EmailAdviseDate;
                _view.ContactPersonalRowVMXAdvisedDate = _view.ContactPersonalRowDataItem.VMXAdviseDate;
                _view.ContactPersonalRowInPersonAdvisedDate = _view.ContactPersonalRowDataItem.InPersonAdviseDate;
            }
        }

        #endregion

        #endregion
    }
}
