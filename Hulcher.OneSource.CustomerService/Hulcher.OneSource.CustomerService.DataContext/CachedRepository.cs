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
using System.Runtime.Caching;
using System.Data.SqlClient;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public class CachedRepository<T> : EFRepository<T>, ICachedRepository<T> where T : class
    {
        private string stringType = typeof(T).ToString();
        private ObjectCache cache = MemoryCache.Default;

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

        public void ReloadCacheItem(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason == CacheEntryRemovedReason.ChangeMonitorChanged)
                LoadCacheItem();
        }

        public void LoadCacheItem()
        {
            this._objectset = null;
            this.UnitOfWork = new EFUnitOfWork();

            IList<T> returnList;
            ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;
            if (query == null)
                returnList = ListAllForIObjectSet();
            else
                returnList = ListAllForObjectSet();

            Cache<T> cache = new Cache<T>(this);

            cache.AddObject(returnList);
        }

        public void LoadCacheItem(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList)
        {
            this._objectset = null;
            this.UnitOfWork = new EFUnitOfWork();

            IList<T> returnList;
            ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;
            if (query == null)
                returnList = ListAllForIObjectSet(where, includeList);
            else
                returnList = ListAllForObjectSet(where, includeList);

            Cache<T> cache = new Cache<T>(this);

            cache.AddObject(returnList);
        }

        new public IList<T> ListAll(params string[] includeList)
        {
            if (null != cache[stringType] && includeList.Length == 0)
            {
                return cache[stringType] as IList<T>;
            }
            else
            {
                IList<T> returnList;
                ObjectQuery<T> query = ObjectSet as ObjectQuery<T>;
                if (query == null)
                    returnList = ListAllForIObjectSet(includeList);
                else
                    returnList = ListAllForObjectSet(includeList);

                return returnList;
            }
        }

        new public IList<T> ListAll(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList)
        {
            IList<T> returnList;

            if (null != cache[stringType] && includeList.Length == 0)
            {
                returnList = cache[stringType] as List<T>;

                return returnList.Where(where.Compile()).ToList();
            }
            else
            {
                ObjectQuery<T> query = ObjectSet.Where(where) as ObjectQuery<T>;

                if (query == null)
                    returnList = ListAllForIObjectSet(where, includeList);
                else
                    returnList = ListAllForObjectSet(where, includeList);

                return returnList;
            }
        }

        new public IList<T> ListAll<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, TKey>> orderby, bool ascending, params string[] includeList)
        {
            IList<T> returnList;

            if (null != cache[stringType] && includeList.Length == 0)
            {
                returnList = cache[stringType] as List<T>;

                if (ascending)
                    return returnList.Where(where.Compile()).OrderBy(orderby.Compile()).ToList();
                else
                    return returnList.Where(where.Compile()).OrderByDescending(orderby.Compile()).ToList();
            }
            else
            {
                ObjectQuery<T> query = ObjectSet.Where(where) as ObjectQuery<T>;

                if (query == null)
                    returnList = ListAllForIObjectSet(where, orderby, ascending, includeList);
                else
                    returnList = ListAllForObjectSet(where, orderby, ascending, includeList);

                if (ascending)
                    return returnList.OrderBy(orderby.Compile()).ToList();
                else
                    return returnList.OrderByDescending(orderby.Compile()).ToList();
            }
        }

        new public T Get(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, params string[] includeList)
        {
            if (null != cache[stringType]) //&& includeList.Length == 0)
            {
                IList<T> returnList = cache[stringType] as List<T>;

                return returnList.Where(myWhere.Compile()).FirstOrDefault();
            }
            else
            {
                ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;
                if (query == null)
                    return GetForIObjectSet(myWhere, includeList);
                else
                    return GetForObjectSet(myWhere, includeList);
            }
        }

        new public T Get(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, MergeOption option, params string[] includeList)
        {
            if (null != cache[stringType] && includeList.Length == 0)
            {
                IList<T> returnList = cache[stringType] as List<T>;

                return returnList.Where(myWhere.Compile()).FirstOrDefault();
            }
            else
            {
                ObjectQuery<T> query = ObjectSet.Where(myWhere) as ObjectQuery<T>;
                if (query == null)
                    return GetForIObjectSet(myWhere, includeList);
                else
                    return GetForObjectSet(myWhere, option, includeList);
            }
        }
    }
}
