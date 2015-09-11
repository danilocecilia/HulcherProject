using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class CustomerInfo : System.Web.UI.UserControl, ICustomerInfoView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private CustomerInfoPresenter _presenter;

        private int? _jobId;

        #endregion

        #region [ Methods ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.ListAllDivision();

                if (!_jobId.HasValue)
                    ScriptManager.GetCurrent(this.Page).SetFocus(actCustomerContact.TextControlClientID);

                if (Request.QueryString["CloningId"] != null)
                {
                    CloningId = int.Parse(Request.QueryString["CloningId"]);
                    //_presenter.LoadCustomerInfoCloningData();
                }
            }
        }

        protected void actCustomer_TextChanged(object sender, EventArgs e)
        {

            actEIC.ContextKey = actCustomer.SelectedValue;
            actEIC.FilterId = actCustomer.SelectedValue;

            //actAdditionalContact.ContextKey = actCustomer.SelectedValue;
            //actAdditionalContact.FilterId = actCustomer.SelectedValue;

            actBillToContact.ContextKey = actCustomer.SelectedValue;
            actBillToContact.FilterId = actCustomer.SelectedValue;

            actCustomerContact.ContextKey = actCustomer.SelectedValue;
            actCustomerContact.FilterId = actCustomer.SelectedValue;

            if (hidContext.Value != actCustomer.SelectedValue)
            {
                actEIC.SelectedText = "";
                actEIC.SelectedValue = "0";

                actAdditionalContact.SelectedText = "";
                actAdditionalContact.SelectedValue = "0";

                actBillToContact.SelectedText = "";
                actBillToContact.SelectedValue = "0";

                actCustomerContact.SelectedText = "";
                actCustomerContact.SelectedValue = "0";

                hidContext.Value = actCustomer.SelectedValue;
            }

            if (CustomerChanged != null)
            {
                CS_Customer selectedCustomer = new CS_Customer();
                selectedCustomer.ID = int.Parse(actCustomer.SelectedValue);
                selectedCustomer.Name = actCustomer.SelectedText;
                CustomerChanged(selectedCustomer);
            }
        }

        protected void ddlCustomerContact_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void actPOC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(actPOC.SelectedValue) && actPOC.SelectedValue != "0")
            {
                string[] splitValues = actPOC.SelectedValue.Replace(" | ", "|").Split('|');
                ddlDivision.SelectedIndex = 0;

                if (int.Parse(splitValues[1]) < 97)
                {
                    string divisionId = "";

                    foreach (ListItem item  in ddlDivision.Items)
                        if (item.Text.StartsWith(splitValues[1]))
                            divisionId = item.Value;
                    if (!string.IsNullOrEmpty(divisionId))
                    {
                        ddlDivision.SelectedValue = divisionId;
                        ddlDivision_SelectedIndexChanged(sender, e);
                    }
                }

            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            actPOC.ContextKey = ddlDivision.SelectedValue;
            actPOC.FilterId = ddlDivision.SelectedValue;

            if (null != ViewState["DivisionValue"])
            {
                if (ViewState["DivisionValue"].ToString() != ddlDivision.SelectedValue)
                {
                    actPOC.SelectedText = string.Empty;
                    actPOC.SelectedValue = string.Empty;
                }
            }

            if (DivisionChanged != null)
            {
                ViewState["DivisionValue"] = ddlDivision.SelectedValue;

                if (ddlDivision.SelectedValue == "0")
                {
                    DivisionChanged(null);
                }
                else
                {
                    CS_Division selectedDivision = new CS_Division();
                    selectedDivision.ID = int.Parse(ddlDivision.SelectedItem.Value);
                    string[] divisioNameDescription = ddlDivision.SelectedItem.Text.Split('-');
                    selectedDivision.Name = divisioNameDescription[0].Trim();
                    selectedDivision.Description = divisioNameDescription[1].Trim();
                    DivisionChanged(selectedDivision);
                }
            }
        }

        public void ReadyOnlyCustomerInfo()
        {
            ReadOnlyAccess = true;
            actCustomer.Enabled = false;
            hlAddNewCustomer.Enabled = false;
            actEIC.Enabled = false;
            lblAdditionalContact.Enabled = false;
            actAdditionalContact.Enabled = false;
            lblBillToContact.Enabled = false;
            actBillToContact.Enabled = false;
            actCustomerContact.Enabled = false;
            hlAddNewContact.Enabled = false;
            ddlDivision.Enabled = false;
            actPOC.Enabled = false;
        }

        #endregion

        #region [ Delegates and Events ]


        /// <summary>
        /// Delegate to inform the page that the Job type was changed
        /// </summary>
        /// <param name="jobType">New Job Type</param>
        public delegate void DivisionChangedHandler(CS_Division divisionEntity);

        /// <summary>
        /// Delegate to inform the page that the Customer was changed
        /// </summary>
        /// <param name="customerEntity"></param>
        public delegate void CustomerChangedHandler(CS_Customer customerEntity);

        /// <summary>
        /// Delegate to inform the page that the Customer Contact was changed
        /// </summary>
        /// <param name="customerEntity"></param>
        public delegate void CustomerContactChangedHandler(CS_Contact customerContactEntity);

        /// <summary>
        /// Event triggered when the Job type is changed
        /// </summary>
        public event DivisionChangedHandler DivisionChanged;

        /// <summary>
        /// Event triggered when the Customer is changed
        /// </summary>
        public event CustomerChangedHandler CustomerChanged;

        /// <summary>
        /// Event triggered when the Customer is changed
        /// </summary>
        public event CustomerContactChangedHandler CustomerContactChanged;

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new CustomerInfoPresenter(this);
        }

        #endregion

        #region [ View Interface Implementation ]

        /// <summary>
        /// Display the error Message
        /// </summary>
        /// <param name="message"></param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        /// <summary>
        /// Set the Division List to the Division ComboBox
        /// </summary>
        public IList<CS_Division> DivisionList
        {
            set
            {
                ddlDivision.Items.Clear();
                ddlDivision.SelectedIndex = -1;
                ddlDivision.DataTextField = "ExtendedDivisionName";
                ddlDivision.DataValueField = "Id";
                ddlDivision.DataSource = value;
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem(" - Select One - ", "0"));
            }
        }

        /// <summary>
        /// Get the Selected Value of the Customer ComboBox
        /// </summary>
        public long CustomerValue
        {
            get
            {
                return long.Parse(actCustomer.SelectedValue);
            }
            set
            {
                actCustomer.SelectedValue = value.ToString();
            }
        }

        /// <summary>
        /// Get the Selected Value of the Division ComboBox
        /// </summary>
        public long DivisionValue
        {
            get
            {
                return long.Parse(ddlDivision.SelectedValue);
            }
            set
            {
                if (ddlDivision.Items.FindByValue(value.ToString()) != null)
                {
                    ddlDivision.SelectedValue = value.ToString();
                }
            }
        }

        /// <summary>
        /// Set the Validation Group of the Validators inside the User Control
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                actCustomer.ValidationGroup = value;
                actCustomerContact.ValidationGroup = value;
            }
        }

        public CS_View_GetJobData CustomerInfoLoad
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CS_View_GetJobData customerInfo = value;
                if (null != customerInfo)
                {
                    actCustomer.SelectedValue = customerInfo.CustomerID.ToString();
                    actCustomer.SetValue = customerInfo.CustomerID.ToString();

                    actEIC.FilterId = customerInfo.CustomerID.ToString();
                    actEIC.ContextKey = customerInfo.CustomerID.ToString();
                    //actAdditionalContact.FilterId = customerInfo.CustomerID.ToString();
                    //actAdditionalContact.ContextKey = customerInfo.CustomerID.ToString();
                    actBillToContact.FilterId = customerInfo.CustomerID.ToString();
                    actBillToContact.ContextKey = customerInfo.CustomerID.ToString();
                    actCustomerContact.FilterId = customerInfo.CustomerID.ToString();
                    actCustomerContact.ContextKey = customerInfo.CustomerID.ToString();
                    hlAddNewContact.NavigateUrl = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&CustomerId=" + customerInfo.CustomerID.ToString() + "', '', 'width=1200, height=600, scrollbars=1, resizable=yes');";
                    actPOC.FilterId = customerInfo.CustomerID.ToString();
                    actPOC.ContextKey = customerInfo.CustomerID.ToString();

                    if (customerInfo.OnSiteContactID.HasValue)
                    {
                        actEIC.SelectedValue = customerInfo.OnSiteContactID.ToString();
                        //actEIC.SelectedText = customerInfo.OnSiteFullContactInformation;
                    }
                    else
                    {
                        actEIC.SetValue = "0";
                        actEIC.SelectedText = "";
                    }

                    if (customerInfo.SecondaryContactID.HasValue)
                    {
                        actAdditionalContact.SelectedValue = customerInfo.SecondaryContactID.ToString();
                        //actAdditionalContact.SelectedText = customerInfo.SecondaryFullContactInformation;
                    }
                    else
                    {
                        actAdditionalContact.SetValue = "0";
                        actAdditionalContact.SelectedText = "";
                    }

                    if (customerInfo.BillToContactID.HasValue)
                    {
                        actBillToContact.SelectedValue = customerInfo.BillToContactID.ToString();
                        //actBillToContact.SelectedText = customerInfo.BillToFullContactInformation;
                    }
                    else
                    {
                        actBillToContact.SetValue = "0";
                        actBillToContact.SelectedText = "";
                    }

                    if (customerInfo.PrimaryContactID.HasValue)
                    {
                        actCustomerContact.SelectedValue = customerInfo.PrimaryContactID.ToString();
                        actCustomerContact.SetValue = customerInfo.PrimaryContactID.ToString();
                        //actCustomerContact.SelectedText = customerInfo.PrimaryFullContactInformation;
                    }
                    else
                    {
                        actCustomerContact.SetValue = "0";
                        actCustomerContact.SelectedText = "";
                    }

                    _presenter.ListAllDivision();

                    if (customerInfo.DivisionID != null && customerInfo.DivisionID.HasValue)
                        ddlDivision.SelectedValue = customerInfo.DivisionID.ToString();

                    if (customerInfo.POCEmployeeID != null && customerInfo.POCEmployeeID.HasValue)
                    {
                        actPOC.SelectedValue = customerInfo.POCEmployeeID.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Get the CustomerInfo Entity to the save function
        /// </summary>
        public CS_CustomerInfo CustomerInfoEntity
        {
            get
            {
                CS_CustomerInfo customerInfo = new CS_CustomerInfo();

                customerInfo.Active = true;
                customerInfo.CustomerId = int.Parse(actCustomer.SelectedValue);

                if (actEIC.SelectedValue != "0")
                    customerInfo.EicContactId = int.Parse(actEIC.SelectedValue);

                if (actAdditionalContact.SelectedValue != "0")
                    customerInfo.AdditionalContactId = int.Parse(actAdditionalContact.SelectedValue);

                if (actBillToContact.SelectedValue != "0")
                    customerInfo.BillToContactId = int.Parse(actBillToContact.SelectedValue);

                if (actCustomerContact.SelectedValue != "0")
                    customerInfo.InitialCustomerContactId = int.Parse(actCustomerContact.SelectedValue);

                if (ddlDivision.SelectedIndex > 0)
                    customerInfo.DivisionId = int.Parse(ddlDivision.SelectedValue);

                if (actPOC.SelectedValue != "0")
                {
                    string[] splitValues = actPOC.SelectedValue.Replace(" | ", "|").Split('|');

                    customerInfo.PocEmployeeId = int.Parse(splitValues[0]);
                }

                customerInfo.CreatedBy = ((ContentPage)Page.Master).Username;
                customerInfo.CreationDate = DateTime.Now;

                customerInfo.ModificationDate = DateTime.Now;
                customerInfo.ModifiedBy = ((ContentPage)Page.Master).Username;

                return customerInfo;
            }
        }

        /// <summary>
        /// Get and Set the JobId
        /// </summary>
        public int? JobId
        {
            get
            {
                return _jobId;
            }
            set
            {
                _jobId = value;
            }
        }

        /// <summary>
        /// Job id when cloning a job record
        /// </summary>
        public int? CloningId
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if the user has only read access to this page
        /// </summary>
        public bool ReadOnlyAccess
        {
            get
            {
                if (null == ViewState["ReadOnlyAccess"])
                    ViewState["ReadOnlyAccess"] = false;
                return Convert.ToBoolean(ViewState["ReadOnlyAccess"]);
            }
            set
            {
                ViewState["ReadOnlyAccess"] = value;
            }
        }

        #endregion

        #region [ Properties ]
        public int EmployeeId
        {
            get { return 0; }
        }

        public string DivisionClientID
        {
            get
            {
                return ddlDivision.ClientID;
            }
        }

        //public string CustomerContatRequiredFieldClientID
        //{
        //    get
        //    {
        //        return actCustomerContact.RequiredFieldClientId;
        //    }
        //}

        //public string CustomerHiddenFieldValueClientID
        //{
        //    get
        //    {
        //        return actCustomer.HiddenFieldValueClientID;
        //    }
        //}

        //public string CustomerEssentialHiddenFieldValueClientID
        //{
        //    set
        //    {
        //        hidCustomerEssentialHiddenFieldValueClientID.Value = value;
        //    }
        //}

        public void RequiredFieldValidatorActCustomer()
        {
            if (actCustomerContact.SelectedValue != string.Empty && actCustomerContact.SelectedValue != "0")
            {
                actCustomerContact.RequiredField = false;
            }
            else
            {
                actCustomerContact.RequiredField = true;
            }
        }
        #endregion
    }
}