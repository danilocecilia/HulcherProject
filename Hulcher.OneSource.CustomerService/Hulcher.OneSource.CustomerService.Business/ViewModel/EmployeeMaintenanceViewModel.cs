using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class EmployeeMaintenanceViewModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Page View. Contains access to Page shared properties.
        /// </summary>
        private IEmployeeMaintenanceView _view;

        /// <summary>
        /// Call Criteria Business Class
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Employee Business Class
        /// </summary>
        private EmployeeModel _employeeModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default Class Constructor
        /// </summary>
        /// <param name="view">Interface View</param>
        public EmployeeMaintenanceViewModel(IEmployeeMaintenanceView view)
        {
            _view = view;

            _callCriteriaModel = new CallCriteriaModel();
            _employeeModel = new EmployeeModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Saves employee related information
        /// </summary>
        public void Save()
        {
            bool save = true;
            string message = "";

            // Set employee information
            CS_Employee employee = SetEmployeeInfo();

            // Set Off Call information
            CS_EmployeeOffCallHistory offCall = SetOffCallInfo();

            // Set Coverage information
            CS_EmployeeCoverage coverage = SetCoverageInfo();

            if (_view.IsOffCall)
            {
                CS_EmployeeOffCallHistory proxyOffCallCheck = _employeeModel.GetActiveOffCallEmployeeById(offCall.ProxyEmployeeID);

                if (null != proxyOffCallCheck)
                {
                    save = false;
                    message += "The off call proxy employee can not be set as proxy for the current employee, because it has an active off call.";
                }
            }

            if (save)
            {
                // Save information
                _employeeModel.SaveEmployee(employee, offCall, coverage, _view.Username, _view.IsCoverage, _view.IsOffCall, _view.AdditionalContactPhoneList);
                _view.DisplayMessage("Employee Profile saved successfully.", true);
            }
            else
            {
                _view.DisplayMessage(message, false);
            }
        }

        #endregion

        #region [ Private Methods ]

        private CS_Employee SetEmployeeInfo()
        {
            CS_Employee employee = null;
            if (_view.EmployeeId.HasValue)
            {
                employee = new CS_Employee();

                employee.ID = _view.EmployeeId.Value;
                employee.Address = _view.Address;
                employee.City = _view.City;
                employee.StateProvinceCode = _view.State;
                employee.CountryCode = _view.Country;
                employee.Address2 = _view.Address2;
                employee.PostalCode = _view.PostalCode;
                employee.DayAreaCode = _view.DayAreaCode;
                employee.DayPhone = _view.DayPhone;
                employee.HomeAreaCode = _view.HomeAreaCode;
                employee.HomePhone = _view.HomePhone;
                employee.FaxAreaCode = _view.FaxAreaCode;
                employee.FaxPhone = _view.FaxPhone;
                employee.MobileAreaCode = _view.MobileAreaCode;
                employee.MobilePhone = _view.MobilePhone;
                employee.OtherPhoneAreaCode = _view.OtherPhoneAreaCode;
                employee.OtherPhone = _view.OtherPhone;
                employee.IsDentonPersonal = _view.IsDentonPersonal;
                employee.IsKeyPerson = _view.IsKeyPerson;
            }
            return employee;
        }

        private CS_EmployeeOffCallHistory SetOffCallInfo()
        {
            CS_EmployeeOffCallHistory offCall = null;
            if (_view.EmployeeId.HasValue)
            {
                offCall = new CS_EmployeeOffCallHistory();

                offCall.EmployeeID = _view.EmployeeId.Value;
                if (_view.ProxyEmployeeId.HasValue)
                    offCall.ProxyEmployeeID = _view.ProxyEmployeeId.Value;
                if (_view.OffCallStartDate.HasValue)
                    offCall.OffCallStartDate = _view.OffCallStartDate.Value;
                if (_view.OffCallEndDate.HasValue)
                    offCall.OffCallEndDate = _view.OffCallEndDate.Value;
                if (_view.OffCallReturnTime.HasValue)
                    offCall.OffCallReturnTime = _view.OffCallReturnTime.Value;
            }
            return offCall;
        }

        private CS_EmployeeCoverage SetCoverageInfo()
        {
            CS_EmployeeCoverage coverage = null;
            if (_view.EmployeeId.HasValue)
            {
                coverage = new CS_EmployeeCoverage();
                coverage.EmployeeID = _view.EmployeeId.Value;
                if (_view.CoverageStartDate.HasValue && _view.CoverageStartTime.HasValue)
                    coverage.CoverageStartDate = _view.CoverageStartDate.Value + _view.CoverageStartTime.Value;
                if (_view.CoverageEndDate.HasValue && _view.CoverageEndTime.HasValue)
                    coverage.CoverageEndDate = _view.CoverageEndDate.Value + _view.CoverageEndTime.Value;
                if (_view.CoverageDuration.HasValue)
                    coverage.Duration = _view.CoverageDuration.Value;
                if (_view.CoverageDivisionID.HasValue)
                    coverage.DivisionID = _view.CoverageDivisionID.Value;
            }
            return coverage;
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _callCriteriaModel.Dispose();
            _employeeModel.Dispose();

            _callCriteriaModel = null;
        }

        #endregion
    }
}
