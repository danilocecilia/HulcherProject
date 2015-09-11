using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Data.Linq.Mapping;

namespace Hulcher.OneSource.CustomerService.DataContext
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		public IUnitOfWork UnitOfWork { get; set; }
		private IObjectSet<T> _objectset;
		private IObjectSet<T> ObjectSet
		{
			get
			{
				if (_objectset == null)
				{
					_objectset = UnitOfWork.CreateObjectSet<T>();
				}
				return _objectset;
			}
		}

		public T Add(T entity)
		{
			 ObjectSet.AddObject(entity);
             UnitOfWork.Save();
             return entity;
		}

		public IList<T> AddList(IList<T> entityList)
		{
			 foreach (T entity in entityList)
             {
                 ObjectSet.AddObject(entity);
             }
             UnitOfWork.Save();
             return entityList;
		}

		public T Update(T entity)
		{
			 ObjectStateEntry stateEntry = null;
 			 if (!UnitOfWork.TryGetObjectStateEntry(entity, out stateEntry))
			 {
                 ObjectSet.Attach(entity);
             }
             UnitOfWork.ChangeObjectState(entity, EntityState.Modified);			 
             UnitOfWork.Save();
             return entity;
		}

		public IList<T> UpdateList(IList<T> entityList)
		{
			foreach (T entity in entityList)
            {
				ObjectStateEntry stateEntry = null;
 				if (!UnitOfWork.TryGetObjectStateEntry(entity, out stateEntry))
				{
                    ObjectSet.Attach(entity);
                }
                UnitOfWork.ChangeObjectState(entity, EntityState.Modified);
            }
            UnitOfWork.Save();                
            return entityList;
		}

				public IList<T> ListAll(params string[] includeList)
		{
            ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;
            if (query == null)
                return ListAllForIObjectSet(includeList);
            else
                return ListAllForObjectSet(includeList);           
		}

        public IList<T> ListAllForObjectSet(params string[] includeList)
        {
			ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]) as ObjectQuery<T>;

			query.MergeOption = MergeOption.NoTracking;

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

        public IList<T> ListAllForIObjectSet(params string[] includeList)
        {
			IQueryable<T> query = ObjectSet;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]);

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

		public IList<T> ListAll(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList)
		{
            ObjectQuery<T> query = ObjectSet.Where(where) as ObjectQuery<T>;

            if (query == null)
                return ListAllForIObjectSet(where, includeList);
            else
                return ListAllForObjectSet(where, includeList);
		}

        public IList<T> ListAllForObjectSet(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList)
        {
			ObjectQuery<T> query = ObjectSet.Where(where) as ObjectQuery<T>;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]) as ObjectQuery<T>;

			query.MergeOption = MergeOption.NoTracking;

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

        public IList<T> ListAllForIObjectSet(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList)
        {
			IQueryable<T> query = ObjectSet.Where(where);

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]);

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

		public IList<T> ListAll<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T,TKey>> orderby, bool ascending, params string[] includeList)
		{
            ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;
            if (query == null)
                return ListAllForIObjectSet<TKey>(where, orderby, ascending, includeList);
            else
                return ListAllForObjectSet<TKey>(where, orderby, ascending, includeList);
		}

        public IList<T> ListAllForObjectSet<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, TKey>> orderby, bool ascending, params string[] includeList)
        {
			ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;

			if (ascending)
				query = query.Where(where).OrderBy(orderby) as ObjectQuery<T>;
			else
				query = query.Where(where).OrderByDescending(orderby) as ObjectQuery<T>;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]) as ObjectQuery<T>;

			query.MergeOption = MergeOption.NoTracking;

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

        public IList<T> ListAllForIObjectSet<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, TKey>> orderby, bool ascending, params string[] includeList)
        {
			IQueryable<T> query = ObjectSet;

			if (ascending)
				query = query.Where(where).OrderBy(orderby);
			else
				query = query.Where(where).OrderByDescending(orderby);

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]);

			IList<T> returnList = query.ToList<T>();

			return returnList;
        }

		public T Get(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, params string[] includeList)
		{
            ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;
            if (query == null)
                return GetForIObjectSet(myWhere, includeList);
            else
                return GetForObjectSet(myWhere, includeList);
		}

		public T Get(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, MergeOption option, params string[] includeList)
		{
            ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;
            if (query == null)
                return GetForIObjectSet(myWhere, includeList);
            else
                return GetForObjectSet(myWhere, option, includeList);
		}

        public T GetForObjectSet(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, MergeOption option, params string[] includeList)
        {
			ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]) as ObjectQuery<T>;

			query.MergeOption = option;

			T returnItem = query.FirstOrDefault();

			return returnItem;
        }

		public T GetForObjectSet(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, params string[] includeList)
        {
			ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;

			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]) as ObjectQuery<T>;

			query.MergeOption = MergeOption.NoTracking;

			T returnItem = query.FirstOrDefault();

			return returnItem;
        }

        public T GetForIObjectSet(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, params string[] includeList)
        {
			IQueryable<T> query = ObjectSet.Where(myWhere);
			for (int i = 0; i < includeList.Length; i++)
				query = query.Include<T>(includeList[i]);
			T returnItem = query.FirstOrDefault();

			return returnItem;
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
	}
}