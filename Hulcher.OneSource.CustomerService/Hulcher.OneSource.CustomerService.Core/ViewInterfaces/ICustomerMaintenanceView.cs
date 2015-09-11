using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface ICustomerMaintenanceView : IBaseView
    {
        #region [ Common ]

        Globals.CustomerMaintenance.ViewType ViewType { get; }

        bool ExistingCustomer { get; set; }

        bool NewCustomer { get; set; }

        bool NewContact { get; set; }

        bool EditingContact { get; set; }

        bool EditingCustomer { get; set; }

        bool ShowCustomerSection { set; }

        bool ShowContactSection { set; }

        bool ShowGridSection { set; }

        string PhoneValidatorValidationGroup { set; }

        string SaveButtonsValidationGroup { set; }

        string SaveButtonsOnClientClick { set; }

        bool SaveAndCloseClicked { get; set; }

        string SaveMessage { get; set; }

        CS_Customer SavedCustomer { set; }

        CS_Contact SavedContact { set; }

        bool? BillToContact { get; }

        #endregion

        #region [ Call Criteria ]

        /// <summary>
        /// Get the Username from MasterPage
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the Domain of the current user
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Sets the page to be read-only (when the user has no permission)
        /// </summary>
        bool ReadOnly { set; }

        bool EnableCallCriteria { set; }

        #endregion

        #region [ Sort ]

        string[] OrderBy { get; }

        Globals.Common.Sort.CustomerMaintenanceSortColumns SortColumn { get; }

        Globals.Common.Sort.SortDirection SortDirection { get; }

        #endregion

        #region [ Dashboard ]

        Globals.CustomerMaintenance.FilterType FilterType { get; }

        string FilterValue { get; }

        IList<CS_Customer> CustomerList { get; set; }

        IList<CS_Contact> AllContactsList { get; set; }

        IList<CS_Contact> ContactList { get; set; }

        #region [ Customer ]

        CS_Customer CustomerRowDataItem { get; set; }

        #region [ Fields ]

        string CustomerRowCustomerName { get; set; }

        int CustomerRowCustomerID { get; set; }

        bool CustomerRowHasContacts { get; set; }

        #endregion

        #endregion

        #region [ Contact ]

        CS_Contact ContactRowDataItem { get; set; }

        #region [ Fields ]

        int ContactRowCustomerID { get; set; }

        string ContactRowCustomerName { get; set; }

        string ContactRowContactName { get; set; }

        int ContactRowContactID { get; set; }

        string ContactRowType { get; set; }

        string ContactRowLocation { get; set; }

        #endregion

        #endregion

        #region [ Commands ]

        string ItemCommandName { get; set; }

        #endregion

        #endregion

        #region [ Form ]

        #region [ Customer ]

        int? CustomerID {get;set;}

        string CustomerName { get; set; }

        string CustomerAttn { get; set; }

        string CustomerAddress1 { get; set; }

        string CustomerAddress2 { get; set; }

        string CustomerState { get; set; }

        string CustomerCity { get; set; }

        string CustomerCountry { get; set; }

        string CustomerZipCode { get; set; }

        string CustomerHomePhoneCodeArea { get; set; }

        string CustomerHomePhone { get; set; }

        string CustomerFaxPhoneAreaCode { get; set; }

        string CustomerFaxPhone { get; set; }

        string CustomerBillingName { get; set; }

        string CustomerBillingAddress1 { get; set; }

        string CustomerBillingAddress2 { get; set; }

        string CustomerBillingAttn { get; set; }

        string CustomerBillingState { get; set; }

        string CustomerBillingCity { get; set; }

        string CustomerBillingCountry { get; set; }

        string CustomerBillingZipCode { get; set; }
       
        string CustomerBillingHomePhoneAreaCode { get; set; }

        string CustomerBillingHomePhone { get; set; }

        string CustomerBillingFaxPhoneCodeArea { get; set; }

        string CustomerBillingFaxPhone { get; set; }

        string CustomerBillingSalutation { get; set; }

        short? CustomerThruProject { get; set; }

        string CustomerEmail { get; set; }

        string CustomerWebpage { get; set; }

        string CustomerIMAddress { get; set; }

        string CustomerAlertNotification { get; set; }

        bool CustomerOperatorAlert { get; set; }

        bool CustomerCreditCheck { get; set; }

        bool CustomerRequestWarning { set; }

        bool CustomerCollection { get; set; }

        IList<int> selectedCustomerSpecificInfoType { get; set; }

        IList<CS_CustomerSpecificInfoType> customerSpecificInfoTypeList { get; set; }

        bool CustomerCollectionPermission { set; }

        #endregion

        #region [ Contact ]

        int? ContactId { get; set; }

        int ContactCustomerID { get; set; }
        
        /// <summary>
        /// Additional Contact Phone Type Source
        /// </summary>
        List<CS_PhoneType> AdditionalContactPhoneTypeSource { set; }

        bool? ContactType { get; set; }
        
        string ContactName { get; set; }

        string ContactLastName { get; set; }

        string ContactAlias { get; set; }

        string ContactNumber { get; set; }

        string ContactAttn { get; set; }

        string ContactAddress { get; set; }

        string ContactAddress2 { get; set; }

        string ContactState { get; set; }

        string ContactCity { get; set; }

        string ContactCountry { get; set; }

        string ContactZipcode { get; set; }    

        string ContactHomePhoneCodeArea { get; set; }

        string ContactHomePhone { get; set; }

        string ContactFaxPhoneCodeArea { get; set; }

        string ContactFaxPhone { get; set; }

        string ContactBillingName { get; set; }

        string ContactBillingAddress { get; set; }

        string ContactBillingAddress2 { get; set; }

        string ContactBillingAttn { get; set; }

        string ContactBillingState { get; set; }

        string ContactBillingCity { get; set; }

        string ContactBillingCountry { get; set; }

        string ContactBillingZipcode { get; set; }

        string ContactBillingHomePhoneCodeArea { get; set; }

        string ContactBillingHomePhone { get; set; }

        string ContactBillingFaxPhoneCodeArea { get; set; }

        string ContactBillingFaxPhone { get; set; }

        string ContactBillingSalutation { get; set; }

        //short? ContactBillingThruProject { get; set; }

        string ContactEmail { get; set; }

        string ContactWebpage { get; set; }

        string ContactIMAddress { get; set; }

        bool ContactRequestWarning { set; }

        IList<CS_PhoneNumber> AdditionalContactPhoneGridDataSource { set; }

        List<PhoneNumberVO> AdditionalContactPhoneList { get; }

        int? CallCriteriaContactID { get; set; }

        #endregion

        #endregion
    }
}
