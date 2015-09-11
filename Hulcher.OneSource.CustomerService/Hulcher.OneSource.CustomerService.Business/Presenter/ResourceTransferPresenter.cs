using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ResourceTransferPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// View Instance
        /// </summary>
        IResourceTransferView _view;

        /// <summary>
        /// ViewModel instance
        /// </summary>
        ResourceTransferViewModel _viewModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">View instance</param>
        public ResourceTransferPresenter(IResourceTransferView view)
        {
            _view = view;
            _viewModel = new ResourceTransferViewModel(_view);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void TransferResources()
        {
            try
            {
                _viewModel.TransferResources();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Transfer the resources!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Transfer the resources (Presenter - TransferResources Method).", false);
            }
        }

        /// <summary>
        /// List Resources on page load
        /// </summary>
        public void LoadPage()
        {
            try
            {
                _viewModel.LoadResources();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Resources Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Resources Information (Presenter - LoadResources Method).", false);
            }
        }

        /// <summary>
        /// Fill the fields for the resources repeater
        /// </summary>
        public void LoadResourceRepeaterRow()
        {
            try
            {
                _viewModel.SetResourceRowData();
                _viewModel.LoadJobCallLogByResources();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Resouces/CallLog Repeater!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Resouces/CallLog Repeater (Presenter - LoadResourceRepeaterRow Method).", false);
            }
        }

        /// <summary>
        /// Fill the fieds for the call log repeater
        /// </summary>
        public void LoadJobCallLogRepeaterRow()
        {
            try
            {
                _viewModel.SetJobCallLogRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load JobCallLog fields Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load JobCallLog fields Information (Presenter - LoadJobCallLogRepeaterRow Method).", false);
            }
        }

        #endregion
    }
}
