using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Data;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Contact Dao Class
    /// </summary>
    public interface ICustomerDao : IBaseDao<CS_Customer, long>
    {
        /// <summary>
        /// Searches a Customer by its number
        /// </summary>
        CS_Customer GetByNumber(string number);

        IList<CS_Customer> ListCustomerByName(string name);
    }
}
