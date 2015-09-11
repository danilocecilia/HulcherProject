using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Web.UserControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ResourceAllocation : System.Web.UI.Page, IResourceAllocationView
    {
        #region [ Attributes ]

        private ResourceAllocationPresenter _presenter;

        #endregion

        #region [ Private Properties ]

        private string[] OrderBy
        {
            get { return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); }
        }

        private RepeaterItem EquipmentComboRepeaterItem { get; set; }
        private RepeaterItem EquipmentsAddRepeaterItem { get; set; }

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ResourceAllocationPresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Page ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
                PrepareCallEntryButton();
            }

            this.Title = PageTitle;
        }

        #endregion

        #region [ Resource Conversion ]

        protected void gvReserveList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetColorOnRow(e.Row);
            }
        }

        #endregion

        #region [ Add Resource - Equipment ]

        protected void btnFindEquipmentAdd_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEquipmentGridAdd();
            ScriptManager.GetCurrent(this).SetFocus(btnFindEquipmentAdd);
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.RebindEquipmentGridAdd();
            ScriptManager.GetCurrent(this).SetFocus(btnFindEquipmentAdd);
        }

        protected void rptEquipmentsAdd_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EquipmentsAddRepeaterItem = e.Item;
                _presenter.FillEquipmentGridAddRow();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                EquipmentsAddRepeaterItem = e.Item;
                EquipmentsAddHeaderCssClass();
            }
        }

        protected void rptEquipmentCombo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EquipmentComboRepeaterItem = e.Item;
                _presenter.FillEquipmentGridAddRowCombo();
            }
        }

        protected void btnAddShoppingCartEquipmentAdd_Click(object sender, EventArgs e)
        {
            _presenter.AddEquipmentToShoppingCart();

            upGridShopCart.Update();
            btnAdd.Enabled = true;

            ScriptManager.GetCurrent(this).SetFocus(btnAddShoppingCartEquipmentAdd);
        }

        #endregion

        #region [ Add Resource - Employee ]

        protected void btnFindEmployeeAdd_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEmployeeGridAdd();
            ScriptManager.GetCurrent(this).SetFocus(btnFindEmployeeAdd);
        }

        // TODO: Change this method to fill properties instead of using page method
        protected void gvEmployeesAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                if (dr["Assigned"].ToString() == "Assigned")
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
                else if (dr["Assigned"].ToString() == "Available")
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                else if (dr["Assigned"].ToString() == "Unavailable")
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;

                if (dr["JobId"].ToString() == Globals.GeneralLog.ID.ToString())
                {
                    ((HyperLink)e.Row.FindControl("lnkJobEmployeeAdd")).Enabled = false;
                }
                else
                {
                    ((HyperLink)e.Row.FindControl("lnkJobEmployeeAdd")).NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", dr["JobId"].ToString());
                }

                CheckBox chkEmployeeAdd = e.Row.Cells[8].FindControl("chkEmployeeAdd") as CheckBox;
                if (null != chkEmployeeAdd)
                {
                    if (Convert.ToBoolean(dr["SelectAvailable"]) == false)
                        chkEmployeeAdd.Checked = true;

                    if (dr["Assigned"].ToString() == "Assigned")
                        chkEmployeeAdd.Visible = false;

                    if (dr["Assigned"].ToString() == "Unavailable")
                        chkEmployeeAdd.Enabled = false;
                }
            }
        }

        protected void btnAddShoppingCartEmployeeAdd_Click(object sender, EventArgs e)
        {
            _presenter.AddEmployeeToShoppingCart();

            upGridShopCart.Update();
            btnAdd.Enabled = true;

            ScriptManager.GetCurrent(this).SetFocus(btnAddShoppingCartEmployeeAdd);
        }

        #endregion

        #region [ Reserve Resource - Equipment ]

        protected void btnFindResourceReserve_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEquipmentReserve();
            ScriptManager.GetCurrent(this).SetFocus(btnFindResourceReserve);
        }

        protected void btnAddShoppingCartEquipmentReserve_Click(object sender, EventArgs e)
        {
            _presenter.ReserveEquipmentTypeToShoppingCart();

            upGridShopCart.Update();
            btnAdd.Enabled = true;

            ScriptManager.GetCurrent(this).SetFocus(btnAddShoppingCartEquipmentReserve);
        }

        // TODO: Change this method to fill properties instead of using page method
        protected void gvReserveEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_View_ReserveEquipment item = e.Row.DataItem as CS_View_ReserveEquipment;
                if (null != item)
                {
                    string entry = string.Format("{0};{1}", item.EquipmentTypeID, item.DivisionID);

                    LinkButton lnkAssigned = e.Row.Cells[6].FindControl("lnkAssigned") as LinkButton;
                    if (null != lnkAssigned)
                    {
                        lnkAssigned.CommandArgument = entry;
                    }
                    Button btnEquipmentReserve = e.Row.Cells[7].FindControl("btnEquipmentReserve") as Button;
                    if (null != btnEquipmentReserve)
                    {
                        btnEquipmentReserve.CommandArgument = entry;
                    }

                    int reserveCount = 0;
                    bool exists = ReserveEquipmentLocalCount.TryGetValue(entry, out reserveCount);
                    if (!exists)
                        if (item.Reserve.HasValue)
                            reserveCount = item.Reserve.Value;

                    e.Row.Cells[5].Text = reserveCount.ToString();
                }
            }
        }

        protected void lnkAssigned_OnCommand(object sender, CommandEventArgs e)
        {
            string[] splittedArgs = e.CommandArgument.ToString().Split(';');
            SelectedEquipmentType = Convert.ToInt32(splittedArgs[0]);
            SelectedDivision = Convert.ToInt32(splittedArgs[1]);
            _presenter.ListAllJobsByEquipmentTypeAndDivision();

            ScriptManager.GetCurrent(this).SetFocus(btnClosePopup);
        }

        // TODO: Change this method to fill properties instead of using page method
        protected void rptJobDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CS_Job job = e.Item.DataItem as CS_Job;
                Label lblJobNumber = e.Item.FindControl("lblJobNumber") as Label;
                HyperLink hlJobNumber = e.Item.FindControl("hlJobNumber") as HyperLink;

                if (null != job && null != hlJobNumber)
                {
                    if (job.ID == Globals.GeneralLog.ID)
                    {
                        lblJobNumber.Text = job.Number;
                        hlJobNumber.Visible = false;
                        lblJobNumber.Visible = true;
                    }
                    else
                    {
                        hlJobNumber.Text = job.PrefixedNumber;
                        hlJobNumber.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", job.ID);
                    }
                }

            }
        }

        #endregion

        #region [ Reserve Resource - Employee ]

        protected void btnFindEmployeeReserve_Click(object sender, EventArgs e)
        {
            _presenter.ListFilteredEmployeeReserve();
            ScriptManager.GetCurrent(this).SetFocus(btnFindEmployeeReserve);
        }

        protected void btnAddShoppingCartEmployeeReserve_Click(object sender, EventArgs e)
        {
            _presenter.ReserveEmployeeToShoppingCart();

            upGridShopCart.Update();
            btnAdd.Enabled = true;

            ScriptManager.GetCurrent(this).SetFocus(btnAddShoppingCartEmployeeReserve);
        }

        // TODO: Change this method to fill properties instead of using page method
        protected void gvReserveEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_View_EmployeeInfo item = e.Row.DataItem as CS_View_EmployeeInfo;
                if (null != item)
                {
                    if (item.Assigned.Equals("Assigned"))
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
                    else if (item.Assigned.Equals("Available"))
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;

                    if (item.JobId.HasValue)
                    {
                        HyperLink lnkJobEmployeeReserve = e.Row.FindControl("lnkJobEmployeeReserve") as HyperLink;
                        if (item.JobId.ToString() == Globals.GeneralLog.ID.ToString())
                        {
                            lnkJobEmployeeReserve.Enabled = false;
                        }
                        else
                        {
                            lnkJobEmployeeReserve.Text = item.PrefixedNumber;
                            lnkJobEmployeeReserve.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", item.JobId.ToString());
                        }
                    }
                }
            }
        }

        #endregion

        #region [ Transfer Shopping Cart ]

        // TODO: Change this method to fill properties instead of using page method
        protected void gvTransferShopCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                List<DataRow> rows = TransferShoppingCart.Select("1 = 1").ToList().FindAll
                    (delegate(DataRow row)
                {
                    return
                        row["ID"] != DBNull.Value
                        && (int.Parse(row["Type"].ToString()) != (int)Globals.ResourceAllocation.Type.ReserveEmployee && int.Parse(row["Type"].ToString()) != (int)Globals.ResourceAllocation.Type.ReserveEquipment);
                });

                if (null != rows && rows.Count > 0)
                {
                    CheckBox chkAllTransfer = e.Row.FindControl("chkAllTransfer") as CheckBox;
                    chkAllTransfer.Visible = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;

                if (dr != null)
                {
                    int typeValue = Int32.Parse(dr["Type"].ToString());
                    Globals.ResourceAllocation.Type type = (Globals.ResourceAllocation.Type)typeValue;

                    System.Drawing.Color color = System.Drawing.Color.Black;

                    switch (type)
                    {
                        case Globals.ResourceAllocation.Type.AddEquipment:
                            color = System.Drawing.Color.Green;
                            break;
                        case Globals.ResourceAllocation.Type.AddEmployee:
                            color = System.Drawing.Color.Red;
                            break;
                        case Globals.ResourceAllocation.Type.ReserveEquipment:
                            color = System.Drawing.Color.Blue;
                            break;
                        case Globals.ResourceAllocation.Type.ReserveEmployee:
                            color = System.Drawing.Color.Black;
                            break;
                    }

                    e.Row.Cells[3].ForeColor = color;

                    CheckBox chkTransfer = e.Row.FindControl("chkTransfer") as CheckBox;
                    if (dr["ID"] != DBNull.Value)
                    {
                        if (type != Globals.ResourceAllocation.Type.ReserveEmployee && type != Globals.ResourceAllocation.Type.ReserveEquipment)
                            chkTransfer.Visible = true;
                    }
                }
            }
        }

        #region [ Transfer Resources ]

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            if (SelectedRowsToTransfer.Count > 0)
                _presenter.TransferResourcesFromShoppingCart();
            else
                DisplayMessage("Please, select one or more resources to transfer.", false);
        }

        #endregion

        #endregion

        #region [ Shopping Cart ]

        // TODO: Change this method to fill properties instead of using page method
        protected void gvShopCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RangeValidator rvDuration = (RangeValidator)e.Row.FindControl("rvDuration");
                DataRowView dr = e.Row.DataItem as DataRowView;

                if (dr != null)
                {
                    int typeValue = Int32.Parse(dr["Type"].ToString());
                    Globals.ResourceAllocation.Type type = (Globals.ResourceAllocation.Type)typeValue;

                    System.Drawing.Color color = System.Drawing.Color.Black;

                    switch (type)
                    {
                        case Globals.ResourceAllocation.Type.AddEquipment:
                            color = System.Drawing.Color.Green;
                            break;
                        case Globals.ResourceAllocation.Type.AddEmployee:
                            color = System.Drawing.Color.Red;
                            break;
                        case Globals.ResourceAllocation.Type.ReserveEquipment:
                            color = System.Drawing.Color.Blue;
                            break;
                        case Globals.ResourceAllocation.Type.ReserveEmployee:
                            color = System.Drawing.Color.Black;
                            break;
                    }

                    e.Row.Cells[3].ForeColor = color;

                    CheckBox chkDeselect = e.Row.FindControl("chkDeselect") as CheckBox;

                    if (dr["ID"] != DBNull.Value)
                    {
                        chkDeselect.Visible = false;
                    }

                    rvDuration.ErrorMessage = "Resource " + dr["Name"].ToString() + " cannot exceed a 90 day reserve period.";
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            _presenter.ClearShoppingCart();

            updAddResource.Update();
            upReserveResource.Update();
            upGridShopCart.Update();

            _presenter.LoadPage();

            ScriptManager.GetCurrent(this).SetFocus(btnReset);
        }

        protected void btnDeselect_Click(object sender, EventArgs e)
        {
            _presenter.RemoveItemsFromShoppingCart();

            updAddResource.Update();
            upReserveResource.Update();
            upGridShopCart.Update();

            ScriptManager.GetCurrent(this).SetFocus(btnDeselect);
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                FillShoppingCartWithDetails();

                _presenter.SaveShoppingCart();

                if (SavedSuccessfully)
                    if (!string.IsNullOrEmpty(ParentControlId))
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "updateCallEntryScreen", string.Format("updateParentPage('{0}');", ParentControlId), true);
            }
        }

        #endregion

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public string UserName
        {
            get { return ((ContentPage)Master).Username; }
        }

        public string PageTitle
        {
            get
            {
                if (null != ViewState["PageTitle"])
                    return ViewState["PageTitle"].ToString();
                return string.Empty;
            }
            set { ViewState["PageTitle"] = value; }
        }

        public int JobID
        {
            get { return Convert.ToInt32(Request.QueryString.Get("JobID")); }
        }

        public string ParentControlId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ParentControlId"]))
                    return Request.QueryString["ParentControlId"];
                else
                    return null;
            }
        }

        public bool DisplayAddResource
        {
            get { return chAddResource.Visible; }
            set { chAddResource.Visible = value; }
        }

        public bool SavedSuccessfully { get; set; }

        #endregion

        #region [ Resource Conversion ]

        public bool ResourceConversion
        {
            get
            {
                if (null != Request.QueryString["ResourceConversion"])
                    return bool.Parse(Request.QueryString["ResourceConversion"]);

                return false;
            }
        }

        public IList<CS_View_ReserveList> ReserveListDatasource
        {
            set
            {
                ViewState["ReserveListDatasource"] = value;
                gvReserveList.DataSource = value;
                gvReserveList.DataBind();
            }
            get
            {
                return (List<CS_View_ReserveList>)ViewState["ReserveListDatasource"];
            }
        }

        public bool ReservedEquipmentsOnly
        {
            get { return chkReserveEquipments.Checked; }
            set
            {
                if (value)
                    chkReserveEquipments.Visible = value;

                chkReserveEquipments.Checked = value;
            }
        }

        #endregion

        #region [ Add Resource - Equipment ]

        #region [ Grid and Filters ]

        public Globals.ResourceAllocation.EquipmentFilters? EquipmentFilterAdd
        {
            get
            {
                if (cbFilterEquipmentAdd.SelectedValue.Equals("0") || cbFilterEquipmentAdd.SelectedValue.Equals(string.Empty))
                    return null;
                return (Globals.ResourceAllocation.EquipmentFilters)int.Parse(cbFilterEquipmentAdd.SelectedValue);
            }
            set
            {
                if (value.HasValue)
                    cbFilterEquipmentAdd.SelectedValue = ((int)value.Value).ToString();
            }
        }

        public string EquipmentFilterValueAdd
        {
            get { return txtFilterValueEquipmentAdd.Text; }
            set { txtFilterValueEquipmentAdd.Text = value; }
        }

        public IList<CS_View_EquipmentInfo> EquipmentsAddGridDataSource
        {
            set
            {
                rptEquipmentsAdd.DataSource = value;
                rptEquipmentsAdd.DataBind();

                pnlNoRows.Visible = (value.Count.Equals(0));
                rptEquipmentsAdd.Visible = !(value.Count.Equals(0));
            }
        }

        public IList<CS_View_EquipmentInfo> EquipmentList
        {
            get
            {
                if (null == ViewState["EquipmentList"])
                    ViewState["EquipmentList"] = new List<CS_View_EquipmentInfo>();

                return ViewState["EquipmentList"] as IList<CS_View_EquipmentInfo>;
            }
            set
            {
                ViewState["EquipmentList"] = value;
            }
        }

        public Dictionary<string, bool> SelectedEquipmentAddList
        {
            get
            {
                if (null == ViewState["SelectedEquipmentList"])
                    ViewState["SelectedEquipmentList"] = new Dictionary<string, bool>();
                return ViewState["SelectedEquipmentList"] as Dictionary<string, bool>;
            }

            set { ViewState["SelectedEquipmentList"] = value; }
        }

        public IList<string> SelectedEquipmentsAdd
        {
            get
            {
                IList<string> selectedItems = new List<string>();

                for (int i = 0; i < rptEquipmentsAdd.Items.Count; i++)
                {
                    CheckBox chkEquipmentAdd = rptEquipmentsAdd.Items[i].FindControl("chkEquipmentAdd") as CheckBox;

                    if (null != chkEquipmentAdd)
                    {
                        Label lblEquipmentId = rptEquipmentsAdd.Items[i].FindControl("lblEquipmentId") as Label;
                        Label lblIsCombo = rptEquipmentsAdd.Items[i].FindControl("lblIsCombo") as Label;
                        Label lblComboId = rptEquipmentsAdd.Items[i].FindControl("lblComboId") as Label;
                        Label lblIsComboUnit = rptEquipmentsAdd.Items[i].FindControl("lblIsComboUnit") as Label;

                        if (chkEquipmentAdd.Checked && null != lblEquipmentId && null != lblIsCombo && null != lblComboId && null != lblIsComboUnit)
                            selectedItems.Add(string.Format("{0}:{1}:{2}:{3}", lblEquipmentId.Text, lblIsCombo.Text, lblComboId.Text, lblIsComboUnit.Text));
                    }

                    Repeater rptEquipmentCombo = rptEquipmentsAdd.Items[i].FindControl("rptEquipmentCombo") as Repeater;

                    for (int j = 0; j < rptEquipmentCombo.Items.Count; j++)
                    {
                        CheckBox chkEquipmentCombo = rptEquipmentCombo.Items[j].FindControl("chkEquipmentCombo") as CheckBox;

                        if (null != chkEquipmentCombo)
                        {
                            Label lblEquipmentId = rptEquipmentCombo.Items[j].FindControl("lblEquipmentId") as Label;
                            Label lblIsCombo = rptEquipmentCombo.Items[j].FindControl("lblIsCombo") as Label;
                            Label lblComboId = rptEquipmentCombo.Items[j].FindControl("lblComboId") as Label;
                            Label lblIsComboUnit = rptEquipmentCombo.Items[j].FindControl("lblIsComboUnit") as Label;

                            if (chkEquipmentCombo.Checked && null != lblEquipmentId && null != lblIsCombo && null != lblComboId && null != lblIsComboUnit)
                                selectedItems.Add(string.Format("{0}:{1}:{2}:{3}", lblEquipmentId.Text, lblIsCombo.Text, lblComboId.Text, lblIsComboUnit.Text));
                        }
                    }
                }

                return selectedItems;
            }
        }

        #endregion

        #region [ Equipment / Combo Row ]

        public CS_View_EquipmentInfo EquipmentRowDataItem
        {
            get { return EquipmentsAddRepeaterItem.DataItem as CS_View_EquipmentInfo; }
            set { throw new NotImplementedException(); }
        }

        public string EquipmentsAddDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = EquipmentsAddRepeaterItem.FindControl("lblDivision") as Label;
                lblDivision.Text = value;
            }
        }

        public string EquipmentsAddDivisionState
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivisionState = EquipmentsAddRepeaterItem.FindControl("lblDivisionState") as Label;
                lblDivisionState.Text = value;
            }
        }

        public string EquipmentsAddComboName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblComboName = EquipmentsAddRepeaterItem.FindControl("lblComboName") as Label;
                lblComboName.Text = value;
            }
        }

        public string EquipmentsAddUnitNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblUnitNumber = EquipmentsAddRepeaterItem.FindControl("lblUnitNumber") as Label;
                lblUnitNumber.Text = value;
            }
        }

        public string EquipmentsAddDescriptor
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDescriptor = EquipmentsAddRepeaterItem.FindControl("lblDescriptor") as Label;
                lblDescriptor.Text = value;
            }
        }

        public string EquipmentsAddStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatus = EquipmentsAddRepeaterItem.FindControl("lblStatus") as Label;
                lblStatus.Text = value;

                if (lblStatus.Text.Equals("Assigned"))
                    lblStatus.ForeColor = System.Drawing.Color.DarkBlue;
                else if (lblStatus.Text.Equals("Available"))
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                else if (lblStatus.Text.Equals("Unavailable"))
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                else if (lblStatus.Text.Equals("Transfer Available"))
                    lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        public string EquipmentsAddJobLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblJobLocation = EquipmentsAddRepeaterItem.FindControl("lblJobLocation") as Label;
                lblJobLocation.Text = value;
            }
        }

        public string EquipmentsAddOperationStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblOperationStatus = EquipmentsAddRepeaterItem.FindControl("lblOperationStatus") as Label;
                lblOperationStatus.Text = value;
            }
        }

        public string EquipmentsAddEquipmentId
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblEquipmentId = EquipmentsAddRepeaterItem.FindControl("lblEquipmentId") as Label;
                lblEquipmentId.Text = value;
            }
        }

        public string EquipmentsAddIsCombo
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsCombo = EquipmentsAddRepeaterItem.FindControl("lblIsCombo") as Label;
                lblIsCombo.Text = value;
            }
        }

        public string EquipmentsAddIsComboUnit
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsComboUnit = EquipmentsAddRepeaterItem.FindControl("lblIsComboUnit") as Label;
                lblIsComboUnit.Text = value;
            }
        }

        public bool EquipmentsAddWhiteLight
        {
            set
            {
                Label lblIsWhiteLight = EquipmentsAddRepeaterItem.FindControl("lblIsWhiteLight") as Label;
                lblIsWhiteLight.Text = value.ToString();
            }
        }

        public string EquipmentsAddJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = EquipmentsAddRepeaterItem.FindControl("hlJobNumber") as HyperLink;
                hlJobNumber.Text = value;
            }
        }

        public string EquipmentsAddType
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlType = EquipmentsAddRepeaterItem.FindControl("hlType") as HyperLink;
                hlType.Text = value;
            }
        }

        public int? EquipmentsAddComboID
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblComboId = EquipmentsAddRepeaterItem.FindControl("lblComboId") as Label;
                    lblComboId.Text = value.Value.ToString();
                }

                HtmlGenericControl divExpand = EquipmentsAddRepeaterItem.FindControl("divExpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedJobs = hfExpandedJobs.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedJobs.Contains(value.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                }

                if (null != divExpand)
                {
                    if (value.HasValue)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','" + EquipmentRowDataItem.ComboID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }

                HtmlTableRow trItem = EquipmentsAddRepeaterItem.FindControl("trItem") as HtmlTableRow;
                if (null != trItem)
                {
                    trItem.Attributes["class"] = "Father" + value.ToString();
                }
            }
        }

        public string EquipmentsAddJobNumberNavigateUrl
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = EquipmentsAddRepeaterItem.FindControl("hlJobNumber") as HyperLink;
                hlJobNumber.NavigateUrl = value;
            }
        }

        public string EquipmentsAddTypeNavigateUrl
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlType = EquipmentsAddRepeaterItem.FindControl("hlType") as HyperLink;
                hlType.NavigateUrl = value;
            }
        }

        public bool EquipmentsAddIsDivConf
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsDivConf = EquipmentsAddRepeaterItem.FindControl("lblIsDivConf") as Label;
                lblIsDivConf.Text = value.ToString();
            }
        }

        public bool EquipmentsAddPermitExpired
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblPermitExpired = EquipmentsAddRepeaterItem.FindControl("lblPermitExpired") as Label;
                lblPermitExpired.Text = value.ToString();
            }
        }

        public bool EquipmentsAddchkEquipmentAdd
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkEquipmentAdd = EquipmentsAddRepeaterItem.FindControl("chkEquipmentAdd") as CheckBox;
                Label lblStatus = EquipmentsAddRepeaterItem.FindControl("lblStatus") as Label;
                Label lblComboId = EquipmentsAddRepeaterItem.FindControl("lblComboId") as Label;
                Label lblOperationStatus = EquipmentsAddRepeaterItem.FindControl("lblOperationStatus") as Label;
                Label lblIsDivConf = EquipmentsAddRepeaterItem.FindControl("lblIsDivConf") as Label;
                Label lblPermitExpired = EquipmentsAddRepeaterItem.FindControl("lblPermitExpired") as Label;
                Label lblIsWhiteLight = EquipmentsAddRepeaterItem.FindControl("lblIsWhiteLight") as Label;

                if (null != chkEquipmentAdd)
                {
                    string onclickScript = string.Empty;

                    //if (lblStatus.Text == "Assigned")
                    //    onclickScript += " if (this.checked) { if (!confirm('This resource is already allocated to another job. Do you want to transfer the resource from the original job onto the new job?')) return false; } ";
                    if (lblStatus.Text == "Assigned")
                        chkEquipmentAdd.Visible = false;

                    if (lblOperationStatus.Text.Equals(Globals.EquipmentMaintenance.Status.Down.ToString()))
                        onclickScript += " if (this.checked) { alert('This resource is marked as Down and cannot be assigned to a job.'); return false; } ";
                    else if (bool.Parse(lblIsDivConf.Text))
                        onclickScript += " if (this.checked) { alert('This combo has equipments of different divisions and cannot be assigned to a job. .'); return false; } ";

                    else if (bool.Parse(lblIsWhiteLight.Text))
                        onclickScript += " if (this.checked) { alert('This resource is marked as White Light and cannot be assigned to a job.'); return false; } ";
                    else if (bool.Parse(lblPermitExpired.Text))
                        onclickScript += " if (this.checked) { if (!confirm('This resource permit is expired. Do you want to continue anyway?')) return false; } ";

                    if (!string.IsNullOrEmpty(lblComboId.Text))
                        onclickScript += " var checkedOk = this.checked; $('." + lblComboId.Text + " input:checkbox').each(function () { if (!this.disabled) this.checked = checkedOk; });";

                    chkEquipmentAdd.Attributes["onclick"] = onclickScript;
                    chkEquipmentAdd.Checked = value;
                    if (chkEquipmentAdd.Checked)
                        chkEquipmentAdd.Enabled = false;
                    else
                        chkEquipmentAdd.Enabled = true;
                }
            }
        }

        public List<CS_View_EquipmentInfo> EquipmentsComboGridDataSource
        {
            get { throw new NotImplementedException(); }
            set
            {
                Repeater rptEquipmentCombo = EquipmentsAddRepeaterItem.FindControl("rptEquipmentCombo") as Repeater;
                rptEquipmentCombo.DataSource = value;
                rptEquipmentCombo.DataBind();
            }
        }

        #endregion

        #region [ Equipment Inside Combo Row ]

        public CS_View_EquipmentInfo EquipmentComboDataItem
        {
            get { return EquipmentComboRepeaterItem.DataItem as CS_View_EquipmentInfo; }
            set { throw new NotImplementedException(); }
        }

        public string EquipmentsComboDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = EquipmentComboRepeaterItem.FindControl("lblDivision") as Label;
                lblDivision.Text = value;
            }
        }

        public string EquipmentsComboDivisionState
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivisionState = EquipmentComboRepeaterItem.FindControl("lblDivisionState") as Label;
                lblDivisionState.Text = value;
            }
        }

        public string EquipmentsComboComboName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblComboName = EquipmentComboRepeaterItem.FindControl("lblComboName") as Label;
                lblComboName.Text = value;
            }
        }

        public string EquipmentsComboUnitNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblUnitNumber = EquipmentComboRepeaterItem.FindControl("lblUnitNumber") as Label;
                lblUnitNumber.Text = value;
            }
        }

        public string EquipmentsComboDescriptor
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDescriptor = EquipmentComboRepeaterItem.FindControl("lblDescriptor") as Label;
                lblDescriptor.Text = value;
            }
        }

        public string EquipmentsComboStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatus = EquipmentComboRepeaterItem.FindControl("lblStatus") as Label;
                lblStatus.Text = value;

                if (lblStatus.Text.Equals("Assigned"))
                    lblStatus.ForeColor = System.Drawing.Color.DarkBlue;
                else if (lblStatus.Text.Equals("Available"))
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                else if (lblStatus.Text.Equals("Unavailable"))
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                else if (lblStatus.Text.Equals("Transfer Available"))
                    lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        public string EquipmentsComboJobLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblJobLocation = EquipmentComboRepeaterItem.FindControl("lblJobLocation") as Label;
                lblJobLocation.Text = value;
            }
        }

        public string EquipmentsComboOperationStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblOperationStatus = EquipmentComboRepeaterItem.FindControl("lblOperationStatus") as Label;
                lblOperationStatus.Text = value;
            }
        }

        public string EquipmentsComboEquipmentId
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblEquipmentId = EquipmentComboRepeaterItem.FindControl("lblEquipmentId") as Label;
                lblEquipmentId.Text = value;
            }
        }

        public string EquipmentsComboIsCombo
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsCombo = EquipmentComboRepeaterItem.FindControl("lblIsCombo") as Label;
                lblIsCombo.Text = value;
            }
        }

        public bool EquipmentsComboWhiteLight
        {
            set
            {
                Label lblIsWhiteLight = EquipmentComboRepeaterItem.FindControl("lblIsWhiteLight") as Label;
                lblIsWhiteLight.Text = value.ToString();
            }
        }

        public string EquipmentsComboIsComboUnit
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsComboUnit = EquipmentComboRepeaterItem.FindControl("lblIsComboUnit") as Label;
                lblIsComboUnit.Text = value;
            }
        }

        public string EquipmentsComboJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = EquipmentComboRepeaterItem.FindControl("hlJobNumber") as HyperLink;
                hlJobNumber.Text = value;
            }
        }

        public string EquipmentsComboType
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlType = EquipmentComboRepeaterItem.FindControl("hlType") as HyperLink;
                hlType.Text = value;
            }
        }

        public int? EquipmentsComboComboID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value.HasValue)
                {
                    Label lblComboId = EquipmentComboRepeaterItem.FindControl("lblComboId") as Label;
                    lblComboId.Text = value.Value.ToString();
                }

                string[] expandedJobs = hfExpandedJobs.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                HtmlTableRow trComboItem = EquipmentComboRepeaterItem.FindControl("trComboItem") as HtmlTableRow;
                if (null != trComboItem)
                {
                    trComboItem.Attributes["class"] = "Child " + value.ToString();
                    if (!expandedJobs.Contains(value.ToString()))
                        trComboItem.Style.Add(HtmlTextWriterStyle.Display, "none");

                    HtmlGenericControl divExpand = EquipmentComboRepeaterItem.FindControl("divExpand") as HtmlGenericControl;
                    if (null != divExpand) divExpand.Visible = false;
                }
            }
        }

        public string EquipmentsComboJobNumberNavigateUrl
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlJobNumber = EquipmentComboRepeaterItem.FindControl("hlJobNumber") as HyperLink;
                hlJobNumber.NavigateUrl = value;
            }
        }

        public string EquipmentsComboTypeNavigateUrl
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlType = EquipmentComboRepeaterItem.FindControl("hlType") as HyperLink;
                hlType.NavigateUrl = value;
            }
        }

        public bool EquipmentsComboIsDivConf
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblIsDivConf = EquipmentComboRepeaterItem.FindControl("lblIsDivConf") as Label;
                lblIsDivConf.Text = value.ToString();
            }
        }

        public bool EquipmentsComboPermitExpired
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblPermitExpired = EquipmentComboRepeaterItem.FindControl("lblPermitExpired") as Label;
                lblPermitExpired.Text = value.ToString();
            }
        }

        public bool EquipmentsCombochkEquipmentAdd
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkEquipmentCombo = EquipmentComboRepeaterItem.FindControl("chkEquipmentCombo") as CheckBox;
                Label lblStatus = EquipmentComboRepeaterItem.FindControl("lblStatus") as Label;
                Label lblComboId = EquipmentComboRepeaterItem.FindControl("lblComboId") as Label;
                Label lblOperationStatus = EquipmentComboRepeaterItem.FindControl("lblOperationStatus") as Label;
                Label lblIsDivConf = EquipmentComboRepeaterItem.FindControl("lblIsDivConf") as Label;
                Label lblPermitExpired = EquipmentComboRepeaterItem.FindControl("lblPermitExpired") as Label;
                Label lblIsWhiteLight = EquipmentComboRepeaterItem.FindControl("lblIsWhiteLight") as Label;

                if (null != chkEquipmentCombo)
                {
                    string onclickScript = string.Empty;
                    //if (lblStatus.Text == "Assigned")
                    //    onclickScript += " if (this.checked) { return confirm('This resource is already allocated to another job. Do you want to transfer the resource from the original job onto the new job?'); } ";
                    if (lblStatus.Text == "Assigned")
                        chkEquipmentCombo.Visible = false;

                    if (lblOperationStatus.Text.Equals(Globals.EquipmentMaintenance.Status.Down.ToString()))
                        onclickScript += " if (this.checked) { alert('This resource is marked as Down and cannot be assigned to a job.'); return false; } ";
                    else if (bool.Parse(lblIsDivConf.Text))
                        onclickScript += " if (this.checked) { alert('This combo has equipments of different divisions and cannot be assigned to a job. .'); return false; } ";

                    else if (bool.Parse(lblIsWhiteLight.Text))
                        onclickScript += " if (this.checked) { alert('This resource is marked as White Light and cannot be assigned to a job.'); return false; } ";
                    else if (bool.Parse(lblPermitExpired.Text))
                        onclickScript += " if (this.checked) { if (!confirm('This resource permit is expired. Do you want to continue anyway?')) return false; } ";
                    
                    


                    onclickScript += " if (!this.checked) { $('.Father" + lblComboId.Text + " input:checkbox').attr('checked', false); }";
                    chkEquipmentCombo.Attributes["onclick"] = onclickScript;

                    chkEquipmentCombo.Checked = value;
                    if (chkEquipmentCombo.Checked)
                        chkEquipmentCombo.Enabled = false;
                    else
                        chkEquipmentCombo.Enabled = true;
                }
            }
        }

        #endregion

        #region [ Sorting ]

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
            set { hfOrderBy.Value = string.Format("{0} {1}", Convert.ToInt32(value), Convert.ToInt32(SortDirection)); }
        }

        public Globals.Common.Sort.SortDirection SortDirection
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.SortDirection.Ascending;

                return (Globals.Common.Sort.SortDirection)Int32.Parse(OrderBy[1]);
            }
            set { hfOrderBy.Value = string.Format("{0} {1}", Convert.ToInt32(SortColumn), Convert.ToInt32(value)); }
        }

        #endregion

        #endregion

        #region [ Add Resource - Employee ]

        #region [ Grid and Filters ]

        public Globals.ResourceAllocation.EmployeeFilters? EmployeeFilterAdd
        {
            get
            {
                if (cbFilterEmployeeAdd.SelectedValue.Equals("0") || cbFilterEmployeeAdd.SelectedValue.Equals(string.Empty))
                    return null;
                return (Globals.ResourceAllocation.EmployeeFilters)int.Parse(cbFilterEmployeeAdd.SelectedValue);
            }
            set
            {
                if (value.HasValue)
                    cbFilterEmployeeAdd.SelectedValue = ((int)value.Value).ToString();
            }
        }

        public string EmployeeFilterValueAdd
        {
            get
            {
                return txtFilterValueEmployeeAdd.Text;
            }
            set
            {
                txtFilterValueEmployeeAdd.Text = value;
            }
        }

        public DataTable EmployeeListAdd
        {
            set
            {
                gvEmployeesAdd.DataSource = value;
                gvEmployeesAdd.DataBind();
            }
        }

        public DataTable EmployeeDataTable
        {
            get
            {
                if (null == ViewState["EmployeeDataSource"])
                {
                    DataTable dtEquipment = new DataTable();
                    dtEquipment.Columns.AddRange(new DataColumn[] { new DataColumn("EmployeeId"), new DataColumn("DivisionName"), new DataColumn("DivisionState"), new DataColumn("EmployeeName"),
                    new DataColumn("Position"), new DataColumn("Assigned"), new DataColumn("JobNumber"), new DataColumn("SelectAvailable"), new DataColumn("JobId"), new DataColumn("DivisionId") });

                    return dtEquipment;
                }
                return ViewState["EmployeeDataSource"] as DataTable;
            }
            set
            {
                ViewState["EmployeeDataSource"] = value;
            }
        }

        public Dictionary<string, bool> SelectedEmployeeAddList
        {
            get
            {
                if (null == ViewState["SelectedEmployeeList"])
                    ViewState["SelectedEmployeeList"] = new Dictionary<string, bool>();
                return ViewState["SelectedEmployeeList"] as Dictionary<string, bool>;
            }

            set { ViewState["SelectedEmployeeList"] = value; }
        }

        public IList<int> SelectedEmployeesAdd
        {
            get
            {
                IList<int> selectedItems = new List<int>();
                for (int i = 0; i < gvEmployeesAdd.Rows.Count; i++)
                {
                    CheckBox chkEmployeeAdd = gvEmployeesAdd.Rows[i].Cells[8].FindControl("chkEmployeeAdd") as CheckBox;
                    Label lblEmployeeId = gvEmployeesAdd.Rows[i].Cells[8].FindControl("lblEmployeeId") as Label;

                    if (null != chkEmployeeAdd && null != lblEmployeeId)
                    {
                        if (chkEmployeeAdd.Checked)
                            selectedItems.Add(Convert.ToInt32(lblEmployeeId.Text));
                    }
                }
                return selectedItems;
            }
        }

        #endregion

        #endregion

        #region [ Reserve Resource - Equipment ]

        #region [ Grid and Filters ]

        public IList<CS_EquipmentType> EquipmentTypeFilterSource
        {
            set
            {
                cbEquipType.DataSource = value;
                cbEquipType.DataTextField = "Name";
                cbEquipType.DataValueField = "ID";
                cbEquipType.DataBind();
                cbEquipType.Items.Insert(0, new ListItem("- Select One -", "Select One"));
                cbEquipType.Items.Insert(1, new ListItem("All", "1"));
            }
        }

        public int? EquipmentTypeId
        {
            get
            {
                if (cbEquipType.SelectedIndex == 1 || cbEquipType.SelectedIndex == 0)
                    return null;

                int equipTypeId;
                if (int.TryParse(cbEquipType.SelectedValue, out equipTypeId))
                    return equipTypeId;

                return null;
            }
        }

        public int? StateId
        {
            get
            {
                if (string.IsNullOrEmpty(actLocation.SelectedValue) || string.IsNullOrEmpty(actLocation.SelectedText))
                    return null;

                int statId;
                if (int.TryParse(actLocation.SelectedValue, out statId))
                    return statId;

                return null;
            }
        }

        public int? DivisionId
        {
            get
            {
                if (string.IsNullOrEmpty(actDivision.SelectedValue) || string.IsNullOrEmpty(actDivision.SelectedText))
                    return null;

                int divId;
                if (int.TryParse(actDivision.SelectedValue, out divId))
                    return divId;

                return null;
            }
        }

        public IList<CS_View_ReserveEquipment> ReserveEquipmentDataSource
        {
            get
            {
                if (null == ViewState["ReserveEquipmentDataSource"])
                    ViewState["ReserveEquipmentDataSource"] = new List<CS_View_ReserveEquipment>();
                return ViewState["ReserveEquipmentDataSource"] as IList<CS_View_ReserveEquipment>;
            }
            set { ViewState["ReserveEquipmentDataSource"] = value; }
        }

        public IList<int[]> SelectedEquipmentsReserve
        {
            get
            {
                IList<int[]> selectedItems = new List<int[]>();
                for (int i = 0; i < gvReserveEquipment.Rows.Count; i++)
                {
                    CheckBox chkEquipmentResereve = gvReserveEquipment.Rows[i].Cells[7].FindControl("chkEquipmentResereve") as CheckBox;
                    TextBox txtEquipmentReserveQuantity = gvReserveEquipment.Rows[i].Cells[7].FindControl("txtEquipmentReserveQuantity") as TextBox;
                    Label lblEquipmentTypeId = gvReserveEquipment.Rows[i].Cells[7].FindControl("lblEquipmentTypeId") as Label;
                    Label lblDivisonId = gvReserveEquipment.Rows[i].Cells[7].FindControl("lblDivisonId") as Label;

                    if (null != chkEquipmentResereve && null != lblEquipmentTypeId)
                    {
                        if (chkEquipmentResereve.Checked)
                            selectedItems.Add(
                                new int[] {
                                    Convert.ToInt32(lblEquipmentTypeId.Text),
                                    Convert.ToInt32(lblDivisonId.Text),
                                    Convert.ToInt32(txtEquipmentReserveQuantity.Text)
                                });
                    }
                }
                return selectedItems;
            }
        }

        #endregion

        #region [ Equipment Type Row ]

        public int SelectedEquipmentType { get; set; }

        public int SelectedDivision { get; set; }

        public IList<CS_Job> JobsRelatedToEquipmentType
        {
            set
            {
                rptJobDetails.DataSource = value;
                rptJobDetails.DataBind();
                pnlPopUp.Style["display"] = "inline";
                mdlPopUpExtender.Show();
            }
        }

        #endregion

        #endregion

        #region [ Reserve Resource - Employee ]

        #region [ Grid and Filters ]

        public Globals.ResourceAllocation.EmployeeFilters? EmployeeFilterReserve
        {
            get
            {
                if (string.IsNullOrEmpty(cbEmployee.SelectedValue) || cbEmployee.SelectedValue.Equals("0"))
                    return null;

                return (Globals.ResourceAllocation.EmployeeFilters)int.Parse(cbEmployee.SelectedValue);
            }
        }

        public string EmployeeFilterValueReserve
        {
            get { return txtFilterValueEmployeeReserve.Text; }
        }

        public IList<CS_View_EmployeeInfo> ReserveEmployeeDataSource
        {
            set
            {
                gvReserveEmployee.DataSource = value;
                gvReserveEmployee.DataBind();
            }
        }

        public Dictionary<string, int> ReserveEquipmentLocalCount
        {
            get
            {
                if (null == ViewState["ReserveEquipmentLocalCount"])
                    ViewState["ReserveEquipmentLocalCount"] = new Dictionary<string, int>();
                return (Dictionary<string, int>)ViewState["ReserveEquipmentLocalCount"];
            }
            set { ViewState["ReserveEquipmentLocalCount"] = value; }
        }

        public IList<int> SelectedEmployeesReserve
        {
            get
            {
                IList<int> selectedItems = new List<int>();
                for (int i = 0; i < gvReserveEmployee.Rows.Count; i++)
                {
                    CheckBox chkEmployeeReserve = gvReserveEmployee.Rows[i].Cells[8].FindControl("chkEmployeeReserve") as CheckBox;
                    Label lblEmployeeId = gvReserveEmployee.Rows[i].Cells[8].FindControl("lblEmployeeId") as Label;

                    if (null != chkEmployeeReserve && null != lblEmployeeId)
                    {
                        if (chkEmployeeReserve.Checked)
                        {
                            selectedItems.Add(Convert.ToInt32(lblEmployeeId.Text));
                            chkEmployeeReserve.Checked = false;
                        }
                    }
                }
                return selectedItems;
            }
        }

        #endregion

        #endregion

        #region [ Transfer Shopping Cart ]

        public DataTable TransferShoppingCart
        {
            get
            {
                if (null == ViewState["TransferShoppingCart"])
                    return null;
                return (DataTable)ViewState["TransferShoppingCart"];
            }
            set
            {
                ViewState["TransferShoppingCart"] = value;
                //FillShoppingCartWithDetails();

                gvTransferShopCart.DataSource = value;
                gvTransferShopCart.DataBind();

                if (gvTransferShopCart.Rows.Count > 0)
                    btnAdd.Enabled = true;
            }
        }

        public IList<int> SelectedRowsToTransfer
        {
            get
            {
                IList<int> returnList = new List<int>();
                for (int i = gvTransferShopCart.Rows.Count - 1; i >= 0; i--)
                {
                    CheckBox chkDeselect = gvTransferShopCart.Rows[i].FindControl("chkTransfer") as CheckBox;
                    if (null != chkDeselect)
                        if (chkDeselect.Checked)
                            returnList.Add(i);
                }
                return returnList;
            }
        }

        public IList<int> ResourceIdToTransfer
        {
            set
            {
                ViewState["ResourceIDList"] = value;

                if (value.Count > 0)
                {
                    StringBuilder idList = new StringBuilder();

                    for (int i = 0; i < value.Count; i++)
                    {
                        idList.Append(value[i].ToString());
                        idList.Append(";");
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ResourceTransfer", string.Format("window.open('/ResourceTransfer.aspx?JobId={0}&ResourceTransferId={1}', '', 'width=940, height=470, scrollbars=1, resizable=yes');", JobID, idList.ToString()), true);
                }
            }
        }

        #endregion

        #region [ Shopping Cart ]

        public DataTable ShoppingCart
        {
            get
            {
                if (null == ViewState["ShoppingCart"])
                    return null;
                return (DataTable)ViewState["ShoppingCart"];
            }
            set
            {
                ViewState["ShoppingCart"] = value;
                FillShoppingCartWithDetails();

                gvShopCart.DataSource = value;
                gvShopCart.DataBind();

                if (gvShopCart.Rows.Count > 0)
                    btnAdd.Enabled = true;

                ReplicationFields();
            }
        }

        public IList<int> SelectedRowsToRemove
        {
            get
            {
                IList<int> returnList = new List<int>();
                for (int i = gvShopCart.Rows.Count - 1; i >= 0; i--)
                {
                    CheckBox chkDeselect = gvShopCart.Rows[i].FindControl("chkDeselect") as CheckBox;
                    if (null != chkDeselect)
                        if (chkDeselect.Checked)
                            returnList.Add(i);
                }
                return returnList;
            }
        }

        public string Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        }

        public DateTime CallDate
        {
            get
            {
                if (dpCallDate.Value.HasValue)
                {
                    string dateString = string.Format("{0} {1}", dpCallDate.Value.Value.ToString("MM/dd/yyyy"), txtCallTime.Text);

                    return DateTime.Parse(dateString);
                }

                return DateTime.Now;
            }
            set
            {
                dpCallDate.Value = value.Date;
                txtCallTime.Text = value.ToString("HH:mm");
            }
        }

        #endregion

        #region [ SubContractor ]
        public string SubContractorInfo
        {
            get
            {
                return txtSubContractorInfo.Text;
            }
            set
            {
                txtSubContractorInfo.Text = value;
            }
        }

        public string FieldPO
        {
            get
            {
                return txtFieldPO.Text;
            }
            set
            {
                txtFieldPO.Text = value;
            }
        }

        public bool IsSubContractor
        {
            get
            {
                return chkSubConstractor.Checked ? true : false;
            }
            set
            {
                chkSubConstractor.Checked = value;
            }
        }
        #endregion

        #endregion

        #region [ Methods ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        private void PrepareCallEntryButton()
        {
            btnCallEntry.Enabled = true;
            btnCallEntry.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobID);
        }

        #endregion

        #region [ Resource Conversion ]

        private void SetColorOnRow(GridViewRow row)
        {
            if (row.Cells[3].Text != "Not Available" && row.Cells[3].Text != "0")
            {
                if (row.Cells[3].Text == "Available")
                    row.Cells[3].ForeColor = System.Drawing.Color.Green;
                else
                {
                    int reserved = int.Parse(row.Cells[2].Text);
                    int available = int.Parse(row.Cells[3].Text);

                    if (reserved > available)
                        row.Cells[3].ForeColor = System.Drawing.Color.FromArgb(210, 163, 0);
                    else
                        row.Cells[3].ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }

            row.Cells[3].Font.Bold = true;
        }

        public void SetPageResourceConversion()
        {
            chReserveList.Visible = true;
            chReserveResource.Visible = false;
        }

        #endregion

        #region [ Add Resource - Equipment ]

        private void EquipmentsAddHeaderCssClass()
        {
            HtmlTableCell control;

            switch (SortColumn)
            {
                case Globals.Common.Sort.EquipmentSortColumns.None:
                    control = new HtmlTableCell();
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                    control = EquipmentsAddRepeaterItem.FindControl("thDivisionName") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                    control = EquipmentsAddRepeaterItem.FindControl("thDivisionState") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                    control = EquipmentsAddRepeaterItem.FindControl("thComboName") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                    control = EquipmentsAddRepeaterItem.FindControl("thUnitNumber") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                    control = EquipmentsAddRepeaterItem.FindControl("thDescriptor") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Status:
                    control = EquipmentsAddRepeaterItem.FindControl("thStatus") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                    control = EquipmentsAddRepeaterItem.FindControl("thJobLocation") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Type:
                    control = EquipmentsAddRepeaterItem.FindControl("thType") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                    control = EquipmentsAddRepeaterItem.FindControl("thEquipmentStatus") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                    control = EquipmentsAddRepeaterItem.FindControl("thJobNumber") as HtmlTableCell;
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

        #region [ Reserve Resource - Equipment ]

        public void BindReserveEquipmentGrid()
        {
            gvReserveEquipment.DataSource = ReserveEquipmentDataSource;
            gvReserveEquipment.DataBind();
        }

        #endregion

        #region [ Shopping Cart ]

        private void FillShoppingCartWithDetails()
        {
            TextBox txtDuration = null;
            DatePicker dpDatePicker = null;
            TextBox txtInitialTime = null;
            DateTime startDateTime = DateTime.MinValue;

            for (int i = 0; i < gvShopCart.Rows.Count; i++)
            {
                if (i < ShoppingCart.Rows.Count)
                {
                    txtDuration = gvShopCart.Rows[i].Cells[3].FindControl("txtDuration") as TextBox;
                    dpDatePicker = gvShopCart.Rows[i].Cells[4].FindControl("dpDatePicker") as DatePicker;
                    txtInitialTime = gvShopCart.Rows[i].Cells[4].FindControl("txtInitialTime") as TextBox;

                    if (null != txtDuration && null != dpDatePicker && null != txtInitialTime)
                    {
                        if (!string.IsNullOrEmpty(txtDuration.Text))
                            ShoppingCart.Rows[i]["Duration"] = Convert.ToDecimal(txtDuration.Text);

                        if (!string.IsNullOrEmpty(txtInitialTime.Text) && null != dpDatePicker.Value)
                        {
                            startDateTime = Convert.ToDateTime(string.Format("{0} {1}", dpDatePicker.Value.Value.ToString("MM/dd/yyyy"), txtInitialTime.Text), new System.Globalization.CultureInfo("en-US"));
                            ShoppingCart.Rows[i]["StartDateTime"] = startDateTime;
                        }
                    }
                }
            }

            for (int i = 0; i < gvTransferShopCart.Rows.Count; i++)
            {
                if (i < TransferShoppingCart.Rows.Count)
                {
                    txtDuration = gvTransferShopCart.Rows[i].Cells[3].FindControl("txtDuration") as TextBox;
                    dpDatePicker = gvTransferShopCart.Rows[i].Cells[4].FindControl("dpDatePicker") as DatePicker;
                    txtInitialTime = gvTransferShopCart.Rows[i].Cells[4].FindControl("txtInitialTime") as TextBox;

                    if (null != txtDuration && null != dpDatePicker && null != txtInitialTime)
                    {
                        if (!string.IsNullOrEmpty(txtDuration.Text))
                            TransferShoppingCart.Rows[i]["Duration"] = Convert.ToDecimal(txtDuration.Text);

                        if (!string.IsNullOrEmpty(txtInitialTime.Text) && null != dpDatePicker.Value)
                        {
                            startDateTime = Convert.ToDateTime(string.Format("{0} {1}", dpDatePicker.Value.Value.ToString("MM/dd/yyyy"), txtInitialTime.Text), new System.Globalization.CultureInfo("en-US"));
                            TransferShoppingCart.Rows[i]["StartDateTime"] = startDateTime;
                        }
                    }
                }
            }
        }

        private void ReplicationFields()
        {
            if (gvShopCart.Rows.Count > 0)
            {
                // Find the first new row
                int firstRow = 0;
                for (int i = 0; i < gvShopCart.Rows.Count; i++)
                {
                    CheckBox chkDeselect = gvShopCart.Rows[i].Cells[5].FindControl("chkDeselect") as CheckBox;
                    if (chkDeselect.Visible)
                    {
                        firstRow = i;
                        break;
                    }
                }

                //Start function for Duration Fields

                TextBox txtDuration = (TextBox)gvShopCart.Rows[firstRow].Cells[3].FindControl("txtDuration");

                var script = new StringBuilder();
                //script.Append(" $(document).ready(function () {");
                script.AppendFormat("$('#{0}').bind('blur', function()", txtDuration.ClientID);
                script.Append("{");
                script.AppendFormat("var duration = $('#{0}').val().length;", txtDuration.ClientID);

                for (int i = firstRow + 1; i < gvShopCart.Rows.Count; i++)
                {
                    TextBox txt = (TextBox)gvShopCart.Rows[i].FindControl("txtDuration");

                    script.AppendFormat("var duration_{0} = $('#{1}').val().length;", txt.ClientID, txt.ClientID);
                    script.AppendFormat("if(duration > 0 && duration_{0} == 0)", txt.ClientID);
                    script.AppendFormat("$('#{0}').val($('#{1}').val());", txt.ClientID, txtDuration.ClientID);
                }

                script.Append("});");
                //script.Append("});");


                ScriptManager.RegisterStartupScript(this, GetType(), "duration", script.ToString(), true);


                //End 

                //Start function for the Initial Time

                TextBox txtInitialTime = (TextBox)gvShopCart.Rows[firstRow].FindControl("txtInitialTime");

                var script2 = new StringBuilder();
                //script2.Append(" $(document).ready(function () {");
                script2.AppendFormat("$('#{0}').bind('blur', function()", txtInitialTime.ClientID);
                script2.Append("{");
                script2.AppendFormat("var initialTime = $('#{0}').val().length;", txtInitialTime.ClientID);

                for (int i = firstRow + 1; i < gvShopCart.Rows.Count; i++)
                {
                    TextBox txt = (TextBox)gvShopCart.Rows[i].FindControl("txtInitialTime");

                    script2.AppendFormat("var initialTime_{0} = $('#{1}').val().length;", txt.ClientID, txt.ClientID);
                    script2.AppendFormat("if(initialTime > 0 && initialTime_{0} == 0)", txt.ClientID);
                    script2.AppendFormat("$('#{0}').val($('#{1}').val());", txt.ClientID, txtInitialTime.ClientID);
                }

                script2.Append("});");
                //script2.Append("});");


                ScriptManager.RegisterStartupScript(this, GetType(), "initialTime", script2.ToString(), true);

                //End



                //Start function for StartDatetime fields

                DatePicker txtStartDateTime = (DatePicker)gvShopCart.Rows[firstRow].FindControl("dpDatePicker");

                var script1 = new StringBuilder();
                //script1.Append(" $(document).ready(function () {");
                script1.AppendFormat("$('#{0}').datepicker('option',", txtStartDateTime.TextBoxClientID);
                script1.Append("{");
                script1.AppendFormat("onSelect: function(date)");
                script1.Append("{");

                script1.AppendFormat("var startDateTime = $('#{0}').val().length;", txtStartDateTime.TextBoxClientID);

                for (int i = firstRow + 1; i < gvShopCart.Rows.Count; i++)
                {
                    DatePicker txt = (DatePicker)gvShopCart.Rows[i].FindControl("dpDatePicker");

                    script1.AppendFormat("var startDateTime_{0} = $('#{1}').val().length;", txt.TextBoxClientID, txt.TextBoxClientID);
                    script1.AppendFormat("if(startDateTime > 0 && startDateTime_{0} == 0)", txt.TextBoxClientID);
                    script1.AppendFormat("$('#{0}').val($('#{1}').val());", txt.TextBoxClientID, txtStartDateTime.TextBoxClientID);
                }

                script1.Append("} });");
                //script1.Append("});");

                ScriptManager.RegisterStartupScript(this, GetType(), "startDateTime", script1.ToString(), true);
                txtStartDateTime.ReplicationScript = script1.ToString();
                //End

            }
        }

        #endregion

        #endregion



    }
}