using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using System.Web.UI.HtmlControls;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class RegionalMaintenance : System.Web.UI.Page, IRegionalMaintenanceView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Presenter Class
        /// </summary>
        private RegionalMaintenancePresenter _presenter;

        private int divisionRowDivisionID;
        private int divisionRowRegionID;
        private int? comboRowRegionID;
        private int comboRowDivisionID;
        private int? comboRowID;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new RegionalMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        protected void btnFindRegionMaintenance_Click(object sender, EventArgs e)
        {
            hfExpandedRegions.Value = string.Empty;
            hfExpandedDivisions.Value = string.Empty;
            hfExpandedCombos.Value = string.Empty;

            _presenter.Find();
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.BindRegion();
        }

        public void btnSaveContinue_Click(object sender, EventArgs e)
        {
            _presenter.SaveContinue();
        }

        public void btnSaveClose_Click(object sender, EventArgs e)
        {
            _presenter.SaveClose();
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.Find();
            ShowHideRegionalDisplay = false;
        }

        #region [ Repeater ]

        #region [ Region ]

        protected void rptRegion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RegionRepeaterItem = e.Item;
                _presenter.CreateRegionRow();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                RegionRepeaterItem = e.Item;
                SetGridHeaderCssClass();
            }
        }

        protected void rptRegion_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditRegion")
            {
                RegionID = int.Parse(e.CommandArgument.ToString());
                _presenter.LoadEditMode();
            }
        }

        #endregion

        #region [ Division ]

        protected void rptDivision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DivisionRepeaterItem = e.Item;
                _presenter.CreateDivisionRow();
            }
        }

        #endregion

        #region [ Employee ]

        protected void rptEmployee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EmployeeRepeaterItem = e.Item;
                _presenter.CreateEmployeeRow();
            }
        }

        #endregion

        #region [ Equipment ]

        protected void rptCombo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ComboRepeaterItem = e.Item;
                _presenter.CreateComboRow();
            }
        }

        protected void rptEquipment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EquipmentRepeaterItem = e.Item;
                _presenter.CreateEquipmentRow();
            }
        }

        #endregion

        #endregion

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        private void SetGridHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                    control = RegionRepeaterItem.FindControl("thRegion") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                    control = RegionRepeaterItem.FindControl("thDivision") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                    control = RegionRepeaterItem.FindControl("thEmployeeEquipment") as HtmlTableCell;
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

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public string Username
        {
            get { return Master.Username; }
            set { throw new NotImplementedException(); }
        }

        public int RegionID
        {
            get
            {
                if (null != ViewState["RegionID"])
                    return int.Parse(ViewState["RegionID"].ToString());

                return 0;
            }
            set
            {
                ViewState["RegionID"] = value;
            }
        }

        public bool EditMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                pnlCreation.Visible = value;
                pnlVisualization.Visible = !value;
            }
        }

        public Globals.RegionalMaintenance.FilterType FilterType
        {
            get { return (Globals.RegionalMaintenance.FilterType)Convert.ToInt32(ddlFilterRegionMaintenance.SelectedValue); }
        }

        public string FilterValue
        {
            get { return txtFilterValueRegionMaintenance.Text; }
        }

        public bool ShowHideRegionalDisplay
        {
            set
            {
                pnlCreation.Visible = value;
                pnlVisualization.Visible = !value;
            }
        }

        #endregion

        #region [ Dashboard ]

        #region [ Sort ]

        public string[] OrderBy
        {
            get
            {
                return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Globals.Common.Sort.RegionalMaintenanceSortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.RegionalMaintenanceSortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Common.Sort.RegionalMaintenanceSortColumns)result;
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

        #region [ Region ]

        public IList<CS_Region> RegionList
        {
            get
            {
                return ViewState["RegionList"] as IList<CS_Region>;
            }
            set
            {
                ViewState["RegionList"] = value;
            }
        }

        public IList<CS_Region> RegionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ShowHideControls(value.Count.Equals(0));
                RegionList = value;
                rptRegion.DataSource = value;
                rptRegion.DataBind();
            }
        }

        public void ShowHideControls(bool show)
        {
            if (!show)
            {
                pnlNoRowsRegion.Visible = false;
                rptRegion.Visible = true;
            }
            else
            {
                pnlNoRowsRegion.Visible = true;
                rptRegion.Visible = false;
            }
        }

        private RepeaterItem RegionRepeaterItem
        {
            get;
            set;
        }

        public CS_Region RegionDataItem
        {
            get
            {
                return RegionRepeaterItem.DataItem as CS_Region;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region [ Fields ]

        public bool RegionRowHasDivision
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlGenericControl divExpand = RegionRepeaterItem.FindControl("divexpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpandRegion('" + divExpand.ClientID + "','Reg" + RegionRowRegionID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }

        }

        public string RegionRowRegionName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblRegion = RegionRepeaterItem.FindControl("lblRegion") as Label;

                if (null != lblRegion)
                    lblRegion.Text = value;
            }
        }

        public int RegionRowRegionID
        {
            get
            {
                HiddenField hfRegionID = RegionRepeaterItem.FindControl("hfRegionID") as HiddenField;

                if (null != hfRegionID)
                {
                    return int.Parse(hfRegionID.Value);
                }

                return 0;
            }
            set
            {
                HiddenField hfRegionID = RegionRepeaterItem.FindControl("hfRegionID") as HiddenField;
                Button btnEdit = RegionRepeaterItem.FindControl("btnEdit") as Button;

                if (null != btnEdit && null != hfRegionID)
                {
                    btnEdit.CommandArgument = value.ToString();
                    hfRegionID.Value = value.ToString();
                }

                HtmlGenericControl divExpand = RegionRepeaterItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedRegions = hfExpandedRegions.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedRegions.Contains("Reg" + value.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                }
            }
        }

        #endregion

        #endregion

        #region [ Division ]

        public IList<CS_Division> RegionDivisionList
        {
            get
            {
                return ViewState["RegionDivisionList"] as IList<CS_Division>;
            }
            set
            {
                ViewState["RegionDivisionList"] = value;
            }
        }

        public IList<CS_Division> DivisionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater divisionRepeater = RegionRepeaterItem.FindControl("rptDivision") as Repeater;

                if (null != divisionRepeater)
                {
                    divisionRepeater.DataSource = value;
                    divisionRepeater.DataBind();
                }
            }
        }

        public CS_Division DivisionDataItem
        {
            get
            {
                return DivisionRepeaterItem.DataItem as CS_Division;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private RepeaterItem DivisionRepeaterItem
        {
            get;
            set;
        }

        #region [ Fields ]

        public bool DivisionRowHasEmployeeOrEquipment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlGenericControl divExpand = DivisionRepeaterItem.FindControl("divexpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpandDivision('" + divExpand.ClientID + "','Div" + DivisionRowDivisionID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }

        }

        public int DivisionRowRegionID
        {
            get
            {
                return divisionRowRegionID;
            }
            set
            {
                divisionRowRegionID = value;
                HtmlTableRow trResource = DivisionRepeaterItem.FindControl("trDivision") as HtmlTableRow;
                if (null != trResource)
                {
                    trResource.Attributes["class"] = "odd Division Reg" + value.ToString();
                    trResource.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedRegions.Value.Contains("Reg" + value.ToString() + ";"))
                        trResource.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trResource.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public int DivisionRowDivisionID
        {
            get { return divisionRowDivisionID; }
            set
            {
                divisionRowDivisionID = value;

                HtmlGenericControl divExpand = DivisionRepeaterItem.FindControl("divexpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedDivisions = hfExpandedDivisions.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] expandedRegions = hfExpandedRegions.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedDivisions.Contains("Div" + value.ToString()) &&
                        expandedRegions.Contains("Reg" + divisionRowRegionID.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                    else
                        hfExpandedDivisions.Value = hfExpandedDivisions.Value.Replace("Div" + value.ToString() + ";", "");
                }
            }
        }

        public string DivisionRowDivisionName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblDivision = DivisionRepeaterItem.FindControl("lblDivision") as Label;

                if (null != lblDivision)
                    lblDivision.Text = value;
            }
        }

        #endregion

        #endregion

        #region [ Employee ]

        public IList<CS_Employee> EmployeeList
        {
            get
            {
                return ViewState["EmployeeList"] as IList<CS_Employee>;
            }
            set
            {
                ViewState["EmployeeList"] = value;
            }
        }

        public IList<CS_Employee> EmployeeDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater employeeRepeater = DivisionRepeaterItem.FindControl("rptEmployee") as Repeater;

                if (null != employeeRepeater)
                {
                    employeeRepeater.DataSource = value;
                    employeeRepeater.DataBind();
                }
            }
        }

        private RepeaterItem EmployeeRepeaterItem
        {
            get;
            set;
        }

        public CS_Employee EmployeeDataItem
        {
            get
            {
                return EmployeeRepeaterItem.DataItem as CS_Employee;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region [ Fields ]

        public int EmployeeRowRegionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEmployee = EmployeeRepeaterItem.FindControl("trEmployee") as HtmlTableRow;
                if (null != trEmployee)
                {
                    trEmployee.Attributes["class"] = "Reserved Employee Reg" + value.ToString();
                    trEmployee.Attributes.Add("oncontextmenu", "return false;");
                    trEmployee.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
            }
        }

        public int EmployeeRowDivisionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEmployee = EmployeeRepeaterItem.FindControl("trEmployee") as HtmlTableRow;
                if (null != trEmployee)
                {
                    trEmployee.Attributes["class"] = trEmployee.Attributes["class"] + " Div" + value.ToString();
                    trEmployee.Attributes.Add("oncontextmenu", "return false;");
                    trEmployee.Style.Add(HtmlTextWriterStyle.Display, "none");
                    if (!hfExpandedDivisions.Value.Contains("Div" + value.ToString() + ";"))
                        trEmployee.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trEmployee.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public string EmployeeRowEmployeeName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblEmployee = EmployeeRepeaterItem.FindControl("lblEmployee") as Label;

                if (null != lblEmployee)
                    lblEmployee.Text = value;
            }
        }

        #endregion

        #endregion

        #region [ Equipment ]

        private RepeaterItem ComboRepeaterItem { get; set; }

        private RepeaterItem EquipmentRepeaterItem { get; set; }

        public IList<CS_View_EquipmentInfo> EquipmentList
        {
            get
            {
                return ViewState["EquipmentList"] as IList<CS_View_EquipmentInfo>;
            }
            set
            {
                ViewState["EquipmentList"] = value;
            }
        }

        public IList<CS_View_EquipmentInfo> ComboDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater rptCombo = DivisionRepeaterItem.FindControl("rptCombo") as Repeater;

                if (null != rptCombo)
                {
                    rptCombo.DataSource = value;
                    rptCombo.DataBind();
                }
            }
        }

        public IList<CS_View_EquipmentInfo> EquipmentDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater rptEquipment = ComboRepeaterItem.FindControl("rptEquipment") as Repeater;

                if (null != rptEquipment)
                {
                    rptEquipment.DataSource = value;
                    rptEquipment.DataBind();
                }
            }
        }

        public CS_View_EquipmentInfo ComboDataItem
        {
            get
            {
                return ComboRepeaterItem.DataItem as CS_View_EquipmentInfo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CS_View_EquipmentInfo EquipmentDataItem
        {
            get
            {
                return EquipmentRepeaterItem.DataItem as CS_View_EquipmentInfo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region [ Fields ]

        public int? ComboRowRegionID
        {
            get
            {
                return comboRowRegionID;
            }
            set
            {
                comboRowRegionID = value;
                HtmlTableRow trCombo = ComboRepeaterItem.FindControl("trCombo") as HtmlTableRow;
                if (null != trCombo)
                {
                    trCombo.Attributes["class"] = "Child Combo Reg" + value.ToString();
                    trCombo.Attributes.Add("oncontextmenu", "return false;");
                    trCombo.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
            }
        }

        public int ComboRowDivisionID
        {
            get
            {
                return comboRowDivisionID;
            }
            set
            {
                comboRowDivisionID = value;
                HtmlTableRow trCombo = ComboRepeaterItem.FindControl("trCombo") as HtmlTableRow;
                if (null != trCombo)
                {
                    trCombo.Attributes["class"] = trCombo.Attributes["class"] + " Div" + value.ToString();
                    trCombo.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedDivisions.Value.Contains("Div" + value.ToString() + ";"))
                        trCombo.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trCombo.Style.Add(HtmlTextWriterStyle.Display, "");
                }
            }
        }

        public string ComboRowName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblCombo = ComboRepeaterItem.FindControl("lblCombo") as Label;

                if (null != lblCombo)
                    lblCombo.Text = value;
            }
        }

        public int? ComboRowID
        {
            get { return comboRowID; }
            set
            {
                comboRowID = value;
                if (value.HasValue)
                {
                    HtmlGenericControl divExpand = ComboRepeaterItem.FindControl("divexpand") as HtmlGenericControl;
                    if (null != divExpand)
                    {
                        string[] expandedCombos = hfExpandedCombos.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] expandedDivisions = hfExpandedDivisions.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] expandedRegions = hfExpandedRegions.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (expandedCombos.Contains("EquipmentCombo" + value.ToString()) &&
                            expandedDivisions.Contains("Div" + comboRowDivisionID.ToString()) &&
                            expandedRegions.Contains("Reg" + comboRowRegionID.ToString()))
                            divExpand.Attributes["class"] = "Collapse";
                        else
                            hfExpandedCombos.Value = hfExpandedCombos.Value.Replace("EquipmentCombo" + value.ToString() + ";", "");
                    }
                }
            }
        }

        public bool ComboRowHasEquipments
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlGenericControl divExpand = ComboRepeaterItem.FindControl("divexpand") as HtmlGenericControl;

                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpandCombo('" + divExpand.ClientID + "','EquipmentCombo" + ComboRowID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }
        }

        public int? EquipmentRowRegionID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEquipment = EquipmentRepeaterItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] = "Child Equipment EquipmentCombo" + ComboRowID.ToString() + " Reg" + value.ToString();
                    trEquipment.Attributes.Add("oncontextmenu", "return false;");
                    trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
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
                HtmlTableRow trEquipment = EquipmentRepeaterItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] = trEquipment.Attributes["class"] + " Div" + value.ToString();
                    trEquipment.Attributes.Add("oncontextmenu", "return false;");
                    trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
            }
        }

        public int? EquipmentRowComboID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trEquipment = EquipmentRepeaterItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trEquipment)
                {
                    trEquipment.Attributes["class"] = trEquipment.Attributes["class"] + " Div" + value.ToString();
                    trEquipment.Attributes.Add("oncontextmenu", "return false;");
                    if (hfExpandedCombos.Value.Contains("EquipmentCombo" + value.ToString() + ";"))
                        trEquipment.Style.Add(HtmlTextWriterStyle.Display, "");
                    else
                        trEquipment.Style.Add(HtmlTextWriterStyle.Display, "none");
                }
            }
        }

        public string EquipmentRowName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label lblEquipment = EquipmentRepeaterItem.FindControl("lblEquipment") as Label;

                if (null != lblEquipment)
                    lblEquipment.Text = value;
            }
        }

        #endregion

        #endregion

        #region [ CRUD ]

        public IList<CS_Division> DivisionList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlDivision.DataSource = value;
                ddlDivision.DataBind();
            }
        }

        public IList<int> SelectedDivisions
        {
            get
            {
                List<int> selectedDivisions = new List<int>();

                for (int i = 0; i < ddlDivision.SelectedValues.Count; i++)
                {
                    selectedDivisions.Add(int.Parse(ddlDivision.SelectedValues[i]));
                }

                return selectedDivisions;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    ddlDivision.Items.FindByValue(value[i].ToString()).Selected = true;
                }
            }
        }

        public int? selectedRVP
        {
            get
            {
                if (cbRegionalRVP.SelectedValue != "0")
                    return int.Parse(cbRegionalRVP.SelectedValue);
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    cbRegionalRVP.SelectedValue = value.ToString();
            }
        }

        public IList<CS_Employee> RVPList
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                cbRegionalRVP.DataSource = value;
                cbRegionalRVP.DataBind();

                cbRegionalRVP.Items.Insert(0, new ListItem("- Select One -", "0"));
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
