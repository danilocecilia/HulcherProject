using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IResourceTransferView : IBaseView
    {
        /// <summary>
        /// Job Identifier that will receive the new resources
        /// </summary>
        int JobIdTo { get; set; }

        /// <summary>
        /// Job id that will be used to retrieve the resources information
        /// </summary>
        int JobId { get; set; }

        /// <summary>
        /// List of resources id that will be used to retrieve the resources information
        /// </summary>
        List<int> ResourceTransferId { get; set; }

        /// <summary>
        /// List of resources to bind repeater
        /// </summary>
        IList<CS_Resource> ResourceListDataSource { get; set; }

        /// <summary>
        /// Set the resource id to a field inside the repeater
        /// </summary>
        int ResourceId { set; }

        /// <summary>
        /// Set resource type to a field inside repeater
        /// </summary>
        string ResourceType { set; }

        /// <summary>
        /// Set resource name to a field inside repeater
        /// </summary>
        string ResourceName { set; }

        /// <summary>
        /// Get entity resource
        /// </summary>
        CS_Resource ResourceRepeaterDataItem { get; }

        /// <summary>
        /// Get the entity call log
        /// </summary>
        CS_CallLog JobCallLogRepeaterDataItem { get; }

        /// <summary>
        /// Set the calllist to do the bind of call log repeater
        /// </summary>
        IList<CS_CallLog> CallLogListDataSource { get; set; }

        /// <summary>
        /// Set the call type for the jobcalllog repeater
        /// </summary>
        string CallType { set; }

        /// <summary>
        /// Set the callledinby for jobcalllog repeater
        /// </summary>
        string CalledInBy { set; }

        /// <summary>
        /// Set the call date for the calllog repeater
        /// </summary>
        string CallDate { set; }

        /// <summary>
        /// Set the calltime for the calllog repeater
        /// </summary>
        string CallTime { set; }

        /// <summary>
        /// Set the modified by for the calllog repeater
        /// </summary>
        string ModifiedBy { set; }

        /// <summary>
        /// Set the calllog id for the calllog repeater to be used on saving record
        string CallLogId { set; }

        /// <summary>
        /// Set the modification id for the calllog repeater to be used on saving record
        /// </summary>
        string ModificationId { set; }

        /// <summary>
        /// Set the notes for the calllog repeater to be used on saving record
        /// </summary>
        string Notes { set; }

        /// <summary>
        /// Get, Set the list of Resources being transfered
        /// </summary>
        List<CS_Resource> SelectedResources { get; set; }

        /// <summary>
        /// Get a dictionary containing the transfer type of the selected Resources
        /// </summary>
        Dictionary<int, Globals.TransferResource.TransferType> SelectedResourcesTransferType { get; }

        /// <summary>
        /// Get the list of CallLogs being transfered
        /// </summary>
        List<int> SelectedCallLogIds { get; }

        /// <summary>
        /// Returns the name of the current User
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Indicates if in the selection of resources to transfer there's at least one equipment
        /// </summary>
        bool HasEquipments { get; set; }
    }
}
