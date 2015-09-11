using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CallTypeDao : BaseDao<CS_CallType, int>, ICallTypeDao
    {

        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_CallType>> _getByPrimaryCallTypeQuery;

        /// <summary>
        /// Singleton Attribute
        /// </summary>
        private static ICallTypeDao _singleton;

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property for the CallTypeDao
        /// </summary>
        public static ICallTypeDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CallTypeDao();

                return _singleton;
            }
        }
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for a Query that filters CallTypes by PrimaryCallType
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_CallType>> GetByPrimaryCallTypeQuery
        {
            get 
            {
                if (null == _getByPrimaryCallTypeQuery)
                {
                    _getByPrimaryCallTypeQuery = 
                        CompiledQuery.Compile((CustomerServiceModelContainer ctx, int primaryCallTypeId) => from e in ctx.CS_CallType
                                                                                                            //where e.Active
                                                                                                            //&& e.PrimayCallTypeID == primaryCallTypeId
                                                                                                            select e);
                }
                return _getByPrimaryCallTypeQuery;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Filters the CallTypes by PrimaryCallType ID
        /// </summary>
        /// <param name="primaryCallTypeId">ID of the PrimaryCallType</param>
        /// <returns>List of CallTypes related to that ID</returns>
        public List<CS_CallType> FilterByPrimaryCallType(int primaryCallTypeId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetByPrimaryCallTypeQuery.Invoke(db, primaryCallTypeId).ToList();
            }
        }

        #endregion

    }
}
