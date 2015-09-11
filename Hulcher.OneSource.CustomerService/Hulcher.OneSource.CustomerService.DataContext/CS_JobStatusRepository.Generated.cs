using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobStatusRepository
	{
		private IRepository<CS_JobStatus> _repository {get;set;}
		public IRepository<CS_JobStatus> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobStatusRepository(IRepository<CS_JobStatus> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobStatus Add(CS_JobStatus entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobStatus> AddList(IList<CS_JobStatus> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobStatus Update(CS_JobStatus entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobStatus> UpdateList(IList<CS_JobStatus> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobStatus> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobStatus> ListAll(System.Linq.Expressions.Expression<Func<CS_JobStatus, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobStatus> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobStatus, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobStatus, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobStatus Get(System.Linq.Expressions.Expression<Func<CS_JobStatus, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}