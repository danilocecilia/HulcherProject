using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CustomerDao : BaseDao<CS_Customer, long>, ICustomerDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Customer, filtered by Number
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Customer>> _getByNumberQuery; 

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Customer, filtered by Name
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Customer>> _ListCustomerByNameQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICustomerDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public CustomerDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICustomerDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CustomerDao();

                return _singleton;
            }
        }

        #endregion

        #region [ ICustomerDao Implementation ]

        /// <summary>
        /// Executes the BulkCopy process to load Customer Information from Dynamics to a local Table
        /// </summary>
        /// <param name="allEmployees">IDataReader that contains all Customer loaded from Dynamics</param>
        public IList<CS_Customer> ListCustomerByName(string name)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                List<CS_Customer> returnItem = ListCustomerByNameQuery.Invoke(db, name).ToList<CS_Customer>();
                return returnItem;
            }
        }

        #endregion

        #region [ Properties ]

        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Customer>> GetByNumberQuery
        {
            get
            {
                if (null == _getByNumberQuery)
                {
                    _getByNumberQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string customerNumber) => from e in ctx.CS_Customer
                                                                                      where e.CustomerNumber == customerNumber
                                                                                      select e);
                }
                return _getByNumberQuery;
            }
        }

        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Customer>> ListCustomerByNameQuery
        {
            get
            {
                if (null == _ListCustomerByNameQuery)
                {
                    _ListCustomerByNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string customerName) => from e in ctx.CS_Customer
                                                                                      where e.Active == true && e.Name.StartsWith(customerName.Trim())
                                                                                      select e);
                }
                return _ListCustomerByNameQuery;
            }
        }

        #endregion

        #region [ Methods ]

        public CS_Customer GetByNumber(string customerNumber)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_Customer returnItem = GetByNumberQuery.Invoke(db, customerNumber).FirstOrDefault<CS_Customer>();
                return returnItem;
            }
        }

        #endregion
    }
}
