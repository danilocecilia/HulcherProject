using System;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Essential Job Data Presenter
    /// </summary>
    public class EssentialJobDataPresenter
    {
        #region [ Attributes ]

        private IEssentialJobDataView _view;

        private EssentialJobDataViewModel _essentialJobDataViewModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Essential Job Data View Interface</param>
        public EssentialJobDataPresenter(IEssentialJobDataView view)
        {
            this._view = view;
            _essentialJobDataViewModel = new EssentialJobDataViewModel(view);
        }
        #endregion

        #region [ Methods ]

        public void SaveJobData()
        {
            try
            {
                _essentialJobDataViewModel.SaveJobData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Save the new Job!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Save the new Job.", false);
                _view.SavedSuccessfuly = false;
            }
        }

        #endregion
    }
}
