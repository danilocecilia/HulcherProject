using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobPermitRepository
	{
		private IRepository<CS_JobPermit> _repository {get;set;}
		public IRepository<CS_JobPermit> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobPermitRepository(IRepository<CS_JobPermit> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobPermit Add(CS_JobPermit entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobPermit> AddList(IList<CS_JobPermit> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobPermit Update(CS_JobPermit entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobPermit> UpdateList(IList<CS_JobPermit> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobPermit> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobPermit> ListAll(System.Linq.Expressions.Expression<Func<CS_JobPermit, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobPermit> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobPermit, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobPermit, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobPermit Get(System.Linq.Expressions.Expression<Func<CS_JobPermit, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}