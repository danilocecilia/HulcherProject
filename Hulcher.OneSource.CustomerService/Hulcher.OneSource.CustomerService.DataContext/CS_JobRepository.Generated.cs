using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobRepository
	{
		private IRepository<CS_Job> _repository {get;set;}
		public IRepository<CS_Job> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobRepository(IRepository<CS_Job> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Job Add(CS_Job entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Job> AddList(IList<CS_Job> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Job Update(CS_Job entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Job> UpdateList(IList<CS_Job> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Job> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Job> ListAll(System.Linq.Expressions.Expression<Func<CS_Job, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Job> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Job, bool>> where, System.Linq.Expressions.Expression<Func<CS_Job, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Job Get(System.Linq.Expressions.Expression<Func<CS_Job, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}