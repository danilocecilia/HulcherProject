using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_JobSummaryRepository
	{
		private IRepository<CS_View_JobSummary> _repository {get;set;}
		public IRepository<CS_View_JobSummary> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_JobSummaryRepository(IRepository<CS_View_JobSummary> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_JobSummary Add(CS_View_JobSummary entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_JobSummary> AddList(IList<CS_View_JobSummary> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_JobSummary Update(CS_View_JobSummary entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_JobSummary> UpdateList(IList<CS_View_JobSummary> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_JobSummary> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_JobSummary> ListAll(System.Linq.Expressions.Expression<Func<CS_View_JobSummary, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_JobSummary> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_JobSummary, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_JobSummary, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_JobSummary Get(System.Linq.Expressions.Expression<Func<CS_View_JobSummary, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}