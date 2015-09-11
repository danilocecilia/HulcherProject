using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerRepository
	{
		private IRepository<CS_Customer> _repository {get;set;}
		public IRepository<CS_Customer> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CustomerRepository(IRepository<CS_Customer> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Customer Add(CS_Customer entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Customer> AddList(IList<CS_Customer> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Customer Update(CS_Customer entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Customer> UpdateList(IList<CS_Customer> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Customer> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Customer> ListAll(System.Linq.Expressions.Expression<Func<CS_Customer, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Customer> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Customer, bool>> where, System.Linq.Expressions.Expression<Func<CS_Customer, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Customer Get(System.Linq.Expressions.Expression<Func<CS_Customer, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}