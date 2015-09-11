using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class LocationModel : IDisposable
    {
        #region [ Atributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Country
        /// </summary>
        private ICachedRepository<CS_Country> _countryRepository;

        /// <summary>
        /// Repository class for CS_City
        /// </summary>
        private IRepository<CS_City> _cityRepository;

        /// <summary>
        /// Repository class for CS_State
        /// </summary>
        private ICachedRepository<CS_State> _stateRepository;

        /// <summary>
        /// Repository class for CS_Zipcode
        /// </summary>
        private ICachedRepository<CS_ZipCode> _zipCodeRepository;

        /// <summary>
        /// Repository class for CS_Route
        /// </summary>
        private IRepository<CS_Route> _routeRepository;

        #endregion

        #region [ Constructors ]

        public LocationModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _countryRepository = new CachedRepository<CS_Country> { UnitOfWork = _unitOfWork };

            _stateRepository = new CachedRepository<CS_State> { UnitOfWork = _unitOfWork };

            _cityRepository = new EFRepository<CS_City> { UnitOfWork = _unitOfWork };

            _zipCodeRepository = new CachedRepository<CS_ZipCode> { UnitOfWork = _unitOfWork };

            _routeRepository = new EFRepository<CS_Route> { UnitOfWork = _unitOfWork };
        }

        public LocationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _countryRepository = new CachedRepository<CS_Country> { UnitOfWork = _unitOfWork };

            _stateRepository = new CachedRepository<CS_State> { UnitOfWork = _unitOfWork };

            _cityRepository = new EFRepository<CS_City> { UnitOfWork = _unitOfWork };

            _zipCodeRepository = new CachedRepository<CS_ZipCode> { UnitOfWork = _unitOfWork };

            _routeRepository = new EFRepository<CS_Route> { UnitOfWork = _unitOfWork };
        }

        #endregion

        #region [ Methods ]

        #region [ Country ]

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Country> ListAllCountries()
        {

            return _countryRepository.ListAll(w => w.Active);
        }

        /// <summary>
        /// Return the country id by state
        /// </summary>
        /// <param name="stateId">state id</param>
        /// <returns>int</returns>
        public int GetCountryByStateId(int stateId)
        {
            return _stateRepository.Get(w => w.Active && w.ID == stateId).CountryID;
        }

        #endregion

        #region [ State ]

        /// <summry>
        /// List all itens of an entity in the database
        /// </summary>
        /// <returns></returns>
        public IList<CS_State> ListAllStates()
        {
            return _stateRepository.ListAll(w => w.Active, a => a.Name, true);
        }

        /// <summary>
        /// List all items of an entity ordered by country id
        /// </summary>
        /// <returns></returns>
        public IList<CS_State> ListAllStatesOrderedByCountry()
        {
            return _stateRepository.ListAll(w => w.Active, e => e.CS_Country.Name, true);
        }

        /// <summary>
        /// List all States by country id
        /// </summary>
        /// <param name="countryId">country id</param>
        /// <returns>country list</returns>
        public IList<CS_State> GetStateByCountryId(int countryId)
        {
            return _stateRepository.ListAll(w => w.Active && w.CountryID == countryId);
        }

        /// <summary>
        /// Returns a state based on State Identifier
        /// </summary>
        /// <param name="stateId">State Identifier</param>
        /// <returns>State info</returns>
        public CS_State GetState(int stateId)
        {
            return _stateRepository.Get(w => w.Active && w.ID == stateId);
        }

        /// <summary>
        /// Returns a state based on a City Identifier
        /// </summary>
        /// <param name="cityId">City Identifier</param>
        /// <returns>State Info</returns>
        public CS_State GetStateByCityId(int cityId)
        {
            return _stateRepository.Get(w => w.Active && w.CS_City.Any(e => e.ID == cityId));
        }

        /// <summary>
        /// Returns a list of States based on name
        /// </summary>
        /// <param name="prefixText">Prefix used to find by Name (like)</param>
        /// <returns>List of States</returns>
        public IList<CS_State> ListAllStatesByName(string prefixText)
        {
            return _stateRepository.ListAll(
                e => e.Active && e.Name.ToLower().Contains(prefixText.ToLower()),
                order => order.Name,
                true);
        }

        /// <summary>
        /// Returns a list of States based on their Acronym
        /// </summary>
        /// <param name="prefixText">Prefix used to find by Acronym (Equals)</param>
        /// <returns>List of States</returns>
        public IList<CS_State> ListAllStatesByAcronym(string prefixText)
        {
            return ListAllStatesByAcronym(prefixText, string.Empty);
        }

        /// <summary>
        /// Returns a list of States based on their Acronym
        /// </summary>
        /// <param name="prefixText">Prefix used to find by Acronym (Equals)</param>
        /// <returns>List of States</returns>
        public IList<CS_State> ListAllStatesByAcronym(string prefixText, string contextKey)
        {
            int? country = null;
            int parse;

            if (int.TryParse(contextKey, out parse))
                if (parse != 0)
                    country = parse;

            return _stateRepository.ListAll(e => e.Active && (e.Acronym.ToLower().Equals(prefixText.ToLower()) || e.Name.ToLower().Contains(prefixText.ToLower())) && (!country.HasValue || e.CountryID == country.Value), order => order.Name, true, "CS_Country");
        }

        /// <summary>
        /// Returns a list of States based on their Acronym
        /// </summary>
        /// <param name="prefixText">Prefix used to find by Acronym (Equals)</param>
        /// <returns>List of States</returns>
        public IList<CS_State> ListAllStatesByAcronymAndDivision(string prefixText)
        {
            return _stateRepository.ListAll(e => e.Active && (e.CS_Country.Name.Trim().ToLower() + " - " + e.Acronym.ToLower() + " - " + e.Name.ToLower()).Contains(prefixText.ToLower()) && e.CS_Division.Any(f => f.Active), "CS_Country").OrderBy(e => e.CS_Country.Name).OrderBy(e => e.Acronym).OrderBy(e => e.Name).ToList();
        }

        /// <summary>
        /// Returns a list of States based if they have a division tied to it
        /// </summary>
        /// <returns>List of States</returns>
        public IList<CS_State> ListAllStatesByAllocatedDivision()
        {
            return _stateRepository.ListAll(e => e.Active && e.CS_Division.Any(f => f.Active), "CS_Country").OrderBy(e => e.CS_Country.Name).OrderBy(e => e.Acronym).OrderBy(e => e.Name).ToList();
        }

        public IList<CS_State> ListStatesByIds(List<int> stateIds)
        {
            return _stateRepository.ListAll(w => w.Active && stateIds.Contains(w.ID), a => a.Name, true);
        }

        #endregion

        #region [ City ]

        /// <summary>
        /// List all itens of an Entity in the database
        /// </summary>
        /// <returns></returns>
        public IList<CS_City> ListAllCities()
        {
            return _cityRepository.ListAll(w => w.Active);
        }

        /// <summary>
        /// List of cities by state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>city list</returns>
        public IList<CS_City> GetCityByState(int stateId)
        {
            return _cityRepository.ListAll(w => w.Active && w.StateID == stateId);
        }

        /// <summary>
        /// List of cities by state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>city list</returns>
        public IList<CS_City> ListCityByState(string state)
        {
            if (!string.IsNullOrEmpty(state))
                return _cityRepository.ListAll(w => w.Active && (w.CS_State.Name + " " + w.CS_State.Acronym).Contains(state), order => order.Name, true, "CS_State", "CS_State.CS_Country");
            else
                return new List<CS_City>();
        }

        /// <summary>
        /// Returns a list of city based on state and name
        /// </summary>
        /// <returns>List of City</returns>
        public IList<CS_City> ListCityByNameAndState(int stateId, string name)
        {
            return _cityRepository.ListAll(w => w.Active && (w.StateID == stateId || stateId == 0) && (w.Name.ToLower().StartsWith(name.ToLower()) || (w.Name.ToLower() + " - " + w.CS_State.Name.ToLower()).StartsWith(name.ToLower())),
                order => order.Name,
                true, "CS_State");
        }

        public IList<CS_City> ListCitiesByIds(List<int> cityIds)
        {
            return _cityRepository.ListAll(w => w.Active && cityIds.Contains(w.ID));
        }

        /// <summary>
        /// Saves or Updates a City
        /// </summary>
        /// <param name="city">City entity</param>
        /// <param name="zipCodeList">Zip code listing</param>
        /// <param name="username">Username</param>
        public void SaveCity(CS_City city, IList<CS_ZipCode> zipCodeList, string username)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                city.ModificationDate = DateTime.Now;
                //city.ModificationID
                city.ModifiedBy = username;
                city.CSRecord = true;
                city.Active = true;

                for (int i = 0; i < zipCodeList.Count; i++)
                {
                    zipCodeList[i].ModificationDate = DateTime.Now;
                    //zipCodeList[i].ModificationID
                    zipCodeList[i].ModifiedBy = username;
                    zipCodeList[i].Active = true;
                }

                if (city.ID == 0)
                {
                    city.CreationDate = DateTime.Now;
                    //city.CreationID
                    city.CreatedBy = username;

                    _cityRepository.Add(city);

                    for (int i = 0; i < zipCodeList.Count; i++)
                    {
                        zipCodeList[i].CityId = city.ID;
                        zipCodeList[i].CreationDate = DateTime.Now;
                        //zipCodeList[i].CreationID
                        zipCodeList[i].CreatedBy = username;
                    }

                    _zipCodeRepository.AddList(zipCodeList);
                }
                else
                {
                    _cityRepository.Update(city);

                    List<CS_ZipCode> addList = new List<CS_ZipCode>();
                    List<CS_ZipCode> updateList = new List<CS_ZipCode>();
                    List<int> currentUpdateList = zipCodeList.Where(e => e.ID != 0).Select(e => e.ID).ToList();
                    IList<CS_ZipCode> removeList = _zipCodeRepository.ListAll(e => e.Active && e.CityId == city.ID && !currentUpdateList.Contains(e.ID));

                    for (int i = 0; i < zipCodeList.Count; i++)
                    {
                        if (zipCodeList[i].ID == 0)
                        {
                            addList.Add(new CS_ZipCode()
                            {
                                CityId = city.ID,
                                Name = zipCodeList[i].Name,
                                Latitude = zipCodeList[i].Latitude,
                                Longitude = zipCodeList[i].Longitude,
                                CreatedBy = username,
                                CreationDate = DateTime.Now,
                                //CreationID
                                ModifiedBy = username,
                                ModificationDate = DateTime.Now,
                                //ModificationID
                                Active = true
                            });
                        }
                        else
                        {
                            updateList.Add(new CS_ZipCode()
                            {
                                ID = zipCodeList[i].ID,
                                CityId = city.ID,
                                Name = zipCodeList[i].Name,
                                Latitude = zipCodeList[i].Latitude,
                                Longitude = zipCodeList[i].Longitude,
                                CreatedBy = zipCodeList[i].CreatedBy,
                                CreationDate = zipCodeList[i].CreationDate,
                                //CreationID = zipCodeList[i].CreationID,
                                ModifiedBy = username,
                                ModificationDate = DateTime.Now,
                                //ModificationID
                                Active = true
                            });
                        }
                    }

                    if (addList.Count > 0) _zipCodeRepository.AddList(addList);
                    if (updateList.Count > 0) _zipCodeRepository.UpdateList(updateList);
                    if (removeList.Count > 0)
                    {
                        for (int i = 0; i < removeList.Count; i++)
                        {
                            removeList[i].ModificationDate = DateTime.Now;
                            removeList[i].ModifiedBy = username;
                            removeList[i].Active = false;
                        }

                        _zipCodeRepository.UpdateList(removeList);
                    }
                }

                scope.Complete();
            }
        }

        public void RemoveCity(int cityID, string username)
        {
            CS_City city = GetCityByID(cityID);

            city.Active = false;
            city.ModificationDate = DateTime.Now;
            city.ModifiedBy = username;

            _cityRepository.Update(city);
        }

        public CS_City GetCityByID(int cityID)
        {
            return _cityRepository.Get(e => e.Active && e.ID == cityID, "CS_State", "CS_State.CS_Country", "CS_ZipCode");
        }

        #endregion

        #region [ Zip Code ]

        /// <summary>
        /// List of zipcode by city
        /// </summary>
        /// <param name="cityId">city id</param>
        /// <returns>zipcode list</returns>
        public IList<CS_ZipCode> GetZipCodeByCityId(int cityId)
        {
            return _zipCodeRepository.ListAll(w => w.Active && w.CityId == cityId);
        }

        /// <summary>
        /// Returns a list of Zipcode based on City and name
        /// </summary>
        /// <param name="cityId">City Identifier (0 for all)</param>
        /// <param name="prefixText">Prefix used to find by name (like)</param>
        /// <returns>list of ZipCodes</returns>
        public IList<CS_ZipCode> ListZipCodeByNameAndCity(int cityId, string prefixText)
        {
            return _zipCodeRepository.ListAll(
                e => e.Active && (e.CityId == cityId || cityId == 0) && e.ZipCodeNameEdited.ToLower().Contains(prefixText.ToLower()),
                order => order.ZipCodeNameEdited,
                true);
        }

        /// <summary>
        /// Returns a list of all active Zipcodes in the Database
        /// </summary>
        /// <returns></returns>
        public IList<CS_ZipCode> ListAllZipCodes()
        {
            return _zipCodeRepository.ListAll(e => e.Active, order => order.ZipCodeNameEdited, true);
        }

        #endregion

        #region [ Route ]

        /// <summary>
        /// List All Routes by city
        /// </summary>
        /// <param name="cityID">city identifier</param>
        /// <returns>Interface List of CS_Route entities</returns>
        public IList<CS_Route> ListAllRoutes(int? stateId, int? cityID, int? zipCodeId)
        {
            return _routeRepository.ListAll(
                e => e.Active && 
                     (e.CS_City.CS_State.ID == stateId.Value || !stateId.HasValue) && 
                     (e.CityID == cityID.Value || !cityID.HasValue) && 
                     (e.ZipCodeID == zipCodeId.Value || !zipCodeId.HasValue ), 
                orderby => orderby.Hours, 
                true, 
                "CS_City", "CS_City.CS_State", "CS_Division", "CS_ZipCode");
        }

        /// <summary>
        /// Saves or updates a route
        /// </summary>
        /// <param name="route">Route entity</param>
        /// <param name="user">action user</param>
        public void SaveUpdateRoute(IList<CS_Route> routeList, string user)
        {
            try
            {
                if (null != routeList && routeList.Count > 0)
                {
                    DateTime ActionDate = DateTime.Now;
                    IList<CS_Route> routeListAdd = new List<CS_Route>();
                    IList<CS_Route> routeListUpdate = new List<CS_Route>();

                    for (int i = 0; i < routeList.Count; i++)
                    {
                        CS_Route route = routeList[i];

                        if (route.ID.Equals(0))
                        {
                            route.CreatedBy = user;
                            route.ModifiedBy = user;
                            route.CreationDate = ActionDate;
                            route.ModificationDate = ActionDate;
                            route.Active = true;

                            routeListAdd.Add(route);
                        }
                        else
                        {
                            route.ModifiedBy = user;
                            route.ModificationDate = ActionDate;
                            route.Active = true;

                            routeListUpdate.Add(route);
                        }
                    }

                    if (routeListAdd.Count > 0)
                        _routeRepository.AddList(routeListAdd);

                    if (routeListUpdate.Count > 0)
                        _routeRepository.UpdateList(routeListUpdate);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to save Route information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while save Route information", ex);
            }
        }

        public void RemoveRoute(int RouteID, string username)
        {
            CS_Route route = _routeRepository.Get(e => e.ID == RouteID);

            route.Active = false;
            route.ModificationDate = DateTime.Now;
            route.ModifiedBy = username;

            _routeRepository.Update(route);
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]
        public void Dispose()
        {
            _cityRepository = null;
            _countryRepository = null;
            _stateRepository = null;
            _zipCodeRepository = null;
            _routeRepository = null;
            _unitOfWork.Dispose();
            _unitOfWork = null;
        }
        #endregion
    }
}
