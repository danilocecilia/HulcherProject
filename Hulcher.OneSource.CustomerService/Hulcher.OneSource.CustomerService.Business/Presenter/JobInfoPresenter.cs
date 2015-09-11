using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Security;
using System.Web.UI;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// JobInfo Presenter (Block inside Job Record Page)
    /// </summary>
    public class JobInfoPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Permit Info View interface
        /// </summary>
        private IJobInfoView _view;

        private JobModel _jobModel;
        private DivisionModel _divisionModel;
        private CustomerModel _customerModel;
        private EmployeeModel _employeeModel;
        private ResourceAllocationModel _resourceAllocationModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Permit Info View Interface</param>
        public JobInfoPresenter(IJobInfoView view)
        {
            this._view = view;
            this._jobModel = new JobModel();
            this._divisionModel = new DivisionModel();
            this._customerModel = new CustomerModel();
            this._employeeModel = new EmployeeModel();
            _resourceAllocationModel = new ResourceAllocationModel();
        }

        public JobInfoPresenter(IJobInfoView view, JobModel jobModel)
        {
            this._view = view;
            this._jobModel = jobModel;
        }

        #endregion

        #region [ Methods ]

        public void GetReserveByDivision()
        {
            try
            {
                _view.GetReserveByDivision = _resourceAllocationModel.GetReserveByDivision(_view.DivisionId, _view.JobId.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error loading on getting the reserve information.!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading on getting the reserve information.", false);
            }
        }

        /// <summary>
        /// Method for populating the JobStatusList with all Job Status for a specific Job
        /// </summary>
        public void ListAllJobStatus()
        {
            try
            {
                _view.JobStatusList = _jobModel.ListAllJobStatus();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Job Status", false);
            }
        }

        public void ListAllFrequencies()
        {
            try
            {
                _view.FrequencyList = _jobModel.ListAllFrequencies();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading Frequencies", false);
            }
        }

        /// <summary>
        /// Method for populating the JobDivisionsList with all Job Status for a specific Job
        /// </summary>
        public void ListAllJobDivisions()
        {

            try
            {
                _view.JobDivisionEntityList = _jobModel.ListAllJobDivisions();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the JobDivisionsList", false);
            }

        }

        /// <summary>
        /// Method for populating the JobTypes with all Job Status for a specific Job
        /// </summary>
        public void ListAllJobTypes()
        {
            try
            {
                _view.JobTypeList = _jobModel.ListAllJobTypes();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the JobTypes", false);
            }

        }

        /// <summary>
        /// Method for populating the JobCategories with all Job Status for a specific Job
        /// </summary>
        public void ListAllJobCategories()
        {
            try
            {
                _view.JobCategoryList = _jobModel.ListAllJobCategories();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the JobCategories", false);
            }

        }

        /// <summary>
        /// Method for populating the JobPrices with all Job Status for a specific Job
        /// </summary>
        public void ListAllPriceTypes()
        {
            try
            {
                _view.PriceTypeList = _jobModel.ListAllPriceTypes();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Job Price Types", false);
            }
        }

        /// <summary>
        /// Method for populating the Customer Contracts for a specific Customer
        /// </summary>
        public void ListAllCustomerContracts()
        {
            try
            {
                if (null == _view.CustomerFromExternalSource)
                    _view.CustomerContract = null;
                else
                    _view.CustomerContract = _customerModel.ListAllCustomerContracts(_view.CustomerFromExternalSource.ID);
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading Contracts related to the selected company", false);
            }
        }

        public void ListAllApprovingRVP()
        {
            try
            {
                _view.ApprovingRVPList = _employeeModel.ListAllEmployeeRVP().OrderBy(e => e.FullName).ToList<CS_Employee>();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Employee RVP", false);
            }
        }

        /// <summary>
        /// Method for populating the Divisions
        /// </summary>
        public void ListAllDivisions()
        {
            try
            {
                _view.DivisionValueList = _divisionModel.ListAllDivision();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Divisions", false);
            }
        }


        /// <summary>
        /// Method for populating the Lost Job Reasons
        /// </summary>
        public void ListAllLostJobReasons()
        {
            try
            {
                _view.LostJobReasonList = _jobModel.ListAllLostJobReasons();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Lost Job Reasons", false);
            }
        }

        /// <summary>
        /// Method for populating the Competitors List
        /// </summary>
        public void ListAllCompetitors()
        {
            try
            {
                _view.CompetitorList = _jobModel.ListAllCompetitors();
            }
            catch (Exception)
            {
                _view.DisplayMessage("There was an error loading the Competitors List", false);
            }
        }

        public void AddDivision(CS_Division sourceDivision)
        {
            try
            {
                if (sourceDivision == null)
                    _view.JobDivisionEntity.CS_Division = _view.DivisionValue;
                else
                    _view.JobDivisionEntity.CS_Division = sourceDivision;
                if (null != _view.JobDivisionEntity)
                {
                    if (!ContainsDivisionInList(_view.JobDivisionEntity.CS_Division.ID) && _view.JobDivisionEntity.CS_Division.ID != 0)
                    {
                        _view.JobDivisionEntity.Active = true;
                        _view.JobDivisionEntity.CreatedBy = _view.CreatedBy;
                        _view.JobDivisionEntity.CreationDate = DateTime.Now;
                        _view.JobDivisionEntity.ModifiedBy = _view.CreatedBy;
                        _view.JobDivisionEntity.ModificationDate = DateTime.Now;
                        _view.JobDivisionEntityList.Add(_view.JobDivisionEntity);
                        _view.ListJobDivision();
                    }
                    else
                        _view.DisplayMessage("The item already exists in the list or must be selected!", false);
                }
                else
                    throw new Exception("The object containing the Division Information is null!");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to add an item on the list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to add an item on the list.", false);
            }
        }

        public void AddDivisionFromCustomerInfo(CS_Division division)
        {
            if (division != null && !(division.ID <= 0))
            {
                if (!ContainsDivisionInList(division.ID))
                {
                    CS_JobDivision jobDivision = new CS_JobDivision();
                    CS_JobDivision removeAlreadySelectedDivision = null;
                    jobDivision.CS_Division = division;

                    foreach (var item in _view.JobDivisionEntityList)
                    {
                        if (item.IsFromCustomerInfo)
                            removeAlreadySelectedDivision = item;
                        item.PrimaryDivision = false;
                    }

                    if (removeAlreadySelectedDivision != null)
                    {
                        _view.JobDivisionEntityList.Remove(removeAlreadySelectedDivision);
                    }

                    jobDivision.IsFromCustomerInfo = true;
                    jobDivision.DivisionID = jobDivision.CS_Division.ID;
                    jobDivision.Active = true;
                    jobDivision.CreatedBy = _view.CreatedBy;
                    jobDivision.CreationDate = DateTime.Now;
                    jobDivision.ModifiedBy = _view.CreatedBy;
                    jobDivision.ModificationDate = DateTime.Now;
                    jobDivision.PrimaryDivision = true;
                    jobDivision.IsFromCustomerInfo = true;
                    _view.JobDivisionEntityList.Add(jobDivision);
                    _view.ListJobDivision();
                }
            }
        }

        private bool ContainsDivisionInList(int id)
        {
            bool contains = false;
            foreach (var jobDivisionEntity in _view.JobDivisionEntityList)
            {
                if (jobDivisionEntity.CS_Division != null)
                {
                    if (jobDivisionEntity.CS_Division.ID == id)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }

        public void RemoveDivision()
        {
            try
            {
                if (_view.JobDivisionEntityList.Count >= _view.DivisionRemoveIndex)
                {
                    _view.JobDivisionEntityList.RemoveAt(_view.DivisionRemoveIndex);
                    _view.ListJobDivision();
                }
                else
                    throw new Exception("The Item Index informed does not exist on the List");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to remove an item from the list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to remove an item from the list.", false);
            }
        }

        public void LoadJobInfo()
        {
            try
            {
                if (_view.JobId.HasValue)
                {
                    // _view.JobInfoEntity = _view.JobInfoLoad;                    
                    //_view.SpecialPricingEntity = _view.SpecialPricingLoad;
                    //_view.PresetInfoEntity = _view.PresetInfoLoad;
                    // _view.LostJobEntity = _view.LostJobInfoLoad;
                    _view.SpecialPricingEntity = _jobModel.GetSpecialPricing(_view.JobId.Value);
                    _view.PresetInfoEntity = _jobModel.GetPresetInfo(_view.JobId.Value);
                    _view.LostJobEntity = _jobModel.GetLostJob(_view.JobId.Value);
                    _view.JobDivisionEntityList = _jobModel.ListAllDivisionsByJob(_view.JobId.Value);// _view.JobDivisionListLoad;
                    _view.ListJobDivision();
                    _view.CheckJobStatus();

                    if (null != _view.CustomerContractListLoad)
                        _view.CustomerContract = _customerModel.ListAllCustomerContracts(_view.CustomerId);                                        
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has occurred while trying to load Job Info section!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has occurred while trying to load Job Info Section.", false);
            }
        }

        public void GetCustomerSpecificInfoFromCustomer()
        {
            CS_Customer customer = _customerModel.GetCustomerByJobId(_view.JobId.Value);

            if (null != customer.Xml)
            {
                _view.CustomerSpecificInfoXML = customer.Xml;
                GetFieldsFromXml();
            }
        }

        public void GetFieldsFromXml()
        {
            _view.CustomerSpecificInfoControls = DynamicFieldsParser.CreateFieldFromXml(_view.CustomerSpecificInfoXML, new Dictionary<string, object>());
        }

        public string GetXmlFromDynamicControls()
        {
            return DynamicFieldsParser.CreateXmlFromControls(_view.CustomerSpecificInfoControls, _view.CustomerSpecificInfoXML);
        }

        /// <summary>
        /// Set values for the Job Category and Job Type fields based on the selected Job Action
        /// </summary>
        public void SetFieldsFromJobAction()
        {
            try
            {
                CS_JobCategory jobCategory = _jobModel.GetJobCategoryByJobAction(_view.JobActionValue);
                CS_JobType jobType = _jobModel.GetJobTypeByJobAction(_view.JobActionValue);

                if (null != jobCategory && null != jobType)
                {
                    _view.JobCategoryValue = jobCategory.ID;
                    _view.JobTypeValue = jobType.ID;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has occurred while trying to load Job Category and Job Type!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has occurred while trying to load Job Category and Job Type Section.", false);
            }
        }

        public void GetResourcesByDivision()
        {
            try
            {
                _view.GetResourcesByDivision = _resourceAllocationModel.GetResourcesByDivision(_view.DivisionId,
                                                                                               _view.JobId.Value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LoadJobInfoCloningData()
        {
            if (this._view.CloningId.HasValue)
            {
                CS_JobInfo jobInfoFiltered = _jobModel.GetJobInfoWithAppend(this._view.CloningId.Value);
                if (jobInfoFiltered != null)
                {
                    if (jobInfoFiltered.CS_Job_JobStatus != null)
                    {
                        //According to requirements, when cloning the job status must be preset by default                        
                        //jobInfoFiltered.CS_Job_JobStatus.Clear();                        
                        jobInfoFiltered.CS_Job_JobStatus.Add(new CS_Job_JobStatus() { Active = true, ModificationDate = DateTime.Now, CreationDate = DateTime.Now, JobID = _view.CloningId.Value, JobStatusId = (int)Globals.JobRecord.JobStatus.Preset, CS_JobStatus = _jobModel.GetJobStatus((int)Globals.JobRecord.JobStatus.Preset) });
                        // jobInfoFiltered.CS_JobStatus = _jobModel.GetJobStatus(2);
                    }
                    jobInfoFiltered.InitialCallDate = DateTime.Now;
                    jobInfoFiltered.InitialCallTime = DateTime.Now.TimeOfDay;
                    jobInfoFiltered.CustomerSpecificInfo = string.Empty;
                    this._view.JobInfoEntity = jobInfoFiltered;
                }
            }
        }

        public void LoadSpecialPricingInfoCloning()
        {
            try
            {
                if (_view.CloningId.HasValue)
                    _view.SpecialPricingEntity = _jobModel.GetSpecialPricing(_view.CloningId.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Special Pricing information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Special Pricing information. Please try again.", false);
            }
        }

        public void LoadJobDivisionCloning()
        {
            try
            {
                if (_view.CloningId.HasValue)
                {
                    _view.JobDivisionEntityList = FilteredJobDivisionEntityForCloning(_view.CloningId.Value);
                    _view.ListJobDivision();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Special Pricing information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Special Pricing information. Please try again.", false);
            }
        }

        private IList<CS_JobDivision> FilteredJobDivisionEntityForCloning(int cloningId)
        {
            IList<CS_JobDivision> filteredJobDivisionList = new List<CS_JobDivision>();
            foreach (var item in _jobModel.ListJobDivision(cloningId))
            {
                if (item.PrimaryDivision)
                {
                    filteredJobDivisionList.Add(item);
                    break;
                }
            }
            return filteredJobDivisionList;
        }

        #endregion
    }
}
