using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class Permitting : System.Web.UI.Page, IPermittingView
    {
        #region [ Attributes ]

        PermittingPresenter _presenter;

        RepeaterItem _firstTierDataItem;
        RepeaterItem _secondTierDataItem;

        GridViewRow _shoppingCartDataItem;

        RepeaterItem _comboHistoryRepeaterItem;

        private List<int> SelectedEquipments
        {
            get
            {
                if (null == ViewState["SelectedEquipmentsAdd"])
                    SelectedEquipmentsAdd = new List<int>();
                return ViewState["SelectedEquipmentsAdd"] as List<int>;
            }
        }

        private IList<CS_View_EquipmentInfo> EquipmentGridInfoList
        {
            get
            {
                if (null == ViewState["EquipmentGridInfoList"])
                    EquipmentGridInfoList = new List<CS_View_EquipmentInfo>();

                return ViewState["EquipmentGridInfoList"] as IList<CS_View_EquipmentInfo>;
            }
            set
            {
                ViewState["EquipmentGridInfoList"] = value;
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new PermittingPresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Listing / Delete]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadEquipmentCombo();

                if (!QueryStringEquipmentComboId.HasValue)
                {
                    _presenter.HideCreationPanels();
                    ScriptManager.GetCurrent(this).SetFocus(btnCreate);
                }
                else
                {
                    txtHasEquipments.Text = "1";
                    txtHasPrimarySelected.Text = "1";
                    ScriptManager.GetCurrent(this).SetFocus(btnRemove);
                }
            }
        }

        protected void rptCombo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            _firstTierDataItem = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _presenter.SetEquipmentComboRowData();
                _presenter.LoadDetailedEquipmentCombo();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                EquipmentHeaderCssClass();
            }
        }

        protected void rptCombo_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Combo")
            {
                ClearFields(true);
                this.EquipmentComboId = Convert.ToInt32(e.CommandArgument);
                _presenter.LoadCombo();
                txtHasEquipments.Text = "1";
                txtHasPrimarySelected.Text = "1";
            }
            if (e.CommandName == "DeleteCombo")
            {
                this.EquipmentComboId = Convert.ToInt32(e.CommandArgument);

                _presenter.DeleteEquipmentCombo();
                
            }
        }

        protected void rptEquipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _secondTierDataItem = e.Item;
                _presenter.SetDetailedEquipmentComboRowData();
            }
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.LoadEquipmentCombo();
        }

        public void btnCreate_Click(object sender, EventArgs e)
        {
            CreationPanelVisible = true;
            ClearFields(true);
            ScriptManager.GetCurrent(this).SetFocus(cbFilterEquipment);
        }

        public void btnFindEquipment_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEquipmentInfo();
            ScriptManager.GetCurrent(this).SetFocus(txtFilterEquipment);
        }

        #endregion

        #region [ Insert/Update ]

        protected void gvEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_View_EquipmentInfo dataItem = e.Row.DataItem as CS_View_EquipmentInfo;
                SetTextColorOnStatus(e.Row.Cells[5], dataItem.Status);

                CheckBox chkEquipmentAdd = e.Row.FindControl("chkEquipmentAdd") as CheckBox;
                if (null != chkEquipmentAdd)
                {
                    chkEquipmentAdd.Checked = SelectedEquipments.Exists(f => f == dataItem.EquipmentID);
                    chkEquipmentAdd.Enabled = !dataItem.ComboID.HasValue;
                }

                if (dataItem.JobID.HasValue)
                {
                    HyperLink hlJobNumber = e.Row.FindControl("hlJobNumber") as HyperLink;
                    hlJobNumber.Text = dataItem.JobNumber;
                    hlJobNumber.NavigateUrl =
                        string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", dataItem.JobID);
                }

                if (dataItem.IsPrimary == 1)
                    e.Row.Cells[9].Text = "P";
            }
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedEquipmentsAdd != null)
            {
                if (SelectedEquipmentsAdd.Count > 0)
                {
                    _presenter.AddEquipmentsToShopingCart();
                    if (EquipmentInfoShoppingCartDataSource.Count > 0)
                        txtHasEquipments.Text = "1";
                }
            }

            ScriptManager.GetCurrent(this).SetFocus(txtComboName);
        }

        protected void gvShoppingCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _shoppingCartDataItem = e.Row;
                _presenter.LoadShoppingCartRow();
                RadioButton rbPrimaryUnit = (RadioButton)e.Row.FindControl("rbPrimaryUnit");
                if (null != rbPrimaryUnit)
                {
                    string script = "SetUniqueRadioButton('gvShoppingCart.*PrimaryUnit',this); document.getElementById('" + txtHasPrimarySelected.ClientID + "').value = '1';";
                    rbPrimaryUnit.Attributes.Add("onclick", script);
                }
            }
        }

        public void btnRemove_Click(object sender, EventArgs e)
        {
            _presenter.RemoveEquipmentFromShoppingCart();
            if (EquipmentInfoShoppingCartDataSource.Count.Equals(0))
                txtHasEquipments.Text = string.Empty;
            if (PrimaryEquipmentId.Equals(0))
                txtHasPrimarySelected.Text = string.Empty;
        }

        public void btnSaveContinue_Click(object sender, EventArgs e)
        {
            SavePermitting(true);
        }

        public void btnSaveClose_Click(object sender, EventArgs e)
        {
            SavePermitting(false);
            DisplayMessage("Combo saved successfully.", true);
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields(true);
            _presenter.LoadEquipmentCombo();
        }

        #endregion

        #region [ History ]

        public void rptComboHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _comboHistoryRepeaterItem = e.Item;
                _presenter.FillComboLogList();
            }
        }

        #endregion

        #endregion

        #region [ Properties ]

        #region [ Listing ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public string[] OrderBy
        {
            get
            {
                return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Globals.Permitting.PermittingSortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Permitting.PermittingSortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Permitting.PermittingSortColumns)result;
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

        public List<CS_View_EquipmentInfo> EquipmentInfoListData { get; set; }

        public IList<CS_View_EquipmentInfo> FirstTierDataSource
        {
            get { throw new NotImplementedException(); }
            set
            {
                rptCombo.DataSource = value;
                rptCombo.DataBind();
            }
        }

        public CS_View_EquipmentInfo FirstTierDataItem
        {
            get { return _firstTierDataItem.DataItem as CS_View_EquipmentInfo; }
            set { throw new NotImplementedException(); }
        }

        public int FirstTierComboId
        {
            get { throw new NotImplementedException(); }
            set
            {
                Button lbEdit = _firstTierDataItem.FindControl("btnEdit") as Button;
                if (null != lbEdit)
                {
                    lbEdit.CommandArgument = value.ToString();
                }

                Button btnDelete = _firstTierDataItem.FindControl("btnDelete") as Button;
                if (null != lbEdit)
                {
                    btnDelete.CommandArgument = value.ToString();
                    btnDelete.OnClientClick = "return confirm('Are you sure you want to delete the combo?')";
                }

                HtmlGenericControl divexpand = _firstTierDataItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divexpand)
                {
                    if (value > 0)
                    {
                        divexpand.Attributes.Add("onclick", "CollapseExpand('" + divexpand.ClientID + "','" + value.ToString() + "');");
                        divexpand.Visible = true;

                        //string[] expandedjobs = hfExpandedCombos.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        //if (expandedjobs.Contains(value.ToString()))
                        //    divexpand.Attributes["class"] = "collapse";
                    }
                    else
                        divexpand.Visible = false;
                }
            }
        }

        public int FirstTierJobId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = _firstTierDataItem.FindControl("hlJobNumber") as HyperLink;
                if (null != hlJobNumber) hlJobNumber.NavigateUrl =
                    string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", value);
            }
        }

        public string FirstTierComboName
        {
            get
            {
                Label lblComboName = _firstTierDataItem.FindControl("lblComboName") as Label;
                if (null != lblComboName)
                    return lblComboName.Text;

                return string.Empty;
            }
            set
            {
                Label lblComboName = _firstTierDataItem.FindControl("lblComboName") as Label;
                if (null != lblComboName) lblComboName.Text = value;
            }
        }

        public string FirstTierUnitNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblUnitNumber = _firstTierDataItem.FindControl("lblUnitNumber") as Label;
                if (null != lblUnitNumber) lblUnitNumber.Text = value;
            }
        }

        public DateTime FirstTierCreateDate
        {
            get
            {
                Label lblCreateDate = _firstTierDataItem.FindControl("lblCreateDate") as Label;
                if (null != lblCreateDate)
                    return DateTime.Parse(lblCreateDate.Text);

                return DateTime.MinValue;
            }
            set
            {
                Label lblCreateDate = _firstTierDataItem.FindControl("lblCreateDate") as Label;
                if (null != lblCreateDate) lblCreateDate.Text = value.ToShortDateString();
            }
        }

        public string FirstTierDivisionNumber
        {
            get
            {
                Label lblDivName = _firstTierDataItem.FindControl("lblDivName") as Label;
                if (null != lblDivName)
                    return lblDivName.Text;

                return string.Empty;
            }
            set
            {
                Label lblDivName = _firstTierDataItem.FindControl("lblDivName") as Label;
                if (null != lblDivName) lblDivName.Text = value;
            }
        }

        public string FirstTierDivisionState
        {
            get
            {
                Label lblDivState = _firstTierDataItem.FindControl("lblDivState") as Label;
                if (null != lblDivState)
                    return lblDivState.Text;

                return string.Empty;
            }
            set
            {
                Label lblDivState = _firstTierDataItem.FindControl("lblDivState") as Label;
                if (null != lblDivState) lblDivState.Text = value;
            }
        }

        public string FirstTierTypeDescriptor
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblTypeDescriptor = _firstTierDataItem.FindControl("lblTypeDescriptor") as Label;
                if (null != lblTypeDescriptor) lblTypeDescriptor.Text = value;
            }
        }

        public string FirstTierJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = _firstTierDataItem.FindControl("hlJobNumber") as HyperLink;
                if (null != hlJobNumber) hlJobNumber.Text = value;
            }
        }

        public IList<CS_View_EquipmentInfo> SecondTierDataSource
        {
            get { throw new NotImplementedException(); }
            set
            {
                Repeater rptEquipments = (Repeater)_firstTierDataItem.FindControl("rptEquipments");
                if (null != rptEquipments)
                {
                    rptEquipments.DataSource = value;
                    rptEquipments.DataBind();
                }
            }
        }

        public CS_View_EquipmentInfo SecondTierDataItem
        {
            get { return _secondTierDataItem.DataItem as CS_View_EquipmentInfo; }
            set { throw new NotImplementedException(); }
        }

        public int SecondTierJobId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = _secondTierDataItem.FindControl("hlJobNumber") as HyperLink;
                if (null != hlJobNumber) hlJobNumber.NavigateUrl =
                    string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", value);
            }
        }

        public string SecondTierUnitNumber
        {
            get
            {
                Label lblUnitNumber = _secondTierDataItem.FindControl("lblUnitNumber") as Label;
                if (null != lblUnitNumber)
                    return lblUnitNumber.Text;

                return string.Empty;
            }
            set
            {
                Label lblUnitNumber = _secondTierDataItem.FindControl("lblUnitNumber") as Label;
                if (null != lblUnitNumber) lblUnitNumber.Text = value;
            }
        }

        public bool SecondTierPrimaryUnit
        {
            get
            {
                Label lblPrimary = _secondTierDataItem.FindControl("lblPrimary") as Label;
                if (null != lblPrimary)
                    if (!string.IsNullOrEmpty(lblPrimary.Text))
                        return true;

                return false;
            }
            set
            {
                Label lblPrimary = _secondTierDataItem.FindControl("lblPrimary") as Label;
                if (null != lblPrimary)
                    if (value)
                        lblPrimary.Text = "P";
            }
        }

        public string SecondTierDivisionNumber
        {
            get
            {
                Label lblDivName = _secondTierDataItem.FindControl("lblDivName") as Label;
                if (null != lblDivName)
                    return lblDivName.Text;

                return string.Empty;
            }
            set
            {
                Label lblDivName = _secondTierDataItem.FindControl("lblDivName") as Label;
                if (null != lblDivName) lblDivName.Text = value;
            }
        }

        public string SecondTierDivisionState
        {
            get
            {
                Label lblDivState = _secondTierDataItem.FindControl("lblDivState") as Label;
                if (null != lblDivState)
                    return lblDivState.Text;

                return string.Empty;
            }
            set
            {
                Label lblDivState = _secondTierDataItem.FindControl("lblDivState") as Label;
                if (null != lblDivState) lblDivState.Text = value;
            }
        }

        public string SecondTierEquipmentTypeDescriptor
        {
            get
            {
                Label lblTypeDescriptor = _secondTierDataItem.FindControl("lblTypeDescriptor") as Label;
                if (null != lblTypeDescriptor)
                    return lblTypeDescriptor.Text;

                return string.Empty;
            }
            set
            {
                Label lblTypeDescriptor = _secondTierDataItem.FindControl("lblTypeDescriptor") as Label;
                if (null != lblTypeDescriptor) lblTypeDescriptor.Text = value;
            }
        }

        public string SecondTierJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = _secondTierDataItem.FindControl("hlJobNumber") as HyperLink;
                if (null != hlJobNumber) hlJobNumber.Text = value;
            }
        }

        public string SecondTierItemCssClass
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlTableRow trResource = _secondTierDataItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trResource)
                {
                    trResource.Attributes["class"] = "Child " + value.ToString();
                    trResource.Attributes.Add("oncontextmenu", "return false;");
                    trResource.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //if (!hfExpandedCombos.Value.Contains(";" + value))
                    //    
                    //else
                    //    trResource.Style.Add(HtmlTextWriterStyle.Display, "");

                    HtmlGenericControl divExpand = _secondTierDataItem.FindControl("divExpand") as HtmlGenericControl;
                    if (null != divExpand) divExpand.Visible = false;
                }
            }
        }

        public bool CreationPanelVisible
        {
            get { return pnlCreation.Visible; }
            set { pnlCreation.Visible = value; }
        }

        public bool LogPanelVisible
        {
            get { return pnlLog.Visible; }
            set { pnlLog.Visible = value; }
        }

        #endregion

        #region [ History ]

        public IList<ComboLog> ComboHistoryLogDataSource
        {
            get
            {
                if (null != ViewState["ComboLogList"])
                    return ViewState["ComboLogList"] as IList<ComboLog>;
                return new List<ComboLog>();
            }
            set
            {
                rptComboHistory.DataSource = value;
                rptComboHistory.DataBind();
                ViewState["ComboLogList"] = value;
            }
        }

        public ComboLog ComboHistoryRepeaterDataItem
        {
            get { return _comboHistoryRepeaterItem.DataItem as ComboLog; }
            set { throw new NotImplementedException(); }
        }

        public string ComboHistoryRowName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblName = _comboHistoryRepeaterItem.FindControl("lblName") as Label;
                if (null != lblName) lblName.Text = value;
            }
        }

        public string ComboHistoryRowUnits
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDetails = _comboHistoryRepeaterItem.FindControl("lblUnitsText") as Label;
                if (null != lblDetails) lblDetails.Text = value;
            }
        }

        public string ComboHistoryRowPrimary
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDetails = _comboHistoryRepeaterItem.FindControl("lblPrimaryText") as Label;
                if (null != lblDetails) lblDetails.Text = value;
            }
        }

        public string ComboHistoryRowDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDetails = _comboHistoryRepeaterItem.FindControl("lblDivisionText") as Label;
                if (null != lblDetails) lblDetails.Text = value;
            }
        }

        #endregion

        #region [ Insert/Update/Delete ]

        #region [ Equipment Listing ]

        public Globals.ResourceAllocation.EquipmentFilters EquipmentFilters
        {
            get
            {
                return (Globals.ResourceAllocation.EquipmentFilters)int.Parse(cbFilterEquipment.SelectedValue);
            }
            set { throw new NotImplementedException(); }
        }

        public string FilterValue
        {
            get
            {
                return txtFilterEquipment.Text;
            }
            set { throw new NotImplementedException(); }
        }

        public IList<CS_View_EquipmentInfo> ListFilteredEquipmentInfo
        {
            get { throw new NotImplementedException(); }
            set
            {
                gvEquipments.DataSource = value;
                gvEquipments.DataBind();
                EquipmentGridInfoList = value;
            }
        }

        #endregion

        #region [ Combo ]

        public bool IsAssignedToJob
        {
            get;
            set;
        }

        public CS_EquipmentCombo EquipmentCombo
        {
            get
            {
                return new CS_EquipmentCombo()
                {
                    ID = EquipmentComboId.HasValue ? EquipmentComboId.Value : 0,
                    PrimaryEquipmentID = PrimaryEquipmentId,
                    Name = ComboName,
                    ComboType = ComboType
                };
            }
            set { throw new NotImplementedException(); }
        }

        public int? EquipmentComboId
        {
            get
            {
                if (string.IsNullOrEmpty(hfEquipmentComboId.Value))
                    return null;
                return Convert.ToInt32(hfEquipmentComboId.Value);
            }
            set
            {
                if (value.HasValue)
                    hfEquipmentComboId.Value = value.Value.ToString();
                else
                    hfEquipmentComboId.Value = string.Empty;
            }
        }

        public int? QueryStringEquipmentComboId
        {
            get
            {
                if (null == Request.QueryString["ComboId"])
                    return null;
                return Convert.ToInt32(Request.QueryString["ComboId"].ToString());
            }
        }

        public string ComboName
        {
            get { return txtComboName.Text; }
            set { txtComboName.Text = value; }
        }

        public string ComboType
        {
            get { return txtComboType.Text; }
            set { txtComboType.Text = value; }
        }

        public int PrimaryEquipmentId
        {
            get
            {
                int primaryEquipmentId = 0;
                for (int i = 0; i < gvShoppingCart.Rows.Count; i++)
                {
                    RadioButton rb = gvShoppingCart.Rows[i].FindControl("rbPrimaryUnit") as RadioButton;
                    if (rb.Checked)
                    {
                        Label lblEquipmentId = gvShoppingCart.Rows[i].FindControl("lblEquipmentId") as Label;
                        primaryEquipmentId = int.Parse(lblEquipmentId.Text);
                        break;
                    }
                }
                return primaryEquipmentId;
            }
        }

        public string UserName
        {
            get { return Master.Username; }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Combo Equipments - Shopping Cart ]

        public IList<EquipmentComboVO> EquipmentComboChildren
        {
            get
            {
                IList<EquipmentComboVO> _equipmentComboChildren = new List<EquipmentComboVO>();
                foreach (int equipmentId in SelectedEquipments)
                {
                }

                for (int i = 0; i < gvEquipments.Rows.Count; i++)
                {
                    CheckBox cb = gvEquipments.Rows[i].FindControl("chkEquipmentAdd") as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblEquipmentId = gvEquipments.Rows[i].FindControl("lblEquipmentId") as Label;
                        CS_View_EquipmentInfo equipmentInfo = EquipmentGridInfoList.First(e => e.EquipmentID == int.Parse(lblEquipmentId.Text));

                        _equipmentComboChildren.Add(new EquipmentComboVO()
                        {
                            ComboId = (EquipmentComboId.HasValue ? EquipmentComboId.Value : 0),
                            EquipmentId = equipmentInfo.EquipmentID,
                            IsPrimary = EquipmentCombo.PrimaryEquipmentID == equipmentInfo.EquipmentID,
                            UnitNumber = equipmentInfo.UnitNumber,
                            Descriptor = equipmentInfo.Descriptor,
                            DivisionNumber = equipmentInfo.DivisionName,
                            Seasonal = equipmentInfo.Seasonal
                        });
                    }
                }

                return _equipmentComboChildren;
            }
            set { throw new NotImplementedException(); }
        }

        public IList<EquipmentComboVO> EquipmentInfoShoppingCartDataSource
        {
            get
            {
                if (null == ViewState["EquipmentInfoShoppingCartDataSource"])
                    ViewState["EquipmentInfoShoppingCartDataSource"] = new List<EquipmentComboVO>();
                return ViewState["EquipmentInfoShoppingCartDataSource"] as IList<EquipmentComboVO>;
            }
            set
            {
                ViewState["EquipmentInfoShoppingCartDataSource"] = value;

                gvShoppingCart.DataSource = value;
                gvShoppingCart.DataBind();
            }
        }

        public EquipmentComboVO EquipmentInfoItem
        {
            get { return _shoppingCartDataItem.DataItem as EquipmentComboVO; }
            set { throw new NotImplementedException(); }
        }

        public bool IsPrimaryObjectSelected
        {
            get { throw new NotImplementedException(); }
            set
            {
                RadioButton rbPrimaryUnit = _shoppingCartDataItem.FindControl("rbPrimaryUnit") as RadioButton;
                if (null != rbPrimaryUnit) rbPrimaryUnit.Checked = value;
            }
        }

        public string DivisionSelected
        {
            get { throw new NotImplementedException(); }
            set { _shoppingCartDataItem.Cells[2].Text = value; }
        }

        public string UnitNumberSelected
        {
            get { throw new NotImplementedException(); }
            set { _shoppingCartDataItem.Cells[3].Text = value; }
        }

        public string DescriptorSelected
        {
            get { throw new NotImplementedException(); }
            set { _shoppingCartDataItem.Cells[4].Text = value; ; }
        }

        public IList<int> SelectedEquipmentsAdd
        {
            get
            {
                List<int> returnValue = new List<int>();
                for (int i = 0; i < gvEquipments.Rows.Count; i++)
                {
                    CheckBox chkBox = gvEquipments.Rows[i].FindControl("chkEquipmentAdd") as CheckBox;
                    if (null != chkBox)
                    {
                        if (chkBox.Checked)
                        {
                            Label lblEquipmentId = gvEquipments.Rows[i].FindControl("lblEquipmentId") as Label;
                            returnValue.Add(Int32.Parse(lblEquipmentId.Text));
                        }
                    }
                }

                SelectedEquipmentsAdd = returnValue;
                return returnValue;
            }
            set { ViewState["SelectedEquipmentsAdd"] = value; }
        }

        public IList<int> RemovedEquipments
        {
            get
            {
                List<int> returnValue = new List<int>();
                for (int i = 0; i < gvShoppingCart.Rows.Count; i++)
                {
                    CheckBox chkBox = gvShoppingCart.Rows[i].FindControl("chkEquipmentAdd") as CheckBox;
                    if (null != chkBox)
                    {
                        if (chkBox.Checked)
                        {
                            Label lblEquipmentId = gvShoppingCart.Rows[i].FindControl("lblEquipmentId") as Label;
                            returnValue.Add(Int32.Parse(lblEquipmentId.Text));
                        }
                    }
                }

                return returnValue;
            }
            set { throw new NotImplementedException(); }
        }

        public bool SavedSuccessfuly { get; set; }

        #endregion

        #endregion

        #endregion

        #region [ Methods ]

        private void EquipmentHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Permitting.PermittingSortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Permitting.PermittingSortColumns.DivisionName:
                    control = _firstTierDataItem.FindControl("thDivisionName") as HtmlTableCell;
                    break;
                case Globals.Permitting.PermittingSortColumns.DivisionState:
                    control = _firstTierDataItem.FindControl("thDivisionState") as HtmlTableCell;
                    break;
                case Globals.Permitting.PermittingSortColumns.ComboUnit:
                    control = _firstTierDataItem.FindControl("thComboUnit") as HtmlTableCell;
                    break;
                case Globals.Permitting.PermittingSortColumns.Descriptor_Type:
                    control = _firstTierDataItem.FindControl("thEquipmentTypeDescriptor") as HtmlTableCell;
                    break;
                case Globals.Permitting.PermittingSortColumns.JobNumber:
                    control = _firstTierDataItem.FindControl("thJobNumber") as HtmlTableCell;
                    break;
                case Globals.Permitting.PermittingSortColumns.CreateDate:
                    control = _firstTierDataItem.FindControl("thCreateDate") as HtmlTableCell;
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

        private void SetTextColorOnStatus(TableCell cell, string value)
        {
            if (value == "Assigned")
                cell.ForeColor = System.Drawing.Color.DarkBlue;
            else if (value == "Available")
                cell.ForeColor = System.Drawing.Color.DarkGreen;
            else if (value == "Unavailable")
                cell.ForeColor = System.Drawing.Color.Red;
            else if (value == "Transfer Available")
                cell.ForeColor = System.Drawing.Color.Green;
        }

        private void SavePermitting(bool saveContinue)
        {
            if (IsValid)
            {
                _presenter.SaveEquipmentCombo();
                if (SavedSuccessfuly)
                {
                    if (saveContinue)
                        _presenter.BindPermittingHistoryLog();
                    _presenter.LoadEquipmentCombo();

                    ClearFields(false);
                    _presenter.ListFilteredEquipmentInfo();
                }
            }
        }

        private void ClearFields(bool clearEquipmentGrid)
        {
            EquipmentComboId = null;
            ComboName = string.Empty;
            ComboType = string.Empty;
            txtHasEquipments.Text = string.Empty;
            txtHasPrimarySelected.Text = string.Empty;
            SelectedEquipmentsAdd = null;
            EquipmentInfoShoppingCartDataSource = null;

            if (clearEquipmentGrid)
            {
                ListFilteredEquipmentInfo = null;
                cbFilterEquipment.SelectedIndex = 0;
                txtFilterEquipment.Text = string.Empty;
            }
        }

        #endregion
    }
}
