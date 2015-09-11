using System.Collections.Generic;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the City Dao Class
    /// </summary>
    public interface ICityDao : IBaseDao<CS_City, int>
    {
        /// <summary>
        /// Get cities by state
        /// </summary>
        /// <param name="stateId">state id</param>
        /// <returns>city list</returns>
        IList<CS_City> GetCityByState(int stateId);
    }
}
