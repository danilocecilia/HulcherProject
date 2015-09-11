using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.DataContext;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using System.Web.UI.HtmlControls;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class QuickReference : System.Web.UI.Page, IQuickReferenceView
    {
        #region [ Attributes ]

        /// <summary>
        /// Quick Reference Presenter instance
        /// </summary>
        private QuickReferencePresenter _presenter;

        private RepeaterItem _firstTierItem;

        private RepeaterItem _secondTierItem;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new QuickReferencePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.ListAllEquipmentAdd();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex != 0)
                _presenter.ListFilteredEquipmentAdd();
            else
                _presenter.ListAllEquipmentAdd();
        }

        protected void rptCombo_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptEquipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _secondTierItem = e.Item;
                _presenter.loadSecondTierItem();
            }
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex != 0)
                _presenter.ListFilteredEquipmentAdd();
            else
                _presenter.ListAllEquipmentAdd();
        }

        #endregion

        #region [ Properties ]

        #region Filter

        public Globals.ResourceAllocation.EquipmentFilters EquipmentFilter
        {
            get { return (Globals.ResourceAllocation.EquipmentFilters)int.Parse(cbFilter.SelectedValue); }
        }

        public string EquipmentFilterValue
        {
            get { return txtFilterValue.Text; }
        }

        #endregion

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #region [ EquipmentInfoData ]

        public List<CS_View_EquipmentInfo> EquipmentInfoListData
        {
            get
            {
                if (null == ViewState["EquipmentInfoListData"])
                    return new List<CS_View_EquipmentInfo>();

                return ViewState["EquipmentInfoListData"] as List<CS_View_EquipmentInfo>;
            }
            set
            {
                ViewState["EquipmentInfoListData"] = value;
            }
        }

        #endregion

        #region [ Header ]

        private void EquipmentHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Common.Sort.EquipmentSortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                    control = _firstTierItem.FindControl("thDivisionName") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                    control = _firstTierItem.FindControl("thDivisionState") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                    control = _firstTierItem.FindControl("thComboName") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                    control = _firstTierItem.FindControl("thUnitNumber") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                    control = _firstTierItem.FindControl("thDescriptor") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Status:
                    control = _firstTierItem.FindControl("thStatus") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                    control = _firstTierItem.FindControl("thJobLocation") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Type:
                    control = _firstTierItem.FindControl("thType") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                    control = _firstTierItem.FindControl("thOperationStatus") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                    control = _firstTierItem.FindControl("thJobNumber") as HtmlTableCell;
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

        #region [ First Tier (Combos and Solo Equip's) ]

        #region [ Item and Data ]

        private RepeaterItem FirstTierRepeaterItem
        {
            get
            {
                return _firstTierItem;
            }
            set
            {
                _firstTierItem = value;
            }
        }

        public CS_View_EquipmentInfo FirstTierDataItem
        {
            get
            {
                return _firstTierItem.DataItem as CS_View_EquipmentInfo;
            }
        }

        public IList<CS_View_EquipmentInfo> FirstTierDataSource
        {
            get
            {
                return new List<CS_View_EquipmentInfo>();
            }
            set
            {
                ShowHideControls(value.Count.Equals(0));
                rptCombo.DataSource = value;
                rptCombo.DataBind();
            }
        }

        public void ShowHideControls(bool show)
        {
            if (show)
            {
                pnlNoRows.Visible = true;
                rptCombo.Visible = false;
            }
            else
            {
                pnlNoRows.Visible = false;
                rptCombo.Visible = true;
            }
        }

        #endregion

        #region [ Repeater Fields ]

        public string FirstTierItemDivisionName
        {
            set
            {
                Label lblDivisionName = _firstTierItem.FindControl("lblDivisionName") as Label;
                lblDivisionName.Text = value;
            }
        }

        public string FirstTierItemDivisionState
        {
            set
            {
                Label lblDivisionState = _firstTierItem.FindControl("lblDivisionState") as Label;
                lblDivisionState.Text = value;
            }
        }

        public string FirstTierItemComboName
        {
            set
            {
                Label lblComboName = _firstTierItem.FindControl("lblComboName") as Label;
                lblComboName.Text = value;
            }
        }

        public string FirstTierItemUnitNumber
        {
            set
            {
                Label lblUnitNumber = _firstTierItem.FindControl("lblUnitNumber") as Label;
                lblUnitNumber.Text = value;
            }
        }

        public string FirstTierItemDescriptor
        {
            set
            {
                Label lblDescriptor = _firstTierItem.FindControl("lblDescriptor") as Label;
                lblDescriptor.Text = value;
            }
        }

        public string FirstTierItemStatus
        {
            set
            {
                Label lblStatus = _firstTierItem.FindControl("lblStatus") as Label;
                lblStatus.Text = value;
                lblStatus.Font.Bold = true;

                if (value == "Assigned")
                    lblStatus.ForeColor = System.Drawing.Color.DarkBlue;
                else if (value == "Available")
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                else if (value == "Unavailable")
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                else if (value == "Transfer Available")
                    lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        public string FirstTierItemJobLocation
        {
            set
            {
                Label lblJobLocation = _firstTierItem.FindControl("lblJobLocation") as Label;
                lblJobLocation.Text = value;
            }
        }

        public string FirstTierItemLastCallEntryText
        {
            set
            {
                HyperLink hlLastCallEntry = _firstTierItem.FindControl("hlLastCallEntry") as HyperLink;
                hlLastCallEntry.Text = value;
            }
        }

        public int[] FirstTierItemLastCallEntryID
        {
            set
            {
                HyperLink hlLastCallEntry = _firstTierItem.FindControl("hlLastCallEntry") as HyperLink;
                hlLastCallEntry.NavigateUrl = string.Format("javascript:OpenCallEntry({0}, {1})", value[0], value[1]);
            }
        }

        public string FirstTierItemOperationStatus
        {
            set
            {
                Label lblOperationStatus = _firstTierItem.FindControl("lblOperationStatus") as Label;
                lblOperationStatus.Text = value;
            }
        }

        public string FirstTierItemJobNumberText
        {
            set
            {
                LinkButton lbJob = _firstTierItem.FindControl("lbJob") as LinkButton;
                lbJob.Text = value;
            }
        }

        public int FirstTierItemJobNumberID
        {
            set
            {
                WebUtil util = new WebUtil();
                string queryString = util.BuildQueryStringForQuickReference(new Dictionary<string, string> { { "JobId", value.ToString() } });
                string script = util.BuildNewWindowClientScript("/JobRecord.aspx", queryString, "", 870, 600, true, true, false);
                LinkButton lbJob = _firstTierItem.FindControl("lbJob") as LinkButton;
                lbJob.OnClientClick = script;
            }
        }

        public int? FirstTierItemComboID
        {
            set
            {
                HtmlGenericControl divExpand = _firstTierItem.FindControl("divExpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    if (value.HasValue && value.Value > 0)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','" + value.Value.ToString() + "');");
                        divExpand.Visible = true;

                        string[] expandedJobs = hfExpandedCombos.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (expandedJobs.Contains(value.ToString()))
                            divExpand.Attributes["class"] = "Collapse";
                    }
                    else
                        divExpand.Visible = false;
                }
            }

        }

        #endregion

        #endregion

        #region [ Second Tier (Equipments) ]

        #region [ Item and Data ]

        /// <summary>
        /// Second Tier (Equipment) Item
        /// </summary>
        private RepeaterItem SecondTierRepeaterItem
        {
            get
            {
                return _secondTierItem;
            }
            set
            {
                _secondTierItem = value;
            }
        }

        public CS_View_EquipmentInfo SecondTierDataItem
        {
            get
            {
                return _secondTierItem.DataItem as CS_View_EquipmentInfo;
            }
        }

        public IList<CS_View_EquipmentInfo> SecondTierDataSource
        {
            get
            {
                return new List<CS_View_EquipmentInfo>();
            }
            set
            {
                Repeater rptEquipment = _firstTierItem.FindControl("rptEquipments") as Repeater;

                rptEquipment.DataSource = value;
                rptEquipment.DataBind();
            }
        }

        #endregion

        #region [ Repeater Fields ]

        public string SecondTierItemDivisionName
        {
            set
            {
                Label lblDivisionName = _secondTierItem.FindControl("lblDivisionName") as Label;
                lblDivisionName.Text = value;
            }
        }

        public string SecondTierItemDivisionState
        {
            set
            {
                Label lblDivisionState = _secondTierItem.FindControl("lblDivisionState") as Label;
                lblDivisionState.Text = value;
            }
        }

        public string SecondTierItemUnitNumber
        {
            set
            {
                Label lblUnitNumber = _secondTierItem.FindControl("lblUnitNumber") as Label;
                lblUnitNumber.Text = value;
            }
        }

        public string SecondTierItemDescriptor
        {
            set
            {
                Label lblDescriptor = _secondTierItem.FindControl("lblDescriptor") as Label;
                lblDescriptor.Text = value;
            }
        }

        public string SecondTierItemStatus
        {
            set
            {
                Label lblStatus = _secondTierItem.FindControl("lblStatus") as Label;
                lblStatus.Text = value;

                if (value == "Assigned")
                    lblStatus.ForeColor = System.Drawing.Color.DarkBlue;
                else if (value == "Available")
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                else if (value == "Unavailable")
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                else if (value == "Transfer Available")
                    lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        public string SecondTierItemJobLocation
        {
            set
            {
                Label lblJobLocation = _secondTierItem.FindControl("lblJobLocation") as Label;
                lblJobLocation.Text = value;
            }
        }

        public string SecondTierItemLastCallEntryText
        {
            set
            {
                HyperLink hlLastCallEntry = _secondTierItem.FindControl("hlLastCallEntry") as HyperLink;
                hlLastCallEntry.Text = value;
            }
        }

        public int[] SecondTierItemLastCallEntryID
        {
            set
            {
                HyperLink hlLastCallEntry = _secondTierItem.FindControl("hlLastCallEntry") as HyperLink;
                hlLastCallEntry.NavigateUrl = string.Format("javascript:OpenCallEntry({0}, {1})", value[0], value[1]);
            }
        }

        public string SecondTierItemOperationStatus
        {
            set
            {
                Label lblOperationStatus = _secondTierItem.FindControl("lblOperationStatus") as Label;
                lblOperationStatus.Text = value;
            }
        }

        public string SecondTierItemJobNumberText
        {
            set
            {
                LinkButton lbJob = _secondTierItem.FindControl("lbJob") as LinkButton;
                lbJob.Text = value;
            }
        }

        public int SecondTierItemJobNumberID
        {
            set
            {
                WebUtil util = new WebUtil();
                string queryString = util.BuildQueryStringForQuickReference(new Dictionary<string, string> { { "JobId", value.ToString() } });
                string script = util.BuildNewWindowClientScript("/JobRecord.aspx", queryString, "", 870, 600, true, true, false);
                LinkButton lbJob = _secondTierItem.FindControl("lbJob") as LinkButton;
                lbJob.OnClientClick = script;
            }
        }

        public string SecondTierItemCssClass
        {
            set
            {
                HtmlTableRow trResource = _secondTierItem.FindControl("trEquipment") as HtmlTableRow;
                if (null != trResource)
                {
                    trResource.Attributes["class"] = "Child " + value.ToString();
                    trResource.Attributes.Add("oncontextmenu", "return false;");
                    if (!hfExpandedCombos.Value.Contains(";" + value))
                        trResource.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        trResource.Style.Add(HtmlTextWriterStyle.Display, "");

                    HtmlGenericControl divExpand = _secondTierItem.FindControl("divExpand") as HtmlGenericControl;
                    if (null != divExpand) divExpand.Visible = false;
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

        public Globals.Common.Sort.EquipmentSortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.EquipmentSortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Common.Sort.EquipmentSortColumns)result;
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
    }
}
