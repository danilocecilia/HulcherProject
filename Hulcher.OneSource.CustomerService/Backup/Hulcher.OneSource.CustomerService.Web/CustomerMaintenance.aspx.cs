using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI.HtmlControls;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class CustomerMaintenance : System.Web.UI.Page, ICustomerMaintenanceView
    {
        #region [ Attributes ]

        CustomerMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new CustomerMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.VerifyAccess();
            _presenter.VerifyAccessCollection();

            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        protected void btnNewCustomerRequest_Click(object sender, EventArgs e)
        {
            _presenter.LoadScreenForNewCustomer();
        }

        protected void btnNewContactRequest_Click(object sender, EventArgs e)
        {
            _presenter.LoadScreenForNewContact();
        }

        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                SaveAndCloseClicked = false;
                _presenter.Save();
                uscCallCriteria.CallCriteriaEditable(true);
                //_presenter.BindGrid();

                if (EditingCustomer)
                {
                    if (SavedCustomer != null)
                    {
                        CustomerID = SavedCustomer.ID;
                        ItemCommandName = "EditCustomer";
                        _presenter.CustomerItemCommand();
                    }
                }
                else
                {
                    if (SavedContact != null)
                        ContactId = SavedContact.ID;

                    if (ContactId != 0)
                    {
                        _presenter.ContactItemCommand();
                        uscCallCriteria.BindCallCriteriaListing();
                    }
                }
            }
        }

        protected void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                SaveAndCloseClicked = true;
                _presenter.Save();
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.BindGrid();
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.BindGrid();
        }

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CustomerRowItem = e.Item;
                _presenter.BindCustomerRow();

            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                CustomerRowItem = e.Item;
                SetGridHeaderCssClass();
            }
        }

        protected void rptContact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ContactRowItem = e.Item;
                _presenter.BindContactRow();
            }
        }

        protected void rptCustomer_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            ItemCommandName = e.CommandName;
            CustomerRowItem = e.Item;
            CustomerID = CustomerRowCustomerID;
            ContactCustomerID = CustomerRowCustomerID;

            ExistingCustomer = true;

            _presenter.CustomerItemCommand();
        }

        protected void rptContact_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            ItemCommandName = e.CommandName;
            ContactRowItem = e.Item;
            ContactId = ContactRowContactID;

            ContactCustomerID = ContactRowCustomerID;

            if (ContactId != 0)
            {
                _presenter.ContactItemCommand();
                uscCallCriteria.BindCallCriteriaListing();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            uscCallCriteria.ClearFields();
            uscCallCriteria.ClearCallTypes();
            _presenter.Cancel();
        }

        #endregion

        #region [ Methods ]

        public void DisableCallCriteria()
        {

        }

        private void SetGridHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Customer:
                    control = CustomerRowItem.FindControl("thCustomer") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Contact:
                    control = CustomerRowItem.FindControl("thContact") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Type:
                    control = CustomerRowItem.FindControl("thType") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerMaintenanceSortColumns.Location:
                    control = CustomerRowItem.FindControl("thLocation") as HtmlTableCell;
                    break;
                default:
                    control = new HtmlTableCell();
                    break;
            }

            if (SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                control.Attributes.Add("class", control.Attributes["class"] + " Ascending");
            else
                control.Attributes.Add("class", control.Attributes["class"] + " Descending");
        }

        //private void EnableMasks(bool enable)
        //{
        //    mskAdditionalContactArea.Enabled = enable;
        //    mskAdditionalContactNumber.Enabled = enable;
        //    mskBillingContactFaxCode.Enabled = enable;
        //    mskBillingContactFaxPhone.Enabled = enable;
        //    mskBillingContactHomePhoneArea.Enabled = enable;
        //    mskBillingContactHomePhoneNumber.Enabled = enable;
        //    mskBillingCustomerFaxCode.Enabled = enable;
        //    mskBillingCustomerFaxPhone.Enabled = enable;
        //    mskBillingCustomerHomePhoneArea.Enabled = enable;
        //    mskBillingCustomerHomePhoneNumber.Enabled = enable;
        //    mskContactFaxCode.Enabled = enable;
        //    mskContactFaxPhone.Enabled = enable;
        //    mskContactHomePhoneArea.Enabled = enable;
        //    mskContactHomePhoneNumber.Enabled = enable;
        //    //mskCustomerFaxCode.Enabled = enable;
        //    //mskCustomerFaxPhone.Enabled = enable;
        //    mskCustomerHomePhoneArea.Enabled = enable;
        //    mskCustomerHomePhoneNumber.Enabled = enable;
        //}

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            this.Master.DisplayMessage(message, closeWindow);
        }

        public Globals.CustomerMaintenance.ViewType ViewType
        {
            get
            {
                switch (Request.QueryString["ViewType"].ToUpper())
                {
                    case "CUSTOMER":
                        lblPageTitle.Text = "Company Profile Maintenance";
                        lblHeader.Text = "Company Listing";
                        return Globals.CustomerMaintenance.ViewType.Customer;
                    case "CONTACT":
                        lblPageTitle.Text = "Contact Profile Maintenance";
                        lblHeader.Text = "Contact Listing";
                        Title = "Contact Profile Maintenance";
                        return Globals.CustomerMaintenance.ViewType.Contact;
                    default:
                        return Globals.CustomerMaintenance.ViewType.Error;
                }
            }
        }

        public string UserName
        {
            get { return this.Master.Username; }
        }

        public string Domain
        {
            get { return this.Master.Domain; }
        }

        public bool ReadOnly
        {
            set
            {
                if (value)
                {
                    pnlFullAccess.Visible = false;
                    pnlNoAccess.Visible = true;
                }
            }
        }

        public bool NewCustomer
        {
            get
            {
                if (null == ViewState["NewCustomer"])
                {
                    if (null != Request.QueryString["NewCustomer"])
                        ViewState["NewCustomer"] = bool.Parse(Request.QueryString["NewCustomer"]);
                    else
                        ViewState["NewCustomer"] = false;
                }

                return Convert.ToBoolean(ViewState["NewCustomer"]);
            }
            set { ViewState["NewCustomer"] = value; }
        }

        public bool NewContact
        {
            get
            {
                if (null == ViewState["NewContact"])
                {
                    if (null != Request.QueryString["NewContact"])
                        ViewState["NewContact"] = bool.Parse(Request.QueryString["NewContact"]);
                    else
                        ViewState["NewContact"] = false;
                }

                return Convert.ToBoolean(ViewState["NewContact"]);
            }
            set { ViewState["NewContact"] = value; }
        }

        public bool EditingContact
        {
            get
            {
                if (null != ViewState["EditingContact"])
                    return Convert.ToBoolean(ViewState["EditingContact"]);

                return false;
            }
            set
            {
                ViewState["EditingContact"] = value;
            }
        }

        public bool EditingCustomer
        {
            get
            {
                if (null != ViewState["EditingCustomer"])
                    return Convert.ToBoolean(ViewState["EditingCustomer"]);

                return false;
            }
            set
            {
                ViewState["EditingCustomer"] = value;
            }
        }

        public bool ShowCustomerSection
        {
            set
            {
                if (null != Request.QueryString["NewCustomer"])
                {
                    btnSaveAndContinue.Visible = false;
                    btnCancel.Visible = false;
                }

                if (value)
                {
                    pnlCustomerFields.Style.Add("display", "");
                    divControlCustomer.Disabled = false;
                    actCustomerSelection.Enabled = false;
                }
                else
                {
                    pnlCustomerFields.Style.Add("display", "none");
                    divControlCustomer.Disabled = true;
                }
            }
        }

        public bool ShowContactSection
        {
            set
            {
                if (null != Request.QueryString["NewContact"])
                {
                    btnSaveAndContinue.Visible = false;
                    btnCancel.Visible = false;
                }

                if (value)
                {
                    pnlContactFields.Style.Add("display", "");
                    divControlContact.Disabled = false;
                    actCustomerSelection.Enabled = true;
                }
                else
                {
                    pnlContactFields.Style.Add("display", "none");
                    divControlContact.Disabled = true;
                }
            }
        }

        public bool ShowGridSection
        {
            set
            {
                pnlSelection.Visible = value;

                pnlSelectionCustomer.Visible = (ViewType == Globals.CustomerMaintenance.ViewType.Customer);
                pnlSelectionContact.Visible = (ViewType == Globals.CustomerMaintenance.ViewType.Contact);

                if (value)
                    divControl.Style.Add("display", "none");
                else
                    divControl.Style.Add("display", "");

                btnCancel.Visible = !value;
            }
        }

        public string PhoneValidatorValidationGroup
        {
            set
            {
                fakeValidator.ValidationGroup = value;
                fakeWorkPhoneValidator.ValidationGroup = value;
                fakeFaxPhoneValidator.ValidationGroup = value;
                fakeBWorkPhoneValidator.ValidationGroup = value;
                fakeBFaxPhoneValidator.ValidationGroup = value;
                fakePhoneListValidator.ValidationGroup = value;
            }
        }

        public string SaveButtonsValidationGroup
        {
            set
            {
                btnSave.ValidationGroup = value;
                btnSaveAndContinue.ValidationGroup = value;
            }
        }

        public string SaveButtonsOnClientClick
        {
            set
            {
                btnSave.OnClientClick = string.Format(value, btnSave.UniqueID);
                btnSaveAndContinue.OnClientClick = string.Format(value, btnSaveAndContinue.UniqueID);
            }
        }

        public bool SaveAndCloseClicked
        {
            get;
            set;
        }

        public string SaveMessage
        {
            get;
            set;
        }

        public CS_Customer SavedCustomer
        {
            get
            {
                if (ViewState["SavedCustomer"] != null)
                    return ViewState["SavedCustomer"] as CS_Customer;

                return null;
            }
            set
            {
                ViewState["SavedCustomer"] = value;

                if (!string.IsNullOrEmpty(this.RefField))
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "SetOpenerFields", string.Format("if (window.opener) window.opener.SetAutocompleteField('{0}', '{1}', '{2}');", value.FullCustomerInformation, value.ID, this.RefField), true);
            }
        }

        public CS_Contact SavedContact
        {
            get
            {
                if (ViewState["SavedContact"] != null)
                    return ViewState["SavedContact"] as CS_Contact;

                return null;
            }
            set
            {
                ViewState["SavedContact"] = value;

                if (!string.IsNullOrEmpty(this.RefField))
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "SetOpenerFields", string.Format("if (window.opener) window.opener.SetAutocompleteField('{0}', '{1}', '{2}');", value.FullContactInformation, value.ID, this.RefField), true);
            }
        }

        public string RefField
        {
            get
            {
                if (null != Request.QueryString["RefField"])
                    return Request.QueryString["RefField"].ToString();

                return string.Empty;
            }
        }

        public bool? BillToContact
        {
            get
            {
                if (null != Request.QueryString["BillToContact"])
                    return bool.Parse(Request.QueryString["BillToContact"]);

                return null;
            }
        }

        #endregion

        #region [ Dashboard ]

        public Globals.CustomerMaintenance.FilterType FilterType
        {
            get { return (Globals.CustomerMaintenance.FilterType)Convert.ToInt32(ddlFilter.SelectedValue); }
        }

        public string FilterValue
        {
            get { return txtFilterValue.Text; }
        }

        #region [ Sort ]

        public string[] OrderBy
        {
            get
            {
                return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Globals.Common.Sort.CustomerMaintenanceSortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.CustomerMaintenanceSortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Common.Sort.CustomerMaintenanceSortColumns)result;
            }
        }

        public Globals.Common.Sort.SortDirection SortDirection
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.SortDirection.Ascending;

                return (Globals.Common.Sort.SortDirection)Int32.Parse(OrderBy[1]);
            }
        }

        #endregion

        #region [ Customer ]

        public IList<CS_Customer> CustomerList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value.Count == 0)
                {
                    pnlNoRows.Visible = true;
                    rptCustomer.Visible = false;
                }
                else
                {
                    if (value.Count > 300)
                    {
                        pnlFilterExced.Visible = true;
                        pnlNoRows.Visible = true;
                        rptCustomer.Visible = false;
                    }
                    else
                    {
                        pnlFilterExced.Visible = false;
                        pnlNoRows.Visible = false;
                        rptCustomer.Visible = true;
                        rptCustomer.DataSource = value;
                        rptCustomer.DataBind();
                    }
                }
            }
        }

        public CS_Customer CustomerRowDataItem
        {
            get
            {
                return CustomerRowItem.DataItem as CS_Customer;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private RepeaterItem CustomerRowItem
        {
            get;
            set;
        }

        #region [ Fields ]

        public string CustomerRowCustomerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCustomer = CustomerRowItem.FindControl("lblCustomer") as Label;

                if (null != lblCustomer)
                {
                    lblCustomer.Text = value;
                }
            }
        }

        public int CustomerRowCustomerID
        {
            get
            {
                HiddenField hfCustomerID = CustomerRowItem.FindControl("hfCustomerID") as HiddenField;

                if (null != hfCustomerID)
                {
                    int customerID;
                    int.TryParse(hfCustomerID.Value, out customerID);

                    return customerID;
                }

                return 0;
            }
            set
            {
                HiddenField hfCustomerID = CustomerRowItem.FindControl("hfCustomerID") as HiddenField;

                if (null != hfCustomerID)
                {
                    hfCustomerID.Value = value.ToString();
                }

                Button btnEdit = (Button)CustomerRowItem.FindControl("btnEdit");

                if (btnEdit != null)
                {
                    btnEdit.CommandArgument = value.ToString();
                }

                HtmlGenericControl divExpand = CustomerRowItem.FindControl("divExpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (hfExpandedItems.Value.Contains(value.ToString() + ";"))
                        divExpand.Attributes["class"] = "Collapse";
                }
            }
        }

        public bool CustomerRowHasContacts
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                //HtmlGenericControl divExpand = CustomerRowItem.FindControl("divExpand") as HtmlGenericControl;

                //if (null != divExpand)
                //{
                //    if (value)
                //    {
                //        divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','" + CustomerRowDataItem.ID.ToString() + "');");
                //        divExpand.Visible = true;
                //    }
                //    else
                //        divExpand.Visible = false;
                //}
            }

        }

        #endregion

        #endregion

        #region [ Contacts ]

        public IList<CS_Contact> AllContactsList { get; set; }

        public IList<CS_Contact> ContactList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value.Count == 0)
                {
                    pnlNoRows.Visible = true;
                    rptContact.Visible = false;
                }
                else
                {
                    if (value.Count > 300)
                    {
                        pnlFilterExced.Visible = true;
                        pnlNoRows.Visible = true;
                        rptContact.Visible = false;
                    }
                    else
                    {
                        pnlFilterExced.Visible = false;
                        pnlNoRows.Visible = false;
                        rptContact.Visible = true;
                        rptContact.DataSource = value;
                        rptContact.DataBind();
                    }
                }
            }
        }

        public CS_Contact ContactRowDataItem
        {
            get
            {
                return ContactRowItem.DataItem as CS_Contact;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private RepeaterItem ContactRowItem
        {
            get;
            set;
        }

        #region [ Fields ]

        public int ContactRowCustomerID
        {
            get
            {
                HiddenField hfCustomerID = ContactRowItem.FindControl("hfCustomerID") as HiddenField;

                if (null != hfCustomerID)
                {
                    int customerID;
                    int.TryParse(hfCustomerID.Value, out customerID);

                    return customerID;
                }

                return 0;
            }
            set
            {
                HiddenField hfCustomerID = ContactRowItem.FindControl("hfCustomerID") as HiddenField;

                if (null != hfCustomerID)
                {
                    hfCustomerID.Value = value.ToString();
                }

                HtmlTableRow trContact = ContactRowItem.FindControl("tr2") as HtmlTableRow;
                if (null != trContact)
                {
                    trContact.Attributes["class"] += " " + value.ToString();

                    //if (!hfExpandedItems.Value.Contains(value.ToString() + ";"))
                    //    trContact.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //else
                    //    trContact.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public string ContactRowCustomerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCustomerName = ContactRowItem.FindControl("lblCustomerName") as Label;

                if (null != lblCustomerName)
                {
                    lblCustomerName.Text = value.ToString();
                }
            }
        }

        public string ContactRowContactName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblContact = ContactRowItem.FindControl("lblContact") as Label;

                if (null != lblContact)
                {
                    lblContact.Text = value;
                }
            }
        }

        public int ContactRowContactID
        {
            get
            {
                HiddenField hfContactID = ContactRowItem.FindControl("hfContactID") as HiddenField;

                if (null != hfContactID)
                {
                    int contactID;
                    int.TryParse(hfContactID.Value, out contactID);

                    return contactID;
                }

                return 0;
            }
            set
            {
                HiddenField hfContactID = ContactRowItem.FindControl("hfContactID") as HiddenField;

                if (null != hfContactID)
                {
                    hfContactID.Value = value.ToString();
                }

                Button btnEdit = (Button)ContactRowItem.FindControl("btnEdit");

                if (btnEdit != null)
                {
                    btnEdit.CommandArgument = value.ToString();
                }
            }
        }

        public string ContactRowType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblType = ContactRowItem.FindControl("lblType") as Label;

                if (null != lblType)
                {
                    lblType.Text = value;
                }
            }
        }

        public string ContactRowLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblLocation = ContactRowItem.FindControl("lblLocation") as Label;

                if (null != lblLocation)
                {
                    lblLocation.Text = value;
                }
            }
        }

        #endregion

        #endregion

        #region [ Commands ]

        public string ItemCommandName { get; set; }

        #endregion

        #endregion

        #region [ Customer Section ]

        public int? CustomerID
        {
            get
            {
                if (null != ViewState["CustomerID"])
                {
                    return int.Parse(ViewState["CustomerID"].ToString());
                }
                else if (null != Request.QueryString["CustomerID"])
                {
                    ViewState["CustomerID"] = Request.QueryString["CustomerID"];

                    return int.Parse(Request.QueryString["CustomerID"]);
                }

                return null;
            }
            set
            {
                ViewState["CustomerID"] = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return txtCustomerName.Text;
            }
            set
            {
                txtCustomerName.Text = value;
            }
        }

        public string CustomerAttn
        {
            get
            {
                return txtCustomerAttn.Text;
            }
            set
            {
                txtCustomerAttn.Text = value;
            }
        }

        public string CustomerAddress1
        {
            get
            {
                return txtCustomerAddress.Text;
            }
            set
            {
                txtCustomerAddress.Text = value;
            }
        }

        public string CustomerAddress2
        {
            get
            {
                return txtCustomerAddress2.Text;
            }
            set
            {
                txtCustomerAddress2.Text = value;
            }
        }

        public string CustomerState
        {
            get
            {
                return txtCustomerStateProvinceCode.Text;
            }
            set
            {
                txtCustomerStateProvinceCode.Text = value;
            }
        }

        public string CustomerCity
        {
            get
            {
                return txtCustomerCity.Text;
            }
            set
            {
                txtCustomerCity.Text = value;
            }
        }

        public string CustomerCountry
        {
            get
            {
                return ddlCountryCode.SelectedValue;
            }
            set
            {
                string[] values = { "US", "CA", "MX" };

                if (string.IsNullOrEmpty(value) || !values.Contains(value))
                    value = "US";

                ddlCountryCode.SelectedValue = value;
            }
        }

        public string CustomerZipCode
        {
            get
            {
                return txtCustomerPostalCode.Text;
            }
            set
            {
                txtCustomerPostalCode.Text = value;
            }
        }

        public string CustomerHomePhoneCodeArea
        {
            get;
            set;
        }

        public string CustomerHomePhone
        {
            get
            {
                return txtCustomerHomePhoneNumber.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""); ;
            }
            set
            {
                txtCustomerHomePhoneNumber.Text = value;
            }
        }

        public string CustomerFaxPhoneAreaCode
        {
            get;
            set;
        }

        public string CustomerFaxPhone
        {
            get
            {
                return txtCustomerFaxPhone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""); ;
            }
            set
            {
                txtCustomerFaxPhone.Text = value;
            }
        }

        public string CustomerBillingName
        {
            get
            {
                return txtCustomerBillingName.Text;
            }
            set
            {
                txtCustomerBillingName.Text = value;
            }
        }

        public string CustomerBillingAddress1
        {
            get
            {
                return txtCustomerBillingAddress.Text;
            }
            set
            {
                txtCustomerBillingAddress.Text = value;
            }
        }

        public string CustomerBillingAddress2
        {
            get
            {
                return txtCustomerBillingAddress2.Text;
            }
            set
            {
                txtCustomerBillingAddress2.Text = value;
            }
        }

        public string CustomerBillingAttn
        {
            get
            {
                return txtCustomerBillingAttn.Text;
            }
            set
            {
                txtCustomerBillingAttn.Text = value;
            }
        }

        public string CustomerBillingState
        {
            get
            {
                return txtCustomerBillingStateProvinceCode.Text;
            }
            set
            {
                txtCustomerBillingStateProvinceCode.Text = value;
            }
        }

        public string CustomerBillingCity
        {
            get
            {
                return txtCustomerBillingCity.Text;
            }
            set
            {
                txtCustomerBillingCity.Text = value;
            }
        }

        public string CustomerBillingCountry
        {
            get
            {
                return ddlCustomerBillingCountryCode.SelectedValue;
            }
            set
            {
                string[] values = { "US", "CA", "MX" };

                if (string.IsNullOrEmpty(value) || !values.Contains(value))
                    value = "US";

                ddlCustomerBillingCountryCode.SelectedValue = value;
            }
        }

        public string CustomerBillingZipCode
        {
            get
            {
                return txtCustomerBillingPostalCode.Text;
            }
            set
            {
                txtCustomerBillingPostalCode.Text = value;
            }
        }

        public string CustomerBillingHomePhoneAreaCode
        {
            get;
            set;
        }

        public string CustomerBillingHomePhone
        {
            get
            {
                return txtBillingCustomerHomePhoneNumber.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""); ;
            }
            set
            {
                txtBillingCustomerHomePhoneNumber.Text = value;
            }
        }

        public string CustomerBillingFaxPhoneCodeArea
        {
            get;
            set;
        }

        public string CustomerBillingFaxPhone
        {
            get
            {
                return txtBillingCustomerFaxPhone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""); ;
            }
            set
            {
                txtBillingCustomerFaxPhone.Text = value;
            }
        }

        public string CustomerBillingSalutation
        {
            get
            {
                return txtCustomerBillingSalutation.Text;
            }
            set
            {
                txtCustomerBillingSalutation.Text = value;
            }
        }

        public short? CustomerThruProject
        {
            get
            {
                if (!string.IsNullOrEmpty(txtCustomerThruProjectt.Text))
                    return short.Parse(txtCustomerThruProjectt.Text);
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    txtCustomerThruProjectt.Text = value.Value.ToString();
                else
                    txtCustomerThruProjectt.Text = string.Empty;
            }
        }

        public string CustomerEmail
        {
            get
            {
                return txtCustomerEmail.Text;
            }
            set
            {
                txtCustomerEmail.Text = value;
            }
        }

        public string CustomerWebpage
        {
            get
            {
                return txtCustomerWebPage.Text;
            }
            set
            {
                txtCustomerWebPage.Text = value;
            }
        }

        public string CustomerIMAddress
        {
            get
            {
                return txtCustomerIMAddress.Text;
            }
            set
            {
                txtCustomerIMAddress.Text = value;
            }
        }

        public string CustomerAlertNotification
        {
            get
            {
                return txtNotes.Text;
            }
            set
            {
                txtNotes.Text = value;
            }
        }

        public bool CustomerOperatorAlert
        {
            get
            {
                return chkOperatorAlert.Checked;
            }
            set
            {
                chkOperatorAlert.Checked = value;
            }
        }

        public bool CustomerCreditCheck
        {
            get
            {
                return chkCustomerCreditCheck.Checked;
            }
            set
            {
                chkCustomerCreditCheck.Checked = value;
            }
        }

        public bool CustomerCollection
        {
            get
            {
                return chkIsCollection.Checked;
            }

            set
            {
                chkIsCollection.Checked = value;
            }
        }

        public bool CustomerRequestWarning
        {
            set
            {
                pnlCustomerWarning.Visible = value;
            }
        }

        public IList<int> selectedCustomerSpecificInfoType
        {
            get
            {
                List<int> returnList = new List<int>();

                for (int i = 0; i < cblCustomerSpecificInfoType.Items.Count; i++)
                {
                    if (cblCustomerSpecificInfoType.Items[i].Selected)
                    {
                        returnList.Add(int.Parse(cblCustomerSpecificInfoType.Items[i].Value));
                    }
                }

                return returnList;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    ListItem item = cblCustomerSpecificInfoType.Items.FindByValue(value[i].ToString());

                    if (null != item)
                        item.Selected = true;
                }
            }
        }

        public IList<CS_CustomerSpecificInfoType> customerSpecificInfoTypeList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                cblCustomerSpecificInfoType.DataSource = value;
                cblCustomerSpecificInfoType.DataBind();
            }
        }

        public bool CustomerCollectionPermission
        {
            set
            {
                pnlIsCollection.Visible = value;
            }

        }
        #endregion

        #region [ Contact Section ]

        public int? ContactId
        {
            get
            {
                if (null != ViewState["ContactID"])
                {
                    return int.Parse(ViewState["ContactID"].ToString());
                }
                else if (null != Request.QueryString["ContactID"])
                {
                    ViewState["ContactID"] = Request.QueryString["ContactID"];

                    return int.Parse(Request.QueryString["ContactID"]);
                }

                return null;
            }
            set
            {
                ViewState["ContactID"] = value;
            }
        }

        public int ContactCustomerID
        {
            get
            {
                return int.Parse(actCustomerSelection.SelectedValue);
            }
            set
            {
                actCustomerSelection.ClearSelection();
                actCustomerSelection.SelectedValue = value.ToString();
                divSelectCustomer.Disabled = (!value.Equals(0));
                actCustomerSelection.HasSearchButton = (value.Equals(0));
            }
        }

        public bool? ContactType
        {
            get
            {
                if (ddlContactType.SelectedValue.Equals("0"))
                    return null;

                return bool.Parse(ddlContactType.SelectedValue);
            }
            set
            {
                if (value.HasValue)
                    ddlContactType.SelectedValue = value.ToString();
                else
                    ddlContactType.SelectedValue = false.ToString();

                ddlContactType.Enabled = (!value.HasValue);

                if (value.HasValue)
                    ContactTypeDiv.Visible = value.Value;
            }
        }

        public List<CS_PhoneType> AdditionalContactPhoneTypeSource
        {
            set
            {
                ddlAdditionalContactType.DataSource = value;
                ddlAdditionalContactType.DataBind();
                ddlAdditionalContactType.SelectedValue = Convert.ToInt32(Globals.Phone.PhoneType.Work).ToString();
            }
        }

        public string ContactName
        {
            get
            {
                return txtContactName.Text;
            }
            set
            {
                txtContactName.Text = value;
            }
        }

        public string ContactLastName
        {
            get
            {
                return txtContactLastName.Text;
            }
            set
            {
                txtContactLastName.Text = value;
            }
        }

        public string ContactAlias
        {
            get { return txtContactAlias.Text; }
            set { txtContactAlias.Text = value; }
        }

        public string ContactNumber
        {
            get
            {
                return txtContactContactNumber.Text;
            }
            set
            {
                txtContactContactNumber.Text = value;
            }
        }

        public string ContactAttn
        {
            get
            {
                return txtContactAttn.Text;
            }
            set
            {
                txtContactAttn.Text = value;
            }
        }

        public string ContactAddress
        {
            get
            {
                return txtContactAddress.Text;
            }
            set
            {
                txtContactAddress.Text = value;
            }
        }

        public string ContactAddress2
        {
            get
            {
                return txtContactAddress2.Text;
            }
            set
            {
                txtContactAddress2.Text = value;
            }
        }

        public string ContactState
        {
            get
            {
                return txtContactStateProvinceCode.Text;
            }
            set
            {
                txtContactStateProvinceCode.Text = value;
            }
        }

        public string ContactCity
        {
            get
            {
                return txtContactCity.Text;
            }
            set
            {
                txtContactCity.Text = value;
            }
        }

        public string ContactCountry
        {
            get { return ddlContactCountryCode.SelectedValue; }
            set
            {
                string[] values = { "US", "CA", "MX" };

                if (string.IsNullOrEmpty(value) || !values.Contains(value))
                    value = "US";

                ddlContactCountryCode.SelectedValue = value;
            }
        }

        public string ContactZipcode
        {
            get
            {
                return txtContactPostalCode.Text;
            }
            set
            {
                txtContactPostalCode.Text = value;
            }
        }

        public string ContactHomePhoneCodeArea
        {
            get;

            set;

        }

        public string ContactHomePhone
        {
            get
            {
                if (hfPhoneNumbers.Value != string.Empty)
                {
                    string formatedNumber = string.Empty;

                    string[] workPhone = hfPhoneNumbers.Value.Split('|');

                    for (int i = 0; i < workPhone.Count(); i++)
                    {
                        if (workPhone[i].Contains("Work"))
                        {
                            hfPhoneNumbers.Value = hfPhoneNumbers.Value.Replace(workPhone[i].ToString(), string.Empty);

                            string[] phone = workPhone[i].Split(';');

                            for (int j = 2; j < phone.Count(); j++)
                            {
                                if (phone[j].Contains("ext"))
                                {
                                    string format = phone[j].Replace("ext", ",");

                                    string[] number = format.Split(',');

                                    formatedNumber += number[0].Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                                    formatedNumber += " ext " + number[1].Trim();
                                }
                                else
                                {
                                    formatedNumber += phone[j].Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                                }
                            }
                            return formatedNumber;
                        }
                    }
                }
                return string.Empty;
                // return txtContactHomePhoneNumber.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            }
            set
            {
                txtContactHomePhoneNumber.Text = value;
            }
        }

        public string ContactFaxPhoneCodeArea
        {
            get;
            set;
        }

        public string ContactFaxPhone
        {
            get
            {
                if (hfPhoneNumbers.Value != string.Empty)
                {
                    string formatedNumber = string.Empty;

                    string[] workPhone = hfPhoneNumbers.Value.Split('|');

                    for (int i = 0; i < workPhone.Count(); i++)
                    {
                        if (workPhone[i].Contains("Fax"))
                        {
                            hfPhoneNumbers.Value = hfPhoneNumbers.Value.Replace(workPhone[i].ToString(), string.Empty);

                            string[] phone = workPhone[i].Split(';');

                            for (int j = 2; j < phone.Count(); j++)
                            {
                                formatedNumber += phone[j].Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                            }
                            return formatedNumber;
                        }
                    }
                }
                return string.Empty;
                // return txtContactHomePhoneNumber.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            }
            set
            {
                txtContactFaxPhone.Text = value;
            }
        }

        public string ContactBillingName
        {
            get
            {
                return txtContactBillingName.Text;
            }
            set
            {
                txtContactBillingName.Text = value;
            }
        }

        public string ContactBillingAddress
        {
            get
            {
                return txtContactBillingAddress.Text;
            }
            set
            {
                txtContactBillingAddress.Text = value;
            }
        }

        public string ContactBillingAddress2
        {
            get
            {
                return txtContactBillingAddress2.Text;
            }
            set
            {
                txtContactBillingAddress2.Text = value;
            }
        }

        public string ContactBillingAttn
        {
            get
            {
                return txtContactBillingAttn.Text;
            }
            set
            {
                txtContactBillingAttn.Text = value;
            }
        }

        public string ContactBillingState
        {
            get
            {
                return txtContactBillingStateProvinceCode.Text;
            }
            set
            {
                txtContactBillingStateProvinceCode.Text = value;
            }
        }

        public string ContactBillingCity
        {
            get
            {
                return txtContactBillingCity.Text;
            }
            set
            {
                txtContactBillingCity.Text = value;
            }
        }

        public string ContactBillingCountry
        {
            get
            {
                return ddlContactBillingCountryCode.SelectedValue;
            }
            set
            {
                string[] values = { "US", "CA", "MX" };

                if (string.IsNullOrEmpty(value) || !values.Contains(value))
                    value = "US";

                ddlContactBillingCountryCode.SelectedValue = value;
            }
        }

        public string ContactBillingZipcode
        {
            get
            {
                return txtContactBillingPostalCode.Text;
            }
            set
            {
                txtContactBillingPostalCode.Text = value;
            }
        }

        public string ContactBillingHomePhoneCodeArea
        {
            get;
            set;
        }

        public string ContactBillingHomePhone
        {
            get
            {
                return txtBillingContactHomePhoneNumber.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            }
            set
            {
                txtBillingContactHomePhoneNumber.Text = value;
            }
        }

        public string ContactBillingFaxPhoneCodeArea
        {
            get;
            set;
        }

        public string ContactBillingFaxPhone
        {
            get
            {
                return txtBillingContactFaxPhone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            }
            set
            {
                txtBillingContactFaxPhone.Text = value;
            }
        }

        public string ContactBillingSalutation
        {
            get
            {
                return txtCustomerBillingSalutation.Text;
            }
            set
            {
                txtCustomerBillingSalutation.Text = value;
            }
        }

        //public short? ContactBillingThruProject
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(txtContactThruProjectt.Text))
        //        {
        //            return short.Parse(txtContactThruProjectt.Text);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            txtContactThruProjectt.Text = value.Value.ToString();
        //        }
        //        else
        //            txtContactThruProjectt.Text = string.Empty;
        //    }
        //}

        public string ContactEmail
        {
            get
            {
                return txtContactEmail.Text;
            }
            set
            {
                txtContactEmail.Text = value;
            }
        }

        public string ContactWebpage
        {
            get
            {
                return txtContactWebPage.Text;
            }
            set
            {
                txtContactWebPage.Text = value;
            }
        }

        public string ContactIMAddress
        {
            get
            {
                return txtContactIMAddress.Text;
            }
            set
            {
                txtContactIMAddress.Text = value;
            }
        }

        public bool ContactRequestWarning
        {
            set
            {
                pnlWarning.Visible = value;
            }
        }

        public IList<CS_PhoneNumber> AdditionalContactPhoneGridDataSource
        {
            set
            {
                hfPhoneNumbers.Value = string.Empty;

                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        CS_PhoneNumber number = value[i];
                        string phoneToAdd = string.Format("{0};{1};{2}", number.ID, number.CS_PhoneType.Name, number.Number);

                        if (hfPhoneNumbers.Value.Equals(string.Empty))
                        {
                            hfPhoneNumbers.Value += phoneToAdd;
                        }
                        else
                        {
                            hfPhoneNumbers.Value += string.Format("|{0}", phoneToAdd);
                        }
                    }

                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "createTable", "CreatePhoneGrid();", true);
                }
                else
                    hfPhoneNumbers.Value = string.Empty;
            }
        }

        public List<PhoneNumberVO> AdditionalContactPhoneList
        {
            get
            {
                List<PhoneNumberVO> returnList = new List<PhoneNumberVO>();

                if (!string.IsNullOrEmpty(hfPhoneNumbers.Value))
                {
                    string[] phoneNumbers = hfPhoneNumbers.Value.Split('|');

                    for (int j = 0; j < phoneNumbers.Count(); j++)
                    {
                        if (phoneNumbers[j].Contains("Work"))
                            phoneNumbers[j] = phoneNumbers[j].Replace(phoneNumbers[j].ToString(), string.Empty);

                        if (phoneNumbers[j].Contains("Fax"))
                            phoneNumbers[j] = phoneNumbers[j].Replace(phoneNumbers[j].ToString(), string.Empty);
                    }


                    for (int i = 0; i < phoneNumbers.Length; i++)
                    {
                        string[] values = phoneNumbers[i].Split(';');

                        PhoneNumberVO newPhone = new PhoneNumberVO();

                        if (phoneNumbers[i] != string.Empty)
                        {
                            if (!values[0].Equals("0"))
                                newPhone.ID = int.Parse(values[0]);

                            newPhone.TypeName = values[1];
                            newPhone.Number = values[2];

                            switch (values[1])
                            {
                                case "Home":
                                    newPhone.TypeID = (int)Globals.Phone.PhoneType.Home;
                                    break;
                                case "Cell":
                                    newPhone.TypeID = (int)Globals.Phone.PhoneType.Cell;
                                    break;
                                case "Fax":
                                    newPhone.TypeID = (int)Globals.Phone.PhoneType.Fax;
                                    break;
                            }

                            returnList.Add(newPhone);
                        }
                    }
                }

                return returnList;
            }
        }


        public int? CallCriteriaContactID
        {
            get
            {
                return uscCallCriteria.ContactID;
            }
            set { uscCallCriteria.ContactID = value; }
        }

        #endregion

        #endregion


        public bool EnableCallCriteria
        {
            set
            {
                uscCallCriteria.CallCriteriaGroup(value);
            }
        }

        public bool ExistingCustomer
        {
            get
            {
                return Convert.ToBoolean(hidIsEditCompany.Value);
            }
            set
            {
                hidIsEditCompany.Value = string.IsNullOrEmpty(value.ToString()) ? "false" : "true";
            }
        }

    }
}
