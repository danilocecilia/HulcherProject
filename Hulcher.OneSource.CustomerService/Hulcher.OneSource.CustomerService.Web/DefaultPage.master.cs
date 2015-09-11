using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class DefaultPage : System.Web.UI.MasterPage, IDefaultPageView
    {
        #region [ Attributes ]

        /// <summary>
        /// Presenter class for the Default Page
        /// </summary>
        DefaultPagePresenter _presenter = null;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new DefaultPagePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _presenter.Load();

                lblUsername.Text = Username;
                _presenter.CheckPermissions();
            }
        }

        public void btnQuickSearch_Click(object sender, EventArgs e)
        {
            _presenter.QuickSearch();
            if (!string.IsNullOrEmpty(this.QuickSearchQueryString))
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "quickSearch", this.QuickSearchQueryString, true);
        }

        public void btnSearchJob_Click(object sender, EventArgs e)
        {
            _presenter.SearchJob();          
        }

        #endregion

        #region [ Properties ]

        #region [ Search Initialization ]

        public ListItemCollection ResourceDropDownItems
        {
            get { return ddlResource.Items; }
        }

        public ListItemCollection EquipmentTypeDropDownItems
        {
            get { return ddlEquipmentType.Items; }
        }

        public ListItemCollection JobDescriptionDropDownItems
        {
            get { return ddlJobDescription.Items; }
        }

        public ListItemCollection LocationInfoDropDownItems
        {
            get { return ddlLocationInfo.Items; }
        }

        public ListItemCollection JobInfoDropDownItems
        {
            get { return ddlJobInfo.Items; }
        }

        public ListItemCollection ContactInfoDropDownItems
        {
            get { return ddlContactInfo.Items; }
        }

        public DateTime? StartDate
        {
            get
            {
                //According to the Requirements
                //If StartDate is blank, try EndDate
                //If both are blank, filter last 5 days
                if (dpStartDate.Value.HasValue)
                    return dpStartDate.Value.Value;
                if (dpEndDate.Value.HasValue)
                    return dpEndDate.Value.Value;
                return DateTime.Now.AddDays(-5d);
            }

            set { dpStartDate.Value = value; }
        }

        public DateTime? EndDate
        {
            get
            {
                //According to the Requirements
                //If End date is blank, try Start Date
                //If both are blank, filter last 5 days
                if (dpEndDate.Value.HasValue)
                    return dpEndDate.Value.Value.AddDays(1).AddSeconds(-1);
                else if (dpStartDate.Value.HasValue)
                    return dpStartDate.Value.Value.AddDays(1).AddSeconds(-1);
                return DateTime.Now;
            }

            set { dpEndDate.Value = value; }
        }

        #endregion

        #region [ Search Parameters ]

        public KeyValuePair<string, string> ResourceDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlResource.SelectedItem.Text, ddlResource.SelectedValue); }
        }

        public KeyValuePair<string, string> EquipmentTypeDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlEquipmentType.SelectedItem.Text, ddlEquipmentType.SelectedValue); }
        }

        public KeyValuePair<string, string> JobDescriptionDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlJobDescription.SelectedItem.Text, ddlJobDescription.SelectedValue); }
        }

        public KeyValuePair<string, string> LocationInfoDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlLocationInfo.SelectedItem.Text, ddlLocationInfo.SelectedValue); }
        }

        public KeyValuePair<string, string> JobInfoDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlJobInfo.SelectedItem.Text, ddlJobInfo.SelectedValue); }
        }

        public KeyValuePair<string, string> ContactInfoDropDownSelected
        {
            get { return new KeyValuePair<string, string>(ddlContactInfo.SelectedItem.Text, ddlContactInfo.SelectedValue); }
        }

        public KeyValuePair<string, string> StartDateSelected
        {
            get { return new KeyValuePair<string, string>("StartDate", StartDate.ToString()); }
        }

        public KeyValuePair<string, string> EndDateSelected
        {
            get { return new KeyValuePair<string, string>("EndDate", EndDate.ToString()); }
        }

        public SearchCriteriaVO SearchFiltersVO
        {
            get
            {
                SearchCriteriaVO searchVO = new SearchCriteriaVO();

                searchVO.resourceType = ResourceDropDownSelected.Value;
                searchVO.resourceValue = txtResource.Text;

                if (!EquipmentTypeDropDownSelected.Value.Equals("None") &&
                    !EquipmentTypeDropDownSelected.Value.Equals("All"))
                    searchVO.equipmentType = int.Parse(EquipmentTypeDropDownSelected.Value);
                searchVO.equipmentValue = txtEquipmentType.Text;

                searchVO.jobDescriptionType = JobDescriptionDropDownSelected.Value;
                searchVO.jobDescriptionValue = txtJobDescription.Text;

                searchVO.locationInfoType = LocationInfoDropDownSelected.Value;
                searchVO.locationInfoValue = txtLocationInfo.Text;

                searchVO.jobInfoType = JobInfoDropDownSelected.Value;
                searchVO.jobInfoValue = txtJobInfo.Text;

                searchVO.customerInfoType = ContactInfoDropDownSelected.Value;
                searchVO.customerInfoValue = txtContactInfo.Text;

                searchVO.startDate = StartDate.Value;
                searchVO.endDate = EndDate.Value;

                return searchVO;
            }
            set
            {
            }
        }

        #endregion

        #region [ Search Output ]

        public IList<int> SearchJobList
        {
            get
            {
                return (List<int>)ViewState["SearchJobList"];
            }
            set
            {
                ViewState["SearchJobList"] = value;
            }
        }

        #endregion

        #endregion

        #region [ IDefaultPageView Implementation ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public string Domain
        {
            get { return Master.Domain; }
        }

        public bool EnableNewJobButton
        {
            set
            {
                if (value)
                {
                    menuJobRecord.Attributes["onclick"] = "window.open('/JobRecord.aspx', '', 'width=1040, height=600, scrollbars=1, resizable=yes');";
                    //menuJobRecord.Attributes["class"] = "MenuItem";
                }
                else
                {
                    menuJobRecord.Attributes["onclick"] = "";
                    menuJobRecord.Attributes["class"] = "MenuItemDisabled";
                }
            }
        }

        public bool EnableFirstAlertLink
        {
            set
            {
                liFirstAlert.Visible = value;
            }        
        }

        public bool EnableRouteButton
        {
            set
            {
                menuRouteMaintenance.Visible = value;
            }
        }

        public int QuickSearchJobId
        {
            get;
            set;
        }

        public string QuickSearchJobValue
        {
            get
            {
                return txtQuickJobValue.Text;
            }
            set
            {
                txtQuickJobValue.Text = value;
            }
        }

        public string QuickSearchQueryString
        {
            get;
            set;
        }

        public bool EnableNewCallCriteria
        {
            set
            {
                liEmployee.Visible = value;
                liCustomer.Visible = value;
                liContact.Visible = value;
            }
        }

        public bool IsSearchJobListEmpty
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (!value)
                {
                    string pageGuid = Guid.NewGuid().ToString();
                    Session[string.Format("JobIdList_{0}", pageGuid)] = SearchJobList;

                    Session[string.Format("ContactFilter_{0}", pageGuid)] = ContactInfoDropDownSelected.Value;
                    Session[string.Format("JobFilter_{0}", pageGuid)] = JobInfoDropDownSelected.Value;
                    Session[string.Format("LocationFilter_{0}", pageGuid)] = LocationInfoDropDownSelected.Value;
                    Session[string.Format("JobDescriptionFilter_{0}", pageGuid)] = JobDescriptionDropDownSelected.Value;
                    Session[string.Format("EquipmentTypeFilter_{0}", pageGuid)] = EquipmentTypeDropDownSelected.Value;
                    Session[string.Format("ResourceFilter_{0}", pageGuid)] = ResourceDropDownSelected.Value;

                    Session[string.Format("ContactFilterValue_{0}", pageGuid)] = txtContactInfo.Text;
                    Session[string.Format("JobFilterValue_{0}", pageGuid)] = txtJobInfo.Text;
                    Session[string.Format("LocationFilterValue_{0}", pageGuid)] = txtLocationInfo.Text;
                    Session[string.Format("JobDescriptionFilterValue_{0}", pageGuid)] = txtJobDescription.Text;
                    Session[string.Format("EquipmentTypeFilterValue_{0}", pageGuid)] = txtEquipmentType.Text;
                    Session[string.Format("ResourceFilteValuer_{0}", pageGuid)] = txtResource.Text;

                    Session[string.Format("DateRangeBeginValue_{0}", pageGuid)] = StartDate.Value;
                    Session[string.Format("DateRangeEndValue_{0}", pageGuid)] = EndDate.Value;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "SendEmailPage", string.Format("var newWindow = window.open('/DashboardSearch.aspx?PageGuid={0}', '', 'width=1020, height=600, scrollbars=0, resizable=yes');", pageGuid), true);
                }
                else
                {
                    DisplayMessage("No results were found for the Job Search Criteria.",false);
                }
            }
        }

        public bool EnablePermitingNotification
        {
            set { hfPermitingNotification.Value = value.ToString().ToLower(); }
        }

        #endregion
    }
}