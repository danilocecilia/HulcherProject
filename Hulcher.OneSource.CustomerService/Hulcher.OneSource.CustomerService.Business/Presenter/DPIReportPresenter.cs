using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class DPIReportPresenter
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Contract Detail Page
        /// </summary>
        private IDPIReportView _view;

        /// <summary>
        /// Instance of the DPI Model
        /// </summary>
        private DPIModel _model;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Instance of the Email View Interface</param>
        public DPIReportPresenter(IDPIReportView view)
        {
            this._view = view;
            _model = new DPIModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Bind Report Data
        /// </summary>
        public void BindReport()
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                IList<CS_View_DPIReport> reportData;

                using (_model = new DPIModel())
                {
                    reportData = _model.ListDPIReportInformation(_view.ReportView.Value, _view.ReportDate.Value);

                    if (_view.ReportView == Globals.DPIReport.ReportView.NewJobs)
                    {
                        parameters.Add("MTD", _model.GetDPIReportMTDCount(_view.ReportDate.Value).ToString());
                        parameters.Add("YTD", _model.GetDPIReportYTDCount(_view.ReportDate.Value).ToString());
                        parameters.Add("TotalRevenue", _model.GetDPIReportTotalRevenue(_view.ReportDate.Value).ToString("$#,##0.00"));
                        _view.NewJobsDataSource = reportData;
                    }
                    else
                    {
                        parameters.Add("TotalRevenue", _model.GetDPIReportContinuingTotalRevenue(_view.ReportDate.Value).ToString("$#,##0.00"));
                        _view.ContinuingJobsDataSource = reportData;
                    }

                    _view.ReportParameters = parameters;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to bind the Report!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to bind the Report.", false);
            }
        }

        #endregion
    }
}
