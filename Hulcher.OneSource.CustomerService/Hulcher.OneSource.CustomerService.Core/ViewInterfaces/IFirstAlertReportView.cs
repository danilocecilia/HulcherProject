using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IFirstAlertReportView:IBaseView
    {
        /// <summary>
        /// Gets ReportName Parameter (File Name)
        /// </summary>
        string ReportName { get; }

        /// <summary>
        /// Sets Report Data Source
        /// </summary>
        IList<CS_FirstAlert> FirsAlertReportDataSource { set; }

        /// <summary>
        /// Sets Person Report Data Source
        /// </summary>
        IList<CS_FirstAlertPerson> FirstAlertReportHulcherPersonsDataSource { set; }

        /// <summary>
        /// Sets Person Report Data Source
        /// </summary>
        IList<CS_FirstAlertPerson> FirstAlertReportNonHulcherPersonsDataSource { set; }

        /// <summary>
        /// Gets the first alert identifier
        /// </summary>
        int FirstAlertId { get; }

        /// <summary>
        /// Sets Report Parameters
        /// </summary>
        IDictionary<string, string> ReportParameters { set; }

        /// <summary>
        /// Sets the receipts of the first alert email
        /// </summary>
        string ReceiptsList { set; }

        /// <summary>
        /// Sets Vehicles Report Data Source
        /// </summary>
        IList<CS_View_FirstAlertReportHulcherVehicles> FirstAlertReportHulcherVehicleDataSource { set; }

        /// <summary>
        /// Sets Vehicles Report Data Source
        /// </summary>
        IList<CS_View_FirstAlertReportOtherVehicle> FirstAlertReportOtherVehicleDataSource { set; }

        /// <summary>
        /// Sets Contact Personal Report Data Source
        /// </summary>
        IList<CS_View_FirstAlertReportContactPersonal> FirstAlertReportContactPersonalDataSource { set; }
    }
}
