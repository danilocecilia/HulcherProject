using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ScopeOfWorkRepository
	{
		private IRepository<CS_ScopeOfWork> _repository {get;set;}
		public IRepository<CS_ScopeOfWork> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ScopeOfWorkRepository(IRepository<CS_ScopeOfWork> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_ScopeOfWork Add(CS_ScopeOfWork entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_ScopeOfWork> AddList(IList<CS_ScopeOfWork> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_ScopeOfWork Update(CS_ScopeOfWork entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_ScopeOfWork> UpdateList(IList<CS_ScopeOfWork> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_ScopeOfWork> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_ScopeOfWork> ListAll(System.Linq.Expressions.Expression<Func<CS_ScopeOfWork, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_ScopeOfWork> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_ScopeOfWork, bool>> where, System.Linq.Expressions.Expression<Func<CS_ScopeOfWork, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_ScopeOfWork Get(System.Linq.Expressions.Expression<Func<CS_ScopeOfWork, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}