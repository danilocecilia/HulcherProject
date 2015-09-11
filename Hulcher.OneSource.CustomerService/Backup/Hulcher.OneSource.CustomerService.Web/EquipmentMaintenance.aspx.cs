using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Web.UserControls;
using System.Web.UI.HtmlControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class EquipmentMaintenance : System.Web.UI.Page, IEquipmentMaintenanceView
    {
        #region [ Attributes ]

        #region  [ EquipmentDisplay ]
        private RepeaterItem _firstTierItem;

        private RepeaterItem _secondTierItem;

        private RepeaterItem _thirdTierItem;

        private int divisionRowEquipmentTypeID;

        private int divisionRowDivisionID;
        #endregion

        /// <summary>
        /// Instance of Presenter Class
        /// </summary>
        private EquipmentMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new EquipmentMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            KeepValidatorsState();
            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEquipment();
        }

        protected void gvEquipmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EquipmentRow = e.Row;
                EquipmentRowDataItem = e.Row.DataItem as CS_View_EquipmentInfo;
                _presenter.BindEquipmentRow();
            }
        }

        protected void gvEquipmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("EditEquipment"))
            {
                EquipmentID = Convert.ToInt32(e.CommandArgument);
                _presenter.SetJobID();
                _presenter.VerifyIfResourceIsAssignedToJob();
                _presenter.LoadEquipment();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.SaveEquipment();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEquipment();
            ShowHideEquipmentDisplayRepeater = false;
            ReplicateChangesToCombo = false;
        }

        protected void btnFindMgmEq_Click(object sender, EventArgs e)
        {
            _presenter.BindManagementEquipmentDashboard();
            _presenter.BindEquipmentTypeDisplay();
        }

        protected void btnSaveEquipmentDisplay_Click(object sender, EventArgs e)
        {
            _presenter.UpdateEquipmentDisplay();
        }

        protected void btnEquipmentDisplay_Click(object sender, EventArgs e)
        {
            ShowHideEquipmentDisplayRepeater = true;
            ShowHidePanelVisualization = false;

            _presenter.BindManagementEquipmentDashboard();
            _presenter.BindEquipmentTypeDisplay();

        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.BindEquipmentTypeDisplay();
        }

        #region [ Repeater ]
        protected void rptEquipmentType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _firstTierItem = e.Item;
                _presenter.LoadFirstTierItem();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                _firstTierItem = e.Item;
                EquipmentHeaderCssClass();
            }
        }

        protected void rptDivision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _secondTierItem = e.Item;
                _presenter.CreateDivisionRow();
            }
        }

        protected void rptEquipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _thirdTierItem = e.Item;
                _presenter.CreateEquipmentRow();
            }
        }
        #endregion

        #region [ Equipment Status UpDown ]

        public void ddlEquipmentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.ShowHideEquipmentStatusDuration();
        }

        #endregion

        #endregion

        #region [ Properties ]

        public int JobID
        {
            get;
            set;
        }

        #region [ Listing ]

        public IList<CS_View_EquipmentInfo> EquipmentList
        {
            set
            {
                gvEquipmentList.DataSource = value;
                gvEquipmentList.DataBind();
            }
        }

        public Globals.EquipmentMaintenance.FilterType? FilterType
        {
            get
            {
                if (string.IsNullOrEmpty(cbFilter.SelectedValue) || cbFilter.SelectedValue.Equals("0"))
                    return null;
                else
                    return (Globals.EquipmentMaintenance.FilterType)Convert.ToInt32(cbFilter.SelectedValue);
            }
        }

        public string FilterValue
        {
            get { return txtFilterValue.Text; }
        }

        public bool? EnableJobNumberLink
        {
            get
            {
                HyperLink hlJobNumber = (HyperLink)EquipmentRow.FindControl("hlJobNumber");
                if (hlJobNumber != null)
                {
                    return hlJobNumber.Enabled;
                }
                else
                    return null;
            }
            set
            {
                HyperLink hlJobNumber = (HyperLink)EquipmentRow.FindControl("hlJobNumber");
                if (hlJobNumber != null && value.HasValue)
                {
                    hlJobNumber.Enabled = value.Value;
                }
            }
        }

        #region [ Equipment Row ]

        private GridViewRow EquipmentRow { get; set; }

        public CS_View_EquipmentInfo EquipmentRowDataItem { get; set; }

        public int EquipmentRowEquipmentID
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfEquipmentId = EquipmentRow.Cells[0].FindControl("hfEquipmentId") as HiddenField;
                if (null != hfEquipmentId) hfEquipmentId.Value = value.ToString();

                LinkButton lnkEdit = EquipmentRow.Cells[11].FindControl("lnkEdit") as LinkButton;
                if (null != lnkEdit) lnkEdit.CommandArgument = value.ToString();
            }
        }

        public string EquipmentRowDivisionName
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[1].Text = value;
            }
        }

        public string EquipmentRowDivisionState
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[2].Text = value;
            }
        }

        public string EquipmentRowComboName
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[3].Text = value;
            }
        }

        public string EquipmentRowUnitNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[4].Text = value;
            }
        }

        public string EquipmentRowDescription
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[5].Text = value;
            }
        }

        public string EquipmentRowStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[6].Text = value;
                EquipmentRow.Cells[6].Font.Bold = true;
                if (value.Equals("Available"))
                    EquipmentRow.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                else
                    EquipmentRow.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        public string EquipmentRowJobLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[7].Text = value;
            }
        }

        public string EquipmentRowLastCallEntryDescription
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    HyperLink hlLastCallEntry = EquipmentRow.Cells[8].FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry) hlLastCallEntry.Text = value;
                }
            }
        }

        public int?[] EquipmentRowLastCallEntryID
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value[0].HasValue & value[1].HasValue)
                {
                    HyperLink hlLastCallEntry = EquipmentRow.Cells[8].FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry) hlLastCallEntry.NavigateUrl = string.Format("javascript:OpenCallEntry({0}, {1})", value[0].Value, value[1].Value);
                }
            }
        }

        public string EquipmentRowJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    HyperLink hlJobNumber = EquipmentRow.Cells[9].FindControl("hlJobNumber") as HyperLink;
                    if (null != hlJobNumber) hlJobNumber.Text = value;
                }
            }
        }

        public int? EquipmentRowJobID
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    HyperLink hlJobNumber = EquipmentRow.Cells[9].FindControl("hlJobNumber") as HyperLink;
                    if (null != hlJobNumber) hlJobNumber.NavigateUrl = string.Format("javascript:OpenJobRecord({0})", value.Value.ToString());
                }
            }
        }

        public string EquipmentRowOperationalStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                EquipmentRow.Cells[10].Text = value;
            }
        }

        #endregion

        #region [ EquipmentDisplay ]

        public IList<CS_EquipmentType> EquipmentTypeList
        {
            get
            {
                return ViewState["EquipmentTypeList"] as IList<CS_EquipmentType>;
            }
            set
            {
                ViewState["EquipmentTypeList"] = value;
            }
        }

        public string FilterValueEquipmentDisplay
        {
            get
            {
                return txtFilterValueMgmEq.Text;
            }
        }

        public Globals.EquipmentMaintenance.FilterType FilterTypeEquipmentDisplay
        {
            get
            {
                return (Globals.EquipmentMaintenance.FilterType)Convert.ToInt32(cbFilterEquipmentDisplay.SelectedValue);
            }
        }

        public IList<CS_View_EquipmentInfo> EquipmentListDisplay
        {
            get
            {
                return ViewState["EquipmentListDisplay"] as IList<CS_View_EquipmentInfo>;
            }
            set
            {
                ViewState["EquipmentListDisplay"] = value;
            }
        }

        public List<int> SelectedHeavyEquipments
        {
            get
            {
                return SelectedCheckBoxes(true, false);
            }
        }

        public List<int> SelectedDisplayInResourceAllocation
        {
            get
            {
                return SelectedCheckBoxes(false, true);
            }
        }

        #region [ EquipmentType ]

        public bool EquipmentTypechkHeavyEquipment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CheckBox chkEquipmentTypeHeavyEquipment = _firstTierItem.FindControl("chkEquipmentTypeHeavyEq") as CheckBox;

                if (null != chkEquipmentTypeHeavyEquipment)
                {
                    chkEquipmentTypeHeavyEquipment.Checked = value;
                }
            }
        }

        public bool EquipmentTypechkEquipmentTypeResAllocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CheckBox chkEquipmentTypeResAllocation = _firstTierItem.FindControl("chkEquipmentTypeResAllocation") as CheckBox;

                if (null != chkEquipmentTypeResAllocation)
                {
                    chkEquipmentTypeResAllocation.Checked = value;

                }
            }
        }

        public string FirstTierItemUnitType
        {
            set
            {
                Label lblUnitType = _firstTierItem.FindControl("lblUnitType") as Label;
                lblUnitType.Text = value;
            }
        }

        public CS_EquipmentType FirstTierDataItem
        {
            get
            {
                return _firstTierItem.DataItem as CS_EquipmentType;
            }
        }

        public IList<CS_EquipmentType> EquipmentTypeDataSource
        {
            set
            {
                EquipmentTypeList = value;
                pnlNoRows.Visible = value.Count.Equals(0);
                rptEquipmentType.Visible = !value.Count.Equals(0);
                rptEquipmentType.DataSource = value;
                rptEquipmentType.DataBind();
            }
        }


        public bool EquipmentTypeRowHasDivision
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlGenericControl divExpand = _firstTierItem.FindControl("divexpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpandEquipmentType('" + divExpand.ClientID + "','EqType" + EquipmentTypeRowID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }
        }

        public int EquipmentTypeRowID
        {
            get
            {
                HiddenField hfEquipmentTypeID = _firstTierItem.FindControl("hfEquipmentTypeID") as HiddenField;

                if (null != hfEquipmentTypeID)
                {
                    return int.Parse(hfEquipmentTypeID.Value);
                }

                return 0;
            }
            set
            {
                HiddenField hfEquipmentTypeID = _firstTierItem.FindControl("hfEquipmentTypeID") as HiddenField;

                if (null != hfEquipmentTypeID)
                {
                    hfEquipmentTypeID.Value = value.ToString();
                }

                HtmlGenericControl divExpand = _firstTierItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedRegions = hfExpandedEquipmentType.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedRegions.Contains("EqType" + value.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                }

                CheckBox chkEquipmentTypeHeavyEquipment = _firstTierItem.FindControl("chkEquipmentTypeHeavyEq") as CheckBox;

                if (null != chkEquipmentTypeHeavyEquipment)
                {
                    chkEquipmentTypeHeavyEquipment.Attributes.Add("onclick", string.Format("CheckAllH(this,{0});GetEquipmentValuesH();", value.ToString()));

                    if (hfSelectedH.Value.Contains("EqTypeMain" + value.ToString()))
                        chkEquipmentTypeHeavyEquipment.Checked = true;
                }

                CheckBox chkEquipmentTypeResAllocation = _firstTierItem.FindControl("chkEquipmentTypeResAllocation") as CheckBox;

                if (null != chkEquipmentTypeResAllocation)
                {
                    chkEquipmentTypeResAllocation.Attributes.Add("onclick", string.Format("CheckAllD(this,{0});GetEquipmentValuesD();", value.ToString()));

                    if (hfSelectedD.Value.Contains("EqTypeMain" + value.ToString()))
                        chkEquipmentTypeResAllocation.Checked = true;
                }

                HtmlTableRow trItem = _firstTierItem.FindControl("trItem") as HtmlTableRow;

                if (null != trItem)
                    trItem.Attributes["class"] += " EqTypeMain" + value.ToString();

            }
        }
        #endregion

        #region [ Division ]
        public bool DivisionchkHeavyEquipment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CheckBox chkDivisionHeavyEquipment = _secondTierItem.FindControl("chkDivisionHeavyEquipment") as CheckBox;

                if (null != chkDivisionHeavyEquipment)
                {
                    chkDivisionHeavyEquipment.Checked = value;
                }
            }
        }

        public bool DivisionchkDisplayInResource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CheckBox chkResourceAllocation = _secondTierItem.FindControl("chkResourceAllocation") as CheckBox;

                if (null != chkResourceAllocation)
                {
                    chkResourceAllocation.Checked = value;
                }
            }
        }

        public CS_View_EquipmentInfo SecondTierDataItem
        {
            get
            {
                return _secondTierItem.DataItem as CS_View_EquipmentInfo;
            }
        }

        public string SecondTierItemDivisionName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblDivision = _secondTierItem.FindControl("lblDivision") as Label;
                lblDivision.Text = value;
            }
        }

        public IList<CS_View_EquipmentInfo> DivisionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater rptDivision = _firstTierItem.FindControl("rptDivision") as Repeater;

                if (null != rptDivision)
                {
                    rptDivision.DataSource = value;
                    rptDivision.DataBind();
                }
            }
        }

        public int DivisionRowEquipmentTypeID
        {
            get
            {
                return divisionRowEquipmentTypeID;
            }
            set
            {
                divisionRowEquipmentTypeID = value;
                HtmlTableRow trDivision = _secondTierItem.FindControl("trDivision") as HtmlTableRow;
                if (null != trDivision)
                {
                    trDivision.Attributes["class"] = "odd Division EqType" + value.ToString();
                    trDivision.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedEquipmentType.Value.Contains("EqType" + value.ToString() + ";"))
                        trDivision.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trDivision.Style.Add(HtmlTextWriterStyle.Display, "");
                }

                HtmlTableRow trItem = _secondTierItem.FindControl("trDivision") as HtmlTableRow;

                if (null != trItem)
                    trItem.Attributes["class"] += " DivMain" + SecondTierDataItem.DivisionID;
            }
        }

        public int DivisionRowDivisionID
        {
            get { return divisionRowDivisionID; }
            set
            {
                divisionRowDivisionID = value;

                HtmlGenericControl divExpand = _secondTierItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedDivisions = hfExpandedDivision.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] expandedEquipmentType = hfExpandedEquipmentType.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedDivisions.Contains("EqType" + divisionRowEquipmentTypeID + "Div" + value.ToString()) && expandedEquipmentType.Contains("EqType" + divisionRowEquipmentTypeID.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                    else
                        hfExpandedDivision.Value = hfExpandedDivision.Value.Replace("EqType" + divisionRowEquipmentTypeID + "Div" + value.ToString() + ";", "");
                }

                CheckBox chkDivisionHeavyEquipment = _secondTierItem.FindControl("chkDivisionHeavyEquipment") as CheckBox;
                CheckBox chkResourceAllocation = _secondTierItem.FindControl("chkResourceAllocation") as CheckBox;

                if (null != chkDivisionHeavyEquipment && chkResourceAllocation != null)
                {
                    chkDivisionHeavyEquipment.Attributes.Add("onclick", string.Format("CheckAllDivisionH(this,{0},{1});GetEquipmentValuesH();", FirstTierDataItem.ID, value.ToString()));
                    chkResourceAllocation.Attributes.Add("onclick", string.Format("CheckAllDivisionD(this,{0},{1});GetEquipmentValuesD();", FirstTierDataItem.ID, value.ToString()));

                    if (hfSelectedH.Value.Contains("EqType" + FirstTierDataItem.ID + " " + "DivMain" + value.ToString()))
                        chkDivisionHeavyEquipment.Checked = true;

                    if (hfSelectedD.Value.Contains("EqType" + FirstTierDataItem.ID + " " + "DivMain" + value.ToString()))
                        chkResourceAllocation.Checked = true;
                }
            }
        }

        public bool DivisionRowHasEquipment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlGenericControl divExpand = _secondTierItem.FindControl("divexpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpandDivision('" + divExpand.ClientID + "', 'EqType" + DivisionRowEquipmentTypeID.ToString() + "', 'Div" + DivisionRowDivisionID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }
        }
        #endregion

        #region [ Equipment ]

        public bool EquipmentchkHeavyEquipment
        {
            set
            {
                CheckBox chkEquipmentHeavyEquipment = _thirdTierItem.FindControl("chkEquipmentHeavyEquipment") as CheckBox;

                if (null != chkEquipmentHeavyEquipment)
                {
                    chkEquipmentHeavyEquipment.Checked = value;
                }
            }
        }

        public bool EquipmentchkResourceAllocation
        {
            set
            {
                CheckBox chkResourceAllocation = _thirdTierItem.FindControl("chkResourceAllocation") as CheckBox;

                if (null != chkResourceAllocation)
                {
                    chkResourceAllocation.Checked = value;
                }
            }
        }

        public int EquipmentRowDivisionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEquipment = _thirdTierItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] = trEquipment.Attributes["class"] + " Div" + value.ToString();
                    trEquipment.Attributes.Add("oncontextmenu", "return false;");
                    trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
                    if (!hfExpandedDivision.Value.Contains("EqType" + ThirdTierDataItem.EquipmentTypeID + "Div" + value.ToString() + ";"))
                        trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trEquipment.Style.Add(HtmlTextWriterStyle.Display, "");
                }

                CheckBox chkEquipmentHeavyEquipment = _thirdTierItem.FindControl("chkEquipmentHeavyEquipment") as CheckBox;
                HtmlTableRow equipmentRowH = _thirdTierItem.FindControl("trEquipment") as HtmlTableRow;

                if (null != chkEquipmentHeavyEquipment)
                {
                    chkEquipmentHeavyEquipment.Attributes.Add("onclick",
                                                              string.Format("CheckDivisionByEquipmentH(this,{0},{1},'{2}','{3}');GetEquipmentValuesH();",
                                                                            ThirdTierDataItem.EquipmentTypeID,
                                                                            ThirdTierDataItem.DivisionID,
                                                                            equipmentRowH.ClientID,
                                                                            ThirdTierDataItem.EquipmentID));

                    if (hfSelectedH.Value.Contains("Eq" + ThirdTierDataItem.EquipmentID))
                        chkEquipmentHeavyEquipment.Checked = true;
                }

                CheckBox chkResourceAllocation = _thirdTierItem.FindControl("chkResourceAllocation") as CheckBox;

                if (null != chkResourceAllocation)
                {
                    chkResourceAllocation.Attributes.Add("onclick",
                                                            string.Format("CheckDivisionByEquipmentD(this,{0},{1},'{2}');GetEquipmentValuesD();",
                                                                          ThirdTierDataItem.EquipmentTypeID,
                                                                          ThirdTierDataItem.DivisionID,
                                                                          equipmentRowH.ClientID));


                    if (hfSelectedD.Value.Contains("Eq" + ThirdTierDataItem.EquipmentID))
                        chkResourceAllocation.Checked = true;
                }
            }
        }

        public CS_View_EquipmentInfo ThirdTierDataItem
        {
            get
            {
                return _thirdTierItem.DataItem as CS_View_EquipmentInfo;
            }
        }

        public string ThirdTierItemUnitDescription
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblUnitDescription = _thirdTierItem.FindControl("lblUnitDescription") as Label;
                lblUnitDescription.Text = value;
            }
        }

        public string ThirdTierItemUnitNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblUnitNumber = _thirdTierItem.FindControl("lblUnitNumber") as Label;
                lblUnitNumber.Text = value;
            }
        }

        public IList<CS_View_EquipmentInfo> EquipmentDisplayDataSource
        {
            set
            {
                Repeater rptEquipments = _secondTierItem.FindControl("rptEquipments") as Repeater;

                if (null != rptEquipments)
                {
                    rptEquipments.DataSource = value;
                    rptEquipments.DataBind();
                }
            }
        }

        public int EquipmentRowEquipmentTypeID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEquipment = _thirdTierItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] = "Child Equipment EqType" + value.ToString();
                    trEquipment.Attributes.Add("oncontextmenu", "return false;");
                    trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
            }
        }

        public int EquipmentDisplayEquipmentID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HiddenField hfEquipmentID = _thirdTierItem.FindControl("hfEquipmentID") as HiddenField;
                if (null != hfEquipmentID)
                {
                    hfEquipmentID.Value = value.ToString();
                }

                HtmlTableRow trEquipment = _thirdTierItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] += " Eq" + value.ToString();
                }
            }
        }
        #endregion

        #endregion

        #region [ Sort ]

        private string[] OrderBy
        {
            get
            {
                return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Globals.Common.Sort.EquipmentDisplaySortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.EquipmentDisplaySortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Common.Sort.EquipmentDisplaySortColumns)result;
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

        #endregion

        #region [ Form ]
        public bool ShowHidePanelVisualization
        {
            get
            {
                return pnlVisualization.Visible;
            }
            set
            {
                pnlVisualization.Visible = value;
            }
        }

        public bool EditMode
        {
            get { return pnlCreation.Visible; }
            set
            {
                pnlVisualization.Visible = !value;
                pnlCreation.Visible = value;

                EquipmentCoverageFieldsRequired = value;
            }
        }

        public int EquipmentID
        {
            get
            {
                if (null == ViewState["EquipmentId"])
                    ViewState["EquipmentId"] = 0;
                return Convert.ToInt32(ViewState["EquipmentId"]);
            }
            set { ViewState["EquipmentId"] = value; }
        }

        public string Username
        {
            get { return Master.Username; }
            set { throw new NotImplementedException(); }
        }

        #region [ Equipment Fields - Read Only ]

        public string EquipmentName
        {
            get { throw new NotImplementedException(); }
            set { lblName.Text = value; }
        }

        public string EquipmentDescription
        {
            get { throw new NotImplementedException(); }
            set { lblDescription.Text = value; }
        }

        public string EquipmentType
        {
            get { throw new NotImplementedException(); }
            set { lblType.Text = value; }
        }

        public string EquipmentLicensePlate
        {
            get { throw new NotImplementedException(); }
            set { lblLicensePlate.Text = value; }
        }

        public string EquipmentSerialNumber
        {
            get { throw new NotImplementedException(); }
            set { lblSerialNumber.Text = value; }
        }

        public string EquipmentYear
        {
            get { throw new NotImplementedException(); }
            set { lblYear.Text = value; }
        }

        public string EquipmentNotes
        {
            get { throw new NotImplementedException(); }
            set { lblNotes.Text = value; }
        }

        public string EquipmentBodyType
        {
            get { throw new NotImplementedException(); }
            set { lblBodyType.Text = value; }
        }

        public string EquipmentMake
        {
            get { throw new NotImplementedException(); }
            set { lblMake.Text = value; }
        }

        public string EquipmentModel
        {
            get { throw new NotImplementedException(); }
            set { lblModel.Text = value; }
        }

        public string EquipmentFunction
        {
            get { throw new NotImplementedException(); }
            set { lblEquipmentFunction.Text = value; }
        }

        public string EquipmentAssignedTo
        {
            get { throw new NotImplementedException(); }
            set { lblAssignedTo.Text = value; }
        }

        public string EquipmentRegisteredState
        {
            get { throw new NotImplementedException(); }
            set { lblRegisteredState.Text = value; }
        }

        public bool? EquipmentAttachPanelBoss
        {
            get { throw new NotImplementedException(); }
            set { lblAttachPanelBoss.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachPileDriver
        {
            get { throw new NotImplementedException(); }
            set { lblAttachPileDriver.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachSlipSheet
        {
            get { throw new NotImplementedException(); }
            set { lblAttachSlipSheet.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachTieClamp
        {
            get { throw new NotImplementedException(); }
            set { lblAttachTieClamp.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachTieInserter
        {
            get { throw new NotImplementedException(); }
            set { lblAttachTieInserter.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachTieTamper
        {
            get { throw new NotImplementedException(); }
            set { lblAttachTieTamper.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        public bool? EquipmentAttachUnderCutter
        {
            get { throw new NotImplementedException(); }
            set { lblAttachUnderCutter.Text = value.HasValue ? (value.Value ? "Yes" : "No") : string.Empty; }
        }

        #endregion

        #region [ Equipment Update ]

        #region [ White Light ]

        public bool IsWhiteLight
        {
            get { return chkWhiteLight.Checked; }
            set { chkWhiteLight.Checked = value; }
        }

        public string WhiteLightNotes
        {
            get { return txtWhiteLightNotes.Text; }
            set { txtWhiteLightNotes.Text = value; }
        }

        public IList<CS_EquipmentWhiteLight> WhiteLightHistoryGridDataSource
        {
            set
            {
                gvWhiteLightHistory.DataSource = value;
                gvWhiteLightHistory.DataBind();
            }
        }

        #endregion

        #region [ Equipment ]
        public string ActualEquipmentDivision
        {
            set
            {
                lblDivisionName.Text = value;
            }
        }

        public int DivisionID
        {
            get
            {
                return Convert.ToInt32(actDivision.SelectedValue);
            }
            set
            {
                actDivision.SetValue = value.ToString();
            }
        }

        public string DivisionName
        {
            set
            {
                actDivision.SelectedText = value;
            }
        }

        public string EquipmentStatus
        {
            get { return ddlEquipmentStatus.SelectedValue; }
            set { ddlEquipmentStatus.SelectedValue = value; }
        }

        public bool IsHeavyEquipment
        {
            get { return chkHeavyEquipment.Checked; }
            set { chkHeavyEquipment.Checked = value; }
        }

        public bool IsSeasonal
        {
            get { return chkSeasonal.Checked; }
            set { chkSeasonal.Checked = value; }
        }

        public bool DisplayInResourceAllocation
        {
            get { return chkDisplayInResourceAllocation.Checked; }
            set { chkDisplayInResourceAllocation.Checked = value; }
        }

        public bool ReplicateChangesToCombo
        {
            get
            {
                return chkReplicateCombo.Checked;
            }
            set
            {
                chkReplicateCombo.Checked = value;
            }
        }

        public bool ReplicateChangesToComboVisibility
        {
            get { return divReplicateCombo.Visible; }
            set { divReplicateCombo.Visible = value; }
        }
        #endregion

        #region [ Equipment Coverage ]

        public bool EquipmentCoverageFieldsRequired
        {
            set
            {
                dpEquipmentCoverageTO.EnableDisableMasketEditValidator = value;
                dpEquipmentCoverageFrom.EnableDisableMasketEditValidator = value;
                rfvCoverageTimeValidatorTO.Enabled = value;
                rfvCoverageTimeValidatorFrom.Enabled = value;
            }
        }

        public bool EquipmentCoverageStartFields
        {
            set
            {
                if (value == false)
                {
                    pnlEquipmentCoverageStart.Style.Add("display", "none");
                }
                else
                {
                    pnlEquipmentCoverageStart.Style.Add("display", "block");
                }
            }
        }

        public bool EnableDisablePanelCoverage
        {
            set
            {
                if (value)
                {
                    pnlCoverage.Style.Add("display", "none");
                    lblMsg.Visible = true;
                }
                else
                {
                    pnlCoverage.Style.Add("display", "block");
                    lblMsg.Visible = false;
                }
            }
        }

        public bool EquipmentCoverageEndFields
        {
            set
            {
                if (value == false)
                {
                    pnlEquipmentCoverageEnd.Style.Add("display", "none");
                }
                else
                {
                    pnlEquipmentCoverageEnd.Style.Add("display", "block");
                }
            }
        }

        public List<CS_EquipmentCoverage> CoverageHistoryGridDataSource
        {
            set
            {
                gvCoverHistory.DataSource = value;
                gvCoverHistory.DataBind();
            }
        }

        public DateTime? CoverageEndDate
        {
            get
            {
                if (dpEquipmentCoverageTO.Value.HasValue && !txtCoverageTimeTO.Text.Equals(string.Empty))
                {
                    return Convert.ToDateTime(
                        string.Format("{0} {1}", dpEquipmentCoverageTO.Value.Value.ToString("MM/dd/yyyy"), txtCoverageTimeTO.Text),
                        new System.Globalization.CultureInfo("en-US"));
                }
                return null;
            }
            set
            {
                dpEquipmentCoverageTO.Value = value;
                if (value.HasValue)
                    txtCoverageTimeTO.Text = value.Value.ToString("HH:mm");
                else
                    txtCoverageTimeTO.Text = string.Empty;
            }
        }

        public DateTime? CoverageStartDate
        {
            get
            {
                if (dpEquipmentCoverageFrom.Value.HasValue && !txtCoverageTimeFrom.Text.Equals(string.Empty))
                {
                    return Convert.ToDateTime(
                        string.Format("{0} {1}", dpEquipmentCoverageFrom.Value.Value.ToString("MM/dd/yyyy"), txtCoverageTimeFrom.Text),
                        new System.Globalization.CultureInfo("en-US"));
                }
                return null;
            }
            set
            {
                dpEquipmentCoverageFrom.Value = value;
                txtCoverageTimeFrom.Text = value.Value.ToString("HH:mm");
            }
        }

        public bool IsEquipmentCoverage
        {
            get
            {
                return chkCoverage.Checked;
            }
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

        public int? EquipmentCoverageDuration
        {
            get
            {
                if (txtCoverageDuration.Text != string.Empty)
                    return Convert.ToInt32(txtCoverageDuration.Text);

                return null;
            }
            set
            {
                txtCoverageDuration.Text = value.ToString();
            }
        }
        #endregion

        #region [ Equipment Down ]
        public List<CS_EquipmentDownHistory> DownHistoryGridDataSource
        {
            set
            {
                gvStatusHistory.DataSource = value;
                gvStatusHistory.DataBind();
            }
        }

        public DateTime? DownHistoryEndDate
        {
            get
            {
                if (dpEquipmentEndDate.Value.HasValue && !txtEquipmentStatusUpTime.Text.Equals(string.Empty))
                {
                    return Convert.ToDateTime(
                        string.Format("{0} {1}", dpEquipmentEndDate.Value.Value.ToString("MM/dd/yyyy"), txtEquipmentStatusUpTime.Text),
                        new System.Globalization.CultureInfo("en-US"));
                }

                return null;
            }
            set
            {
                txtEquipmentStatusUpTime.Text = value.Value.ToString("HH:mm");
                dpEquipmentEndDate.Value = value;
            }
        }

        public DateTime? DownHistoryStartDate
        {
            get
            {
                if (dpEquipmentStartDate.Value.HasValue && !txtEquipmentTime.Text.Equals(string.Empty))
                {
                    return Convert.ToDateTime(
                        string.Format("{0} {1}", dpEquipmentStartDate.Value.Value.ToString("MM/dd/yyyy"), txtEquipmentTime.Text),
                        new System.Globalization.CultureInfo("en-US"));
                }
                return null;
            }
            set
            {
                dpEquipmentStartDate.Value = value;
                txtEquipmentTime.Text = value.Value.ToString("HH:mm");
            }
        }

        public bool EquipmentStatusUpDateFieldsVisibility
        {
            set { pnlStatusUp.Visible = value; }
        }

        public bool EquipmentStatusDateFieldsVisibility
        {
            set { pnlStatusDown.Visible = value; }
        }

        public bool EquipmentStatusDateTimeRequired
        {
            set
            {
                dpEquipmentStartDate.IsValidEmpty = !value;
                dpEquipmentStartDate.ValidationGroup = value ? "EquipmentUpdate" : "";

                rfvTimeValidator.IsValidEmpty = !value;
                rfvTimeValidator.ValidationGroup = value ? "EquipmentUpdate" : "";
            }
        }

        public bool EquipmentStatusUpDateTimeRequired
        {
            set
            {
                dpEquipmentEndDate.IsValidEmpty = !value;
                dpEquipmentEndDate.ValidationGroup = value ? "EquipmentUpdate" : "";

                rfvEqquipmentUpTimeValidator.IsValidEmpty = !value;
                rfvEqquipmentUpTimeValidator.ValidationGroup = value ? "EquipmentUpdate" : "";
            }
        }

        public bool EquipmentStatusDurationRequired
        {
            set
            {
                rfvDurationEqDown.Enabled = value;
                cpvEDCoverageDuration.Enabled = value;
            }
        }

        public int? EquipmentDownDuration
        {
            get
            {
                if (txtDuration.Text != string.Empty)
                    return Convert.ToInt32(txtDuration.Text);

                return null;
            }
            set
            {
                if (null != value)
                    txtDuration.Text = value.ToString();
                else
                    txtDuration.Text = string.Empty;
            }
        }
        #endregion

        #endregion

        public bool ShowHideEquipmentDisplayRepeater
        {
            get
            {
                return pnlManagementEquipment.Visible;
            }
            set
            {
                pnlManagementEquipment.Visible = value;
            }
        }

        #endregion

        #endregion

        #region [ Methods ]

        private void EquipmentHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Common.Sort.EquipmentDisplaySortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitType:
                    control = _firstTierItem.FindControl("thUnitType") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentDisplaySortColumns.DivisionName:
                    control = _firstTierItem.FindControl("thDivision") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitNumber:
                    control = _firstTierItem.FindControl("thUnitNumber") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitDescription:
                    control = _firstTierItem.FindControl("thUnitDescription") as HtmlTableCell;
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

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void ClearEquipmentFields()
        {
            chkHeavyEquipment.Checked = false;

            ddlEquipmentStatus.SelectedIndex = 0;
            dpEquipmentStartDate.Value = null;
            txtEquipmentTime.Text = string.Empty;
            txtDuration.Text = string.Empty;
            dpEquipmentEndDate.Value = null;
            txtEquipmentStatusUpTime.Text = string.Empty;

            chkWhiteLight.Checked = false;

            chkCoverage.Checked = false;
            actDivision.SetValue = "0";
            actDivision.SelectedText = string.Empty;
            dpEquipmentCoverageFrom.Value = null;
            txtCoverageTimeFrom.Text = string.Empty;
            txtCoverageDuration.Text = string.Empty;
            dpEquipmentCoverageTO.Value = null;
            txtCoverageTimeTO.Text = string.Empty;

            EquipmentCoverageFieldsRequired = false;
        }

        private void KeepValidatorsState()
        {
            if (chkCoverage.Checked)
            {
                dpEquipmentCoverageFrom.EnableDisableMasketEditValidator = true;
                rfvCoverageTimeValidatorFrom.Enabled = true;
                actDivision.RequiredField = true;
                rfvCoverageDuration.Enabled = true;
                cpvCoverageDuration.Enabled = true;

                dpEquipmentCoverageTO.EnableDisableMasketEditValidator = false;
                rfvCoverageTimeValidatorTO.Enabled = false;
            }
            else if (hfIsCoverage.Value != string.Empty)
            {
                dpEquipmentCoverageFrom.EnableDisableMasketEditValidator = true;
                rfvCoverageTimeValidatorFrom.Enabled = true;
                actDivision.RequiredField = true;
                rfvCoverageDuration.Enabled = true;
                cpvCoverageDuration.Enabled = true;

                dpEquipmentCoverageTO.EnableDisableMasketEditValidator = true;
                rfvCoverageTimeValidatorTO.Enabled = true;
            }
            else
            {
                dpEquipmentCoverageFrom.EnableDisableMasketEditValidator = false;
                rfvCoverageTimeValidatorFrom.Enabled = false;
                actDivision.RequiredField = false;
                rfvCoverageDuration.Enabled = false;
                cpvCoverageDuration.Enabled = false;

                dpEquipmentCoverageTO.EnableDisableMasketEditValidator = false;
                rfvCoverageTimeValidatorTO.Enabled = false;
            }

            Page.Validate();
        }

        public bool IsEquipmentAssignedToJob
        {
            get
            {
                if (null == ViewState["IsEquipmentAssignedToJob"])
                    return false;

                return bool.Parse(ViewState["IsEquipmentAssignedToJob"].ToString());
            }
            set
            {
                ViewState["IsEquipmentAssignedToJob"] = value;

                if (value)
                {
                    btnSave.OnClientClick = "return ValidateJobAssigment();";
                }
            }
        }

        private List<int> SelectedCheckBoxes(bool heavyEquipment, bool displayInResource)
        {
            IList<int> selectedItems = new List<int>();

            for (int i = 0; i < rptEquipmentType.Items.Count; i++)
            {
                Repeater rptDivision = rptEquipmentType.Items[i].FindControl("rptDivision") as Repeater;

                for (int j = 0; j < rptDivision.Items.Count; j++)
                {
                    Repeater rptEquipments = rptDivision.Items[j].FindControl("rptEquipments") as Repeater;

                    if (null != rptEquipments)
                    {
                        for (int k = 0; k < rptEquipments.Items.Count; k++)
                        {
                            CheckBox chk = new CheckBox();

                            if (heavyEquipment)
                                chk = rptEquipments.Items[k].FindControl("chkEquipmentHeavyEquipment") as CheckBox;
                            else
                                chk = rptEquipments.Items[k].FindControl("chkResourceAllocation") as CheckBox;

                            if (null != chk)
                            {
                                if (chk.Checked)
                                {
                                    HiddenField hfEquipmentID = rptEquipments.Items[k].FindControl("hfEquipmentID") as HiddenField;

                                    selectedItems.Add(Convert.ToInt32(hfEquipmentID.Value));
                                }
                            }
                        }
                    }
                }
            }

            return selectedItems.ToList();
        }

        #endregion
    }
}
