using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IZipCodeDao : IBaseDao<CS_ZipCode, int>
    {
        /// <summary>
        /// Get list zip code by City
        /// </summary>
        /// <param name="cityId">city id</param>
        /// <returns>zipcode list</returns>
        IList<CS_ZipCode> GetZipCodeByCity(int cityId);
    }
}
