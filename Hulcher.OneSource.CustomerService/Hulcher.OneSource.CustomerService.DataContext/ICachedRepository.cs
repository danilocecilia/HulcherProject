using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Runtime.Caching;

namespace Hulcher.OneSource.CustomerService.DataContext
{ 
	public interface ICachedRepository<T> : IRepository<T>
    {
        void ReloadCacheItem(CacheEntryRemovedArguments args);
        void LoadCacheItem(System.Linq.Expressions.Expression<Func<T, bool>> where,params string[] includeList);
    }
}