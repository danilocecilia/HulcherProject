using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallCriteriaRepository
	{
		private IRepository<CS_CallCriteria> _repository {get;set;}
		public IRepository<CS_CallCriteria> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallCriteriaRepository(IRepository<CS_CallCriteria> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallCriteria Add(CS_CallCriteria entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallCriteria> AddList(IList<CS_CallCriteria> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallCriteria Update(CS_CallCriteria entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallCriteria> UpdateList(IList<CS_CallCriteria> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallCriteria> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallCriteria> ListAll(System.Linq.Expressions.Expression<Func<CS_CallCriteria, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallCriteria> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallCriteria, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallCriteria, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallCriteria Get(System.Linq.Expressions.Expression<Func<CS_CallCriteria, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}