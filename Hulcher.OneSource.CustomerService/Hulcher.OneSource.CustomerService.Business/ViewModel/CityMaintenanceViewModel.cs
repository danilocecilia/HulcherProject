using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class CityMaintenanceViewModel
    {
        #region [ Attributes ]

        ICityMaintenanceView _view;

        LocationModel _model;

        #endregion

        #region [ Constructor ]

        public CityMaintenanceViewModel(ICityMaintenanceView view, LocationModel model)
        {
            _view = view;
            _model = model;
        }

        #endregion

        #region [ Methods ]

        public void FindCityList()
        {
            _view.CityListDataSource = _model.ListCityByState(_view.StateName);

            _view.EnableVisualizationPanel = true;
            _view.EnableCreationPanel = false;
        }

        public void BindCityRow()
        {
            _view.CityRowCityID = _view.CityRowDataItem.ID;
            _view.CityRowStateName = _view.CityRowDataItem.CS_State.Name;
            _view.CityRowCountry = _view.CityRowDataItem.CS_State.CS_Country.Name;
            _view.CityRowCSRecord = _view.CityRowDataItem.CSRecord;
        }

        public void CityListRowCommand()
        {
            if (_view.CityListRowCommandName == Globals.Common.GridView.GridCommandNames.Edit)
            {
                _view.ClearForm();
                _view.CityID = _view.CityListRowCommandArgument;
                _view.CityEntity = _model.GetCityByID(_view.CityListRowCommandArgument);
                _view.EnableCreationPanel = true;
                _view.EnableVisualizationPanel = false;
            }
            else
            {
                _model.RemoveCity(_view.CityListRowCommandArgument, _view.Username);
                FindCityList();
            }
        }

        public void SaveCity()
        {
            _model.SaveCity(_view.CityEntity, _view.ZipCodeList, _view.Username);

            FindCityList();
        }

        #endregion

        
    }
}
