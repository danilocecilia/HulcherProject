using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_Region_DivisionRepository
	{
		private IRepository<CS_Region_Division> _repository {get;set;}
		public IRepository<CS_Region_Division> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_Region_DivisionRepository(IRepository<CS_Region_Division> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Region_Division Add(CS_Region_Division entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Region_Division> AddList(IList<CS_Region_Division> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Region_Division Update(CS_Region_Division entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Region_Division> UpdateList(IList<CS_Region_Division> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Region_Division> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Region_Division> ListAll(System.Linq.Expressions.Expression<Func<CS_Region_Division, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Region_Division> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Region_Division, bool>> where, System.Linq.Expressions.Expression<Func<CS_Region_Division, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Region_Division Get(System.Linq.Expressions.Expression<Func<CS_Region_Division, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}