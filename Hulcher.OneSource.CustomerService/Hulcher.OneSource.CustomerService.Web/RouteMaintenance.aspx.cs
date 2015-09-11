using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;


namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class RouteMaintenance : System.Web.UI.Page, IRouteMaintenanceView
    {
        #region [ Attributes ]

        RouteMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new RouteMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.ClearForm();

                _presenter.VerifyAccess();
                btnCreate.Visible = !ReadOnly;

                _presenter.LoadPage();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            _presenter.ClearForm();
            //_presenter.CopyStateCityToCreate();
            CreationPanelVisible = true;
            VisualizationPanelVisible = false;
        }

        public void gvRoute_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _presenter.ClearForm();
            RouteListRowCommandName = (Globals.Common.GridView.GridCommandNames)int.Parse(e.CommandName);
            RouteListRowCommandArgument = int.Parse(e.CommandArgument.ToString());
            _presenter.RouteListRowCommand();
        }

        public void gvRoute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RouteRow = e.Row;
                _presenter.RouteBindRow();
            }
        }

        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveAndClose = false;
                _presenter.SaveUpdateRoute();
                _presenter.BindDashboard();
                _presenter.ClearForm();
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveAndClose = true;
                _presenter.SaveUpdateRoute();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.ClearForm();
        }

        protected void actState_TextChanged(object sender, EventArgs e)
        {
            actCity.ContextKey = actState.SelectedValue;
        }

        protected void actFilterState_TextChanged(object sender, EventArgs e)
        {
            actFilterCity.ContextKey = actFilterState.SelectedValue;
        }

        protected void btnFindLocation_Click(object sender, EventArgs e)
        {
            _presenter.BindDashboard();
            //ScriptManager.GetCurrent(this).SetFocus(txtFilterLocation);
        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void ClearForm()
        {
            hidDivisions.Text = string.Empty;
            actState.ClearSelection();
            actCity.ClearSelection();
            ddlDivision.ClearSelection();
            actZipCode.ClearSelection();
        }

        public void CopyStateCityToCreate()
        {
            if (!string.IsNullOrEmpty(actFilterState.SelectedValue))
            {
                actState.SelectedText = actFilterState.SelectedText;
                actState.SetValue = actFilterState.SelectedValue;
            }

            if (!string.IsNullOrEmpty(actFilterCity.SelectedValue))
            {
                actCity.SelectedText = actFilterCity.SelectedText;
                actCity.SetValue = actFilterCity.SelectedValue;
            }
        }

        #endregion

        #region [ Properties ]

        #region [ Dashboard ]

        public int? CityID
        {
            get
            {
                if (actFilterCity.SelectedValue == "0")
                    return null;

                return int.Parse(actFilterCity.SelectedValue);
            }
        }

        public int? ZipCodeID
        {
            get
            {
                if (actFilterZipCode.SelectedValue == "0")
                    return null;

                return int.Parse(actFilterZipCode.SelectedValue);
            }
        }

        public int? StateID
        {
            get
            {
                if (actFilterState.SelectedValue == "0")
                    return null;
                return int.Parse(actFilterState.SelectedValue);
            }
        }

        public IList<CS_Route> RouteDashboardDataSource
        {
            set
            {
                gvRoute.DataSource = value;
                gvRoute.DataBind();
                RouteGridInfoList = value;
            }
        }

        private GridViewRow RouteRow
        {
            get;
            set;
        }

        public CS_Route RouteRowDataItem
        {
            get
            {
                return RouteRow.DataItem as CS_Route;
            }
        }

        public int RouteRowID
        {
            set
            {
                LinkButton lnkEdit = RouteRow.FindControl("lnkEdit") as LinkButton;
                lnkEdit.CommandArgument = value.ToString();
                lnkEdit.CommandName = ((int)Globals.Common.GridView.GridCommandNames.Edit).ToString();

                LinkButton lnkRemove = RouteRow.FindControl("lnkRemove") as LinkButton;
                lnkRemove.CommandArgument = value.ToString();
                lnkRemove.CommandName = ((int)Globals.Common.GridView.GridCommandNames.Remove).ToString();
            }
        }

        public string RouteRowLocation
        {
            set
            {
                Label lblLocation = RouteRow.FindControl("lblLocation") as Label;
                lblLocation.Text = value;
            }
        }

        public string RouteRowDivision
        {
            set
            {
                Label lblDivision = RouteRow.FindControl("lblDivision") as Label;
                lblDivision.Text = value;
            }
        }

        public int RouteRowMiles
        {
            set
            {
                Label lblMiles = RouteRow.FindControl("lblMiles") as Label;
                lblMiles.Text = value.ToString("0000");
            }
        }

        public double RouteRowHours
        {
            set
            {
                Label lblHours = RouteRow.FindControl("lblHours") as Label;
                lblHours.Text = value.ToString("00.00");
            }
        }

        public int RouteRowFuel
        {
            set
            {
                Label lblFuel = RouteRow.FindControl("lblFuel") as Label;
                lblFuel.Text = value.ToString();
            }
        }

        public string RouteRowRoute
        {
            set
            {
                Label lblRoute = RouteRow.FindControl("lblRoute") as Label;
                lblRoute.Text = value;
            }
        }

        public string RouteRowCityPermitOffice
        {
            set
            {
                Label lblCityPermitOffice = RouteRow.FindControl("lblCityPermitOffice") as Label;
                lblCityPermitOffice.Text = value;
            }
        }

        public string RouteRowCountyPermitOffice
        {
            set
            {
                Label lblCountyPermitOffice = RouteRow.FindControl("lblCountyPermitOffice") as Label;
                lblCountyPermitOffice.Text = value;
            }
        }

        public bool RouteRowReadOnly
        {
            set
            {
                LinkButton lnkEdit = RouteRow.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkRemove = RouteRow.FindControl("lnkRemove") as LinkButton;
                if (lnkEdit != null)
                    lnkEdit.Visible = !ReadOnly;
                if (lnkRemove != null)
                    lnkRemove.Visible = !ReadOnly;
            }
        }

        public Globals.Common.GridView.GridCommandNames RouteListRowCommandName
        {
            get;
            set;
        }

        public int RouteListRowCommandArgument
        {
            get;
            set;
        }

        public IList<CS_Route> RouteGridInfoList
        {
            get
            {
                if (null == ViewState["RouteGridInfoList"])
                    RouteGridInfoList = new List<CS_Route>();

                return ViewState["RouteGridInfoList"] as IList<CS_Route>;
            }
            set
            {
                ViewState["RouteGridInfoList"] = value;
            }
        }

        #endregion

        #region [ Form ]

        public bool EditMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int? RouteID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserName
        {
            get { return ((ContentPage)Page.Master).Username; }
        }

        public bool SavedSuccessfuly
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CreationPanelVisible
        {
            get { return pnlCreation.Visible; }
            set { pnlCreation.Visible = value; }
        }

        public bool CreationPanelEnabled
        {
            set
            {
                actState.Enabled = value;
                actCity.Enabled = value;
                btnAdd.Enabled = value;
            }
        }

        public bool VisualizationPanelVisible
        {
            get { return pnlVisualization.Visible; }
            set { pnlVisualization.Visible = value; }
        }

        public bool EnableDisableCreationButton
        {
            set
            {
                btnCreate.Enabled = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                if (null == ViewState["ReadOnly"])
                    ViewState["ReadOnly"] = false;
                return Convert.ToBoolean(ViewState["ReadOnly"]);
            }
            set { ViewState["ReadOnly"] = value; }
        }

        public string Domain
        {
            get { return ((ContentPage)Page.Master).Domain; }
        }

        public IList<CS_Division> ListAllDivisions
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlDivision.DataValueField = "ID";
                ddlDivision.DataTextField = "ExtendedDivisionName";
                ddlDivision.DataSource = value;
                ddlDivision.DataBind();
            }
        }

        public IList<CS_Route> RouteList
        {
            get
            {
                IList<CS_Route> routeList = new List<CS_Route>();

                if (!string.IsNullOrEmpty(hidDivisions.Text))
                {
                    string[] routes = hidDivisions.Text.Split('|');

                    for (int i = 0; i < routes.Length; i++)
                    {

                        string[] route = routes[i].Split(';');

                        if (route.Length == 13)
                        {
                            string createdby = UserName;
                            string mofiedby = UserName;
                            DateTime createdDate = DateTime.Now;
                            DateTime modifiedDate = createdDate;
                            int? Miles = null, Fuel = null;
                            int miles, fuel;
                            decimal? Hour = null;
                            decimal hour;

                            if (int.TryParse(route[5], out miles))
                                Miles = miles;

                            if (decimal.TryParse(route[6], out hour))
                                Hour = hour;

                            if (int.TryParse(route[7], out fuel))
                                Fuel = fuel;

                            if (!string.IsNullOrEmpty(route[11]))
                                createdby = route[11];

                            if (!string.IsNullOrEmpty(route[12]))
                                createdDate = DateTime.Parse(route[12]);

                            CS_Route routeEntity = new CS_Route();

                            routeEntity.DivisionID = int.Parse(route[0]);
                            routeEntity.CityID = int.Parse(route[2]);
                            routeEntity.ID = int.Parse(route[4]);
                            routeEntity.Miles = Miles;
                            routeEntity.Hours = Hour;
                            routeEntity.Fuel = Fuel;

                            if (route[8] == "0" || route[8] == string.Empty)
                                routeEntity.ZipCodeID = null;
                            else
                                routeEntity.ZipCodeID = int.Parse(route[8]);

                            routeEntity.CityPermitOffice = route[9];
                            routeEntity.CountyPermitOffice = route[10];
                            routeEntity.CreatedBy = createdby;
                            routeEntity.CreationDate = createdDate;
                            routeEntity.ModifiedBy = mofiedby;
                            routeEntity.ModificationDate = modifiedDate;


                            routeList.Add(routeEntity);
                        }
                    }
                }

                return routeList;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    CS_Route route = value[i];
                    hidDivisions.Text += string.Format("|{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}",
                        route.DivisionID
                        , route.CS_Division.Name
                        , route.CityID
                        , route.CS_City.CityStateInformation + " - " + (route.ZipCodeID.HasValue ? route.CS_ZipCode.ZipCodeNameEdited : string.Empty)
                        , route.ID
                        , (route.Miles.HasValue) ? route.Miles.Value.ToString("0000") : string.Empty
                        , (route.Hours.HasValue) ? route.Hours.Value.ToString("00.00") : string.Empty
                        , (route.Fuel.HasValue) ? route.Fuel.Value.ToString() : string.Empty
                        , route.ZipCodeID
                        , route.CityPermitOffice
                        , route.CountyPermitOffice
                        , route.CreatedBy
                        , route.CreationDate);
                }

                if (value.Count == 1)
                {
                    CS_Route route = value[0];
                    List<string> divs = new List<string>();
                    actState.SetValue = route.CS_City.CS_State.ID.ToString();
                    actState.SelectedText = route.CS_City.CS_State.AcronymName;
                    actCity.SetValue = route.ID.ToString();
                    actCity.SelectedText = route.CS_City.CityStateInformation;
                    divs.Add(route.DivisionID.ToString());
                    ddlDivision.SelectedValues = divs;
                    if (route.ZipCodeID.HasValue)
                    {
                        actZipCode.SelectedText = route.CS_ZipCode.ZipCodeNameEdited;
                        actZipCode.SetValue = route.CS_ZipCode.ID.ToString();

                    }
                }
            }
        }

        public bool SaveAndClose
        {
            get;
            set;
        }

        #endregion

        #endregion



    }
}
