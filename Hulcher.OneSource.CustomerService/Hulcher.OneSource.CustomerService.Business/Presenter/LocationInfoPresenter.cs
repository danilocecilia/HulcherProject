using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class LocationInfoPresenter
    {

        /// <summary>
        /// Instance of the Job Record View Interface
        /// </summary>
        private ILocationInfoView _view;

        /// <summary>
        /// Instance of the Location Business Class
        /// </summary>
        private LocationModel _model;

        private JobModel _jobModel;

        public LocationInfoPresenter(ILocationInfoView view)
        {
            this._view = view;
            this._model = new LocationModel();
            this._jobModel = new JobModel();
        }

        public LocationInfoPresenter(ILocationInfoView view, JobModel jobModel)
        {
            this._view = view;
            this._jobModel = jobModel;
        }

        /// <summary>
        /// List All Countries in the Database
        /// </summary>
        public void ListAllCountries()
        {
            try
            {
                _view.CountryList = _model.ListAllCountries();
            }
            catch (Exception)
            {

                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        /// <summary>
        /// List all cities in the database
        /// </summary>
        public void ListAllCities()
        {
            try
            {
                _view.CityList = _model.ListAllCities();
            }
            catch (Exception)
            {
                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        /// <summary>
        /// List all states in the database
        /// </summary>
        public void ListAllStates()
        {
            try
            {
                _view.StateList = _model.ListAllStates();
            }
            catch (Exception)
            {
                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        /// <summary>
        /// List states by country
        /// </summary>
        public void StateListByCountryId()
        {
            try
            {
                _view.StateListByCountryId = _model.GetStateByCountryId(_view.CountryValue);
            }
            catch (Exception)
            {
                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        /// <summary>
        /// List city by state 
        /// </summary>
        public void CitiesByStateId()
        {
            try
            {
                _view.CityListByStateId = _model.GetCityByState(_view.StateValue);
            }
            catch (Exception)
            {
                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        /// <summary>
        /// List zipcode by city
        /// </summary>
        public void ZipCodeByCity()
        {
            try
            {
                _view.ZipCodeListByCityId = _model.GetZipCodeByCityId(_view.CityValue);
            }
            catch (Exception)
            {
                _view.DisplayMessage("An error ocurred!", false);
            }
        }

        //public virtual void GetLocationInfoByJobId()
        //{
        //    try
        //    {
        //        if (_view.JobId.HasValue)
        //            _view.LocationInfoEntity = _jobModel.GetLocationInfoByJobId(_view.JobId.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(string.Format("An error has ocurred while trying to load the Location information!\n{0}\n{1}", ex.Message, ex.StackTrace));
        //        _view.DisplayMessage("An error ocurred while trying to load the Location information. Please try again.", false);
        //    }
        //}

        //public void LoadLocationInfoCloningData()
        //{
        //    try
        //    {
        //        if (_view.CloningId.HasValue)
        //            _view.LocationInfoEntity = _jobModel.GetLocationInfoByJobId(_view.CloningId.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(string.Format("An error has ocurred while trying to load the Location information!\n{0}\n{1}", ex.Message, ex.StackTrace));
        //        _view.DisplayMessage("An error ocurred while trying to load the Location information. Please try again.", false);
        //    }
        //}
    }
}
