using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class FirstAlert : System.Web.UI.Page, IFirstAlertView
    {
        #region [ Attributes ]

        FirstAlertPresenter _presenter;

        private TableRow _tableRow = null;

        bool _readOnly;
        bool _deletePermission;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new FirstAlertPresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Common ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        #endregion

        #region [ First Alert Listing ]

        protected void btnSearchAlert_Click(object sender, EventArgs e)
        {
            _presenter.LoadFilteredFirstAlert();
        }

        protected void gvAlertViewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_FirstAlert dataItem = e.Row.DataItem as CS_FirstAlert;

                FirstAlertRowDataItem = dataItem;

                _tableRow = e.Row;

                _presenter.SetDetailedFirstAlertRowData();
            }
        }

        protected void gvAlertViewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("FirstAlertEdit"))
            {
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow row = lb.NamingContainer as GridViewRow;
                string id = gvAlertViewer.DataKeys[row.RowIndex][0].ToString();
                int firstAlertId = int.Parse(id);
                FirstAlertID = firstAlertId;
                _presenter.ResetFirstAlert();
                _presenter.FillFirstAlertHeaderFields();
            }
            else if (e.CommandName.Equals("Print"))
            {
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow row = lb.NamingContainer as GridViewRow;
                string id = gvAlertViewer.DataKeys[row.RowIndex][0].ToString();
                int firstAlertId = int.Parse(id);
                OpenReport(firstAlertId);
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            EditMode = true;
            _presenter.ResetFirstAlert();
        }

        #endregion

        #region [ First Alert Form ]

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.ClearValidationFields();
            txtFirstAlertType.Text = string.Empty;
            txtDivision.Text = string.Empty;
            EditMode = false;
            _presenter.HideVehiclesForm();
            _presenter.HidePersonForm();

            if (JobIDFromExternalSource.HasValue)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "closeWindow", "window.close();", true);
            else
                _presenter.LoadFirstAlert();
        }

        protected void actAlertHeaderJobNumber_TextChanged(object sender, EventArgs e)
        {
            _presenter.FillJobFields();
        }

        protected void chkGeneralLog_CheckedChanged(object sender, EventArgs e)
        {
            if (actAlertHeaderJobNumber.SelectedValue.Equals("0"))
            {
                ClearJobFields();
                if (chkGeneralLog.Checked)
                    txtValidateJob.Text = "1";
                else
                    txtValidateJob.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.SaveUpdateFirstAlert();
                if (SavedSuccessfully)
                {
                    _presenter.ClearValidationFields();
                    _presenter.HideVehiclesForm();
                    _presenter.HidePersonForm();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.DeleteFirstAlert();
            DisplayMessage("First Alert deleted successfully", true);
        }

        #endregion

        #region [ Vehicle ]

        protected void btnVehicleListAdd_Click(object sender, EventArgs e)
        {
            this.IsHulcherVehicle = true;

            if (!JobID.Equals(0))
                _presenter.BindEquipment();
            else
                FilteredEquipmentsDataSource = new List<CS_View_EquipmentInfo>();

            _presenter.ShowVehiclesForm();
            _presenter.ShowHulcherVehicles();
            _presenter.ResetAddVehicle();
            _presenter.HideOtherVehicles();
            ScriptManager.GetCurrent(this).SetFocus(btnVehicleFormAdd);
        }

        protected void btnHulcherVehicleSearch_Click(object sender, EventArgs e)
        {
            _presenter.BindEquipment();
            ScriptManager.GetCurrent(this).SetFocus(btnHulcherVehicleSearch);
        }

        protected void btnVehicleFormAdd_Click(object sender, EventArgs e)
        {
            _presenter.AddVehicles();
            _presenter.ResetAddVehicle();
            _presenter.HideVehiclesForm();
        }

        protected void btnVehicleFormClose_Click(object sender, EventArgs e)
        {
            _presenter.ResetAddVehicle();
            _presenter.HideVehiclesForm();
        }

        protected void rbVehiclesFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.ResetAddVehicle();

            if (this.IsHulcherVehicle)
            {
                _presenter.BindEquipment();
                _presenter.ShowHulcherVehicles();
                _presenter.HideOtherVehicles();
            }
            else
            {
                _presenter.HideHulcherVehicles();
                _presenter.ShowOtherVehicles();
            }
        }

        protected void gvVehiclesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            VehiclesListCommandName = e.CommandName;
            VehiclesListCommandArgument = int.Parse(e.CommandArgument.ToString());
            _presenter.VehiclesListRowCommand();
        }

        protected void gvFilteredEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FilteredEquipmentsRow = e.Row;
                _presenter.FilteredEquipmentsRowDataBound();
            }
        }

        protected void gvVehiclesList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FirstAlertVehicleRow = e.Row;
                _presenter.SetVehiclesListItems();
            }
        }

        #endregion

        #region [ Person ]

        protected void btnPersonListAdd_Click(object sender, EventArgs e)
        {
            _presenter.ResetPersonAdd();
            _presenter.ShowPersonForm();

            if (!JobID.Equals(0))
                _presenter.BindEmployee();
            else
                EmployeeDataSource = new List<CS_Employee>();
        }

        protected void btnPersonFormAdd_Click(object sender, EventArgs e)
        {
            _presenter.AddPersonToList();
        }

        protected void btnPersonFormClose_Click(object sender, EventArgs e)
        {
            _presenter.ResetPersonAdd();
            _presenter.HidePersonForm();
        }

        protected void gvPeopleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PersonEdit")
            {
                _presenter.ResetPersonAdd();
                CurrentFirstAlertPersonIndex = int.Parse(e.CommandArgument.ToString());
                LoadPersonDetails();
                _presenter.ShowPersonForm();
            }
            else if (e.CommandName == "RemovePerson")
            {
                _presenter.ResetPersonAdd();
                CurrentFirstAlertPersonIndex = int.Parse(e.CommandArgument.ToString());
                _presenter.RemovePersonFromList();
            }
        }

        protected void gvPeopleList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PeopleListRow = e.Row;

                _presenter.AddPeopleListDetails();
            }
        }

        protected void rblIsHulcherEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bool.Parse(rblIsHulcherEmployee.SelectedValue))
            {
                pnlOtherPeople.Visible = false;
                pnlHulcherPeople.Visible = true;
                chkFilterPeopleFromJob.Visible = true;
            }
            else
            {
                pnlOtherPeople.Visible = true;
                pnlHulcherPeople.Visible = false;
                chkFilterPeopleFromJob.Visible = false;
            }
        }

        protected void btnHulcherPeopleSearch_Click(object sender, EventArgs e)
        {
            _presenter.BindEmployee();
            ScriptManager.GetCurrent(this).SetFocus(btnHulcherPeopleSearch);
        }

        protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EmployeeListRow = e.Row;
                EmployeeListRowDataItem = e.Row.DataItem as CS_Employee;

                _presenter.AddEmployeeListDetails();
            }
        }

        #endregion

        #region [ Contact Personal ]

        protected void gvContactPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_FirstAlertContactPersonal dataItem = e.Row.DataItem as CS_FirstAlertContactPersonal;

                ContactPersonalRowDataItem = dataItem;

                ContactPersonalRow = e.Row;

                _presenter.FillContactPersonalRow();
            }
        }

        #endregion

        #endregion

        #region [ Properties ]

        #region [ Security ]

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
            get
            {
                return _readOnly;
            }
            set
            {
                if (value)
                {
                    _readOnly = value;
                    pnlVisualization.Visible = false;
                    pnlNoAccess.Visible = true;
                }
            }
        }

        public bool DeletePermission
        {
            get
            {
                return _deletePermission;
            }
            set
            {
                _deletePermission = value;
                gvAlertViewer.Columns[8].Visible = value;
            }
        }

        #endregion

        #region [ Common ]

        public bool SavedSuccessfully { get; set; }

        #endregion

        #region [ First Alert Listing ]

        public Globals.FirstAlert.FirstAlertFilters? FirstAlertFilter
        {
            get
            {
                if (cbFilterSearchAlert.SelectedValue.Equals("0") || string.IsNullOrEmpty(cbFilterSearchAlert.SelectedValue))
                    return null;
                else
                    return (Globals.FirstAlert.FirstAlertFilters)int.Parse(cbFilterSearchAlert.SelectedValue);
            }
        }

        public string FilterValue
        {
            get
            {
                if (string.IsNullOrEmpty(txtFilterDateAndTime.Text))
                    return txtFilterSearchAlert.Text;
                else
                {
                    return txtFilterDateAndTime.Text;
                }
            }
        }

        public IList<CS_FirstAlert> FirstAlertList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                gvAlertViewer.DataSource = value;
                gvAlertViewer.DataBind();
            }
        }

        #region [ Grid Row ]

        public CS_FirstAlert FirstAlertRowDataItem { get; set; }

        public string FirstAlertRowAlertId
        {
            get
            {
                HiddenField hfAlertId = _tableRow.FindControl("hfAlertId") as HiddenField;
                return hfAlertId.Value;
            }
            set
            {
                HiddenField hfAlertId = _tableRow.FindControl("hfAlertId") as HiddenField;
                if (null != hfAlertId) hfAlertId.Value = value;
            }
        }

        public string FirstAlertRowAlertNumber
        {
            get
            {
                return _tableRow.Cells[1].Text;
            }
            set
            {
                _tableRow.Cells[1].Text = value;
            }
        }

        public string FirstAlertRowFirstAlertType
        {
            get
            {
                return _tableRow.Cells[2].Text;
            }
            set
            {
                _tableRow.Cells[2].Text = value;
            }
        }

        public string FirstAlertRowAlertDateAndTime
        {
            get
            {
                return _tableRow.Cells[3].Text;
            }
            set
            {
                _tableRow.Cells[3].Text = value;
            }
        }

        public string FirstAlertRowJobNumber
        {
            get
            {
                return _tableRow.Cells[4].Text;
            }
            set
            {
                _tableRow.Cells[4].Text = value;
            }
        }

        public string FirstAlertRowDivision
        {
            get
            {
                return _tableRow.Cells[5].Text;
            }
            set
            {
                _tableRow.Cells[5].Text = value;
            }
        }

        public string FirstAlertRowCustomer
        {
            get
            {
                return _tableRow.Cells[6].Text;
            }
            set
            {
                _tableRow.Cells[6].Text = value;
            }
        }

        public string FirsAlertRowLocation
        {
            get
            {
                return _tableRow.Cells[7].Text;
            }
            set
            {
                _tableRow.Cells[7].Text = value;
            }
        }

        #endregion

        #endregion

        #region [ First Alert Form ]

        public bool EditMode
        {
            get { return pnlCreation.Visible; }
            set
            {
                pnlCreation.Visible = value;
                pnlVisualization.Visible = !value;

                FirstAlertPersonList = null;
                FirstAlertVehicleList = null;

                updVisualization.Update();
                updCreation.Update();

                lblTitle.Text = "First Alert";
            }
        }

        public int? JobIDFromExternalSource
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString.Get("JobID")))
                    return null;
                else
                    return Convert.ToInt32(Request.QueryString.Get("JobID"));
            }
        }

        public int FirstAlertID
        {
            get
            {
                if (null == ViewState["FirstAlertID"])
                    return 0;

                return (int)ViewState["FirstAlertID"];
            }
            set
            {
                ViewState["FirstAlertID"] = value;
            }
        }

        public int JobID
        {
            get
            {
                return int.Parse(actAlertHeaderJobNumber.SelectedValue);
            }
        }

        public CS_Job JobRelatedInformation
        {
            set
            {
                if (null != value)
                {
                    if (null != value.CS_JobDivision)
                    {
                        foreach (CS_JobDivision division in value.CS_JobDivision)
                        {
                            if (division.Active)
                            {
                                ddlDivision.Items.FindByValue(division.DivisionID.ToString()).Selected = true;
                                txtDivision.Text += "," + division.DivisionID.ToString();
                            }
                        }
                    }

                    if (null != value.CS_CustomerInfo)
                    {
                        if (null != value.CS_CustomerInfo.CS_Customer)
                        {
                            actAlertHeaderCustomer.SetValue = value.CS_CustomerInfo.CustomerId.ToString();
                            actAlertHeaderCustomer.SelectedText = value.CS_CustomerInfo.CS_Customer.FullCustomerInformation;
                        }
                        if (value.CS_CustomerInfo.EicContactId.HasValue)
                        {
                            actAlertHeaderEIC.SetValue = value.CS_CustomerInfo.EicContactId.Value.ToString();
                            actAlertHeaderEIC.SelectedText = value.CS_CustomerInfo.CS_Contact2.FullContactInformation;
                        }
                    }

                    if (null != value.CS_LocationInfo)
                    {
                        cbAlertHeaderCountry.SelectedValue = value.CS_LocationInfo.CountryID.ToString();
                        actAlertHeaderState.ContextKey = value.CS_LocationInfo.CountryID.ToString();
                        actAlertHeaderState.SetValue = value.CS_LocationInfo.StateID.ToString();
                        actAlertHeaderState.SelectedText = value.CS_LocationInfo.CS_State.AcronymName;
                        actAlertHeaderCity.SetValue = value.CS_LocationInfo.CityID.ToString();
                        actAlertHeaderCity.SelectedText = value.CS_LocationInfo.CS_City.Name;
                    }

                    txtValidateJob.Text = "1";

                    actAlertHeaderCustomer.Enabled = false;
                    actAlertHeaderEIC.Enabled = false;
                    actAlertHeaderCity.Enabled = false;
                    actAlertHeaderState.Enabled = false;
                    cbAlertHeaderCountry.Enabled = false;
                }
                else
                {
                    if (!chkGeneralLog.Checked)
                        txtValidateJob.Text = string.Empty;

                    actAlertHeaderCustomer.Enabled = true;
                    actAlertHeaderEIC.Enabled = true;
                    actAlertHeaderCity.Enabled = true;
                    actAlertHeaderState.Enabled = true;
                    cbAlertHeaderCountry.Enabled = true;
                }
            }
        }

        public CS_FirstAlert FirstAlertEntity
        {
            get
            {
                DateTime date = DateTime.Parse(string.Format("{0} {1}", dpAlertHeaderDate.Value.Value.ToShortDateString(), txtAlertHeaderTime.Text));

                CS_FirstAlert returnValue = new CS_FirstAlert()
                {
                    ID = FirstAlertID,
                    Date = date,
                    ReportedBy = actAlertHeaderReportedBy.SelectedText,
                    Details = txtAlertHeaderDetails.Text,
                    HasPoliceReport = chkAlertHeaderPoliceReport.Checked,
                    PoliceAgency = txtAlertHeaderPoliceAgency.Text,
                    PoliceReportNumber = txtAlertHeaderPoliceReport.Text,
                    Active = true
                };

                returnValue.CopyToGeneralLog = chkGeneralLog.Checked;
                if (actAlertHeaderJobNumber.SelectedValue.Equals(string.Empty) || actAlertHeaderJobNumber.SelectedValue.Equals("0"))
                    returnValue.JobID = Globals.GeneralLog.ID;
                else
                    returnValue.JobID = int.Parse(actAlertHeaderJobNumber.SelectedValue);

                if (!actAlertHeaderCustomer.SelectedValue.Equals(string.Empty) && !actAlertHeaderCustomer.SelectedValue.Equals("0"))
                    returnValue.CustomerID = int.Parse(actAlertHeaderCustomer.SelectedValue);
                if (!actAlertHeaderEIC.SelectedValue.Equals(string.Empty) && !actAlertHeaderEIC.SelectedValue.Equals("0"))
                    returnValue.InChargeEmployeeID = int.Parse(actAlertHeaderEIC.SelectedValue);
                if (!cbAlertHeaderCountry.SelectedValue.Equals(string.Empty) && !cbAlertHeaderCountry.SelectedValue.Equals("0"))
                    returnValue.CountryID = int.Parse(cbAlertHeaderCountry.SelectedValue);
                if (!actAlertHeaderState.SelectedValue.Equals(string.Empty) && !actAlertHeaderState.SelectedValue.Equals("0"))
                    returnValue.StateID = int.Parse(actAlertHeaderState.SelectedValue);
                if (!actAlertHeaderCity.SelectedValue.Equals(string.Empty) && !actAlertHeaderCity.SelectedValue.Equals("0"))
                    returnValue.CityID = int.Parse(actAlertHeaderCity.SelectedValue);

                if (!actAlertHeaderCompletedBy.SelectedValue.Equals(string.Empty) && !actAlertHeaderCompletedBy.SelectedValue.Equals("0"))
                    returnValue.CompletedByEmployeeID = int.Parse(actAlertHeaderCompletedBy.SelectedValue);

                return returnValue;
            }
            set
            {
                if (null == value)
                {
                    chkGeneralLog.Checked = true;
                    txtValidateJob.Text = "1";

                    if (JobIDFromExternalSource.HasValue)
                        actAlertHeaderJobNumber.SelectedValue = JobIDFromExternalSource.Value.ToString();
                    else
                    {
                        actAlertHeaderJobNumber.SetValue = "0";
                        actAlertHeaderJobNumber.SelectedText = string.Empty;
                    }

                    actAlertHeaderCustomer.SetValue = "0";
                    actAlertHeaderCustomer.SelectedText = string.Empty;

                    foreach (ListItem item in ddlDivision.Items)
                        item.Selected = false;
                    foreach (ListItem item in ddlFirstAlertType.Items)
                        item.Selected = false;

                    actAlertHeaderEIC.SetValue = "0";
                    actAlertHeaderEIC.SelectedText = string.Empty;

                    cbAlertHeaderCountry.SelectedIndex = 0;
                    actAlertHeaderState.SetValue = "0";
                    actAlertHeaderState.SelectedText = string.Empty;
                    actAlertHeaderCity.SetValue = "0";
                    actAlertHeaderCity.SelectedText = string.Empty;

                    dpAlertHeaderDate.Value = DateTime.Now;
                    txtAlertHeaderTime.Text = DateTime.Now.ToString("HH:mm");

                    actAlertHeaderReportedBy.SelectedText = string.Empty;
                    actAlertHeaderReportedBy.SelectedValue = "0";

                    actAlertHeaderCompletedBy.SetValue = "0";
                    actAlertHeaderCompletedBy.SelectedText = string.Empty;
                    actAlertHeaderCompletedBy.ReadOnly = false;

                    if (Master.LoggedEmployee.HasValue)
                    {
                        actAlertHeaderCompletedBy.SelectedValue = Master.LoggedEmployee.Value.ToString();
                        actAlertHeaderCompletedBy.ReadOnly = true;
                    }

                    txtAlertHeaderDetails.Text = string.Empty;
                    chkAlertHeaderPoliceReport.Checked = false;
                    txtAlertHeaderPoliceAgency.Text = string.Empty;
                    txtAlertHeaderPoliceReport.Text = string.Empty;

                    pnlContactPersonal.Visible = false;
                }
                else
                {
                    lblTitle.Text = string.Format("First Alert - {0}", value.Number);

                    txtValidateJob.Text = "1";
                    chkGeneralLog.Checked = value.CopyToGeneralLog;
                    if (value.JobID == Globals.GeneralLog.ID)
                    {
                        actAlertHeaderJobNumber.SetValue = "0";
                        actAlertHeaderJobNumber.SelectedText = string.Empty;
                    }
                    else
                    {
                        actAlertHeaderJobNumber.SelectedValue = value.JobID.ToString();
                        actAlertHeaderJobNumber.SelectedText = value.CS_Job.PrefixedJobNumber.ToString();
                    }
                    if (value.CustomerID.HasValue)
                    {
                        actAlertHeaderCustomer.SetValue = value.CustomerID.ToString();
                        actAlertHeaderCustomer.SelectedText = value.CS_Customer.FullCustomerInformation;
                    }

                    if (value.InChargeEmployeeID.HasValue)
                    {
                        actAlertHeaderEIC.SetValue = value.InChargeEmployeeID.Value.ToString();
                        actAlertHeaderEIC.SelectedText = value.CS_Employee_InCharge.FullName;
                    }

                    cbAlertHeaderCountry.SelectedValue = value.CS_Country.ID.ToString();

                    if (value.StateID.HasValue)
                    {
                        actAlertHeaderState.SetValue = value.StateID.Value.ToString();
                        actAlertHeaderState.SelectedText = string.Format("{0} - {1}", value.CS_State.Acronym.Trim(), value.CS_State.Name.Trim());
                    }

                    if (value.CityID.HasValue)
                    {
                        actAlertHeaderCity.SetValue = value.CityID.Value.ToString();
                        actAlertHeaderCity.SelectedText = value.CS_City.CityStateInformation;
                    }

                    dpAlertHeaderDate.Value = value.Date;
                    txtAlertHeaderTime.Text = new DateTime(value.Date.Ticks).ToString("HH:mm");

                    actAlertHeaderReportedBy.SelectedText = value.ReportedBy;

                    if (value.CompletedByEmployeeID.HasValue)
                    {
                        actAlertHeaderCompletedBy.SetValue = value.CompletedByEmployeeID.Value.ToString();
                        actAlertHeaderCompletedBy.SelectedText = value.CS_Employee_CompletedBy.FullName;
                    }
                    txtAlertHeaderDetails.Text = value.Details;
                    chkAlertHeaderPoliceReport.Checked = value.HasPoliceReport;
                    txtAlertHeaderPoliceAgency.Text = value.PoliceAgency;
                    txtAlertHeaderPoliceReport.Text = value.PoliceReportNumber;

                    pnlContactPersonal.Visible = true;
                }
            }
        }

        public IList<CS_FirstAlertType> FirstAlertType
        {
            set
            {
                ddlFirstAlertType.DataValueField = "ID";
                ddlFirstAlertType.DataTextField = "Description";
                ddlFirstAlertType.DataSource = value;
                ddlFirstAlertType.DataBind();
            }
        }

        public IList<CS_FirstAlertFirstAlertType> FirstAlertFirstAlertTypeList
        {
            set
            {
                List<string> types = new List<string>();

                foreach (CS_FirstAlertFirstAlertType type in value)
                {
                    types.Add(type.FirstAlertTypeID.ToString());
                }

                if (types.Count > 0)
                    ddlFirstAlertType.SelectedValues = types;

                LoadFirstAlertTypeOnTextBox(types);

                ViewState["CS_FirstAlertFirstAlertTypeList"] = types;
            }
            get
            {
                IList<CS_FirstAlertFirstAlertType> typeList = new List<CS_FirstAlertFirstAlertType>();
                List<string> types = new List<string>();
                //types.AddRange(ddlFirstAlertType.SelectedValues);

                types.AddRange(txtFirstAlertType.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                List<string> list = null;

                if (null == ViewState["CS_FirstAlertFirstAlertTypeList"])
                    list = new List<string>();
                else
                    list = ((List<string>)ViewState["CS_FirstAlertFirstAlertTypeList"]);

                foreach (string type in types)
                {
                    int typeId = int.Parse(type);

                    string strId = list.FirstOrDefault(e => int.Parse(e) == typeId);

                    typeList.Add(
                        new CS_FirstAlertFirstAlertType()
                        {
                            FirstAlertTypeID = typeId,
                            FirstAlertID = this.FirstAlertID,
                            Active = true
                        }
                    );
                }

                return typeList;
            }
        }

        public IList<CS_FirstAlertDivision> FirstAlertDivisionList
        {
            get
            {
                IList<CS_FirstAlertDivision> divisionList = new List<CS_FirstAlertDivision>();
                List<string> divisions = new List<string>();
                divisions.AddRange(txtDivision.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                List<string> list = null;

                if (null == ViewState["FirstAlertDivisionList"])
                    list = new List<string>();
                else
                    list = ((List<string>)ViewState["FirstAlertDivisionList"]);

                foreach (string division in divisions)
                {
                    int divisionId = int.Parse(division);

                    string strId = list.FirstOrDefault(e => int.Parse(e) == divisionId);

                    divisionList.Add(
                        new CS_FirstAlertDivision()
                        {
                            DivisionID = divisionId,
                            FirstAlertID = this.FirstAlertID,
                            Active = true
                        }
                    );
                }

                return divisionList;
            }
            set
            {
                List<string> divisions = new List<string>();

                foreach (CS_FirstAlertDivision division in value)
                {
                    divisions.Add(division.DivisionID.ToString());
                }

                if (divisions.Count > 0)
                    ddlDivision.SelectedValues = divisions;

                LoadDivisionsOnTextBox(divisions);

                ViewState["FirstAlertDivisionList"] = divisions;
            }
        }

        public int? CallLogID
        {
            get
            {
                if (null != Request.QueryString["CallLogID"])
                    return int.Parse(Request.QueryString["CallLogID"]);
                return null;
            }
        }

        public IList<CS_Division> DivisionList
        {
            set
            {
                ddlDivision.DataSource = value;
                ddlDivision.DataTextField = "ExtendedDivisionName";
                ddlDivision.DataValueField = "ID";
                ddlDivision.DataBind();
            }
        }

        public string DivisionValue
        {
            set
            {
                txtDivision.Text = value;
            }
        }

        public string FirstAlertTypeValue
        {
            set
            {
                txtFirstAlertType.Text = value;
            }
        }

        #endregion

        #region [ Vehicle ]

        public int CurrentFirstAlertVehicleEquipmentID
        {
            get
            {
                if (null == ViewState["CurrentFirstAlertVehicleEquipmentID"])
                    return 0;

                return (int)ViewState["CurrentFirstAlertVehicleEquipmentID"];
            }
            set
            {
                ViewState["CurrentFirstAlertVehicleEquipmentID"] = value;
            }
        }

        public int CurrentFirstAlertVehicleID
        {
            get
            {
                if (null == ViewState["CurrentFirstAlertVehicleID"])
                    return 0;

                return (int)ViewState["CurrentFirstAlertVehicleID"];
            }
            set
            {
                ViewState["CurrentFirstAlertVehicleID"] = value;
            }
        }

        public int CurrentFirstAlertVehicleIndex
        {
            get
            {
                if (null == ViewState["CurrentFirstAlertVehicleIndex"])
                    return -1;

                return (int)ViewState["CurrentFirstAlertVehicleIndex"];
            }
            set
            {
                ViewState["CurrentFirstAlertVehicleIndex"] = value;

                if (value > -1)
                {
                    CS_FirstAlertVehicle vehicle = FirstAlertVehicleList[value];

                    if (vehicle.IsHulcherVehicle)
                    {
                        CurrentFirstAlertVehicleEquipmentID = vehicle.EquipmentID.Value;
                        _presenter.GetVehicleByEquipmentID();
                        _presenter.ShowHulcherVehicles();
                        _presenter.HideOtherVehicles();
                        IsHulcherVehicle = true;
                    }
                    else
                    {
                        OtherVehicle = vehicle;
                        _presenter.HideHulcherVehicles();
                        _presenter.ShowOtherVehicles();
                        IsHulcherVehicle = false;
                    }

                    CurrentFirstAlertVehicleID = vehicle.ID;
                }
            }
        }

        public IList<CS_FirstAlertVehicle> FirstAlertVehicleList
        {
            get
            {
                if (null == ViewState["FirstAlertVehicleList"])
                    FirstAlertVehicleList = new List<CS_FirstAlertVehicle>();

                IList<CS_FirstAlertVehicle> oldList = (IList<CS_FirstAlertVehicle>)ViewState["FirstAlertVehicleList"];
                IList<CS_FirstAlertVehicle> returnList = new List<CS_FirstAlertVehicle>();

                for (int i = 0; i < oldList.Count; i++)
                {
                    CS_FirstAlertVehicle vehicle = oldList[i];

                    returnList.Add
                        (
                            new CS_FirstAlertVehicle()
                            {
                                ID = vehicle.ID,
                                FirstAlertID = vehicle.FirstAlertID,
                                IsHulcherVehicle = vehicle.IsHulcherVehicle,
                                EquipmentID = vehicle.EquipmentID,
                                Make = vehicle.Make,
                                Model = vehicle.Model,
                                Year = vehicle.Year,
                                Damage = vehicle.Damage,
                                EstimatedCost = vehicle.EstimatedCost,
                                TemporaryID = vehicle.TemporaryID,
                                Active = true

                            }
                        );
                }

                return returnList;
            }
            set
            {
                ViewState["FirstAlertVehicleList"] = value;
                gvVehiclesList.DataSource = value;
                gvVehiclesList.DataBind();
            }
        }

        public IList<CS_FirstAlertVehicle> PersonVehicleList
        {
            set
            {
                cbVehicle.SelectedIndex = -1;
                cbVehicle.Items.Clear();
                cbVehicle.Items.Add(new ListItem(string.Empty, string.Empty));
                for (int i = 0; i < value.Count; i++)
                    cbVehicle.Items.Add(new ListItem(value[i].Description, value[i].TemporaryID.ToString()));

                updPeople.Update();
            }
        }

        public IList<CS_View_EquipmentInfo> FilteredEquipmentsDataSource
        {
            set
            {
                hfVehiclesAdded.Value = "";
                gvFilteredEquipments.DataSource = value;
                gvFilteredEquipments.DataBind();
            }
        }

        public Globals.FirstAlert.EquipmentFilters? EquipmentFilter
        {
            get
            {
                if (cbHulcherVehicleSearch.SelectedValue.Equals("0") || cbHulcherVehicleSearch.SelectedValue.Equals(string.Empty))
                    return null;
                return (Globals.FirstAlert.EquipmentFilters)int.Parse(cbHulcherVehicleSearch.SelectedValue);
            }
            set
            {
                if (EquipmentFilter.HasValue)
                    cbHulcherVehicleSearch.SelectedValue = ((int)EquipmentFilter.Value).ToString();
                else
                    cbHulcherVehicleSearch.SelectedValue = "0";
            }
        }

        public string EquipmentFilterValue
        {
            get { return txtHulcherVehicleSearch.Text; }
            set { txtHulcherVehicleSearch.Text = value; }
        }

        public bool IsHulcherVehicle
        {
            get
            {
                return bool.Parse(rbVehiclesFormType.SelectedValue);
            }
            set
            {
                rbVehiclesFormType.SelectedValue = value.ToString().ToLower();
            }
        }

        public IList<CS_FirstAlertVehicle> SelectedVehicles
        {
            get
            {
                List<CS_FirstAlertVehicle> returnList = new List<CS_FirstAlertVehicle>();
                returnList.AddRange(FirstAlertVehicleList.ToList());

                CS_FirstAlertVehicle editingVehicle = null;

                if (CurrentFirstAlertVehicleIndex > -1)
                    editingVehicle = FirstAlertVehicleList[CurrentFirstAlertVehicleIndex];

                if (this.IsHulcherVehicle)
                {

                    for (int i = 0; i < gvFilteredEquipments.Rows.Count; i++)
                    {
                        if (gvFilteredEquipments.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkSelect = gvFilteredEquipments.Rows[i].FindControl("chkSelect") as CheckBox;
                            HiddenField hfEquipmentID = gvFilteredEquipments.Rows[i].FindControl("hfEquipmentID") as HiddenField;
                            Label lblUnitNumber = gvFilteredEquipments.Rows[i].FindControl("lblUnitNumber") as Label;
                            Label lblMake = gvFilteredEquipments.Rows[i].FindControl("lblMake") as Label;
                            HiddenField hfModel = gvFilteredEquipments.Rows[i].FindControl("hfModel") as HiddenField;
                            TextBox txtDamage = gvFilteredEquipments.Rows[i].FindControl("txtDamage") as TextBox;
                            TextBox txtEstCost = gvFilteredEquipments.Rows[i].FindControl("txtEstCost") as TextBox;
                            HiddenField hfYear = gvFilteredEquipments.Rows[i].FindControl("hfYear") as HiddenField;

                            int? year = null;
                            decimal? estimatedCost = null;
                            int editingVehicleIndex = 0;

                            if (!string.IsNullOrEmpty(hfYear.Value))
                                year = int.Parse(hfYear.Value);

                            if (!string.IsNullOrEmpty(txtEstCost.Text))
                                estimatedCost = decimal.Parse(txtEstCost.Text.Replace("$", string.Empty));

                            if (chkSelect.Checked)
                            {
                                if (CurrentFirstAlertVehicleIndex == -1)
                                {
                                    editingVehicle = returnList.FirstOrDefault(e => e.EquipmentID == int.Parse(hfEquipmentID.Value));
                                    editingVehicleIndex = returnList.IndexOf(editingVehicle);
                                }

                                CS_FirstAlertVehicle vehicle = new CS_FirstAlertVehicle()
                                {
                                    ID = (null != editingVehicle) ? editingVehicle.ID : 0,
                                    TemporaryID = (null != editingVehicle) ? editingVehicle.TemporaryID : NewVehicleTempID(),
                                    UnitNumber = lblUnitNumber.Text,
                                    FirstAlertID = this.FirstAlertID,
                                    IsHulcherVehicle = this.IsHulcherVehicle,
                                    EquipmentID = int.Parse(hfEquipmentID.Value),
                                    Make = lblMake.Text,
                                    Model = hfModel.Value,
                                    Year = year,
                                    Damage = txtDamage.Text,
                                    EstimatedCost = estimatedCost,
                                    Active = true
                                };


                                if (null == editingVehicle)
                                    returnList.Add(vehicle);
                                else if (CurrentFirstAlertVehicleIndex == -1)
                                {
                                    returnList[editingVehicleIndex] = vehicle;
                                    editingVehicle = null;
                                }
                                else
                                    returnList[CurrentFirstAlertVehicleIndex] = vehicle;
                            }
                        }
                    }

                }
                else
                {
                    CS_FirstAlertVehicle vehicle = OtherVehicle;

                    if (null == editingVehicle)
                        returnList.Add(vehicle);
                    else
                        returnList[CurrentFirstAlertVehicleIndex] = vehicle;
                }

                return returnList;
            }
        }

        public bool FilterVehiclesByJobID
        {
            get
            {
                return chkFilterVehiclesByJob.Checked;
            }
            set
            {
                chkFilterVehiclesByJob.Checked = value;
                chkFilterVehiclesByJob.Enabled = value;
            }
        }

        public CS_FirstAlertVehicle OtherVehicle
        {
            get
            {
                CS_FirstAlertVehicle editingVehicle = null;

                if (CurrentFirstAlertVehicleIndex > -1)
                    editingVehicle = FirstAlertVehicleList[CurrentFirstAlertVehicleIndex];

                int? year = null;
                decimal? estimatedCost = null;

                if (!string.IsNullOrEmpty(txtOtherVehicleYear.Text))
                    year = int.Parse(txtOtherVehicleYear.Text);

                if (!string.IsNullOrEmpty(txtOtherVehicleEstCost.Text))
                    estimatedCost = decimal.Parse(txtOtherVehicleEstCost.Text.Replace("$", string.Empty));

                return new CS_FirstAlertVehicle()
                {
                    ID = (null != editingVehicle) ? editingVehicle.ID : 0,
                    TemporaryID = (null != editingVehicle) ? editingVehicle.TemporaryID : NewVehicleTempID(),
                    FirstAlertID = this.FirstAlertID,
                    IsHulcherVehicle = IsHulcherVehicle,
                    Make = txtOtherVehicleMake.Text,
                    Model = txtOtherVehicleModel.Text,
                    Year = year,
                    Damage = txtOtherVehicleDamage.Text,
                    EstimatedCost = estimatedCost,
                    Active = true
                };
            }
            set
            {
                if (null == value)
                {
                    txtOtherVehicleMake.Text = string.Empty;
                    txtOtherVehicleModel.Text = string.Empty;
                    txtOtherVehicleYear.Text = string.Empty;
                    txtOtherVehicleDamage.Text = string.Empty;
                    txtOtherVehicleEstCost.Text = string.Empty;
                }
                else
                {
                    txtOtherVehicleMake.Text = value.Make;
                    txtOtherVehicleModel.Text = value.Model;
                    txtOtherVehicleYear.Text = value.Year.ToString();
                    txtOtherVehicleDamage.Text = value.Damage;
                    txtOtherVehicleEstCost.Text = string.Format("{0:C}", value.EstimatedCost);
                }
            }
        }

        public CS_FirstAlertVehicle FirstAlertVehicleListDataItem
        {
            get
            {
                if (null != FirstAlertVehicleRow)
                    return FirstAlertVehicleRow.DataItem as CS_FirstAlertVehicle;

                return new CS_FirstAlertVehicle();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirstAlertVehicleListUnitNumber
        {
            set
            {
                FirstAlertVehicleRow.Cells[1].Text = value;
            }
        }

        public string FirstAlertVehicleListMake
        {
            set
            {
                FirstAlertVehicleRow.Cells[2].Text = value;
            }
        }

        public string FirstAlertVehicleListModel
        {
            set
            {
                FirstAlertVehicleRow.Cells[3].Text = value;
            }
        }

        public string FirstAlertVehicleListYear
        {
            set
            {
                FirstAlertVehicleRow.Cells[4].Text = value;
            }
        }

        public string FirstAlertVehicleListDamage
        {
            set
            {
                FirstAlertVehicleRow.Cells[5].Text = value;
            }
        }

        public string FirstAlertVehicleListHulcher
        {
            set
            {
                FirstAlertVehicleRow.Cells[6].Text = value;
            }
        }

        private GridViewRow FirstAlertVehicleRow
        {
            get;
            set;
        }

        #region [ Filtered Vehicles RowDataBound ]

        private GridViewRow FilteredEquipmentsRow { get; set; }

        public int FilteredEquipmentsEquipmentID
        {
            get
            {
                int equipmentID = 0;

                HiddenField hfEquipmentID = FilteredEquipmentsRow.FindControl("hfEquipmentID") as HiddenField;

                if (null != hfEquipmentID)
                    equipmentID = int.Parse(hfEquipmentID.Value);

                return equipmentID;
            }
            set
            {
                HiddenField hfEquipmentID = FilteredEquipmentsRow.FindControl("hfEquipmentID") as HiddenField;

                if (null != hfEquipmentID)
                    hfEquipmentID.Value = value.ToString();
            }
        }

        public string FilteredEquipmentsDamage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                TextBox txtDamage = FilteredEquipmentsRow.FindControl("txtDamage") as TextBox;

                if (null != txtDamage)
                    txtDamage.Text = value;
            }
        }

        public string FilteredEquipmentsEstCost
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                TextBox txtEstCost = FilteredEquipmentsRow.FindControl("txtEstCost") as TextBox;

                if (null != txtEstCost)
                    txtEstCost.Text = string.Format("{0:C}", double.Parse(value));
            }
        }

        public bool FilteredEquipmentsSelect
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CheckBox chkSelect = FilteredEquipmentsRow.FindControl("chkSelect") as CheckBox;

                if (null != chkSelect)
                    chkSelect.Checked = value;

                chkSelect.Enabled = !value;

                if (CurrentFirstAlertVehicleIndex > -1)
                {
                    chkSelect.CssClass = "checkedVehicle";
                    hfVehiclesAdded.Value = "hasVehicles";
                }

                chkSelect.Attributes.Add("onclick", "SetVehiclesValidation(this);");
            }
        }

        #endregion

        public bool VehiclesFormVisible
        {
            set { pnlVehiclesForm.Visible = value; }
        }

        public bool VehiclesListVisible
        {
            set { divVehiclesList.Visible = value; }
        }

        public bool HulcherVehiclesVisible
        {
            set
            { 
                chkFilterVehiclesByJob.Visible = value; 
                pnlHulcherVehicles.Visible = value; 
            }
        }

        public bool OtherVehiclesVisible
        {
            set { pnlOtherVehicles.Visible = value; }
        }

        public bool HulcherVehicleHeaderEnabled
        {
            set
            {
                rbVehiclesFormType.Enabled = value;
                chkFilterVehiclesByJob.Enabled = value;
                cbHulcherVehicleSearch.Enabled = value;
                txtHulcherVehicleSearch.Enabled = value;
                btnHulcherVehicleSearch.Enabled = value;
            }
        }

        #region [ Vehicle List Row Command ]

        public bool IsVehicleListEdit
        {
            get { return VehiclesListCommandName == "VehicleEdit"; }
        }

        public string VehiclesListCommandName { get; set; }

        public int VehiclesListCommandArgument { get; set; }

        #endregion

        #endregion

        #region [ Person ]

        public IList<FirstAlertPersonVO> FirstAlertPersonList
        {
            get
            {
                if (null == ViewState["FirstAlertPersonList"])
                    FirstAlertPersonList = new List<FirstAlertPersonVO>();

                return (IList<FirstAlertPersonVO>)ViewState["FirstAlertPersonList"];
            }
            set
            {
                ViewState["FirstAlertPersonList"] = value;
                gvPeopleList.DataSource = value;
                gvPeopleList.DataBind();
            }
        }

        public int CurrentFirstAlertPersonIndex
        {
            get
            {
                if (null == ViewState["CurrentFirstAlertPersonIndex"])
                    return -1;

                return int.Parse(ViewState["CurrentFirstAlertPersonIndex"].ToString());
            }
            set
            {
                ViewState["CurrentFirstAlertPersonIndex"] = value;
            }
        }

        public FirstAlertPersonVO NewFirstAlertPerson
        {
            get
            {
                FirstAlertPersonVO person;

                if (CurrentFirstAlertPersonIndex > -1)
                    person = FirstAlertPersonList[CurrentFirstAlertPersonIndex];
                else
                    person = new FirstAlertPersonVO();

                #region [ Is Hulcher Employee ]

                if (bool.Parse(rblIsHulcherEmployee.SelectedValue))
                {
                    person.IsHulcherEmployee = true;

                    for (int i = 0; i < gvEmployeeList.Rows.Count; i++)
                    {
                        CheckBox chk = gvEmployeeList.Rows[i].FindControl("rbSelect") as RadioButton;

                        if (chk.Checked)
                        {
                            HiddenField hfID = gvEmployeeList.Rows[i].FindControl("hfID") as HiddenField;


                            person.EmployeeID = int.Parse(hfID.Value);
                            person.LastName = gvEmployeeList.Rows[i].Cells[1].Text;
                            person.FirstName = gvEmployeeList.Rows[i].Cells[2].Text;
                            break;
                        }
                    }

                    if (null == person.EmployeeID)
                        return null;
                }
                else
                {
                    person.IsHulcherEmployee = false;
                    person.FirstName = txtPersonFirstName.Text;
                    person.LastName = txtPersonLastName.Text;

                    if (string.IsNullOrEmpty(person.FirstName) && string.IsNullOrEmpty(person.LastName))
                        return null;
                }

                #endregion

                #region [ Person Details ]

                #region [ TextBox ]

                person.InjuryNature = txtPersonInjuryNature.Text;
                person.InjuryBodyPart = txtPersonInjuryBodyPart.Text;
                person.InsuranceCompany = txtPersonInsuranceCompany.Text;
                person.PolicyNumber = txtPersonPolicyNumber.Text;
                person.Address = txtPersonAddress.Text;
                person.Details = txtPersonDetails.Text;

                #endregion

                #region [ AutoComplete/ComboBox ]

                if (cbVehicle.SelectedIndex > 0)
                    person.FirstAlertVehicleIndex = int.Parse(cbVehicle.SelectedValue);

                if (cbPersonVehiclePosition.SelectedIndex > 0)
                    person.VehiclePosition = short.Parse(cbPersonVehiclePosition.SelectedValue);

                if (cbPersonCountry.SelectedIndex > 0)
                {
                    person.CountryID = int.Parse(cbPersonCountry.SelectedValue);
                    person.CountryName = cbPersonCountry.SelectedItem.Text;
                }

                if (actPersonState.SelectedValue != "0")
                {
                    person.StateID = int.Parse(actPersonState.SelectedValue);
                    person.StateName = actPersonState.SelectedText;
                }

                if (actPersonCity.SelectedValue != "0")
                {
                    person.CityID = int.Parse(actPersonCity.SelectedValue);
                    person.CityName = actPersonCity.SelectedText;
                }


                if (actPersonZipCode.SelectedValue != "0")
                {
                    person.ZipCodeID = int.Parse(actPersonZipCode.SelectedValue);
                    person.ZipCodeName = actPersonZipCode.SelectedText;
                }

                if (cbPersonMedicalSeverity.SelectedIndex > 0)
                    person.MedicalSeverity = short.Parse(cbPersonMedicalSeverity.SelectedValue);

                #endregion

                #region [ CheckBox ]

                person.DrugScreenRequired = chkPersonDrugScreen.Checked;

                #endregion

                #endregion

                #region [ Doctor Details ]

                #region [ TextBox ]

                person.DoctorsName = txtPersonDoctorsName.Text;
                if (!string.IsNullOrEmpty(txtPersonDoctorsPhoneNumber.Text))
                    person.DoctorsPhoneNumber = txtPersonDoctorsPhoneNumber.Text;

                #endregion

                #region [ AutoComplete/ComboBox ]

                if (cbPersonDoctorsCountry.SelectedIndex > 0)
                {
                    person.DoctorsCountryID = int.Parse(cbPersonDoctorsCountry.SelectedValue);
                    person.DoctorsCountryName = cbPersonDoctorsCountry.SelectedItem.Text;
                }

                if (actPersonDoctorsState.SelectedValue != "0")
                {
                    person.DoctorsStateID = int.Parse(actPersonDoctorsState.SelectedValue);
                    person.DoctorsStateName = actPersonDoctorsState.SelectedText;
                }

                if (actPersonDoctorsCity.SelectedValue != "0")
                {
                    person.DoctorsCityID = int.Parse(actPersonDoctorsCity.SelectedValue);
                    person.DoctorsCityName = actPersonDoctorsCity.SelectedText;
                }

                if (actPersonDoctorsZipCode.SelectedValue != "0")
                {
                    person.DoctorsZipCodeID = int.Parse(actPersonDoctorsZipCode.SelectedValue);
                    person.DoctorsZipCodeName = actPersonDoctorsZipCode.SelectedText;
                }

                #endregion

                #endregion

                #region [ Hospital Details ]

                #region [ TextBox ]

                person.HospitalName = txtPersonHospitalName.Text;

                if (!string.IsNullOrEmpty(txtPersonHospitalPhoneNumber.Text))
                    person.HospitalPhoneNumber = txtPersonHospitalPhoneNumber.Text;

                #endregion

                #region [ AutoComplete/ComboBox ]

                if (cbPersonHospitalCountry.SelectedIndex > 0)
                {
                    person.HospitalCountryID = int.Parse(cbPersonHospitalCountry.SelectedValue);
                    person.HospitalCountryName = cbPersonHospitalCountry.SelectedItem.Text;
                }

                if (actPersonHospitalState.SelectedValue != "0")
                {
                    person.HospitalStateID = int.Parse(actPersonHospitalState.SelectedValue);
                    person.HospitalStateName = actPersonHospitalState.SelectedText;
                }

                if (actPersonHospitalCity.SelectedValue != "0")
                {
                    person.HospitalCityID = int.Parse(actPersonHospitalCity.SelectedValue);
                    person.HospitalCityName = actPersonHospitalCity.SelectedText;
                }

                if (actPersonHospitalZipCode.SelectedValue != "0")
                {
                    person.HospitalZipCodeID = int.Parse(actPersonHospitalZipCode.SelectedValue);
                    person.HospitalZipCodeName = actPersonHospitalZipCode.SelectedText;
                }

                #endregion

                #endregion

                #region [ Drivers License Details ]

                #region [ TextBox ]

                person.DriversLicenseNumber = txtPersonDriversLicenseNumber.Text;
                person.DriversLicenseAddress = txtPersonDriversLicenseAddress.Text;

                #endregion

                #region [ AutoComplete/ComboBox ]

                if (cbPersonDriversLicenseCountry.SelectedIndex > 0)
                {
                    person.DriversLicenseCountryID = int.Parse(cbPersonDriversLicenseCountry.SelectedValue);
                    person.DriversLicenseCountryName = cbPersonDriversLicenseCountry.SelectedValue;
                }

                if (actPersonDriversLicenseState.SelectedValue != "0")
                {
                    person.DriversLicenseStateID = int.Parse(actPersonDriversLicenseState.SelectedValue);
                    person.DriversLicenseStateName = actPersonHospitalState.SelectedText;
                }

                if (actPersonDriversLicenseCity.SelectedValue != "0")
                {
                    person.DriversLicenseCityID = int.Parse(actPersonDriversLicenseCity.SelectedValue);
                    person.DriversLicenseCityName = actPersonDriversLicenseCity.SelectedText;
                }

                if (actPersonDriversLicenseZipCode.SelectedValue != "0")
                {
                    person.DriversLicenseZipCodeID = int.Parse(actPersonDriversLicenseZipCode.SelectedValue);
                    person.DriversLicenseZipCodeName = actPersonDriversLicenseZipCode.SelectedText;
                }

                #endregion

                #endregion

                return person;
            }
            set
            {
                if (null == value)
                {
                    rblIsHulcherEmployee.SelectedValue = "true";
                    rblIsHulcherEmployee_SelectedIndexChanged(null, null);

                    txtPersonLastName.Text = string.Empty;
                    txtPersonFirstName.Text = string.Empty;

                    cbVehicle.SelectedIndex = -1;
                    cbPersonVehiclePosition.SelectedIndex = -1;

                    actPersonCity.SetValue = "0";
                    actPersonCity.SelectedText = string.Empty;
                    actPersonState.SetValue = "0";
                    actPersonState.SelectedText = string.Empty;
                    cbPersonCountry.SelectedIndex = 0;
                    actPersonZipCode.SetValue = "0";
                    actPersonZipCode.SelectedText = string.Empty;
                    txtPersonAddress.Text = string.Empty;

                    txtPersonInjuryNature.Text = string.Empty;
                    txtPersonInjuryBodyPart.Text = string.Empty;
                    cbPersonMedicalSeverity.SelectedValue = string.Empty;
                    txtPersonInsuranceCompany.Text = string.Empty;
                    txtPersonPolicyNumber.Text = string.Empty;
                    chkPersonDrugScreen.Checked = false;
                    txtPersonDetails.Text = string.Empty;

                    txtPersonDoctorsName.Text = string.Empty;
                    txtPersonDoctorsPhoneNumber.Text = string.Empty;
                    actPersonDoctorsCity.SetValue = "0";
                    actPersonDoctorsCity.SelectedText = string.Empty;
                    actPersonDoctorsState.SetValue = "0";
                    actPersonDoctorsState.SelectedText = string.Empty;
                    cbPersonCountry.SelectedIndex = 0;
                    actPersonDoctorsZipCode.SetValue = "0";
                    actPersonDoctorsZipCode.SelectedText = string.Empty;

                    txtPersonHospitalName.Text = string.Empty;
                    txtPersonHospitalPhoneNumber.Text = string.Empty;
                    actPersonHospitalCity.SetValue = "0";
                    actPersonHospitalCity.SelectedText = string.Empty;
                    actPersonHospitalState.SetValue = "0";
                    actPersonHospitalState.SelectedText = string.Empty;
                    cbPersonCountry.SelectedIndex = 0;
                    actPersonHospitalZipCode.SetValue = "0";
                    actPersonHospitalZipCode.SelectedText = string.Empty;

                    txtPersonDriversLicenseNumber.Text = string.Empty;
                    chkPersonSameAddressAsAbove.Checked = false;
                    txtPersonDriversLicenseAddress.Text = string.Empty;
                    actPersonDriversLicenseCity.SetValue = "0";
                    actPersonDriversLicenseCity.SelectedText = string.Empty;
                    actPersonDriversLicenseState.SetValue = "0";
                    actPersonDriversLicenseState.SelectedText = string.Empty;
                    cbPersonCountry.SelectedIndex = 0;
                    actPersonDriversLicenseZipCode.SetValue = "0";
                    actPersonDriversLicenseZipCode.SelectedText = string.Empty;

                }
            }
        }

        public bool FilterEmployeeByJobID
        {
            get
            {
                return chkFilterPeopleFromJob.Checked;
            }
            set
            {
                chkFilterPeopleFromJob.Checked = value;
                chkFilterPeopleFromJob.Enabled = (this.JobID != 0);
            }
        }

        public Globals.FirstAlert.EmployeeFilters? EmployeeFilter
        {
            get
            {
                if (cbHulcherPeopleSearch.SelectedValue.Equals("0") || cbHulcherPeopleSearch.SelectedValue.Equals(string.Empty))
                    return Globals.FirstAlert.EmployeeFilters.None;
                return (Globals.FirstAlert.EmployeeFilters)int.Parse(cbHulcherPeopleSearch.SelectedValue);
            }
            set
            {
                cbHulcherPeopleSearch.SelectedValue = "0";
            }
        }

        public string EmployeeFilterValue
        {
            get { return txtHulcherPeopleSearch.Text; }
            set { txtHulcherPeopleSearch.Text = value; }
        }

        public IList<CS_Employee> EmployeeDataSource
        {
            set
            {
                gvEmployeeList.DataSource = value;
                gvEmployeeList.DataBind();
            }
        }

        public string EmployeeListRowDivision
        {
            set
            {
                EmployeeListRow.Cells[0].Text = value;
            }
        }

        public string EmployeeListRowLastName
        {
            set
            {
                EmployeeListRow.Cells[1].Text = value;
            }
        }

        public string EmployeeListRowFirstName
        {
            set
            {
                EmployeeListRow.Cells[2].Text = value;
            }
        }

        public string EmployeeListRowLocation
        {
            set
            {
                EmployeeListRow.Cells[3].Text = value;
            }
        }

        public string EmployeeListRowID
        {
            set
            {
                ((HiddenField)EmployeeListRow.FindControl("hfID")).Value = value;
            }
        }

        public CS_Employee EmployeeListRowDataItem
        {
            get;
            set;
        }

        private GridViewRow EmployeeListRow
        {
            get;
            set;
        }

        public int? EditEmployeeID
        {
            get
            {
                FirstAlertPersonVO person = FirstAlertPersonList[CurrentFirstAlertPersonIndex];

                if (person.IsHulcherEmployee)
                {
                    return person.EmployeeID;
                }

                return null;
            }
        }

        public string PeopleListRowLastName
        {
            set
            {
                PeopleListRow.Cells[0].Text = value;
            }
        }

        public string PeopleListRowFirstName
        {
            set
            {
                PeopleListRow.Cells[1].Text = value;
            }
        }

        public string PeopleListRowHulcherEmployee
        {
            set
            {
                PeopleListRow.Cells[2].Text = value;
            }
        }

        public string PeopleListRowLocation
        {
            set
            {
                PeopleListRow.Cells[3].Text = value;
            }
        }

        public FirstAlertPersonVO PeopleListRowDataItem
        {
            get
            {
                return PeopleListRow.DataItem as FirstAlertPersonVO;
            }
        }

        private GridViewRow PeopleListRow
        {
            get;
            set;
        }

        public bool PeopleFormVisible
        {
            set { pnlPeopleForm.Visible = value; }
        }

        public bool PeopleListVisible
        {
            set { pnlPeopleList.Visible = value; }
        }

        public bool IsHulcherEmployee
        {
            get
            {
                return bool.Parse(rblIsHulcherEmployee.SelectedValue);
            }
            set
            {
                rblIsHulcherEmployee.SelectedValue = value.ToString().ToLower();
            }
        }

        #endregion

        #region [ Contact Personal ]

        public IList<CS_FirstAlertContactPersonal> FirstAlertContactPersonalList
        {
            get
            {
                IList<CS_FirstAlertContactPersonal> returnList = ViewState["ContactPersonal"] as IList<CS_FirstAlertContactPersonal>;
                if (null != returnList)
                {
                    for (int i = 0; i < gvContactPersonal.Rows.Count; i++)
                    {
                        HiddenField hidID = gvContactPersonal.Rows[i].FindControl("hidID") as HiddenField;
                        CheckBox chkEmailAdvised = gvContactPersonal.Rows[i].FindControl("chkEmailAdvised") as CheckBox;
                        CheckBox chkVMXAdvised = gvContactPersonal.Rows[i].FindControl("chkVMXAdvised") as CheckBox;
                        CheckBox chkInPersonAdvised = gvContactPersonal.Rows[i].FindControl("chkInPersonAdvised") as CheckBox;

                        if (hidID != null && chkEmailAdvised != null && chkVMXAdvised != null && chkInPersonAdvised != null)
                        {
                            CS_FirstAlertContactPersonal item = returnList.Where(e => e.ID == Convert.ToInt32(hidID.Value)).FirstOrDefault();
                            if (null != item)
                            {
                                bool modified = false;
                                if (chkEmailAdvised.Checked && !item.EmailAdviseDate.HasValue)
                                {
                                    item.EmailAdviseDate = DateTime.Now;
                                    item.EmailAdviseUser = UserName;
                                    modified = true;
                                }
                                else if (!chkEmailAdvised.Checked && item.EmailAdviseDate.HasValue)
                                {
                                    item.EmailAdviseDate = null;
                                    item.EmailAdviseUser = null;
                                    modified = true;
                                }

                                if (chkVMXAdvised.Checked && !item.VMXAdviseDate.HasValue)
                                {
                                    item.VMXAdviseDate = DateTime.Now;
                                    item.VMXAdviseUser = UserName;
                                    modified = true;
                                }
                                else if (!chkVMXAdvised.Checked && item.VMXAdviseDate.HasValue)
                                {
                                    item.VMXAdviseDate = null;
                                    item.VMXAdviseUser = null;
                                    modified = true;
                                }

                                if (chkInPersonAdvised.Checked && !item.InPersonAdviseDate.HasValue)
                                {
                                    item.InPersonAdviseDate = DateTime.Now;
                                    item.InPersonAdviseUser = UserName;
                                    modified = true;
                                }
                                else if (!chkInPersonAdvised.Checked && item.InPersonAdviseDate.HasValue)
                                {
                                    item.InPersonAdviseDate = null;
                                    item.InPersonAdviseUser = null;
                                    modified = true;
                                }

                                if (modified)
                                {
                                    item.ModificationDate = DateTime.Now;
                                    item.ModifiedBy = UserName;
                                }

                                // Clear related items, in order to update data
                                item.CS_Contact = null;
                                item.CS_Employee = null;
                                item.CS_FirstAlert = null;
                                item.EntityKey = null;
                            }
                        }
                    }
                }
                return returnList;
            }
            set
            {
                ViewState["ContactPersonal"] = value;
                gvContactPersonal.DataSource = value;
                gvContactPersonal.DataBind();
            }
        }

        #region [ Grid Row ]

        public TableRow ContactPersonalRow { get; set; }

        public CS_FirstAlertContactPersonal ContactPersonalRowDataItem { get; set; }

        public int ContactPersonalRowID
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hidID = ContactPersonalRow.FindControl("hidID") as HiddenField;
                if (null != hidID) hidID.Value = value.ToString();
            }
        }

        public string ContactPersonalRowName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblName = ContactPersonalRow.FindControl("lblName") as Label;
                if (null != lblName) lblName.Text = value.ToString();
            }
        }

        public DateTime? ContactPersonalRowEmailAdvisedDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkEmailAdvised = ContactPersonalRow.FindControl("chkEmailAdvised") as CheckBox;
                if (value.HasValue)
                {
                    Label lblEmailAdvisedDate = ContactPersonalRow.FindControl("lblEmailAdvisedDate") as Label;
                    if (null != lblEmailAdvisedDate) lblEmailAdvisedDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                    if (null != chkEmailAdvised) chkEmailAdvised.Checked = true;
                }
                else
                    if (null != chkEmailAdvised) chkEmailAdvised.Checked = false;
            }
        }
        
        public DateTime? ContactPersonalRowVMXAdvisedDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkVMXAdvised = ContactPersonalRow.FindControl("chkVMXAdvised") as CheckBox;
                if (value.HasValue)
                {
                    Label lblVMXAdvisedDate = ContactPersonalRow.FindControl("lblVMXAdvisedDate") as Label;
                    if (null != lblVMXAdvisedDate) lblVMXAdvisedDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                    if (null != chkVMXAdvised) chkVMXAdvised.Checked = true;
                }
                else
                    if (null != chkVMXAdvised) chkVMXAdvised.Checked = false;
            }
        }

        public DateTime? ContactPersonalRowInPersonAdvisedDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkInPersonAdvised = ContactPersonalRow.FindControl("chkInPersonAdvised") as CheckBox;
                if (value.HasValue)
                {
                    Label lblInPersonAdvisedDate = ContactPersonalRow.FindControl("lblInPersonAdvisedDate") as Label;
                    if (null != lblInPersonAdvisedDate) lblInPersonAdvisedDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                    if (null != chkInPersonAdvised) chkInPersonAdvised.Checked = true;
                }
                else
                    if (null != chkInPersonAdvised) chkInPersonAdvised.Checked = false;
            }
        }

        #endregion

        #endregion

        #endregion

        #region [ Methods ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        private void ClearJobFields()
        {
            actAlertHeaderJobNumber.SetValue = "0";
            actAlertHeaderJobNumber.SelectedText = string.Empty;
            actAlertHeaderJobNumber.Enabled = true;

            actAlertHeaderCustomer.SetValue = "0";
            actAlertHeaderCustomer.SelectedText = string.Empty;
            actAlertHeaderCustomer.Enabled = true;

            actAlertHeaderCity.SetValue = "0";
            actAlertHeaderCity.SelectedText = string.Empty;
            actAlertHeaderCity.Enabled = true;

            actAlertHeaderState.SetValue = "0";
            actAlertHeaderState.SelectedText = string.Empty;
            actAlertHeaderState.Enabled = true;

            actAlertHeaderEIC.SetValue = "0";
            actAlertHeaderEIC.SelectedText = string.Empty;
            actAlertHeaderEIC.Enabled = true;

            cbAlertHeaderCountry.SelectedIndex = 0;
            cbAlertHeaderCountry.Enabled = true;
        }

        public void BlockPeopleFilterForEdit()
        {
            rblIsHulcherEmployee.Enabled = false;
            chkFilterPeopleFromJob.Enabled = false;
            cbHulcherPeopleSearch.Enabled = false;
            txtHulcherPeopleSearch.Enabled = false;
            btnHulcherPeopleSearch.Enabled = false;
        }

        public void UnblockPeopleFilterForEdit()
        {
            rblIsHulcherEmployee.Enabled = true;
            chkFilterPeopleFromJob.Enabled = true;
            cbHulcherPeopleSearch.Enabled = true;
            txtHulcherPeopleSearch.Enabled = true;
            btnHulcherPeopleSearch.Enabled = true;
        }

        #endregion

        #region [ First Alert Form ]

        public void LoadDivisionsOnTextBox(List<string> lst)
        {
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    txtDivision.Text += "," + ddlDivision.Items.FindByValue(lst[i]).Value;
                }
            }
        }

        public void LoadFirstAlertTypeOnTextBox(List<string> lst)
        {
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    txtFirstAlertType.Text += "," + ddlFirstAlertType.Items.FindByValue(lst[i]).Value;
                }
            }
        }

        #endregion

        #region [ Vehicle ]

        public int NewVehicleTempID()
        {
            int newVehicleTempID = 0;

            if (null == ViewState["NewVehicleTempID"])
                newVehicleTempID++;
            else
            {
                newVehicleTempID = (int)ViewState["NewVehicleTempID"];
                newVehicleTempID++;
            }

            ViewState["NewVehicleTempID"] = newVehicleTempID;

            return newVehicleTempID;
        }

        #endregion

        #region [ Person ]

        private string VehicleTempID()
        {
            FirstAlertPersonVO person = FirstAlertPersonList[CurrentFirstAlertPersonIndex];

            if (person.FirstAlertVehicleIndex.HasValue)
            {
                return person.FirstAlertVehicleIndex.ToString();
            }
            else
            {
                for (int i = 0; i < FirstAlertVehicleList.Count; i++)
                {
                    if (person.FirstAlertVehicleID.Value == FirstAlertVehicleList[i].ID)
                    {
                        return FirstAlertVehicleList[i].TemporaryID.ToString();
                    }
                }
            }

            return "";
        }

        private void LoadPersonDetails()
        {
            if (CurrentFirstAlertPersonIndex > -1)
            {
                FirstAlertPersonVO person = FirstAlertPersonList[CurrentFirstAlertPersonIndex];

                if (person != null)
                {
                    #region [ Is Hulcher Employee ]

                    rblIsHulcherEmployee.SelectedValue = person.IsHulcherEmployee.ToString().ToLower();

                    if (person.IsHulcherEmployee)
                    {
                        if (EditEmployeeID.HasValue)
                        {
                            _presenter.BindEmployeeByID();

                            ((RadioButton)gvEmployeeList.Rows[0].FindControl("rbSelect")).Checked = true;
                        }
                        pnlHulcherPeople.Visible = true;
                        pnlOtherPeople.Visible = false;

                    }
                    else
                    {
                        txtPersonLastName.Text = person.LastName;
                        txtPersonFirstName.Text = person.FirstName;

                        pnlHulcherPeople.Visible = false;
                        pnlOtherPeople.Visible = true;
                    }

                    #endregion

                    #region [ Person Details ]

                    #region [ TextBox ]

                    txtPersonInjuryNature.Text = person.InjuryNature;
                    txtPersonInjuryBodyPart.Text = person.InjuryBodyPart;
                    txtPersonInsuranceCompany.Text = person.InsuranceCompany;
                    txtPersonPolicyNumber.Text = person.PolicyNumber;
                    txtPersonAddress.Text = person.Address;
                    txtPersonDetails.Text = person.Details;

                    #endregion

                    #region [ AutoComplete/ComboBox ]

                    if (person.FirstAlertVehicleID.HasValue || person.FirstAlertVehicleIndex.HasValue)
                        cbVehicle.SelectedValue = VehicleTempID();

                    if (person.VehiclePosition.HasValue)
                        cbPersonVehiclePosition.SelectedValue = person.VehiclePosition.Value.ToString();

                    if (person.CountryID.HasValue)
                        cbPersonCountry.SelectedValue = person.CountryID.ToString();

                    if (person.StateID.HasValue)
                    {
                        actPersonState.SetValue = person.StateID.Value.ToString();
                        actPersonState.SelectedText = person.StateName;
                    }

                    if (person.CityID.HasValue)
                    {
                        actPersonCity.SetValue = person.CityID.Value.ToString();
                        actPersonCity.SelectedText = person.CityName;
                    }

                    if (person.ZipCodeID.HasValue)
                    {
                        actPersonZipCode.SetValue = person.ZipCodeID.Value.ToString();
                        actPersonZipCode.SelectedText = person.ZipCodeName;
                    }

                    if (person.MedicalSeverity != 0)
                        cbPersonMedicalSeverity.SelectedValue = person.MedicalSeverity.ToString();

                    #endregion

                    #region [ CheckBox ]

                    if (person.DrugScreenRequired.HasValue)
                        chkPersonDrugScreen.Checked = person.DrugScreenRequired.Value;

                    #endregion

                    #endregion

                    #region [ Doctor Details ]

                    #region [ TextBox ]

                    txtPersonDoctorsName.Text = person.DoctorsName;

                    if (!string.IsNullOrEmpty(person.DoctorsPhoneNumber))
                        txtPersonDoctorsPhoneNumber.Text = person.DoctorsPhoneNumber;

                    #endregion

                    #region [ AutoComplete/ComboBox ]

                    if (person.DoctorsCountryID.HasValue)
                        cbPersonDoctorsCountry.SelectedValue = person.DoctorsCountryID.ToString();

                    if (person.DoctorsStateID.HasValue)
                    {
                        actPersonDoctorsState.SetValue = person.DoctorsStateID.Value.ToString();
                        actPersonDoctorsState.SelectedText = person.DoctorsStateName;
                    }

                    if (person.DoctorsCityID.HasValue)
                    {
                        actPersonDoctorsCity.SetValue = person.DoctorsCityID.Value.ToString();
                        actPersonDoctorsCity.SelectedText = person.DoctorsCityName;
                    }

                    if (person.DoctorsZipCodeID.HasValue)
                    {
                        actPersonDoctorsZipCode.SetValue = person.DoctorsZipCodeID.Value.ToString();
                        actPersonDoctorsZipCode.SelectedText = person.DoctorsZipCodeName;
                    }

                    #endregion

                    #endregion

                    #region [ Hospital Details ]

                    #region [ TextBox ]

                    txtPersonHospitalName.Text = person.HospitalName;

                    if (!string.IsNullOrEmpty(person.HospitalPhoneNumber))
                        txtPersonHospitalPhoneNumber.Text = person.HospitalPhoneNumber;

                    #endregion

                    #region [ AutoComplete/ComboBox ]

                    if (person.HospitalCountryID.HasValue)
                        cbPersonHospitalCountry.SelectedValue = person.HospitalCountryID.ToString();

                    if (person.HospitalStateID.HasValue)
                    {
                        actPersonHospitalState.SetValue = person.HospitalStateID.Value.ToString();
                        actPersonHospitalState.SelectedText = person.HospitalStateName;
                    }

                    if (person.HospitalCityID.HasValue)
                    {
                        actPersonHospitalCity.SetValue = person.HospitalCityID.Value.ToString();
                        actPersonHospitalCity.SelectedText = person.HospitalCityName;
                    }

                    if (person.HospitalZipCodeID.HasValue)
                    {
                        actPersonHospitalZipCode.SetValue = person.HospitalZipCodeID.Value.ToString();
                        actPersonHospitalZipCode.SelectedText = person.HospitalZipCodeName;
                    }

                    #endregion

                    #endregion

                    #region [ Drivers License Details ]

                    #region [ TextBox ]

                    txtPersonDriversLicenseNumber.Text = person.DriversLicenseNumber;
                    txtPersonDriversLicenseAddress.Text = person.DriversLicenseAddress;

                    #endregion

                    #region [ AutoComplete/ComboBox ]

                    if (person.DriversLicenseCountryID.HasValue)
                        cbPersonDriversLicenseCountry.SelectedValue = person.DriversLicenseCountryID.ToString();

                    if (person.DriversLicenseStateID.HasValue)
                    {
                        actPersonDriversLicenseState.SetValue = person.DriversLicenseStateID.Value.ToString();
                        actPersonDriversLicenseState.SelectedText = person.DriversLicenseStateName;
                    }

                    if (person.DriversLicenseCityID.HasValue)
                    {
                        actPersonDriversLicenseCity.SetValue = person.DriversLicenseCityID.Value.ToString();
                        actPersonDriversLicenseCity.SelectedText = person.DriversLicenseCityName;
                    }

                    if (person.DriversLicenseZipCodeID.HasValue)
                    {
                        actPersonDriversLicenseZipCode.SetValue = person.DriversLicenseZipCodeID.Value.ToString();
                        actPersonDriversLicenseZipCode.SelectedText = person.DriversLicenseZipCodeName;
                    }

                    #endregion

                    #endregion
                }

                BlockPeopleFilterForEdit();
            }
        }

        #endregion

        #region [ Report ]

        public void OpenReport(int firstAlertId)
        {
            ScriptManager.RegisterClientScriptBlock(
                this,
                this.GetType(),
                "OpenReport",
                string.Format(
                    "var reportWindow = window.open('FirstAlertReport.aspx?FirstAlertId={0}', '', 'width=1040, height=600, scrollbars=1, resizable=yes');",
                    firstAlertId),
                true);
        }

        #endregion

        #endregion
    }
}
