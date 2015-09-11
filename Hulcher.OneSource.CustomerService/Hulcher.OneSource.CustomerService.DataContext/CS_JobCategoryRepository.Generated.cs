using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobCategoryRepository
	{
		private IRepository<CS_JobCategory> _repository {get;set;}
		public IRepository<CS_JobCategory> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobCategoryRepository(IRepository<CS_JobCategory> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobCategory Add(CS_JobCategory entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobCategory> AddList(IList<CS_JobCategory> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobCategory Update(CS_JobCategory entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobCategory> UpdateList(IList<CS_JobCategory> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobCategory> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobCategory> ListAll(System.Linq.Expressions.Expression<Func<CS_JobCategory, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobCategory> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobCategory, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobCategory, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobCategory Get(System.Linq.Expressions.Expression<Func<CS_JobCategory, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}