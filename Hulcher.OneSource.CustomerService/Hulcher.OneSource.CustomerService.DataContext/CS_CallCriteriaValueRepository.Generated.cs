using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallCriteriaValueRepository
	{
		private IRepository<CS_CallCriteriaValue> _repository {get;set;}
		public IRepository<CS_CallCriteriaValue> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallCriteriaValueRepository(IRepository<CS_CallCriteriaValue> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallCriteriaValue Add(CS_CallCriteriaValue entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallCriteriaValue> AddList(IList<CS_CallCriteriaValue> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallCriteriaValue Update(CS_CallCriteriaValue entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallCriteriaValue> UpdateList(IList<CS_CallCriteriaValue> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallCriteriaValue> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallCriteriaValue> ListAll(System.Linq.Expressions.Expression<Func<CS_CallCriteriaValue, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallCriteriaValue> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallCriteriaValue, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallCriteriaValue, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallCriteriaValue Get(System.Linq.Expressions.Expression<Func<CS_CallCriteriaValue, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}