using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.DataClasses;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Text;
using System.Web.UI.HtmlControls;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.CallCriteria
{
    public partial class CallCriteriaInfo : System.Web.UI.UserControl, ICallCriteriaInfoView
    {
        #region [ Attributes ]

        private CallCriteriaInfoPresenter _presenter;
        private RepeaterItem PrimaryCallTypeRepeaterItem { get; set; }
        List<int> _filterList = new List<int>();

        #endregion

        #region [ Overrides ]
        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new CallCriteriaInfoPresenter(this);
        }
        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsCallCriteriaEntityNull)
                {
                    _presenter.ListAllCountries();
                    _presenter.ListAllDivisions();
                    _presenter.ListAllInterimBilling();
                    _presenter.ListAllJobActions();
                    _presenter.ListAllJobCategories();
                    _presenter.ListAllJobStatus();
                    _presenter.ListAllJobType();
                    _presenter.ListAllPriceType();
                    _presenter.ListAllCallTypes();
                }

            }

            ManageLevelScripts();
        }

        protected void txtDivisionCustomerWideLevel_TextChanged(object sender, EventArgs e)
        {
            _presenter.LoadCallCriteria();
        }

        protected void rptPrimaryCallTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PrimaryCallTypeRepeaterItem = e.Item;
                _presenter.FillPrimaryCallTypeRow();
                FillPrimaryCallTypeCssClass();
                FillPrimaryCallTypeScript();
            }
        }

        protected void chkAddAll_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAddAll = this.PrimaryCallTypeRepeaterItem.FindControl("chkAddAll") as CheckBox;

            chkAddAll.Checked = false;

        }

        protected void chkCallTypes_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkCallTypes = this.PrimaryCallTypeRepeaterItem.FindControl("chkCallTypes") as CheckBox;

            chkCallTypes.Checked = false;

        }

        protected void rptCallCriteriaListing_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CallCriteriaRepeaterItem = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                _presenter.FillCallCriteriaListingRow();
        }

        protected void rptCallCriteriaListing_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            CallCriteriaRowCommand = e.CommandName;
            CallCriteiraRowCommandArgument = Convert.ToInt32(e.CommandArgument);
            _presenter.CallCriteriaListingRowCommand();
        }

        protected void rptJobAttributes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            JobAttributesRepeaterItem = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                _presenter.FillJobAttributesRow();
        }

        protected void rptJobCallLogConditions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            JobCallLogConditionsRepeaterItem = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                _presenter.FillJobCallLogConditionsRow();
        }

        protected void btnAddCallCriteria_Click(object sender, EventArgs e)
        {
            _presenter.AddCallCriteria();
            ManageLevelScripts();
            CallCriteriaEditable(true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.Cancel();
            ManageLevelScripts();
            CallCriteriaEditable(true);
        }
        
        #endregion

        #region [ Methods ]

        private void ManageLevelScripts()
        {

            rbDivision.Attributes.Add("onclick", string.Format("toggleAll(this.checked, '{0}');toggleAll(false, '{1}');document.getElementById('{2}').disabled = true;document.getElementById('{2}').value = '';ValidatorEnable($get('{3}'), false);", actDivisionLevel.BodyID, actCustomerLevel.BodyID, txtSystemWideLevel.ClientID, rfvSystemWide.ClientID));
            rbCustomer.Attributes.Add("onclick", string.Format("toggleAll(false, '{0}');toggleAll(this.checked, '{1}');document.getElementById('{2}').disabled = true;document.getElementById('{2}').value = '';ValidatorEnable($get('{3}'), false);", actDivisionLevel.BodyID, actCustomerLevel.BodyID, txtSystemWideLevel.ClientID, rfvSystemWide.ClientID));
            rbWide.Attributes.Add("onclick", string.Format("toggleAll(false, '{0}');toggleAll(false, '{1}');document.getElementById('{2}').disabled = false;ValidatorEnable($get('{3}'), true);", actDivisionLevel.BodyID, actCustomerLevel.BodyID, txtSystemWideLevel.ClientID, rfvSystemWide.ClientID));

            if (rbDivision.Checked)
            {
                actDivisionLevel.Enabled = true;
                actCustomerLevel.Enabled = false;
                txtSystemWideLevel.Enabled = false;
                actDivisionLevel.RequiredField = true;
                actCustomerLevel.RequiredField = false;
                rfvSystemWide.Enabled = false;
            }
            else if (rbCustomer.Checked)
            {
                actDivisionLevel.Enabled = false;
                actCustomerLevel.Enabled = true;
                txtSystemWideLevel.Enabled = false;
                actDivisionLevel.RequiredField = false;
                actCustomerLevel.RequiredField = true;
                rfvSystemWide.Enabled = false;
            }
            else if (rbWide.Checked)
            {
                actDivisionLevel.Enabled = false;
                actCustomerLevel.Enabled = false;
                txtSystemWideLevel.Enabled = true;
                actDivisionLevel.RequiredField = false;
                actCustomerLevel.RequiredField = false;
                rfvSystemWide.Enabled = true;
            }
            else
            {
                actDivisionLevel.Enabled = false;
                actCustomerLevel.Enabled = false;
                txtSystemWideLevel.Enabled = false;
                actDivisionLevel.RequiredField = false;
                actCustomerLevel.RequiredField = false;
                rfvSystemWide.Enabled = false;
                btnAddCallCriteria.Enabled = false;
                rbCustomer.Enabled = false;
                rbDivision.Enabled = false;
                rbWide.Enabled = false;

            }
        }

        public void ClearCallTypes()
        {
            _presenter.ListAllCallTypes();
        }

        private void FillPrimaryCallTypeCssClass()
        {
            HtmlGenericControl dvSelectAll = this.PrimaryCallTypeRepeaterItem.FindControl("dvSelectAll") as HtmlGenericControl;
            HtmlGenericControl dvColumn = this.PrimaryCallTypeRepeaterItem.FindControl("divColumn") as HtmlGenericControl;

            if (null != dvSelectAll && null != dvColumn)
            {
                dvColumn.Attributes["class"] = string.Format("{0} {1}", dvColumn.Attributes["Class"], PrimaryCallTypeRepeaterDataItem.ID);
                dvSelectAll.Attributes["class"] = "selectAllCallTypes";
            }

        }

        private void FillPrimaryCallTypeScript()
        {
            CheckBox chkAddAll = this.PrimaryCallTypeRepeaterItem.FindControl("chkAddAll") as CheckBox;

            if (null != chkAddAll)
            {
                chkAddAll.Attributes["onclick"] = string.Format("toggleSingleCallType('{0}','{1}');", PrimaryCallTypeRepeaterDataItem.ID, chkAddAll.ClientID);
            }
        }

        public void ClearFields()
        {
            actDivisionLevel.ClearSelection();
            chkDivision.Checked = false;
            ddlDivision.Items.Clear();
            actCustomer.ClearSelection();
            chkCustomer.Checked = false;
            actCustomerLevel.ClearSelection();
            txtSystemWideLevel.Text = string.Empty;
            chkJobStatus.Checked = false;
            chkPriceType.Checked = false;
            ddlPriceType.Items.Clear();
            ddlJobStatus.ClearSelection();
            chkJobCategory.Checked = false;
            ddlJobCategory.ClearSelection();
            chkJobType.Checked = false;
            ddlJobType.ClearSelection();
            chkJobAction.Checked = false;
            ddlJobAction.ClearSelection();
            chkInterimbilling.Checked = false;
            ddlInterimbilling.ClearSelection();
            chkGeneralLog.Checked = false;
            chkCountry.Checked = false;
            ddlCountry.ClearSelection();
            chkState.Checked = false;
            acState.ClearSelection();
            chkCity.Checked = false;
            acCity.ClearSelection();
            lstBoxSelectedCity.ClearSelection();
            chkCarCount.Checked = false;
            txtCarCount.Text = string.Empty;
            chkCommodities.Checked = false;
            ddlCommodities.ClearSelection();
            chkChemicals.Checked = false;
            ddlChemicals.ClearSelection();
            chkWhiteLight.Checked = false;
            _presenter.ListAllCountries();
            _presenter.ListAllDivisions();
            _presenter.ListAllInterimBilling();
            _presenter.ListAllJobActions();
            _presenter.ListAllJobCategories();
            _presenter.ListAllJobStatus();
            _presenter.ListAllJobType();
            _presenter.ListAllPriceType();
            lstSelectedCustomers.Items.Clear();
            lstBoxSelectedState.Items.Clear();
            lstBoxSelectedCity.Items.Clear();
            txtCarCount.Text = string.Empty;
            chkHeavyEquipment.Checked = false;
            chkNonHeavyEquipment.Checked = false;
            chkAllEquipment.Checked = false;
            chkAllJobs.Checked = false;
            chkAllCallTypes.Checked = false;
            txtNotes.Text = string.Empty;
            ClearCallTypes();
            hfCustomerValues.Value = string.Empty;
            hfCityValues.Value = string.Empty;
            hfStateValues.Value = string.Empty;
            rbDivision.Checked = false;
            rbCustomer.Checked = false;
            rbWide.Checked = false;
        
            rblAllEquipment.SelectedValue = "0";
            rblCarCount.SelectedValue = "0";
            rblChemicals.SelectedValue = "0";
            rblCity.SelectedValue = "0";
            rblCommodities.SelectedValue = "0";
            rblCountry.SelectedValue = "0";
            rblCustomer.SelectedValue = "0";
            rblDivision.SelectedValue = "0";
            rblGeneralLog.SelectedValue = "0";
            rblHeavyEquipment.SelectedValue = "0";
            rblInterimBilling.SelectedValue = "0";
            rblJobAction.SelectedValue = "0";
            rblJobCategory.SelectedValue = "0";
            rblJobStatus.SelectedValue = "0";
            rblJobType.SelectedValue = "0";
            rblNonHeavyEquipment.SelectedValue = "0";
            rblPriceType.SelectedValue = "0";
            rblState.SelectedValue = "0";
            rblWhiteLight.SelectedValue = "0";
        }

        public void BindCallCriteriaListing()
        {
            _presenter.BindCallcriteriaListing();
        }

        #endregion

        #region [ View Interface Implementation ]

        /// <summary>
        /// Set the call criteria radion button ( division, customer, wide level ) enable true/false
        /// </summary>
        /// <param name="value">bool</param>
        public void CallCriteriaEditable(bool value)
        {
            rbWide.Enabled = value;
            rbDivision.Enabled = value;
            rbCustomer.Enabled = value;
        }
        public bool ShowHidePanelNowRowsCcListing
        {
            set
            {
                pnlNoRowsCallCriteriaListing.Visible = value;
                pnlRowsCallCriteriaListing.Visible = !value;
            }
        }

        public void CallCriteriaGroup(bool value)
        {
            pnlGroupCriteria.Enabled = value;
        }

        public bool AddCallCriteriaEnabled
        {
            set
            {
                btnAddCallCriteria.Enabled = value;
            }
        }

        public Globals.CallCriteria.CallCriteriaLevel SelectedLevel
        {
            get
            {
                if (rbDivision.Checked)
                    return Globals.CallCriteria.CallCriteriaLevel.Division;
                else if (rbCustomer.Checked)
                    return Globals.CallCriteria.CallCriteriaLevel.Customer;
                else
                    return Globals.CallCriteria.CallCriteriaLevel.Wide;
            }
            set
            {
                if (value == Globals.CallCriteria.CallCriteriaLevel.Division)
                    rbDivision.Checked = true;
                if (value == Globals.CallCriteria.CallCriteriaLevel.Customer)
                    rbCustomer.Checked = true;
                else
                    rbWide.Checked = true;
            }
        }

        public string Notes
        {
            get
            {
                return txtNotes.Text;
            }
        }

        public int? EditingDivisionID
        {
            get
            {
                return int.Parse(actDivisionLevel.SelectedValue);
            }
            set
            {
                actDivisionLevel.SetValue = value.HasValue ? value.ToString() : "0";
            }
        }

        public int? EditingCustomerID
        {
            get
            {
                return int.Parse(actCustomerLevel.SelectedValue);
            }
            set
            {
                actCustomerLevel.SetValue = value.HasValue ? value.ToString() : "0";
            }
        }

        public string EditingWideName
        {
            get
            {
                return txtSystemWideLevel.Text;
            }
            set
            {
                txtSystemWideLevel.Text = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }

        private bool IsCallCriteriaEntityNull
        {
            get
            {
                return (null == ViewState["callCriteria"]);
            }
        }

        public CS_CallCriteria CallCriteriaEntity
        {
            get
            {
                CS_CallCriteria callCriteria = new CS_CallCriteria();
                DateTime actionDate = DateTime.Now;

                if (null != ViewState["callCriteria"])
                {
                    CS_CallCriteria oldCallCriteria = (CS_CallCriteria)ViewState["callCriteria"];
                    callCriteria.ID = oldCallCriteria.ID;
                    callCriteria.EmployeeID = oldCallCriteria.EmployeeID;
                    callCriteria.ContactID = oldCallCriteria.ContactID;
                    callCriteria.ModifiedBy = Username;
                    callCriteria.CreatedBy = oldCallCriteria.CreatedBy;
                    callCriteria.CreationID = oldCallCriteria.CreationID;
                    callCriteria.ModificationID = null;
                    callCriteria.CreationDate = oldCallCriteria.CreationDate;
                    callCriteria.ModificationDate = actionDate;
                    callCriteria.Active = oldCallCriteria.Active;
                    callCriteria.DivisionID = oldCallCriteria.DivisionID.HasValue ? oldCallCriteria.DivisionID : null;
                    callCriteria.CustomerID = oldCallCriteria.CustomerID.HasValue ? oldCallCriteria.CustomerID : null;
                    callCriteria.SystemWideLevel = !string.IsNullOrEmpty(oldCallCriteria.SystemWideLevel) ? oldCallCriteria.SystemWideLevel : string.Empty;
                    callCriteria.Notes = txtNotes.Text;
                }
                else
                {
                    callCriteria.EmployeeID = EmployeeID;
                    callCriteria.ContactID = ContactID;
                    callCriteria.ModifiedBy = Username;
                    callCriteria.CreatedBy = Username;
                    callCriteria.CreationID = null;
                    callCriteria.ModificationID = null;
                    callCriteria.CreationDate = actionDate;
                    callCriteria.ModificationDate = actionDate;
                    callCriteria.Active = true;
                    callCriteria.DivisionID = rbDivision.Checked ? EditingDivisionID : null;
                    callCriteria.CustomerID = rbCustomer.Checked ? EditingCustomerID : null;
                    callCriteria.SystemWideLevel = rbWide.Checked ? EditingWideName : string.Empty;
                    callCriteria.Notes = txtNotes.Text;
                }

                return callCriteria;
            }
            set
            {
                if (value == null)
                    ClearFields();
                else
                {
                    ViewState["callCriteria"] = value;
                    txtNotes.Text = value.Notes;

                    actDivisionLevel.SetValue = value.DivisionID.HasValue ? value.DivisionID.ToString() : "0";
                    actDivisionLevel.SelectedText = value.DivisionID.HasValue ? value.CS_Division.Name : "";
                    rbDivision.Checked = value.DivisionID.HasValue;

                    actCustomerLevel.SetValue = value.CustomerID.HasValue ? value.CustomerID.ToString() : "0";
                    actCustomerLevel.SelectedText = value.CustomerID.HasValue ? value.CS_Customer.FullCustomerInformation : "";
                    rbCustomer.Checked = value.CustomerID.HasValue;

                    txtSystemWideLevel.Text = string.IsNullOrEmpty(value.SystemWideLevel) ? string.Empty : value.SystemWideLevel;
                    rbWide.Checked = !string.IsNullOrEmpty(value.SystemWideLevel);
                }
            }
        }

        public IList<CS_CallCriteriaValue> CallCriteriaValueEntityList
        {
            get
            {
                IList<CS_CallCriteriaValue> callCriteriaValueList = new List<CS_CallCriteriaValue>();

                //Customer Criterias
                string[] custumerIds = hfCustomerValues.Value.Split(';');
                for (int i = 0; i < custumerIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(custumerIds[i].Trim()))
                    {
                        callCriteriaValueList.Add(
                        new CS_CallCriteriaValue()
                        {
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                            Value = custumerIds[i],
                            Active = true,
                            IsAnd = rblCustomer.SelectedValue == "1" ? true : false
                        });
                    }
                }

                //Division Criterias
                for (int i = 0; i < ddlDivision.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                        Value = ddlDivision.SelectedValues[i],
                        Active = true,
                        IsAnd = rblDivision.SelectedValue == "1" ? true : false
                    });
                }

                //Job Status Criterias
                for (int i = 0; i < ddlJobStatus.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                        Value = ddlJobStatus.SelectedValues[i],
                        Active = true,
                        IsAnd = rblJobStatus.SelectedValue == "1" ? true : false
                    });
                }

                //Price Type Criterias
                for (int i = 0; i < ddlPriceType.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                        Value = ddlPriceType.SelectedValues[i],
                        Active = true,
                        IsAnd = rblPriceType.SelectedValue == "1" ? true : false
                    });
                }

                //Job Category Criterias
                for (int i = 0; i < ddlJobCategory.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                        Value = ddlJobCategory.SelectedValues[i],
                        Active = true,
                        IsAnd = rblJobCategory.SelectedValue == "1" ? true : false
                    });
                }

                //Job Type Criterias
                for (int i = 0; i < ddlJobType.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                        Value = ddlJobType.SelectedValues[i],
                        Active = true,
                        IsAnd = rblJobType.SelectedValue == "1" ? true : false
                    });
                }

                //Job Action Criterias
                for (int i = 0; i < ddlJobAction.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobAction,
                        Value = ddlJobAction.SelectedValues[i],
                        Active = true,
                        IsAnd = rblJobAction.SelectedValue == "1" ? true : false
                    });
                }

                //Interim Billing Criterias
                for (int i = 0; i < ddlInterimbilling.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Interimbilling,
                        Value = ddlInterimbilling.SelectedValues[i],
                        Active = true,
                        IsAnd = rblInterimBilling.SelectedValue == "1" ? true : false
                    });
                }

                //General Log Criterias
                if (chkGeneralLog.Checked)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.GeneralLog,
                        Value = chkGeneralLog.Checked.ToString(),
                        Active = true,
                        IsAnd = rblGeneralLog.SelectedValue == "1" ? true : false
                    });
                }

                //Country Criterias
                for (int i = 0; i < ddlCountry.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Country,
                        Value = ddlCountry.SelectedValues[i],
                        Active = true,
                        IsAnd = rblCountry.SelectedValue == "1" ? true : false
                    });
                }

                //State Criterias
                string[] statesIds = hfStateValues.Value.Split(';');
                for (int i = 0; i < statesIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(statesIds[i].Trim()))
                    {
                        callCriteriaValueList.Add(
                        new CS_CallCriteriaValue()
                        {
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.State,
                            Value = statesIds[i],
                            Active = true,
                            IsAnd = rblState.SelectedValue == "1" ? true : false
                        });
                    }
                }

                //City Criterias
                string[] citiesIds = hfCityValues.Value.Split(';');
                for (int i = 0; i < citiesIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(citiesIds[i].Trim()))
                    {
                        callCriteriaValueList.Add(
                        new CS_CallCriteriaValue()
                        {
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.City,
                            Value = citiesIds[i],
                            Active = true,
                            IsAnd = rblCity.SelectedValue == "1" ? true : false
                        });
                    }
                }

                //Car Count Criterias
                if (!string.IsNullOrEmpty(txtCarCount.Text.Trim()))
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.CarCount,
                        Value = (ddlCarCount.SelectedValue + txtCarCount.Text.Trim()),
                        Active = true,
                        IsAnd = rblCarCount.SelectedValue == "1" ? true : false
                    });
                }

                //Commodities Criterias
                for (int i = 0; i < ddlCommodities.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Commodities,
                        Value = ddlCommodities.SelectedValues[i],
                        Active = true,
                        IsAnd = rblCommodities.SelectedValue == "1" ? true : false
                    });
                }

                //Chemicals Criterias
                for (int i = 0; i < ddlChemicals.SelectedValues.Count; i++)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Chemicals,
                        Value = ddlChemicals.SelectedValues[i],
                        Active = true,
                        IsAnd = rblChemicals.SelectedValue == "1" ? true : false
                    });
                }

                //Heavy Equipment Criterias
                if (chkHeavyEquipment.Checked)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment,
                        Value = chkHeavyEquipment.Checked.ToString(),
                        Active = true,
                        IsAnd = rblHeavyEquipment.SelectedValue == "1" ? true : false
                    });
                }

                //Non-Heavy Equipment Criterias
                if (chkNonHeavyEquipment.Checked)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment,
                        Value = chkNonHeavyEquipment.Checked.ToString(),
                        Active = true,
                        IsAnd = rblNonHeavyEquipment.SelectedValue == "1" ? true : false
                    });
                }

                //All Equipment Criterias
                if (chkAllEquipment.Checked)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.AllEquipment,
                        Value = chkAllEquipment.Checked.ToString(),
                        Active = true,
                        IsAnd = rblAllEquipment.SelectedValue == "1" ? true : false
                    });
                }

                //White Light Criteria
                if (chkWhiteLight.Checked)
                {
                    callCriteriaValueList.Add(
                    new CS_CallCriteriaValue()
                    {
                        CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.WhiteLight,
                        Value = chkWhiteLight.Checked.ToString(),
                        Active = true,
                        IsAnd = rblWhiteLight.SelectedValue == "1" ? true : false
                    });
                }

                for (int i = 0; i < rptPrimaryCallTypes.Items.Count; i++)
                {
                    CheckBoxList callTypes = rptPrimaryCallTypes.Items[i].FindControl("chkCallTypes") as CheckBoxList;

                    for (int j = 0; j < callTypes.Items.Count; j++)
                    {
                        if (callTypes.Items[j].Selected)
                        {
                            callCriteriaValueList.Add(
                            new CS_CallCriteriaValue()
                            {
                                CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.CallType,
                                Value = callTypes.Items[j].Value,
                                Active = true
                            });
                        }
                    }
                }

                return callCriteriaValueList;
            }
            set
            {
                StringBuilder script = new StringBuilder();
                IList<CS_CallCriteriaValue> callCriteriaValueList = value;

                //Customer Criterias
                List<CS_CallCriteriaValue> valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer).ToList();
                chkCustomer.Checked = valueList.Count > 0;

                if (chkCustomer.Checked)
                    rblCustomer.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();


                _filterList = new List<int>();

                for (int i = 0; i < valueList.Count; i++)
                {
                    _filterList.Add(int.Parse(valueList[i].Value));
                    hfCustomerValues.Value += string.Format(";{0}", valueList[i].Value);
                }

                if (chkCustomer.Checked)
                    script.AppendFormat("toogleField('{0}', \"customerFields\");", chkCustomer.ClientID);

                _presenter.ListCustomers();

                //Division Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division).ToList();
                chkDivision.Checked = valueList.Count > 0;

                if (chkDivision.Checked)
                {
                    rblDivision.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlDivision\");", chkDivision.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlDivision.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Job Status Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobStatus).ToList();
                chkJobStatus.Checked = valueList.Count > 0;

                if (chkJobStatus.Checked)
                {
                    rblJobStatus.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlJobStatus\");", chkJobStatus.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlJobStatus.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Price Type Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.PriceType).ToList();
                chkPriceType.Checked = valueList.Count > 0;

                if (chkPriceType.Checked)
                {
                    rblPriceType.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlPriceType\");", chkPriceType.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlPriceType.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Job Category Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobCategory).ToList();
                chkJobCategory.Checked = valueList.Count > 0;

                if (chkJobCategory.Checked)
                {
                    rblJobCategory.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlJobCategory\");", chkJobCategory.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlJobCategory.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Job Type Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobType).ToList();
                chkJobType.Checked = valueList.Count > 0;

                if (chkJobType.Checked)
                {
                    rblJobType.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlJobType\");", chkJobType.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlJobType.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Job Action Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobAction).ToList();
                chkJobAction.Checked = valueList.Count > 0;

                if (chkJobAction.Checked)
                {
                    rblJobAction.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlJobAction\");", chkJobAction.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlJobAction.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Interim Billing Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Interimbilling).ToList();
                chkInterimbilling.Checked = valueList.Count > 0;

                if (chkInterimbilling.Checked)
                {
                    rblInterimBilling.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlInterimbilling\");", chkInterimbilling.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlInterimbilling.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //General Log Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.GeneralLog).ToList();
                chkGeneralLog.Checked = (valueList.Count > 0);

                if (chkGeneralLog.Checked)
                {
                    rblGeneralLog.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"\");", chkGeneralLog.ClientID);
                }

                //Country Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Country).ToList();
                chkCountry.Checked = valueList.Count > 0;

                if (chkCountry.Checked)
                {
                    rblCountry.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlCountry\");", chkCountry.ClientID);
                }

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlCountry.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //State Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.State).ToList();
                chkState.Checked = valueList.Count > 0;

                if (chkState.Checked)
                    rblState.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();

                _filterList = new List<int>();

                for (int i = 0; i < valueList.Count; i++)
                {
                    _filterList.Add(int.Parse(valueList[i].Value));
                    hfStateValues.Value += string.Format(";{0}", valueList[i].Value);
                }

                if (chkState.Checked)
                    script.AppendFormat("toogleField('{0}', \"stateFields\");", chkState.ClientID);

                _presenter.ListStates();

                //City Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.City).ToList();
                chkCity.Checked = valueList.Count > 0;

                if (chkCity.Checked)
                    rblCity.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();

                _filterList = new List<int>();

                for (int i = 0; i < valueList.Count; i++)
                {
                    _filterList.Add(int.Parse(valueList[i].Value));
                    hfCityValues.Value += string.Format(";{0}", valueList[i].Value);
                }

                if (chkCity.Checked)
                    script.AppendFormat("toogleField('{0}', \"cityFields\");", chkCity.ClientID);

                _presenter.ListCities();

                //Car Count Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CarCount).ToList();
                chkCarCount.Checked = valueList.Count > 0;

                if (chkCarCount.Checked)
                {
                    rblCarCount.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"txtCarCount\");", chkCarCount.ClientID);
                }
                else
                    ddlCarCount.SelectedValue = string.Empty;

                if (valueList.Count > 0)
                {
                    string carCount = valueList[0].Value;

                    if (carCount.Substring(0, 1) == "<" || carCount.Substring(0, 1) == ">")
                    {
                        txtCarCount.Text = carCount.Substring(1);
                        ddlCarCount.SelectedValue = carCount.Substring(0, 1);
                    }
                    else
                    {
                        txtCarCount.Text = carCount;
                        ddlCarCount.SelectedValue = string.Empty;
                    }
                }

                //Commodities Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Commodities).ToList();
                chkCommodities.Checked = valueList.Count > 0;

                if (chkCommodities.Checked)
                {
                    rblCommodities.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlCommodities\");", chkCommodities.ClientID);
                }
                else
                    ddlCommodities.ClearSelection();

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlCommodities.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Chemicals Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Chemicals).ToList();
                chkChemicals.Checked = valueList.Count > 0;

                if (chkChemicals.Checked)
                {
                    rblChemicals.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"ddlChemicals\");", chkChemicals.ClientID);
                }
                else
                    ddlChemicals.ClearSelection();

                for (int i = 0; i < valueList.Count; i++)
                {
                    ListItem item = ddlChemicals.Items.FindByValue(valueList[i].Value);

                    if (null != item)
                        item.Selected = true;
                }

                //Heavy Equipment Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment).ToList();
                chkHeavyEquipment.Checked = (valueList.Count > 0);

                if (chkHeavyEquipment.Checked)
                {
                    rblHeavyEquipment.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"\");", chkHeavyEquipment.ClientID);
                }

                //Non-Heavy Equipment Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment).ToList();
                chkNonHeavyEquipment.Checked = (valueList.Count > 0);

                if (chkNonHeavyEquipment.Checked)
                {
                    rblNonHeavyEquipment.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"\");", chkNonHeavyEquipment.ClientID);
                }

                //All Equipment Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.AllEquipment).ToList();
                chkAllEquipment.Checked = (valueList.Count > 0);

                if (chkAllEquipment.Checked)
                {
                    rblAllEquipment.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"\");", chkAllEquipment.ClientID);
                }

                //White Light Criterias
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.WhiteLight).ToList();
                chkWhiteLight.Checked = (valueList.Count > 0);

                if (chkWhiteLight.Checked)
                {
                    rblWhiteLight.SelectedValue = Convert.ToInt32(valueList.FirstOrDefault().IsAnd).ToString();
                    script.AppendFormat("toogleField('{0}', \"\");", chkWhiteLight.ClientID);
                }

                //CallTypes
                valueList = callCriteriaValueList.Where(e => e.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CallType).ToList();

                for (int j = 0; j < rptPrimaryCallTypes.Items.Count; j++)
                {
                    CheckBoxList callTypes = rptPrimaryCallTypes.Items[j].FindControl("chkCallTypes") as CheckBoxList;

                    for (int i = 0; i < valueList.Count; i++)
                    {
                        ListItem item = callTypes.Items.FindByValue(valueList[i].Value);
                        if (null != item)
                            item.Selected = true;
                    }
                }

                script.AppendLine("CreateGrid();");

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                if (null == scriptManager)
                {
                    ClientScriptManager cs = this.Page.ClientScript;
                    cs.RegisterStartupScript(GetType(), string.Format("toogleFields_{0}", ID), script.ToString(), true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), string.Format("toogleFields_{0}", ID), script.ToString(), true);
                }
            }
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((ContentPage)this.Page.Master).DisplayMessage(message, closeWindow);
        }

        public IList<CS_Division> DivisionList
        {

            set
            {
                ddlDivision.Items.Clear();
                ddlDivision.DataSource = value;
                ddlDivision.DataTextField = "ExtendedDivisionName";
                ddlDivision.DataValueField = "ID";
                ddlDivision.DataBind();
            }
        }

        public IList<CS_JobStatus> JobSatusList
        {

            set
            {
                ddlJobStatus.Items.Clear();
                ddlJobStatus.DataSource = value;
                ddlJobStatus.DataTextField = "Description";
                ddlJobStatus.DataValueField = "ID";
                ddlJobStatus.DataBind();
            }
        }

        public IList<CS_PriceType> PriceTypeList
        {
            set
            {
                ddlPriceType.Items.Clear();
                ddlPriceType.DataSource = value;
                ddlPriceType.DataTextField = "Description";
                ddlPriceType.DataValueField = "ID";
                ddlPriceType.DataBind();
            }
        }

        public IList<CS_JobCategory> JobCategoryList
        {

            set
            {
                ddlJobCategory.Items.Clear();
                ddlJobCategory.DataSource = value;
                ddlJobCategory.DataTextField = "Description";
                ddlJobCategory.DataValueField = "ID";
                ddlJobCategory.DataBind();
            }
        }

        public IList<CS_JobType> JobTypeList
        {

            set
            {
                ddlJobType.Items.Clear();
                ddlJobType.DataSource = value;
                ddlJobType.DataTextField = "Description";
                ddlJobType.DataValueField = "ID";
                ddlJobType.DataBind();
            }
        }

        public IList<CS_JobAction> JobActionList
        {

            set
            {
                ddlJobAction.Items.Clear();
                ddlJobAction.DataSource = value;
                ddlJobAction.DataTextField = "Description";
                ddlJobAction.DataValueField = "ID";
                ddlJobAction.DataBind();
            }
        }

        public IList<CS_Frequency> FrequencyList
        {

            set
            {
                ddlInterimbilling.Items.Clear();
                ddlInterimbilling.DataSource = value;
                ddlInterimbilling.DataTextField = "Description";
                ddlInterimbilling.DataValueField = "ID";
                ddlInterimbilling.DataBind();
            }
        }

        public IList<CS_Country> CountryList
        {

            set
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = value;
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
        }

        public int CountryValue
        {
            get
            {
                return int.Parse(ddlCountry.SelectedValue);
            }
            set
            {
                ddlCountry.SelectedValue = value.ToString();
            }
        }

        public IList<CS_PrimaryCallType> PrimaryCallTypeList
        {
            set
            {
                rptPrimaryCallTypes.DataSource = value;
                rptPrimaryCallTypes.DataBind();

                if (rptPrimaryCallTypes.Items.Count > 0)
                {
                    HtmlGenericControl divColumn = rptPrimaryCallTypes.Items[rptPrimaryCallTypes.Items.Count - 1].FindControl("divColumn") as HtmlGenericControl;
                    if (null != divColumn)
                        divColumn.Attributes["class"] = divColumn.Attributes["class"].Replace("column20CallTypes", "column18CallTypes");
                }
            }
        }

        public CS_PrimaryCallType PrimaryCallTypeRepeaterDataItem
        {
            get { return PrimaryCallTypeRepeaterItem.DataItem as CS_PrimaryCallType; }
            set { throw new NotImplementedException(); }
        }

        public string PrimaryCallTypeRepeaterRowDescription
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblPrimaryCallType = PrimaryCallTypeRepeaterItem.FindControl("lblPrimaryCallType") as Label;
                if (null != lblPrimaryCallType) lblPrimaryCallType.Text = value;

                CheckBox chkAddAll = PrimaryCallTypeRepeaterItem.FindControl("chkAddAll") as CheckBox;
                if (null != chkAddAll) chkAddAll.Text = string.Format("ALL {0}", value);
            }
        }

        public IList<CS_PrimaryCallType_CallType> PrimaryCallTypeRepeaterRowCallTypeList
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBoxList chkCallTypes = PrimaryCallTypeRepeaterItem.FindControl("chkCallTypes") as CheckBoxList;
                if (null != chkCallTypes)
                {
                    chkCallTypes.Items.Clear();
                    foreach (CS_PrimaryCallType_CallType callType in value)
                        chkCallTypes.Items.Add(new ListItem(callType.CS_CallType.Description, callType.CallTypeID.ToString()));
                }
            }
        }

        public List<int> FilterList
        {
            get
            {
                return _filterList;
            }
        }

        public IList<CS_Customer> CustomerList
        {
            set
            {
                lstSelectedCustomers.DataTextField = "FullCustomerInformation";
                lstSelectedCustomers.DataValueField = "ID";
                lstSelectedCustomers.DataSource = value;
                lstSelectedCustomers.DataBind();
            }
        }

        public IList<CS_City> CityList
        {
            set
            {
                lstBoxSelectedCity.DataTextField = "CityStateInformation";
                lstBoxSelectedCity.DataValueField = "ID";
                lstBoxSelectedCity.DataSource = value;
                lstBoxSelectedCity.DataBind();
            }
        }

        public IList<CS_State> StateList
        {
            set
            {
                lstBoxSelectedState.DataTextField = "AcronymName";
                lstBoxSelectedState.DataValueField = "ID";
                lstBoxSelectedState.DataSource = value;
                lstBoxSelectedState.DataBind();
            }
        }

        public bool PanelCallCriteria
        {
            set
            {
                pnlCallCriteria.Visible = value;
            }
        }

        public bool PanelCustomer
        {
            set
            {
                pnlCustomer.Visible = value;
            }
        }

        public bool PanelDivision
        {
            set
            {
                pnlDivision.Visible = value;
            }
        }


        #region [ Call Criteria Listing ]

        public string Username
        {
            get { return ((ContentPage)Page.Master).Username; }
        }

        public IList<CS_CallCriteria> CallCriteriaList
        {
            get
            {
                return ViewState["callCriteriaList"] as IList<CS_CallCriteria>;
            }
            set
            {
                ViewState["callCriteriaList"] = value;
            }
        }

        #region [ Data Sources ]

        public IList<CS_CallCriteria> CallCriteriaRepeaterDataSource
        {
            set
            {
                rptCallCriteriaListing.DataSource = value;
                rptCallCriteriaListing.DataBind();
            }
        }

        public IList<CallCriteriaItemVO> JobAttributesRepeaterDataSource
        {
            set
            {
                Repeater rptJobAttributes = CallCriteriaRepeaterItem.FindControl("rptJobAttributes") as Repeater;
                if (null != rptJobAttributes)
                {
                    rptJobAttributes.DataSource = value;
                    rptJobAttributes.DataBind();
                }

                HtmlGenericControl divExpandJobAttributes = CallCriteriaRepeaterItem.FindControl("divExpandJobAttributes") as HtmlGenericControl;
                if (null != divExpandJobAttributes)
                {
                    if (value != null && value.Count > 0)
                    {
                        divExpandJobAttributes.Attributes.Add("onclick", "CollapseExpandJobAttributes('" + divExpandJobAttributes.ClientID + "','CC" + CallCriteriaRowId.ToString() + "');");
                        divExpandJobAttributes.Visible = true;
                    }
                    else
                        divExpandJobAttributes.Visible = false;
                }
            }
        }

        public IList<CallCriteriaItemVO> JobCallLogConditionsRepeaterDataSource
        {
            set
            {
                Repeater rptJobCallLogConditions = CallCriteriaRepeaterItem.FindControl("rptJobCallLogConditions") as Repeater;
                if (null != rptJobCallLogConditions)
                {
                    rptJobCallLogConditions.DataSource = value;
                    rptJobCallLogConditions.DataBind();
                }

                HtmlGenericControl divExpandJobCallLogConditions = CallCriteriaRepeaterItem.FindControl("divExpandJobCallLogConditions") as HtmlGenericControl;
                if (null != divExpandJobCallLogConditions)
                {
                    if (value != null && value.Count > 0)
                    {
                        divExpandJobCallLogConditions.Attributes.Add("onclick", "CollapseExpandCallLogConditions('" + divExpandJobCallLogConditions.ClientID + "','CC" + CallCriteriaRowId.ToString() + "');");
                        divExpandJobCallLogConditions.Visible = true;
                    }
                    else
                        divExpandJobCallLogConditions.Visible = false;
                }
            }
        }


        #endregion

        #region [ Data Items ]

        public CS_CallCriteria CallCriteriaRepeaterDataItem
        {
            get { return CallCriteriaRepeaterItem.DataItem as CS_CallCriteria; }
            set { throw new NotImplementedException(); }
        }

        public CallCriteriaItemVO JobAttributesRepeaterDataItem
        {
            get { return JobAttributesRepeaterItem.DataItem as CallCriteriaItemVO; }
            set { throw new NotImplementedException(); }
        }

        public CallCriteriaItemVO JobCallLogConditionsRepeaterDataItem
        {
            get { return JobCallLogConditionsRepeaterItem.DataItem as CallCriteriaItemVO; }
            set { throw new NotImplementedException(); }
        }

        private RepeaterItem CallCriteriaRepeaterItem { get; set; }

        private RepeaterItem JobAttributesRepeaterItem { get; set; }

        private RepeaterItem JobCallLogConditionsRepeaterItem { get; set; }

        #endregion

        #region [ Row Attributes - Call Criteria ]

        public int CallCriteriaRowId
        {
            get
            {
                HiddenField hfCriteriaID = CallCriteriaRepeaterItem.FindControl("hfCriteriaID") as HiddenField;
                if (null != hfCriteriaID)
                    return Convert.ToInt32(hfCriteriaID.Value);
                else
                    return 0;
            }
            set
            {
                HiddenField hfCriteriaID = CallCriteriaRepeaterItem.FindControl("hfCriteriaID") as HiddenField;
                if (null != hfCriteriaID) hfCriteriaID.Value = value.ToString();

                Button btnEdit = CallCriteriaRepeaterItem.FindControl("btnEdit") as Button;
                if (null != btnEdit) btnEdit.CommandArgument = value.ToString();

                Button btnRemove = CallCriteriaRepeaterItem.FindControl("btnRemove") as Button;
                if (null != btnRemove) btnRemove.CommandArgument = value.ToString();

                HtmlTableRow trJobAttributesHeader = CallCriteriaRepeaterItem.FindControl("trJobAttributesHeader") as HtmlTableRow;
                if (null != trJobAttributesHeader)
                {
                    trJobAttributesHeader.Attributes["class"] = "odd Attributes CC" + value.ToString();
                    trJobAttributesHeader.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedCallCriterias.Value.Contains("CC" + value.ToString() + ";"))
                        trJobAttributesHeader.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trJobAttributesHeader.Style.Add(HtmlTextWriterStyle.Display, "");
                }
                HtmlTableRow trJobCallLogConditionsHeader = CallCriteriaRepeaterItem.FindControl("trJobCallLogConditionsHeader") as HtmlTableRow;
                if (null != trJobCallLogConditionsHeader)
                {
                    trJobCallLogConditionsHeader.Attributes["class"] = "odd CallLogs CC" + value.ToString();
                    trJobCallLogConditionsHeader.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedCallCriterias.Value.Contains("CC" + value.ToString() + ";"))
                        trJobCallLogConditionsHeader.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trJobCallLogConditionsHeader.Style.Add(HtmlTextWriterStyle.Display, "");
                }
                HtmlTableRow trAdvisement = CallCriteriaRepeaterItem.FindControl("trAdvisement") as HtmlTableRow;
                if (null != trAdvisement)
                {
                    trAdvisement.Attributes["class"] = "even Advisement CC" + value.ToString();
                    trAdvisement.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedCallCriterias.Value.Contains("CC" + value.ToString() + ";"))
                        trAdvisement.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trAdvisement.Style.Add(HtmlTextWriterStyle.Display, "");
                }

                HtmlGenericControl divExpand = CallCriteriaRepeaterItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    divExpand.Attributes.Add("onclick", "CollapseExpandCallCriteria('" + divExpand.ClientID + "','CC" + value.ToString() + "');");
                    divExpand.Visible = true;
                }
            }
        }

        public string CallCriteriaRowDescription
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblSelectedCriteria = CallCriteriaRepeaterItem.FindControl("lblSelectedCriteria") as Label;
                if (null != lblSelectedCriteria) lblSelectedCriteria.Text = value;
            }
        }

        public string CallCriteriaRowAdviseNotes
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblNote = CallCriteriaRepeaterItem.FindControl("lblNote") as Label;
                if (null != lblNote) lblNote.Text = value;
            }
        }

        public string CallCriteriaRowCommand { get; set; }

        public int CallCriteiraRowCommandArgument { get; set; }

        #endregion

        #region [ Row Attributes - Job Attributes ]

        public string JobAttributesRowCriteira
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCriteria = JobAttributesRepeaterItem.FindControl("lblCriteria") as Label;
                if (null != lblCriteria) lblCriteria.Text = value;

                HtmlTableRow trJobAttributes = JobAttributesRepeaterItem.FindControl("trJobAttributes") as HtmlTableRow;
                if (null != trJobAttributes)
                {
                    trJobAttributes.Attributes["class"] = "even AttributesItems CC" + CallCriteriaRowId.ToString();
                    trJobAttributes.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedJobAttributes.Value.Contains("CC" + value.ToString() + ";"))
                        trJobAttributes.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trJobAttributes.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public string JobAttributesRowSelected
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblSelected = JobAttributesRepeaterItem.FindControl("lblSelected") as Label;
                if (null != lblSelected) lblSelected.Text = value;
            }
        }

        #endregion

        #region [ Row Attributes - Job Call Log Conditions ]

        public string JobCallLogConditionsRowCriteira
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCriteria = JobCallLogConditionsRepeaterItem.FindControl("lblCriteria") as Label;
                if (null != lblCriteria) lblCriteria.Text = value;

                HtmlTableRow trJobCallLogConditions = JobCallLogConditionsRepeaterItem.FindControl("trJobCallLogConditions") as HtmlTableRow;
                if (null != trJobCallLogConditions)
                {
                    trJobCallLogConditions.Attributes["class"] = "even CallLogsItems CC" + CallCriteriaRowId.ToString();
                    trJobCallLogConditions.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedJobCallLogConditions.Value.Contains("CC" + value.ToString() + ";"))
                        trJobCallLogConditions.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trJobCallLogConditions.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public string JobCallLogConditionsRowSelected
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblSelected = JobCallLogConditionsRepeaterItem.FindControl("lblSelected") as Label;
                if (null != lblSelected) lblSelected.Text = value;
            }
        }

        #endregion

        #endregion

        #region [ Common ]

        public int? EmployeeID
        {
            get
            {
                if (null != ViewState["EmployeeID"])
                    return Convert.ToInt32(ViewState["EmployeeID"]);

                return null;
            }
            set
            {
                ViewState["EmployeeID"] = value;
            }
        }

        public int? ContactID
        {
            get
            {
                if (null != ViewState["ContactID"])
                {
                    return Convert.ToInt32(ViewState["ContactID"]);
                }
                return null;
            }
            set
            {
                ViewState["ContactID"] = value;
            }
        }

        #endregion
        #endregion
    }
}
