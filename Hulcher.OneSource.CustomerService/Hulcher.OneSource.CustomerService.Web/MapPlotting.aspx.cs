using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class MapPlotting : System.Web.UI.Page, IMapPlottingView
    {
        #region [ Attributes ]

        MapPlottingPresenter _presenter;

        #endregion 

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new MapPlottingPresenter(this);
        }

        #endregion

        #region [ Contructor ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                _presenter.LoadPage();
        }

        #endregion

        #region [ Properties ]

        #region [ Filter ]

        #region [ Multiselects ]

        public IList<CS_Division> DivisionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlDivisions.Items.Clear();
                ddlDivisions.DataTextField = "Name";
                ddlDivisions.DataValueField = "ID";
                ddlDivisions.DataSource = value;
                ddlDivisions.DataBind();
            }
        }

        public IList<CS_JobAction> JobActionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlJobAction.Items.Clear();
                ddlJobAction.DataTextField = "Description";
                ddlJobAction.DataValueField = "ID";
                ddlJobAction.DataSource = value;
                ddlJobAction.DataBind();
            }
        }

        public IList<CS_JobCategory> JobCategoryDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlJobCategory.Items.Clear();
                ddlJobCategory.DataTextField = "Description";
                ddlJobCategory.DataValueField = "ID";
                ddlJobCategory.DataSource = value;
                ddlJobCategory.DataBind();
            }
        }

        public IList<CS_PriceType> PriceTypeDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlPriceType.Items.Clear();
                ddlPriceType.DataTextField = "Description";
                ddlPriceType.DataValueField = "ID";
                ddlPriceType.DataSource = value;
                ddlPriceType.DataBind();
            }
        }

        public IList<CS_EquipmentType> EquipmentTypeDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlEquipmentTypes.Items.Clear();
                ddlEquipmentTypes.DataTextField = "Name";
                ddlEquipmentTypes.DataValueField = "ID";
                ddlEquipmentTypes.DataSource = value;
                ddlEquipmentTypes.DataBind();
            }
        }

        public IList<CS_Region> RegionDataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ddlRegion.Items.Clear();
                ddlRegion.DataTextField = "Name";
                ddlRegion.DataValueField = "ID";
                ddlRegion.DataSource = value;
                ddlRegion.DataBind();
            }
        }

        #endregion

        #endregion

        #endregion

        public void DisplayMessage(string message, bool closeWindow)
        {
            throw new NotImplementedException();
        }
    }
}