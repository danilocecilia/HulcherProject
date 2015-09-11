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
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class EmployeeMaintenance : System.Web.UI.Page, IEmployeeMaintenanceView
    {
        #region [ Attributes ]

        private EmployeeMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new EmployeeMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.VerifyAccess();
            if (!IsPostBack)
            {
                if (null != Request.QueryString["EmployeeID"])
                {
                    EmployeeId = Convert.ToInt32(Request.QueryString["EmployeeID"]);
                    actEmployee.SelectedValue = EmployeeId.Value.ToString();
                    FillEmployeeFields();
                }
            }
        }

        protected void EmployeeContact_OnTextChanged(object sender, EventArgs e)
        {
            FillEmployeeFields();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            KeepValidatorsState();
            if (IsValid)
                _presenter.Save();
        }

     

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public string Domain
        {
            get { return Master.Domain; }
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

        #endregion

        #region [ Call Criteria ]

        public int? CallCriteriaEmployeeID
        {
            get
            {
                return uscCallCriteria.EmployeeID;
            }
            set { uscCallCriteria.EmployeeID = value; }
        }

        #endregion

        #region [ Employee ]

        public string DayAreaCode
        {
            get
            {
                return txtEmployeeDayCode.Text;
            }
            set
            {
                txtEmployeeDayCode.Text = value;
            }
        }

        public string DayPhone
        {
            get
            {
                return txtEmployeeDayPhone.Text;
            }
            set
            {
                txtEmployeeDayPhone.Text = value;
            }
        }

        public string FaxAreaCode
        {
            get
            {
                return txtEmployeeFaxCode.Text;
            }
            set
            {
                txtEmployeeFaxCode.Text = value;
            }
        }

        public string FaxPhone
        {
            get
            {
                return txtEmployeeFaxPhone.Text;
            }
            set
            {
                txtEmployeeFaxPhone.Text = value;
            }
        }

        public int? EmployeeId
        {
            get
            {
                if (null != ViewState["EmployeeID"])
                {
                    return Convert.ToInt32(ViewState["EmployeeID"]);
                    
                }
                else if (string.Empty != actEmployee.SelectedValue)
                {
                    return Int32.Parse(actEmployee.SelectedValue);
                    
                }
                return null;
            }

            set
            {
                ViewState["EmployeeID"] = value;
                uscCallCriteria.EmployeeID = value;
            }
        }

        public string EmployeeName
        {
            get { return lblEmployeeNameDesc.Text; }
            set { lblEmployeeNameDesc.Text = value; }
        }

        public DateTime? HireDate
        {
            get
            {
                if (string.IsNullOrEmpty(lblEmployeeHireDateDesc.Text))
                    return null;
                else
                    return Convert.ToDateTime(lblEmployeeHireDateDesc.Text, new System.Globalization.CultureInfo("en-US"));
            }
            set
            {
                if (value.HasValue)
                    lblEmployeeHireDateDesc.Text = value.Value.ToString("MM/dd/yyyy");
                else
                    lblEmployeeHireDateDesc.Text = string.Empty;
            }
        }

        public string PersonID
        {
            get { return lblEmployeeIDDesc.Text; }
            set { lblEmployeeIDDesc.Text = value; }
        }

        public string Position
        {
            get { return lblEmployeePositionDesc.Text; }
            set { lblEmployeePositionDesc.Text = value; }
        }

        public string PassportNumber
        {
            get { return lblEmployeePassportDesc.Text; }
            set { lblEmployeePassportDesc.Text = value; }
        }

        public string EmployeeDivision
        {
            get { return lblEmployeeDivisionDesc.Text; }
            set
            {
                lblEmployeeDivisionDesc.Text = value;
                lblDivisionName.Text = value;
            }
        }

        public IList<CS_EmployeeEmergencyContact> EmployeeContacts
        {
            get { throw new NotImplementedException(); }
            set
            {
                gvICEContacts.DataSource = value;
                gvICEContacts.DataBind();
            }
        }

        public string Address
        {
            get { return txtAddress.Text; }
            set { txtAddress.Text = value; }
        }

        public string Address2
        {
            get { return txtAddress2.Text; }
            set { txtAddress2.Text = value; }
        }

        public string City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }

        public string State
        {
            get { return txtStateProvinceCode.Text; }
            set { txtStateProvinceCode.Text = value; }
        }

        public string Country
        {
            get { return txtCountryCode.Text; }
            set { txtCountryCode.Text = value; }
        }

        public string PostalCode
        {
            get { return txtPostalCode.Text; }
            set { txtPostalCode.Text = value; }
        }

        public string HomeAreaCode
        {
            get { return txtEmployeeHomePhoneArea.Text; }
            set { txtEmployeeHomePhoneArea.Text = value; }
        }

        public string HomePhone
        {
            get { return txtEmployeeHomePhoneNumber.Text; }
            set { txtEmployeeHomePhoneNumber.Text = value; }
        }

        public string MobileAreaCode
        {
            get { return txtEmployeeCellPhoneArea.Text; }
            set { txtEmployeeCellPhoneArea.Text = value; }
        }

        public string MobilePhone
        {
            get { return txtEmployeeCellPhoneNumber.Text; }
            set { txtEmployeeCellPhoneNumber.Text = value; }
        }

        public string OtherPhoneAreaCode
        {
            get { return txtEmployeeOtherPhoneArea.Text; }
            set { txtEmployeeOtherPhoneArea.Text = value; }
        }

        public string OtherPhone
        {
            get { return txtEmployeeOtherPhoneNumber.Text; }
            set { txtEmployeeOtherPhoneNumber.Text = value; }
        }

        public bool IsDentonPersonal
        {
            get { return chkDentonPersonel.Checked; }
            set { chkDentonPersonel.Checked = value; }
        }

        public List<CS_PhoneType> AdditionalContactPhoneTypeSource
        {
            set
            {
                ddlAdditionalContactType.DataSource = value;
                ddlAdditionalContactType.DataBind();
            }
        }

        public List<PhoneNumberVO> AdditionalContactPhoneList
        {
            get
            {
                List<PhoneNumberVO> returnList = new List<PhoneNumberVO>();
                string[] phoneNumbers = hfPhoneNumbers.Value.Split('|');

                if (!string.IsNullOrEmpty(hfPhoneNumbers.Value))
                {
                    for (int i = 0; i < phoneNumbers.Length; i++)
                    {
                        string[] values = phoneNumbers[i].Split(';');

                        PhoneNumberVO newPhone = new PhoneNumberVO();

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
                            case "VMX":
                                newPhone.TypeID = (int)Globals.Phone.PhoneType.VMX;
                                break;
                            case "Extension":
                                newPhone.TypeID = (int)Globals.Phone.PhoneType.Extension;
                                break;
                            case "Pager":
                                newPhone.TypeID = (int)Globals.Phone.PhoneType.Pager;
                                break;
                            case "PIN Number":
                                newPhone.TypeID = (int)Globals.Phone.PhoneType.PINNumber;
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
        }

        public List<CS_PhoneNumber> AdditionalContactPhoneGridDataSource
        {
            set
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

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "createTable", "CreatePhoneTable();", true);
            }
        }

        public bool IsKeyPerson
        {
            get {return chkKeyPerson.Checked;}
            set {chkKeyPerson.Checked = value;}
        }

        #endregion

        #region [ Off Call ]

        public bool IsOffCall
        {
            get { return chkOffCall.Checked; }
            set { chkOffCall.Checked = value; }
        }

        public int? ProxyEmployeeId
        {
            get
            {
                if (actProxyEmployee.SelectedValue.Equals("0"))
                    return null;
                else
                    return Convert.ToInt32(actProxyEmployee.SelectedValue);
            }
            set
            {
                actProxyEmployee.SelectedValue = value.ToString();
            }
        }

        public DateTime? OffCallStartDate
        {
            get { return dpOffCallFrom.Value; }
            set { dpOffCallFrom.Value = value; }
        }

        public DateTime? OffCallEndDate
        {
            get { return dpOffCallTo.Value; }
            set { dpOffCallTo.Value = value; }
        }

        public TimeSpan? OffCallReturnTime
        {
            get
            {
                if (string.IsNullOrEmpty(txtReturnTime.Text))
                    return null;
                else
                    return TimeSpan.Parse(txtReturnTime.Text);
            }
            set
            {
                if (value.HasValue)
                    txtReturnTime.Text = string.Format("{0}:{1}", value.Value.Hours, value.Value.Minutes);
            }
        }

        public IList<CS_EmployeeOffCallHistory> OffCallHistoryList
        {
            get { throw new NotImplementedException(); }
            set
            {
                gvOffCallHistory.DataSource = value;
                gvOffCallHistory.DataBind();
            }
        }

        #endregion

        #region [ Coverage ]

        public bool RequireCoverageFields
        {
            get { throw new NotImplementedException(); }
            set
            {
                dpEmployeeCoverageTO.EnableDisableMasketEditValidator = value;
                dpEmployeeCoverageFrom.EnableDisableMasketEditValidator = value;
                rfvCoverageTimeValidatorTO.Enabled = value;
                rfvCoverageTimeValidatorFrom.Enabled = value;
            }
        }

        public bool DisplayCoverageStartFields
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value == false)
                {
                    pnlEmployeeCoverageStart.Style.Add("display", "none");
                }
                else
                {
                    pnlEmployeeCoverageStart.Style.Add("display", "block");
                }
            }
        }

        public bool DisplayCoverageEndFields
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value == false)
                {
                    pnlEmployeeCoverageEnd.Style.Add("display", "none");
                }
                else
                {
                    pnlEmployeeCoverageEnd.Style.Add("display", "block");
                }
            }
        }

        public bool IsEmployeeAssignedToJob
        {
            get
            {
                if (null == ViewState["IsEmployeeAssignedToJob"])
                    return false;

                return bool.Parse(ViewState["IsEmployeeAssignedToJob"].ToString());
            }
            set
            {
                ViewState["IsEmployeeAssignedToJob"] = value;

                if (value)
                {
                    btnSaveClose.OnClientClick = "return ValidateJobAssigment();";
                    lblOffCallMsg.Visible = true;
                    pnlOffCall.Visible = false;
                }
            }
        }

        public bool IsCoverageEdit
        {
            get { return (chkCoverage.Checked || hfIsCoverage.Value == "1"); }
        }

        public bool IsCoverage
        {
            get { return chkCoverage.Checked; }
            set
            {
                chkCoverage.Checked = value;
                if (value)
                {
                    hfIsCoverage.Value = "1";
                    rfvCoverageDuration.Enabled = true;
                    cpvCoverageDuration.Enabled = true;
                    actDivision.RequiredFieldControl.Enabled = true;
                }
                else
                {
                    hfIsCoverage.Value = string.Empty;
                    rfvCoverageDuration.Enabled = false;
                    cpvCoverageDuration.Enabled = false;
                    actDivision.RequiredFieldControl.Enabled = false;
                }
            }
        }

        public string ActualEmployeeDivision
        {
            get { return lblDivisionName.Text; }
            set { lblDivisionName.Text = value; }
        }

        public DateTime? CoverageStartDate
        {
            get { return dpEmployeeCoverageFrom.Value; }
            set { dpEmployeeCoverageFrom.Value = value; }
        }

        public TimeSpan? CoverageStartTime
        {
            get
            {
                if (string.IsNullOrEmpty(txtCoverageTimeFrom.Text))
                    return null;
                else
                    return TimeSpan.Parse(txtCoverageTimeFrom.Text);
            }
            set
            {
                if (value.HasValue)
                    txtCoverageTimeFrom.Text = string.Format("{0}:{1}", value.Value.Hours, value.Value.Minutes);
            }
        }

        public TimeSpan? CoverageEndTime
        {
            get
            {
                if (string.IsNullOrEmpty(txtCoverageTimeTO.Text))
                    return null;
                else
                    return TimeSpan.Parse(txtCoverageTimeTO.Text);
            }
            set
            {
                if (value.HasValue)
                    txtCoverageTimeTO.Text = string.Format("{0}:{1}", value.Value.Hours, value.Value.Minutes);
            }
        }

        public DateTime? CoverageEndDate
        {
            get { return dpEmployeeCoverageTO.Value; }
            set { dpEmployeeCoverageTO.Value = value; }
        }

        public int? CoverageDuration
        {
            get
            {
                if (string.IsNullOrEmpty(txtCoverageDuration.Text))
                    return null;
                else
                    return Convert.ToInt32(txtCoverageDuration.Text);
            }
            set
            {
                if (value.HasValue)
                    txtCoverageDuration.Text = value.Value.ToString();
                else
                    txtCoverageDuration.Text = string.Empty;
            }
        }

        public int? CoverageDivisionID
        {
            get
            {
                if (actDivision.SelectedValue.Equals("0"))
                    return null;
                else
                    return Convert.ToInt32(actDivision.SelectedValue);
            }
            set
            {
                if (value.HasValue)
                    actDivision.SelectedValue = value.ToString();
                else
                    actDivision.SelectedValue = "0";
            }
        }

        public IList<CS_EmployeeCoverage> CoverageHistoryList
        {
            get { throw new NotImplementedException(); }
            set
            {
                gvCoverHistory.DataSource = value;
                gvCoverHistory.DataBind();
            }
        }

        #endregion

        #region [ Driving Info ]

        public string DriversLicenseNumber
        {
            get { return lblDriversLicenseNumberDesc.Text; }
            set { lblDriversLicenseNumberDesc.Text = value; }
        }

        public string DriversLicenseClass
        {
            get { return lblDriversLicenseClassDesc.Text; }
            set { lblDriversLicenseClassDesc.Text = value; }
        }

        public string DriversLicenseState
        {
            get { return lblDriversLicenseStateDesc.Text; }
            set { lblDriversLicenseStateDesc.Text = value; }
        }

        public DateTime? DriversLicenseExpireDate
        {
            get
            {
                if (string.IsNullOrEmpty(lblDriversLicenseExpireDateDesc.Text))
                    return null;
                else
                    return Convert.ToDateTime(lblDriversLicenseExpireDateDesc.Text, new System.Globalization.CultureInfo("en-US"));
            }
            set
            {
                if (value.HasValue)
                    lblDriversLicenseExpireDateDesc.Text = value.Value.ToString("MM/dd/yyyy");
                else
                    lblDriversLicenseExpireDateDesc.Text = string.Empty;
            }
        }

        #endregion

        #endregion

        #region [ Methods ]

        private void FillEmployeeFields()
        {
            EmployeeId = int.Parse(actEmployee.SelectedValue);

            if (EmployeeId != 0)
            {
                uscCallCriteria.ClearFields();
                uscCallCriteria.ClearCallTypes();

                uscCallCriteria.EmployeeID = EmployeeId;

                _presenter.LoadEmployeeInfo();

                uscCallCriteria.BindCallCriteriaListing();
                uscCallCriteria.CallCriteriaGroup(true);

                divControl.Disabled = false;
            }
            else
                divControl.Disabled = true;
        }

        private void KeepValidatorsState()
        {
            if (chkCoverage.Checked)
            {
                dpEmployeeCoverageFrom.IsValidEmpty = false;
                rfvCoverageTimeValidatorFrom.IsValidEmpty = false;
                actDivision.RequiredField = true;

                dpEmployeeCoverageTO.IsValidEmpty = true;
                rfvCoverageTimeValidatorTO.IsValidEmpty = true;
            }
            else if (hfIsCoverage.Value != string.Empty)
            {
                dpEmployeeCoverageFrom.IsValidEmpty = false;
                rfvCoverageTimeValidatorFrom.IsValidEmpty = false;
                actDivision.RequiredField = true;

                dpEmployeeCoverageTO.IsValidEmpty = false;
                rfvCoverageTimeValidatorTO.IsValidEmpty = false;
            }
            else
            {
                dpEmployeeCoverageFrom.IsValidEmpty = true;
                rfvCoverageTimeValidatorFrom.IsValidEmpty = true;
                actDivision.RequiredField = false;

                dpEmployeeCoverageTO.IsValidEmpty = true;
                rfvCoverageTimeValidatorTO.IsValidEmpty = true;
            }

            if (chkOffCall.Checked)
            {
                dpOffCallFrom.IsValidEmpty = false;
                dpOffCallTo.IsValidEmpty = false;
                rfvReturnTime.Enabled = true;
                actProxyEmployee.RequiredField = true;
            }
            else
            {
                dpOffCallFrom.IsValidEmpty = true;
                dpOffCallTo.IsValidEmpty = true;
                rfvReturnTime.Enabled = false;
                actProxyEmployee.RequiredField = false;
            }

            Page.Validate();
        }

        #endregion
    }
}
