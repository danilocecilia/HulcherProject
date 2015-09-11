using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ResourceTransfer : System.Web.UI.Page, IResourceTransferView
    {
        #region [ Attributes ]

        ResourceTransferPresenter _presenter;

        private RepeaterItem ResourceRepeaterItem;

        private RepeaterItem JobCallLogRepeaterItem;

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ResourceTransferPresenter(this);
        }

        #endregion

        #region [ Load ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public int JobIdTo
        {
            get { return int.Parse(actJobNumber.SelectedValue); }
            set { throw new NotImplementedException(); }
        }

        public int JobId
        {
            get { return Convert.ToInt32(Request.QueryString.Get("JobId")); }
            set { throw new NotImplementedException(); }
        }

        public List<int> ResourceTransferId
        {
            get
            {
                string[] splitResources = Request.QueryString.Get("ResourceTransferId").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                return new List<int>(from c in splitResources select Convert.ToInt32(c));
            }
            set { throw new NotImplementedException(); }
        }

        public bool HasEquipments
        {
            get { return Convert.ToBoolean(hidHasEquipments.Value); }
            set { hidHasEquipments.Value = value.ToString(); }
        }

        public List<CS_Resource> SelectedResources
        {
            get
            {
                if (null != ViewState["SelectedResources"])
                    return ViewState["SelectedResources"] as List<CS_Resource>;
                return new List<CS_Resource>();
            }
            set { ViewState["SelectedResources"] = value; }
        }

        public Dictionary<int, Globals.TransferResource.TransferType> SelectedResourcesTransferType
        {
            get
            {
                Dictionary<int, Globals.TransferResource.TransferType> dictionary = new Dictionary<int, Globals.TransferResource.TransferType>();

                for (int i = 0; i < rptResources.Items.Count; i++)
                {
                    RadioButtonList type = rptResources.Items[i].FindControl("rblTransferType") as RadioButtonList;
                    HiddenField id = rptResources.Items[i].FindControl("hfResourceId") as HiddenField;

                    if (null != type && null!= id)
                    {
                        dictionary.Add(int.Parse(id.Value), (Core.Globals.TransferResource.TransferType)int.Parse(type.SelectedValue));
                    }
                }

                return dictionary;
            }
        }

        public List<int> SelectedCallLogIds
        {
            get
            {
                List<int> list = new List<int>();

                for (int i = 0; i < rptResources.Items.Count; i++)
                {
                    Repeater rptCallLog = rptResources.Items[i].FindControl("rptCallLogs") as Repeater;

                    if (null != rptCallLog)
                    {
                        for (int l = 0; l < rptCallLog.Items.Count; l++)
                        {
                            HiddenField id = rptCallLog.Items[l].FindControl("hidCallId") as HiddenField;
                            CheckBox check = rptCallLog.Items[l].FindControl("chkTransfer") as CheckBox;

                            if (check.Checked && !list.Exists(e => e == int.Parse(id.Value)))
                                list.Add(int.Parse(id.Value));
                        }
                    }
                }

                return list;
            }
        }

        public string Username
        {
            get
            {
                return Master.Username;
            }
        }

        #endregion

        #region [ Data Items ]

        public CS_Resource ResourceRepeaterDataItem
        {
            get { return ResourceRepeaterItem.DataItem as CS_Resource; }
        }

        public CS_CallLog JobCallLogRepeaterDataItem
        {
            get { return JobCallLogRepeaterItem.DataItem as CS_CallLog; }
        }
        #endregion

        #region [ Data Sources ]

        public IList<CS_Resource> ResourceListDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                rptResources.DataSource = value;
                rptResources.DataBind();
            }
        }

        public IList<CS_CallLog> CallLogListDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater rptCallLogs = ResourceRepeaterItem.FindControl("rptCallLogs") as Repeater;
                if (null != rptCallLogs)
                {
                    rptCallLogs.DataSource = value;
                    rptCallLogs.DataBind();
                }
            }
        }

        #endregion

        #region [ Fields Resource Repeater ]

        public int ResourceId
        {
            set
            {
                HiddenField hfId = ResourceRepeaterItem.FindControl("hfResourceId") as HiddenField;

                if (null != hfId)
                    hfId.Value = value.ToString();
            }
        }

        public string ResourceType
        {
            set
            {
                Label lblResourceType = ResourceRepeaterItem.FindControl("lblResourceType") as Label;

                if (null != lblResourceType)
                    lblResourceType.Text = value;
            }
        }

        public string ResourceName
        {
            set
            {
                Label lblResourceName = ResourceRepeaterItem.FindControl("lblResourceName") as Label;

                if (null != lblResourceName)
                    lblResourceName.Text = value;
            }
        }

        #endregion  

        #region [ Fields CallLog Repeater ]

        public string CallLogId
        {
            set
            {
                HiddenField hidCallId = JobCallLogRepeaterItem.FindControl("hidCallId") as HiddenField;

                if (hidCallId != null)
                    hidCallId.Value = value;
            }
        }

        public string ModificationId
        {
            set
            {
                HiddenField hidCallLastModification = JobCallLogRepeaterItem.FindControl("hidCallLastModification") as HiddenField;

                if (hidCallLastModification != null)
                    hidCallLastModification.Value = value;
            }
        }

        public string CallType
        {
            set
            {
                Label lblCallType = JobCallLogRepeaterItem.FindControl("lblCallType") as Label;

                if (null != lblCallType)
                    lblCallType.Text = value;
            }
        }

        public string CalledInBy
        {
            set
            {
                Label lblCalledInBy = JobCallLogRepeaterItem.FindControl("lblCalledInBy") as Label;

                if (null != lblCalledInBy)
                    lblCalledInBy.Text = value;

            }
        }

        public string CallDate
        {
            set
            {
                Label lblCallDate = JobCallLogRepeaterItem.FindControl("lblCallDate") as Label;

                if (null != lblCallDate)
                    lblCallDate.Text = value;
            }
        }

        public string CallTime
        {
            set
            {
                Label lblCallTime = JobCallLogRepeaterItem.FindControl("lblCallTime") as Label;

                if (null != lblCallTime)
                    lblCallTime.Text = value;
            }
        }

        public string ModifiedBy
        {
            set
            {
                Label lblModifiedBy = JobCallLogRepeaterItem.FindControl("lblModifiedBy") as Label;

                if (null != lblModifiedBy)
                    lblModifiedBy.Text = value;
            }
        }

        public string Notes
        {
            set
            {
                Label lblNotes = JobCallLogRepeaterItem.FindControl("lblNotes") as Label;

                Label lblTool = JobCallLogRepeaterItem.FindControl("lblTool") as Label;
                
                HtmlTableCell pnlCell = JobCallLogRepeaterItem.FindControl("pnlCell") as HtmlTableCell;

                Panel pnlToolTip = JobCallLogRepeaterItem.FindControl("pnlToolTip") as Panel;


                if (null != pnlCell && null != pnlToolTip)
                {
                    pnlCell.Attributes.Add("onmouseenter", "ShowToolTip(document.getElementById('" + pnlToolTip.ClientID + "'), document.getElementById('" + lblNotes.ClientID + "'), document.getElementById('" + lblTool.ClientID + "'));");
                    pnlCell.Attributes.Add("onmouseleave", "document.getElementById('" + pnlToolTip.ClientID + "').style.display = 'none'; document.getElementById('" + lblTool.ClientID + "').innerHTML = '';");
                }

                if (null != lblNotes)
                    lblNotes.Text = value;
            }
        }


        #endregion

        #endregion

        #region [ Events ]

        public void btnTransfer_Click(object sender, EventArgs e)
        {
            _presenter.TransferResources();
        }

        protected void rptResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ResourceRepeaterItem = e.Item;

                _presenter.LoadResourceRepeaterRow();
            }
        }

        protected void rptCallLogs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobCallLogRepeaterItem = e.Item;

                _presenter.LoadJobCallLogRepeaterRow();
            }
        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}