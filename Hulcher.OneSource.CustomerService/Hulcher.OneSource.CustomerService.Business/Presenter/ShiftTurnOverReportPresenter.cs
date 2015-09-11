using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ShiftTurnOverReportPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// ReportView view interface
        /// </summary>
        IShiftTurnOverReportView _view;

        /// <summary>
        /// Instance of Job Model
        /// </summary>
        JobModel _jobModel = new JobModel();

        /// <summary>
        /// Instance of Equipment Model
        /// </summary>
        EquipmentModel _equipmentModel = new EquipmentModel();

        #endregion

        #region [ Constructor ]

        public ShiftTurnOverReportPresenter(IShiftTurnOverReportView view)
        {
            _view = view;
        }

        #endregion

        #region [ Methods ]

        public void LoadReport()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.ClearReportViewer();

                    Dictionary<string, string> parameters = new Dictionary<string,string>();

                    if (_view.ReportView == Globals.ShiftTurnoverReport.ReportView.Job)
                    {
                        switch ((Globals.JobRecord.JobStatus)_view.JobStatusID)
                        {
                            case Globals.JobRecord.JobStatus.Active:
                                parameters.Add("Title", "Active Jobs Report");
                                _view.ActiveJobViewReportDataSource = _jobModel.ListActiveTurnoverReport();
                                break;
                            case Globals.JobRecord.JobStatus.Preset:
                                parameters.Add("Title", "Preset Jobs Report");
                                _view.JobViewPresetReportDataSource = _jobModel.FindTurnoverNonActive(_view.JobStatusID);
                                break;
                            case Globals.JobRecord.JobStatus.Potential:
                                parameters.Add("Title", "Potential Jobs Report");
                                _view.JobViewPresetReportDataSource = _jobModel.FindTurnoverNonActive(_view.JobStatusID);
                                break;
                            case Globals.JobRecord.JobStatus.PresetPurchase:
                                parameters.Add("Title", "Preset Purchase Jobs Report");
                                _view.JobViewPresetReportDataSource = _jobModel.FindTurnoverNonActive(_view.JobStatusID);
                                break;
                        }

                        parameters.Add("ReportViewType", ((int)Globals.ShiftTurnoverReport.ReportView.Job).ToString());

                        
                    }
                    else
                    {
                        //parameters.Add("Title", "Equipment Report");
                        //parameters.Add("ReportViewType", ((int)Globals.ShiftTurnoverReport.ReportView.QuickReference).ToString());
                        
                        _view.QuickReferenceReportDataSource = _equipmentModel.ListAllHeavyComboOrdered();
                    }

                    _view.ReportParameters = parameters;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the report.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the report.", false);
            }
        }

        public void ListJobStatus()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobStatusList = _jobModel.ListAllJobStatus();
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the job status list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the job status list.", false);
            }
        }

        #endregion
    }
}
