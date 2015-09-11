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

namespace Hulcher.OneSource.CustomerService.DossierImport
{
    /// <summary>
    /// Dynamics Import Service class 
    /// </summary>
    public partial class DossierImportService : ServiceBase
    {
        #region [ Attributes ]

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        Thread dossierImportWorker;

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent dossierImportStop = new AutoResetEvent(false);

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public DossierImportService()
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
            dossierImportWorker = new Thread(DoWork);
            dossierImportWorker.Start();
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            dossierImportStop.Set();
            dossierImportWorker.Join();
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
                if (dossierImportStop.WaitOne(20000))
                    return;
                try
                {
                    EquipmentModel equipmentModel = new EquipmentModel();
                    equipmentModel.ServiceWork();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the Dossier Import Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        #endregion
    }
}
