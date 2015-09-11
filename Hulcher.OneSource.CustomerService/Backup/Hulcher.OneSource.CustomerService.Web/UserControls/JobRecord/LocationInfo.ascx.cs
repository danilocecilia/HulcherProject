using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class LocationInfo : UserControl, ILocationInfoView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private LocationInfoPresenter _presenter;

        private int? _jobId;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new LocationInfoPresenter(this);
        }

        #endregion

        #region [ View Interface Implementation ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        /// <summary>
        /// Sets the Country List to the dropdownlist on the WebPage
        /// </summary>
        public IList<CS_Country> CountryList
        {
            set
            {
                ddlCountry.DataSource = value;
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
        }

        /// <summary>
        /// Sets the City List to the dropdownlist on the WebPage
        /// </summary>
        public IList<CS_City> CityList
        {
            set
            {
                //ddlCity.DataSource = value;
                //ddlCity.DataTextField = "ID";
                //ddlCity.DataValueField = "Name";
                //ddlCity.DataBind();
            }
        }

        /// <summary>
        /// Sets the State list to the dropdownlist on the webpage
        /// </summary>
        public IList<CS_State> StateList
        {
            set
            {
                //ddlState.DataSource = value;
                //ddlState.DataTextField = "Name";
                //ddlState.DataValueField = "ID";
                //ddlState.DataBind();
            }
        }

        /// <summary>
        /// Set all states by country
        /// </summary>
        public IList<CS_State> StateListByCountryId
        {
            set
            {
                //ddlState.DataSource = value;
                //ddlState.DataTextField = "Name";
                //ddlState.DataValueField = "ID";
                //ddlState.DataBind();
                //ddlState.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        /// <summary>
        /// Set all cities by state
        /// </summary>
        public IList<CS_City> CityListByStateId
        {
            set
            {
                //ddlCity.Items.Clear();
                //ddlCity.SelectedIndex = -1;
                //ddlCity.DataSource = value;
                //ddlCity.DataTextField = "Name";
                //ddlCity.DataValueField = "ID";
                //ddlCity.DataBind();
                //ddlCity.Items.Insert(0, new ListItem("- Select One -", "0"));
            }
        }

        /// <summary>
        /// Set all zipcode by city
        /// </summary>
        public IList<CS_ZipCode> ZipCodeListByCityId
        {
            set
            {
                //ddlZipCode.Items.Clear();
                //ddlZipCode.SelectedIndex = -1;
                //ddlZipCode.DataSource = value;
                //ddlZipCode.DataTextField = "Name";
                //ddlZipCode.DataValueField = "ID";
                //ddlZipCode.DataBind();
                //ddlZipCode.Items.Insert(0, new ListItem("- Select One -", "0"));
            }
        }

        /// <summary>
        /// Get state value
        /// </summary>
        public int StateValue
        {
            get { return int.Parse(actState.SelectedValue); }

        }

        /// <summary>
        /// Get city value
        /// </summary>
        public int CityValue
        {
            get { return int.Parse(actCity.SelectedValue); }
        }

        /// <summary>
        /// Get zipcode value
        /// </summary>
        public int ZipCode
        {
            get { return int.Parse(actZipCode.SelectedValue); }
        }

        /// <summary>
        /// Get sitename value
        /// </summary>
        public string SiteName
        {
            get { return txtSiteName.Text; }
        }

        /// <summary>
        /// Get alternatelocation value
        /// </summary>
        public string AlternateLocation
        {
            get { return txtAlternateLocation.Text; }
        }

        /// <summary>
        /// Get directions value
        /// </summary>
        public string Directions
        {
            get { return txtDirections.Text; }
        }

        public int? CloningId
        {
            get;
            set;
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.ListAllCountries();
        }

        protected void ComboBoxCountry_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            actState.ContextKey = ddlCountry.SelectedValue;
            actState.SetValue = "0";
            actState.SelectedText = "";

            actCity.SetValue = "0";
            actCity.SelectedText = "";

            actZipCode.SetValue = "0";
            actZipCode.SelectedText = "";

            ScriptManager.GetCurrent(this.Page).SetFocus(actState.ClientID + "_text");

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

        #region [ Properties ]

        public string StateBehaviorID
        {
            get
            {
                return actState.BehaviorId;
            }
        }

        public string StateHiddenFieldValueClientID
        {
            get
            {
                return actState.HiddenFieldValueClientID;
            }
        }

        public string StateTextControlClientID
        {
            get
            {
                return actState.TextControlClientID;
            }
        }

        //public string CityBehaviorID
        //{
        //    get
        //    {
        //        return actCity.BehaviorId;
        //    }
        //}

        //public string CityHiddenFieldValueClientID
        //{
        //    get
        //    {
        //        return actCity.HiddenFieldValueClientID;
        //    }
        //}

        //public string CityTextControlClientID
        //{
        //    get
        //    {
        //        return actCity.TextControlClientID;
        //    }
        //}

        public string ZipCodeBehaviorID
        {
            get
            {
                return actZipCode.BehaviorId;
            }
        }

        public string ZipCodeHiddenFieldValueClientID
        {
            get
            {
                return actZipCode.HiddenFieldValueClientID;
            }
        }

        public string ZipCodeTextControlClientID
        {
            get { return actZipCode.TextControlClientID; }
        }

        /// <summary>
        /// Get the country value
        /// </summary>
        public int CountryValue
        {
            get
            {
                return int.Parse(ddlCountry.SelectedValue);
            }
            set
            {
                ddlCountry.SelectedValue = value.ToString();
            }
        }

        /// <summary>
        /// Get the zipcode value
        /// </summary>
        public string ZipCodeValue
        {
            get { return (actZipCode.SelectedValue); }
        }

        /// <summary>
        /// Access the Location Info Entity for saving
        /// </summary>
        public CS_LocationInfo LocationInfoEntity
        {
            get
            {
                var csLocationInfo = new CS_LocationInfo
                                         {
                                             JobID = 1,
                                             CountryID = int.Parse(ddlCountry.SelectedValue),
                                             StateID = int.Parse(actState.SelectedValue),
                                             CityID = int.Parse(actCity.SelectedValue),
                                             SiteName = txtSiteName.Text,
                                             AlternateName = txtAlternateLocation.Text,
                                             Directions = txtDirections.Text,
                                             ZipCodeId = int.Parse(actZipCode.SelectedValue),
                                             CreatedBy = ((ContentPage)Page.Master).Username,
                                             CreationDate = DateTime.Now,
                                             ModifiedBy = ((ContentPage)Page.Master).Username,
                                             ModificationDate = DateTime.Now,
                                             Active = true
                                         };
                return csLocationInfo;
            }
        }

        public CS_View_GetJobData LocationInfoLoad
        {
            set
            {
                if (ddlCountry.Items.Count == 0)
                    _presenter.ListAllCountries();

                ddlCountry.SelectedValue = "1";
                actState.ContextKey = "1";

                CS_View_GetJobData jobData = value;
                if (null != jobData)
                {
                    if (jobData.CountryID.HasValue)
                        ddlCountry.SelectedValue = jobData.CountryID.Value.ToString();

                    if (jobData.StateID.HasValue)
                    {
                        actState.SetValue = jobData.StateID.Value.ToString();
                        actState.SelectedText = jobData.StateAcronymName;
                        actState.ContextKey = ddlCountry.SelectedValue;
                    }

                    if (jobData.CityID.HasValue)
                    {
                        actCity.SetValue = jobData.CityID.Value.ToString();
                        actCity.SelectedText = jobData.CityStateInformation;
                        actCity.ContextKey = actState.SelectedValue;
                    }

                    if (jobData.ZipCodeID.HasValue)
                    {
                        actZipCode.SetValue = jobData.ZipCodeID.Value.ToString();
                        actZipCode.SelectedText = jobData.ZipCodeName;
                        actZipCode.ContextKey = actCity.SelectedValue;
                    }

                    txtAlternateLocation.Text = jobData.AlternateName;
                    txtDirections.Text = jobData.Directions;
                    txtSiteName.Text = jobData.SiteName;
                }
            }
        }

        /// <summary>
        /// Set the Validation Group of the Validators inside the User Control
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                actCity.ValidationGroup = value;
                actState.ValidationGroup = value;
                actZipCode.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Get and Set the JobId
        /// </summary>
        public int? JobId
        {
            get
            {
                return _jobId;
            }
            set
            {
                _jobId = value;
            }
        }
        #endregion

        #region [ METHODS ]

        public void ReadyOnlyLocationInfo()
        {
            actZipCode.Enabled = false;
            actState.Enabled = false;
            ddlCountry.Enabled = false;
            actCity.Enabled = false;
            txtAlternateLocation.Enabled = false;
            txtDirections.Enabled = false;
            txtSiteName.Enabled = false;
        }

        #endregion



    }
}
