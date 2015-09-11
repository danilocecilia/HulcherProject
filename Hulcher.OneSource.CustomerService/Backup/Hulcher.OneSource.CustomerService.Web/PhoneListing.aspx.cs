using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class PhoneListing : System.Web.UI.Page, IPhoneListingView
    {
        #region [ Attributes ]

        private PhoneListingPresenter _presenter;
        
        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new PhoneListingPresenter(this);
        }
        
        #endregion

        #region [ Methods ]

        public void ClearFields()
        {
            cboViewBy.SelectedValue = "0";
            txtSearchFor.Text = string.Empty;
        }

        public void ClearDivisionFields()
        {
            hfPhoneNumbers.Text = string.Empty;
            LocalDivisionID = null;
            actState.ClearSelection();
            actCity.ClearSelection();
            actZipCode.ClearSelection();
            btnDelete.Enabled = false;
            txtDivisionName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlDivisionPhoneType.SelectedIndex = 0;
            txtDivisionPhoneArea.Text = string.Empty;
            txtDivisionPhoneNumber.Text = string.Empty;
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.BindPhoneTypeListing();
        }

        protected void cboViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboViewBy.SelectedValue == "1")
            {
                btnAddDivision.Style[HtmlTextWriterStyle.Display] = "";
            }
            else
            {
                btnAddDivision.Style[HtmlTextWriterStyle.Display] = "none";
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredPhoneListing();
        }

        protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PhoneListingCustomerRow = e.Row;
                PhoneListingCustomerRowDataItem = e.Row.DataItem as CustomerPhoneVO;

                _presenter.BindPhoneListingCustomerRow();
            }
        }

        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PhoneListingEmployeeRow = e.Row;

                PhoneListingDataItem = e.Row.DataItem as EmployeePhoneVO;

                _presenter.BindPhoneListingEmployee();
            }
        }

        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDivision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PhoneListingDivisionRow = e.Row;
                PhoneListingDivisionRowDataItem = e.Row.DataItem as DivisionPhoneNumberVO;

                _presenter.BindPhoneListingDivisionRow();
            }
        }

        protected void gvDivision_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditDivision")
            {
                _presenter.ClearDivisionFields();
                LocalDivisionID = Convert.ToInt32(e.CommandArgument);
                _presenter.LoadLocalDivision();
            }
        }

        protected void btnAddDivision_Click(object sender, EventArgs e)
        {
            ShowHidePanelButtons = true;
            ShowHidePanelDivisionPhoneNumber = true;
            ShowHideFilter = false;
            _presenter.ClearDivisionFields();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.DeleteLocalDivision();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.ClearFields();
            ShowHidePanelButtons = false;
            ShowHidePanelDivisionPhoneNumber = false;
            ShowHideFilter = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.SaveLocalDivision();
        }

        #endregion

        #region [ Properties ]

        #region [ Filter ]
        public Globals.Phone.PhoneFilterType PhoneListingFilter
        {
            get
            {
                return (Globals.Phone.PhoneFilterType)Enum.Parse(typeof(Globals.Phone.PhoneFilterType), cboViewBy.SelectedValue);
            }
        }

        public string FilterValue
        {
            get
            {
                return txtSearchFor.Text;
            }
        }

        public bool ShowHideFilter
        {
            get
            {
                return pnlFilter.Visible;
            }
            set
            {
                pnlFilter.Visible = value;
            }
        }
        #endregion

        #region [ Customer ]

        private GridViewRow PhoneListingCustomerRow { get; set; }

        public IList<CustomerPhoneVO> PhoneListingCustomerDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                gvCustomer.DataSource = value;
                gvCustomer.DataBind();
            }
        }
        public CustomerPhoneVO PhoneListingCustomerRowDataItem
        {
            get;
            set;
        }

        public string PhoneListingCustomerCustomerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCustomerName = PhoneListingCustomerRow.FindControl("lblCustomerName") as Label;
                if (null != lblCustomerName)
                {
                    lblCustomerName.Text = value;
                }
            }
        }
        public string PhoneListingCustomerCustomerNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCustomerNumber = PhoneListingCustomerRow.FindControl("lblCustomerNumber") as Label;
                if (null != lblCustomerNumber)
                {
                    lblCustomerNumber.Text = value;
                }
            }
        }
        public string PhoneListingCustomerContactName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblContactName = PhoneListingCustomerRow.FindControl("lblContactName") as Label;
                if (null != lblContactName)
                {
                    lblContactName.Text = value;
                }
            }
        }
        public string PhoneListingCustomerPhoneType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblPhoneType = PhoneListingCustomerRow.FindControl("lblPhoneType") as Label;
                if (null != lblPhoneType)
                {
                    lblPhoneType.Text = value;
                }
            }
        }
        public string PhoneListingCustomerPhoneNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblPhoneNumber = PhoneListingCustomerRow.FindControl("lblPhoneNumber") as Label;
                if (null != lblPhoneNumber)
                {
                    lblPhoneNumber.Text = value;
                }
            }
        }
        public string PhoneListingCustomerCustomerNotes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCustomerNotes = PhoneListingCustomerRow.FindControl("lblCustomerNotes") as Label;
                if (null != lblCustomerNotes)
                {
                    lblCustomerNotes.Text = value;
                }
            }
        }

        #endregion

        #region [ Employee ]

        public GridViewRow PhoneListingEmployeeRow { get; set; }

        public string PhoneListingEmployeeNameRow
        {
            set
            {
                Label lblEmployeeName = PhoneListingEmployeeRow.FindControl("lblEmployeeName") as Label;
                lblEmployeeName.Text = value;
            }
        }

        public string PhoneListingPhoneTypeRow
        {
            set
            {
                Label lblPhoneType = PhoneListingEmployeeRow.FindControl("lblPhoneType") as Label;
                lblPhoneType.Text = value;
            }
        }

        public string PhoneListingPhoneNumber
        {
            set
            {
                Label lblPhoneNumber = PhoneListingEmployeeRow.FindControl("lblPhoneNumber") as Label;
                lblPhoneNumber.Text = value;
            }
        }

        public EmployeePhoneVO PhoneListingDataItem
        {
            get;
            set;
        }

        public List<EmployeePhoneVO> ListFilteredEmployeePhoneListing
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                gvEmployee.DataSource = value;
                gvEmployee.DataBind();
            }
        }

        #endregion

        #region [ Division ]

        public bool ShowHidePanelButtons
        {
            get
            {
                return pnlButtons.Visible;
            }
            set
            {
                pnlButtons.Visible = value;
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return btnDelete.Enabled;
            }
            set
            {
                btnDelete.Enabled = value;
            }
        }

        public bool ShowHidePanelDivisionPhoneNumber
        {
            get
            {
                return pnlDivisionPhoneNumber.Visible;
            }
            set
            {
                pnlDivisionPhoneNumber.Visible = value;
            }
        }

        public IList<CS_PhoneType> DivisionPhoneTypeListingDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlDivisionPhoneType.DataSource = value;
                ddlDivisionPhoneType.DataBind();
            }
        }

        #region [ Grid ]

        private GridViewRow PhoneListingDivisionRow
        {
            get;
            set;
        }

        public IList<DivisionPhoneNumberVO> PhoneListingDivisionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                gvDivision.DataSource = value;
                gvDivision.DataBind();
            }
        }

        public DivisionPhoneNumberVO PhoneListingDivisionRowDataItem
        {
            get;
            set;
        }

        public int PhoneListingDivisionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Button btnEditDivision = PhoneListingDivisionRow.FindControl("btnEditDivision") as Button;
                if (null != btnEditDivision)
                {
                    btnEditDivision.CommandArgument = value.ToString();
                }
            }
        }

        public string PhoneListingDivisionName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblDivision = PhoneListingDivisionRow.FindControl("lblDivision") as Label;
                if (null != lblDivision)
                {
                    lblDivision.Text = value;
                }
            }
        }

        public string PhoneListingDivisionAddress
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblAddress = PhoneListingDivisionRow.FindControl("lblAddress") as Label;
                if (null != lblAddress)
                {
                    lblAddress.Text = value;
                }
            }
        }

        public string PhoneListingDivisionPhoneType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblPhoneType = PhoneListingDivisionRow.FindControl("lblPhoneType") as Label;
                if (null != lblPhoneType)
                {
                    lblPhoneType.Text = value;
                }
            }
        }

        public string PhoneListingDivisionPhoneNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblPhoneNumber = PhoneListingDivisionRow.FindControl("lblPhoneNumber") as Label;
                if (null != lblPhoneNumber)
                {
                    lblPhoneNumber.Text = value;
                }
            }
        }

        #endregion

        public int? LocalDivisionID
        {
            get
            {
                if (null == ViewState["LocalDivisionID"])
                    return null;
                else
                    return Convert.ToInt32(ViewState["LocalDivisionID"]);
            }
            set { ViewState["LocalDivisionID"] = value; }
        }

        public string LocalDivisionNumber
        {
            get { return txtDivisionName.Text; }
            set { txtDivisionName.Text = value; }
        }

        public string LocalDivisionAddress
        {
            get { return txtAddress.Text; }
            set { txtAddress.Text = value; }
        }

        public int? LocalDivisionStateID
        {
            get
            {
                if (actState.SelectedValue != "0")
                    return int.Parse(actState.SelectedValue);

                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    actState.SetValue = value.Value.ToString();
                    actCity.ContextKey = value.Value.ToString();
                }
            }
        }

        public int? LocalDivisionCityID
        {
            get
            {
                if (actCity.SelectedValue != "0")
                    return int.Parse(actCity.SelectedValue);

                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    actCity.SetValue = value.Value.ToString();
                    actZipCode.ContextKey = value.Value.ToString();
                }
            }
        }

        public int? LocalDivisionZipCodeID
        {
            get
            {
                if (actZipCode.SelectedValue != "0")
                    return int.Parse(actZipCode.SelectedValue);

                return null;
            }
            set
            {
                if (value.HasValue)
                    actZipCode.SetValue = value.Value.ToString();
            }
        }

        public string LocalDivisionStateName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                actState.SelectedText = value;
            }
        }

        public string LocalDivisionCityName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                actCity.SelectedText = value;
            }
        }

        public string LocalDivisionZipCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                actZipCode.SelectedText = value;
            }
        }

        public IList<CS_DivisionPhoneNumber> LocalDivisionPhoneListing
        {
            get
            {
                List<CS_DivisionPhoneNumber> returnList = new List<CS_DivisionPhoneNumber>();
                string[] phoneNumbers = hfPhoneNumbers.Text.Split('|');

                if (!string.IsNullOrEmpty(hfPhoneNumbers.Text))
                {
                    for (int i = 0; i < phoneNumbers.Length; i++)
                    {
                        string[] values = phoneNumbers[i].Split(';');

                        CS_DivisionPhoneNumber newPhone = new CS_DivisionPhoneNumber();

                        if (!values[0].Equals("0"))
                            newPhone.ID = int.Parse(values[0]);

                        newPhone.Number = values[2];

                        switch (values[1])
                        {
                            case "Home":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.Home;
                                break;
                            case "Cell":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.Cell;
                                break;
                            case "Fax":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.Fax;
                                break;
                            case "VMX":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.VMX;
                                break;
                            case "Extension":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.Extension;
                                break;
                            case "Pager":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.Pager;
                                break;
                            case "PIN Number":
                                newPhone.PhoneTypeID = (int)Globals.Phone.PhoneType.PINNumber;
                                break;
                        }

                        returnList.Add(newPhone);
                    }

                    return returnList;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    CS_DivisionPhoneNumber number = value[i];
                    string phoneToAdd = string.Format("{0};{1};{2}", number.ID, number.CS_PhoneType.Name, number.Number);

                    if (hfPhoneNumbers.Text.Equals(string.Empty))
                    {
                        hfPhoneNumbers.Text += phoneToAdd;
                    }
                    else
                    {
                        hfPhoneNumbers.Text += string.Format("|{0}", phoneToAdd);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "createTable", "CreatePhoneTable();", true);
            }
        }

        #endregion

        #region [ Common ]

        public string Username
        {
            get { return Master.Username; }
        }

        #endregion

        #endregion

        #region [ Methods ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void AlterVisibilityCustomerGrid(bool value)
        {
            gvCustomer.Visible = value;
        }

        public void AlterVisibilityEmployeeGrid(bool value)
        {
            gvEmployee.Visible = value;
        }

        public void AlterVisibilityDivisionGrid(bool value)
        {
            gvDivision.Visible = value;
        }

        #endregion

        #endregion


        
    }
}
