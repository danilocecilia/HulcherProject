using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Map Plotting Presenter
    /// </summary>
    public class MapPlottingPresenter
    {
        #region [ Attributes ]

        private IMapPlottingView _view;

        private DivisionModel _divisionModel;
        private JobModel _jobModel;
        private EquipmentModel _equipmentModel;
        private RegionModel _regionModel;

        #endregion

        #region [ Constructor ]

        public MapPlottingPresenter(IMapPlottingView view)
        {
            _view = view;
            _divisionModel = new DivisionModel();
            _jobModel = new JobModel();
            _equipmentModel = new EquipmentModel();
            _regionModel = new RegionModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Load ]

        public void LoadPage()
        {
            LoadDivisions();
            LoadJobActions();
            LoadJobCategories();
            LoadPriceTypes();
            LoadEquipmentTypes();
            LoadRegions();
        }

        #endregion

        #region [ Filter ]

        #region [ Multiselects ]

        public void LoadDivisions()
        {
            _view.DivisionDataSource = _divisionModel.ListAllDivision();
        }

        public void LoadJobActions()
        {
            _view.JobActionDataSource = _jobModel.ListAllJobActions();
        }

        public void LoadJobCategories()
        {
            _view.JobCategoryDataSource = _jobModel.ListAllJobCategories();
        }

        public void LoadPriceTypes()
        {
            _view.PriceTypeDataSource = _jobModel.ListAllPriceTypes();
        }

        public void LoadEquipmentTypes()
        {
            _view.EquipmentTypeDataSource = _equipmentModel.ListAllEquipmentType();
        }

        public void LoadRegions()
        {
            _view.RegionDataSource = _regionModel.ListAllRegions();
        }

        #endregion

        #endregion

        #endregion
    }
}
