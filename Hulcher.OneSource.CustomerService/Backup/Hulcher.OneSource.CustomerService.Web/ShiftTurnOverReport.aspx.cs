using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.DataContext;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Reporting.WebForms;
using System.Security;
using System.Security.Permissions;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ShiftTurnOverReport : System.Web.UI.Page, IShiftTurnOverReportView
    {
        #region [ Attributes ]

        ShiftTurnOverReportPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ShiftTurnOverReportPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _presenter.ListJobStatus();
            }
        }

        public void btnGenerate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.LoadReport();
            }
        }

        #endregion

        #region [ Properties ]

        public string ReportName
        {
            get
            {
                return "ShiftTurnOver";
            }
        }

        public IList<CS_View_TurnoverActiveReport> ActiveJobViewReportDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

                ReportDataSource source = new ReportDataSource("Source", value);
                Master.ReportViewer.LocalReport.DataSources.Clear();
                Master.ReportViewer.LocalReport.DataSources.Add(source);
                Master.ReportViewer.Visible = true;
                Master.ReportViewer.LocalReport.ReportPath = @"Reports\ShiftTurnover.rdlc";

            }
        }

        public IList<CS_View_EquipmentInfo> QuickReferenceReportDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

                ReportDataSource equipmentSource = new ReportDataSource("HeavyEquipment", value);
                ReportDataSource source = new ReportDataSource("Source", new List<CS_View_EquipmentInfo>());
                Master.ReportViewer.LocalReport.DataSources.Clear();
                Master.ReportViewer.LocalReport.DataSources.Add(equipmentSource);
                Master.ReportViewer.LocalReport.DataSources.Add(source);
                Master.ReportViewer.Visible = true;
                Master.ReportViewer.LocalReport.ReportPath = @"Reports\ShiftTurnover_QuickReference.rdlc";
            }
        }

        public IList<CS_View_TurnoverNonActiveReport> JobViewPresetReportDataSource
        {
            set
            {
                PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);
                Master.ReportViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

                ReportDataSource source = new ReportDataSource("Source", value);
                Master.ReportViewer.LocalReport.DataSources.Clear();
                Master.ReportViewer.LocalReport.DataSources.Add(source);
                Master.ReportViewer.Visible = true;
                Master.ReportViewer.LocalReport.ReportPath = @"Reports\ShiftTurnoverPresetPotential.rdlc";
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

        public IList<CS_JobStatus> JobStatusList
        {
            set
            {
                Globals.JobRecord.JobStatus[] status = new Globals.JobRecord.JobStatus[] { Globals.JobRecord.JobStatus.Active, Globals.JobRecord.JobStatus.Preset, Globals.JobRecord.JobStatus.Potential, Globals.JobRecord.JobStatus.PresetPurchase };
                IList<CS_JobStatus> jobStatusList = value.Where(e => status.Contains((Globals.JobRecord.JobStatus)e.ID)).ToList();
                ddlJobStatus.DataSource = jobStatusList;
                ddlJobStatus.DataTextField = "Description";
                ddlJobStatus.DataValueField = "ID";
                ddlJobStatus.DataBind();
                ddlJobStatus.Items.Insert(0, new ListItem("- Select One - ", "0"));
                ddlJobStatus.SelectedValue = ((int)Globals.JobRecord.JobStatus.Active).ToString();
            }
        }

        public int JobStatusID
        {
            get { return int.Parse(ddlJobStatus.SelectedValue); }
        }

        public Globals.ShiftTurnoverReport.ReportView ReportView
        {
            get { return (Globals.ShiftTurnoverReport.ReportView)int.Parse(rblReportType.SelectedValue); }
        }

        /// <summary>
        /// Display Message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="closeWindow">closeWindow</param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }
        #endregion

        #region [ Methods ]

        /// <summary>
        /// Clear report viewer
        /// </summary>
        public void ClearReportViewer()
        {
            Master.ReportViewer.Reset();
        }

      
        #endregion

    }
}
