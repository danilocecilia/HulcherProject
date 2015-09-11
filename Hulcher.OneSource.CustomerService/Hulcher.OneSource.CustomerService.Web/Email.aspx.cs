using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class Email : System.Web.UI.Page, IEmailView
    {
        #region [ Attributes ]

        private EmailPresenter _presenter;

        #endregion

        #region [ Properties ]

        public string EmailSubject
        {
            get { return txtSubject.Text; }
            set { txtSubject.Text = value; }
        }

        public string EmailBody
        {
            get
            {
                return editorHtmlTextArea.Content + "<BR />" + litTable.Text;
            }
            set
            {
                litTable.Text = value;
            }
        }

        public int? JobID
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString.Get("JobID")) && ViewState["JobID"] == null)
                {
                    ViewState["JobID"] = null;
                }
                else if (ViewState["JobID"] == null && !string.IsNullOrEmpty(Request.QueryString.Get("JobID")))
                    ViewState["JobID"] = int.Parse(Request.QueryString.Get("JobID"));

                return (int?)ViewState["JobID"];
            }
            set
            {
                ViewState["JobID"] = value;
            }
        }

        public IList<CS_CallLogCallCriteriaEmail> Receipts
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (null != value && value.Count > 0)
                {
                    cblTo.Items.Clear();
                    for (int i = 0; i < value.Count; i++)
                    {
                        ListItem item = new ListItem(value[i].Name, string.Format("{0};{1}", value[i].Email, value[i].ID));
                        if (value[i].Status == (int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending ||
                            value[i].Status == (int)Globals.CallCriteria.CallCriteriaEmailStatus.Error)
                            item.Selected = true;
                        else
                            item.Selected = false;
                        item.Attributes["class"] += " checkBoxTo";
                        item.Attributes["onclick"] = "ValidateEmails()";
                        cblTo.Items.Add(item);
                    }
                    lblNoReceipts.Visible = false;
                    cblTo.Visible = true;
                }
                else
                {
                    lblNoReceipts.Visible = true;
                    cblTo.Visible = false;
                }
            }
        }

        public IList<EmailVO> EmailVOReceipts
        {

            set
            {
                if (null != value && value.Count > 0)
                {
                    cblTo.Items.Clear();
                    for (int i = 0; i < value.Count; i++)
                    {
                        ListItem item = new ListItem(value[i].Name, string.Format("{0};{1}", value[i].Email, value[i].ID));
                        if (value[i].Status == (int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending ||
                            value[i].Status == (int)Globals.CallCriteria.CallCriteriaEmailStatus.Error)
                            item.Selected = true;
                        else
                            item.Selected = false;
                        item.Attributes["class"] += " checkBoxTo";
                        item.Attributes["onclick"] = "ValidateEmails()";
                        cblTo.Items.Add(item);
                    }
                }

                else
                {
                    lblNoReceipts.Visible = true;
                    cblTo.Visible = false;
                }
            }
        }

        public string ReceiptsString
        {
            get
            {
                string emailList = string.Empty;

                foreach (ListItem item in cblTo.Items)
                {
                    if (item.Selected)
                    {
                        string[] emailValues = item.Value.Split(';');
                        emailList += ";" + emailValues[0];
                    }
                }
                if (!string.IsNullOrEmpty(txtCc.Text))
                    emailList += ";" + txtCc.Text;

                return emailList.Substring(1);
            }
        }

        public List<int> ReceiptsIds
        {
            get
            {
                List<int> ids = new List<int>();

                foreach (ListItem item in cblTo.Items)
                {
                    if (item.Selected)
                    {
                        string[] emailValues = item.Value.Split(';');
                        ids.Add(Convert.ToInt32(emailValues[1]));
                    }
                }

                return ids;
            }
        }

        public string UserName
        {
            get { return this.Master.Username; }
            set { throw new NotImplementedException(); }
        }

        public int? CreationId
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        public List<int> CallLogListId
        {
            get
            {
                string[] callLogIdArray = Request.QueryString["CallLogListID"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                return callLogIdArray.Select(id => (Convert.ToInt32(id))).ToList();
            }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new EmailPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString.Get("JobID")))
                    _presenter.LoadPage();
                else
                    _presenter.JobRecordFillingEmail();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (IsValid)
                _presenter.SendMail();
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
