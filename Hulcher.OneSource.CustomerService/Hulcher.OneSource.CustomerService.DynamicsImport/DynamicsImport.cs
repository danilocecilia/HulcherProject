using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.DynamicsImport
{
    /// <summary>
    /// Dynamics Import Service class 
    /// </summary>
    public partial class DynamicsImportService : ServiceBase
    {
        #region Attributes

        Thread dynamicsImportWorker;
        AutoResetEvent dynamicsImportStop = new AutoResetEvent(false);

        #endregion

        #region Constructor

        public DynamicsImportService()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method called when the service starts
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            dynamicsImportWorker = new Thread(DoWork);
            dynamicsImportWorker.Start();            
        }

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        private void DoWork(object arg)
        {
            for (; ; )
            {
                if (dynamicsImportStop.WaitOne(20000)) 
                    return;
                try
                {
                    CustomerModel customerModel = new CustomerModel();
                    customerModel.ServiceWork();
                }
                catch (Exception ex)
                {
                    SettingsModel settings = new SettingsModel();
                    Logger.Write(string.Format("An error has ocurred while running the Dynamics Import Service!\n{0}\n{1}", ex.Message, ex.StackTrace));

                    StringBuilder mailError = new StringBuilder();
                    mailError.AppendLine(string.Format("An error has ocurred while running the Dynamics Import Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                    //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dynamics Import Service - Error occured on Contact Information", false, null);
                }
                //Logger.Write("Test log Service");
            }            
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            dynamicsImportStop.Set();
            dynamicsImportWorker.Join();
        }

        #endregion
    }
}
