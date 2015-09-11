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
    public class JobDescriptionPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the View Interface
        /// </summary>
        private IJobDescriptionView _view;

        /// <summary>
        /// Instance of the Job Model class
        /// </summary>
        private JobModel _model;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CustomerInfo View Interface</param>
        public JobDescriptionPresenter(IJobDescriptionView view)
        {
            this._view = view;
            this._model = new JobModel();
        }

        public JobDescriptionPresenter(IJobDescriptionView view, JobModel jobModel)
        {
            this._view = view;
            this._model = jobModel;
        }

        #endregion

        #region [ Methods ]

        public void AddScopeOfWork()
        {
            _view.ScopeOfWorkEntity.Active = true;
            _view.ScopeOfWorkEntity.CreationDate = DateTime.Now;

            _view.ScopeOfWorkList.Add(_view.ScopeOfWorkEntity);

            _view.ListScopeOfWork();
            _view.ClearFields();
        }

        public void LoadScopeOfWork()
        {
            try
            {
                if (_view.JobId.HasValue)
                    _view.ScopeOfWorkList = _model.GetScopeOfWorkByJobId(_view.JobId.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the ScopeOfWork information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the ScopeOfWork information. Please try again.", false);
            }
        }

        #endregion
    }
}
