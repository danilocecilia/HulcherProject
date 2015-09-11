using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_Job_JobStatusRepository
	{
		private IRepository<CS_Job_JobStatus> _repository {get;set;}
		public IRepository<CS_Job_JobStatus> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_Job_JobStatusRepository(IRepository<CS_Job_JobStatus> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Job_JobStatus Add(CS_Job_JobStatus entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Job_JobStatus> AddList(IList<CS_Job_JobStatus> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Job_JobStatus Update(CS_Job_JobStatus entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Job_JobStatus> UpdateList(IList<CS_Job_JobStatus> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Job_JobStatus> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Job_JobStatus> ListAll(System.Linq.Expressions.Expression<Func<CS_Job_JobStatus, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Job_JobStatus> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Job_JobStatus, bool>> where, System.Linq.Expressions.Expression<Func<CS_Job_JobStatus, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Job_JobStatus Get(System.Linq.Expressions.Expression<Func<CS_Job_JobStatus, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}