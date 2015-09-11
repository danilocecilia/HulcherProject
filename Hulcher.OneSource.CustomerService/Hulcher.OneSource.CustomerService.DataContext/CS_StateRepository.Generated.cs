using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_StateRepository
	{
		private IRepository<CS_State> _repository {get;set;}
		public IRepository<CS_State> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_StateRepository(IRepository<CS_State> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_State Add(CS_State entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_State> AddList(IList<CS_State> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_State Update(CS_State entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_State> UpdateList(IList<CS_State> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_State> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_State> ListAll(System.Linq.Expressions.Expression<Func<CS_State, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_State> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_State, bool>> where, System.Linq.Expressions.Expression<Func<CS_State, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_State Get(System.Linq.Expressions.Expression<Func<CS_State, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}