using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DivisionRepository
	{
		private IRepository<CS_Division> _repository {get;set;}
		public IRepository<CS_Division> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DivisionRepository(IRepository<CS_Division> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Division Add(CS_Division entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Division> AddList(IList<CS_Division> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Division Update(CS_Division entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Division> UpdateList(IList<CS_Division> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Division> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Division> ListAll(System.Linq.Expressions.Expression<Func<CS_Division, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Division> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Division, bool>> where, System.Linq.Expressions.Expression<Func<CS_Division, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Division Get(System.Linq.Expressions.Expression<Func<CS_Division, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}