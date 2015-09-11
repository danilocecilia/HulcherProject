using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
	public interface IStateDao : IBaseDao<CS_State, int>
	{
        /// <summary>
        /// Returns a list of states of a chosen country
        /// </summary>
        /// <param name="countryId">country id</param>
        /// <returns>state list</returns>
	    IList<CS_State> GetStateByCountry(int countryId);

        /// <summary>
        /// Returns a list of states, ordered by country
        /// </summary>
        /// <returns></returns>
        IList<CS_State> ListAllByCountry();
	}
}
