using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Data.Linq.Mapping;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Base class for all Dao classes
    /// </summary>
    /// <typeparam name="T">Type of an Entity (a class for a table)</typeparam>
    /// <typeparam name="TId">Type of the entity identifier (Primary Key)</typeparam>
    public abstract class BaseDao<T, TId> : IBaseDao<T, TId>
        where T : System.Data.Objects.DataClasses.EntityObject
    {
        #region [ Attributes ]

        /// <summary>
        /// Entity Set name used for Insert and Update Methods
        /// </summary>
        private string _entitySetName;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public BaseDao()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                this._entitySetName = db.CreateObjectSet<T>().EntitySet.Name;
            }
        }

        #endregion

        #region [ IBaseDao Interface ]
        
        /// <summary>
        /// Adds an Entity in the Database
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <returns>Entity inserted</returns>
        public T Add(T entity)
        {
            using (var db = new CustomerServiceModelContainer())
            {                
                db.AddObject(_entitySetName, entity);
                db.SaveChanges();
                return entity;
            }
        }

        public IList<T> AddList(IList<T> entityList)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                foreach (T entity in entityList)
                {
                    db.AddObject(_entitySetName, entity);
                }

                db.SaveChanges();

                return entityList;
            }
        }

        /// <summary>
        /// Update an Entity in the Database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Entity updated</returns>
        public T Update(T entity)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.AttachTo(_entitySetName, entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                db.SaveChanges();
                return entity;
            }
        }

        public IList<T> UpdateList(IList<T> entityList)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                foreach (T entity in entityList)
                {
                    db.AttachTo(_entitySetName, entity);
                    db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                }
                
                db.SaveChanges();
                
                return entityList;
            }
        }

        /// <summary>
        /// List all active items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<T> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<T> returnList = db.CreateObjectSet<T>().Where("it.Active = true").ToList<T>();
                return returnList;
            }
        }

        /// <summary>
        /// List all active items of an Entity
        /// </summary>
        /// <param name="orderby">Order by Clause</param>
        /// <returns>List of Entities</returns>
        public IList<T> ListAll(string orderby)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<T> returnList = null;
                if (orderby.IndexOf(',') >= 0)
                {
                    StringBuilder strOrderBy = new StringBuilder();
                    string[] splitOrderBy = orderby.Split(',');
                    for (int i = 0; i < splitOrderBy.Length; i++)
                    {
                        if (i.Equals(0))
                            strOrderBy.AppendFormat("it.{0}", splitOrderBy[i]);
                        else
                            strOrderBy.AppendFormat(", it.{0}", splitOrderBy[i]);
                    }
                    returnList = db.CreateObjectSet<T>().Where("it.Active = true").OrderBy(strOrderBy.ToString()).ToList<T>();
                }
                else
                    returnList = db.CreateObjectSet<T>().Where("it.Active = true").OrderBy(string.Format("it.{0}", orderby)).ToList<T>();
                return returnList;
            }
        }

        /// <summary>
        /// Get a specific Entity in the Database
        /// </summary>
        /// <param name="id">Entity's Identifier (Primary Key)</param>
        /// <returns>Entity loaded with all its information</returns>
        public T Get(TId id)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                T returnItem = db.CreateObjectSet<T>().Where(string.Format("it.{0} = @id", GetPrimaryKeyName()), 
                    new ObjectParameter[] { 
                        new ObjectParameter("id", id) 
                    })
                    .FirstOrDefault<T>();
                return returnItem;
            }
        }

        public void ExecuteSql(ObjectContext c, string sql)     
        {         
            var entityConnection = (System.Data.EntityClient.EntityConnection)c.Connection;         
            DbConnection conn = entityConnection.StoreConnection;          
            ConnectionState initialState = conn.State;        
            try         
            {             
                if (initialState != ConnectionState.Open)                
                    conn.Open();  // open connection if not already open             
                using (DbCommand cmd = conn.CreateCommand())             
                {                 
                    cmd.CommandText = sql;                 
                    cmd.ExecuteNonQuery();             
                }        
            }        
            finally         
            {            
                if (initialState != ConnectionState.Open)                
                    conn.Close(); // only close connection if not initially open         
            }     
        } 



        #endregion

        #region [ Methods ]

        private string GetPrimaryKeyName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is EdmScalarPropertyAttribute)
                    {
                        if ((attribute as EdmScalarPropertyAttribute).EntityKeyProperty == true)
                            return pI.Name;
                    }
                    else if (attribute is ColumnAttribute)
                    {

                        if ((attribute as ColumnAttribute).IsPrimaryKey == true)
                            return pI.Name;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
