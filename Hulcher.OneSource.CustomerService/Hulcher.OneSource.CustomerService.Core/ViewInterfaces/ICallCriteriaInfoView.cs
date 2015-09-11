using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Collections;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface ICallCriteriaInfoView : IBaseView
    {
        string Notes { get; }

        void CallCriteriaGroup(bool value);

        /// <summary>
        /// Entity to save at the db
        /// </summary>
        CS_CallCriteria CallCriteriaEntity { get; set; }

        /// <summary>
        /// List of call criteria value to save at the db
        /// </summary>
        IList<CS_CallCriteriaValue> CallCriteriaValueEntityList { get; set; }

        /// <summary>
        /// Filter list for loading new customer, state and city
        /// </summary>
        List<int> FilterList { get; }

        /// <summary>
        /// Set list of all customers
        /// </summary>
        IList<CS_Customer> CustomerList { set; }

        /// <summary>
        /// Set list of all divisions
        /// </summary>
        IList<CS_Division> DivisionList { set; }

        /// <summary>
        /// Set list of all job status
        /// </summary>
        IList<CS_JobStatus> JobSatusList { set; }

        /// <summary>
        /// Set list of all price types
        /// </summary>
        IList<CS_PriceType> PriceTypeList { set; }

        /// <summary>
        /// Set list of all job categories
        /// </summary>
        IList<CS_JobCategory> JobCategoryList { set; }

        /// <summary>
        /// Set list of all job types
        /// </summary>
        IList<CS_JobType> JobTypeList { set; }

        /// <summary>
        /// Set list of all job action
        /// </summary>
        IList<CS_JobAction> JobActionList { set; }

        /// <summary>
        /// Set list of all frequency period
        /// </summary>
        IList<CS_Frequency> FrequencyList { set; }

        /// <summary>
        /// Set list of all countries
        /// </summary>
        IList<CS_Country> CountryList { set; }

        /// <summary>
        /// Set list of all cities
        /// </summary>
        IList<CS_City> CityList { set; }

        /// <summary>
        /// Set list of all States
        /// </summary>
        IList<CS_State> StateList { set; }

        /// <summary>
        /// Get the country value to populate the combo city
        /// </summary>
        int CountryValue { get; }

        ///// <summary>
        ///// Get the state value to populate the city list
        ///// </summary>
        //int StateValue { get; }

        ///// <summary>
        ///// Set the city list by state
        ///// </summary>
        //IList<CS_City> CityListByStateId { set; }

        #region [ Job Call Log ]

        /// <summary>
        /// Sets the Primary Call Type Listing
        /// </summary>
        IList<CS_PrimaryCallType> PrimaryCallTypeList { set; }

        /// <summary>
        /// Gets the current row of the Pimary Call Type Repeater
        /// </summary>
        CS_PrimaryCallType PrimaryCallTypeRepeaterDataItem { get; set; }

        /// <summary>
        /// Sets the current Primary Call Type Description inside the Repeater
        /// </summary>
        string PrimaryCallTypeRepeaterRowDescription { get; set; }

        /// <summary>
        /// Sets the Call Type listing inside the current Primary Call Type
        /// </summary>
        IList<CS_PrimaryCallType_CallType> PrimaryCallTypeRepeaterRowCallTypeList { get; set; }

        #endregion

        #region [ Call Criteria Listing ]

        string Username { get; }

        IList<CS_CallCriteria> CallCriteriaList { get; set; }

        #region [ Data Sources ]

        /// <summary>
        /// Data Source for the Call Criteria Listing Repeater
        /// </summary>
        IList<CS_CallCriteria> CallCriteriaRepeaterDataSource { set; }

        IList<CallCriteriaItemVO> JobAttributesRepeaterDataSource { set; }

        IList<CallCriteriaItemVO> JobCallLogConditionsRepeaterDataSource { set; }

        #endregion

        #region [ Data Items ]

        CS_CallCriteria CallCriteriaRepeaterDataItem { get; set; }

        CallCriteriaItemVO JobAttributesRepeaterDataItem { get; set; }

        CallCriteriaItemVO JobCallLogConditionsRepeaterDataItem { get; set; }

        #endregion

        #region [ Row Attributes - Call Criteria ]

        int CallCriteriaRowId { get; set; }

        string CallCriteriaRowDescription { get; set; }

        string CallCriteriaRowAdviseNotes { get; set; }

        string CallCriteriaRowCommand { get; set; }

        int CallCriteiraRowCommandArgument { get; set; }

        #endregion

        #region [ Row Attributes - Job Attributes ]

        string JobAttributesRowCriteira { get; set; }

        string JobAttributesRowSelected { get; set; }

        #endregion

        #region [ Row Attributes - Job Call Log Conditions ]

        string JobCallLogConditionsRowCriteira { get; set; }

        string JobCallLogConditionsRowSelected { get; set; }

        #endregion

        #endregion

        #region [ Common ]

        int? EmployeeID { get; set; }

        int? ContactID { get; set; }

        #endregion

        bool PanelCallCriteria { set; }

        bool PanelCustomer { set; }
        
        bool PanelDivision { set; }

        Globals.CallCriteria.CallCriteriaLevel SelectedLevel { get; set; }

        int? EditingDivisionID { get; set; }

        int? EditingCustomerID { get; set; }

        string EditingWideName { get; set; }

        void ClearFields();

        bool AddCallCriteriaEnabled { set; }

        bool ShowHidePanelNowRowsCcListing { set; }

        void CallCriteriaEditable(bool p);
    }
}
