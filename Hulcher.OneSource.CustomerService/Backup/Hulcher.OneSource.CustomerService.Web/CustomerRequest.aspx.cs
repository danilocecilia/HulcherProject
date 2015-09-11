using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI.HtmlControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class CustomerRequest : System.Web.UI.Page, ICustomerRequestView
    {
        #region [ Attributes ]

        private CustomerRequestPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new CustomerRequestPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.LoadPage();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.BindRequest();
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.BindRequest();
        }

        protected void rptRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RequestItemRepeaterItem = e.Item;
                _presenter.BindRequestRow();
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                RequestItemRepeaterItem = e.Item;
                SetGridHeaderCssClass();
            }
        }

        protected void rptRequest_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRequest")
            {
                RequestID = Convert.ToInt32(e.CommandArgument);
                _presenter.DeleteRequest();
            }
            else if (e.CommandName == "ResendRequest")
            {
                RequestID = Convert.ToInt32(e.CommandArgument);
                _presenter.ResendRequest();
            }
        }

        #endregion

        #region [ Properties ]

        #region [ Filter ]

        public Globals.CustomerMaintenance.RequestFilterType FilterType
        {
            get { return (Globals.CustomerMaintenance.RequestFilterType)Convert.ToInt32(ddlFilterType.SelectedValue); }
            set { ddlFilterType.SelectedValue = Convert.ToInt32(value).ToString(); }
        }

        public string FilterValue
        {
            get { return txtFilterValue.Text; }
            set { txtFilterValue.Text = value; }
        }

        #endregion

        #region [ Request Listing ]

        public string[] OrderBy
        {
            get
            {
                return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public Globals.Common.Sort.CustomerRequestSortColumns SortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.CustomerRequestSortColumns.None;

                int result;
                Int32.TryParse(OrderBy[0], out result);
                return (Globals.Common.Sort.CustomerRequestSortColumns)result;
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

        public IList<CS_Request> RequestList
        {
            get { throw new NotImplementedException(); }
            set
            {
                pnlNoRows.Visible = value.Count.Equals(0);
                rptRequest.Visible = !value.Count.Equals(0);
                rptRequest.DataSource = value;
                rptRequest.DataBind();
            }
        }

        private RepeaterItem RequestItemRepeaterItem { get; set; }

        public CS_Request RequestItem
        {
            get { return RequestItemRepeaterItem.DataItem as CS_Request; }
            set { throw new NotImplementedException(); }
        }

        #region [ Fields ]

        public int RequestItemRequestID
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfRequestID = RequestItemRepeaterItem.FindControl("hfRequestID") as HiddenField;
                if (null != hfRequestID) hfRequestID.Value = value.ToString();

                HtmlTableRow trNotes = RequestItemRepeaterItem.FindControl("trNotes") as HtmlTableRow;
                HtmlGenericControl divExpand = RequestItemRepeaterItem.FindControl("divExpand") as HtmlGenericControl;

                if (null != trNotes && null != divExpand)
                {
                    trNotes.Attributes["class"] += " Req" + value.ToString();
                    if (!hfExpandedItems.Value.Contains("Req" + value.ToString() + ";"))
                    {
                        trNotes.Style.Add(HtmlTextWriterStyle.Display, "none");
                        divExpand.Attributes["class"] = "Expand";
                    }
                    else
                    {
                        trNotes.Style.Add(HtmlTextWriterStyle.Display, "");
                        divExpand.Attributes["class"] = "Collapse";
                    }

                    divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','Req" + value.ToString() + "');");
                    divExpand.Visible = true;
                }

                Button btnDelete = RequestItemRepeaterItem.FindControl("btnDelete") as Button;
                Button btnResend = RequestItemRepeaterItem.FindControl("btnResend") as Button;
                if (null != btnDelete && null != btnResend)
                {
                    btnDelete.CommandArgument = value.ToString();
                    btnResend.CommandArgument = value.ToString();
                }
            }
        }

        public DateTime RequestItemRequestDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDate = RequestItemRepeaterItem.FindControl("lblDate") as Label;
                if (null != lblDate) lblDate.Text = value.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

        public string RequestItemRequestedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblRequestedBy = RequestItemRepeaterItem.FindControl("lblRequestedBy") as Label;
                if (null != lblRequestedBy) lblRequestedBy.Text = value;
            }
        }

        public string RequestItemRequestType
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblType = RequestItemRepeaterItem.FindControl("lblType") as Label;
                if (null != lblType) lblType.Text = value;
            }
        }

        public string RequestItemCustomerContactName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCustomerContactName = RequestItemRepeaterItem.FindControl("lblCustomerContactName") as Label;
                if (null != lblCustomerContactName) lblCustomerContactName.Text = value;
            }
        }

        public Globals.CustomerMaintenance.RequestStatus RequestItemRequestStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatus = RequestItemRepeaterItem.FindControl("lblStatus") as Label;
                Button btnDelete = RequestItemRepeaterItem.FindControl("btnDelete") as Button;
                Button btnResend = RequestItemRepeaterItem.FindControl("btnResend") as Button;
                if (null != lblStatus && null != btnDelete && null != btnResend)
                {
                    btnDelete.Visible = false;
                    btnResend.Visible = false;
                    switch (value)
                    {
                        case Globals.CustomerMaintenance.RequestStatus.Pending:
                            lblStatus.Text = "Pending";
                            btnDelete.Visible = true;
                            btnResend.Visible = true;
                            break;
                        case Globals.CustomerMaintenance.RequestStatus.Approved:
                            lblStatus.Text = "Approved";
                            break;
                        case Globals.CustomerMaintenance.RequestStatus.Cancelled:
                            lblStatus.Text = "Cancelled";
                            break;
                    }
                }
            }
        }

        public string RequestItemRequestNotes
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblNotes = RequestItemRepeaterItem.FindControl("lblNotes") as Label;
                if (null != lblNotes) lblNotes.Text = value;
            }
        }

        #endregion

        #endregion

        #region [ Commands ]

        public int RequestID { get; set; }

        public string Username
        {
            get { return Master.Username; }
            set { throw new NotImplementedException(); }
        }

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
                case Globals.Common.Sort.CustomerRequestSortColumns.Date:
                    control = RequestItemRepeaterItem.FindControl("thDate") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerRequestSortColumns.RequestedBy:
                    control = RequestItemRepeaterItem.FindControl("thRequestedBy") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerRequestSortColumns.Type:
                    control = RequestItemRepeaterItem.FindControl("thType") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerRequestSortColumns.CustomerContactName:
                    control = RequestItemRepeaterItem.FindControl("thCustomerContactName") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerRequestSortColumns.Status:
                    control = RequestItemRepeaterItem.FindControl("thStatus") as HtmlTableCell;
                    break;
                case Globals.Common.Sort.CustomerRequestSortColumns.None:
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

    }
}