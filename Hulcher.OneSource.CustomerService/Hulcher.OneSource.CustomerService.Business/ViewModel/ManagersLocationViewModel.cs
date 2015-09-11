using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class ManagersLocationViewModel
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the page view. Constains access to Page shared properties
        /// </summary>
        private IManagersLocationView _view;

        /// <summary>
        /// Access to Employee related DB Objects
        /// </summary>
        private EmployeeModel _employeeModel;

        
        /// <summary>
        /// Access to CallLog related DB Objects
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructor ]
        public ManagersLocationViewModel(IManagersLocationView view)
        {
            _view = view;

            _employeeModel = new EmployeeModel(new EFUnitOfWork());
            _callLogModel = new CallLogModel();
        }

        public ManagersLocationViewModel(IManagersLocationView view, EmployeeModel employeeModel)
        {
            _view = view;

            _employeeModel = employeeModel;
        }
        #endregion

        #region [ Methods ]

        /// <summary>
        /// List all Managers Location
        /// </summary>
        /// <returns>list</returns>
        public void ListAllManagersLocation()
        {
            _view.ListAllManagersLocation = _employeeModel.ListAllManagersLocation().OrderBy(e => e.EmployeeFullName).ToList();
        }

        /// <summary>
        /// Bind rows of the gridview
        /// </summary>
        public void BindManagersLocationRow()
        {
            if (_view.ManagersLocationDataItem != null)
            {
                _view.ManagersLocationRowEmployeeName = _view.ManagersLocationDataItem.EmployeeFullName;
                _view.ManagersLocationRowEmployeeId = _view.ManagersLocationDataItem.EmployeeID;
                if (_view.ManagersLocationDataItem.JobID.HasValue)
                    _view.ManagersLocationRowJobId = _view.ManagersLocationDataItem.JobID.Value.ToString();
                else
                    _view.ManagersLocationRowJobId = null;

                if (_view.ManagersLocationDataItem.LastCallJobID.HasValue)
                    _view.ManagersLocationRowCallEntryJobId = _view.ManagersLocationDataItem.LastCallJobID.Value.ToString();
                if (_view.ManagersLocationDataItem.LastCallLogID.HasValue)
                    _view.ManagersLocationRowCallEntryId = _view.ManagersLocationDataItem.LastCallLogID.Value.ToString();

                if (!_callLogModel.GetCallTypeByDescription(_view.ManagersLocationDataItem.LastCallTypeDescription).IsAutomaticProcess)
                    _view.ManagersLocationRowLastCallType = _view.ManagersLocationDataItem.LastCallTypeDescription;

                _view.ManagersLocationRowHotelDetails = _view.ManagersLocationDataItem.LastHotelNote != null ? StringManipulation.TabulateString(_view.ManagersLocationDataItem.LastHotelNote) : string.Empty;
                _view.ManagersLocationRowJobNumber = _view.ManagersLocationDataItem.PrefixedNumber;
                _view.ManagersLocationRowLastCallLogDate = _view.ManagersLocationDataItem.LastCallLogDate;
            }
        }

        /// <summary>
        /// Filtering of the Mananagers Location
        /// </summary>
        public void FilterManagersLocation()
        {
            _view.ListAllManagersLocation = _employeeModel.ListFilteredManagersLocation(_view.Name, _view.CallTypeID, _view.JobID).OrderBy(e => e.EmployeeFullName).ToList();
        }
        #endregion
    }
}
