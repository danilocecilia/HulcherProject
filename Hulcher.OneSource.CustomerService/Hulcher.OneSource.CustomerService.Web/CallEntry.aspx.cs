using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

using AjaxControlToolkit;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class CallEntry : System.Web.UI.Page, ICallEntryView
    {
        #region [ Atributes ]

        /// <summary>
        /// CallEntry Presenter object
        /// </summary>
        CallEntryPresenter _presenter;

        /// <summary>
        /// Selected CallTypeID
        /// </summary>
        private int? _callTypeId;

        /// <summary>
        /// Call log entity
        /// </summary>
        private CS_CallLog _callLog;

        /// <summary>
        /// Call Type entity
        /// </summary>
        private CS_CallType _callType;

        /// <summary>
        /// Formatter object to Compress/Decompress the ViewState
        /// </summary>
        private ObjectStateFormatter _formatter = new ObjectStateFormatter();

        #endregion

        #region [ Override ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new CallEntryPresenter(this);

            if (null != this.SelectedCallType)
            {
                _presenter.CreateDynamicFields();
            }
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            _formatter.Serialize(ms, viewState);
            byte[] viewStateArray = ms.ToArray();
            ScriptManager.RegisterHiddenField(this, "__COMPRESSEDVIEWSTATE", Convert.ToBase64String(WebUtil.Compress(viewStateArray)));
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            string vsString = Request.Form["__COMPRESSEDVIEWSTATE"];
            byte[] bytes = Convert.FromBase64String(vsString);
            bytes = WebUtil.Decompress(bytes);
            return _formatter.Deserialize(Convert.ToBase64String(bytes));
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //uscEqRequested.Visible = false;
                dpCallDate.Value = DateTime.Now;
                txtCallTime.Text = string.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);

                if (IsHistoryUpdate)
                    btnSaveContinue.Visible = false;
                if (!string.IsNullOrEmpty(Request.QueryString["RefreshUniqueID"]))
                {
                    RefreshUniqueID = Request.QueryString["RefreshUniqueID"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["JobId"]))
                {
                    hfJobId.Value = Request.QueryString["JobId"];
                    int jobId = Int32.Parse(Request.QueryString["JobId"]);
                    if (jobId == Globals.GeneralLog.ID)
                        LoadPageWithGeneralLogJob(false);
                    else
                        LoadPageWithJob(jobId, false);

                    if (!string.IsNullOrEmpty(Request.QueryString["CallEntryID"]))
                    {
                        CallID = Convert.ToInt32(Request.QueryString["CallEntryID"]);
                        JobId = Convert.ToInt32(Request.QueryString["JobId"]);

                        _presenter.ListAddedResource();
                        _presenter.ListAllAddedContacts();

                        _presenter.LoadCallLog();
                        _presenter.LoadCallLogEquipmentType();

                        if (null != _callType && _callType.CallCriteria)
                        {
                            _presenter.SetInitialAdvisePanels();
                            _presenter.ListAllEmployeeCallLogInfoCallCriteria();
                            _presenter.ListInitialAdiviseCallLogs();
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckChanges",
                            string.Format("window.setTimeout('doBeforeSave(\"{0}\", \"{1}\")', 1000);", hfSave.ClientID, ckbCopyGeneralLog.ClientID),
                            true);
                    }

                    if (!string.IsNullOrEmpty(Request.QueryString["CallTypeID"]))
                    {
                        ddlCallType.SelectedValue = Request.QueryString["CallTypeID"].ToString();
                        ddlCallType_SelectedIndexChanged(ddlCallType, new EventArgs());
                        ddlCallType.Enabled = false;
                    }
                }
                else
                {
                    //Following business requirements, the Job Record Grid
                    //should be displayed initially
                    rbJobRecord.Checked = true;
                    rbJobRecord_CheckedChanged(rbJobRecord, new EventArgs());

                    ReadOnlyPage = true;
                }

                _presenter.GetPageTitle();
            }
            else
            {

                if (hfSave.Value != string.Empty)
                {
                    if (hfSave.Value == "1")
                    {
                        ckbCopyGeneralLog.Checked = true;
                        ckbCopyGeneralLog.Enabled = true;
                    }
                    else
                    {
                        ckbCopyGeneralLog.Checked = false;
                        ckbCopyGeneralLog.Enabled = true;
                    }
                }
            }
        }

        #region [ Job Search ]

        protected void rbGeneralLog_CheckedChanged(object sender, EventArgs e)
        {
            ckbCopyGeneralLog.Visible = false;
            LoadPageWithGeneralLogJob(false);
            _presenter.GetPageTitle();

            ScriptManager.GetCurrent(this).SetFocus(actCalledInEmployee.TextControlClientID);
        }

        protected void rbJobRecord_CheckedChanged(object sender, EventArgs e)
        {
            gvJobFilter.DataBind();
            phFindJob.Visible = true;

            ScriptManager.GetCurrent(this).SetFocus(ddlFilter);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty)
                _presenter.GetJobFilterGridInfo();

            ScriptManager.GetCurrent(this).SetFocus(btnFind);
        }

        protected void gvJobFilter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnSelect = e.Row.FindControl("btnSelect") as Button;
                Panel pnlJobFilter = e.Row.FindControl("pnlJobFilter") as Panel;
                pnlJobFilter.DefaultButton = btnSelect.ID;

                CS_Job job = e.Row.DataItem as CS_Job;
                if (job != null)
                {
                    //Division
                    List<CS_JobDivision> divisionList = job.CS_JobDivision.ToList();
                    foreach (CS_JobDivision division in divisionList)
                    {
                        if (division.PrimaryDivision)
                        {
                            e.Row.Cells[1].Text = division.CS_Division.Name;
                            break;
                        }
                    }

                    //JobNumber
                    e.Row.Cells[2].Text = job.PrefixedNumber;

                    //Status
                    if (null != job.CS_JobInfo && null != job.CS_JobInfo.LastJobStatus)
                        e.Row.Cells[3].Text = job.CS_JobInfo.LastJobStatus.Description;

                    //Customer
                    if (null != job.CS_CustomerInfo && null != job.CS_CustomerInfo.CS_Customer)
                        e.Row.Cells[4].Text = job.CS_CustomerInfo.CS_Customer.Name;

                    //JobLocation
                    if (null != job.CS_LocationInfo)
                        e.Row.Cells[5].Text = string.Format("{0}, {1}", job.CS_LocationInfo.CS_City.Name, job.CS_LocationInfo.CS_State.Acronym);
                }
            }
        }

        protected void btnSelect_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SelectButton")
            {
                LoadPageWithJob(int.Parse(e.CommandArgument.ToString()), false);
                _presenter.GetPageTitle();
                hfJobId.Value = e.CommandArgument.ToString();

                ScriptManager.GetCurrent(this).SetFocus(actCalledInEmployee.TextControlClientID);
            }
        }

        #endregion

        #region [ Called In By ]

        protected void btnFakeEmployeeChange_Click(object sender, EventArgs e)
        {
            actCalledInEmployee_TextChanged(sender, e);
        }

        protected void btnFakeCustomerChange_Click(object sender, EventArgs e)
        {
            actCalledInCustomer_TextChanged(sender, e);
        }

        protected void actCalledInCustomer_TextChanged(object sender, EventArgs e)
        {
            if (string.Empty != actCalledInCustomer.SelectedText)
            {
                actCalledInEmployee.SelectedText = string.Empty;
                actCalledInEmployee.SelectedValue = "0";
                actCalledInEmployee.RequiredField = false;
            }

            if (_callType != null)
                AddRemoveCallContact();

            ScriptManager.GetCurrent(this).SetFocus(actCalledInCustomer.TextControlClientID);
        }

        protected void actCalledInEmployee_TextChanged(object sender, EventArgs e)
        {
            if (string.Empty != actCalledInEmployee.SelectedText)
            {
                actCalledInCustomer.SelectedText = string.Empty;
                actCalledInCustomer.SelectedValue = "0";
                actCalledInCustomer.RequiredField = false;
            }

            if (_callType != null)
                AddRemoveCallEmployee();
        }

        protected void chkUserCall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserCall.Checked)
            {
                actCalledInCustomer.ClearSelection();
                actCalledInEmployee.ClearSelection();
                txtCalledInExternal.Text = "";

                actCalledInCustomer.RequiredField = false;
                actCalledInEmployee.RequiredField = false;
                rfvCalledInExternal.Enabled = false;

                hfValidatiorCustomerStatus.Value = "false";
                hfValidatorEmployeeStatus.Value = "false";
                hfValidatorExternalStatus.Value = "false";
            }
            else
            {
                if (actCalledInEmployee.SelectedValue != "0")
                {
                    actCalledInEmployee.RequiredField = true;
                    actCalledInCustomer.RequiredField = false;
                    rfvCalledInExternal.Enabled = false;
                    hfValidatorEmployeeStatus.Value = "true";
                    hfValidatiorCustomerStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "false";
                }
                if (actCalledInCustomer.SelectedValue != "0")
                {
                    actCalledInCustomer.RequiredField = true;
                    actCalledInEmployee.RequiredField = false;
                    rfvCalledInExternal.Enabled = false;
                    hfValidatiorCustomerStatus.Value = "true";
                    hfValidatorEmployeeStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "false";
                }
                if (txtCalledInExternal.Text != "")
                {
                    actCalledInCustomer.RequiredField = false;
                    actCalledInEmployee.RequiredField = false;
                    rfvCalledInExternal.Enabled = true;
                    hfValidatiorCustomerStatus.Value = "false";
                    hfValidatorEmployeeStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "true";
                }
            }
        }

        protected void actCustomer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(actCustomer.SelectedValue))
            {
                CustomerId = int.Parse(actCustomer.SelectedValue);
                SetDynamicPanelsVisibility();
                EnableCalledIn();

                ScriptManager.GetCurrent(this).SetFocus(actCalledInCustomer.TextControlClientID);

                if (actCalledInCustomer.SelectedValue == string.Empty || actCalledInCustomer.SelectedValue == "0")
                    actCalledInCustomer.ClearSelection();


            }
            else
                ScriptManager.GetCurrent(this).SetFocus(actCustomer.TextControlClientID);
        }

        #endregion

        #region [ Call Type ]

        protected void cbPrimaryCallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PrimaryCallTypeId = int.Parse(cbPrimaryCallType.SelectedValue);
            //_presenter.GetPrimaryCallType();
            //_presenter.ValidatePrimaryCallType(false);
            //SetDynamicPanelsVisibility();
            //hideAdvise();
        }

        protected void ddlCallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbPrimaryCallType.Items.Clear();
            SelectedCallType = null;
            SelectedPrimaryCallType = null;

            hidePanels();

            if (0 != ddlCallType.SelectedIndex)
            {
                LoadCallType();
                _callTypeId = int.Parse(ddlCallType.SelectedValue);
                _presenter.GetCallType();
                _presenter.ListPrimaryCallTypes();


                //if (cbPrimaryCallType.Items.Count == 2)
                //{
                //cbPrimaryCallType.SelectedIndex = 1;
                //cbPrimaryCallType_SelectedIndexChanged(cbPrimaryCallType, new EventArgs());

                PrimaryCallTypeId = PrimaryCallTypeList.FirstOrDefault().ID; //int.Parse(cbPrimaryCallType.SelectedValue);
                _presenter.GetPrimaryCallType();
                _presenter.ValidatePrimaryCallType(false);
                SetDynamicPanelsVisibility();
                hideAdvise();
                _presenter.CreateDynamicFields();
                //}

                if (_callType.CallCriteria)
                {
                    //hidePanels();                    
                    //phContactsAutoComplete.Visible = false;
                    phResourceAssigned.Visible = false;
                    pnlResourceReadOnly.Visible = false;
                    pnlFields.Visible = true;
                    phPersonsAdvise.Visible = true;
                    lblAdviceOrTrack.Text = "Select Additional Person(s) to Advise or Track:";
                    lblPersonSelected.Text = "Additional Person(s) Selected:";
                    pnlInitialAdvise.Visible = true;
                    pnlCallLogInitialAdvise.Visible = true;
                    _presenter.ListAllEmployeeCallLogInfoCallCriteria();
                    _presenter.ListInitialAdiviseCallLogs();

                    ckbCopyGeneralLog.Visible = false;
                    ckbShiftTransferLog.Visible = false;
                }
                else
                {
                    pnlInitialAdvise.Visible = false;
                    pnlCallLogInitialAdvise.Visible = false;
                    lblAdviceOrTrack.Text = "Select Person(s):";
                    lblPersonSelected.Text = "Person(s) Selected:";
                    AddRemoveCallEmployee();
                    AddRemoveCallContact();
                }

                hideAdvise();
                hideOrder();
                //ScriptManager.GetCurrent(this).SetFocus(cbPrimaryCallType);
            }
            else
            {
                phDynamic.Controls.Clear();
                ScriptManager.GetCurrent(this).SetFocus(ddlCallType);
            }
            //uscEqRequested.DisplayPanel = true;
        }

        private void AddRemoveCallEmployee()
        {
            if ((_callType.ID != (int)Globals.CallEntry.CallType.CheckedCall && AddedCallEmployee) || (_callType.ID == (int)Globals.CallEntry.CallType.CheckedCall && AddedCallEmployee && actCalledInEmployee.SelectedValue != AddedCallEmployeeID.ToString() && SelectedPersons.Any(f => f.EmployeeID == AddedCallEmployeeID)))
            {
                SelectedPersons.Remove(SelectedPersons.FirstOrDefault(f => f.EmployeeID == AddedCallEmployeeID));
                _presenter.BindPersonsShopingCart();
                AddedCallEmployee = false;
                AddedCallEmployeeID = 0;
            }

            if (_callType.ID == (int)Globals.CallEntry.CallType.CheckedCall && actCalledInEmployee.SelectedValue != "0" && !SelectedPersons.Any(f => f.EmployeeID.ToString() == actCalledInEmployee.SelectedValue))
            {
                this.SelectedEmployeeId = int.Parse(actCalledInEmployee.SelectedValue);
                _presenter.AddEmployeeToCallEntry();
                _presenter.BindPersonsShopingCart();

                AddedCallEmployee = true;
                AddedCallEmployeeID = int.Parse(actCalledInEmployee.SelectedValue);

                ScriptManager.GetCurrent(this).SetFocus(btnAddPersons);
            }
        }

        private void AddRemoveCallContact()
        {
            if ((_callType.ID != (int)Globals.CallEntry.CallType.CheckedCall && AddedCallContact) || (_callType.ID == (int)Globals.CallEntry.CallType.CheckedCall && AddedCallContact && actCalledInCustomer.SelectedValue != AddedCallContactID.ToString() && SelectedPersons.Any(f => f.ContactID == AddedCallContactID)))
            {
                SelectedPersons.Remove(SelectedPersons.FirstOrDefault(f => f.ContactID == AddedCallContactID));
                _presenter.BindPersonsShopingCart();
                AddedCallContact = false;
                AddedCallContactID = 0;
            }

            if (_callType.ID == (int)Globals.CallEntry.CallType.CheckedCall && actCalledInCustomer.SelectedValue != "0" && !SelectedPersons.Any(f => f.ContactID.ToString() == actCalledInCustomer.SelectedValue))
            {
                this.SelectedContactId = int.Parse(actCalledInCustomer.SelectedValue);
                _presenter.AddContactToCallEntry();
                _presenter.BindPersonsShopingCart();

                AddedCallContact = true;
                AddedCallContactID = int.Parse(actCalledInCustomer.SelectedValue);

                ScriptManager.GetCurrent(this).SetFocus(btnAddPersons);
            }
        }

        #endregion

        #region [ Initial Advise ]

        protected void gvInitialAdvise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                this.InitialAdviseRow = e.Row;
                this.SelectedPersonInitialAdvise = (CallCriteriaResourceVO)e.Row.DataItem;
                _presenter.FillInitialAdviseControlsFromCallLogResource();
            }
        }

        #endregion

        #region [ Persons To Advise ]

        protected void btnFilterPesonsAdvise_OnClick(object sender, EventArgs e)
        {
            _presenter.FilterContactOrEmployee();
        }

        protected void gvPersonalAdvise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem is CS_Employee)
                {
                    CS_Employee employee = (CS_Employee)e.Row.DataItem;

                    Label lblDivNum = (Label)e.Row.FindControl("lblDivNum");

                    if (null != employee.CS_Division)
                        lblDivNum.Text = employee.FullDivisionName;

                    Label lblPerson = (Label)e.Row.FindControl("lblPerson");

                    lblPerson.Text = employee.FullName;

                    Label lblContactInfo = (Label)e.Row.FindControl("lblContactInfo");

                    lblContactInfo.Text = employee.ContactInfo.Replace("\n", "<br />");

                    HiddenField hfPersonType = e.Row.FindControl("hfPersonType") as HiddenField;
                    HiddenField hfPersonId = e.Row.FindControl("hfPersonSelect") as HiddenField;
                    CheckBox chk = e.Row.FindControl("chkPersonSelect") as CheckBox;

                    hfPersonId.Value = employee.ID.ToString();
                    hfPersonType.Value = "Employee";

                    if (SelectedPersons.Any(f => f.EmployeeID == employee.ID))
                    {
                        chk.Checked = true;
                    }
                }
                else
                {
                    CS_Contact contact = (CS_Contact)e.Row.DataItem;

                    Label lblDivNum = (Label)e.Row.FindControl("lblDivNum");

                    if (null != contact.CS_Customer_Contact)
                        lblDivNum.Text = contact.CS_Customer_Contact.First().CS_Customer.FullCustomerInformation;

                    Label lblPerson = (Label)e.Row.FindControl("lblPerson");

                    lblPerson.Text = contact.FullName;

                    Label lblContactInfo = (Label)e.Row.FindControl("lblContactInfo");

                    lblContactInfo.Text = contact.ContactInfo;

                    HiddenField hfPersonType = e.Row.FindControl("hfPersonType") as HiddenField;
                    HiddenField hfPersonId = e.Row.FindControl("hfPersonSelect") as HiddenField;
                    CheckBox chk = e.Row.FindControl("chkPersonSelect") as CheckBox;

                    hfPersonId.Value = contact.ID.ToString();
                    hfPersonType.Value = "Contact";

                    if (SelectedPersons.Any(f => f.ContactID == contact.ID))
                    {
                        chk.Checked = true;
                    }
                }
            }
        }

        protected void btnAddPersons_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPersonalAdvise.Rows.Count; i++)
            {
                CheckBox chk = gvPersonalAdvise.Rows[i].FindControl("chkPersonSelect") as CheckBox;
                HiddenField hfId = gvPersonalAdvise.Rows[i].FindControl("hfPersonSelect") as HiddenField;
                HiddenField hfT = gvPersonalAdvise.Rows[i].FindControl("hfPersonType") as HiddenField;

                if (hfT.Value.Equals("Employee"))
                {
                    if (chk.Checked && !SelectedPersons.Any(f => f.EmployeeID.ToString() == hfId.Value))
                    {
                        this.SelectedEmployeeId = int.Parse(hfId.Value);
                        _presenter.AddEmployeeToCallEntry();
                    }
                }
                else
                {
                    if (chk.Checked && !SelectedPersons.Any(f => f.ContactID.ToString() == hfId.Value))
                    {
                        this.SelectedContactId = int.Parse(hfId.Value);
                        _presenter.AddContactToCallEntry();
                    }
                }
            }

            _presenter.BindPersonsShopingCart();

            ScriptManager.GetCurrent(this).SetFocus(btnAddPersons);
        }

        protected void btnResetPersons_Click(object sender, EventArgs e)
        {
            SelectedPersons.Clear();
            _presenter.BindPersonsShopingCart();

            for (int i = 0; i < gvPersonalAdvise.Rows.Count; i++)
            {
                CheckBox chk = gvPersonalAdvise.Rows[i].FindControl("chkPersonSelect") as CheckBox;
                chk.Checked = false;
            }

            ScriptManager.GetCurrent(this).SetFocus(btnResetPersons);
        }

        protected void gvPersonsShopingCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_CallLogResource callLogResource = e.Row.DataItem as CS_CallLogResource;

                if (callLogResource.CS_Contact == null)
                {
                    CS_Employee employee = callLogResource.CS_Employee;

                    Label lblPerson = (Label)e.Row.FindControl("lblPerson");

                    lblPerson.Text = employee.FullName;

                    HiddenField hfPersonType = e.Row.FindControl("hfPersonType") as HiddenField;
                    HiddenField hfPersonId = e.Row.FindControl("hfPersonSelect") as HiddenField;
                    CheckBox chk = e.Row.FindControl("chkPersonSelect") as CheckBox;

                    hfPersonId.Value = employee.ID.ToString();
                    hfPersonType.Value = "Employee";

                    if (this.CallTypeId == (int)Globals.CallEntry.CallType.Advise)
                    {
                        CheckBoxList cbMethodOfContact = e.Row.FindControl("cbMethodOfContact") as CheckBoxList;

                        if (callLogResource.InPerson.HasValue)
                            cbMethodOfContact.Items.FindByValue("1").Selected = callLogResource.InPerson.Value;

                        if (callLogResource.Voicemail.HasValue)
                            cbMethodOfContact.Items.FindByValue("2").Selected = callLogResource.Voicemail.Value;
                    }
                }
                else
                {
                    CS_Contact contact = callLogResource.CS_Contact;

                    Label lblPerson = (Label)e.Row.FindControl("lblPerson");

                    lblPerson.Text = contact.FullName;

                    HiddenField hfPersonType = e.Row.FindControl("hfPersonType") as HiddenField;
                    HiddenField hfPersonId = e.Row.FindControl("hfPersonSelect") as HiddenField;
                    CheckBox chk = e.Row.FindControl("chkPersonSelect") as CheckBox;

                    hfPersonId.Value = contact.ID.ToString();
                    hfPersonType.Value = "Contact";
                }
            }
        }

        protected void btnRemovePersonsShopingCart_Click(object sender, EventArgs e)
        {
            List<ListItem> lstRemovedIds = new List<ListItem>();

            for (int i = 0; i < gvPersonsShopingCart.Rows.Count; i++)
            {
                CheckBox chk = gvPersonsShopingCart.Rows[i].FindControl("chkPersonSelect") as CheckBox;
                HiddenField hfId = gvPersonsShopingCart.Rows[i].FindControl("hfPersonSelect") as HiddenField;
                HiddenField hfT = gvPersonsShopingCart.Rows[i].FindControl("hfPersonType") as HiddenField;

                if (hfT.Value.Equals("Employee"))
                {
                    if (chk.Checked)
                    {
                        lstRemovedIds.Add(new ListItem(hfT.Value, hfId.Value));
                        SelectedPersons.Remove(SelectedPersons.FirstOrDefault(f => f.EmployeeID.ToString() == hfId.Value));
                    }
                }
                else
                {
                    if (chk.Checked)
                    {
                        lstRemovedIds.Add(new ListItem(hfT.Value, hfId.Value));
                        SelectedPersons.Remove(SelectedPersons.FirstOrDefault(f => f.ContactID.ToString() == hfId.Value));
                    }
                }
                chk.Checked = false;
            }

            for (int i = 0; i < gvPersonalAdvise.Rows.Count; i++)
            {
                CheckBox chk = gvPersonalAdvise.Rows[i].FindControl("chkPersonSelect") as CheckBox;
                HiddenField hfId = gvPersonalAdvise.Rows[i].FindControl("hfPersonSelect") as HiddenField;
                HiddenField hfT = gvPersonalAdvise.Rows[i].FindControl("hfPersonType") as HiddenField;

                if (lstRemovedIds.Exists(f => f.Text == hfT.Value && f.Value == hfId.Value))
                    chk.Checked = false;
            }

            _presenter.BindPersonsShopingCart();
            ScriptManager.GetCurrent(this).SetFocus(btnResetPersons);
        }

        #endregion

        #region [ Resources Assigned ]

        protected void btnFilterResource_Click(object sender, EventArgs e)
        {
            _presenter.ListAllFilteredResourcesCallLogInfoInfoByJob();
            ScriptManager.GetCurrent(this).SetFocus(btnFilterResource);
        }

        protected void gvResource_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_View_Resource_CallLogInfo resource = (CS_View_Resource_CallLogInfo)e.Row.DataItem;

                Label lblDivNum = (Label)e.Row.FindControl("lblDivNum");

                lblDivNum.Text = resource.DivisionName;

                Label lblResource = (Label)e.Row.FindControl("lblResource");

                Label lblUnitNumber = (Label)e.Row.FindControl("lblUnitNumber");

                if (resource.EquipmentID.HasValue)
                {
                    if (string.IsNullOrEmpty(resource.ComboName))
                        lblUnitNumber.Text = resource.EquipmentUnitNumber;
                    else
                        lblUnitNumber.Text = resource.ComboName;

                    lblResource.Text = resource.EquipmentDescription;
                }
                else
                    lblResource.Text = string.Format("{0}, {1}", resource.EmployeeName, resource.EmployeeFirstName);

                if (resource.CallDate.HasValue)
                {
                    Label lblCallDate = (Label)e.Row.FindControl("lblCallDate");

                    lblCallDate.Text = resource.CallDate.Value.ToString("MM/dd/yyyy");

                    Label lblCallTime = (Label)e.Row.FindControl("lblCallTime");

                    lblCallTime.Text = resource.CallDate.Value.ToString("HH:mm");
                }

                CheckBox chk = e.Row.FindControl("chkResourceSelect") as CheckBox;
                chk.Attributes.Add("onclick", "validateResources()");

                if (SelectedResources.Any(f => f.ID == resource.ResourceID))
                {
                    chk.Checked = true;
                    txtValidateResources.Text = "1";
                }

                HiddenField hfCallTypeID = e.Row.FindControl("hfCallTypeID") as HiddenField;
                HiddenField hfJobID = e.Row.FindControl("hfJobID") as HiddenField;
                HyperLink hlCallEntry = e.Row.FindControl("hlLastCallEntry") as HyperLink;

                if (!hfJobID.Value.Equals(string.Empty) && resource.CallLogID.HasValue && null != ddlCallType.Items.FindByValue(resource.CallTypeID.ToString()))
                    hlCallEntry.NavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", hfJobID.Value.ToString(), resource.CallLogID.Value.ToString());
                else
                    hlCallEntry.NavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";
            }
        }

        protected void btnResetResource_Click(object sender, EventArgs e)
        {
            SelectedResources.Clear();

            for (int i = 0; i < gvResource.Rows.Count; i++)
            {
                CheckBox chk = gvResource.Rows[i].FindControl("chkResourceSelect") as CheckBox;
                chk.Checked = false;
            }
        }



        #endregion

        #region [ Resources - Read Only ]

        protected void gvResourceReadOnly_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_CallLogResource resource = (CS_CallLogResource)e.Row.DataItem;

                Label lblDivNum = (Label)e.Row.FindControl("lblDivNum");
                Label lblResource = (Label)e.Row.FindControl("lblResource");
                Label lblUnitNumber = (Label)e.Row.FindControl("lblUnitNumber");

                if (null == resource || null == lblDivNum || null == lblResource || null == lblUnitNumber)
                    return;

                if (resource.EquipmentID.HasValue)
                {
                    if (null != resource.CS_Equipment.CS_Division)
                        lblDivNum.Text = resource.CS_Equipment.CS_Division.Name;

                    if (resource.CS_Equipment.ComboID.HasValue)
                    {
                        if (null != resource.CS_Equipment.CS_EquipmentCombo)
                            lblUnitNumber.Text = resource.CS_Equipment.CS_EquipmentCombo.Name;
                    }
                    else
                        lblUnitNumber.Text = resource.CS_Equipment.Name;

                    lblResource.Text = resource.CS_Equipment.Description;
                }
                else
                {
                    if (null != resource.CS_Employee.CS_Division)
                        lblDivNum.Text = resource.CS_Employee.CS_Division.Name;

                    lblResource.Text = resource.CS_Employee.FullName;
                }
            }
        }

        #endregion

        #region [ Save ]

        protected void btnParentUpdate_Click(object sender, EventArgs e)
        {
            SetDynamicPanelsVisibility();
        }

        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {
            loadPersonsListToSave();
            LoadResourceListToSave();

            this.SaveAndContinue = true;
            _presenter.SaveCallLog();

            if (this.SavedSuccessfuly)
            {
                ClearFields();

                ReadyOnlyForUpdate = false;

                Title = "New Call Entry";

                if (GeneralLog || JobId == Globals.GeneralLog.ID)
                {
                    LoadPageWithGeneralLogJob(true);
                }
                else
                {
                    LoadPageWithJob(JobId.Value, true);
                }

                _presenter.GetPageTitle();

                ddlCallType.SelectedIndex = 0;
                ddlCallType_SelectedIndexChanged(ddlCallType, new EventArgs());

                ckbCopyGeneralLog.Checked = false;
                ckbShiftTransferLog.Checked = false;

                if (chkUserCall.Checked)
                {
                    actCalledInCustomer.RequiredField = false;
                    actCalledInEmployee.RequiredField = false;
                    rfvCalledInExternal.Enabled = false;

                    hfValidatiorCustomerStatus.Value = "false";
                    hfValidatorEmployeeStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "false";
                }
                else if (!actCalledInEmployee.SelectedValue.Equals("0"))
                {
                    actCalledInCustomer.RequiredField = false;
                    rfvCalledInExternal.Enabled = false;

                    hfValidatiorCustomerStatus.Value = "false";
                    hfValidatorEmployeeStatus.Value = "true";
                    hfValidatorExternalStatus.Value = "false";
                }
                else if (!actCalledInCustomer.SelectedValue.Equals("0"))
                {
                    actCalledInEmployee.RequiredField = false;
                    rfvCalledInExternal.Enabled = false;

                    hfValidatiorCustomerStatus.Value = "true";
                    hfValidatorEmployeeStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "false";
                }
                else if (!string.IsNullOrEmpty(txtCalledInExternal.Text))
                {
                    actCalledInEmployee.RequiredField = false;
                    actCalledInCustomer.RequiredField = false;

                    hfValidatiorCustomerStatus.Value = "false";
                    hfValidatorEmployeeStatus.Value = "false";
                    hfValidatorExternalStatus.Value = "true";
                }

                pnlCallLog.Visible = true;
                _presenter.BindCallLogHistory();
                pnlCallLogInitialAdvise.Visible = false;

                ScriptManager.GetCurrent(this).SetFocus(dpCallDate.TextBoxClientID);
            }
        }

        protected void btnSaveAndClose_OnClick(object sender, EventArgs e)
        {
            loadPersonsListToSave();
            LoadResourceListToSave();

            this.SaveAndContinue = false;
            _presenter.SaveCallLog();
            //if (JobId.HasValue && CallID.HasValue)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow",
            //        "window.open('Email.aspx?JobId=" + this.JobId.Value + "&CallEntryId=" + CallID.Value +"','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=0,resizable=0,width=600,');", true);
            //}
            //Master.DisplayMessage("Call Entry saved successfully.", true);
        }

        protected void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            _presenter.BindCallLogHistory();
            pnlForDefaultButton.Attributes.Remove("disabled");
            upCallEntry.Update();
        }

        #endregion

        #region [ Call Log History ]

        protected void rptCallLogHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CS_CallLog callLog = e.Item.DataItem as CS_CallLog;
                Label lblNote = e.Item.FindControl("lblNote") as Label;
                Label lblCallTitle = e.Item.FindControl("lblCallTitle") as Label;

                StringBuilder callNote = new StringBuilder();
                callNote.AppendFormat("Call Date:<Text>{0}<BL>", callLog.CallDate.ToShortDateString());
                callNote.AppendFormat("Call Time:<Text>{0}<BL>", callLog.CallDate.ToString("HH:mm"));

                if (callLog.CallTypeID == (int)Globals.CallEntry.CallType.CheckedCall)
                {
                    if (null != callLog.CalledInByCustomer)
                        callNote.AppendFormat("Called in by Company:<Text>{0}<BL>", callLog.CS_Contact.FullContactInformation);

                    if (null != callLog.CalledInByEmployee)
                        callNote.AppendFormat("Called in by Employee:<Text>{0}<BL>", callLog.CS_Employee.FullName);

                    if (!string.IsNullOrEmpty(callLog.CalledInByExternal))
                        callNote.AppendFormat("Called in by External:<Text>{0}<BL>", callLog.CalledInByExternal);

                    if (callLog.UserCall.HasValue)
                        if (callLog.UserCall.Value)
                            callNote.Append("Caller Info:<Text>Call generated by user<BL>");
                }

                if (!string.IsNullOrEmpty(callLog.Note))
                {
                    callNote.Append(callLog.Note);
                }

                lblNote.Text = StringManipulation.TabulateString(callNote.ToString());

                if (null != callLog.CS_CallType)
                {
                    lblCallTitle.Text = callLog.CS_CallType.Description;

                    if (callLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.Advise) ||
                        callLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.InitialLog))
                    {
                        if (callLog.CS_CallLogResource != null && callLog.CS_CallLogResource.Count > 0)
                        {
                            Repeater rptResources = e.Item.FindControl("rptResources") as Repeater;
                            if (null != rptResources)
                            {
                                rptResources.ItemDataBound += new RepeaterItemEventHandler(rptResources_ItemDataBound);
                                rptResources.DataSource = callLog.CS_CallLogResource;
                                rptResources.DataBind();
                                rptResources.Visible = true;
                            }
                        }
                    }
                }

                LinkButton hlUpdate = e.Item.FindControl("hlUpdate") as LinkButton;

                if (null != hlUpdate)
                    hlUpdate.OnClientClick = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}&IsHistoryUpdate={2}&RefreshUniqueID={3}', '', 'width=870, height=600, scrollbars=1, resizable=yes'); $('.updateProgressMessage')[0].innerHTML = 'Waiting Update ...'; $find('mdlPopUpExtender').show(); return false; ", callLog.JobID, callLog.ID, true.ToString(), btnRefreshHistory.UniqueID);

            }
        }

        protected void rptCallLogHistoryInitialAdvise_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CS_CallLog callLog = e.Item.DataItem as CS_CallLog;
                Repeater rptResources = e.Item.FindControl("rptResources") as Repeater;
                if (null != rptResources && null != callLog)
                {
                    HtmlGenericControl divDetails = e.Item.FindControl("divDetails") as HtmlGenericControl;
                    if (callLog.CS_CallLogResource != null && callLog.CS_CallLogResource.Count > 0)
                    {
                        rptResources.ItemDataBound += new RepeaterItemEventHandler(rptResources_ItemDataBound);
                        rptResources.DataSource = callLog.CS_CallLogResource;
                        rptResources.DataBind();
                    }
                    else
                    {
                        divDetails.Visible = false;
                    }
                }
            }
        }

        protected void rptResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CS_CallLogResource callLogResource = e.Item.DataItem as CS_CallLogResource;
                if (null != callLogResource)
                {
                    Label lblName = e.Item.FindControl("lblName") as Label;
                    Label lblDetails = e.Item.FindControl("lblDetails") as Label;
                    Label lblAdviseNote = e.Item.FindControl("lblAdviseNote") as Label;

                    string details = "Emailed on {0} {1}";
                    if (callLogResource.InPerson.HasValue && callLogResource.InPerson.Value)
                        details += "<br />In Person on {0} {1}";
                    if (callLogResource.Voicemail.HasValue && callLogResource.Voicemail.Value)
                        details += "<br />Voicemail on {0} {1}";
                    details = string.Format(details,
                        callLogResource.CS_CallLog.CallDate.ToString("MM/dd"),
                        callLogResource.CS_CallLog.CallDate.ToString("HH:mm"));

                    if (callLogResource.EmployeeID.HasValue)
                    {
                        lblName.Text = callLogResource.CS_Employee1.FullName;

                        AdviseNoteResourceID = callLogResource.EmployeeID.Value;
                        AdviseNoteIsEmployee = true;
                        _presenter.GetInitialAdviseNote();
                        lblAdviseNote.Text = AdviseNote; //"10 Cars, Division 084, Hazmat"; // TODO: Fixed value until we create the Call Criteria funcionality
                    }
                    else if (callLogResource.ContactID.HasValue)
                    {
                        lblName.Text = callLogResource.CS_Contact.FullName;

                        AdviseNoteResourceID = callLogResource.ContactID.Value;
                        AdviseNoteIsEmployee = false;
                        _presenter.GetInitialAdviseNote();
                        lblAdviseNote.Text = AdviseNote; //"10 Cars, Division 084, Hazmat"; // TODO: Fixed value until we create the Call Criteria funcionality
                    }
                    lblDetails.Text = details;
                }
            }
        }

        #endregion

        #endregion

        #region [ Methods ]

        protected void loadPersonsListToSave()
        {
            SelectedPersons.Clear();

            if (pnlInitialAdvise.Visible)
            {
                for (int i = 0; i < gvInitialAdvise.Rows.Count; i++)
                {
                    CheckBoxList chk = gvInitialAdvise.Rows[i].FindControl("cbMethodOfContact") as CheckBoxList;
                    HiddenField hfPersonId = gvInitialAdvise.Rows[i].FindControl("hfPersonSelect") as HiddenField;
                    HiddenField hfPersonType = gvInitialAdvise.Rows[i].FindControl("hfPersonType") as HiddenField;
                    CountableTextBox txtNotes = gvInitialAdvise.Rows[i].FindControl("txtNotes") as CountableTextBox;

                    if (hfPersonType.Value.Equals("Employee"))
                    {
                        SelectedEmployeeId = Convert.ToInt32(hfPersonId.Value);
                        _presenter.AddEmployeeToCallEntry();
                    }
                    else
                    {
                        SelectedContactId = Convert.ToInt32(hfPersonId.Value);
                        _presenter.AddContactToCallEntry();
                    }

                    this.SelectedPersons[i].Notes = txtNotes.Text;
                    this.SelectedPersons[i].InPerson = chk.Items.FindByValue("1").Selected;
                    this.SelectedPersons[i].Voicemail = chk.Items.FindByValue("2").Selected;
                }
            }

            if (phPersonsAdvise.Visible)
            {
                int currentIndex = this.SelectedPersons.Count;
                for (int i = 0; i < gvPersonsShopingCart.Rows.Count; i++)
                {
                    CheckBox chk = gvPersonsShopingCart.Rows[i].FindControl("chkPersonSelect") as CheckBox;
                    HiddenField hfPersonId = gvPersonsShopingCart.Rows[i].FindControl("hfPersonSelect") as HiddenField;
                    HiddenField hfPersonType = gvPersonsShopingCart.Rows[i].FindControl("hfPersonType") as HiddenField;


                    if (hfPersonType.Value.Equals("Employee"))
                    {
                        SelectedEmployeeId = Convert.ToInt32(hfPersonId.Value);
                        _presenter.AddEmployeeToCallEntry();

                        if (this.CallTypeId == (int)Globals.CallEntry.CallType.Advise)
                        {
                            CheckBoxList chkMethodOfContact = gvPersonsShopingCart.Rows[i].FindControl("cbMethodOfContact") as CheckBoxList;
                            this.SelectedPersons[currentIndex + i].InPerson = chkMethodOfContact.Items.FindByValue("1").Selected;
                            this.SelectedPersons[currentIndex + i].Voicemail = chkMethodOfContact.Items.FindByValue("2").Selected;
                        }
                    }
                    else
                    {
                        SelectedContactId = Convert.ToInt32(hfPersonId.Value);
                        _presenter.AddContactToCallEntry();

                        if (this.CallTypeId == (int)Globals.CallEntry.CallType.Advise)
                        {
                            CheckBoxList chkMethodOfContact = gvPersonsShopingCart.Rows[i].FindControl("cbMethodOfContact") as CheckBoxList;
                            this.SelectedPersons[currentIndex + i].InPerson = chkMethodOfContact.Items.FindByValue("1").Selected;
                            this.SelectedPersons[currentIndex + i].Voicemail = chkMethodOfContact.Items.FindByValue("2").Selected;
                        }
                    }
                }
            }
        }

        protected void LoadResourceListToSave()
        {
            SelectedResources.Clear();

            for (int i = 0; i < gvResource.Rows.Count; i++)
            {
                CheckBox chk = gvResource.Rows[i].FindControl("chkResourceSelect") as CheckBox;

                if (chk.Checked)
                {
                    HiddenField hfResourceId = gvResource.Rows[i].FindControl("hfResourceSelect") as HiddenField;

                    SelectedResourceId = Convert.ToInt32(hfResourceId.Value);
                    _presenter.AddResourceToCallEntry();
                }
            }
        }

        private void hidePanels()
        {
            phPersonsAdvise.Visible = false;
            //phContactsAutoComplete.Visible = false;
            phResourceAssigned.Visible = false;
            pnlResourceReadOnly.Visible = false;
            pnlDynamic.Visible = false;
            pnlFields.Visible = false;
        }

        private void LoadCallType()
        {
            if (null != SelectedCallType)
            {
                if (SelectedCallType.CallCriteria)
                {
                    btnResetPersons.Visible = false;

                    foreach (GridViewRow gridViewRow in gvPersonalAdvise.Rows)
                    {
                        CheckBox checkBox = (CheckBox)gridViewRow.FindControl("chkPersonSelect");
                        checkBox.Enabled = false;
                    }
                }
            }
        }

        private void hideAdvise()
        {
            if (!ddlCallType.SelectedItem.Text.Equals("Initial Advise", StringComparison.OrdinalIgnoreCase))
            {
                gvPersonalAdvise.Columns[2].Visible = false;
            }
            else
            {
                gvPersonalAdvise.Columns[2].Visible = true;
            }
        }

        private void hideOrder()
        {
            if (ddlCallType.SelectedItem.Text.Equals("Order", StringComparison.OrdinalIgnoreCase))
            {
                phResourceAssigned.Visible = false;
                pnlEquipmentType.Visible = true;
            }
            else if (!SelectedPrimaryCallType.PersonsPrimaryCallType)
            {
                phResourceAssigned.Visible = true;
                pnlEquipmentType.Visible = false;
            }
        }

        private void SetDynamicPanelsVisibility()
        {
            //if (SelectedCallType != null)
            //    actCustomer.Enabled = (!SelectedCallType.CallCriteria);
            //else
            //    actCustomer.Enabled = true;

            //if (cbPrimaryCallType.Enabled)
            //{
            //    _presenter.ClearPersons();
            //    _presenter.ClearResources();
            //}


            if (null != SelectedPrimaryCallType)
            {
                _presenter.ClearPersons();
                _presenter.ClearResources();


                phPersonsAdvise.Visible = SelectedPrimaryCallType.PersonsPrimaryCallType;
                phAddPerson.Visible = SelectedPrimaryCallType.PersonsPrimaryCallType && SelectedCallType.ID != (int)Globals.CallEntry.CallType.CheckedCall;
                phResourceAssigned.Visible = !SelectedPrimaryCallType.PersonsPrimaryCallType;
                pnlResourceReadOnly.Visible = false;
                btnSaveClose.Enabled = true;
                btnSaveContinue.Enabled = true;

                if (!SelectedPrimaryCallType.PersonsPrimaryCallType)
                {
                    if (JobId.HasValue && JobId.Value.Equals(Globals.GeneralLog.ID) && CallID.HasValue)
                    {
                        _presenter.ListResourcesReadOnly();
                        pnlResourceReadOnly.Visible = true;
                        phResourceAssigned.Visible = false;
                        btnSaveClose.Enabled = false;
                        btnSaveContinue.Enabled = false;
                    }
                    else
                    {
                        _presenter.ListResourceAssignedFilterOptions();
                        _presenter.ListAllFilteredResourcesCallLogInfoInfoByJob();
                        rfvValidateResources.Enabled = true;
                        lblAdviceOrTrack.Text = "Select Person(s):";
                        lblPersonSelected.Text = "Person(s) Selected:";
                    }
                }

                pnlFields.Visible = true;
            }
            else
            {
                hidePanels();
            }
        }

        private void ClearFields()
        {
            SelectedPersons.Clear();
            ddlCallType.SelectedIndex = 0;
            phPersonsAdvise.Visible = false;
            phResourceAssigned.Visible = false;
            pnlResourceReadOnly.Visible = false;
            rfvValidateResources.Enabled = false;
            pnlDynamic.Visible = false;
            phDynamic.Controls.Clear();
            FilteredEmployeeCustomerName = string.Empty;

        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        private void LoadPageWithJob(int jobId, bool saveContinue)
        {
            JobId = jobId;

            EnableCallTypeCombos();

            _presenter.GetJobInfo();

            _presenter.ListCallTypes();

            btnAddResource.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdate.ClientID);

            phJobFilter.Visible = false;

            //phContactsAutoComplete.Visible = false;

            hfGeneralLog.Value = string.Empty;
            pnlSelectCustomer.Visible = false;

            if (!CallID.HasValue && !saveContinue)
            {
                chkUserCall.Checked = true;
                chkUserCall_CheckedChanged(null, null);
            }
        }

        private void LoadPageWithGeneralLogJob(bool saveContinue)
        {
            JobId = Globals.GeneralLog.ID;

            EnableCallTypeCombos();

            _presenter.ListCallTypes();

            phJobFilter.Visible = false;

            phJobInformation.Visible = true;
            pnlSelectCustomer.Visible = true;

            divSpecific.Visible = false;

            btnAddResource.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdate.ClientID);

            hfGeneralLog.Value = "GeneralLog";

            if (!CallID.HasValue && !saveContinue)
            {
                chkUserCall.Checked = true;
                chkUserCall_CheckedChanged(null, null);
            }
        }

        private void EnableCallTypeCombos()
        {
            //cbPrimaryCallType.Enabled = true;
            ddlCallType.Enabled = true;
        }

        private void EnableCalledIn()
        {
            if (actCalledInCustomer.SelectedValue != "0")
                actCalledInCustomer.Enabled = true;
            else if (actCalledInEmployee.SelectedValue != "0")
                actCalledInEmployee.Enabled = true;
        }

        private void DisableCalledIn()
        {
            actCalledInCustomer.Enabled = false;
            actCalledInEmployee.Enabled = false;
        }

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public string PageTitle
        {
            set { this.Title = value; }
        }

        private bool AddedCallEmployee
        {
            get
            {
                if (null == ViewState["AddedCallEmployee"])
                    AddedCallEmployee = false;

                return bool.Parse(ViewState["AddedCallEmployee"].ToString());
            }
            set
            {
                ViewState["AddedCallEmployee"] = value;
            }

        }

        private int AddedCallEmployeeID
        {
            get
            {
                if (null == ViewState["AddedCallEmployeeID"])
                    AddedCallEmployeeID = 0;

                return int.Parse(ViewState["AddedCallEmployeeID"].ToString());
            }
            set
            {
                ViewState["AddedCallEmployeeID"] = value;
            }

        }

        private bool AddedCallContact
        {
            get
            {
                if (null == ViewState["AddedCallContact"])
                    AddedCallContact = false;

                return bool.Parse(ViewState["AddedCallContact"].ToString());
            }
            set
            {
                ViewState["AddedCallContact"] = value;
            }

        }

        private int AddedCallContactID
        {
            get
            {
                if (null == ViewState["AddedCallContactID"])
                    AddedCallContactID = 0;

                return int.Parse(ViewState["AddedCallContactID"].ToString());
            }
            set
            {
                ViewState["AddedCallContactID"] = value;
            }

        }

        public int? JobId
        {
            get
            {
                string returnValue = string.Empty;
                if (!string.IsNullOrEmpty(hfJobId.Value))
                    returnValue = hfJobId.Value;
                else
                    returnValue = Request.Form.Get(hfJobId.UniqueID);

                if (string.IsNullOrEmpty(returnValue))
                    return null;
                else
                    return Convert.ToInt32(returnValue);
            }
            set
            {
                if (value.HasValue)
                    hfJobId.Value = value.Value.ToString();
                else
                    hfJobId.Value = string.Empty;
                ReadOnlyPage = false;
            }
        }

        public bool GeneralLog
        {
            get
            {
                if (Globals.GeneralLog.ID == JobId)
                    return true;

                return false;
            }
        }

        public int? CustomerId
        {
            get
            {
                return (int?)ViewState["CustomerId"];
            }
            set
            {
                ViewState["CustomerId"] = value;
                actCalledInCustomer.ContextKey = value.ToString();

                if (value.HasValue)
                    hypAddNewCustomerContact.NavigateUrl = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actCalledInCustomer&CustomerID=" + value.Value.ToString() + "', '', 'width=1200, height=600, scrollbars=1, resizable=yes');";
                else
                    hypAddNewCustomerContact.NavigateUrl = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actCalledInCustomer', '', 'width=1200, height=600, scrollbars=1, resizable=yes');";
            }
        }

        public bool ReadOnlyPage
        {
            set
            {
                dpCallDate.EnableDatePicker = !value;
                txtCallTime.Enabled = !value;
                //cbPrimaryCallType.Enabled = !value;
                ddlCallType.Enabled = !value;
                actCustomer.Enabled = !value;
                actCustomer.RequiredField = false;
                actCalledInEmployee.Enabled = !value;
                actCalledInCustomer.Enabled = !value;
                hypAddNewCustomerContact.Enabled = !value;
                ckbShiftTransferLog.Enabled = !value;
                ckbCopyGeneralLog.Enabled = !value;
                btnSaveContinue.Enabled = !value;
                btnSaveClose.Enabled = !value;
            }
        }

        public bool OpenEmailPage
        {
            set
            {
                StringBuilder script = new StringBuilder();
  
                if (value)
                {
                    if (!IsHistoryUpdate)
                       script.Append("CheckEmail();");
                }

                if (IsHistoryUpdate)
                {
                    hfIsHistoryUpdate.Value = "Done";
                    script.AppendFormat(" if (window.opener != null) window.opener.__doPostBack('{0}','');", RefreshUniqueID);
                }

                script.Append(" window.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SendEmailPage", script.ToString(), true);
            }
        }

        public string RefreshUniqueID
        {
            set
            {
                hfRefreshUniqueID.Value = value;
            }
            get
            {

                return hfRefreshUniqueID.Value;
            }
        }

        public bool IsHistoryUpdate
        {
            set
            {
                hfIsHistoryUpdate.Value = value.ToString();
            }
            get
            {
                if (string.IsNullOrEmpty(hfIsHistoryUpdate.Value))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["IsHistoryUpdate"]))
                        IsHistoryUpdate = bool.Parse(Request.QueryString["IsHistoryUpdate"]);
                    else
                        IsHistoryUpdate = false;
                }

                return bool.Parse(hfIsHistoryUpdate.Value);
            }
        }

        public bool SaveAndContinue { get; set; }

        public bool SavedSuccessfuly { get; set; }

        private bool ReadyOnlyForUpdate
        {
            set
            {
                txtCallTime.Enabled = false;
                dpCallDate.EnableDatePicker = value;
                //cbPrimaryCallType.Enabled = value;
                ddlCallType.Enabled = value;
            }
        }

        private string Username
        {
            get { return Master.Username; }
        }

        #endregion

        #region [ Job Filter ]

        public Globals.JobRecord.CallEntryFilter JobFilterType
        {
            get { return (Globals.JobRecord.CallEntryFilter)Enum.Parse(typeof(Globals.JobRecord.CallEntryFilter), ddlFilter.SelectedValue); }
        }

        public string JobFilterValue
        {
            get { return txtName.Text; }
        }

        public IList<CS_Job> JobFilterGridDataSource
        {
            set
            {
                gvJobFilter.DataSource = value;
                gvJobFilter.DataBind();
            }
        }

        #endregion

        #region [ Job Panel ]

        public CS_Job Job
        {
            set
            {
                CS_Job jobInformation = value;
                if (null != jobInformation)
                {
                    phJobInformation.Visible = true;

                    if (!string.IsNullOrEmpty(jobInformation.NumberOrInternalTracking))
                        lblJobNumber.Text = jobInformation.PrefixedNumber;

                    CS_JobDivision division = jobInformation.CS_JobDivision.SingleOrDefault(e => e.PrimaryDivision);

                    if (division != null)
                        lblDivisionNumber.Text = division.CS_Division.Name.Trim();

                    lblCityName.Text = jobInformation.CS_LocationInfo.CS_City.Name.Trim();

                    lblStateName.Text = jobInformation.CS_LocationInfo.CS_State.Name.Trim();

                    lblCustomerName.Text = jobInformation.CS_CustomerInfo.CS_Customer.Name.Trim();
                }
            }
        }

        public string PrefixedJobNumber
        {
            get { return lblJobNumber.Text; }
        }

        #endregion

        #region [ Call Type ]

        public int CallTypeId
        {
            get
            {
                if (!_callTypeId.HasValue)
                {
                    if (!string.IsNullOrEmpty(Request.Form[hfSelectedCallTypeId.UniqueID]))
                        _callTypeId = int.Parse(Request.Form[hfSelectedCallTypeId.UniqueID]);
                    else
                        _callTypeId = 0;
                }

                return _callTypeId.Value;
            }
            set
            {
                _callTypeId = value;

            }
        }

        public int PrimaryCallTypeId { get; set; }

        public IList<CS_PrimaryCallType> PrimaryCallTypeList { get; set; }

        public IList<CS_CallType> CallTypeList
        {
            set
            {
                ddlCallType.DataTextField = "Description";
                ddlCallType.DataValueField = "ID";
                ddlCallType.DataSource = value;
                ddlCallType.DataBind();
                ddlCallType.Items.Insert(0, new ListItem("- Select One -", "0"));
            }
        }

        public CS_PrimaryCallType SelectedPrimaryCallType
        {
            get
            {
                return (CS_PrimaryCallType)ViewState["SelectedPrimaryCallType"];
            }
            set
            {
                ViewState["SelectedPrimaryCallType"] = value;
            }
        }

        public CS_CallType SelectedCallType
        {
            get
            {
                if (null == _callType)
                    _presenter.GetCallType();

                return _callType;
            }
            set
            {
                _callType = value;
                if (null != _callType)
                    hfSelectedCallTypeId.Value = _callType.ID.ToString();
            }
        }

        #endregion

        #region [ Call Log Fields ]

        public int? CallID
        {
            get
            {
                if (null != ViewState["CallEntryID"])
                {
                    return (int?)ViewState["CallEntryID"];
                }
                else if (null != Request.QueryString["CallEntryID"])
                {
                    return Int32.Parse(Request.QueryString["CallEntryID"]);
                }
                else return null;
            }
            set
            {
                ViewState["CallEntryID"] = value;
            }
        }

        public CS_CallLog CallEntryEntity
        {
            get
            {
                int? contactId, employeeId;
                string external;

                if ("0" == actCalledInCustomer.SelectedValue || string.IsNullOrEmpty(actCalledInCustomer.SelectedValue))
                {
                    contactId = null;
                }
                else
                {
                    contactId = Int32.Parse(actCalledInCustomer.SelectedValue);
                }

                if ("0" == actCalledInEmployee.SelectedValue || string.IsNullOrEmpty(actCalledInEmployee.SelectedValue))
                {
                    employeeId = null;
                }
                else
                {
                    employeeId = Int32.Parse(actCalledInEmployee.SelectedValue);
                }

                if (txtCalledInExternal.Text == "")
                {
                    external = null;
                }
                else
                {
                    external = txtCalledInExternal.Text;
                }

                return new CS_CallLog()
                {
                    ID = CallID.HasValue ? CallID.Value : 0,
                    JobID = JobId.Value,
                    PrimaryCallTypeId = SelectedPrimaryCallType.ID,
                    CallTypeID = SelectedCallType.ID,
                    CallDate = CallDateTime.Value,
                    CalledInByEmployee = employeeId,
                    CalledInByCustomer = contactId,
                    CalledInByExternal = external,
                    Note = null,
                    CreatedBy = Username,
                    ModifiedBy = Username,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    Active = true,
                    UserCall = chkUserCall.Checked
                };
            }
            set
            {
                _callLog = value;
                if (null != _callLog)
                {
                    if (_callLog.ShiftTransferLog.HasValue)
                        ckbShiftTransferLog.Checked = _callLog.ShiftTransferLog.Value;

                    if (_callLog.JobID == Globals.GeneralLog.ID)
                        ckbCopyGeneralLog.Visible = false;

                    if (_callLog.HasGeneralLog.HasValue)
                    {
                        ckbCopyGeneralLog.Checked = true;
                        ckbCopyGeneralLog.Enabled = false;
                    }

                    if (_callLog.PrimaryCallTypeId.HasValue)
                        PrimaryCallTypeId = _callLog.PrimaryCallTypeId.Value;

                    if (_callLog.CalledInByEmployee.HasValue)
                    {
                        actCalledInEmployee.SelectedValue = _callLog.CalledInByEmployee.Value.ToString();
                        actCalledInCustomer.RequiredField = false;
                        rfvCalledInExternal.Enabled = false;
                        actCalledInEmployee.RequiredField = true;

                        hfValidatorEmployeeStatus.Value = "true";
                        hfValidatiorCustomerStatus.Value = "false";
                        hfValidatorExternalStatus.Value = "false";
                    }

                    if (_callLog.CalledInByCustomer.HasValue)
                    {
                        actCalledInCustomer.SelectedValue = _callLog.CalledInByCustomer.Value.ToString();
                        actCalledInEmployee.RequiredField = false;
                        rfvCalledInExternal.Enabled = false;
                        actCalledInCustomer.RequiredField = true;

                        hfValidatorEmployeeStatus.Value = "false";
                        hfValidatiorCustomerStatus.Value = "true";
                        hfValidatorExternalStatus.Value = "false";
                    }

                    if (!string.IsNullOrEmpty(_callLog.CalledInByExternal))
                    {
                        txtCalledInExternal.Text = _callLog.CalledInByExternal;
                        actCalledInCustomer.RequiredField = false;
                        actCalledInEmployee.RequiredField = false;
                        rfvCalledInExternal.Enabled = true;

                        hfValidatorEmployeeStatus.Value = "false";
                        hfValidatiorCustomerStatus.Value = "false";
                        hfValidatorExternalStatus.Value = "true";
                    }

                    CallTypeId = _callLog.CallTypeID;
                    CallDateTime = _callLog.CallDate;

                    _presenter.GetCallType();
                    _presenter.GetPrimaryCallType();
                    //_presenter.ListCallTypes();
                    //_presenter.ListPrimaryCallTypes();

                    ddlCallType.Items.Add(new ListItem(_callType.Description, _callType.ID.ToString()));

                    if (SelectedPrimaryCallType.PersonsPrimaryCallType)
                    {
                        _presenter.ClearPersons();
                        _presenter.ListAllAddedContacts();
                        _presenter.ListAllAddedEmployess();
                        _presenter.BindPersonsShopingCart();
                    }

                    ddlCallType.SelectedValue = CallTypeId.ToString();
                    //cbPrimaryCallType.SelectedValue = PrimaryCallTypeId.ToString();

                    LoadCallType();
                    hideAdvise();
                    ReadyOnlyForUpdate = false;
                    SetDynamicPanelsVisibility();
                    hideOrder();
                    _presenter.CreateDynamicFields();

                    if (_callLog.UserCall.HasValue && _callLog.UserCall.Value)
                    {
                        chkUserCall.Checked = _callLog.UserCall.Value;
                        chkUserCall_CheckedChanged(chkUserCall, new EventArgs());
                        rfvCalledInExternal.Enabled = false;
                        actCalledInCustomer.RequiredField = false;
                        actCalledInEmployee.RequiredField = false;

                        hfValidatorEmployeeStatus.Value = "false";
                        hfValidatiorCustomerStatus.Value = "false";
                        hfValidatorExternalStatus.Value = "false";
                    }
                    else
                        chkUserCall.Checked = false;
                }
                else
                {
                    DisplayMessage("No Call Log was found!", true);
                }
            }
        }

        public DateTime? CallDateTime
        {
            get
            {
                if (dpCallDate.Value.HasValue && !txtCallTime.Text.Equals(string.Empty))
                {
                    return Convert.ToDateTime(
                        string.Format("{0} {1}", dpCallDate.Value.Value.ToString("MM/dd/yyyy"), txtCallTime.Text),
                        new System.Globalization.CultureInfo("en-US"));
                }
                else
                    return null;
            }
            set
            {

                DateTime date = value.Value;

                dpCallDate.Value = date;
                txtCallTime.Text = date.ToString("HH:mm");
            }
        }

        public List<Control> DynamicFieldsControls
        {
            get
            {
                List<Control> controlList = new List<Control>();
                foreach (Control c in phDynamic.Controls)
                {
                    controlList.Add(c);
                }
                return controlList;
            }
            set
            {
                phDynamic.Controls.Clear();

                for (int i = 0; i < value.Count; i++)
                {
                    phDynamic.Controls.Add(value[i]);
                    if (i.Equals(0))
                        ScriptManager.GetCurrent(this).SetFocus(value[i]);
                }

                pnlDynamic.Visible = true;
            }
        }

        public string DynamicControlXmlString
        {
            get
            {
                if (null != _callLog)
                {
                    return _callLog.Xml;
                }
                else
                {
                    return SelectedCallType.Xml;
                }

            }
        }

        public bool CopyToShiftTransferLog
        {
            get { return ckbShiftTransferLog.Checked; }
            set { ckbShiftTransferLog.Checked = value; }
        }

        public bool CopyToGeneralLog
        {
            get
            {
                if (!string.IsNullOrEmpty(hfSave.Value))
                {
                    int value = Convert.ToInt32(hfSave.Value);
                    if (value == 1 && ckbCopyGeneralLog.Checked)
                        return true;
                }
                return false;
            }
            set
            {
                ckbCopyGeneralLog.Checked = value;
            }
        }

        public string Notes
        {
            get
            {
                return ((CountableTextBox)InitialAdviseRow.FindControl("txtNotes")).Text;
            }
            set
            {
                ((CountableTextBox)InitialAdviseRow.FindControl("txtNotes")).Text = value;
            }
        }

        #endregion

        #region [ Initial Advise ]

        public bool InitialAdviseVisibility
        {
            get { return pnlInitialAdvise.Visible; }
            set { pnlInitialAdvise.Visible = value; }
        }

        public IList<CallCriteriaResourceVO> PersonInitialAdviseGridDataSource
        {
            get
            {
                return gvInitialAdvise.DataSource as List<CallCriteriaResourceVO>;
            }
            set
            {
                gvInitialAdvise.DataSource = value;
                gvInitialAdvise.DataBind();
            }
        }

        public IList<int> CallCriteriaIDs
        {
            get
            {
                if (ViewState["CallCriteriaIDs"] == null)
                    CallCriteriaIDs = new List<int>();

                return (List<int>)ViewState["CallCriteriaIDs"];
            }
            set
            {
                ViewState["CallCriteriaIDs"] = value;
            }
        }

        private GridViewRow InitialAdviseRow { get; set; }

        public CallCriteriaResourceVO SelectedPersonInitialAdvise { get; set; }

        public string InitialAdviseGridDivisionNumber
        {
            get
            {
                return ((Label)InitialAdviseRow.FindControl("lblDivCust")).Text;
            }
            set
            {
                ((Label)InitialAdviseRow.FindControl("lblDivCust")).Text = value;
            }
        }

        public string InitialAdviseGridName
        {
            get
            {
                return ((Label)InitialAdviseRow.FindControl("lblName")).Text;
            }
            set
            {
                ((Label)InitialAdviseRow.FindControl("lblName")).Text = value;
            }
        }

        public string InitialAdviseGridNote
        {
            get
            {
                return ((Label)InitialAdviseRow.FindControl("lblAdiviseNote")).Text;
            }
            set
            {
                ((Label)InitialAdviseRow.FindControl("lblAdiviseNote")).Text = value;
            }
        }

        public string InitialAdviseGridContactInfo
        {
            get
            {
                return ((Label)InitialAdviseRow.FindControl("lblContactInfo")).Text;
            }
            set
            {
                ((Label)InitialAdviseRow.FindControl("lblContactInfo")).Text = value;
            }
        }

        public string InitialAdviseGridResourceId
        {
            get
            {
                return ((HiddenField)InitialAdviseRow.FindControl("hfPersonSelect")).Value;
            }
            set
            {
                ((HiddenField)InitialAdviseRow.FindControl("hfPersonSelect")).Value = value;
            }
        }

        public string InitialAdviseGridResourceType
        {
            get
            {
                return ((HiddenField)InitialAdviseRow.FindControl("hfPersonType")).Value;
            }
            set
            {
                ((HiddenField)InitialAdviseRow.FindControl("hfPersonType")).Value = value;
            }
        }

        public bool InitialAdviseGridInPerson
        {
            get
            {
                CheckBoxList methodOfContact = (CheckBoxList)InitialAdviseRow.FindControl("cbMethodOfContact");
                return methodOfContact.Items.FindByValue(Convert.ToInt32(Globals.CallEntry.MethodOfContactValue.InPerson).ToString()).Selected;
            }
            set
            {
                CheckBoxList methodOfContact = (CheckBoxList)InitialAdviseRow.FindControl("cbMethodOfContact");
                methodOfContact.Items.FindByValue(Convert.ToInt32(Globals.CallEntry.MethodOfContactValue.InPerson).ToString()).Selected = value;
            }
        }

        public bool InitialAdviseGridVoicemail
        {
            get
            {
                CheckBoxList methodOfContact = (CheckBoxList)InitialAdviseRow.FindControl("cbMethodOfContact");
                return methodOfContact.Items.FindByValue(Convert.ToInt32(Globals.CallEntry.MethodOfContactValue.Voicemail).ToString()).Selected;
            }
            set
            {
                CheckBoxList methodOfContact = (CheckBoxList)InitialAdviseRow.FindControl("cbMethodOfContact");
                methodOfContact.Items.FindByValue(Convert.ToInt32(Globals.CallEntry.MethodOfContactValue.Voicemail).ToString()).Selected = value;
            }
        }

        #endregion

        #region [ Persons To Advise ]

        public bool PersonsAdviseVisibility
        {
            get { return phPersonsAdvise.Visible; }
            set { phPersonsAdvise.Visible = value; }
        }

        public Globals.CallEntry.typeOfPerson TypeOfPerson
        {
            get
            {
                if (ddlFilterContacts.SelectedValue == "0")
                {
                    return Globals.CallEntry.typeOfPerson.Both;
                }
                else if (ddlFilterContacts.SelectedValue == "1")
                {
                    return Globals.CallEntry.typeOfPerson.Contact;
                }
                else
                {
                    return Globals.CallEntry.typeOfPerson.Employee;
                }
            }
        }

        public string FilteredEmployeeCustomerName
        {
            get { return txtFilteredEmployeeCustomerName.Text; }
            set { txtFilteredEmployeeCustomerName.Text = value; }
        }

        public IList<object> PersonGridDataSource
        {
            set
            {
                gvPersonalAdvise.DataSource = value;
                gvPersonalAdvise.DataBind();
                _presenter.BindPersonsShopingCart();
            }
        }

        public IList<CS_CallLogResource> SelectedPersons
        {
            get
            {
                if (ViewState["SelectedEmployees"] == null)
                    ViewState["SelectedEmployees"] = new List<CS_CallLogResource>();

                return (IList<CS_CallLogResource>)ViewState["SelectedEmployees"];
            }
            set
            {
                ViewState["SelectedEmployees"] = value;
            }
        }

        public int SelectedEmployeeId { get; set; }

        public int SelectedContactId { get; set; }

        public IList<object> PersonsShopingCartDataSource
        {
            set
            {
                if (this.CallTypeId == (int)Globals.CallEntry.CallType.Advise)
                    gvPersonsShopingCart.Columns[2].Visible = true;
                else
                    gvPersonsShopingCart.Columns[2].Visible = false;

                gvPersonsShopingCart.DataSource = value;
                gvPersonsShopingCart.DataBind();
            }
        }

        #endregion

        #region [ Resources Assigned ]

        public bool ResourceAssignedVisibility
        {
            get { return phResourceAssigned.Visible; }
            set { phResourceAssigned.Visible = value; }
        }

        public Globals.CallEntry.ResourceFilterType? ResourceFilterType
        {
            get
            {
                if (string.IsNullOrEmpty(ddlFilterResource.SelectedValue))
                    return null;
                else
                    return (Globals.CallEntry.ResourceFilterType)Convert.ToInt32(ddlFilterResource.SelectedValue);
            }
        }

        public string ResourceFilterValue
        {
            get { return txtFilterResource.Text; }
        }

        public IList<ListItemVO> ResourceAssignedFilterValues
        {
            set
            {
                ddlFilterResource.DataSource = value;
                ddlFilterResource.DataBind();

                ddlFilterResource.Items.Insert(0, new ListItem("- Select One -", "0"));
            }
        }

        public IList<CS_View_Resource_CallLogInfo> ResourceGridDataSource
        {
            set
            {
                gvResource.DataSource = value;
                gvResource.DataBind();
            }
        }

        public IList<CS_Resource> SelectedResources
        {
            get
            {
                if (ViewState["SelectedResources"] == null)
                    ViewState["SelectedResources"] = new List<CS_Resource>();

                return (IList<CS_Resource>)ViewState["SelectedResources"];
            }
            set
            {
                ViewState["SelectedResources"] = value;
            }
        }

        public int SelectedResourceId { get; set; }

        public IList<LocalEquipmentTypeVO> SelectedEquipmentTypes
        {
            get
            {
                return uscEqRequested.SelectedEquipments;
            }
            set
            {
                uscEqRequested.SelectedEquipments = value;
            }
        }

        #endregion

        #region [ Resources Read-Only ]

        public IList<CS_CallLogResource> ResourceReadOnlyDataSource
        {
            set
            {
                gvResourceReadOnly.DataSource = value;
                gvResourceReadOnly.DataBind();
            }
        }

        #endregion

        #region [ Call Log History ]

        public bool CallLogHistoryPanelVisibility
        {
            get { return pnlCallLog.Visible; }
            set { pnlCallLog.Visible = value; }
        }

        public bool CallLogInitialAdviseHistoryVisibility
        {
            get { return pnlCallLogInitialAdvise.Visible; }
            set { pnlCallLogInitialAdvise.Visible = value; }
        }

        public IList<int> CallLogHistoryList
        {
            get
            {
                if (null == ViewState["CallLogHistoryList"])
                    ViewState["CallLogHistoryList"] = new List<int>();

                return ViewState["CallLogHistoryList"] as IList<int>;
            }
            set
            {
                ViewState["CallLogHistoryList"] = value;

                string callLogHistoryIds = string.Empty;
                foreach (int id in value)
                    callLogHistoryIds += ";" + id.ToString();
                if (!string.IsNullOrEmpty(callLogHistoryIds))
                    callLogHistoryIds = callLogHistoryIds.Substring(1);
                hfEmail.Value = callLogHistoryIds;
            }
        }

        public IList<CS_CallLog> CallLogHistory
        {
            set
            {
                rptCallLogHistory.DataSource = value;
                rptCallLogHistory.DataBind();
            }
        }

        public IList<CS_CallLog> InitialAdiviseCallLogHistory
        {
            set
            {
                rptCallLogHistoryInitialAdvise.DataSource = value;
                rptCallLogHistoryInitialAdvise.DataBind();
            }
        }

        public int AdviseNoteResourceID { get; set; }

        public bool AdviseNoteIsEmployee { get; set; }

        public string AdviseNote { get; set; }

        #endregion

        #endregion


        
    }
}
