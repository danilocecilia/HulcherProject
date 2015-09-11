using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Reporting.WebForms;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class FirstAlertReport : System.Web.UI.Page, IFirstAlertReportView
    {
        #region [ Attributes ]

        FirstAlertReportPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new FirstAlertReportPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (FirstAlertId != 0)
                {
                    _presenter.BindReport();
                    _presenter.ConfigureEmail();
                }
            }
        }

        #endregion

        #region [ Properties ]

        public string ReportName
        {
            get { return "FirstAlert"; }
        }

        public IList<CS_FirstAlert> FirsAlertReportDataSource
        {
            set 
            {                
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("Source", value);
                this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
            }
        }

        public int FirstAlertId
        {
            get {
                if (Request.QueryString.Get("FirstAlertId") == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(Request.QueryString.Get("FirstAlertId"));
                }
            }
        }

        public IDictionary<string, string> ReportParameters
        {
            set
            {
                if (value.Count > 0)
                {
                    List<ReportParameter> lstParameters = new List<ReportParameter>();

                    foreach (KeyValuePair<string, string> param in value)
                    {
                        ReportParameter teste = new ReportParameter();

                        teste.Name = param.Key;
                        teste.Values.Add(param.Value);
                        lstParameters.Add(teste);
                    }

                    Master.ReportViewer.LocalReport.SetParameters(lstParameters);
                }

                Master.ReportViewer.LocalReport.Refresh();
            }
        }

        public string ReceiptsList
        {
            set
            {
                Master.Receipts = value;
                Master.ReceiptsVisible = false;
            }
        }

        public IList<CS_FirstAlertPerson> FirstAlertReportHulcherPersonsDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("HulcherPersonDataSet", value);
                this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
            }
        }

        public IList<CS_FirstAlertPerson> FirstAlertReportNonHulcherPersonsDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("NonHulcherPersonDataSet", value);
                //this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
            }
        }

        public IList<CS_View_FirstAlertReportHulcherVehicles> FirstAlertReportHulcherVehicleDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("HulcherVehicleDataSet", value);
                //this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
            }
        }


        public IList<CS_View_FirstAlertReportOtherVehicle> FirstAlertReportOtherVehicleDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("OtherVehicleDataSet", value);
                //this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
            }
        }

        public IList<CS_View_FirstAlertReportContactPersonal> FirstAlertReportContactPersonalDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("ContactPersonalDataSet", value);
                //this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\FirstAlert.rdlc";
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
