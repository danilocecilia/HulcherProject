using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class ResourceTransferViewModel : IDisposable
    {
        #region [ Attributes ]

        private IResourceTransferView _view;
        private CallLogModel _callLogModel;
        private JobModel _jobModel;

        #endregion

        #region [ Constructor ]

        public ResourceTransferViewModel(IResourceTransferView view)
        {
            _view = view;
        }

        #endregion

        #region [ Methods ]

        #region [ Resources ]

        public void TransferResources()
        {
            using (_jobModel = new JobModel())
            {
                CS_JobStatus jobStatus = _jobModel.GetJobById(_view.JobIdTo).CS_JobInfo.LastJobStatus;

                if (null == jobStatus || (null != jobStatus && jobStatus.ID != (int)Globals.JobRecord.JobStatus.Active))
                {
                    _view.DisplayMessage("Resources cannot be transfered to an inactive job.", false);
                    return;
                }
            }

            using (ResourceAllocationModel _resourceAllocationModel = new ResourceAllocationModel())
            {
                if (_view.SelectedResources.Count > 0)
                {
                    List<CS_Resource> newJobResourceSelection = CopyResourcesToNewList(_view.SelectedResources, _view.JobIdTo);
                    _resourceAllocationModel.TransferResources(_view.SelectedResources, newJobResourceSelection, _view.SelectedResourcesTransferType, _view.SelectedCallLogIds, _view.JobId, _view.JobIdTo, _view.Username);

                    _view.DisplayMessage("Resources transfered successfully!", true);
                }
                else
                {
                    _view.DisplayMessage("There was an error loading the resources for transfer, please try opening the page again.", true);
                }
            }
        }

        private List<CS_Resource> CopyResourcesToNewList(List<CS_Resource> oldResources, int toJobId)
        {
            List<CS_Resource> list = new List<CS_Resource>();

            for (int i = 0; i < oldResources.Count; i++)
            {
                CS_Resource oldResource = oldResources[i];

                CS_Resource newResource = new CS_Resource()
                {
                    EmployeeID = oldResource.EmployeeID,
                    EquipmentID = oldResource.EquipmentID,
                    JobID = toJobId,
                    Description = oldResource.Description,
                    Duration = oldResource.Duration,
                    StartDateTime = oldResource.StartDateTime,
                    Type = oldResource.Type,
                    Active = true
                };

                list.Add(newResource);
            }

            return list;
        }

        /// <summary>
        /// Method to load the resource information based on job id and resource id
        /// </summary>
        public void LoadResources()
        {
            using (ResourceAllocationModel _resourceAllocationModel = new ResourceAllocationModel())
            {
                IList<CS_Resource> lstResource = new List<CS_Resource>();

                if (_view.ResourceTransferId.Count > 0)
                {
                    CS_Resource resource = new CS_Resource();

                    _view.HasEquipments = false;
                    for (int i = 0; i < _view.ResourceTransferId.Count; i++)
                    {
                        resource = _resourceAllocationModel.GetResourceByJobAndResourceId(_view.JobId, _view.ResourceTransferId[i]);

                        if (resource.EquipmentID.HasValue)
                            _view.HasEquipments = true;

                        if (null != resource)
                            lstResource.Add(resource);
                    }
                }

                if (lstResource.Count > 0)
                {
                    _view.ResourceListDataSource = lstResource;
                    _view.SelectedResources = lstResource.ToList();
                }
            }
        }

        /// <summary>
        /// Set all fields for the resource repeater
        /// </summary>
        public void SetResourceRowData()
        {
            if (null != _view.ResourceRepeaterDataItem)
            {
                _view.ResourceId = _view.ResourceRepeaterDataItem.ID;

                if (_view.ResourceRepeaterDataItem.Type == (int)Globals.ResourceAllocation.ResourceType.Employee)
                {
                    _view.ResourceName = _view.ResourceRepeaterDataItem.CS_Employee.FullName;
                    _view.ResourceType = "Employee";
                }
                else
                {
                    _view.ResourceName = _view.ResourceRepeaterDataItem.CS_Equipment.Name;
                    _view.ResourceType = "Equipment";
                }
            }
        }
        #endregion

        #region [ JobCallLogs ]
        /// <summary>
        /// Method to load all jobcalllogs and set it to the property CallLogListDataSource
        /// </summary>
        public void LoadJobCallLogByResources()
        {
            using (_callLogModel = new CallLogModel())
            {
                IList<CS_CallLog> lstCallLogs = _callLogModel.GetCallLogByResource(
                    _view.ResourceRepeaterDataItem.JobID,
                    _view.ResourceRepeaterDataItem.EmployeeID,
                    _view.ResourceRepeaterDataItem.EquipmentID);

                if (null != lstCallLogs)
                {
                    _view.CallLogListDataSource = lstCallLogs;
                }
            }
        }

        /// <summary>
        /// Set all fields for the jobcall log repeater
        /// </summary>
        public void SetJobCallLogRowData()
        {
            if (null != _view.JobCallLogRepeaterDataItem)
            {
                _view.CallType = _view.JobCallLogRepeaterDataItem.CS_CallType.Description;

                if (_view.JobCallLogRepeaterDataItem.CalledInByCustomer.HasValue)
                    _view.CalledInBy = _view.JobCallLogRepeaterDataItem.CS_Contact.FullName;
                else if (_view.JobCallLogRepeaterDataItem.CalledInByEmployee.HasValue)
                    _view.CalledInBy = _view.JobCallLogRepeaterDataItem.CS_Employee.FullName;
                else
                    _view.CalledInBy = "User";

                _view.CallDate = _view.JobCallLogRepeaterDataItem.CallDate.ToString("MM/dd/yyyy");

                _view.CallTime = _view.JobCallLogRepeaterDataItem.CallDate.ToString("HH:mm:ss");

                _view.ModifiedBy = _view.JobCallLogRepeaterDataItem.ModifiedBy;

                _view.CallLogId = _view.JobCallLogRepeaterDataItem.ID.ToString();

                _view.ModificationId = _view.JobCallLogRepeaterDataItem.ModificationID.ToString();

                if (!string.IsNullOrEmpty(_view.JobCallLogRepeaterDataItem.Note))
                    _view.Notes = StringManipulation.TabulateString(_view.JobCallLogRepeaterDataItem.Note);
            }
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]
        public void Dispose()
        {
            _callLogModel.Dispose();
            _callLogModel = null;

            _jobModel.Dispose();
            _jobModel = null;
        }
        #endregion
    }
}
