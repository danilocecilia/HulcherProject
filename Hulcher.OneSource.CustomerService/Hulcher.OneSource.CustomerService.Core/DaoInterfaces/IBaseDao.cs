using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Base Interface for all Dao classess
    /// </summary>
    /// <typeparam name="T">Type of an Entity (a class for a table)</typeparam>
    /// <typeparam name="TId">Type of the entity identifier (Primary Key)</typeparam>
    public interface IBaseDao<T, TId>
    {
        /// <summary>
        /// Adds an Entity in the Database
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <returns>Entity inserted</returns>
        T Add(T entity);
        IList<T> AddList(IList<T> entity);

        /// <summary>
        /// Update an Entity in the Database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Entity updated</returns>
        T Update(T entity);
        IList<T> UpdateList(IList<T> entity);
        
        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        IList<T> ListAll();

        /// <summary>
        /// List all active items of an Entity
        /// </summary>
        /// <param name="orderby">Order by Clause</param>
        /// <returns>List of Entities</returns>
        IList<T> ListAll(string orderby);

        /// <summary>
        /// Get a specific Entity in the Database
        /// </summary>
        /// <param name="id">Entity's Identifier (Primary Key)</param>
        /// <returns>Entity loaded with all its information</returns>
        T Get(TId id);

        void ExecuteSql(ObjectContext c, string sql);
    }
}
