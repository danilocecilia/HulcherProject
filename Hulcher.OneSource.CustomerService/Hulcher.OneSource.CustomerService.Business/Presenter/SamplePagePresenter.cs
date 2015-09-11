using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Sample Page Presenter Class
    /// </summary>
    public class SamplePagePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Sample Page View Interface
        /// </summary>
        private ISamplePageView _view;

        /// <summary>
        /// Instance of the Job Business Class
        /// </summary>
        private JobModel _model;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Sample Page View Interface</param>
        public SamplePagePresenter(ISamplePageView view)
        {
            this._view = view;
            this._model = new JobModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// List All Jobs in the Database
        /// </summary>
        public void ListAllJobs()
        {
            try
            {
                _view.JobList = _model.ListAllJobs();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to list all job records!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to list all job records", false);
            }
        }

        /// <summary>
        /// Deletes a Job Record
        /// </summary>
        public void DeleteJob()
        {
            try
            {
                _model.DeleteJob(_view.JobId, _view.Username);
                ListAllJobs();
            }
            catch (Exception ex)
            {                
                Logger.Write(string.Format("There was an error while trying to delete a job records!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to delete a job records", false);
            }
        }

        #endregion
    }
}
