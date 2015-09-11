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
    public interface IContactDao : IBaseDao<CS_Contact, int>
    {
        IList<CS_Contact> ListAllFiltered(long customerId, bool dynamicsContact);
        
        IList<CS_Contact> ListFilteredByName(long customerId, bool dynamicsContact, string name);

        IList<CS_Contact> ListAllFilteredByName(bool dynamicsContact, string name);

        CS_Contact GetByNumber(string contactNumber);
    }
}
