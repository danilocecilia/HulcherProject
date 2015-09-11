using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class sysdiagramsRepository
	{
		private IRepository<sysdiagrams> _repository {get;set;}
		public IRepository<sysdiagrams> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public sysdiagramsRepository(IRepository<sysdiagrams> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public sysdiagrams Add(sysdiagrams entity)
		{
			return Repository.Add(entity);
		}

		public IList<sysdiagrams> AddList(IList<sysdiagrams> entity)
		{
			return Repository.AddList(entity);
		}

		public sysdiagrams Update(sysdiagrams entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<sysdiagrams> UpdateList(IList<sysdiagrams> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<sysdiagrams> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<sysdiagrams> ListAll(System.Linq.Expressions.Expression<Func<sysdiagrams, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<sysdiagrams> ListAll<TKey>(System.Linq.Expressions.Expression<Func<sysdiagrams, bool>> where, System.Linq.Expressions.Expression<Func<sysdiagrams, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public sysdiagrams Get(System.Linq.Expressions.Expression<Func<sysdiagrams, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}