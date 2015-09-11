using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobInfoRepository
	{
		private IRepository<CS_JobInfo> _repository {get;set;}
		public IRepository<CS_JobInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobInfoRepository(IRepository<CS_JobInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobInfo Add(CS_JobInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobInfo> AddList(IList<CS_JobInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobInfo Update(CS_JobInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobInfo> UpdateList(IList<CS_JobInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_JobInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobInfo Get(System.Linq.Expressions.Expression<Func<CS_JobInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}