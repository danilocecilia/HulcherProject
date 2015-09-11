using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class JobRecordPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Job Record View Interface
        /// </summary>
        private IJobRecordView _view;

        /// <summary>
        /// Instance of the Job Record Model
        /// </summary>
        private JobModel _model;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Job Record View Interface</param>
        public JobRecordPresenter(IJobRecordView view)
        {
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        public void LoadJobEntity()
        {
            try
            {
                if (_view.JobId.HasValue)
                {
                    _model = new JobModel();
                    _view.JobLoad = _model.GetJobData(_view.JobId.Value);
                    _view.RequestedEquipmentTypeList = _model.GetEquipmentRequestedVOByJob(_view.JobId.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Record!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to load the Job Record.", false);
            }
        }

        /// <summary>
        /// Saves a Job Record
        /// </summary>
        public void SaveJobData()
        {
            try
            {
                if ((_view.JobInfo.InitialCallDate.Date + _view.JobInfo.InitialCallTime) > DateTime.Now)
                {
                    _view.DisplayMessage("Job Info: Initial Call Date can not be greater than today.", false);
                    return;
                }

                if (_view.JobId.HasValue)
                {
                    int jobId = _view.JobId.Value;
                    string username = _view.Username;

                    CS_Job job = _view.Job;
                    CS_CustomerInfo customerInfo = _view.CustomerInfo;
                    IList<CS_JobDivision> divisionList = _view.DivisionList;
                    CS_PresetInfo presetInfo = _view.PresetInfo;
                    CS_LostJobInfo lostJobInfo = _view.LostJobInfo;
                    CS_SpecialPricing specialPricing = _view.SpecialPricing;
                    CS_JobInfo jobInfo = _view.JobInfo;
                    CS_LocationInfo locationInfo = _view.LocationInfo;
                    CS_JobDescription jobDescription = _view.JobDescription;
                    IList<CS_ScopeOfWork> scopeOfWorkList = _view.ScopeOfWorkList;
                    IList<CS_JobPermit> permitInfoList = _view.PermitInfoList;
                    IList<CS_JobPhotoReport> photoReportList = _view.PhotoReportList;
                    IList<LocalEquipmentTypeVO> requestedEquipmentList = _view.RequestedEquipmentTypeList;

                    job.ID = jobId;
                    job.ModifiedBy = username;
                    job.ModificationDate = DateTime.Now;

                    customerInfo.JobId = jobId;
                    customerInfo.ModifiedBy = username;
                    customerInfo.ModificationDate = DateTime.Now;

                    foreach (CS_JobDivision jobDivision in divisionList)
                    {
                        jobDivision.JobID = jobId;
                        jobDivision.ModifiedBy = username;
                        jobDivision.ModificationDate = DateTime.Now;
                        if (null != jobDivision.CS_Division)
                        {
                            jobDivision.DivisionID = jobDivision.CS_Division.ID;
                            jobDivision.CS_Division = null;
                        }
                    }

                    if (null != presetInfo)
                    {
                        presetInfo.JobId = jobId;
                        presetInfo.ModifiedBy = username;
                        presetInfo.ModificationDate = DateTime.Now;
                    }

                    if (null != lostJobInfo)
                    {
                        lostJobInfo.JobId = jobId;
                        lostJobInfo.ModifiedBy = username;
                        lostJobInfo.ModificationDate = DateTime.Now;
                    }

                    if (null != specialPricing)
                    {
                        specialPricing.JobId = jobId;
                        specialPricing.ModifiedBy = username;
                        specialPricing.ModificationDate = DateTime.Now;
                    }

                    jobInfo.JobID = jobId;
                    jobInfo.ModifiedBy = username;
                    jobInfo.ModificationDate = DateTime.Now;

                    locationInfo.JobID = jobId;
                    locationInfo.ModifiedBy = username;
                    locationInfo.ModificationDate = DateTime.Now;

                    jobDescription.JobId = jobId;
                    jobDescription.ModifiedBy = username;
                    jobDescription.ModificationDate = DateTime.Now;

                    foreach (CS_ScopeOfWork scopeOfWork in scopeOfWorkList)
                    {
                        scopeOfWork.JobId = jobId;
                    }

                    foreach (CS_JobPermit jobPermit in permitInfoList)
                    {
                        jobPermit.JobID = jobId;
                    }

                    foreach (CS_JobPhotoReport photoReport in photoReportList)
                    {
                        photoReport.JobID = jobId;
                    }

                    CS_Job_JobStatus jobStatusHistory = new CS_Job_JobStatus()
                    {
                        Active = true,
                        CreationDate = DateTime.Now,
                        CreatedBy = _view.Username,
                        ModificationDate = DateTime.Now,
                        ModifiedBy = _view.Username,
                        JobID = _view.JobId.Value,
                        JobStatusId = _view.JobStatusId,
                        JobStartDate = _view.JobStartDate,
                        JobCloseDate = _view.JobCloseDate
                    };

                    if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Active)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Working;
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Cancelled || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Lost)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Done;
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Potential || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Created;

                    this._model = new JobModel()
                    {
                        NewJob = job,
                        NewCustomer = customerInfo,
                        NewJobDivision = divisionList,
                        NewPresetInfo = presetInfo,
                        NewLostJobInfo = lostJobInfo,
                        NewSpecialPricing = specialPricing,
                        NewJobInfo = jobInfo,
                        NewLocationInfo = locationInfo,
                        NewJobDescription = jobDescription,
                        NewScopeOfWork = scopeOfWorkList,
                        NewPermit = permitInfoList,
                        NewPhotoReport = photoReportList,
                        JobStatusID = _view.JobStatusId,
                        NewJobStatusHistory = jobStatusHistory,
                        NewRequestedEquipment = requestedEquipmentList
                    };

                    IDictionary<string, object> output = _model.UpdateJobData(_view.CreateInitialAdvise, false);

                    if (output.ContainsKey("ResourceConversion"))
                        _view.ResourceConversion = bool.Parse(output["ResourceConversion"].ToString());

                    if (output.ContainsKey("HasResources"))
                    {
                        bool hasResources = bool.Parse(output["HasResources"].ToString());

                        if (hasResources)
                            _view.DisplayMessage(output["Message"].ToString(), false);

                        _view.HasResources = hasResources;
                    }

                    if (output.ContainsKey("HasBillToContact"))
                    {
                        bool hasBillToContact = bool.Parse(output["HasBillToContact"].ToString());

                        if (!hasBillToContact)
                            _view.DisplayMessage(output["Message"].ToString(), false);
                    }

                    if (output.ContainsKey("HasAssignedResources"))
                    {
                        bool hasAssignedResources = bool.Parse(output["HasAssignedResources"].ToString());

                        if (hasAssignedResources)
                            _view.DisplayMessage(output["Message"].ToString(), false);

                        _view.HasResources = hasAssignedResources;
                    }

                    if (output.ContainsKey("OperationNotAllowed"))
                    {
                        bool operationNotAllowed = bool.Parse(output["OperationNotAllowed"].ToString());

                        if (operationNotAllowed)
                            _view.DisplayMessage(output["Message"].ToString(), false);

                        return;
                    }

                    if (output.ContainsKey("HasTemporaryCustomer"))
                    {
                        bool hasTemporaryCustomer = bool.Parse(output["HasTemporaryCustomer"].ToString());

                        if (hasTemporaryCustomer)
                            _view.DisplayMessage(output["Message"].ToString(),false);
                    }

                    if (_view.CreateInitialAdvise)
                    {
                        _view.SetCloseEnabled();
                        _view.CreateInitialAdvise = false;
                    }
                }
                else
                {
                    CS_Job job = _view.Job;

                    if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Active)
                    {
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Working;
                        job.IsBilling = true;
                    }
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Cancelled || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Lost)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Done;
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Potential || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Created;

                    this._model = new JobModel()
                    {
                        NewJob = job,
                        NewCustomer = _view.CustomerInfo,
                        NewJobDivision = _view.DivisionList,
                        NewPresetInfo = _view.PresetInfo,
                        NewLostJobInfo = _view.LostJobInfo,
                        NewSpecialPricing = _view.SpecialPricing,
                        NewJobInfo = _view.JobInfo,
                        NewLocationInfo = _view.LocationInfo,
                        NewJobDescription = _view.JobDescription,
                        NewScopeOfWork = _view.ScopeOfWorkList,
                        NewPermit = _view.PermitInfoList,
                        NewPhotoReport = _view.PhotoReportList,
                        NewRequestedEquipment = _view.RequestedEquipmentTypeList,
                        JobStatusID = _view.JobStatusId,
                        NewJobStatusHistory = new CS_Job_JobStatus()
                        {
                            Active = true,
                            CreationDate = DateTime.Now,
                            CreatedBy = _view.Username,
                            ModificationDate = DateTime.Now,
                            ModifiedBy = _view.Username,
                            JobStatusId = _view.JobStatusId,
                            JobStartDate = _view.JobStartDate,
                            JobCloseDate = _view.JobCloseDate
                        }
                    };

                    _model.SaveJobData(true, _view.CloningId, false);
                }

                bool saveInitialAdivise = _view.SaveInitialAdviseEmail;
                _view.CreateInitialAdvise = false;
                _view.JobId = _model.NewJob.ID;
                LoadJobEntity();
                UpdateTitle();
                _view.DisplayMessage("Job Record saved successfully", false);
                _view.OpenEmailInitialAdvise = saveInitialAdivise;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to save the Job Record!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to save the Job Record.", false);
            }
        }

        /// <summary>
        /// Update the job record when user clicks on save and close
        /// </summary>
        public void UpdateJobData()
        {
            try
            {
                if ((_view.JobInfo.InitialCallDate.Date + _view.JobInfo.InitialCallTime) > DateTime.Now)
                {
                    _view.DisplayMessage("Job Info: Initial Call Date can not be greater than today.", false);
                    return;
                }

                if (_view.JobId.HasValue)
                {
                    int jobId = _view.JobId.Value;
                    string username = _view.Username;

                    CS_Job job = _view.Job;
                    CS_CustomerInfo customerInfo = _view.CustomerInfo;
                    IList<CS_JobDivision> divisionList = _view.DivisionList;
                    CS_PresetInfo presetInfo = _view.PresetInfo;
                    CS_LostJobInfo lostJobInfo = _view.LostJobInfo;
                    CS_SpecialPricing specialPricing = _view.SpecialPricing;
                    CS_JobInfo jobInfo = _view.JobInfo;
                    CS_LocationInfo locationInfo = _view.LocationInfo;
                    CS_JobDescription jobDescription = _view.JobDescription;
                    IList<CS_ScopeOfWork> scopeOfWorkList = _view.ScopeOfWorkList;
                    IList<CS_JobPermit> permitInfoList = _view.PermitInfoList;
                    IList<CS_JobPhotoReport> photoReportList = _view.PhotoReportList;
                    IList<LocalEquipmentTypeVO> requestedEquipmentList = _view.RequestedEquipmentTypeList;

                    job.ID = jobId;
                    job.ModifiedBy = username;
                    job.ModificationDate = DateTime.Now;

                    customerInfo.JobId = jobId;
                    customerInfo.ModifiedBy = username;
                    customerInfo.ModificationDate = DateTime.Now;

                    foreach (CS_JobDivision jobDivision in divisionList)
                    {
                        jobDivision.JobID = jobId;
                        jobDivision.ModifiedBy = username;
                        jobDivision.ModificationDate = DateTime.Now;
                        if (null != jobDivision.CS_Division)
                        {
                            jobDivision.DivisionID = jobDivision.CS_Division.ID;
                            jobDivision.CS_Division = null;
                        }
                    }

                    if (null != presetInfo)
                    {
                        presetInfo.JobId = jobId;
                        presetInfo.ModifiedBy = username;
                        presetInfo.ModificationDate = DateTime.Now;
                    }

                    if (null != lostJobInfo)
                    {
                        lostJobInfo.JobId = jobId;
                        lostJobInfo.ModifiedBy = username;
                        lostJobInfo.ModificationDate = DateTime.Now;
                    }

                    if (null != specialPricing)
                    {
                        specialPricing.JobId = jobId;
                        specialPricing.ModifiedBy = username;
                        specialPricing.ModificationDate = DateTime.Now;
                    }

                    jobInfo.JobID = jobId;
                    jobInfo.ModifiedBy = username;
                    jobInfo.ModificationDate = DateTime.Now;

                    locationInfo.JobID = jobId;
                    locationInfo.ModifiedBy = username;
                    locationInfo.ModificationDate = DateTime.Now;

                    jobDescription.JobId = jobId;
                    jobDescription.ModifiedBy = username;
                    jobDescription.ModificationDate = DateTime.Now;

                    foreach (CS_ScopeOfWork scopeOfWork in scopeOfWorkList)
                    {
                        scopeOfWork.JobId = jobId;
                    }

                    foreach (CS_JobPermit jobPermit in permitInfoList)
                    {
                        jobPermit.JobID = jobId;
                    }

                    foreach (CS_JobPhotoReport photoReport in photoReportList)
                    {
                        photoReport.JobID = jobId;
                    }

                    CS_Job_JobStatus jobStatusHistory = new CS_Job_JobStatus()
                    {
                        Active = true,
                        CreationDate = DateTime.Now,
                        CreatedBy = _view.Username,
                        ModificationDate = DateTime.Now,
                        ModifiedBy = _view.Username,
                        JobID = _view.JobId.Value,
                        JobStatusId = _view.JobStatusId,
                        JobStartDate = _view.JobStartDate,
                        JobCloseDate = _view.JobCloseDate
                    };

                    if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Active)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Working;
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Cancelled || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Lost)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Done;
                    else if (_view.JobStatusId == (int)Globals.JobRecord.JobStatus.Potential || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || _view.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                        job.BillingStatus = (int)Globals.JobRecord.BillingStatus.Created;

                    this._model = new JobModel()
                    {
                        NewJob = job,
                        NewCustomer = customerInfo,
                        NewJobDivision = divisionList,
                        NewPresetInfo = presetInfo,
                        NewLostJobInfo = lostJobInfo,
                        NewSpecialPricing = specialPricing,
                        NewJobInfo = jobInfo,
                        NewLocationInfo = locationInfo,
                        NewJobDescription = jobDescription,
                        NewScopeOfWork = scopeOfWorkList,
                        NewPermit = permitInfoList,
                        NewPhotoReport = photoReportList,
                        JobStatusID = _view.JobStatusId,
                        NewJobStatusHistory = jobStatusHistory,
                        NewRequestedEquipment = requestedEquipmentList
                    };

                    IDictionary<string, object> output = _model.UpdateJobData(_view.CreateInitialAdvise, false);

                    if (output.ContainsKey("ResourceConversion"))
                        _view.ResourceConversion = bool.Parse(output["ResourceConversion"].ToString());

                    if (output.ContainsKey("HasResources"))
                    {
                        bool hasResources = bool.Parse(output["HasResources"].ToString());

                        if (hasResources)
                            _view.DisplayMessage(output["Message"].ToString(), _view.SaveAndClose);

                        _view.HasResources = hasResources;
                    }

                    if (output.ContainsKey("HasBillToContact"))
                    {
                        bool hasBillToContact = bool.Parse(output["HasBillToContact"].ToString());

                        if (!hasBillToContact)
                            _view.DisplayMessage(output["Message"].ToString(), false);
                    }

                    if (output.ContainsKey("HasAssignedResources"))
                    {
                        bool hasAssignedResources = bool.Parse(output["HasAssignedResources"].ToString());

                        if (hasAssignedResources)
                            _view.DisplayMessage(output["Message"].ToString(), _view.SaveAndClose);

                        _view.HasResources = hasAssignedResources;
                    }

                    if (output.ContainsKey("OperationNotAllowed"))
                    {
                        bool operationNotAllowed = bool.Parse(output["OperationNotAllowed"].ToString());

                        if (operationNotAllowed)
                            _view.DisplayMessage(output["Message"].ToString(), false);

                        return;
                    }

                    if (output.ContainsKey("HasTemporaryCustomer"))
                    {
                        bool hasTemporaryCustomer = bool.Parse(output["HasTemporaryCustomer"].ToString());

                        if (hasTemporaryCustomer)
                            _view.DisplayMessage(output["Message"].ToString(), _view.SaveAndClose);
                    }

                    if (_view.CreateInitialAdvise)
                    {
                        _view.SetCloseEnabled();
                        _view.CreateInitialAdvise = false;
                    }
                }


                bool saveInitialAdivise = _view.SaveInitialAdviseEmail;
                _view.CreateInitialAdvise = false;
                _view.JobId = _model.NewJob.ID;
                LoadJobEntity();
                UpdateTitle();
                _view.DisplayMessage("Job Record saved successfully", _view.SaveAndClose);
                //_view.OpenEmailInitialAdvise = saveInitialAdivise;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to save the Job Record!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to save the Job Record.", false);
            }
        }

        public void CheckReadOnlyAccess()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.Username, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.AlterJob });

                if (!azOP[0].Result)
                    _view.ReadyOnlyJobRecords = true;
                else
                    _view.ReadyOnlyJobRecords = false;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to verify user permissions to update Job Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to verify user permissions to update Job Information.", false);
            }
        }

        /// <summary>
        /// Updates Title information based on Job Id
        /// </summary>
        public void UpdateTitle()
        {
            try
            {
                if (_view.JobId.HasValue)
                {
                    _view.PageTitle = string.Format("{0} - Job Record", _view.JobLoad.JobPrefixedNumber);
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

        /// <summary>
        /// Set the emergency response to the control on the page
        /// </summary>
        public void SetEmergencyResponse()
        {
            try
            {
                if (null != _view.JobLoad)
                    _view.IsEmergencyResponse = _view.JobLoad.JobEmergencyResponse;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to get emergency response information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to get emergency response information.", false);
            }

        }

        /// <summary>
        /// Set the JobStartDate and JobCloseDate based on JobId
        /// </summary>
        public void GetJobStartAndCloseDate()
        {
            try
            {

                _view.JobStartDate = _view.JobLoad.JobStartDate;
                _view.JobCloseDate = _view.JobLoad.JobCloseDate;

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load the Job start date or Job close date.\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load the Job start date or Job close date.", false);
            }
        }
        #endregion

        
    }
}
