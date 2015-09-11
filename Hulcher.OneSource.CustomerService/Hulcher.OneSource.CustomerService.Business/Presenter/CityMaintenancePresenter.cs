using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class CityMaintenancePresenter
    {
        #region [ Attributes ]

        ICityMaintenanceView _view;

        LocationModel _model;

        CityMaintenanceViewModel _viewModel;

        #endregion

        #region [ Constructor ]

        public CityMaintenancePresenter(ICityMaintenanceView view)
        {
            _view = view;
            _model = new LocationModel();
            _viewModel = new CityMaintenanceViewModel(_view, _model);
        }

        #endregion

        #region [ Methods ]

        public void FindCityList()
        {
            try
            {
                _viewModel.FindCityList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to list Cities!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to list Cities.", false);
            }
        }

        public void BindCityRow()
        {
            try
            {
                _viewModel.BindCityRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load City Data (row)!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load City Data.", false);
            }
        }

        public void CityListRowCommand()
        {
            try
            {
                _viewModel.CityListRowCommand();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to execute the command!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to execute the command.", false);
            }
        }

        public void AddCity()
        {
            try
            {
                _view.CityEntity = null;
                _view.ClearForm();
                _view.EnableCreationPanel = true;
                _view.EnableVisualizationPanel = false;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to prepare the form to add a new City!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to prepare the form to add a new City.", false);
            }
        }

        public void SaveCity()
        {
            try
            {
                _viewModel.SaveCity();
                _view.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save City information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save City information.", false);
            }
        }

        #endregion
    }
}
