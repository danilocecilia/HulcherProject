using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerAliasRepository
	{
		private IRepository<CS_CustomerAlias> _repository {get;set;}
		public IRepository<CS_CustomerAlias> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CustomerAliasRepository(IRepository<CS_CustomerAlias> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CustomerAlias Add(CS_CustomerAlias entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CustomerAlias> AddList(IList<CS_CustomerAlias> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CustomerAlias Update(CS_CustomerAlias entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CustomerAlias> UpdateList(IList<CS_CustomerAlias> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CustomerAlias> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CustomerAlias> ListAll(System.Linq.Expressions.Expression<Func<CS_CustomerAlias, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CustomerAlias> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CustomerAlias, bool>> where, System.Linq.Expressions.Expression<Func<CS_CustomerAlias, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CustomerAlias Get(System.Linq.Expressions.Expression<Func<CS_CustomerAlias, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}