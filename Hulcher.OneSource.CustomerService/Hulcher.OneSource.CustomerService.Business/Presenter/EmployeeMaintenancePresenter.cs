using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class EmployeeMaintenancePresenter
    {
        #region [ Attributes ]

        private IEmployeeMaintenanceView _view;

        private EmployeeMaintenanceViewModel _viewModel;

        private EmployeeModel _employeeModel;

        #endregion

        #region [ Constructor ]

        public EmployeeMaintenancePresenter(IEmployeeMaintenanceView view)
        {
            _view = view;

            _viewModel = new EmployeeMaintenanceViewModel(view);
            _employeeModel = new EmployeeModel();
        }

        public EmployeeMaintenancePresenter(IEmployeeMaintenanceView view, EmployeeModel model)
        {
            _view = view;

            _viewModel = new EmployeeMaintenanceViewModel(view);
            _employeeModel = model;
        }

        #endregion

        #region [ Methods ]

        public void VerifyAccess()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.Username, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.ManageCallCriteria });

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

        public void Save()
        {
            try
            {
                _viewModel.Save();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save the Employee information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save the Employee information. Please try again.", false);
            }
        }

        public void LoadEmployeeInfo()
        {
            CS_Employee selectedEmployee = null;
            try
            {
                if (_view.EmployeeId.HasValue)
                {
                    IList<CS_EmployeeEmergencyContact> emergencyContactsList = new List<CS_EmployeeEmergencyContact>();
                    selectedEmployee = _employeeModel.GetEmployeeForMaintenance(_view.EmployeeId.Value);
                    _view.EmployeeName = selectedEmployee.FirstName + "," + selectedEmployee.Name;
                    _view.HireDate = selectedEmployee.HireDate;
                    _view.PersonID = selectedEmployee.PersonID;
                    _view.Address = selectedEmployee.Address;
                    _view.Address2 = selectedEmployee.Address2;
                    _view.City = selectedEmployee.City;
                    _view.State = selectedEmployee.StateProvinceCode;
                    _view.Country = selectedEmployee.CountryCode;
                    _view.PostalCode = selectedEmployee.PostalCode;
                    _view.Position = selectedEmployee.BusinessCardTitle;
                    _view.EmployeeDivision = selectedEmployee.FullDivisionName;
                    _view.PassportNumber = selectedEmployee.PassportNumber;
                    _view.IsKeyPerson = selectedEmployee.IsKeyPerson;
                    foreach (var contact in selectedEmployee.CS_EmployeeEmergencyContact)
                    {
                        emergencyContactsList.Add(contact);
                    }
                    _view.DayAreaCode = selectedEmployee.DayAreaCode;
                    _view.DayPhone = selectedEmployee.DayPhone;
                    _view.FaxAreaCode = selectedEmployee.FaxAreaCode;
                    _view.FaxPhone = selectedEmployee.FaxPhone;

                    _view.EmployeeContacts = emergencyContactsList;
                    _view.HomeAreaCode = selectedEmployee.HomeAreaCode;
                    _view.HomePhone = selectedEmployee.HomePhone;
                    _view.MobileAreaCode = selectedEmployee.MobileAreaCode;
                    _view.MobilePhone = selectedEmployee.MobilePhone;
                    _view.OtherPhoneAreaCode = selectedEmployee.OtherPhoneAreaCode;
                    _view.OtherPhone = selectedEmployee.OtherPhone;
                    _view.IsDentonPersonal = selectedEmployee.IsDentonPersonal;
                    _view.DriversLicenseNumber = selectedEmployee.DriversLicenseNumber;
                    _view.DriversLicenseClass = selectedEmployee.DriversLicenseClass;
                    _view.DriversLicenseState = selectedEmployee.DriversLicenseStateProvinceCode;
                    _view.DriversLicenseExpireDate = selectedEmployee.DriversLicenseExpireDate;
                    _view.CallCriteriaEmployeeID = _view.EmployeeId;
                }
                else
                    _view.CallCriteriaEmployeeID = null;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Employee information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Employee information. Please try again.", false);
            }

            if (null != selectedEmployee)
            {
                LoadEmployeeOffCallInformation(selectedEmployee.CS_EmployeeOffCallHistory.FirstOrDefault(e => e.Active));
                LoadEmployeeOffCallHistory(selectedEmployee.CS_EmployeeOffCallHistory.Where(e => !e.Active).ToList());
                LoadEmployeeCoverageInfo(selectedEmployee.CS_EmployeeCoverage.FirstOrDefault(e => e.Active));
                ListEmployeeCoverageHistory(selectedEmployee.CS_EmployeeCoverage.Where(e => !e.Active).ToList());
                EnableDisableCoverageRequiredFields();
                LoadPhoneTypes();
                LoadAdditionalPhoneNumbers(selectedEmployee.CS_PhoneNumber2.Where(e => e.Active).ToList());
                VerifyIfResourceIsAssignedToJob(selectedEmployee.CS_Resource.FirstOrDefault(e => e.Active));
            }
        }

        private void LoadEmployeeOffCallInformation(CS_EmployeeOffCallHistory selectedEmployeeOffCall)
        {
            try
            {
                if (selectedEmployeeOffCall != null)
                {
                    _view.IsOffCall = true;
                    _view.OffCallStartDate = selectedEmployeeOffCall.OffCallStartDate;
                    _view.OffCallEndDate = selectedEmployeeOffCall.OffCallEndDate;
                    _view.OffCallReturnTime = selectedEmployeeOffCall.OffCallReturnTime;
                    _view.ProxyEmployeeId = selectedEmployeeOffCall.CS_Employee_Proxy.ID;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Employee OffCall information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Employee OffCall information. Please try again.", false);
            }
        }

        private void LoadEmployeeOffCallHistory(IList<CS_EmployeeOffCallHistory> offCallHistoryList)
        {
            try
            {
                _view.OffCallHistoryList = offCallHistoryList;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Employee OffCall history!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Employee OffCall history. Please try again.", false);
            }
        }

        private void LoadEmployeeCoverageInfo(CS_EmployeeCoverage employeeCoverage)
        {
            try
            {
                if (employeeCoverage != null)
                {
                    _view.IsCoverage = true;
                    _view.CoverageDivisionID = employeeCoverage.DivisionID;
                    _view.CoverageStartDate = employeeCoverage.CoverageStartDate;
                    _view.CoverageStartTime = employeeCoverage.CoverageStartDate.TimeOfDay;
                    _view.CoverageEndDate = employeeCoverage.CoverageEndDate;
                    if (employeeCoverage.CoverageEndDate.HasValue)
                        _view.CoverageEndTime = employeeCoverage.CoverageEndDate.Value.TimeOfDay;

                    _view.CoverageDuration = employeeCoverage.Duration;

                    _view.DisplayCoverageStartFields = true;
                    if (!employeeCoverage.CoverageEndDate.HasValue)
                    {
                        _view.DisplayCoverageEndFields = false;
                        _view.CoverageEndDate = DateTime.Now;
                        _view.CoverageEndTime = DateTime.Now.TimeOfDay;
                    }
                    else
                        _view.DisplayCoverageEndFields = true;
                }
                else
                {
                    _view.IsCoverage = false;
                    _view.CoverageDivisionID = null;
                    _view.CoverageStartDate = null;
                    _view.CoverageStartTime = null;
                    _view.CoverageEndDate = null;
                    _view.CoverageEndTime = null;
                    _view.CoverageDuration = null;

                    _view.DisplayCoverageStartFields = false;
                    _view.DisplayCoverageEndFields = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Employee Coverage information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Employee Coverage information. Please try again.", false);
            }
        }

        private void ListEmployeeCoverageHistory(IList<CS_EmployeeCoverage> coverageHistory)
        {
            try
            {
                _view.CoverageHistoryList = coverageHistory;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Employee Coverage information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Employee Coverage information. Please try again.", false);
            }
        }

        /// <summary>
        /// Verify actual condictons to enable/disable required fields related to coverage on the page
        /// </summary>
        public void EnableDisableCoverageRequiredFields()
        {
            try
            {
                _view.RequireCoverageFields = _view.IsCoverage;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to set Employee Coverage Required fields!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to set Employee Coverage Required fields. Please try again.", false);
            }
        }

        /// <summary>
        /// Apply coverage enable/disable panel when employee is assigned to a job
        /// </summary>
        private void VerifyIfResourceIsAssignedToJob(CS_Resource resource)
        {
            try
            {
                _view.IsEmployeeAssignedToJob = (null != resource);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to set Employee Coverage Required fields!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to set Employee Coverage Required fields. Please try again.", false);
            }
        }

        private void LoadPhoneTypes()
        {
            _view.AdditionalContactPhoneTypeSource = _employeeModel.LoadPhoneTypes();
        }

        private void LoadAdditionalPhoneNumbers(List<CS_PhoneNumber> phoneNumberList)
        {
            _view.AdditionalContactPhoneGridDataSource = phoneNumberList;
        }

       

        #endregion
    }
}
