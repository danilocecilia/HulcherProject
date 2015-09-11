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
    public partial class DPIReport : System.Web.UI.Page, IDPIReportView
    {
        #region [ Attributes ]

        DPIReportPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new DPIReportPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null != this.ReportView)
                    _presenter.BindReport();
            }
        }

        #endregion

        #region [ Properties ]

        public string ReportName
        {
            get { return "DPIResports"; }
        }

        public IList<CS_View_DPIReport> NewJobsDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("Source", value);
                this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\NewJobsDPI.rdlc";
            }
        }

        public IList<CS_View_DPIReport> ContinuingJobsDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                this.Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
                ReportDataSource source = new ReportDataSource("Source", value);
                this.Master.ReportViewer.LocalReport.DataSources.Clear();
                this.Master.ReportViewer.LocalReport.DataSources.Add(source);
                this.Master.ReportViewer.Visible = true;
                this.Master.ReportViewer.LocalReport.ReportPath = @"Reports\ContinuingJobsDPI.rdlc";
            }
        }

        public Globals.DPIReport.ReportView? ReportView
        {
            get
            {
                if (Request.QueryString.Get("ReportView") == null)
                {
                    return null;
                }
                else
                {
                    return (Globals.DPIReport.ReportView)int.Parse(Request.QueryString.Get("ReportView"));
                }
            }
        }

        public DateTime? ReportDate
        {
            get
            {
                if (Request.QueryString.Get("ReportDate") == null)
                {
                    return null;
                }
                else
                {
                    return DateTime.Parse(Request.QueryString.Get("ReportDate"));
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
                        ReportParameter reportParameter = new ReportParameter();

                        reportParameter.Name = param.Key;
                        reportParameter.Values.Add(param.Value);
                        lstParameters.Add(reportParameter);
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

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}
