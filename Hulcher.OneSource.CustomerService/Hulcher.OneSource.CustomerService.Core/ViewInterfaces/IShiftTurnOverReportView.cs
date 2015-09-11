using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IShiftTurnOverReportView : IBaseView
    {
        /// <summary>
        /// Gets ReportName Parameter (File Name)
        /// </summary>
        string ReportName { get; }

        /// <summary>
        /// Sets Report Data Source for Active Jobs
        /// </summary>
        IList<CS_View_TurnoverActiveReport> ActiveJobViewReportDataSource { set; }

        /// <summary>
        /// Sets Report Data Source for preset / potential
        /// </summary>
        IList<CS_View_TurnoverNonActiveReport> JobViewPresetReportDataSource { set; }

        /// <summary>
        /// Sets Report Data Source
        /// </summary>
        IList<CS_View_EquipmentInfo> QuickReferenceReportDataSource { set; }

        /// <summary>
        /// Sets Report Parameters
        /// </summary>
        IDictionary<string,string> ReportParameters { set; }

        /// <summary>
        /// Set Job Status combo
        /// </summary>
        IList<CS_JobStatus> JobStatusList { set; }

        /// <summary>
        /// Selected Job Identifier
        /// </summary>
        int JobStatusID { get; }

        /// <summary>
        /// Selected Report View
        /// </summary>
        Globals.ShiftTurnoverReport.ReportView ReportView { get; }

        /// <summary>
        /// Clear Report Viewer
        /// </summary>
        void ClearReportViewer();
    }
}
