using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// Essential Job Data ViewModel Class
    /// </summary>
    public class EssentialJobDataViewModel
    {
        #region [ Attributes ]

        private IEssentialJobDataView _view;
        private JobModel _jobModel;
        private LocationModel _locationModel;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Essential Job Data View Interface</param>
        public EssentialJobDataViewModel(IEssentialJobDataView view)
        {
            _view = view;
            _jobModel = new JobModel();
            _locationModel = new LocationModel();
        }
        #endregion

        #region [ Methods ]

        public void SaveJobData()
        {
            if ((_view.InitialCallDate.Date + _view.InitialCallTime) > DateTime.Now)
            {
                _view.DisplayMessage("Initial Call Date can not be greater than today.", false);
                _view.SavedSuccessfuly = false;
                return;
            }

            // CS_Job
            CS_Job csJob = new CS_Job();

            csJob.CreatedBy = _view.Username;
            csJob.CreationDate = DateTime.Now;
            csJob.ModifiedBy = _view.Username;
            csJob.ModificationDate = DateTime.Now;
            csJob.Active = true;
            csJob.EmergencyResponse = _view.IsEmergencyResponse;
            
            _jobModel.NewJob = csJob;

            // Validations for Hulcher Contact and Division
            //int? calledInByContactId = null;
            //if (_view.PrimaryContactId != null && _view.HulcherContactId != null)
            //{
            //    //calledInByContactId = _view.PrimaryContactId;
            //    _view.PrimaryContactId = null;
            //}
            int? hulcherDivision = null;
            if (_view.HulcherContactId.HasValue)
                hulcherDivision = _view.PrimaryDivisionId;

            // CS_CustomerInfo
            CS_CustomerInfo customerInfo = new CS_CustomerInfo()
            {
                Active = true,
                InitialCustomerContactId = _view.PrimaryContactId,
                CustomerId = _view.CustomerId,
                PocEmployeeId = _view.HulcherContactId,
                DivisionId = hulcherDivision,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now
            };
            _jobModel.NewCustomer = customerInfo;

            // CS_JobDivision
            IList<CS_JobDivision> jobDivisionList = new List<CS_JobDivision>();
            jobDivisionList.Add(new CS_JobDivision()
            {
                Active = true,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                DivisionID = _view.PrimaryDivisionId,
                IsFromCustomerInfo = hulcherDivision.HasValue,
                ModificationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                PrimaryDivision = true
            });
            _jobModel.NewJobDivision = jobDivisionList;

            // CS_JobInfo
            CS_JobCategory jobCategory = _jobModel.GetJobCategoryByJobAction(_view.JobActionId);
            CS_JobType jobType = _jobModel.GetJobTypeByJobAction(_view.JobActionId);
            DateTime? startDate = null;
            DateTime? closedDate = null;
            if (_view.JobStatusId.Equals((int)Globals.JobRecord.JobStatus.Active))
                startDate = DateTime.Now;
            else if (_view.JobStatusId.Equals((int)Globals.JobRecord.JobStatus.Closed))
                closedDate = DateTime.Now;

            CS_JobInfo jobInfo = new CS_JobInfo()
            {
                Active = true,
                InitialCallDate = _view.InitialCallDate,
                InitialCallTime = _view.InitialCallTime,
                //JobStatusID = _view.JobStatusId,
                PriceTypeID = _view.PriceTypeId,
                JobActionID = _view.JobActionId,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now,
                JobCategoryID = jobCategory.ID,
                JobTypeID = jobType.ID,
            };
            _jobModel.NewJobInfo = jobInfo;

            CS_Job_JobStatus jobStatus = new CS_Job_JobStatus()
            {
                Active = true,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now,
                JobStatusId = _view.JobStatusId,
                JobStartDate = startDate,
                JobCloseDate = closedDate
            };

            _jobModel.NewJobStatusHistory = jobStatus;
            _jobModel.JobStatusID = _view.JobStatusId;

            if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Active)
                _jobModel.NewJob.BillingStatus = (int)Globals.JobRecord.BillingStatus.Working;
            else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Cancelled || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Lost)
                _jobModel.NewJob.BillingStatus = (int)Globals.JobRecord.BillingStatus.Done;
            else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Potential || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                _jobModel.NewJob.BillingStatus = (int)Globals.JobRecord.BillingStatus.Created;

            // CS_LocationInfo
            int countryId = _locationModel.GetCountryByStateId(_view.StateId);
            CS_LocationInfo locationInfo = new CS_LocationInfo()
            {
                Active = true,
                CountryID = countryId,
                StateID = _view.StateId,
                CityID = _view.CityId,
                ZipCodeId = _view.ZipCode,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now
            };
            _jobModel.NewLocationInfo = locationInfo;

            // CS_ScopeOfWork
            IList<CS_ScopeOfWork> lstScopeOfWork = new List<CS_ScopeOfWork>();
            lstScopeOfWork.Add(new CS_ScopeOfWork()
            {
                Active = true,
                ScopeOfWork = _view.ScopeOfWork,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now
            });
            _jobModel.NewScopeOfWork = lstScopeOfWork;

            // CS_JobDescription
            CS_JobDescription jobDescription = new CS_JobDescription()
            {
                Active = true,
                CreatedBy = _view.Username,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.Username,
                ModificationDate = DateTime.Now
            };
            _jobModel.NewJobDescription = jobDescription;
            _jobModel.SaveJobData(false, null, true);
            _view.JobId = _jobModel.NewJob.ID;

            _view.SavedSuccessfuly = true;
        }


        #endregion

    }
}

