using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.Utils;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.IVantageImport
{
    /// <summary>
    /// IVantage Import Service class 
    /// </summary>
    public partial class IVantageImport : ServiceBase
    {
        #region [ Attributes ]

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        Thread iVantageImportWorker;

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent iVantageImportStop = new AutoResetEvent(false);

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public IVantageImport()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Events ]

        /// <summary>
        /// Method called when the service starts
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            iVantageImportWorker = new Thread(DoWork);
            iVantageImportWorker.Start();
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            iVantageImportStop.Set();
            iVantageImportWorker.Join();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        private void DoWork(object arg)
        {
            for (; ; )
            {
                if (iVantageImportStop.WaitOne(10000))
                    return;

                try
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    employeeModel.ServiceWork();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the IVantage Import Service!\n{0}\n{1}", ex.Message, ex.StackTrace));

                    SettingsModel settings = new SettingsModel();
                    EmailModel emailModel = new EmailModel();
                    StringBuilder mailError = new StringBuilder();
                    mailError.AppendLine(string.Format("An error has ocurred while running the IVantage Import Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                    //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "IVantage Import Service - Error occured on Employee Information", false, null);
                    emailModel.SaveEmailList(settings.GetITEmailOnError(), "IVantage Import Service - Error occured on Employee Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);
                }
            }
        }

        #endregion
    }
}
