using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallCriteriaTypeRepository
	{
		private IRepository<CS_CallCriteriaType> _repository {get;set;}
		public IRepository<CS_CallCriteriaType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallCriteriaTypeRepository(IRepository<CS_CallCriteriaType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallCriteriaType Add(CS_CallCriteriaType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallCriteriaType> AddList(IList<CS_CallCriteriaType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallCriteriaType Update(CS_CallCriteriaType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallCriteriaType> UpdateList(IList<CS_CallCriteriaType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallCriteriaType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallCriteriaType> ListAll(System.Linq.Expressions.Expression<Func<CS_CallCriteriaType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallCriteriaType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallCriteriaType, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallCriteriaType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallCriteriaType Get(System.Linq.Expressions.Expression<Func<CS_CallCriteriaType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}