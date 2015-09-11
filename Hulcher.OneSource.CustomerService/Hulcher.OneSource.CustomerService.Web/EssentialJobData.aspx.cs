using System;
using System.Web.UI;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class EssentialJobData : Page, IEssentialJobDataView
    {
        #region [ Atributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private EssentialJobDataPresenter _presenter;

        private int _jobId;
        private bool _savedSuccessfuly;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new EssentialJobDataPresenter(this);
        }

        #endregion

        #region [ Properties ]

        public bool IsEmergencyResponse
        {
            get
            {
                return Convert.ToBoolean(rblEmergencyResponse.SelectedValue);
            }
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public int? PrimaryContactId
        {
            get
            {
                if (actContact.SelectedValue == "0")
                    return null;
                else
                {
                    return int.Parse(actContact.SelectedValue);
                }

            }
            set
            {
                if (value.HasValue)
                    actContact.SelectedValue = value.Value.ToString();
                else
                    actContact.SelectedValue = "0";
            }
        }

        public int CustomerId
        {
            get { return int.Parse(actCustomer.SelectedValue); }
        }

        public int? HulcherContactId
        {
            get
            {
                if (actHulcherContact.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actHulcherContact.SelectedValue);
            }
            set { actHulcherContact.SelectedValue = value.ToString(); }
        }

        public int PrimaryDivisionId
        {
            get { return int.Parse(actDivision.SelectedValue); }
        }

        public DateTime InitialCallDate
        {
            get { return Convert.ToDateTime(dpCallDate.Value); }
        }

        public TimeSpan InitialCallTime
        {
            get { return TimeSpan.Parse(txtCallTime.Text); }
        }

        public int JobStatusId
        {
            get { return int.Parse(actJobStatus.SelectedValue); }
        }

        public int PriceTypeId
        {
            get { return int.Parse(actPriceType.SelectedValue); }
        }

        public int JobActionId
        {
            get { return int.Parse(actJobAction.SelectedValue); }
        }

        public int StateId
        {
            get { return int.Parse(actState.SelectedValue); }
        }

        public int CityId
        {
            get { return int.Parse(actCity.SelectedValue); }
        }

        public int ZipCode
        {
            get { return int.Parse(actZipCode.SelectedValue); }
        }

        public string ScopeOfWork
        {
            get { return txtScopeEquipment.Text; }
        }

        public int JobId
        {
            get { return _jobId; }

            set { _jobId = value; }
        }

        public bool SavedSuccessfuly
        {
            get { return _savedSuccessfuly; }
            set { _savedSuccessfuly = value; }
        }

        #endregion

        #region [ Methods ]

        #endregion

        #region [ Events ]

        protected void SaveAndContinue_OnClick(object sender, EventArgs e)
        {
            _presenter.SaveJobData();
            if (_savedSuccessfuly)
                Response.Redirect(string.Format("JobRecord.aspx?JobId={0}&CreateInitialAdvise=TRUE", _jobId));
            else
            {
                if (!string.IsNullOrEmpty(actHulcherContact.SelectedText))
                {
                    actContact.RequiredField = false;
                }
                else if (!string.IsNullOrEmpty(actContact.SelectedText))
                {
                    actHulcherContact.RequiredField = false;
                }
                else
                {
                    actContact.RequiredField = true;
                    actHulcherContact.RequiredField = true;
                }
            }
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", 
            //    "window.open('Email.aspx?JobId=" + _jobId  + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=0,resizable=0,width=600,');document.location.href='JobRecord.aspx?JobId=" + _jobId + "'", true);
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpCallDate.Value = DateTime.Now;
                txtCallTime.Text = DateTime.Now.ToString("HH:mm");

                ScriptManager.GetCurrent(this).SetFocus(actContact.TextControlClientID);
            }
        }

        protected void actCustomer_TextChanged(object sender, EventArgs e)
        {
            actContact.ContextKey = actCustomer.SelectedValue;
        }

        protected void actCity_TextChanged(object sender, EventArgs e)
        {
            actZipCode.ContextKey = actCity.SelectedValue;
        }

        protected void actState_TextChanged(object sender, EventArgs e)
        {
            actCity.ContextKey = actState.SelectedValue;
        }

        #endregion

        #region [ Implementation of Interface ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}
