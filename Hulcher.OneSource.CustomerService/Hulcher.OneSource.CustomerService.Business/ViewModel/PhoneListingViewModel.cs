using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class PhoneListingViewModel : IDisposable
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the phone listing view interface
        /// </summary>
        private IPhoneListingView _view;

        /// <summary>
        /// Instance of the employee model
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the customer model
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the division model
        /// </summary>
        private DivisionModel _divisionModel;

        #endregion

        #region [ Constructor ]
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">isntance of the phone listing view</param>
        public PhoneListingViewModel(IPhoneListingView view)
        {
            _view = view;
            _employeeModel = new EmployeeModel();
            _customerModel = new CustomerModel();
            _divisionModel = new DivisionModel();
        }
        #endregion

        #region [ Methods ]

        public void ListFilteredPhoneListing()
        {
            switch (_view.PhoneListingFilter)
            {
                case Globals.Phone.PhoneFilterType.Division:
                    _view.PhoneListingDivisionDataSource = _divisionModel.ParseEntityToDivisionPhone(_divisionModel.ListLocalDivisionByName(_view.FilterValue));
                    _view.AlterVisibilityCustomerGrid(false);
                    _view.AlterVisibilityEmployeeGrid(false);
                    _view.AlterVisibilityDivisionGrid(true);
                    break;
                case Globals.Phone.PhoneFilterType.Customer:
                    _view.PhoneListingCustomerDataSource = _customerModel.ParseEntityToCustomerPhone(_customerModel.ListCustomerContactPhoneInformation(Globals.CustomerMaintenance.FilterType.Customer, _view.FilterValue));
                    _view.AlterVisibilityCustomerGrid(true);
                    _view.AlterVisibilityEmployeeGrid(false);
                    _view.AlterVisibilityDivisionGrid(false);
                    break;
                case Globals.Phone.PhoneFilterType.Employee:
                    _view.ListFilteredEmployeePhoneListing = _employeeModel.ListFilteredEmployeePhoneVO(_view.PhoneListingFilter, _view.FilterValue);
                    _view.AlterVisibilityCustomerGrid(false);
                    _view.AlterVisibilityDivisionGrid(false);
                    _view.AlterVisibilityEmployeeGrid(true);
                    break;
                default:
                    break;
            }
        }

        #region [ Employee ]

        /// <summary>
        /// Bind emplotee phone listing information on the grid
        /// </summary>
        public void BindPhoneListingEmployeeRow()
        {
            if (_view.PhoneListingDataItem != null)
            {
                EmployeePhoneVO phoneListingVO = _view.PhoneListingDataItem;

                _view.PhoneListingEmployeeNameRow = phoneListingVO.EmployeeName;

                _view.PhoneListingPhoneTypeRow = phoneListingVO.PhoneType;
                _view.PhoneListingPhoneNumber = phoneListingVO.PhoneNumber;

            }
        }

        #endregion

        #region [ Customer ]

        public void BindPhoneListingCustomerRow()
        {
            if (_view.PhoneListingCustomerRowDataItem != null)
            {
                _view.PhoneListingCustomerCustomerName = _view.PhoneListingCustomerRowDataItem.CustomerName;
                _view.PhoneListingCustomerCustomerNumber = _view.PhoneListingCustomerRowDataItem.CustomerNumber;
                _view.PhoneListingCustomerContactName = _view.PhoneListingCustomerRowDataItem.ContactName;
                _view.PhoneListingCustomerPhoneType = _view.PhoneListingCustomerRowDataItem.PhoneType;
                _view.PhoneListingCustomerPhoneNumber = _view.PhoneListingCustomerRowDataItem.PhoneNumber;
                _view.PhoneListingCustomerCustomerNotes = _view.PhoneListingCustomerRowDataItem.CustomerNotes;
            }
        }

        #endregion

        #region [ Division ]

        public void BindDivisionPhoneTypeListing()
        {
            _view.DivisionPhoneTypeListingDataSource = _divisionModel.LoadPhoneTypes();
        }

        public void BindPhoneListingDivisionRow()
        {
            if (_view.PhoneListingDivisionRowDataItem != null)
            {
                _view.PhoneListingDivisionID = _view.PhoneListingDivisionRowDataItem.DivisionID;
                _view.PhoneListingDivisionName = _view.PhoneListingDivisionRowDataItem.DivisionName;
                _view.PhoneListingDivisionAddress = _view.PhoneListingDivisionRowDataItem.FullAddressInformation;
                _view.PhoneListingDivisionPhoneType = _view.PhoneListingDivisionRowDataItem.PhoneType;
                _view.PhoneListingDivisionPhoneNumber = _view.PhoneListingDivisionRowDataItem.PhoneNumber;
            }
        }

        public void LoadLocalDivision()
        {
            if (_view.LocalDivisionID.HasValue)
            {
                _view.ShowDeleteButton = true;
                CS_LocalDivision divisionData = _divisionModel.LoadLocalDivisionWithPhones(_view.LocalDivisionID.Value);
                if (null != divisionData)
                {
                    _view.LocalDivisionNumber = divisionData.Name;
                    _view.LocalDivisionAddress = divisionData.Address;
                    if (divisionData.StateID.HasValue)
                    {
                        _view.LocalDivisionStateID = divisionData.StateID;
                        _view.LocalDivisionStateName = divisionData.CS_State.AcronymName;
                    }

                    if (divisionData.CityID.HasValue)
                    {
                        _view.LocalDivisionCityID = divisionData.CityID;
                        _view.LocalDivisionCityName = divisionData.CS_City.CityStateInformation;
                    }

                    if (divisionData.ZipCodeID.HasValue)
                    {
                        _view.LocalDivisionZipCodeID = divisionData.ZipCodeID;
                        _view.LocalDivisionZipCode = divisionData.CS_ZipCode.ZipCodeNameEdited;
                    }
                    _view.LocalDivisionPhoneListing = divisionData.CS_DivisionPhoneNumber.ToList();

                    _view.ShowHidePanelDivisionPhoneNumber = true;
                    _view.ShowHidePanelButtons = true;
                    _view.ShowHideFilter = false;

                }
            }
        }

        /// <summary>
        /// Saves or Updates a local Division
        /// </summary>
        public void SaveLocalDivision()
        {
            CS_LocalDivision divisionData = new CS_LocalDivision();
            if (_view.LocalDivisionID.HasValue)
                divisionData = _divisionModel.LoadLocalDivision(_view.LocalDivisionID.Value);
            else
            {
                divisionData.CreatedBy = _view.Username;
                divisionData.CreationDate = DateTime.Now;
                divisionData.Active = true;
            }

            divisionData.Name = _view.LocalDivisionNumber;
            divisionData.Address = _view.LocalDivisionAddress;
            divisionData.StateID = _view.LocalDivisionStateID;
            divisionData.CityID = _view.LocalDivisionCityID;
            divisionData.ZipCodeID = _view.LocalDivisionZipCodeID;
            divisionData.ModifiedBy = _view.Username;
            divisionData.ModificationDate = DateTime.Now;

            _divisionModel.SaveLocalDivision(divisionData, _view.LocalDivisionPhoneListing);

            ListFilteredPhoneListing();

            _view.ShowHidePanelDivisionPhoneNumber = false;
            _view.ShowHidePanelButtons = false;
            _view.ShowHideFilter = true;
            _view.ClearDivisionFields();
        }

        /// <summary>
        /// Delete or Updates a local Division
        /// </summary>
        public void DeleteLocalDivision()
        {
            _divisionModel.DeleteLocalDivision(_view.LocalDivisionID.Value, _view.Username);

            ListFilteredPhoneListing();

            _view.ShowHidePanelDivisionPhoneNumber = false;
            _view.ShowHidePanelButtons = false;
            _view.ShowHideFilter = true;
            _view.ClearDivisionFields();
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _customerModel.Dispose();
            _employeeModel.Dispose();

            _employeeModel = null;
            _customerModel = null;
        }
        #endregion
    }
}
