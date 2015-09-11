using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class AutoCompleteWebServicePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Sample Page View Interface
        /// </summary>
        private IAutoCompleteWebServiceView _view;

        /// <summary>
        /// Instance of the Contact Business Class
        /// </summary>
        private CustomerModel _customerModel;
        private EmployeeModel _employeeModel;
        private CallLogModel _callLogModel;
        private DivisionModel _divisionModel;
        private JobModel _jobModel;
        private LocationModel _locationModel;
        private EquipmentModel _equipmentModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CustomerInfo View Interface</param>
        public AutoCompleteWebServicePresenter(IAutoCompleteWebServiceView view)
        {
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        /// List all Customers in the Database and fill the Dropdown
        /// </summary>
        public void ListCustomerByName()
        {
            try
            {
                _customerModel = new CustomerModel();
                _view.CustomerList = _customerModel.ListCustomerByName(_view.PrefixText).OrderBy(e => e.FullCustomerInformation).ToList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Customer Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListDynamicsContactByName()
        {
            try
            {
                _customerModel = new CustomerModel();
                _view.DynamicsContactList = _customerModel.ListFilteredContactsByName(long.Parse(_view.ContextKey), true, _view.PrefixText);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListCustomerServiceContactByName()
        {
            try
            {
                _customerModel = new CustomerModel();
                _view.CustomerServiceContactList = _customerModel.ListFilteredContactsByName(long.Parse(_view.ContextKey), false, _view.PrefixText);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListJobStatusByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobStatusList = _jobModel.ListJobStatusByDescription(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListJobNumberByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllJobsByNumber(_view.PrefixText, _view.ContextKey);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListJobNumberByDescriptionWithGeneral()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllJobsByNumberWithGeneral(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListBillableJobNumberByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllBillableJobsByNumber(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListEmployeeByName()
        {
            try
            {
                long divisionId = 0;

                if (!string.IsNullOrEmpty(_view.ContextKey))
                    divisionId = Convert.ToInt32(_view.ContextKey);

                _employeeModel = new EmployeeModel();
                _view.EmployeeList = _employeeModel.ListFilteredEmployeeByName(divisionId, _view.PrefixText).OrderBy(e => e.CS_Division, new Globals.JobRecord.EmployeeComparer()).ThenBy(e => e.FullName).ToList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListAllCustomerContactByName()
        {
            try
            {
                _customerModel = new CustomerModel();
                _view.ContactList = _customerModel.ListAllFilteredContactsByName(Int32.Parse(_view.ContextKey), _view.PrefixText);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List All divisions filtered by Name
        /// </summary>
        public void ListDivisionByName()
        {
            try
            {
                using (_divisionModel = new DivisionModel())
                {
                    _view.DivisionList = _divisionModel.ListAllFilteredDivisionByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List All Price Types filtered by Name
        /// </summary>
        public void ListPriceTypeByName()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.PriceTypeList = _jobModel.ListAllFilteredPriceTypesByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Price Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all job action filtered by name
        /// </summary>
        public void ListJobActionByName()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobActionList = _jobModel.ListAllJobActionByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Action information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListCityByNameAndState()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.CityList = _locationModel.ListCityByNameAndState(int.Parse(_view.ContextKey), _view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the City Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all active ZipCodes filtered by City and Name (Like)
        /// </summary>
        public void ListZipCodesByNameAndCity()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.ZipCodeList = _locationModel.ListZipCodeByNameAndCity(int.Parse(_view.ContextKey), _view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Zip Code Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }


        /// <summary>
        /// List all active States
        /// </summary>
        public void ListStatesByAcronym()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.StateList = _locationModel.ListAllStatesByAcronym(_view.PrefixText, _view.ContextKey);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the State Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all active States
        /// </summary>
        public void ListStatesByAcronymAndDivision()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.StateList = _locationModel.ListAllStatesByAcronymAndDivision(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the State Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all Active Call Types
        /// </summary>
        public void ListCallTypes()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.CallTypeList = _callLogModel.ListCallTypeFilteredByDescription(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Call Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all equipment type
        /// </summary>
        public void ListAllEquipmentType()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentTypeList = _equipmentModel.ListAllEquipmentType();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Call Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all equipment type by name
        /// </summary>
        public void ListAllEquipmentTypeByName()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentTypeList = _equipmentModel.ListAllEquipmentTypeByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Call Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List equipment by equipment type
        /// </summary>
        public void ListAllEquipmentByEqType()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentType = _equipmentModel.ListAllEquipmentByEqType(int.Parse(_view.ContextKey), _view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListAllLocalEquipmentTypeByName()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.LocalEquipmentTypeList = _equipmentModel.ListAllLocalEquipmentTypeByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the local equipment type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List of all project manager
        /// </summary>
        public void ListProjectManager()
        {
            try
            {
                using (_employeeModel = new EmployeeModel())
                {
                    _view.ProjectManagerList = _employeeModel.ListAllEmployeeProjectManagerByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {

                 Logger.Write(string.Format("There was an error loading the Project Managers\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        #endregion
    }
}
