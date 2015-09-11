using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace Hulcher.OneSource.CustomerService.Data
{
    /// <summary>
    /// Customer Contact Dao Class
    /// </summary>
    public class CustomerContractDao : BaseDao<CS_CustomerContract, int>, ICustomerContractDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific contract by CustomerNumber
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, string, IQueryable<CS_CustomerContract>> _getByNumberQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all contracts of a specific customer
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_CustomerContract>> _listAllByCustomerQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICustomerContractDao _singleton;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class contructor
        /// </summary>
        public CustomerContractDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICustomerContractDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CustomerContractDao();

                return _singleton;
            }
        }


        #endregion

        #region [ ICustomerContractDao Implementation ]

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        public CS_CustomerContract GetByNumber(string customerNumber, string contractNumber)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_CustomerContract returnList = GetByNumberQuery.Invoke(db, customerNumber, contractNumber) as CS_CustomerContract;
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, string, string, IQueryable<CS_CustomerContract>> GetByNumberQuery
        {
            get
            {
                if (null == _getByNumberQuery)
                {
                    _getByNumberQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string customerNumber, string contractNumber) => from e in ctx.CS_CustomerContract
                                                                                                             from c in ctx.CS_Customer
                                                                                                             where e.CustomerID == c.ID
                                                                                                             && c.CustomerNumber == customerNumber
                                                                                                             && e.ContractNumber == contractNumber
                                                                                                             select e);

                }
                return _getByNumberQuery;
            }
        }

        /// <summary>
        /// Returns a list of all active contracts related to a specific customer
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>List of active contracts</returns>
        public IList<CS_CustomerContract> ListAllByCustomer(long customerId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_CustomerContract> returnList = ListAllByCustomerQuery.Invoke(db, customerId).ToList<CS_CustomerContract>();
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List All contracts of a specific customer
        /// </summary>
        public Func<CustomerServiceModelContainer, long, IQueryable<CS_CustomerContract>> ListAllByCustomerQuery
        {
            get
            {
                if (null == _listAllByCustomerQuery)
                {
                    _listAllByCustomerQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long customerId) => from e in ctx.CS_CustomerContract
                                                                                where e.CustomerID == customerId
                                                                                && e.Active == true
                                                                                select e);

                }
                return _listAllByCustomerQuery;
            }
        }

        /// <summary>
        /// Clear all items of the CS_CustomerContract table (For testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                var entityConnection = (System.Data.EntityClient.EntityConnection)db.Connection;
                DbConnection conn = entityConnection.StoreConnection;
                ConnectionState initialState = conn.State;
                try
                {
                    if (initialState != ConnectionState.Open)
                        conn.Open();  // open connection if not already open             
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM CS_CustomerContract";
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close(); // only close connection if not initially open         
                }    
            }
        }

        #endregion
    }
}
