using System.Collections.Generic;
using Hulcher.OneSource.CustomerService.DataContext;
using System;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the JobRecord
    /// </summary>
    public interface IJobRecordView : IBaseView
    {
        /// <summary>
        /// Get or Set the emergency response
        /// </summary>
        bool IsEmergencyResponse { get; set; }

        /// <summary>
        /// Property for accessing the CustomerInfo entity
        /// </summary>
        CS_CustomerInfo CustomerInfo { get; }

        /// <summary>
        /// Property for accessing the JobDivision entity list
        /// </summary>
        IList<CS_JobDivision> DivisionList { get; }

        /// <summary>
        /// Property for accessing the PresetInfo entity
        /// </summary>
        CS_PresetInfo PresetInfo { get; }

        /// <summary>
        /// Property for accessing the LostJobInfo entity
        /// </summary>
        CS_LostJobInfo LostJobInfo { get; }

        /// <summary>
        /// Property for accessing the SpecialPricing entity
        /// </summary>
        CS_SpecialPricing SpecialPricing { get; }

        /// <summary>
        /// Property for accessing the JobInfo entity
        /// </summary>
        CS_JobInfo JobInfo { get; }

        /// <summary>
        /// Property for accessing the LocationInfo entity
        /// </summary>
        CS_LocationInfo LocationInfo { get; }

        /// <summary>
        /// Property for accessing the JobDescription entity
        /// </summary>
        CS_JobDescription JobDescription { get; }

        /// <summary>
        /// Property for accessing the ScopeOfWork entity List
        /// </summary>
        IList<CS_ScopeOfWork> ScopeOfWorkList { get; }

        /// <summary>
        /// Property for accessing the PermitInfo entity List
        /// </summary>
        IList<CS_JobPermit> PermitInfoList { get; }

        /// <summary>
        /// Property for acessing the JobPhotoReport entity List
        /// </summary>
        IList<CS_JobPhotoReport> PhotoReportList { get; }

        /// <summary>
        /// Property for acessing the LocalEquipmentTypeVO Entity List
        /// </summary>
        IList<LocalEquipmentTypeVO> RequestedEquipmentTypeList { get; set; }

        /// <summary>
        /// Property for accessing the Job entity
        /// </summary>
        CS_Job Job { get; }

        /// <summary>
        /// Used for the Load
        /// </summary>
        CS_View_GetJobData JobLoad { get; set; }

        /// <summary>
        /// Property for storing the Job Id
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Job ID when cloning a job record
        /// </summary>
        int? CloningId { get; set; }

        /// <summary>
        /// Job Status Id for the current Job
        /// </summary>
        int JobStatusId { get; }

        /// <summary>
        /// Property for getting the username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Property for getting the Domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Propertie for set specific ready only
        /// </summary>
        bool ReadyOnlyJobRecords { set; }

        /// <summary>
        /// Gets or Sets page Title
        /// </summary>
        string PageTitle { get; set; }

        string txtParentUpdateClient { get; }

        //void SetEmailPage();

        /// <summary>
        /// Propertie to specify if at update initial advise should be saved.
        /// </summary>
        bool CreateInitialAdvise { get; set; }

        bool SaveAndClose { get; set; }

        void SetCloseEnabled();

        /// <summary>
        /// Property that opens the ResourceAllocation in the ConversionMode
        /// </summary>
        bool ResourceConversion { set; }

        /// <summary>
        /// Property that opens the CallEntry to create a Parked CallType
        /// </summary>
        bool HasResources { set; }

        /// <summary>
        /// Property that gets the JobStartDate
        /// </summary>
        DateTime? JobStartDate { get; set; }

        /// <summary>
        /// Property that gets the JobCloseDate
        /// </summary>
        DateTime? JobCloseDate { get; set; }

        bool SaveInitialAdviseEmail { get; set; }

        bool OpenEmailInitialAdvise { set; }
    }
}
