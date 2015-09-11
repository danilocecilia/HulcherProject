using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class JobRecordPrintPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Job Record View Interface
        /// </summary>
        private IJobRecordPrintView _view;

        /// <summary>
        /// Instance of the Job Record Model
        /// </summary>
        private JobModel _model;

        /// <summary>
        /// Instance of the Customer Model
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the Employee Model
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the Division Model
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of the CallLog Model
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Job Record View Interface</param>
        public JobRecordPrintPresenter(IJobRecordPrintView view)
        {
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Updates Title information based on Job Id
        /// </summary>
        public void UpdateTitle()
        {
            try
            {
                if (_view.JobId.HasValue)
                {
                    using (_model = new JobModel())
                    {
                        CS_Job job = _model.GetJobById(_view.JobId.Value);
                        if (null != job && null != job.CS_JobInfo)
                        {
                            string title = string.Format("{0} - Job Record", job.PrefixedNumber);
                            _view.PageTitle = title;
                        }
                    }
                }
                else
                    _view.PageTitle = "New Job Record";
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to update Title information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to update Title information.", false);
            }
        }

        public void GetJob()
        {
                try
                {
                    if (_view.JobId.HasValue)
                    {
                        CS_CustomerInfo customerInfo = null;

                        using (_model = new JobModel())
                        {
                            _view.Job = _model.GetJobById(_view.JobId.Value);
                        }

                        using (_model = new JobModel())
                        {
                            _view.SpecialPricingEntity = _model.GetSpecialPricing(_view.JobId.Value);
                            _view.PresetInfoEntity = _model.GetPresetInfo(_view.JobId.Value);
                            _view.LostJobEntity = _model.GetLostJob(_view.JobId.Value);
                            _view.JobDivisionEntityList = _model.ListJobDivision(_view.JobId.Value);
                            _view.JobDescriptionEntity = _model.GetJobDescriptionByJobId(_view.JobId.Value);

                            if (null != _view.Job.CS_JobInfo && !string.IsNullOrEmpty(_view.Job.CS_JobInfo.CustomerSpecificInfo))
                                _view.CustomerSpecificFields = CustomerSpecificInfo.DeserializeObject(_view.Job.CS_JobInfo.CustomerSpecificInfo);

                            customerInfo = _model.GetCustomerInfoByJobId(_view.JobId.Value);
                        }

                        using (_customerModel = new CustomerModel())
                        {
                            if (null != customerInfo)
                                _view.CustomerContract = _customerModel.ListAllCustomerContracts(customerInfo.CustomerId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has occurred while trying to load Job Info section!\n{0}\n{1}", ex.Message, ex.StackTrace));
                    _view.DisplayMessage("An error has occurred while trying to load Job Info Section.", false);
                }
        }

        public void GetContactName()
        {
            string name = "--";

            if (_view.Identifier.HasValue)
            {
                using (_customerModel = new CustomerModel())
                {
                    name = _customerModel.GetContactById(_view.Identifier.Value).FullContactInformation;
                }
            }

            _view.ParamName = name;
        }

        public void GetEmployeeName()
        {
            string name = "--";

            if (_view.Identifier.HasValue)
            {
                using (_employeeModel = new EmployeeModel())
                {
                    name = _employeeModel.GetEmployee(_view.Identifier.Value).FullName;
                }
            }

            _view.ParamName = name;
        }

        public void GetDivisionName()
        {
            string name = "--";

            if (_view.Identifier.HasValue)
            {
                using (_divisionModel = new DivisionModel())
                {
                    name = _divisionModel.GetDivision(_view.Identifier.Value).Name;
                }
            }

            _view.ParamName = name;
        }
        
        public void BindEquipmentRequestedGrid()
        {
            using (_model = new JobModel())
            {
                if (_view.JobId.HasValue)
                    _view.EquipmentRequestDataSource = _model.GetEquipmentRequestedByJob(_view.JobId.Value);
            }
        }

        public void BindPermitInfoGrid()
        {
            using (_model = new JobModel())
            {
                if (_view.JobId.HasValue)
                    _view.PermitInfoGridDataSource = _model.GetPermitInfoByJob(_view.JobId.Value);
            }
        }

        public void BindPhotoReportGrid()
        {
            using (_model = new JobModel())
            {
                if (_view.JobId.HasValue)
                    _view.PhotoReporGridDataSource = _model.GetPhotoReportByJobId(_view.JobId.Value);
            }
        }

        public void BindJobCallLog()
        {
            using (_callLogModel = new CallLogModel())
            {
                if (_view.JobId.HasValue)
                    _view.JobCallLogGridDataSource = _callLogModel.ListAllJobCallLogs(_view.JobId.Value);
            }
        }

        public void BindScopeOfWork()
        {
            using (_model = new JobModel())
            {
                if (_view.JobId.HasValue)
                    _view.ScopeOfWorkGridDataSource = _model.GetScopeOfWorkByJobId(_view.JobId.Value);
            }
        }

        #endregion
    }
}
