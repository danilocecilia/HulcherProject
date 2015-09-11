using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Presenter class for the Default Page
    /// </summary>
    public class DefaultPagePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// View Interface object
        /// </summary>
        private IDefaultPageView _view;

        /// <summary>
        /// Class that contains the page logic
        /// </summary>
        private DefaultPageViewModel _viewModel;

        private JobModel _jobModel;
        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">View Interface object</param>
        public DefaultPagePresenter(IDefaultPageView view)
        {
            _view = view;
            _viewModel = new DefaultPageViewModel(view);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Check user permissions to enable/disable buttons
        /// </summary>
        public void CheckPermissions()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.Username, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.CreateJobRecord, Globals.Security.Operations.ManageCallCriteria, Globals.Security.Operations.FirstAlert, Globals.Security.Operations.Permiting, Globals.Security.Operations.Route });
                if (null != azOP && azOP.Length > 0)
                {
                    _view.EnableNewJobButton = azOP[0].Result;
                    _view.EnableNewCallCriteria = azOP[1].Result;
                    _view.EnableFirstAlertLink = azOP[2].Result;
                    _view.EnablePermitingNotification = azOP[3].Result;
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to verify User Permissions to enable New Job Button!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to verify User Permissions to enable New Job Button.", false);
            }
        }

        /// <summary>
        /// Search for a Job based on Number/Internal Tracking
        /// </summary>
        public void QuickSearch()
        {
            try
            {
                CS_Job job = null;
                using (_jobModel = new JobModel())
                {
                    if (!string.IsNullOrEmpty(_view.QuickSearchJobValue))
                        job = _jobModel.GetJobByNumber(_view.QuickSearchJobValue);
                }

                if (job != null)
                {
                    _view.QuickSearchJobId = job.ID;

                    WebUtil util = new WebUtil();
                    string querystring = util.BuildQueryStringToQuickSearch(_view);

                    string script = util.BuildNewWindowClientScript("/JobRecord.aspx", querystring, string.Empty, 870, 600, true, true, false);
                    _view.QuickSearchQueryString = script;
                }
                else
                {
                    _view.DisplayMessage("Job Not Found!", false);
                }
            }
            catch (Exception)
            {
                _view.DisplayMessage("There is an error searching the Job", false);
            }
        }

        #region [ Job Search Criteria ]

        public void SearchJob()
        {
            _viewModel.SearchJob();
        }
        #endregion

        #endregion

        public void Load()
        {
            _viewModel.FillSearchFields();
        }
    }
}
