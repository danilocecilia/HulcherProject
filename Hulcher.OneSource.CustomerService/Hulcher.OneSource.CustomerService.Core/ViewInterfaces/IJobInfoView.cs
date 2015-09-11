using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Utils;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IJobInfoView : IBaseView
    {
        #region [ Dropdowns ]

        /// <summary>
        /// Set the JobStatus list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_JobStatus> JobStatusList { set; }

        /// <summary>
        /// Set the PriceType list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_PriceType> PriceTypeList { set; }

        /// <summary>
        /// Set the JobCategory list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_JobCategory> JobCategoryList { set; }

        /// <summary>
        /// Set the JobType list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_JobType> JobTypeList { set; }

        /// <summary>
        /// Set the LostJobReason list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_LostJobReason> LostJobReasonList { set; }

        /// <summary>
        /// Set the Competitor list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_Competitor> CompetitorList { set; }


        /// <summary>
        /// Set the Frequency list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_Frequency> FrequencyList { set; }

    
        /// <summary>
        /// Get the Resource Type list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_Employee> ApprovingRVPList { set; }

        /// <summary>
        /// List of Divisions to get and populate view
        /// </summary>
        IList<CS_Division> DivisionValueList { set; }

        #endregion

        #region [ Properties ]
        /// <summary>sta
        /// Return the id of division
        /// </summary>
        int DivisionId { get; set; }

        IList<CS_Reserve> GetReserveByDivision { get; set; }

        /// <summary>
        /// Used for the load
        /// </summary>
        CS_View_GetJobData JobInfoLoad { get; set; }

        /// <summary>
        /// Used for the load
        /// </summary>
        CS_SpecialPricing SpecialPricingLoad { get; set; }

        CS_PresetInfo PresetInfoLoad { get; set; }

        CS_LostJobInfo LostJobInfoLoad { get; set; }

        IList<CS_JobDivision> JobDivisionListLoad { get; set; }

        IList<CS_CustomerContract> CustomerContractListLoad { get; set; }

        /// <summary>
        /// Property used to store Job Identification number and to load Job Info User Control
        /// </summary>
        int? JobId { get;  set; }

        /// <summary>
        /// Property go get/set the customer id related to job
        /// </summary>
        int CustomerId { get; set; }

        /// <summary>
        /// Property used to set job id when cloning a job record
        /// </summary>
        int? CloningId { get; set; }

        /// <summary>
        /// Index to keep track when removing a Division from list
        /// </summary>
        int DivisionRemoveIndex { get; set; }

        /// <summary>
        /// JobInfo Information related to the job record
        /// </summary>
        CS_JobInfo JobInfoEntity { get; set; }

        /// <summary>
        /// Special Pricing Information related to the job record
        /// </summary>
        CS_SpecialPricing SpecialPricingEntity { get; set; }

        /// <summary>
        /// Preset Information related to the job record
        /// </summary>
        CS_PresetInfo PresetInfoEntity { get; set; }

        /// <summary>
        /// Lost Job Information related to the job record
        /// </summary>
        CS_LostJobInfo LostJobEntity { get; set; }

        /// <summary>
        /// Set the JobDivision list to the grid on the webpage
        /// </summary>
        IList<CS_JobDivision> JobDivisionEntityList { get; set; }

        /// <summary>
        /// Get the Customer Contract list to the gridview on the webpage
        /// </summary>
        IList<CS_CustomerContract> CustomerContract { set; }

        /// <summary>
        /// Get the InitialCallDate
        /// </summary>
        DateTime InitialCallDate { get; }

        /// <summary>
        /// Get the initialCallTime
        /// </summary>
        TimeSpan InitialCallTime { get; }

        /// <summary>
        /// Get the JobStatusValue
        /// </summary>
        int JobStatusValue { get; set; }

        /// <summary>
        /// Job Start Date to get and populate view
        /// </summary>
        DateTime? JobStartDate { get; set; }

        /// <summary>
        /// Job Price Type
        /// </summary>
        int PriceTypeValue { get; }

        /// <summary>
        /// Job Category
        /// </summary>
        int JobCategoryValue { get; set; }

        /// <summary>
        /// Job Type
        /// </summary>
        int JobTypeValue { get; set; }

        /// <summary>
        /// Job Action
        /// </summary>
        int JobActionValue { get; }

        /// <summary>
        /// Job Close Date to get and populate view
        /// </summary>
        DateTime? JobCloseDate { get; set; }

        /// <summary>
        /// Lost Job Notes to get and populate view
        /// </summary>
        string LostJobNotes { get; set; }

        /// <summary>
        /// Lost Job Reason to get and populate view
        /// </summary>
        int LostJobReasonValue { get; set; }

        /// <summary>
        /// Job Competitor to get and populate view
        /// </summary>
        int CompetitorValue { get; set; }

        /// <summary>
        /// Employee POC to get and populate view
        /// </summary>
        int PocFollowUp { get; set; }

        /// <summary>
        /// Job preset instructions to get and populate view
        /// </summary>
        string PresetInstructions { get; set; }

        /// <summary>
        /// Job Preset Date to get and populate view
        /// </summary>
        DateTime? PresetDate { get; set; }

        /// <summary>
        /// Job Preset Time to get and populate view
        /// </summary>
        TimeSpan? PresetTime { get; set; }

        /// <summary>
        /// Interim Bill value
        /// </summary>
        bool InterimBill { get; }

        /// <summary>
        /// Job Requested By to get and populate view
        /// </summary>
        int RequestedByValue { get; set; }

        int? ApprovingRVPValue { get; set; }

        int FrequencyValue { get; set; }

        string UserName { get; }

        string Domain { get; }

        CS_JobDivision JobDivisionEntity { get; set; }

        void ListJobDivision();

        CS_Division DivisionValue { get; }

        bool LostJobInfoValidationIsEnabled { get; set; }

        bool PresetInfoValidationIsEnabled { get; set; }

        bool RequestedByEnabled { get; set; }

        bool FrequencyEnabled { get; set; }

        bool CustomerSpecificInfoVisibility { get; set; }

        string CustomerSpecificInfoXML { get; set; }

        List<Control> CustomerSpecificInfoControls { get; set; }

        CS_Division DivisionFromExternalSource { get; set; }

        CS_Customer CustomerFromExternalSource { get; set; }

        //IList<CS_Employee> ProjectManagerList { set; }

        string ValidationGroup { set; }

        string PresetValidationGroup { set; }

        string CreatedBy { get; }

        IList<CS_Resource> GetResourcesByDivision { get; set; }

        #endregion

        #region [ Methods ]

        void RefreshUpdatePanel();

        void ClearDivisions();

        void ReadyOnlyJobInfo();

        #endregion

        void CheckJobStatus();
    }
}
