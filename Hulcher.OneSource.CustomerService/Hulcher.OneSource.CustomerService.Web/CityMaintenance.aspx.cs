using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class CityMaintenance : System.Web.UI.Page, ICityMaintenanceView
    {
        #region [ Attributes ]

        CityMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new CityMaintenancePresenter(this);
        }

        #endregion

        #region [ Constructor ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CityListDataSource = new List<CS_City>();
            }
        }

        #endregion

        #region [ Events ]

        protected void btnNewCity_Click(object sender, EventArgs e)
        {
            _presenter.AddCity();
        }

        protected void btnSaveCity_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.SaveCity();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.FindCityList();
        }

        protected void gvCityList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CityRow = e.Row;
                _presenter.BindCityRow();
            }
        }

        protected void gvCityList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CityListRowCommandName = (Globals.Common.GridView.GridCommandNames)int.Parse(e.CommandName);
            CityListRowCommandArgument = int.Parse(e.CommandArgument.ToString());
            _presenter.CityListRowCommand();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.FindCityList();
        }

        #endregion

        #region [ Properties ]

        #region [ City List ]

        private GridViewRow CityRow { get; set; }

        public IList<CS_City> CityListDataSource
        {
            set
            {
                gvCityList.DataSource = value;
                gvCityList.DataBind();
            }
        }

        public CS_City CityRowDataItem
        {
            get { return CityRow.DataItem as CS_City; }
        }

        public int CityRowCityID
        {
            set
            {
                LinkButton lnkEdit = CityRow.FindControl("lnkEdit") as LinkButton;
                lnkEdit.CommandArgument = value.ToString();
                lnkEdit.CommandName = ((int)Globals.Common.GridView.GridCommandNames.Edit).ToString();

                LinkButton lnkRemove = CityRow.FindControl("lnkRemove") as LinkButton;
                lnkRemove.CommandArgument = value.ToString();
                lnkRemove.CommandName = ((int)Globals.Common.GridView.GridCommandNames.Remove).ToString();
                lnkRemove.OnClientClick = "return confirm('Are you sure you want to remove this City?');";
            }
            get { throw new NotImplementedException(); }
        }

        public string CityRowStateName
        {
            set
            {
                Label lblState = CityRow.FindControl("lblState") as Label;
                lblState.Text = value;
            }
            get { throw new NotImplementedException(); }
        }

        public string CityRowCountry
        {
            set
            {
                Label lblCountry = CityRow.FindControl("lblCountry") as Label;
                lblCountry.Text = value;
            }
            get { throw new NotImplementedException(); }
        }

        public bool CityRowCSRecord
        {
            set
            {
                LinkButton lnkEdit = CityRow.FindControl("lnkEdit") as LinkButton;
                lnkEdit.Visible = value;

                LinkButton lnkRemove = CityRow.FindControl("lnkRemove") as LinkButton;
                lnkRemove.Visible = value;
            }
            get { throw new NotImplementedException(); }
        }

        public Globals.Common.GridView.GridCommandNames CityListRowCommandName
        {
            set;
            get;
        }

        public int CityListRowCommandArgument
        {
            set;
            get;
        }

        #endregion

        #region [ Filter ]

        public string StateName
        {
            get
            {
                return txtFilterValue.Text;
            }
        }

        #endregion

        #region [ Common ]

        public int? CityID
        {
            get
            {
                return ViewState["CityID"] as int?;
            }
            set
            {
                ViewState["CityID"] = value;
            }
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public CS_City CityEntity
        {
            get
            {
                string createdby = Username;
                DateTime createdDate = DateTime.Now;
                DateTime modifiedDate = createdDate;

                if (!string.IsNullOrEmpty(hidCreatedBy.Value))
                    createdby = hidCreatedBy.Value;

                if (!string.IsNullOrEmpty(hidCreatedDate.Value))
                    createdDate = DateTime.Parse(hidCreatedDate.Value);

                return new CS_City()
                {
                    ID = ((CityID.HasValue) ? CityID.Value : 0),
                    Name = txtName.Text,
                    StateID = int.Parse(actState.SelectedValue),
                    CreationDate = createdDate,
                    CreatedBy = createdby,
                    ModificationDate = modifiedDate,
                    ModifiedBy = Username,
                    Active = true
                };
            }
            set
            {
                if (value != null)
                {
                    CityID = value.ID;
                    txtName.Text = value.Name;
                    actState.SetValue = value.CS_State.ID.ToString();
                    actState.SelectedText = value.CS_State.CountryAcronymName;
                    hidCreatedBy.Value = value.CreatedBy;
                    hidCreatedDate.Value = value.CreationDate.ToString();

                    for (int i = 0; i < value.CS_ZipCode.Count; i++)
                    {
                        CS_ZipCode zipCode = value.CS_ZipCode.ToList()[i];
                        if (zipCode.Active)
                            hidZipcode.Value += string.Format("|{0};{1};{2};{3};{4};{5}", zipCode.Name, zipCode.Latitude, zipCode.Longitude, zipCode.ID, zipCode.CreatedBy, zipCode.CreationDate);
                    }
                }
                else
                {
                    CityID = null;
                    txtName.Text = string.Empty;
                    actState.ClearSelection();
                    hidZipcode.Value = string.Empty;
                }
            }
        }

        public IList<CS_ZipCode> ZipCodeList
        {
            get
            {
                IList<CS_ZipCode> zipCodeList = new List<CS_ZipCode>();
                string[] codeList = hidZipcode.Value.Split('|');

                for (int i = 0; i < codeList.Length; i++)
                {
                    string[] code = codeList[i].Split(';');

                    if (code.Length == 6)
                    {
                        double? latitude = null, longitude = null;
                        double lat, lon;

                        if (double.TryParse(code[1], out lat))
                            latitude = lat;

                        if (double.TryParse(code[2], out lon))
                            longitude = lon;

                        string createdby = Username;
                        DateTime createdDate = DateTime.Now;
                        DateTime modifiedDate = createdDate;

                        if (!string.IsNullOrEmpty(code[4]))
                            createdby = code[4];

                        if (!string.IsNullOrEmpty(code[5]))
                            createdDate = DateTime.Parse(code[5]);

                        CS_ZipCode zipCode = new CS_ZipCode()
                        {
                            ID = Convert.ToInt32(code[3]),
                            Name = code[0],
                            Latitude = latitude,
                            Longitude = longitude,
                            CityId = (CityID.HasValue)? CityID.Value : 0,
                            CreationDate = createdDate,
                            CreatedBy = createdby,
                            ModificationDate = modifiedDate,
                            ModifiedBy = Username,
                            Active = true
                        };

                        zipCodeList.Add(zipCode);
                    }
                }

                return zipCodeList;
            }
        }

        public bool EnableVisualizationPanel
        {
            get { return pnlVisualization.Visible; }
            set { pnlVisualization.Visible = value; }
        }

        public bool EnableCreationPanel
        {
            get { return pnlCreation.Visible; }
            set { pnlCreation.Visible = value; }
        }

        #endregion

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void ClearForm()
        {
            CityID = null;
            hidCreatedBy.Value = string.Empty;
            hidCreatedDate.Value = string.Empty;
            hidZipcode.Value = string.Empty;
            txtName.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtLat.Text = string.Empty;
            txtLon.Text = string.Empty;
            actState.ClearSelection();
        }

        #endregion
    }
}
