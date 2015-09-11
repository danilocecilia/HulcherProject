using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data;


namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
     ///<summary>
     ///Interface for the Customer Contract Dao class
     ///</summary>
    public interface ICustomerContractDao : IBaseDao<CS_CustomerContract, int>
    {
        CS_CustomerContract GetByNumber(string customerNumber, string contractNumber);

        IList<CS_CustomerContract> ListAllByCustomer(long customerId);

        void ClearAll();
    }
}
