using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobTypeRepository
	{
		private IRepository<CS_JobType> _repository {get;set;}
		public IRepository<CS_JobType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobTypeRepository(IRepository<CS_JobType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobType Add(CS_JobType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobType> AddList(IList<CS_JobType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobType Update(CS_JobType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobType> UpdateList(IList<CS_JobType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobType> ListAll(System.Linq.Expressions.Expression<Func<CS_JobType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobType, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobType Get(System.Linq.Expressions.Expression<Func<CS_JobType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}