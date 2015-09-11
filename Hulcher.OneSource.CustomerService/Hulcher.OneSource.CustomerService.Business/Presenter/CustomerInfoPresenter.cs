using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{

    public class CustomerInfoPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Sample Page View Interface
        /// </summary>
        private ICustomerInfoView _view;

        /// <summary>
        /// Instance of the Contact Business Class
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the Division Business Class
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of the Employee Business Class
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the Job Business Class
        /// </summary>
        private JobModel _jobModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CustomerInfo View Interface</param>
        public CustomerInfoPresenter(ICustomerInfoView view)
        {
            this._view = view;
            this._customerModel = new CustomerModel();
            this._divisionModel = new DivisionModel();
            this._employeeModel = new EmployeeModel();
            this._jobModel = new JobModel();
        }

        public CustomerInfoPresenter(ICustomerInfoView view,JobModel jobModel)
        {
            this._view = view;
            this._jobModel = jobModel;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// List all Divisions in the Database and fill the Dropdown
        /// </summary>
        public void ListAllDivision()
        {
            try
            {
                _view.DivisionList = _divisionModel.ListAllDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Division Information. Please try again.", false);
            }
        }

        public void GetDivisionByEmployee()
        {
            try
            {
                _view.DivisionValue = _divisionModel.GetDivisionByEmployee(_view.EmployeeId).ID;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Division Information. Please try again.", false);
            }
        }


        //public virtual void GetCustomerInfoByJobId()
        //{
        //    try
        //    {
        //        if (_view.JobId.HasValue)
        //        {
        //            //_view.CustomerInfoEntity = _jobModel.GetCustomerInfoByJobId(_view.JobId.Value);
        //            _view.CustomerInfoLoad = _
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(string.Format("An error has ocurred while trying to load the CustomerInfo information!\n{0}\n{1}", ex.Message, ex.StackTrace));
        //        _view.DisplayMessage("An error ocurred while trying to load the CustomerInfo information. Please try again.", false);
        //    }
        //}

        //public void LoadCustomerInfoCloningData()
        //{
        //    try
        //    {
        //        if (_view.CloningId.HasValue)
        //        {
        //            _view.CustomerInfoEntity = _jobModel.GetCustomerInfoByJobId(_view.CloningId.Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(string.Format("An error has ocurred while trying to load the CustomerInfo information!\n{0}\n{1}", ex.Message, ex.StackTrace));
        //        _view.DisplayMessage("An error ocurred while trying to load the CustomerInfo information. Please try again.", false);
        //    }
        //}

        #endregion

    }
}
