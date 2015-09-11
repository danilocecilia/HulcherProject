using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class JobCallLogPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Job Record View Interface
        /// </summary>
        private IJobCallLogView _view;

        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        public JobCallLogPresenter(IJobCallLogView view)
        {
            this._view = view;
            _callLogModel = new CallLogModel();
        }

        public JobCallLogPresenter(IJobCallLogView view, IUnitOfWork unitOfWork)
        {
            this._view = view;
            this._callLogModel = new CallLogModel(unitOfWork);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Method for populating the DataSource with all Call Logs for a specific Job,
        /// filtered accordingly to a User inputed Parameter
        /// </summary>
        public void ListFilteredCallLogs()
        {
            if (null != _view.JobId)
            {
                try
                {
                    using (_callLogModel = new CallLogModel())
                    {
                        if (string.Empty == _view.FilterValue)
                        {
                            _view.DataSource = _callLogModel.ListAllJobCallLogs(_view.JobId.Value).OrderBy(e => e.CallDate).ToList();
                        }
                        else
                        {
                            _view.DataSource = _callLogModel.ListFilteredCallLogs(_view.FilterType, _view.FilterValue, _view.JobId.Value).OrderBy(e => e.CallDate).ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("There was an error loading the Filtered Call Log List!\n{0}\n{1}", ex.Message, ex.StackTrace));
                    _view.DisplayMessage("There was an error loading the Filtered Call Log List", false);
                }
            }
        }

        public void ValidateCallLogGridLinkButtonVisibility(CS_CallLog callLog)
        {
            try
            {   
                _view.SetCallLogGridViewHyperLinkVisibility(!callLog.CS_CallType.IsAutomaticProcess, "hlUpdate");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error validating the visibility for the Call log Grid Link Button!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading the information! Please try again.", false);
            }
        }

        #endregion
    }
}
