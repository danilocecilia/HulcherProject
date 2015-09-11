using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class ResourceAllocationModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Reserve
        /// </summary>
        private IRepository<CS_Reserve> _reserveRepository;

        /// <summary>
        /// Repository class for CS_Resource
        /// </summary>
        private IRepository<CS_Resource> _resourceRepository;

        /// <summary>
        /// Repository class for CS_View_Resource_CallLogInfo
        /// </summary>
        private IRepository<CS_View_Resource_CallLogInfo> _resourceCallLogInfoRepository;

        /// <summary>
        /// Repository class for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for Job
        /// </summary>
        private IRepository<CS_Job> _jobRepository;

        /// <summary>
        /// Repository class for Job Divisions
        /// </summary>
        private IRepository<CS_JobDivision> _jobDivisionRepository;

        /// <summary>
        /// Repository class for Reserve List View
        /// </summary>
        private IRepository<CS_View_ReserveList> _reserveListRepository;

        /// <summary>
        /// Repository class for Resource Allocation Details
        /// </summary>
        private IRepository<CS_ResourceAllocationDetails> _resourceAllocationDetailsRepository;

        /// <summary>
        /// CallLog Model class
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// CallCriteria Model class
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        private StringBuilder _addedResourceBuilder;

        private StringBuilder _reservedResourceBuilder;

        private StringBuilder _initialAdviseBuilder;

        #endregion

        #region [ Constructors ]

        public ResourceAllocationModel()
        {
            _unitOfWork = new EFUnitOfWork() { LazyLoadingEnabled = true };

            InstanceObjects();
        }

        public ResourceAllocationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            InstanceObjects();
        }

        private void InstanceObjects()
        {
            _reserveRepository = new EFRepository<CS_Reserve>() { UnitOfWork = _unitOfWork };
            _resourceRepository = new EFRepository<CS_Resource>() { UnitOfWork = _unitOfWork };
            _resourceCallLogInfoRepository = new EFRepository<CS_View_Resource_CallLogInfo>() { UnitOfWork = _unitOfWork };
            _callLogRepository = new EFRepository<CS_CallLog>() { UnitOfWork = _unitOfWork };
            _callLogResourceRepository = new EFRepository<CS_CallLogResource>() { UnitOfWork = _unitOfWork };
            _jobRepository = new EFRepository<CS_Job>() { UnitOfWork = _unitOfWork };
            _jobDivisionRepository = new EFRepository<CS_JobDivision>() { UnitOfWork = _unitOfWork };
            _reserveListRepository = new EFRepository<CS_View_ReserveList>() { UnitOfWork = _unitOfWork };
            _resourceAllocationDetailsRepository = new EFRepository<CS_ResourceAllocationDetails>() { UnitOfWork = _unitOfWork };

            _callLogModel = new CallLogModel(_unitOfWork);
            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
        }

        #endregion

        #region [ Properties ]

        public IList<CS_Reserve> ReserveSaved
        {
            get;
            set;
        }

        public IList<CS_Resource> ResourceSaved
        {
            get;
            set;
        }

        #endregion

        #region [ Methods ]

        #region [ Resources ]

        /// <summary>
        /// Return list resources by division and job
        /// </summary>
        /// <param name="divisionId">division id</param>
        /// <param name="jobId">job id</param>
        /// <returns>list resouces</returns>
        public IList<CS_Resource> GetResourcesByDivision(int divisionId, int jobId)
        {
            return _resourceRepository.ListAll(w => w.Active && w.JobID == jobId && ((w.Type == 2 && w.CS_Employee.DivisionID == divisionId) || (w.Type == 1 && w.CS_Equipment.DivisionID == divisionId)));
        }

        /// <summary>
        /// Return list all added resource
        /// </summary>
        /// <param name="callLogId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<CS_Resource> ListAddedResource(int callLogId, int type)
        {
            return
                _resourceRepository.ListAll(
                    w => w.CS_Equipment.CS_CallLogResource.Any(e => e.JobID == w.JobID && e.Active &&
                                                                    ((e.EquipmentID == w.EquipmentID &&
                                                                     !e.EmployeeID.HasValue) && !e.ContactID.HasValue) &&
                                                                    e.CallLogID == callLogId && e.Type == type) ||
                    w.CS_Employee.CS_CallLogResource1.Any(e => e.JobID == w.JobID && e.Active &&
                                                                    ((e.EmployeeID == w.EmployeeID &&
                                                                        !e.EquipmentID.HasValue) && !e.ContactID.HasValue) &&
                                                                    e.CallLogID == callLogId && e.Type == type));

        }

        public IList<CS_Resource> ListResourcesByJob(int jobId)
        {
            return _resourceRepository.ListAll(e => e.JobID == jobId && e.Active);
        }

        /// <summary>
        /// Get the especified resource
        /// </summary>
        /// <param name="resourceId">Resource Identifier</param>
        /// <returns>Resource entity</returns>
        public CS_Resource GetResource(int resourceId)
        {
            return _resourceRepository.Get(e => e.ID == resourceId);
        }

        /// <summary>
        /// Verify if there is resource associated to the especific job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>true/false</returns>
        public bool HasResource(int jobId)
        {
            IList<CS_Resource> lstResources = _resourceRepository.ListAll(w => w.JobID == jobId && w.Active).ToList();

            if (lstResources.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method to load all the resources based on jobid and resource id
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <param name="resourceTransferId">resource id</param>
        /// <returns>entity resource</returns>
        public CS_Resource GetResourceByJobAndResourceId(int jobId, int resourceTransferId)
        {
            CS_Resource resource = _resourceRepository.Get(w => w.Active
                                                                && w.JobID == jobId
                                                                && w.ID == resourceTransferId,
                                                               "CS_Employee",
                                                               "CS_Equipment");

            if (null != resource)
            {
                return resource;
            }

            return null;
        }
        #endregion

        #region [ Reserve ]

        /// <summary>
        /// Return list of reserve by division and jobid
        /// </summary>
        /// <param name="divisionId">division id</param>
        /// <param name="jobId">job id</param>
        /// <returns>list of cs_reserve</returns>
        public IList<CS_Reserve> GetReserveByDivision(int divisionId, int jobId)
        {
            return _reserveRepository.ListAll(w => w.DivisionID == divisionId && w.JobID == jobId).ToList();
        }

        public IList<CS_Reserve> ListReserveByJob(int jobId)
        {
            return _reserveRepository.ListAll(e => e.JobID == jobId && e.Active);
        }

        public IList<CS_View_ReserveList> GetReserveListByJob(int jobId)
        {
            return _reserveListRepository.ListAll(e => e.Active && e.JobId == jobId);
        }

        /// <summary>
        /// Inactivate all Reserves of a certain Job Id
        /// </summary>
        public IList<CS_Reserve> ClearReservesByJobId(int jobId, string userName)
        {
            IList<CS_Reserve> reserveList = ListReserveByJob(jobId);

            for (int i = 0; i < reserveList.Count; i++)
            {
                reserveList[i].ModifiedBy = userName;
                reserveList[i].ModificationDate = DateTime.Now;
                reserveList[i].Active = false;
            }

            _reserveRepository.UpdateList(reserveList);

            return reserveList;
        }

        #endregion

        #region [ Call Log Resources ]

        /// <summary>
        /// List all filtered resources by job for call entry
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        public IList<CS_View_Resource_CallLogInfo> ListFilteredResourcesCallLogInfoByJob(int jobId, bool showEmployee, bool showEquipment, Globals.CallEntry.ResourceFilterType? filterType, string filterValue)
        {
            IList<CS_View_Resource_CallLogInfo> returnList = new List<CS_View_Resource_CallLogInfo>();

            if (filterType.HasValue && !string.IsNullOrEmpty(filterValue))
            {
                string[] filterValues = filterValue.Split(',');
                for (int i = 0; i < filterValues.Length; i++)
                    filterValues[i] = filterValues[i].Trim();

                switch (filterType.Value)
                {
                    case Globals.CallEntry.ResourceFilterType.DivisionNumber:

                        if (showEmployee && showEquipment)
                            returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && filterValues.Any(v => e.DivisionName.Contains(v)));
                        else if (showEquipment)
                            returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.EquipmentID.HasValue && e.Active.Value && filterValues.Any(v => e.DivisionName.Contains(v)));
                        else if (showEmployee)
                            returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.EmployeeID.HasValue && e.Active.Value && filterValues.Any(v => e.DivisionName.Contains(v)));
                        break;
                    case Globals.CallEntry.ResourceFilterType.Name:
                        returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && e.EmployeeID.HasValue &&
                                (filterValues.Any(v => e.EmployeeName.Contains(v)) || filterValues.Any(v => e.EmployeeFirstName.Contains(v))));
                        break;
                    case Globals.CallEntry.ResourceFilterType.UnitNumber:
                        returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && e.EquipmentID.HasValue &&
                                filterValues.Any(v => e.EquipmentDescription.Contains(v)));
                        break;
                    case Globals.CallEntry.ResourceFilterType.ComboNumber:
                        returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && e.EquipmentID.HasValue &&
                                filterValues.Any(v => e.ComboName.Contains(v)));
                        break;
                }
            }
            else
            {
                if (showEmployee && showEquipment)
                    returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value);
                else if (showEquipment)
                    returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && e.EquipmentID.HasValue);
                else if (showEmployee)
                    returnList = _resourceCallLogInfoRepository.ListAll(e => e.JobID == jobId && e.Active.Value && e.EmployeeID.HasValue);
            }

            return returnList;
        }

        public IList<ListItemVO> ListResourcesAssignedFilterValues(bool showEmployee, bool showEquipment)
        {
            List<ListItemVO> listItems = new List<ListItemVO>();

            listItems.Add(new ListItemVO() { Text = "Division Name", Value = "1" });

            if (showEmployee)
                listItems.Add(new ListItemVO() { Text = "Name", Value = "2" });

            if (showEquipment)
            {
                listItems.Add(new ListItemVO() { Text = "Unit #", Value = "3" });
                listItems.Add(new ListItemVO() { Text = "Combo #", Value = "4" });
            }

            return listItems;
        }

        #endregion

        #region [ Save/Update Resource Allocation ]

        public void SaveOrUpdateResourceAllocation(int jobId, IList<CS_Reserve> reserveList, IList<CS_Resource> resourceList, string username, IList<int> lstDivisions, string notes, bool updateDetails, DateTime callDate, bool IsSubContractor, string SubContractorInfo, string FieldPO)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        ReserveSaved = UpdateReservedResources(jobId, reserveList, username, callDate);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while trying to update the reserved resources!", ex);
                    }

                    try
                    {
                        ResourceSaved = UpdateAddedResources(jobId, resourceList, username, callDate);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while trying to update the added resources!", ex);
                    }
                    try
                    {
                        if (Globals.GeneralLog.ID != jobId)
                        {
                            IList<CS_JobDivision> lstjobDiv = _jobDivisionRepository.ListAll(w => w.JobID == jobId);

                            IList<int> divisionsToAdd = lstDivisions.Where(divisionId => !lstjobDiv.Any(a => a.DivisionID == divisionId && a.Active)).Distinct().ToList();

                            SaveJobDivision(divisionsToAdd, username, jobId);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while trying to save the new Divisions!", ex);
                    }

                    if (updateDetails)
                    {
                        try
                        {
                            SaveResourceAllocationDetails(jobId, notes, username, null, IsSubContractor, SubContractorInfo, FieldPO);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error while trying to save the Notes!", ex);
                        }
                    }

                    //try
                    //{
                    //    if (Globals.GeneralLog.ID != jobId)
                    //    {
                    //        GenerateAutomaticCallEntryInitialAdvise(jobId, username);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw new Exception("There was an error while trying to generate a new Initial Advise Entry!", ex);
                    //}

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IList<CS_Reserve> UpdateReservedResources(int jobId, IList<CS_Reserve> reserveList, string username, DateTime callDate)
        {
            IList<CS_Reserve> updatedReserve = new List<CS_Reserve>();
            IList<CS_Reserve> addedReserve = new List<CS_Reserve>();

            IList<CS_Reserve> reserveSaved = null;

            if (reserveList != null && reserveList.Count > 0)
            {
                reserveSaved = new List<CS_Reserve>();
                foreach (CS_Reserve reserve in reserveList)
                {
                    if (reserve.ID > 0)
                    {
                        reserve.ModificationDate = DateTime.Now;
                        reserve.ModifiedBy = username;
                        updatedReserve.Add(reserve);
                    }
                    else
                    {
                        reserve.JobID = jobId;
                        reserve.CreationDate = DateTime.Now;
                        reserve.CreateBy = username;
                        reserve.ModificationDate = DateTime.Now;
                        reserve.ModifiedBy = username;
                        addedReserve.Add(reserve);
                    }
                }

                addedReserve = _reserveRepository.AddList(addedReserve);
                updatedReserve = _reserveRepository.UpdateList(updatedReserve);

                //Get the just added resources again to make lazy loading to work in the GenerateAutomaticCallEntry method
                IList<CS_Reserve> getShoppingCartList = _reserveRepository.ListAll(r => r.JobID == jobId && r.Active);
                if (null != getShoppingCartList && getShoppingCartList.Count > 0)
                    GenerateAutomaticCallEntryReserve(jobId, username, getShoppingCartList, callDate);

                foreach (CS_Reserve reserve in addedReserve) reserveSaved.Add(reserve);
                foreach (CS_Reserve reserve in updatedReserve) reserveSaved.Add(reserve);
            }


            return reserveSaved;
        }

        private IList<CS_Resource> UpdateAddedResources(int jobId, IList<CS_Resource> resourceList, string username, DateTime callDate)
        {
            IList<CS_Resource> updatedResource = new List<CS_Resource>();
            IList<CS_Resource> addedResource = new List<CS_Resource>();

            IList<CS_Resource> resourceSaved = null;

            if (resourceList != null && resourceList.Count > 0)
            {
                resourceSaved = new List<CS_Resource>();
                foreach (CS_Resource resource in resourceList)
                {
                    if (resource.ID > 0)
                    {
                        resource.ModificationDate = DateTime.Now;
                        resource.ModifiedBy = username;
                        updatedResource.Add(resource);
                    }
                    else
                    {
                        resource.JobID = jobId;
                        resource.CreationDate = DateTime.Now;
                        resource.CreatedBy = username;
                        resource.ModificationDate = DateTime.Now;
                        resource.ModifiedBy = username;
                        addedResource.Add(resource);
                    }
                }

                _resourceRepository.AddList(addedResource);
                _resourceRepository.UpdateList(updatedResource);

                if (null != updatedResource && updatedResource.Count > 0)
                {
                    UpdateStartDatetime(updatedResource);
                    DPIModel dpiModel = new DPIModel();

                    IList<int> equipmentList = updatedResource.Where(e => e.EquipmentID.HasValue).Select(e => e.EquipmentID.Value).ToList();
                    IList<int> employeeList = updatedResource.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList();

                    dpiModel.UpdateDPIToCalculateByResources(jobId, equipmentList, employeeList);
                }

                if (null != addedResource && addedResource.Count > 0)
                    GenerateAutomaticCallEntryAdd(jobId, username, addedResource, callDate);



                foreach (CS_Resource resource in addedResource) resourceSaved.Add(resource);
                foreach (CS_Resource resource in updatedResource) resourceSaved.Add(resource);

                EquipmentModel equipmentModel = new EquipmentModel(_unitOfWork);

                IList<CS_EquipmentPermit> permitList = equipmentModel.ListExpiredPermitByResourceList(resourceSaved.Where(e => e.EquipmentID.HasValue).ToList());

                equipmentModel.SendNotificationForTransportationTeam(permitList);
            }

            return resourceSaved;
        }

        public void UpdateStartDatetime(IList<CS_Resource> resourceList)
        {
            IList<CS_CallLogResource> updateCallLogResourceList = new List<CS_CallLogResource>();

            int callTypeID = (int)Globals.CallEntry.CallType.AddedResource;

            for (int i = 0; i < resourceList.Count; i++)
            {
                int? equipmentID = resourceList[i].EquipmentID;
                int? employeeID = resourceList[i].EmployeeID;
                int jobID = resourceList[i].JobID;

                CS_CallLogResource callLogResource = _callLogResourceRepository.ListAll(e => (e.EquipmentID.HasValue && e.EquipmentID == equipmentID) ||
                                                                                         (e.EmployeeID.HasValue && e.EmployeeID == employeeID) &&
                                                                                         e.JobID == jobID &&
                                                                                         e.CS_CallLog.CallTypeID == callTypeID &&
                                                                                         e.Active).LastOrDefault();

                if (null != callLogResource)
                {
                    CS_CallLogResource updatedCallLogResource = new CS_CallLogResource()
                    {
                        ID = callLogResource.ID,
                        CallLogID = callLogResource.CallLogID,
                        EmployeeID = callLogResource.EmployeeID,
                        EquipmentID = callLogResource.EquipmentID,
                        ContactID = null,
                        JobID = callLogResource.JobID,
                        Type = callLogResource.Type,
                        ActionDate = resourceList[i].StartDateTime,
                        CreatedBy = callLogResource.CreatedBy,
                        CreationDate = callLogResource.CreationDate,
                        ModifiedBy = resourceList[i].ModifiedBy,
                        ModificationDate = resourceList[i].ModificationDate,
                        Active = true,
                        CreationID = callLogResource.CreationID,
                        ModificationID = callLogResource.ModificationID
                    };

                    updateCallLogResourceList.Add(updatedCallLogResource);
                }
            }

            _callLogResourceRepository.UpdateList(updateCallLogResourceList);
        }

        /// <summary>
        /// Gets the Resource Allocation Details based on a Job Identifier
        /// </summary>
        /// <param name="jobID">Job Identifier</param>
        /// <returns>Resource Allocation Details</returns>
        public CS_ResourceAllocationDetails GetResourceAllocationDetails(int jobID)
        {
            return _resourceAllocationDetailsRepository.Get(e => e.JobID == jobID);
        }

        public void SaveResourceAllocationDetails(int jobID, string notes, string username, int? employeeID, bool IsSubContractor, string SubContractorInfo, string FieldPO)
        {
            CS_ResourceAllocationDetails oldDetails = _resourceAllocationDetailsRepository.Get(e => e.JobID == jobID);
            if (null == oldDetails)
            {
                // Save
                CS_ResourceAllocationDetails details = new CS_ResourceAllocationDetails();
                details.JobID = jobID;
                details.Notes = notes;
                details.CreatedBy = username;
                details.CreationID = employeeID;
                details.CreationDate = DateTime.Now;
                details.ModifiedBy = username;
                details.ModificationID = employeeID;
                details.ModificationDate = DateTime.Now;
                details.IsSubContractor = IsSubContractor;
                details.SubContractorInfo = SubContractorInfo;
                details.FieldPO = FieldPO;
                details.Active = true;

                _resourceAllocationDetailsRepository.Add(details);
            }
            else
            {
                // Update
                CS_ResourceAllocationDetails details = new CS_ResourceAllocationDetails();
                details.JobID = jobID;
                details.Notes = notes;
                details.CreatedBy = oldDetails.CreatedBy;
                details.CreationID = oldDetails.CreationID;
                details.CreationDate = oldDetails.CreationDate;
                details.ModifiedBy = username;
                details.ModificationID = employeeID;
                details.ModificationDate = DateTime.Now;
                details.IsSubContractor = IsSubContractor;
                details.SubContractorInfo = SubContractorInfo;
                details.FieldPO = FieldPO;
                details.Active = true;

                _resourceAllocationDetailsRepository.Update(details);
            }
        }

        public void SendCallLogNotificationAddedEmails(CS_CallLog callLogEntry, List<CS_CallLogResource> resources)
        {
            try
            {
                EmailModel emailModel = new EmailModel(_unitOfWork);
                IList<CS_Email> emails = new List<CS_Email>();
                string receipts = string.Empty;

                //List receipts
                IList<EmailVO> receiptsEmail = _callCriteriaModel.ListReceiptsByCallLog(callLogEntry.CallTypeID.ToString(), callLogEntry.JobID, callLogEntry);

                if (receiptsEmail.Count > 0)
                {
                    for (int i = 0; i < receiptsEmail.Count; i++)
                        receipts += (i == 0) ? receiptsEmail[i].Email : string.Format(";{0}", receiptsEmail[i].Email);

                    //Body
                    string body = GenerateEmailBodyForAddedResources(resources);

                    //Subject
                    string subject = GenerateEmailSubjectForAddedResources(callLogEntry);

                    //Save Email
                    emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);

                    //Save Call Citeria
                    _callCriteriaModel.SendCallLogCriteriaEmails(callLogEntry);
                }
            }
            catch (Exception e)
            {
                throw new Exception("There was an error while trying to send added resources notification!", e);
            }
        }

        private string GenerateEmailSubjectForAddedResources(CS_CallLog callLogEntry)
        {
            StringBuilder subject = new StringBuilder();
            int jobID = callLogEntry.JobID;
            CS_Job job = _jobRepository.Get(e => e.ID == jobID, "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobInfo", "CS_JobInfo.CS_JobAction", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State");

            subject.Append(job.PrefixedNumber);
            subject.Append(", ");
            subject.Append(job.CS_CustomerInfo.CS_Customer.Name.Trim());
            subject.Append(", ");
            subject.Append(job.CS_JobInfo.CS_JobAction.Description);

            if (callLogEntry.CS_Job.ID != (int)Globals.GeneralLog.ID)
            {
                subject.Append(", ");
                subject.Append(job.CS_LocationInfo.CS_City.Name);
                subject.Append(" ");
                subject.Append(job.CS_LocationInfo.CS_State.Acronym);
            }

            subject.Append(", Added Resource");

            return subject.ToString();
        }

        private string GenerateEmailBodyForAddedResources(List<CS_CallLogResource> resources)
        {
            StringBuilder body = new StringBuilder();

            body.AppendLine("<TABLE>");

            body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
            body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
            body.AppendLine("<b>Resources Added</b>");
            body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>&nbsp;</TD>");
            body.AppendLine("</TR>");

            for (int i = 0; i < resources.Count; i++)
            {
                string division = string.Empty;
                string description = string.Empty;
                string date = string.Empty;
                string time = string.Empty;
                string duration = string.Empty;

                if (resources[i].EmployeeID.HasValue)
                {
                    int employeeID = resources[i].EmployeeID.Value;
                    int jobID = resources[i].JobID;
                    CS_Resource resource = _resourceRepository.Get(e => e.EmployeeID == employeeID && e.JobID == jobID && e.Active, "CS_Employee", "CS_Employee.CS_Division");
                    division = resource.CS_Employee.CS_Division.Name;
                    description = resource.CS_Employee.FullName;
                    date = resources[i].ActionDate.Value.ToString("MM/dd/yyyy");
                    time = resources[i].ActionDate.Value.ToString("HH:mm");
                    duration = Convert.ToInt32(resource.Duration).ToString();
                }
                else
                {
                    int equipmentID = resources[i].EquipmentID.Value;
                    int jobID = resources[i].JobID;
                    CS_Resource resource = _resourceRepository.Get(e => e.EquipmentID == equipmentID && e.JobID == jobID && e.Active, "CS_Equipment", "CS_Equipment.CS_Division");
                    division = resource.CS_Equipment.CS_Division.Name;
                    description = resource.CS_Equipment.Name;
                    date = resources[i].ActionDate.Value.ToString("MM/dd/yyyy");
                    time = resources[i].ActionDate.Value.ToString("HH:mm");
                    duration = Convert.ToInt32(resource.Duration).ToString();
                }

                //Division
                body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
                body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Division: </b>");
                body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(division);
                body.AppendLine("</TD>");
                body.AppendLine("</TR>");

                //Description
                body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
                body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Description: </b>");
                body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(description);
                body.AppendLine("</TD>");
                body.AppendLine("</TR>");

                //Date
                body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
                body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Date: </b>");
                body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(date);
                body.AppendLine("</TD>");
                body.AppendLine("</TR>");

                //Time
                body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
                body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Time: </b>");
                body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(time);
                body.AppendLine("</TD>");
                body.AppendLine("</TR>");

                //Duration
                body.AppendLine("<TR style='width: 100%; display: inline-block;'>");
                body.AppendLine("<TD style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Duration: </b>");
                body.AppendLine("</TD><TD style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(duration);
                body.AppendLine("</TD>");
                body.AppendLine("</TR>");
            }

            body.AppendLine("</TABLE>");

            return body.ToString();
        }

        public void SendCallLogNotificationReservedEmails(CS_CallLog callLogEntry, List<CS_Reserve> resources)
        {
            try
            {
                EmailModel emailModel = new EmailModel(_unitOfWork);
                IList<CS_Email> emails = new List<CS_Email>();
                string receipts = string.Empty;

                //List receipts
                IList<EmailVO> receiptsEmail = _callCriteriaModel.ListReceiptsByCallLog(callLogEntry.CallTypeID.ToString(), callLogEntry.JobID, callLogEntry);

                if (receiptsEmail.Count > 0)
                {
                    for (int i = 0; i < receiptsEmail.Count; i++)
                        receipts += (i == 0) ? receiptsEmail[i].Email : string.Format(";{0}", receiptsEmail[i].Email);

                    //Body
                    string body = GenerateEmailBodyForReservedResources(callLogEntry, resources);

                    //Subject
                    string subject = GenerateEmailSubjectForReservedResources(callLogEntry);

                    //Save Email
                    emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);

                    //Save Call Citeria
                    _callCriteriaModel.SendCallLogCriteriaEmails(callLogEntry);
                }
            }
            catch (Exception e)
            {
                throw new Exception("There was an error while trying to send added resources notification!", e);
            }
        }

        private string GenerateEmailSubjectForReservedResources(CS_CallLog callLogEntry)
        {
            StringBuilder subject = new StringBuilder();
            int jobID = callLogEntry.JobID;
            CS_Job job = _jobRepository.Get(e => e.ID == jobID, "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobInfo", "CS_JobInfo.CS_JobAction", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State");

            subject.Append(job.PrefixedNumber);
            subject.Append(", ");
            subject.Append(job.CS_CustomerInfo.CS_Customer.Name.Trim());
            subject.Append(", ");
            subject.Append(callLogEntry.CS_Job.CS_JobInfo.CS_JobAction.Description);

            if (job.ID != (int)Globals.GeneralLog.ID)
            {
                subject.Append(", ");
                subject.Append(callLogEntry.CS_Job.CS_LocationInfo.CS_City.Name);
                subject.Append(" ");
                subject.Append(callLogEntry.CS_Job.CS_LocationInfo.CS_State.Acronym);
            }

            subject.Append(", Reserved Resource");

            return subject.ToString();
        }

        private string GenerateEmailBodyForReservedResources(CS_CallLog callEntry, List<CS_Reserve> resources)
        {
            StringBuilder body = new StringBuilder();

            body.AppendLine("<div>");

            body.AppendLine("<div style='width: 100%; display: inline-block;'>");
            body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
            body.AppendLine("<b>Resources Reserved</b>");
            body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'></div>");
            body.AppendLine("</div>");

            for (int i = 0; i < resources.Count; i++)
            {
                string division = string.Empty;
                string description = string.Empty;
                string date = string.Empty;
                string time = string.Empty;
                string duration = string.Empty;

                if (resources[i].EmployeeID.HasValue)
                {
                    int reserveID = resources[i].ID;
                    CS_Reserve resource = _reserveRepository.Get(e => e.ID == reserveID, "CS_Employee", "CS_Employee.CS_Division");
                    division = resource.CS_Employee.CS_Division.Name;
                    description = resource.CS_Employee.FullName;
                    date = callEntry.CallDate.ToString("MM/dd/yyyy");
                    time = callEntry.CallDate.ToShortTimeString();
                    duration = resource.Duration.ToString();
                }
                else
                {
                    int reserveID = resources[i].ID;
                    CS_Reserve resource = _reserveRepository.Get(e => e.ID == reserveID && e.Active, "CS_EquipmentType", "CS_Division");
                    division = resource.CS_Division.Name;
                    description = resource.CS_EquipmentType.Name;
                    date = callEntry.CallDate.ToString("MM/dd/yyyy");
                    time = callEntry.CallDate.ToShortTimeString();
                    duration = resource.Duration.ToString();
                }

                //Division
                body.AppendLine("<div style='width: 100%; display: inline-block;'>");
                body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Division: </b>");
                body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(division);
                body.AppendLine("</div>");
                body.AppendLine("</div>");

                //Description
                body.AppendLine("<div style='width: 100%; display: inline-block;'>");
                body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Description: </b>");
                body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(description);
                body.AppendLine("</div>");
                body.AppendLine("</div>");

                //Date
                body.AppendLine("<div style='width: 100%; display: inline-block;'>");
                body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Date: </b>");
                body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(date);
                body.AppendLine("</div>");
                body.AppendLine("</div>");

                //Time
                body.AppendLine("<div style='width: 100%; display: inline-block;'>");
                body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Time: </b>");
                body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(time);
                body.AppendLine("</div>");
                body.AppendLine("</div>");

                //Duration
                body.AppendLine("<div style='width: 100%; display: inline-block;'>");
                body.AppendLine("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
                body.AppendLine("<b>Duration: </b>");
                body.AppendLine("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
                body.AppendLine(duration);
                body.AppendLine("</div>");
                body.AppendLine("</div>");
            }

            body.AppendLine("</div>");

            return body.ToString();
        }

        public virtual void GenerateAutomaticCallEntryAdd(int jobId, string userName, IList<CS_Resource> addedResourceList, DateTime callDate)
        {
            List<int> employees = new List<int>();
            List<int> equipments = new List<int>();
            List<CS_CallLogResource> callLogResources = new List<CS_CallLogResource>();

            BuildAddedResourceNote(addedResourceList, jobId);
            CS_CallLog addedResourceCallLogEntry = new CS_CallLog()
            {
                JobID = jobId,
                CallTypeID = (int)Globals.CallEntry.CallType.AddedResource,
                PrimaryCallTypeId = (jobId == Globals.GeneralLog.ID ? (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate : (int)Globals.CallEntry.PrimaryCallType.ResourceUpdateAddedResources),
                CallDate = callDate,
                Xml = null,
                Note = _addedResourceBuilder.ToString(),
                CreatedBy = userName,
                CreationDate = DateTime.Now,
                ModifiedBy = userName,
                ModificationDate = DateTime.Now,
                Active = true
            };

            _callLogRepository.Add(addedResourceCallLogEntry);

            foreach (CS_Resource resource in addedResourceList)
            {
                Globals.CallEntry.CallLogResourceType type = Globals.CallEntry.CallLogResourceType.Employee;
                switch ((Globals.ResourceAllocation.ResourceType)resource.Type)
                {
                    case Globals.ResourceAllocation.ResourceType.Employee:
                        type = Globals.CallEntry.CallLogResourceType.Employee;
                        break;
                    case Globals.ResourceAllocation.ResourceType.Equipment:
                        type = Globals.CallEntry.CallLogResourceType.Equipment;
                        break;
                }

                CS_CallLogResource addedResourceEntry = new CS_CallLogResource()
                {
                    CallLogID = addedResourceCallLogEntry.ID,
                    EmployeeID = resource.EmployeeID,
                    EquipmentID = resource.EquipmentID,
                    ContactID = null,
                    JobID = jobId,
                    Type = (int)type,
                    ActionDate = resource.StartDateTime,
                    CreatedBy = addedResourceCallLogEntry.CreatedBy,
                    CreationDate = addedResourceCallLogEntry.CreationDate,
                    ModifiedBy = addedResourceCallLogEntry.ModifiedBy,
                    ModificationDate = addedResourceCallLogEntry.ModificationDate,
                    Active = true,
                    CreationID = addedResourceCallLogEntry.CreationID,
                    ModificationID = addedResourceCallLogEntry.ModificationID
                };

                addedResourceEntry = _callLogResourceRepository.Add(addedResourceEntry);

                callLogResources.Add(addedResourceEntry);

                if (resource.EmployeeID.HasValue)
                    employees.Add(resource.EmployeeID.Value);
                else
                    equipments.Add(resource.EquipmentID.Value);

            }

            employees = addedResourceList.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList();
            equipments = addedResourceList.Where(e => e.EquipmentID.HasValue).Select(e => e.EquipmentID.Value).ToList();

            SendCallLogNotificationAddedEmails(addedResourceCallLogEntry, callLogResources);

            _callLogModel.VerifyDPICalculate(addedResourceCallLogEntry, employees, equipments);
        }

        public virtual void GenerateAutomaticCallEntryReserve(int jobId, string userName, IList<CS_Reserve> reservedResourceList, DateTime callDate)
        {
            BuildReservedResourceNote(reservedResourceList, jobId);
            CS_CallLog addedResourceCallLogEntry = new CS_CallLog()
            {
                JobID = jobId,
                CallTypeID = (int)Globals.CallEntry.CallType.ReservedResource,
                PrimaryCallTypeId = (jobId == Globals.GeneralLog.ID ? (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate : (int)Globals.CallEntry.PrimaryCallType.ResourceUpdateAddedResources),
                CallDate = callDate,
                Xml = null,
                Note = _reservedResourceBuilder.ToString(),
                CreatedBy = userName,
                CreationDate = DateTime.Now,
                ModifiedBy = userName,
                ModificationDate = DateTime.Now,
                Active = true,
                UserCall = true
            };

            addedResourceCallLogEntry = _callLogRepository.Add(addedResourceCallLogEntry);

            SendCallLogNotificationReservedEmails(addedResourceCallLogEntry, reservedResourceList.ToList());
        }

        public void SaveJobDivision(IList<int> lstDivisions, string userName, int jobId)
        {
            foreach (int divisionId in lstDivisions)
            {
                CS_JobDivision jobDivision = new CS_JobDivision
                {
                    Active = true,
                    CreatedBy = userName,
                    CreationDate = DateTime.Now,
                    PrimaryDivision = false,
                    ModifiedBy = userName,
                    ModificationDate = DateTime.Now,
                    JobID = jobId,
                    DivisionID = divisionId
                };

                _jobDivisionRepository.Add(jobDivision);
            }
        }

        public virtual void GenerateAutomaticCallEntryInitialAdvise(int jobId, string userName)
        {
            BuildInitialAdviseNote(jobId);
            CS_CallLog addedResourceCallLogEntry = new CS_CallLog()
            {
                JobID = jobId,
                CallTypeID = (int)Globals.CallEntry.CallType.InitialAdviseUpdate,
                PrimaryCallTypeId = (jobId == Globals.GeneralLog.ID ? (int)Globals.CallEntry.PrimaryCallType.NonJobUpdateNotification : (int)Globals.CallEntry.PrimaryCallType.JobUpdateNotification),
                CallDate = DateTime.Now,
                Xml = null,
                Note = _initialAdviseBuilder.ToString(),
                CreatedBy = userName,
                CreationDate = DateTime.Now,
                ModifiedBy = userName,
                ModificationDate = DateTime.Now,
                Active = true,
                UserCall = true
            };

            _callLogRepository.Add(addedResourceCallLogEntry);
        }

        #endregion

        #region [ Transfer ]

        /// <summary>
        /// Removes resources from one job and transfer to another, with the CallLogs selected by the user.
        /// Generates a CallLog of the transfer result on each job.
        /// </summary>
        /// <param name="resourceList"></param>
        public void TransferResources(List<CS_Resource> oldResourceList, List<CS_Resource> newResourceList, Dictionary<int, Globals.TransferResource.TransferType> transferTypeList, List<int> callLogIdList, int fromJobId, int toJobId, string username)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                CallLogModel callLogModel = new CallLogModel();

                //Removes resources from old job
                RemoveResources(oldResourceList);

                List<int> divisionIdList = new List<int>();
                DivisionModel divisionModel = new DivisionModel();
                //Get resources divisions
                List<CS_Division> divisionList = divisionModel.ListAllDivisionsByResources(oldResourceList);

                for (int i = 0; i < divisionList.Count; i++)
                {
                    if (!divisionIdList.Exists(e => e == divisionList[i].ID))
                        divisionIdList.Add(divisionList[i].ID);
                }

                //Add resources to new job
                SaveOrUpdateResourceAllocation(toJobId, new List<CS_Reserve>(), newResourceList, username, divisionIdList, string.Empty, false, DateTime.Now, false, string.Empty, string.Empty);

                //Deletes the selected call logs and re-create on another job
                callLogModel.TransferExistingResourceCallLogs(oldResourceList, callLogIdList, toJobId, username);

                //Create parked calllog for equipments on old job
                //Create transfer calllog for both jobs, with 
                callLogModel.CreateTransferResourceCallLogs(oldResourceList, transferTypeList, fromJobId, toJobId, username);

                scope.Complete();
            }
        }

        /// <summary>
        /// Logical Delete for a List of Resources
        /// </summary>
        /// <param name="resourceList">List of CS_Resource</param>
        public void RemoveResources(List<CS_Resource> resourceList)
        {
            if (resourceList.Count > 0)
            {
                for (int i = 0; i < resourceList.Count; i++)
                {
                    CS_Resource resource = resourceList[i];
                    resource.Active = false;
                }

                _resourceRepository.UpdateList(resourceList);
            }
        }

        #endregion

        #region [ Map Plotting ]

        /// <summary>
        /// Returns a list of map plotting objects based on filters
        /// </summary>
        /// <param name="parameters">Object that contains all the parameters for the request</param>
        /// <returns>List of map plotting objects</returns>
        public IList<Globals.MapPlotDataObject> FilterMapPlotting(Globals.MapPlotRequestDataObject parameters)
        {
            bool hasEquipmentType = parameters.EquipmentTypeList != null;
            bool hasRegionList = parameters.RegionList != null;

            if (null == parameters.EquipmentTypeList)
                parameters.EquipmentTypeList = new int[0];
            if (null == parameters.RegionList)
                parameters.RegionList = new int[0];

            int activeJobStatus = (int)Globals.JobRecord.JobStatus.Active;
            int generalLogJobId = Globals.GeneralLog.ID;
            IList<CS_Resource> resourceList = _resourceRepository.ListAll(
                e => (hasEquipmentType ? (e.EquipmentID.HasValue && parameters.EquipmentTypeList.Contains(e.CS_Equipment.EquipmentTypeID)) : true) &&
                     (hasRegionList ? (e.EquipmentID.HasValue &&
                        e.CS_Equipment.CS_Division.CS_Region_Division.Any(f => f.Active && parameters.RegionList.Contains(f.RegionID))) : true) &&
                     (string.IsNullOrEmpty(parameters.ComboNumber) || (e.EquipmentID.HasValue && e.CS_Equipment.ComboID.HasValue && e.CS_Equipment.CS_EquipmentCombo.Name.Contains(parameters.ComboNumber))) &&
                     (string.IsNullOrEmpty(parameters.UnitNumber) || (e.EquipmentID.HasValue && e.CS_Equipment.Name.Contains(parameters.UnitNumber))) &&
                     (string.IsNullOrEmpty(parameters.EmployeeName) || (e.EmployeeID.HasValue && (e.CS_Employee.Name.Contains(parameters.EmployeeName) || e.CS_Employee.FirstName.Contains(parameters.EmployeeName)))) &&
                     (string.IsNullOrEmpty(parameters.EmployeeTitle) || (e.EmployeeID.HasValue && e.CS_Employee.BusinessCardTitle.Contains(parameters.EmployeeTitle))) &&
                     e.Active &&
                     ((!parameters.JobNumberID.HasValue) || (parameters.JobNumberID.HasValue && e.CS_Job.ID == parameters.JobNumberID.Value)) &&
                     e.CS_Job.ID != generalLogJobId &&
                     e.CS_Job.CS_JobInfo.CS_Job_JobStatus.Any(f => f.Active && f.JobStatusId == activeJobStatus),
                "CS_Job", "CS_Job.CS_JobInfo", "CS_Job.CS_JobInfo.CS_Job_JobStatus", "CS_Job.CS_JobInfo.CS_PriceType", "CS_Job.CS_JobInfo.CS_JobType",
                "CS_Job.CS_LocationInfo", "CS_Job.CS_LocationInfo.CS_Country", "CS_Job.CS_LocationInfo.CS_State", "CS_Job.CS_LocationInfo.CS_City", "CS_Job.CS_LocationInfo.CS_ZipCode",
                "CS_Equipment", "CS_Equipment.CS_EquipmentCombo", "CS_Employee");

            IList<Globals.MapPlotDataObject> returnList = new List<Globals.MapPlotDataObject>();
            for (int i = 0; i < resourceList.Count; i++)
            {
                CS_Resource currentResource = resourceList[i];

                Globals.MapPlotDataObject mapObject = new Globals.MapPlotDataObject();
                if (currentResource.CS_Job.CS_LocationInfo.CS_ZipCode.Latitude.HasValue)
                    mapObject.Latitude = currentResource.CS_Job.CS_LocationInfo.CS_ZipCode.Latitude.Value;
                if (currentResource.CS_Job.CS_LocationInfo.CS_ZipCode.Longitude.HasValue)
                    mapObject.Longitude = currentResource.CS_Job.CS_LocationInfo.CS_ZipCode.Longitude.Value;
                mapObject.Type = "R";
                mapObject.Name = string.Format("Job #: {0}", currentResource.CS_Job.PrefixedNumber);
                mapObject.Description = string.Format("Location: {0}, {1}, {2}, {3}",
                    currentResource.CS_Job.CS_LocationInfo.CS_City.Name,
                    currentResource.CS_Job.CS_LocationInfo.CS_State.Acronym,
                    currentResource.CS_Job.CS_LocationInfo.CS_Country.Name,
                    currentResource.CS_Job.CS_LocationInfo.CS_ZipCode.ZipCodeNameEdited);

                if (currentResource.EquipmentID.HasValue)
                {
                    if (currentResource.CS_Equipment.ComboID.HasValue)
                        mapObject.Description += string.Format("<br />Resource: {0} - {1}", currentResource.CS_Equipment.CS_EquipmentCombo.Name, currentResource.CS_Equipment.Description);
                    else
                        mapObject.Description += string.Format("<br />Resource: {0} - {1}", currentResource.CS_Equipment.Name, currentResource.CS_Equipment.Description);
                }
                else
                    mapObject.Description += string.Format("<br />Resource: {0}", currentResource.CS_Employee.FullName);

                returnList.Add(mapObject);
            }

            // Fix position for jobs at the same point
            for (int i = 0; i < returnList.Count; i++)
            {
                IList<Globals.MapPlotDataObject> samePointList = returnList.Where(e => e.Latitude == returnList[i].Latitude && e.Longitude == returnList[i].Longitude).ToList();
                double newLongitude = returnList[i].Longitude;
                double newLatitude = returnList[i].Latitude;
                for (int j = 0; j < samePointList.Count; j++)
                {
                    newLongitude += 0.005;
                    newLatitude += 0.005;
                    samePointList[j].Longitude = newLongitude;
                    samePointList[j].Latitude = newLatitude;
                }
            }

            return returnList;
        }

        #endregion

        #endregion

        #region [ Private Methods ]

        private void BuildAddedResourceNote(IList<CS_Resource> addedResourceList, int jobId)
        {
            _addedResourceBuilder = new StringBuilder();

            if (jobId != (int)Globals.GeneralLog.ID)
            {
                CS_Job job = _jobRepository.Get(e => jobId == e.ID);
                _addedResourceBuilder.Append("Job #:<Text> " + job.PrefixedJobNumber + "<BL>");
            }
            else
                _addedResourceBuilder.Append("Job #:<Text> PA99999 - General Log<BL>");

            _addedResourceBuilder.Append("Resources Added:<Text> " + "<BL>");

            for (int i = 0; i < addedResourceList.Count; i++)
            {
                CS_Resource resource = addedResourceList[i];

                if (resource.EquipmentID != null)
                    _addedResourceBuilder.Append("Division:<Text> " + resource.CS_Equipment.CS_Division.Name + "<BL>");
                else
                    _addedResourceBuilder.Append("Division:<Text> " + resource.CS_Employee.CS_Division.Name + "<BL>");

                _addedResourceBuilder.Append("Description:<Text>");

                if (resource.EquipmentID != null)
                    _addedResourceBuilder.Append("" + resource.CS_Equipment.CompleteName + "<BL>");
                else if (resource.EmployeeID != null)
                    _addedResourceBuilder.Append("" + resource.CS_Employee.FullName + "<BL>");

                _addedResourceBuilder.Append("Date:<Text>" + resource.StartDateTime.Date.ToShortDateString() + "<BL>");
                _addedResourceBuilder.AppendLine("Time:<Text>" + resource.StartDateTime.TimeOfDay.ToString() + "<BL>");
                _addedResourceBuilder.AppendLine("Duration:<Text>" + resource.Duration.ToString() + "<BL><BL>");
            }
        }

        private void BuildReservedResourceNote(IList<CS_Reserve> reservedResourceList, int jobId)
        {
            
            _reservedResourceBuilder = new StringBuilder();

            if (jobId != (int)Globals.GeneralLog.ID)
            {
                CS_Job job = _jobRepository.Get(e => jobId == e.ID);
                _reservedResourceBuilder.Append("Job #:<Text> " + job.PrefixedJobNumber + "<BL>");
            }
            else
                _reservedResourceBuilder.Append("Job #:<Text> PA99999 - General Log<BL>");

            _reservedResourceBuilder.Append("Resources Reserved:<Text> " + "<BL>");
            foreach (CS_Reserve resource in reservedResourceList)
            {
                if (resource.EquipmentTypeID != null)
                    if (null != resource.CS_Division)
                        _reservedResourceBuilder.Append("Division:<Text> " + resource.CS_Division.Name + "<BL>");
                    else
                        _reservedResourceBuilder.Append("Division:<Text> Division not available<BL>");
                else
                    _reservedResourceBuilder.Append("Division:<Text> " + resource.CS_Employee.CS_Division.Name + "<BL>");
                _reservedResourceBuilder.Append("Description:<Text>");
                if (resource.EquipmentTypeID != null)
                    _reservedResourceBuilder.Append("" + resource.CS_EquipmentType.Name + "<BL>");
                else if (resource.EmployeeID != null)
                    _reservedResourceBuilder.Append("" + resource.CS_Employee.FullName + "<BL>");
                _reservedResourceBuilder.Append("Date:<Text>" + resource.StartDateTime.Date.ToShortDateString() + "<BL>");

                _reservedResourceBuilder.AppendLine("Time:<Text>" + resource.StartDateTime.TimeOfDay.ToString() + "<BL>");
                _reservedResourceBuilder.AppendLine("Duration:<Text>" + resource.Duration.ToString() + "<BL><BL>");
            }
        }

        private void BuildInitialAdviseNote(int jobId)
        {
            _initialAdviseBuilder = new StringBuilder();

            CS_Job NewJob = _jobRepository.Get(e => e.ID == jobId, new string[] { "CS_JobInfo", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobCategory", 
                "CS_JobInfo.CS_JobType", "CS_JobInfo.CS_Job_JobStatus", "CS_JobInfo.CS_JobAction", "CS_PresetInfo", "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_CustomerInfo.CS_Employee", "CS_CustomerInfo.CS_Contact3", 
                "CS_JobDivision", "CS_JobDivision.CS_Division", "CS_LocationInfo", "CS_LocationInfo.CS_Country", "CS_LocationInfo.CS_State", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_ZipCode",
                "CS_JobDescription", "CS_ScopeOfWork" });

            if (!string.IsNullOrEmpty(NewJob.Number) && NewJob.CS_JobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.Active))
                _initialAdviseBuilder.Append("Job #:<Text>" + NewJob.Number + "<BL>");
            else
                _initialAdviseBuilder.Append("Job #:<Text>" + NewJob.Internal_Tracking + "<BL>");
            _initialAdviseBuilder.Append("Company:<Text>" + NewJob.CS_CustomerInfo.CS_Customer.Name + "<BL>");
            if (NewJob.CS_CustomerInfo.InitialCustomerContactId.HasValue)
                _initialAdviseBuilder.Append("Initial Company Contact:<Text> " + NewJob.CS_CustomerInfo.CS_Contact3.Name + "<BL>");
            else if (NewJob.CS_CustomerInfo.PocEmployeeId.HasValue)
            {
                CS_Employee hulcherPOC = NewJob.CS_CustomerInfo.CS_Employee;
                if (null != hulcherPOC)
                    _initialAdviseBuilder.Append("Hulcher P.O.C:<Text> " + hulcherPOC.Name + ", " + hulcherPOC.FirstName + "<BL>");
            }

            int j = 0;
            foreach (CS_JobDivision jobDivision in NewJob.CS_JobDivision)
            {
                if (jobDivision.Active)
                {
                    if (j.Equals(0))
                        _initialAdviseBuilder.Append("Division:<Text> " + jobDivision.CS_Division.Name + "<BL>");
                    else
                        _initialAdviseBuilder.Append(" <Text> " + jobDivision.CS_Division.Name + "<BL>");
                    j++;
                }
            }
            _initialAdviseBuilder.Append("Resources:<Text> " + "<BL>");
            if (null != _addedResourceBuilder)
                _initialAdviseBuilder.Append(_addedResourceBuilder.ToString());
            if (null != _reservedResourceBuilder)
                _initialAdviseBuilder.Append(_reservedResourceBuilder.ToString());
            _initialAdviseBuilder.Append("Job Status:<Text> " + NewJob.CS_JobInfo.LastJobStatus.Description + "<BL>");
            _initialAdviseBuilder.Append("Job start date:<Text> " + NewJob.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(e => e.Active).JobStartDate + "<BL>");
            _initialAdviseBuilder.Append("Price type:<Text> " + NewJob.CS_JobInfo.CS_PriceType.Description + "<BL>");
            _initialAdviseBuilder.Append("Job Category:<Text> " + NewJob.CS_JobInfo.CS_JobCategory.Description + "<BL>");
            _initialAdviseBuilder.Append("Job type:<Text> " + NewJob.CS_JobInfo.CS_JobType.Description + "<BL>");
            _initialAdviseBuilder.Append("Job Action:<Text> " + NewJob.CS_JobInfo.CS_JobAction.Description + "<BL>");
            if (null != NewJob.CS_PresetInfo)
            {
                _initialAdviseBuilder.Append("Preset Instructions:<Text> " + NewJob.CS_PresetInfo.Instructions + "<BL>");
                if (NewJob.CS_PresetInfo.Date.HasValue)
                    _initialAdviseBuilder.Append("Preset Date:<Text> " + NewJob.CS_PresetInfo.Date.Value.ToString("MM/dd/yyyy") + "<BL>");
            }
            _initialAdviseBuilder.Append("Location:<Text> " + NewJob.CS_LocationInfo.CS_Country.Name + ", " + NewJob.CS_LocationInfo.CS_State.Name + ", " + NewJob.CS_LocationInfo.CS_City.Name + ", " + NewJob.CS_LocationInfo.CS_ZipCode.ZipCodeNameEdited + "<BL>");
            if (NewJob.CS_JobDescription.NumberEngines.HasValue)
                _initialAdviseBuilder.Append("# of engines:<Text> " + NewJob.CS_JobDescription.NumberEngines.Value + "<BL>");
            if (NewJob.CS_JobDescription.NumberLoads.HasValue)
                _initialAdviseBuilder.Append("# of loads:<Text> " + NewJob.CS_JobDescription.NumberLoads.Value + "<BL>");
            if (NewJob.CS_JobDescription.NumberEmpties.HasValue)
                _initialAdviseBuilder.Append("# of empties:<Text> " + NewJob.CS_JobDescription.NumberEmpties.Value + "<BL>");
            _initialAdviseBuilder.Append("Lading:<Text> " + NewJob.CS_JobDescription.Lading + "<BL>");
            _initialAdviseBuilder.Append("UN#:<Text> " + NewJob.CS_JobDescription.UnNumber + "<BL>");
            _initialAdviseBuilder.Append("STCC Info:<Text> " + NewJob.CS_JobDescription.STCCInfo + "<BL>");
            _initialAdviseBuilder.Append("Hazmat:<Text> " + NewJob.CS_JobDescription.Hazmat + "<BL>");
            _initialAdviseBuilder.Append("Scope of Work: ");

            foreach (CS_ScopeOfWork scopeOfWork in NewJob.CS_ScopeOfWork)
                _initialAdviseBuilder.AppendLine(" <Text>" + scopeOfWork.ScopeOfWork + "<BL>");
        }

        #endregion

        #region [ IDisposable implementation ]

        public void Dispose()
        {
            _resourceRepository = null;
            _reserveRepository = null;
            _resourceCallLogInfoRepository = null;
            _callLogRepository = null;
            _jobRepository = null;
            _resourceAllocationDetailsRepository = null;

            _callLogModel.Dispose();
            _callLogModel = null;
            _callCriteriaModel.Dispose();
            _callCriteriaModel = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
