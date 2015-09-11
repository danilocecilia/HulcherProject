

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.DataContext
{ 
	public interface IRepository<T> 
    {
		IUnitOfWork UnitOfWork { get; set; }
		T Add(T entity);
		IList<T> AddList(IList<T> entity);
		T Update(T entity);
		IList<T> UpdateList(IList<T> entity);
		IList<T> ListAll(params string[] includeList);
		IList<T> ListAll(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList);
		IList<T> ListAll<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T,TKey>> orderby, bool ascending, params string[] includeList);
		T Get(System.Linq.Expressions.Expression<Func<T, bool>> where, params string[] includeList);
		void ExecuteSql(ObjectContext c, string sql);
		T Get(System.Linq.Expressions.Expression<Func<T, bool>> myWhere, MergeOption option, params string[] includeList);
    }
}

