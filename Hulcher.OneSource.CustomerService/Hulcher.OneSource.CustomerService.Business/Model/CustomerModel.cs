using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Integration;
using Hulcher.OneSource.CustomerService.Integration.Entities;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Transactions;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;


namespace Hulcher.OneSource.CustomerService.Business.Model
{
    /// <summary>
    /// Business Class for Contact records
    /// </summary>
    public class CustomerModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Customer
        /// </summary>
        private IRepository<CS_Customer> _customerRepository;

        /// <summary>
        /// Repository class for CS_Contact
        /// </summary>
        private IRepository<CS_Contact> _contactRepository;

        /// <summary>
        /// Repository class for CS_CustomerContract
        /// </summary>
        private IRepository<CS_CustomerContract> _contractRepository;

        /// <summary>
        /// Repository class for CS_CustomerInfo
        /// </summary>
        private IRepository<CS_CustomerInfo> _customerInfoRepository;

        /// <summary>
        /// Repository class for CS_Customer_Contact
        /// </summary>
        private IRepository<CS_Customer_Contact> _customerContactRepository;

        /// <summary>
        /// Repository class for CS_CallCriteria
        /// </summary>
        private IRepository<CS_CallCriteria> _callCriteriaRepository;

        /// <summary>
        /// Repository class for CS_CallCriteriaValue
        /// </summary>
        private IRepository<CS_CallCriteriaValue> _callCriteriaValueRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for CS_Request
        /// </summary>
        private IRepository<CS_Request> _requestRepository;

        /// <summary>
        /// Repository class for CS_PhoneNumber
        /// </summary>
        private IRepository<CS_PhoneNumber> _phoneNumberRepository;

        /// <summary>
        /// Repository class for CS_CustomerSpecificInfoType
        /// </summary>
        private IRepository<CS_CustomerSpecificInfoType> _customerSpecificInfoTypeRepository;

        /// <summary>
        /// Instance for CallCriteriaModel class
        /// </summary>
        CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Repository class for CS_View_Customer_Contact_Phone
        /// </summary>
        private IRepository<CS_View_Customer_Contact_Phone> _customerContactPhoneRepository;

        #endregion

        #region [ Constructors ]

        public CustomerModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _customerRepository = new EFRepository<CS_Customer>();
            _customerRepository.UnitOfWork = _unitOfWork;

            _contactRepository = new EFRepository<CS_Contact>();
            _contactRepository.UnitOfWork = _unitOfWork;

            _contractRepository = new EFRepository<CS_CustomerContract>();
            _contractRepository.UnitOfWork = _unitOfWork;

            _customerInfoRepository = new EFRepository<CS_CustomerInfo>();
            _customerInfoRepository.UnitOfWork = _unitOfWork;

            _customerContactRepository = new EFRepository<CS_Customer_Contact>();
            _customerContactRepository.UnitOfWork = _unitOfWork;

            _callCriteriaRepository = new EFRepository<CS_CallCriteria>();
            _callCriteriaRepository.UnitOfWork = _unitOfWork;

            _callCriteriaValueRepository = new EFRepository<CS_CallCriteriaValue>();
            _callCriteriaValueRepository.UnitOfWork = _unitOfWork;

            _callLogResourceRepository = new EFRepository<CS_CallLogResource>();
            _callLogResourceRepository.UnitOfWork = _unitOfWork;

            _requestRepository = new EFRepository<CS_Request>();
            _requestRepository.UnitOfWork = _unitOfWork;

            _phoneNumberRepository = new EFRepository<CS_PhoneNumber>();
            _phoneNumberRepository.UnitOfWork = _unitOfWork;

            _customerSpecificInfoTypeRepository = new EFRepository<CS_CustomerSpecificInfoType>();
            _customerSpecificInfoTypeRepository.UnitOfWork = _unitOfWork;

            _customerContactPhoneRepository = new EFRepository<CS_View_Customer_Contact_Phone>();
            _customerContactPhoneRepository.UnitOfWork = _unitOfWork;

            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
        }

        public CustomerModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _customerRepository = new EFRepository<CS_Customer>();
            _customerRepository.UnitOfWork = _unitOfWork;

            _contactRepository = new EFRepository<CS_Contact>();
            _contactRepository.UnitOfWork = _unitOfWork;

            _contractRepository = new EFRepository<CS_CustomerContract>();
            _contractRepository.UnitOfWork = _unitOfWork;

            _customerInfoRepository = new EFRepository<CS_CustomerInfo>();
            _customerInfoRepository.UnitOfWork = _unitOfWork;

            _callCriteriaRepository = new EFRepository<CS_CallCriteria>();
            _callCriteriaRepository.UnitOfWork = _unitOfWork;

            _callCriteriaValueRepository = new EFRepository<CS_CallCriteriaValue>();
            _callCriteriaValueRepository.UnitOfWork = _unitOfWork;

            _customerContactRepository = new EFRepository<CS_Customer_Contact>();
            _customerContactRepository.UnitOfWork = _unitOfWork;

            _callLogResourceRepository = new EFRepository<CS_CallLogResource>();
            _callLogResourceRepository.UnitOfWork = _unitOfWork;

            _requestRepository = new EFRepository<CS_Request>();
            _requestRepository.UnitOfWork = _unitOfWork;

            _phoneNumberRepository = new EFRepository<CS_PhoneNumber>();
            _phoneNumberRepository.UnitOfWork = _unitOfWork;

            _customerSpecificInfoTypeRepository = new EFRepository<CS_CustomerSpecificInfoType>();
            _customerSpecificInfoTypeRepository.UnitOfWork = _unitOfWork;

            _customerContactPhoneRepository = new EFRepository<CS_View_Customer_Contact_Phone>();
            _customerContactPhoneRepository.UnitOfWork = _unitOfWork;

            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
        }

        #endregion

        #region [ Methods ]

        #region [ Customer ]

        #region [ Listing ]

        /// <summary>
        /// Check if the customer is already on the database
        /// </summary>
        /// <param name="customerName">customer name</param>
        /// <returns>bool</returns>
        public bool GetCustomerByName(string customerName)
        {
            CS_Customer customer = _customerRepository.Get(w => w.Active && w.Name.Trim().ToLower() == customerName.Trim().ToLower());

            if (null != customer)
                return true;

            return false;
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Customer> ListAllCustomers()
        {
            return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Customer> ListAllCustomers(Globals.CustomerMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.CustomerMaintenance.FilterType.None:
                    return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false);
                case Globals.CustomerMaintenance.FilterType.Customer:
                    return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false
                                                            && arrValue.Any(g => (e.Name + " - " + e.Country + " - " + e.Attn + " - " + (string.IsNullOrEmpty(e.CustomerNumber) ? "" : e.CustomerNumber)).Contains(g)));
                case Globals.CustomerMaintenance.FilterType.Contact:
                    return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false
                                                            && e.CS_Customer_Contact.Any(f => arrValue.Any(g => (f.CS_Contact.LastName + ", " + f.CS_Contact.Name).Contains(g))));
                case Globals.CustomerMaintenance.FilterType.Location:
                    return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false
                                                            && e.CS_Customer_Contact.Any(f => arrValue.Any(g => (f.CS_Contact.Country + ", " + f.CS_Contact.State + ", " + f.CS_Contact.City).Contains(g))));
                default:
                    return new List<CS_Customer>();
            }
        }

        /// <summary>
        /// Returns the General Log fictional Customer Entity
        /// </summary>
        /// <returns>Entity that represents the General Log fictional Customer</returns>
        public CS_Customer GetGeneralLogCustomer()
        {
            return _customerRepository.Get(e => e.IsGeneralLog);
        }

        /// <summary>
        /// Gets customer information based on a Customer Identifier
        /// </summary>
        /// <param name="customerId">Customer Identifier</param>
        /// <returns>customer information</returns>
        public CS_Customer GetCustomerById(int customerId)
        {
            return _customerRepository.Get(e => e.ID == customerId && e.Active);
        }

        public IList<CS_Customer> ListCustomerByName(string name)
        {
            return _customerRepository.ListAll(
                e => e.Active &&
                    e.IsGeneralLog == false &&
                    (
                        (
                            e.Attn.Trim() != "" &&
                            (e.Name.Trim() + " - " + (e.Country == null ? "" : e.Country) + " - " + (e.Attn == null ? "" : e.Attn.Trim()) + " - " + (e.CustomerNumber == null ? "" : e.CustomerNumber.Trim())).Contains(name)) ||
                            ((e.Attn == null || e.Attn.Trim() == "") && (e.Name.Trim() + " - " + (e.Country == null ? "" : e.Country) + " - " + (e.CustomerNumber == null ? "" : e.CustomerNumber.Trim())).Contains(name))
                        )
                    ).ToList();
        }

        /// <summary>
        /// Get a single customer for a specific contact
        /// </summary>
        /// <param name="contactId">Contact Identifier</param>
        /// <returns>Customer List</returns>
        public CS_Customer GetCustomerByContact(int contactId)
        {
            return _customerRepository.ListAll(e => e.CS_Customer_Contact.Any(w => w.CS_Contact.ID == contactId)).FirstOrDefault();
        }

        /// <summary>
        /// Returns All Customer from the given id list
        /// </summary>
        /// <param name="list">Customer identifier list</param>
        /// <returns>CS_Customer List</returns>
        public IList<CS_Customer> ListCustomerByIds(List<int> customerIds)
        {
            return _customerRepository.ListAll(e => e.Active && e.IsGeneralLog == false && customerIds.Any(f => f == e.ID));
        }

        /// <summary>
        /// Returns the customer associated with the given JobID
        /// </summary>
        /// <param name="jobId">ID of the Job</param>
        /// <returns>CS_Customer entity</returns>
        public CS_Customer GetCustomerByJobId(int jobId)
        {
            return _customerRepository.Get(e => e.ID == e.CS_CustomerInfo.FirstOrDefault(f => f.JobId == jobId).CustomerId);
        }

        public IList<CustomerPhoneVO> ParseEntityToCustomerPhone(IList<CS_View_Customer_Contact_Phone> phoneList)
        {
            IList<CustomerPhoneVO> parsedList = new List<CustomerPhoneVO>();
            for (int i = 0; i < phoneList.Count; i++)
            {
                if (phoneList[i].CustomerHomePhone != null && phoneList[i].CustomerHomePhone.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Home",
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].FormatedCustomerHomePhone
                    });
                }
                if (phoneList[i].CustomerBillPhone != null && phoneList[i].CustomerBillPhone.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Bill Phone",
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].FormatedCustomerBillPhone
                    });
                }
                if (phoneList[i].CustomerFax != null && phoneList[i].CustomerFax.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Fax",
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].FormatedCustomerFax
                    });
                }
                if (phoneList[i].CustomerBillFax != null && phoneList[i].CustomerBillFax.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Bill Fax",
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].FormatedCustomerBillFax
                    });
                }

                string contactName = string.Empty;
                if (phoneList[i].ContactDynamics)
                    contactName = phoneList[i].ContactAttn.Trim();
                else
                    contactName = string.Format("{0}, {1}", phoneList[i].ContactLastName.Trim(), phoneList[i].ContactName.Trim());
                if (!string.IsNullOrEmpty(phoneList[i].ContactAlias))
                    contactName += string.Format(" \"{0}\"", phoneList[i].ContactAlias.Trim());

                if (phoneList[i].ContactHomePhone != null && phoneList[i].ContactHomePhone.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Home",
                        ContactName = contactName,
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].ContactHomePhoneCodeArea + " " + phoneList[i].ContactHomePhone
                    });
                }
                if (phoneList[i].ContactAdditionalNumber != null && phoneList[i].ContactAdditionalNumber.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = phoneList[i].CustomerAdditionalPhoneType,
                        ContactName = contactName,
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].ContactAdditionalNumber
                    });
                }
                if (phoneList[i].ContactFax != null && phoneList[i].ContactFax.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Fax",
                        ContactName = contactName,
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].ContactFaxCodeArea + " " + phoneList[i].ContactFax
                    });
                }
                if (phoneList[i].ContactBillPhone != null && phoneList[i].ContactBillPhone.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Bill Phone",
                        ContactName = contactName,
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].ContactBillPhoneCodeArea + " " + phoneList[i].ContactBillPhone
                    });
                }
                if (phoneList[i].ContactBillFax != null && phoneList[i].ContactBillFax.Trim() != string.Empty)
                {
                    parsedList.Add(new CustomerPhoneVO()
                    {
                        PhoneType = "Bill Fax",
                        ContactName = contactName,
                        CustomerName = phoneList[i].CustomerName,
                        CustomerNotes = phoneList[i].CustomerNotes,
                        CustomerNumber = phoneList[i].CustomerNumber,
                        PhoneNumber = phoneList[i].ContactBillFaxCodeArea + " " + phoneList[i].ContactBillFax
                    });
                }
            }
            return parsedList;
        }

        public IList<CS_View_Customer_Contact_Phone> ListCustomerContactPhoneInformation(Globals.CustomerMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            if (filterType == Globals.CustomerMaintenance.FilterType.Customer)
                return _customerContactPhoneRepository.ListAll(
                    e => arrValue.Any(f =>
                        e.ContactLastName.Contains(f) ||
                        e.ContactName.Contains(f) ||
                        e.ContactAlias.Contains(f) ||
                        e.ContactAttn.Contains(f) ||
                        e.CustomerName.Contains(f)
                        ));
            else return null;
        }

        #endregion

        #region [ Save ]

        public void SaveNewCustomerRequest(CS_Customer newCustomer, ref string note)
        {
            try
            {
                CS_Request request = new CS_Request()
                {
                    CustomerID = newCustomer.ID,
                    RequestDate = DateTime.Now,
                    Note = note,
                    Status = (int)Globals.CustomerMaintenance.RequestStatus.Pending,
                    ApprovedDate = null,
                    IsCustomer = true,
                    CreatedBy = newCustomer.ModifiedBy,
                    CreationDate = newCustomer.ModificationDate,
                    CreationID = newCustomer.ModificationID,
                    ModifiedBy = newCustomer.ModifiedBy,
                    ModificationDate = newCustomer.ModificationDate,
                    ModificationID = newCustomer.ModificationID,
                    Active = true
                };

                _requestRepository.Add(request);

                note = note.Replace("@@TemporaryID", newCustomer.ID.ToString() + "-" + request.ID.ToString());

                request.Note = note;
                _requestRepository.Update(request);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to save customer request information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save company request information!", ex);
            }
        }

        public string SaveCustomer(CS_Customer newCustomer, IList<int> selectedCustomerSpecificInfoType, string userName)
        {
            try
            {
                string returnMessage = "Company Profile saved successfully.";

                using (TransactionScope scope = new TransactionScope())
                {
                    if (newCustomer.ID == 0)
                    {
                        SaveNewCustomer(newCustomer, selectedCustomerSpecificInfoType, userName);
                    }
                    else
                    {
                        returnMessage = UpdateCustomer(newCustomer, selectedCustomerSpecificInfoType, userName);
                    }

                    scope.Complete();
                }

                return returnMessage;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to save customer information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save company information!", ex);
            }
        }

        public void SaveNewCustomer(CS_Customer newCustomer, IList<int> selectedCustomerSpecificInfoType, string userName)
        {
            try
            {
                if (null != newCustomer)
                {
                    DynamicFieldsAggregator aggregator = CreateAggregatorForSpecificCustomerInfo(selectedCustomerSpecificInfoType);

                    if (aggregator.Controls.Count > 0)
                        newCustomer.Xml = GetXmlFromAggregator(aggregator);

                    newCustomer = _customerRepository.Add(newCustomer);

                    string emailBody = GenerateCustomerRequestEmailBody(newCustomer, false);

                    SaveNewCustomerRequest(newCustomer, ref emailBody);

                    SendCustomerRequestEmail(emailBody, false, false, null, userName);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to save customer information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save company information!", ex);
            }
        }

        public string UpdateCustomer(CS_Customer newCustomer, IList<int> selectedCustomerSpecificInfoType, string userName)
        {
            try
            {
                Globals.CustomerMaintenance.CS_Customer_Comparer comparer = new Globals.CustomerMaintenance.CS_Customer_Comparer();
                CS_Customer oldCustomer = _customerRepository.Get(e => e.ID == newCustomer.ID);
                string returnMessage = "The Company Profile has been updated.";

                if (null != oldCustomer)
                {
                    CS_Customer updateCustomer = new CS_Customer()
                       {
                           ID = oldCustomer.ID,
                           Attn = oldCustomer.Attn,
                           CustomerNumber = oldCustomer.CustomerNumber,
                           Name = oldCustomer.Name,
                           Address1 = oldCustomer.Address1,
                           Address2 = oldCustomer.Address2,
                           State = oldCustomer.State,
                           City = oldCustomer.City,
                           Country = oldCustomer.Country,
                           Zip = oldCustomer.Zip,
                           Phone = newCustomer.Phone,
                           HomePhoneCodeArea = oldCustomer.HomePhoneCodeArea,
                           Fax = newCustomer.Fax,
                           FaxCodeArea = oldCustomer.FaxCodeArea,
                           Email = oldCustomer.Email,
                           BillName = oldCustomer.BillName,
                           BillAddress1 = oldCustomer.BillAddress1,
                           BillAddress2 = oldCustomer.BillAddress2,
                           BillAttn = oldCustomer.BillAttn,
                           BillState = oldCustomer.BillState,
                           BillCity = oldCustomer.BillCity,
                           BillCountry = oldCustomer.BillCountry,
                           BillPhone = newCustomer.BillPhone,
                           BillingHomePhoneCodeArea = oldCustomer.BillingHomePhoneCodeArea,
                           BillFax = newCustomer.BillFax,
                           BillFaxCodeArea = oldCustomer.BillFaxCodeArea,
                           BillSalutation = oldCustomer.BillSalutation,
                           BillThruProject = oldCustomer.BillThruProject,
                           BillZip = oldCustomer.BillZip,
                           CountryID = oldCustomer.CountryID,
                           Webpage = oldCustomer.Webpage,
                           IMAddress = oldCustomer.IMAddress,
                           AlertNotification = newCustomer.AlertNotification,
                           OperatorAlert = newCustomer.OperatorAlert,
                           CreditCheck = newCustomer.CreditCheck,
                           Checksum = oldCustomer.Checksum,
                           IsGeneralLog = oldCustomer.IsGeneralLog,
                           CreatedBy = oldCustomer.CreatedBy,
                           CreationDate = oldCustomer.CreationDate,
                           ModifiedBy = userName,
                           ModificationDate = DateTime.Now,
                           Active = oldCustomer.Active,
                           IsCollection = newCustomer.IsCollection
                       };

                    DynamicFieldsAggregator aggregator = CreateAggregatorForSpecificCustomerInfo(selectedCustomerSpecificInfoType);

                    if (aggregator.Controls.Count > 0)
                        updateCustomer.Xml = GetXmlFromAggregator(aggregator);

                    _customerRepository.Update(updateCustomer);

                    if (!comparer.Equals(oldCustomer, newCustomer))
                    {
                        string emailBody = GenerateCustomerRequestEmailBody(newCustomer, true);
                        SaveNewCustomerRequest(updateCustomer, ref emailBody);
                        SendCustomerRequestEmail(emailBody, false, false, oldCustomer.CustomerNumber, userName);
                        returnMessage = "A request has been sent to dynamics to create and/or update the Company Information.";
                    }
                }

                return returnMessage;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to update customer information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to update company information!", ex);
            }
        }

        private DynamicFieldsAggregator CreateAggregatorForSpecificCustomerInfo(IList<int> selectedCustomerInfoType)
        {
            List<CS_CustomerSpecificInfoType> customerSpecificType = _customerSpecificInfoTypeRepository.ListAll(e => e.Active && selectedCustomerInfoType.Contains(e.ID)).ToList();

            DynamicFieldsAggregator aggregator = new DynamicFieldsAggregator();

            for (int i = 0; i < customerSpecificType.Count; i++)
            {
                DynamicLabel label = new DynamicLabel();
                label.Text = customerSpecificType[i].Description + ":";
                label.Css = "dynamicLabel";

                string controlName = StringManipulation.RemoveSpecialCharactersForControlName("txt" + customerSpecificType[i].Description.Replace(" ", ""));

                DynamicTextBoxXml textbox = new DynamicTextBoxXml("", controlName, false, "", "", 255, label, "input", "", true);
                textbox.CustomerSpecificInfoTypeID = customerSpecificType[i].ID;
                aggregator.Controls.Add(textbox);
            }

            return aggregator;
        }

        public IList<int> GetSelectedCustomerSpecificInfoType(string xml)
        {
            DynamicFieldsSerialize serialize = new DynamicFieldsSerialize();
            DynamicFieldsAggregator aggregator = serialize.DeserializeObject(xml);
            List<int> returnList = new List<int>();

            for (int i = 0; i < aggregator.Controls.Count; i++)
            {
                returnList.Add(aggregator.Controls[i].CustomerSpecificInfoTypeID);
            }

            return returnList;

        }

        private string GetXmlFromAggregator(DynamicFieldsAggregator aggregator)
        {
            DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
            return serializer.SerializeObject(aggregator);
        }

        #endregion

        #endregion

        #region [ Contact ]

        #region [ Listing ]

        /// <summary>
        /// Verify if the CS_Customer is temporary by checking its Customer Number
        /// </summary>
        /// <param name="customerId">ID of the Customer</param>
        /// <returns>Returns true if a Customer is Temporary</returns>
        public bool HasTemporaryCustomerID(int customerId)
        {
            CS_Customer customer = _customerRepository.Get(e => e.ID == customerId);
            return string.IsNullOrEmpty(customer.CustomerNumber);
        }

        /// <summary>
        /// List ALL filtered contacts
        /// </summary>
        /// <param name="filteredContact">filtered contact</param>
        /// <returns>list</returns>
        public IList<CS_Contact> ListAllFilteredContacts(string filteredContact, int customerId)
        {
            string[] arrValue = filteredContact.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            return _contactRepository.ListAll(w => arrValue.Any(f => w.Active && (w.LastName + ", " + w.Name + " \"" + (w.Alias != null ? w.Alias : "") + "\"").Contains(f) && w.CS_Customer_Contact.Any(e => e.CustomerID == customerId) && w.DynamicsContact == false), new string[] { "CS_Customer_Contact", "CS_Customer_Contact.CS_Customer" }).ToList();
        }

        /// <summary>
        /// Gets Contact information based on a Contact Identifier
        /// </summary>
        /// <param name="contactId">Contact Identifier</param>
        /// <returns>Contact information</returns>
        public CS_Contact GetContactById(int contactId)
        {
            return _contactRepository.Get(e => e.ID == contactId && e.Active);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Contact> ListAllContacts()
        {
            return _contactRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Contact> ListAllContacts(Globals.CustomerMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.CustomerMaintenance.FilterType.None:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(f => f.CS_Customer.Active && f.CS_Customer.IsGeneralLog == false), "CS_Customer_Contact");
                case Globals.CustomerMaintenance.FilterType.Customer:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(f => f.CS_Customer.IsGeneralLog == false && arrValue.Any(g => (f.CS_Customer.Name + " - " + f.CS_Customer.Country + " - " + f.CS_Customer.Attn + " - " + (string.IsNullOrEmpty(f.CS_Customer.CustomerNumber) ? "" : f.CS_Customer.CustomerNumber)).Contains(g))), "CS_Customer_Contact");
                case Globals.CustomerMaintenance.FilterType.Contact:
                    return _contactRepository.ListAll(e => e.Active && arrValue.Any(g => (e.LastName + ", " + e.Name + " \"" + (e.Alias != null ? e.Alias : "") + "\"").Contains(g)), "CS_Customer_Contact");
                case Globals.CustomerMaintenance.FilterType.Location:
                    return _contactRepository.ListAll(e => e.Active
                                                            && e.CS_Customer_Contact.Any(f => f.CS_Customer.IsGeneralLog == false && arrValue.Any(g => (f.CS_Customer.Country + ", " + f.CS_Customer.State + ", " + f.CS_Customer.City).Contains(g)))
                                                            && arrValue.Any(g => (e.Country + ", " + e.State + ", " + e.City).Contains(g)), "CS_Customer_Contact");
                default:
                    return new List<CS_Contact>();
            }
        }

        /// <summary>
        /// List all Contacts by Customer
        /// </summary>
        public IList<CS_Contact> ListContactsByCustomer(Globals.CustomerMaintenance.FilterType filterType, string filterValue, long customerId)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.CustomerMaintenance.FilterType.None:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(j => j.Active && j.CustomerID == customerId));
                case Globals.CustomerMaintenance.FilterType.Customer:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(j => j.Active && j.CustomerID == customerId));
                case Globals.CustomerMaintenance.FilterType.Contact:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(j => j.Active && j.CustomerID == customerId)
                                                           && arrValue.Any(g => (e.LastName + ", " + e.Name + " \"" + (e.Alias != null ? e.Alias : "") + "\"").Contains(g)));
                case Globals.CustomerMaintenance.FilterType.Location:
                    return _contactRepository.ListAll(e => e.Active && e.CS_Customer_Contact.Any(j => j.Active && j.CustomerID == customerId)
                                                           && arrValue.Any(g => (e.Country + ", " + e.State + ", " + e.City).Contains(g)));
                default:
                    return new List<CS_Contact>();
            }
        }

        /// <summary>
        /// List all Contacts filtered by a customerID
        /// </summary>
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="dynamicsContact">Indicates if the method should load Dynamics contacts or CS contacts</param>
        /// <returns>List of Contacts</returns>
        public IList<CS_Contact> ListAllFilteredContacts(long customerId, bool dynamicsContact)
        {
            return _contactRepository.ListAll(e => e.Active && e.DynamicsContact == dynamicsContact && e.CS_Customer_Contact.Any(f => f.CustomerID == customerId));
        }

        /// <summary>
        /// List all Contacts filtered by a customerID and name
        /// </summary>
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="dynamicsContact">Indicates if the method should load Dynamics contacts or CS contacts</param>
        /// <param name="name">Name of the Contact</param>
        /// <returns>List of Contacts</returns>
        public IList<CS_Contact> ListFilteredContactsByName(long customerId, bool dynamicsContact, string name)
        {
            if (customerId == 0)
                if (dynamicsContact)
                    return _contactRepository.ListAll(e => e.Active && e.DynamicsContact == dynamicsContact && (e.Attn.Contains(name) || e.Alias.Contains(name) || (e.Attn.Trim() + " - " + e.Phone.Trim()).Contains(name) || (e.Attn.Trim() + " - " + e.State.Trim()).Contains(name) || (e.Attn.Trim() + " - " + e.State.Trim() + " - " + e.Phone.Trim()).Contains(name)), "CS_PhoneNumber");
                else
                    return _contactRepository.ListAll(e => e.Active && e.DynamicsContact == dynamicsContact && ((e.LastName + ", " + e.Name + " \"" + (e.Alias != null ? e.Alias : "") + "\"").Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.Phone.Trim()).Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.State.Trim()).Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.State.Trim() + " - " + e.Phone.Trim()).Contains(name)), "CS_PhoneNumber");
            else
                if (dynamicsContact)
                    return _contactRepository.ListAll(e => e.Active && e.DynamicsContact == dynamicsContact && (e.Attn.Contains(name) || e.Alias.Contains(name) || (e.Attn.Trim() + " - " + e.Phone.Trim()).Contains(name) || (e.Attn.Trim() + " - " + e.State.Trim()).Contains(name) || (e.Attn.Trim() + " - " + e.State.Trim() + " - " + e.Phone.Trim()).Contains(name))
                                                    && e.CS_Customer_Contact.Any(f => f.CustomerID == customerId), "CS_PhoneNumber");
                else
                    return _contactRepository.ListAll(e => e.Active && e.DynamicsContact == dynamicsContact && ((e.LastName + ", " + e.Name + " \"" + (e.Alias != null ? e.Alias : "") + "\"").Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.Phone.Trim()).Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.State.Trim()).Contains(name) || (e.LastName.Trim() + ", " + e.Name.Trim() + " \"" + (e.Alias != null ? e.Alias : "") + "\"" + " - " + e.State.Trim() + " - " + e.Phone.Trim()).Contains(name))
                                                    && e.CS_Customer_Contact.Any(f => f.CustomerID == customerId), "CS_PhoneNumber");
        }

        /// <summary>
        /// List all Contacts filtered by a customerID and name
        /// </summary>
        /// <param name="customerId">Customer Identifier</param>
        /// <param name="name">Name of the Contact</param>
        /// <returns>List of Contacts</returns>
        public IList<CS_Contact> ListAllFilteredContactsByName(long customerId, string name)
        {
            if (customerId == 0)
                return _contactRepository.ListAll(e => e.Active && e.Name.StartsWith(name), e => e.Name, true);
            else
                return _contactRepository.ListAll(e => e.Active && e.Name.StartsWith(name)
                                                    && e.CS_Customer_Contact.Any(f => f.CustomerID == customerId));
        }

        /// <summary>
        /// List all Contacts filtered by a jobID and name
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Contacts List</returns>
        public IList<CS_Contact> ListAllFilteredContactsByJob(int jobId)
        {
            CS_CustomerInfo customerInfo = _customerInfoRepository.Get(e => e.JobId == jobId && e.Active);

            IList<CS_Customer_Contact> contactList = _customerContactRepository.ListAll(e => e.Active && e.CustomerID == customerInfo.CustomerId);
            List<int> ids = new List<int>();
            for (int i = 0; i < contactList.Count; i++)
                ids.Add(contactList[i].ContactID);

            return _contactRepository.ListAll(
                e => e.Active && ids.Contains(e.ID));//, 
            //new string[] { "CS_Customer_Contact", "CS_Customer_Contact.CS_Customer" });
        }

        /// <summary>
        /// List all Contacts filtered by call log and type
        /// </summary>
        /// <param name="callLogId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<CS_Contact> ListAllAddedContact(int callLogId, int type)
        {
            return _contactRepository.ListAll(w => w.CS_CallLogResource.Any(e => e.ContactID == w.ID && e.Active && e.CallLogID == callLogId && e.Type == type));

        }

        public IList<CS_Contact> GetContactDummyDataForInitialAdvise(int customerId)
        {
            return _contactRepository.ListAll(e => e.ID < 50 && e.CS_Customer_Contact.Any(f => f.CustomerID == customerId) && e.Active,
                order => order.Name, true);
        }

        #endregion

        #region [ Save ]

        public void SaveNewContactRequest(int customerID, CS_Contact newContact, ref string note)
        {
            try
            {
                CS_Request request = new CS_Request()
                {
                    CustomerID = customerID,
                    ContactID = newContact.ID,
                    RequestDate = DateTime.Now,
                    Note = note,
                    Status = (int)Globals.CustomerMaintenance.RequestStatus.Pending,
                    ApprovedDate = null,
                    IsCustomer = false,
                    CreatedBy = newContact.ModifiedBy,
                    CreationDate = newContact.ModificationDate,
                    CreationID = newContact.ModificationID,
                    ModifiedBy = newContact.ModifiedBy,
                    ModificationDate = newContact.ModificationDate,
                    ModificationID = newContact.ModificationID,
                    Active = true
                };

                _requestRepository.Add(request);

                note = note.Replace("@@TemporaryID", newContact.ID + "-" + request.ID);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to save contact request information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save contact request information!", ex);
            }
        }

        public string SaveContact(int customerID, CS_Contact contact, List<PhoneNumberVO> numbers, string userName)
        {
            string returnMessage = "The Company Contact Profile saved successfully."; ;

            using (TransactionScope scope = new TransactionScope())
            {
                DateTime processTime = DateTime.Now;
                bool newContact = contact.ID.Equals(0);
                bool createRequest = false;
                string emailBody = string.Empty;

                //CS_Contact Insert/Update
                try
                {
                    if (!newContact)
                    {
                        returnMessage = "The Company Contact Information has been updated.";
                        Globals.CustomerMaintenance.CS_Contact_Comparer compare = new Globals.CustomerMaintenance.CS_Contact_Comparer();
                        CS_Contact oldContact = _contactRepository.Get(e => e.ID == contact.ID);

                        contact.ContactNumber = oldContact.ContactNumber;
                        contact.CheckSum = oldContact.CheckSum;
                        contact.CreatedBy = oldContact.CreatedBy;
                        contact.CreationDate = oldContact.CreationDate;
                        contact.CreationID = oldContact.CreationID;
                        contact.ModifiedBy = userName;
                        contact.ModificationDate = processTime;
                        //contact.ModificationID = ModificationID;
                        contact.Active = true;

                        emailBody = GenerateContactRequestEmailBody(contact, !newContact);
                        createRequest = !compare.Equals(oldContact, contact) && oldContact.DynamicsContact;

                        if (createRequest)
                        {
                            contact.Name = oldContact.Name;
                            contact.LastName = oldContact.LastName;
                            contact.Alias = oldContact.Alias;
                            contact.ContactNumber = oldContact.ContactNumber;
                            contact.Attn = oldContact.Attn;
                            contact.Address1 = oldContact.Address1;
                            contact.Address2 = oldContact.Address2;
                            contact.State = oldContact.State;
                            contact.City = oldContact.City;
                            contact.Country = oldContact.Country;
                            contact.Zip = oldContact.Zip;
                            contact.HomePhoneCodeArea = oldContact.HomePhoneCodeArea;
                            contact.Phone = oldContact.Phone;
                            contact.FaxCodeArea = oldContact.FaxCodeArea;
                            contact.Fax = oldContact.Fax;
                            contact.Email = oldContact.Email;
                            contact.Webpage = oldContact.Webpage;
                            contact.IMAddress = oldContact.IMAddress;
                            contact.DynamicsContact = oldContact.DynamicsContact;
                            contact.BillName = oldContact.BillName;
                            contact.BillAddress1 = oldContact.BillAddress1;
                            contact.BillAddress2 = oldContact.BillAddress2;
                            contact.BillAttn = oldContact.BillAttn;
                            contact.BillState = oldContact.BillState;
                            contact.BillCity = oldContact.BillCity;
                            contact.BillCountry = oldContact.BillCountry;
                            contact.BillZip = oldContact.BillZip;
                            contact.BillingHomePhoneCodeArea = oldContact.BillingHomePhoneCodeArea;
                            contact.BillPhone = oldContact.BillPhone;
                            contact.BillFaxCodeArea = oldContact.BillFaxCodeArea;
                            contact.BillFax = oldContact.BillFax;
                            contact.BillSalutation = oldContact.BillSalutation;
                            contact.BillThruProject = oldContact.BillThruProject;
                        }

                        contact = _contactRepository.Update(contact);
                    }
                    else
                    {
                        contact.CreatedBy = userName;
                        contact.CreationDate = processTime;
                        //contact.CreationID = CreationID;
                        contact.ModifiedBy = userName;
                        contact.ModificationDate = processTime;
                        //contact.ModificationID = ModificationID;
                        contact.Active = true;

                        contact = _contactRepository.Add(contact);

                        //CS_Customer_Contact Insert
                        CS_Customer_Contact customerContact = new CS_Customer_Contact()
                        {
                            CustomerID = customerID,
                            ContactID = contact.ID,
                            CreatedBy = contact.CreatedBy,
                            CreationDate = contact.CreationDate,
                            ModifiedBy = contact.ModifiedBy,
                            ModificationDate = contact.ModificationDate,
                            Active = contact.Active,
                            CreationID = contact.CreationID,
                            ModificationID = contact.ModificationID
                        };

                        _customerContactRepository.Add(customerContact);
                        createRequest = true && contact.DynamicsContact;
                        emailBody = GenerateContactRequestEmailBody(contact, !newContact);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to save contact information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while trying to save contact information!", ex);
                }

                //CS_Request Insert
                if (createRequest)
                {
                    emailBody = GenerateContactRequestEmailBody(contact, !newContact);

                    SaveNewContactRequest(customerID, contact, ref emailBody);

                    if (newContact)
                        SendContactRequestEmail(emailBody, false, false, null, userName);
                    else
                        SendContactRequestEmail(emailBody, false, false, contact.ContactNumber, userName);

                    returnMessage = "A request has been sent to dynamics to create and/or update the Company Bill-To-Contact information.";
                }

                //CS_PhoneNumber Insert/Update
                try
                {
                    if (null != contact)
                    {
                        List<CS_PhoneNumber> oldPhoneList = new List<CS_PhoneNumber>();

                        if (!newContact)
                            oldPhoneList = ListAllPhonesByEmployee(contact.ID);

                        List<CS_PhoneNumber> newPhoneList = GetListFromPhoneVO(numbers);
                        List<CS_PhoneNumber> removedPhoneList = new List<CS_PhoneNumber>();

                        if (oldPhoneList.Count > 0)
                            removedPhoneList = oldPhoneList.Where(e => !newPhoneList.Any(f => f.ID == e.ID)).ToList();

                        List<CS_PhoneNumber> addedPhoneList = newPhoneList.Where(e => e.ID == 0).ToList();

                        for (int i = 0; i < removedPhoneList.Count; i++)
                        {
                            removedPhoneList[i].ModifiedBy = userName;
                            removedPhoneList[i].ModificationDate = processTime;
                            removedPhoneList[i].Active = false;
                        }

                        for (int i = 0; i < addedPhoneList.Count; i++)
                        {
                            addedPhoneList[i].ContactID = contact.ID;
                            addedPhoneList[i].CreatedBy = userName;
                            addedPhoneList[i].CreationDate = processTime;
                            //addedPhoneList[i].CreationID = 
                            addedPhoneList[i].ModifiedBy = userName;
                            addedPhoneList[i].ModificationDate = processTime;
                            //addedPhoneList[i].ModificationID = 
                            addedPhoneList[i].Active = true;
                        }

                        _phoneNumberRepository.UpdateList(removedPhoneList);
                        _phoneNumberRepository.AddList(addedPhoneList);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Update Additional Contact Information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while updating Additional Contact Information", ex);
                }

                scope.Complete();
            }

            return returnMessage;
        }

        /// <summary>
        /// Retrieves a list of the contact Additional Phone Numbers
        /// </summary>
        /// <param name="contactId">ID of the contact</param>
        /// <returns>List with CS_PhoneNumber entities representing the data</returns>
        public List<CS_PhoneNumber> ListAllPhonesByEmployee(int contactId)
        {
            return _phoneNumberRepository.ListAll(e => e.ContactID == contactId && e.Active, "CS_PhoneType").ToList();
        }

        /// <summary>
        /// Create a stub CS_PhoneNumber from a PhoneNumberVO, for saving
        /// </summary>
        /// <param name="additionalPhoneNumbersList">List of PhoneNumberVO</param>
        /// <returns>List of CS_PhoneNumber</returns>
        private List<CS_PhoneNumber> GetListFromPhoneVO(List<PhoneNumberVO> additionalPhoneNumbersList)
        {
            List<CS_PhoneNumber> returnList = new List<CS_PhoneNumber>();

            for (int i = 0; i < additionalPhoneNumbersList.Count; i++)
            {
                PhoneNumberVO vo = additionalPhoneNumbersList[i];

                CS_PhoneNumber item = new CS_PhoneNumber()
                {
                    ID = vo.ID,
                    Number = vo.Number,
                    PhoneTypeID = vo.TypeID
                };

                returnList.Add(item);
            }

            return returnList;
        }

        public IList<CS_PhoneNumber> GetAdditionalPhonesByContact(int contactId)
        {
            return _phoneNumberRepository.ListAll(e => e.Active && e.ContactID == contactId);
        }

        #endregion

        #endregion

        #region [ Customer Contract ]

        #region [ Listing ]

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <param name="customerId">Customer Id of a specific Customer</param>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_CustomerContract> ListAllCustomerContracts(long customerId)
        {
            return _contractRepository.ListAll(e => e.Active && e.CustomerID == customerId);
        }

        /// <summary>
        /// List the Dynamic Fields in the Customer Table
        /// </summary>
        /// <returns>List of CustomerSpecificInfo Object</returns>
        public IList<CustomerSpecificInfo> ListAllCustomerSpecificInfo(long customerId)
        {
            CS_Customer customer = _customerRepository.Get(e => e.Active && e.ID == customerId);

            if (!string.IsNullOrEmpty(customer.Xml))
                return CustomerSpecificInfo.DeserializeObject(customer.Xml);
            else
                return new List<CustomerSpecificInfo>();
        }

        /// <summary>
        /// Gets Contract Information based on a Contract Identifier
        /// </summary>
        /// <param name="contractId">Contract Identifier</param>
        /// <returns>Contract Details</returns>
        public CS_CustomerContract GetContractById(int contractId)
        {
            return _contractRepository.Get(e => e.Active && e.ID == contractId);
        }

        #endregion

        #endregion

        #region [ Request ]

        #region [ Emails ]

        /// <summary>
        /// Generates the email body to be sent for Customer request
        /// </summary>
        /// <param name="customer">Customer information</param>
        /// <param name="isUpdate">Indicates if it's an update or not</param>
        /// <returns>Email Body</returns>
        private string GenerateCustomerRequestEmailBody(CS_Customer customer, bool isUpdate)
        {
            if (null != customer)
            {
                StringBuilder emailBody = new StringBuilder();

                if (isUpdate)
                    emailBody.Append("<p>A change for an existing Company record has been requested for ");
                else
                    emailBody.Append("<p>A new Company record has been requested for ");
                emailBody.Append(customer.Name);
                if (isUpdate)
                {
                    emailBody.Append(" with a master ID of ");
                    emailBody.Append(customer.CustomerNumber);
                    emailBody.Append(" and ");
                }

                emailBody.Append(" with a temporary ID of @@TemporaryID");

                emailBody.Append(" on ");
                emailBody.Append(DateTime.Now.ToString("MM/dd/yyyy"));
                emailBody.Append(" by ");
                emailBody.Append(customer.ModifiedBy);
                emailBody.Append(".</p>");

                emailBody.Append("<p>Please review company information");
                if (!isUpdate)
                    emailBody.Append(" and determine a master ID should be created in Dynamics.");
                emailBody.Append("</p>");

                StringBuilder customerFormattedData = new StringBuilder();
                customerFormattedData.AppendFormat("Company: <Text>{0}<BL>", customer.Name);
                customerFormattedData.AppendFormat("Bill-To-Contact: <Text>{0}<BL>", customer.BillName);
                customerFormattedData.AppendFormat("Attention: <Text>{0}<BL>", customer.Attn);
                customerFormattedData.AppendFormat("Salutation: <Text>{0}<BL>", customer.BillSalutation);
                customerFormattedData.AppendFormat("Address Line 1: <Text>{0}<BL>", customer.Address1);
                customerFormattedData.AppendFormat("Address Line 2: <Text>{0}<BL>", customer.Address2);
                customerFormattedData.AppendFormat("City: <Text>{0}<BL>", customer.City);
                customerFormattedData.AppendFormat("State/Province: <Text>{0}<BL>", customer.State);
                customerFormattedData.AppendFormat("Postal Code: <Text>{0}<BL>", customer.Zip);
                customerFormattedData.AppendFormat("Country/Region: <Text>{0}<BL>", customer.Country);
                customerFormattedData.AppendFormat("Phone/Extension: <Text>{0}<BL>", customer.Phone);
                customerFormattedData.AppendFormat("Fax/Extension: <Text>{0}<BL>", customer.Fax);
                customerFormattedData.AppendFormat("Email Address: <Text>{0}<BL>", customer.Email);

                emailBody.Append(StringManipulation.TabulateString(customerFormattedData.ToString()));

                return emailBody.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Generates the cancel email body to be sent for Customer request
        /// </summary>
        /// <param name="customer">Customer information</param>
        /// <param name="isUpdate">Indicates if it's an update or not</param>
        /// <param name="requestDate">Request date</param>
        /// <param name="requestedByUsername">Username that created the request</param>
        /// <returns>Email Body</returns>
        private string GenerateCancelCustomerRequestEmailBody(CS_Customer customer, bool isUpdate, DateTime requestDate, string requestedByUsername)
        {
            if (null != customer)
            {
                StringBuilder emailBody = new StringBuilder();

                if (isUpdate)
                    emailBody.Append("<p>The request to change an existing Company record for ");
                else
                    emailBody.Append("<p>The request to add a new Company record for ");
                emailBody.Append(customer.Name);
                if (isUpdate)
                {
                    emailBody.Append(" with a Master ID of ");
                    emailBody.Append(customer.CustomerNumber);
                }
                else
                {
                    emailBody.Append(" with a temporary ID of ");
                    emailBody.Append(customer.ID);
                }
                emailBody.Append(" on ");
                emailBody.Append(requestDate.ToString("MM/dd/yyyy"));
                emailBody.Append(" by ");
                emailBody.Append(requestedByUsername);
                emailBody.Append(" has been cancelled. Please disregard request.</p>");

                return emailBody.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Sends the request Customer email
        /// </summary>
        /// <param name="emailBody">Email body</param>
        /// <param name="resend">Indicates if it's a new attempt of sending the request (TRUE) or it's the first attempt (FALSE)</param>
        /// <param name="cancel">Indicates if it's a new attempt of cancelling the request (TRUE) or it's the first attempt (FALSE)</param>
        /// <param name="customerNumber">If it's an update request, this field will have the Master Customer ID, if not it will be null</param>
        /// <param name="username">Username that made the request</param>
        private void SendCustomerRequestEmail(string emailBody, bool resend, bool cancel, string customerNumber, string username)
        {
            string subject = "New Company Request";
            if (resend)
                subject += " (RESEND)";
            if (cancel)
                subject += " (CANCEL)";
            if (!string.IsNullOrEmpty(customerNumber))
                subject += " - Master ID: " + customerNumber;

            string[] receipts = null;
            SettingsModel settingsModel = new SettingsModel(_unitOfWork);
            string emails = settingsModel.GetCustomerRequestEmails();
            if (!string.IsNullOrEmpty(emails))
                receipts = emails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (null != receipts)
            {
                EmailModel emailModel = new EmailModel(_unitOfWork);
                for (int i = 0; i < receipts.Length; i++)
                {
                    emailModel.SaveEmail(receipts[i], subject, emailBody, username, null);
                }
            }
        }

        /// <summary>
        /// Generates the email body to be sent for Contact request
        /// </summary>
        /// <param name="contact">Contact information</param>
        /// <param name="isUpdate">Indicates if it's an update or not</param>
        /// <returns>Email Body</returns>
        private string GenerateContactRequestEmailBody(CS_Contact contact, bool isUpdate)
        {
            if (null != contact)
            {
                StringBuilder emailBody = new StringBuilder();

                if (isUpdate)
                    emailBody.Append("<p>A change for an existing Company Contact record has been requested for ");
                else
                    emailBody.Append("<p>A new Company Contact record has been requested for ");
                emailBody.Append(contact.Name);
                if (isUpdate)
                {
                    emailBody.Append(" with a master ID of ");
                    emailBody.Append(contact.ContactNumber);
                    emailBody.Append(" and ");
                }
                emailBody.Append(" with a temporary ID of @@TemporaryID");

                emailBody.Append(" on ");
                emailBody.Append(DateTime.Now.ToString("MM/dd/yyyy"));
                emailBody.Append(" by ");
                emailBody.Append(contact.ModifiedBy);
                emailBody.Append(".</p>");

                emailBody.Append("<p>Please review Company Contact information");
                if (!isUpdate)
                    emailBody.Append(" and determine a master ID should be created in Dynamics.");
                emailBody.Append("</p>");

                StringBuilder contactFormattedData = new StringBuilder();
                contactFormattedData.AppendFormat("Contact Name: <Text>{0}, {1}<BL>", contact.LastName, contact.Name);
                contactFormattedData.Append("Company: <Text>");
                foreach (CS_Customer_Contact customer in contact.CS_Customer_Contact)
                    contactFormattedData.AppendFormat("{0} - {1}<BL>", customer.CS_Customer.CustomerNumber, customer.CS_Customer.Name);
                contactFormattedData.AppendFormat("Salutation: <Text>{0}<BL>", contact.BillSalutation);
                contactFormattedData.AppendFormat("Address Line 1: <Text>{0}<BL>", contact.Address1);
                contactFormattedData.AppendFormat("Address Line 2: <Text>{0}<BL>", contact.Address2);
                contactFormattedData.AppendFormat("City: <Text>{0}<BL>", contact.City);
                contactFormattedData.AppendFormat("State/Province: <Text>{0}<BL>", contact.State);
                contactFormattedData.AppendFormat("Postal Code: <Text>{0}<BL>", contact.Zip);
                contactFormattedData.AppendFormat("Country/Region: <Text>{0}<BL>", contact.Country);
                contactFormattedData.AppendFormat("Phone/Extension: <Text>{0}<BL>", contact.Phone);
                contactFormattedData.AppendFormat("Fax/Extension: <Text>{0}<BL>", contact.Fax);
                contactFormattedData.AppendFormat("Email Address: <Text>{0}<BL>", contact.Email);

                emailBody.Append(StringManipulation.TabulateString(contactFormattedData.ToString()));

                return emailBody.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Generates the cancel email body to be sent for Contact request
        /// </summary>
        /// <param name="contact">Contact information</param>
        /// <param name="isUpdate">Indicates if it's an update or not</param>
        /// <param name="requestDate">Request date</param>
        /// <param name="requestedByUsername">Username that created the request</param>
        /// <returns>Email Body</returns>
        private string GenerateCancelContactRequestEmailBody(CS_Contact contact, bool isUpdate, DateTime requestDate, string requestedByUsername)
        {
            if (null != contact)
            {
                StringBuilder emailBody = new StringBuilder();

                if (isUpdate)
                    emailBody.Append("<p>The request to change am existing Company Contact record for ");
                else
                    emailBody.Append("<p>The request to add a new Company Contact record for ");
                emailBody.AppendFormat("{0}, {1}", contact.LastName, contact.Name);

                if (isUpdate)
                {
                    emailBody.Append(" with a master ID of ");
                    emailBody.Append(contact.ContactNumber);
                }
                else
                {
                    emailBody.Append(" with a temporary ID of ");
                    emailBody.Append(contact.ID);
                }
                emailBody.Append(" on ");
                emailBody.Append(requestDate.ToString("MM/dd/yyyy"));
                emailBody.Append(" by ");
                emailBody.Append(requestedByUsername);
                emailBody.Append(" Has been cancelled. Please disregard request.</p>");

                return emailBody.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Sends the request Contact email
        /// </summary>
        /// <param name="emailBody">Email body</param>
        /// <param name="resend">Indicates if it's a new attempt of sending the request (TRUE) or it's the first attempt (FALSE)</param>
        /// <param name="cancel">Indicates if it's a new attempt of cancelling the request (TRUE) or it's the first attempt (FALSE)</param>
        /// <param name="customerNumber">If it's an update request, this field will have the Master Customer Contact ID, if not it will be null</param>
        /// <param name="username">Username that made the request</param>
        private void SendContactRequestEmail(string emailBody, bool resend, bool cancel, string contactNumber, string username)
        {
            string subject = "New Contact Request";
            if (resend)
                subject += " (RESEND)";
            if (cancel)
                subject += " (CANCEL)";
            if (!string.IsNullOrEmpty(contactNumber))
                subject += " - Master ID: " + contactNumber;

            string[] receipts = null;
            SettingsModel settingsModel = new SettingsModel(_unitOfWork);
            string emails = settingsModel.GetContactRequestEmails();
            if (!string.IsNullOrEmpty(emails))
                receipts = emails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (null != receipts)
            {
                EmailModel emailModel = new EmailModel(_unitOfWork);
                for (int i = 0; i < receipts.Length; i++)
                {
                    emailModel.SaveEmail(receipts[i], subject, emailBody, username, null);
                }
            }
        }

        #endregion

        #region [ Listing ]

        /// <summary>
        /// Returns a list of filtered Requests
        /// </summary>
        /// <param name="filterType">Filter Type</param>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>List of requests</returns>
        public IList<CS_Request> ListAllFilteredRequests(Globals.CustomerMaintenance.RequestFilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.CustomerMaintenance.RequestFilterType.CustomerName:
                    return _requestRepository.ListAll(
                        e => e.Active && e.IsCustomer && e.CS_Customer != null &&
                            arrValue.Any(like => e.CS_Customer.Name.Contains(like))
                        , "CS_Customer", "CS_Contact");
                case Globals.CustomerMaintenance.RequestFilterType.ContactName:
                    return _requestRepository.ListAll(
                        e => e.Active && !e.IsCustomer && e.CS_Contact != null &&
                            arrValue.Any(like => e.CS_Contact.Name.Contains(like) || e.CS_Contact.LastName.Contains(like))
                        , "CS_Customer", "CS_Contact");
                case Globals.CustomerMaintenance.RequestFilterType.Status:
                    List<int> statusFilter = new List<int>();
                    if (arrValue.Length == 1 && string.IsNullOrEmpty(arrValue[0]))
                    {
                        statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Pending);
                        statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Approved);
                        statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Cancelled);
                    }
                    for (int i = 0; i < arrValue.Length; i++)
                    {
                        if ("pending".Contains(arrValue[i].ToLower()))
                            statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Pending);
                        if ("approved".Contains(arrValue[i].ToLower()))
                            statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Approved);
                        if ("cancelled".Contains(arrValue[i].ToLower()))
                            statusFilter.Add((int)Globals.CustomerMaintenance.RequestStatus.Cancelled);
                    }
                    return _requestRepository.ListAll(
                        e => e.Active && e.IsCustomer && e.CS_Customer != null &&
                            statusFilter.Any(like => e.Status == like)
                        , "CS_Customer", "CS_Contact");
                case Globals.CustomerMaintenance.RequestFilterType.None:
                default:
                    return _requestRepository.ListAll(
                        e => e.Active && e.Status == (int)Globals.CustomerMaintenance.RequestStatus.Pending
                        , "CS_Customer", "CS_Contact");
            }
        }

        #endregion

        #region [ Delete / Resend ]

        /// <summary>
        /// Removes an existing request
        /// </summary>
        /// <param name="requestId">Request Identifier</param>
        /// <param name="username">Username that requested the deletion</param>
        /// <returns>TRUE if OK, FALSE if the customer is assigned to a job</returns>
        public bool DeleteRequest(int requestId, string username)
        {
            CS_Request request = _requestRepository.Get(e => e.ID == requestId);
            if (null != request)
            {
                CS_Customer assignedCustomer = null;
                if (request.IsCustomer)
                {
                    // verify if customer is assigned to a job
                    assignedCustomer = _customerRepository.Get(
                        e => e.CustomerNumber == null &&
                            e.CS_CustomerInfo.Any(
                            f => f.Active && f.CS_Job.Active && f.CS_Job.CS_JobInfo.CS_Job_JobStatus.Any(
                                h => h.Active && h.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                                )
                            )
                        );
                    if (null != assignedCustomer)
                        return false;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    if (request.IsCustomer)
                    {
                        CS_Customer customer = _customerRepository.Get(e => e.ID == request.CustomerID);
                        if (null != customer)
                        {
                            string emailBody = GenerateCancelCustomerRequestEmailBody(customer, false, request.RequestDate, request.CreatedBy);
                            SendCustomerRequestEmail(emailBody, false, true, customer.CustomerNumber, username);
                        }

                        if (string.IsNullOrEmpty(customer.CustomerNumber))
                        {
                            if (_requestRepository.ListAll(e => e.CustomerID == customer.ID && e.Active && e.ID != requestId).Count == 0)
                            {
                                customer.Active = false;
                                customer.ModifiedBy = username;
                                customer.ModificationDate = DateTime.Now;

                                _customerRepository.Update(customer);
                            }
                        }
                    }
                    else if (request.ContactID.HasValue)
                    {
                        CS_Contact contact = _contactRepository.Get(e => e.ID == request.ContactID.Value);
                        if (null != contact)
                        {
                            string emailBody = GenerateCancelContactRequestEmailBody(contact, false, request.RequestDate, request.CreatedBy);
                            SendContactRequestEmail(emailBody, false, true, contact.ContactNumber, username);
                        }

                        if (string.IsNullOrEmpty(contact.ContactNumber))
                        {
                            if (_requestRepository.ListAll(e => e.ContactID == contact.ID && e.Active && e.ID != requestId).Count == 0)
                            {
                                contact.Active = false;
                                contact.ModifiedBy = username;
                                contact.ModificationDate = DateTime.Now;

                                _contactRepository.Update(contact);
                            }
                        }
                    }

                    request.Active = false;
                    request.ModifiedBy = username;
                    request.ModificationDate = DateTime.Now;
                    _requestRepository.Update(request);

                    scope.Complete();
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Resend an existing request email
        /// </summary>
        /// <param name="requestId">Request Identifier</param>
        public void ResendRequest(int requestId)
        {
            CS_Request request = _requestRepository.Get(e => e.ID == requestId);
            if (null != requestId)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (request.IsCustomer)
                    {
                        CS_Customer customer = _customerRepository.Get(e => e.ID == request.CustomerID);
                        if (null != customer)
                        {
                            string emailBody = request.Note;
                            SendCustomerRequestEmail(emailBody, true, false, customer.CustomerNumber, request.CreatedBy);
                        }
                    }
                    else if (request.ContactID.HasValue)
                    {
                        CS_Contact contact = _contactRepository.Get(e => e.ID == request.ContactID.Value);
                        if (null != contact)
                        {
                            string emailBody = request.Note;
                            SendContactRequestEmail(emailBody, true, false, contact.ContactNumber, request.CreatedBy);
                        }
                    }

                    scope.Complete();
                }
            }
        }

        #endregion

        #endregion

        #region [ Customer Specific Info ]

        #region [ Listing ]

        public IList<CS_CustomerSpecificInfoType> ListAllCustomerSpecificInfoType()
        {
            return _customerSpecificInfoTypeRepository.ListAll(e => e.Active);
        }

        #endregion

        #endregion

        #region [ Call Criteria ]

        #region [ Listing ]

        /// <summary>
        /// Searches for Employee and Criteria info and returns in a related fashion
        /// </summary>
        /// <param name="id">ID of the Employee</param>
        /// <returns>Entity containing Employee Info</returns>
        public CallCriteriaResourceVO GetContactDataForInitialAdvise(int id)
        {
            return BuildContactDataForInitialAdvise(id);
        }

        public CallCriteriaResourceVO GetContactDataForInitialAdvise(int contactId, int callLogId)
        {
            CallCriteriaResourceVO returnItem = BuildContactDataForInitialAdvise(contactId);

            CS_CallLogResource resource = _callLogResourceRepository.Get(e => e.ContactID == contactId && e.CallLogID == callLogId);

            if (resource.InPerson.HasValue)
                returnItem.AdviseInPerson = resource.InPerson.Value;

            if (resource.Voicemail.HasValue)
                returnItem.AdviseByVoicemail = resource.Voicemail.Value;

            if (!string.IsNullOrEmpty(resource.Notes))
                returnItem.Notes = resource.Notes;

            return returnItem;
        }


        private CallCriteriaResourceVO BuildContactDataForInitialAdvise(int id)
        {
            CallCriteriaResourceVO returnItem = new CallCriteriaResourceVO();

            CS_Contact contact = _contactRepository.Get(e => e.Active && e.ID == id && e.CS_Customer_Contact.Any(f => f.Active && f.CS_Customer.Active));
            IList<CS_CallCriteriaValue> criterias = _callCriteriaValueRepository.ListAll(e => e.Active && e.CS_CallCriteria.ContactID == id);

            if (contact != null)
            {
                returnItem.ResourceID = id;
                returnItem.Type = (int)Globals.CallCriteria.EmailVOType.Contact;
                returnItem.Name = contact.FullName;
                returnItem.Customer = contact.CS_Customer_Contact.FirstOrDefault().CS_Customer.Name;
                returnItem.ContactInfo = contact.ContactInfo;
                returnItem.AdviseInPerson = false;
                returnItem.AdviseByVoicemail = false;
            }

            if (criterias.Count > 0)
            {
                StringBuilder initialAdviseInfo = new StringBuilder();
                for (int i = 0; i < criterias.Count; i++)
                {
                    initialAdviseInfo.AppendLine(criterias[i].CS_CallCriteriaType.Description);
                }

                returnItem.InitialAdviseInformation = initialAdviseInfo.ToString();
            }

            return returnItem;
        }

        #endregion

        #endregion

        #endregion

        #region [ Service Methods ]

        /// <summary>
        /// Searches for a customer that goes by a specific Number
        /// </summary>
        /// <param name="customerNumber">Customer Number (6 digit, ending in "00")</param>
        /// <returns>A entity containing all the information of that Customer</returns>
        public CS_Customer GetCustomersByNumber(string customerNumber)
        {
            return _customerRepository.Get(e => e.CustomerNumber == customerNumber && e.IsGeneralLog == false);
        }

        public CS_CustomerContract GetContractByNumber(string customerNumber, string contractNumber)
        {
            return _contractRepository.Get(e => e.ContractNumber == contractNumber && e.CS_Customer.CustomerNumber == customerNumber && e.CS_Customer.IsGeneralLog == false);
        }

        public bool ServiceWork()
        {
            using (SettingsModel settings = new SettingsModel())
            {
                bool succeded = true;

                try
                {
                    UpdateCustomers();
                }
                catch (Exception ex)
                {
                    //Logger.Write(string.Format("An error has ocurred while trying to import the Customer information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                    StringBuilder mailError = new StringBuilder();

                    mailError.AppendLine(string.Format("An Error Ocurred when importing the Customer Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                    //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dynamics Import Service - Error occured on Customer Information", false, null);

                    return false;
                }

                try
                {
                    UpdateContracts();
                }
                catch (Exception ex)
                {
                    //Logger.Write(string.Format("An error has ocurred while trying to import the Contract information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                    StringBuilder mailError = new StringBuilder();

                    mailError.AppendLine(string.Format("An Error Ocurred when importing the Contract Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                    //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dynamics Import Service - Error occured on Contract Information", false, null);

                    succeded = false;
                }

                try
                {
                    UpdateContacts();
                }
                catch (Exception ex)
                {
                    //Logger.Write(string.Format("An error has ocurred while trying to import the Contact information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                    StringBuilder mailError = new StringBuilder();

                    mailError.AppendLine(string.Format("An Error Ocurred when importing the Contact Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                    //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dynamics Import Service - Error occured on Contact Information", false, null);

                    succeded = false;
                }

                return succeded;
            }
        }

        private bool UpdateCustomers()
        {
            try
            {
                using (SettingsModel settings = new SettingsModel())
                {
                    // Bulk Copying Divisions from IVantage
                    CS_CustomerRepository customerRep = new CS_CustomerRepository(_customerRepository, _customerRepository.UnitOfWork);
                    customerRep.BulkCopyAllCustomers(DynamicsIntegration.Singleton.ListAllCustomers(settings.GetLastUpdateCustomer()));

                    //// Run import tool
                    customerRep.UpdateFromIntegration();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateContracts()
        {
            try
            {
                using (SettingsModel settings = new SettingsModel())
                {
                    // Bulk Copying Divisions from IVantage
                    CS_CustomerContractRepository contractRep = new CS_CustomerContractRepository(_contractRepository, _contractRepository.UnitOfWork);
                    contractRep.BulkCopyAllContracts(DynamicsIntegration.Singleton.ListAllContracts(settings.GetLastUpdateContract()));

                    // Run import tool
                    contractRep.UpdateFromIntegration();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateContacts()
        {
            try
            {
                using (SettingsModel settings = new SettingsModel())
                {
                    // Bulk Copying Divisions from IVantage
                    CS_ContactRepository contactRep = new CS_ContactRepository(_contactRepository, _contactRepository.UnitOfWork);
                    contactRep.BulkCopyAllContacts(DynamicsIntegration.Singleton.ListAllContacts(settings.GetLastUpdateContact()));

                    // Run import tool
                    contactRep.UpdateFromIntegration();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _customerRepository = null;
            _contactRepository = null;
            _contractRepository = null;
            _customerInfoRepository = null;
            _requestRepository = null;
            _phoneNumberRepository = null;

            _callCriteriaModel.Dispose();
            _callCriteriaModel = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
