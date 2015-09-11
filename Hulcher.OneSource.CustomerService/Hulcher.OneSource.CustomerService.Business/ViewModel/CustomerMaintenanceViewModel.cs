using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class CustomerMaintenanceViewModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Page View. Contains access to Page shared properties.
        /// </summary>
        private ICustomerMaintenanceView _view;

        /// <summary>
        /// Call Criteria Business class
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;


        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default Class constructor
        /// </summary>
        /// <param name="view">Interface View</param>
        public CustomerMaintenanceViewModel(ICustomerMaintenanceView view)
        {
            _view = view;
        }

        /// <summary>
        /// Unit Tests Class constructor
        /// </summary>
        /// <param name="view">Interface View</param>
        /// <param name="callCriteriaModel">Mocked model</param>
        public CustomerMaintenanceViewModel(ICustomerMaintenanceView view, CallCriteriaModel callCriteriaModel)
        {
            _view = view;

            _callCriteriaModel = callCriteriaModel;
        }

        #endregion

        #region [ Methods ]

        #region [ Dashboard ]

        public void BindCustomer()
        {
            CustomerModel customerModel = new CustomerModel();

            //_view.AllContactsList = customerModel.ListAllContacts(_view.FilterType, _view.FilterValue);
            _view.CustomerList = SortCustomer(customerModel.ListAllCustomers(_view.FilterType, _view.FilterValue));
        }

        public void BindContact()
        {
            CustomerModel customerModel = new CustomerModel();

            //_view.AllContactsList = customerModel.ListAllContacts(_view.FilterType, _view.FilterValue);
            //_view.CustomerList = SortCustomer(customerModel.ListAllCustomers(_view.FilterType, _view.FilterValue));
            _view.ContactList = SortContact(customerModel.ListAllContacts(_view.FilterType, _view.FilterValue));
        }

        public void SetCustomerRowFields()
        {
            _view.CustomerRowCustomerID = _view.CustomerRowDataItem.ID;
            _view.CustomerRowCustomerName = _view.CustomerRowDataItem.FullCustomerInformation;
        }

        public void BindContactsByCustomer()
        {
            CustomerModel customerModel = new CustomerModel();
            _view.ContactList = SortContact(_view.AllContactsList.Where(e => e.CS_Customer_Contact.Any(f => f.CustomerID == _view.CustomerRowCustomerID)).ToList());
        }

        public void SetContactRowFields()
        {
            _view.CustomerRowHasContacts = true;

            _view.ContactRowCustomerID = _view.ContactRowDataItem.CS_Customer_Contact.FirstOrDefault().CustomerID;
            _view.ContactRowCustomerName = _view.ContactRowDataItem.CS_Customer_Contact.FirstOrDefault().CS_Customer.FullCustomerInformation;
            _view.ContactRowContactID = _view.ContactRowDataItem.ID;
            _view.ContactRowContactName = _view.ContactRowDataItem.FullName;
            _view.ContactRowType = _view.ContactRowDataItem.Type;
            _view.ContactRowLocation = _view.ContactRowDataItem.Location;
        }

        private IList<CS_Customer> SortCustomer(IList<CS_Customer> customerList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Customer:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return customerList.OrderBy(e => e.FullCustomerInformation).ToList();
                    else
                        return customerList.OrderByDescending(e => e.FullCustomerInformation).ToList();
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Contact:
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Type:
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Location:
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.None:
                default:
                    return customerList.OrderBy(e => e.FullCustomerInformation).ToList();
            }
        }

        private IList<CS_Contact> SortContact(IList<CS_Contact> contactList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Customer:
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Contact:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return contactList.OrderBy(e => e.FullName).ToList();
                    else
                        return contactList.OrderByDescending(e => e.FullName).ToList();
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Type:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return contactList.OrderBy(e => e.Type).ToList();
                    else
                        return contactList.OrderByDescending(e => e.Type).ToList();
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Location:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return contactList.OrderBy(e => e.Location).ToList();
                    else
                        return contactList.OrderByDescending(e => e.Location).ToList();
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.None:
                default:
                    return contactList.OrderBy(e => e.FullName).ToList();
            }
        }

        #endregion

        #region [ Save ]

        #region [ Customer ]

        public void SaveCustomer()
        {
            CustomerModel customerModel = new CustomerModel();

            CS_Customer customer = new CS_Customer()
            {
                Attn = _view.CustomerAttn,
                Name = _view.CustomerName,
                Address1 = _view.CustomerAddress1,
                Address2 = _view.CustomerAddress2,
                State = _view.CustomerState,
                City = _view.CustomerCity,
                Country = _view.CustomerCountry,
                Zip = _view.CustomerZipCode,
                HomePhoneCodeArea = _view.CustomerHomePhoneCodeArea,
                Phone = _view.CustomerHomePhone,
                FaxCodeArea = _view.CustomerFaxPhoneAreaCode,
                Fax = _view.CustomerFaxPhone,
                Email = _view.CustomerEmail,
                BillName = _view.CustomerBillingName,
                BillAddress1 = _view.CustomerBillingAddress1,
                BillAddress2 = _view.CustomerBillingAddress2,
                BillAttn = _view.CustomerBillingAttn,
                BillState = _view.CustomerBillingState,
                BillCity = _view.CustomerBillingCity,
                BillCountry = _view.CustomerBillingCountry,
                BillingHomePhoneCodeArea = _view.CustomerBillingHomePhoneAreaCode,
                BillPhone = _view.CustomerBillingHomePhone,
                BillFaxCodeArea = _view.CustomerBillingFaxPhoneCodeArea,
                BillFax = _view.CustomerBillingFaxPhone,
                BillSalutation = _view.CustomerBillingSalutation,
                BillThruProject = _view.CustomerThruProject,
                BillZip = _view.CustomerBillingZipCode,
                AlertNotification = _view.CustomerAlertNotification,
                OperatorAlert = _view.CustomerOperatorAlert,
                CreditCheck = _view.CustomerCreditCheck,
                IMAddress = _view.CustomerIMAddress,
                Webpage = _view.CustomerWebpage,
                CountryID = 1,
                IsGeneralLog = false,
                CreatedBy = _view.UserName,
                CreationDate = DateTime.Now,
                ModifiedBy = _view.UserName,
                ModificationDate = DateTime.Now,
                Active = true,
                IsCollection = _view.CustomerCollection

            };

            if (_view.CustomerID.HasValue)
            {
                customer.ID = _view.CustomerID.Value;
            }
                    
            _view.SaveMessage = customerModel.SaveCustomer(customer, _view.selectedCustomerSpecificInfoType,_view.UserName);

            if (_view.NewCustomer)
                _view.SavedCustomer = customer;
        }

        #endregion

        #region [ Contact ]

        public void SaveContact()
        {
            CustomerModel contactModel = new CustomerModel();

            List<PhoneNumberVO> numbers = _view.AdditionalContactPhoneList;

            CS_Contact contact = new CS_Contact()
            {
                Attn = _view.ContactAttn,
                Name = _view.ContactName,
                LastName = _view.ContactLastName,
                Alias = _view.ContactAlias,
                Address1 = _view.ContactAddress,
                Address2 = _view.ContactAddress2,
                State = _view.ContactState,
                City = _view.ContactCity,
                Country = _view.ContactCountry,
                Zip = _view.ContactZipcode,
                HomePhoneCodeArea = _view.ContactHomePhoneCodeArea,
                Phone = _view.ContactHomePhone,
                FaxCodeArea = _view.ContactFaxPhoneCodeArea,
                Fax = _view.ContactFaxPhone,
                Email = _view.ContactEmail,
                BillName = _view.ContactBillingName,
                BillAddress1 = _view.ContactBillingAddress,
                BillAddress2 = _view.ContactBillingAddress2,
                BillAttn = _view.ContactBillingAttn,
                BillState = _view.ContactBillingState,
                BillCity = _view.ContactBillingCity,
                BillCountry = _view.ContactBillingCountry,
                BillingHomePhoneCodeArea = _view.ContactBillingHomePhoneCodeArea,
                BillPhone = _view.ContactBillingHomePhone,
                BillFaxCodeArea = _view.ContactBillingFaxPhoneCodeArea,
                BillFax = _view.ContactBillingFaxPhone,
                BillSalutation = _view.ContactBillingSalutation,
                //BillThruProject = _view.ContactBillingThruProject,
                BillZip = _view.ContactBillingZipcode,
                CountryID = 1,
                Webpage = _view.ContactWebpage,
                IMAddress = _view.ContactIMAddress,
                DynamicsContact = _view.ContactType.Value
            };

            if (_view.ContactId.HasValue)
            {
                contact.ID = _view.ContactId.Value;
            }

            _view.SaveMessage = contactModel.SaveContact(_view.ContactCustomerID, contact, numbers, _view.UserName);

            if (_view.NewContact)
                _view.SavedContact = contact;
        }

        #endregion

        #endregion

        #region [ Load ]
     
        #endregion
        #endregion

        #region [ IDisposable ]

        public void Dispose()
        {
            _callCriteriaModel.Dispose();
        }

        #endregion
    }
}
