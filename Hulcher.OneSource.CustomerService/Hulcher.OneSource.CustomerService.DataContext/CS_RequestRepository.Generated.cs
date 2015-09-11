using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_RequestRepository
	{
		private IRepository<CS_Request> _repository {get;set;}
		public IRepository<CS_Request> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_RequestRepository(IRepository<CS_Request> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Request Add(CS_Request entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Request> AddList(IList<CS_Request> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Request Update(CS_Request entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Request> UpdateList(IList<CS_Request> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Request> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Request> ListAll(System.Linq.Expressions.Expression<Func<CS_Request, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Request> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Request, bool>> where, System.Linq.Expressions.Expression<Func<CS_Request, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Request Get(System.Linq.Expressions.Expression<Func<CS_Request, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}