using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class CustomerMaintenancePresenter
    {
        #region [ Attributes ]

        private ICustomerMaintenanceView _view;

        private CustomerMaintenanceViewModel _customerMaintenanceViewModel;

        private CustomerModel _customerModel;

        private EmployeeModel _employeeModel;

        #endregion

        #region [ Constructor ]

        public CustomerMaintenancePresenter(ICustomerMaintenanceView view)
        {
            _view = view;

            _customerMaintenanceViewModel = new CustomerMaintenanceViewModel(view);

            _customerModel = new CustomerModel();

            _employeeModel = new EmployeeModel();
        }

        #endregion

        #region [ Methods ]

        public void LoadPage()
        {
            try
            {
                if (_view.ViewType == Globals.CustomerMaintenance.ViewType.Error)
                {
                    throw new Exception("Parameter ViewType not found;");
                }

                LoadPhoneTypes();

                if (_view.NewCustomer)
                {
                    LoadScreenForNewCustomer();
                }
                else if (_view.NewContact)
                {
                    LoadScreenForNewContact();

                    _view.EnableCallCriteria = _view.CustomerID.HasValue;

                    if (_view.CustomerID.HasValue)
                        _view.ContactCustomerID = _view.CustomerID.Value;
                    else
                        _view.ContactCustomerID = 0;

                    _view.ContactType = _view.BillToContact;
                }
                else
                {
                    _view.ShowGridSection = true;
                    _view.ShowContactSection = false;
                    _view.ShowCustomerSection = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the page. Please try again.", false);
            }
        }

        /// <summary>
        /// Set the variable validations for the Contact
        /// </summary>
        private void SetValidationsForContact()
        {
            _view.PhoneValidatorValidationGroup = "ContactInformation";
            _view.SaveButtonsValidationGroup = "ContactInformation";
            _view.SaveButtonsOnClientClick = @"CheckPhoneValues('Contact');";
        }

        /// <summary>
        /// Set the variable validations for the Customer
        /// </summary>
        private void SetValidationsForCustomer()
        {
            _view.PhoneValidatorValidationGroup = "CustomerInformation";
            _view.SaveButtonsValidationGroup = "CustomerInformation";
            _view.SaveButtonsOnClientClick = @"CheckPhoneValues('Customer');CheckIfAlreadyExistCustomer('{0}');return false;";
        }

        /// <summary>
        /// Prepare the screen for a new contact
        /// </summary>
        public void LoadScreenForNewContact()
        {
            try
            {
                _view.NewContact = true;
                _view.ShowGridSection = false;
                _view.ShowContactSection = true;
                _view.ShowCustomerSection = false;

                SetValidationsForContact();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the page. Please try again.", false);
            }
        }

        /// <summary>
        /// Prepare the screen for a new customer
        /// </summary>
        public void LoadScreenForNewCustomer()
        {
            try
            {
                _view.ShowGridSection = false;
                _view.ShowCustomerSection = true;
                _view.ShowContactSection = false;

                _view.NewCustomer = true;
                _view.NewContact = false;

                LoadCustomerSpecificInfoType();

                SetValidationsForCustomer();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the page. Please try again.", false);
            }
        }

        public void Cancel()
        {
            try
            {
                _view.ShowGridSection = true;
                _view.ShowCustomerSection = false;
                _view.ShowContactSection = false;
                ClearContacFields();
                ClearCustomerFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while canceling the request!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while canceling the request. Please try again.", false);
            }
        }

        public void Save()
        {
            try
            {
                if (_view.NewCustomer || _view.EditingCustomer)
                {
                    SaveCustomer();
                    _view.NewCustomer = false;
                    _view.EditingCustomer = true;
                    _view.ShowCustomerSection = true;
                }
                else
                {
                    SaveContact();
                    _view.NewContact = false;
                    _view.EditingContact = true;
                    _view.ShowContactSection = true;
                }

                if (_view.SaveAndCloseClicked)
                {
                    _view.DisplayMessage(_view.SaveMessage, true);
                }
                else
                {
                    _view.DisplayMessage(_view.SaveMessage, false);

                    //_view.ShowGridSection = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Save the information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Save the information. Please try again.", false);
            }
        }

        public void SaveContact()
        {
            try
            {
                _customerMaintenanceViewModel.SaveContact();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save the Contact!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save the Contact. Please try again.", false);
            }
        }



        public void SaveCustomer()
        {
            try
            {
                _customerMaintenanceViewModel.SaveCustomer();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save the Customer!\n{0}\n{1}", ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        public void VerifyAccess()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.ManageCallCriteria });

                if (!azOP[0].Result)
                {
                    _view.DisplayMessage("The user does not have access to this functionality", true);
                    _view.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to verify Permissions!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to verify Permissions.", false);
            }
        }

        public void VerifyAccessCollection()
        {

            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.ManageCollection });

                if (azOP[0].Result)
                {
                    _view.CustomerCollectionPermission = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to verify Permissions!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to verify Permissions.", false);
            }
        }

        private void ClearCustomerFields()
        {
            _view.CustomerID = null;
            _view.CustomerName = string.Empty;
            _view.CustomerAttn = string.Empty;
            _view.CustomerAddress1 = string.Empty;
            _view.CustomerAddress2 = string.Empty;
            _view.CustomerState = string.Empty;
            _view.CustomerCity = string.Empty;
            _view.CustomerCountry = string.Empty;
            _view.CustomerZipCode = string.Empty;
            _view.CustomerHomePhoneCodeArea = string.Empty;
            _view.CustomerHomePhone = string.Empty;
            _view.CustomerFaxPhoneAreaCode = string.Empty;
            _view.CustomerFaxPhone = string.Empty;
            _view.CustomerThruProject = null;
            _view.CustomerEmail = string.Empty;
            _view.CustomerWebpage = string.Empty;
            _view.CustomerIMAddress = string.Empty;
            _view.EditingCustomer = false;
            _view.CustomerBillingName = string.Empty;
            _view.CustomerBillingAddress1 = string.Empty;
            _view.CustomerBillingAddress2 = string.Empty;
            _view.CustomerBillingAttn = string.Empty;
            _view.CustomerBillingState = string.Empty;
            _view.CustomerBillingCity = string.Empty;
            _view.CustomerBillingCountry = string.Empty;
            _view.CustomerBillingZipCode = string.Empty;
            _view.CustomerBillingHomePhoneAreaCode = string.Empty;
            _view.CustomerBillingHomePhone = string.Empty;
            _view.CustomerBillingFaxPhoneCodeArea = string.Empty;
            _view.CustomerBillingFaxPhone = string.Empty;
            _view.CustomerBillingSalutation = string.Empty;
        }

        public void LoadCustomerInfo()
        {
            if (_view.CustomerID.HasValue)
            {
                CS_Customer selectedCustomer = _customerModel.GetCustomerById(_view.CustomerID.Value);
                IList<CS_Request> resquests = selectedCustomer.CS_Request.Where(e => e.Status == (int)Globals.CustomerMaintenance.RequestStatus.Pending && e.Active).ToList();
                _view.CustomerName = selectedCustomer.Name;
                _view.CustomerAttn = selectedCustomer.Attn;
                _view.CustomerAddress1 = selectedCustomer.Address1;
                _view.CustomerAddress2 = selectedCustomer.Address2;
                _view.CustomerState = selectedCustomer.State;
                _view.CustomerCity = selectedCustomer.City;
                _view.CustomerCountry = selectedCustomer.Country;
                _view.CustomerZipCode = selectedCustomer.Zip;
                _view.CustomerHomePhoneCodeArea = selectedCustomer.HomePhoneCodeArea;
                _view.CustomerHomePhone = selectedCustomer.CustomerPhoneNumberEdited;
                _view.CustomerFaxPhoneAreaCode = selectedCustomer.FaxCodeArea;
                _view.CustomerFaxPhone = selectedCustomer.CustomerFaxNumberEdited;
                _view.CustomerThruProject = selectedCustomer.BillThruProject;
                _view.CustomerEmail = selectedCustomer.Email;
                _view.CustomerWebpage = selectedCustomer.Webpage;
                _view.CustomerIMAddress = selectedCustomer.IMAddress;
                _view.CustomerAlertNotification = selectedCustomer.AlertNotification;
                _view.CustomerCreditCheck = selectedCustomer.CreditCheck;
                _view.CustomerRequestWarning = resquests.Count > 0;
                _view.CustomerCollection = selectedCustomer.IsCollection;

                if (!string.IsNullOrEmpty(selectedCustomer.Xml))
                    _view.selectedCustomerSpecificInfoType = _customerModel.GetSelectedCustomerSpecificInfoType(selectedCustomer.Xml);

                if (selectedCustomer.OperatorAlert.HasValue)
                    _view.CustomerOperatorAlert = selectedCustomer.OperatorAlert.Value;

                LoadCustomerBillingInformation(selectedCustomer);

                _view.EditingCustomer = true;
                _view.EditingContact = false;
            }
        }

        private void LoadCustomerBillingInformation(CS_Customer selectedCustomer)
        {
            _view.CustomerBillingName = selectedCustomer.BillName;
            _view.CustomerBillingAddress1 = selectedCustomer.BillAddress1;
            _view.CustomerBillingAddress2 = selectedCustomer.BillAddress2;
            _view.CustomerBillingAttn = selectedCustomer.BillAttn;
            _view.CustomerBillingState = selectedCustomer.BillState;
            _view.CustomerBillingCity = selectedCustomer.BillCity;
            _view.CustomerBillingCountry = selectedCustomer.BillCountry;
            _view.CustomerBillingZipCode = selectedCustomer.BillZip;
            _view.CustomerBillingHomePhoneAreaCode = selectedCustomer.BillingHomePhoneCodeArea;
            _view.CustomerBillingHomePhone = selectedCustomer.BillingCustomerPhoneNumberEdited;
            _view.CustomerBillingFaxPhoneCodeArea = selectedCustomer.BillFaxCodeArea;
            _view.CustomerBillingFaxPhone = selectedCustomer.BillingCustomerFaxNumberEdited;
            _view.CustomerBillingSalutation = selectedCustomer.BillSalutation;
        }

        private void ClearContacFields()
        {
            _view.ContactId = null;
            _view.ContactCustomerID = 0;
            _view.ContactName = string.Empty;
            _view.ContactLastName = string.Empty;
            _view.ContactNumber = string.Empty;
            _view.ContactAttn = string.Empty;
            _view.ContactAddress = string.Empty;
            _view.ContactAddress2 = string.Empty;
            _view.ContactState = string.Empty;
            _view.ContactCity = string.Empty;
            _view.ContactCountry = string.Empty;
            _view.ContactZipcode = string.Empty;
            _view.ContactHomePhoneCodeArea = string.Empty;
            
            _view.ContactFaxPhoneCodeArea = string.Empty;
            _view.ContactFaxPhone = string.Empty;
            _view.ContactEmail = string.Empty;
            _view.ContactWebpage = string.Empty;
            _view.ContactIMAddress = string.Empty;
            _view.ContactType = null;
            _view.AdditionalContactPhoneGridDataSource = new List<CS_PhoneNumber>();
            _view.EditingContact = false;
            _view.ContactBillingName = string.Empty;
            _view.ContactBillingAddress = string.Empty;
            _view.ContactBillingAddress2 = string.Empty;
            _view.ContactBillingAttn = string.Empty;
            _view.ContactBillingState = string.Empty;
            _view.ContactBillingCity = string.Empty;
            _view.ContactBillingCountry = string.Empty;
            _view.ContactBillingZipcode = string.Empty;
            _view.ContactBillingHomePhoneCodeArea = string.Empty;
            _view.ContactBillingHomePhone = string.Empty;
            _view.ContactBillingFaxPhoneCodeArea = string.Empty;
            _view.ContactBillingFaxPhone = string.Empty;
            _view.ContactBillingSalutation = string.Empty;
            //_view.ContactBillingThruProject = null;
        }

        public void LoadContactInformation()
        {
            if (_view.ContactId.HasValue)
            {
                CS_Contact selectedContact = _customerModel.GetContactById(_view.ContactId.Value);
                IList<CS_Request> resquests = selectedContact.CS_Request.Where(e => e.Status == (int)Globals.CustomerMaintenance.RequestStatus.Pending && e.Active).ToList();

                _view.ContactId = selectedContact.ID;

                _view.ContactName = selectedContact.Name;
                _view.ContactLastName = selectedContact.LastName;
                _view.ContactAlias = selectedContact.Alias;
                _view.ContactNumber = selectedContact.ContactNumber;
                _view.ContactAttn = selectedContact.Attn;
                _view.ContactAddress = selectedContact.Address1;
                _view.ContactAddress2 = selectedContact.Address2;
                _view.ContactState = selectedContact.State;
                _view.ContactCity = selectedContact.City;
                _view.ContactCountry = selectedContact.Country;
                _view.ContactZipcode = selectedContact.Zip;
                _view.ContactHomePhoneCodeArea = selectedContact.HomePhoneCodeArea;
                _view.ContactHomePhone = selectedContact.ContactEditingPhoneNumber;
                _view.ContactFaxPhoneCodeArea = selectedContact.FaxCodeArea;
                _view.ContactFaxPhone = selectedContact.ContactEditingFaxNumber;
                _view.ContactEmail = selectedContact.Email;
                _view.ContactWebpage = selectedContact.Webpage;
                _view.ContactIMAddress = selectedContact.IMAddress;
                _view.ContactType = selectedContact.DynamicsContact;
                
                IList<CS_PhoneNumber> lstPhoneNumber = _customerModel.GetAdditionalPhonesByContact(_view.ContactId.Value);
                if (!string.IsNullOrEmpty(selectedContact.ContactEditingPhoneNumber))
                {
                    CS_PhoneNumber pn = new CS_PhoneNumber();
                    pn.CS_PhoneType = new CS_PhoneType();
                    pn.Number = selectedContact.ContactEditingPhoneNumber;
                    pn.CS_PhoneType.Name = "Work";
                    lstPhoneNumber.Add(pn);
                }

                if (!string.IsNullOrEmpty(selectedContact.ContactEditingFaxNumber))
                {
                    CS_PhoneNumber pn = new CS_PhoneNumber();
                    pn.CS_PhoneType = new CS_PhoneType();
                    pn.Number = selectedContact.ContactEditingFaxNumber;
                    pn.CS_PhoneType.Name = "Fax";
                    lstPhoneNumber.Add(pn);
                }

                _view.AdditionalContactPhoneGridDataSource = lstPhoneNumber;

                _view.ContactType = selectedContact.DynamicsContact;
                _view.ContactRequestWarning = resquests.Count > 0;
                LoadContactBillinfInfo(selectedContact);
                _view.CallCriteriaContactID = _view.ContactId;

                _view.EditingCustomer = false;
                _view.EditingContact = true;
            }
            else
                _view.CallCriteriaContactID = null;
        }

        private void LoadContactBillinfInfo(CS_Contact selectedContact)
        {
            _view.ContactBillingName = selectedContact.BillName;
            _view.ContactBillingAddress = selectedContact.BillAddress1;
            _view.ContactBillingAddress2 = selectedContact.BillAddress2;
            _view.ContactBillingAttn = selectedContact.BillAttn;
            _view.ContactBillingState = selectedContact.BillState;
            _view.ContactBillingCity = selectedContact.BillCity;
            _view.ContactBillingCountry = selectedContact.BillCountry;
            _view.ContactBillingZipcode = selectedContact.BillZip;
            _view.ContactBillingHomePhoneCodeArea = selectedContact.BillingHomePhoneCodeArea;
            _view.ContactBillingHomePhone = selectedContact.BillingContactPhoneNumber;
            _view.ContactBillingFaxPhoneCodeArea = selectedContact.BillFaxCodeArea;
            _view.ContactBillingFaxPhone = selectedContact.BillingContactFaxPhone;
            _view.ContactBillingSalutation = selectedContact.BillSalutation;
        }

        public void LoadPhoneTypes()
        {
            _view.AdditionalContactPhoneTypeSource = _employeeModel.LoadPhoneTypes();
        }

        public void LoadCustomerSpecificInfoType()
        {
            _view.customerSpecificInfoTypeList = _customerModel.ListAllCustomerSpecificInfoType();
        }

        #region [ Dashboard ]

        public void BindGrid()
        {
            try
            {
                if (_view.FilterType != Globals.CustomerMaintenance.FilterType.None && !string.IsNullOrEmpty(_view.FilterValue) && _view.ViewType == Globals.CustomerMaintenance.ViewType.Customer)
                    _customerMaintenanceViewModel.BindCustomer();

                if (_view.FilterType != Globals.CustomerMaintenance.FilterType.None && !string.IsNullOrEmpty(_view.FilterValue) && _view.ViewType == Globals.CustomerMaintenance.ViewType.Contact)
                    _customerMaintenanceViewModel.BindContact();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load the customer information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load the Company information. Please try again.", false);
            }
        }

        public void BindCustomerRow()
        {
            try
            {
                _customerMaintenanceViewModel.SetCustomerRowFields();
                //_customerMaintenanceViewModel.BindContactsByCustomer();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load the customer information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load the Company information. Please try again.", false);
            }
        }

        public void BindContactRow()
        {
            try
            {
                _customerMaintenanceViewModel.SetContactRowFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load the contact information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load the contact information. Please try again.", false);
            }
        }

        public void CustomerItemCommand()
        {
            if (_view.ItemCommandName == "EditCustomer")
            {
                _view.ShowGridSection = false;
                _view.ShowCustomerSection = true;
                _view.ShowContactSection = false;

                LoadCustomerSpecificInfoType();

                LoadCustomerInfo();
                SetValidationsForCustomer();
            }
            else
            {
                _view.ShowGridSection = false;
                _view.ShowContactSection = true;
                _view.ShowCustomerSection = false;

                _view.NewCustomer = false;
                _view.NewContact = true;
                SetValidationsForContact();
            }
        }

        public void ContactItemCommand()
        {
            _view.ShowGridSection = false;
            _view.ShowContactSection = true;
            _view.ShowCustomerSection = false;

            LoadContactInformation();
            SetValidationsForContact();
        }

        #endregion

        #endregion
    }
}
