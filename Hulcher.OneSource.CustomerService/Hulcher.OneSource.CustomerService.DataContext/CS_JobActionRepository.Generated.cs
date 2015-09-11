using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobActionRepository
	{
		private IRepository<CS_JobAction> _repository {get;set;}
		public IRepository<CS_JobAction> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobActionRepository(IRepository<CS_JobAction> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobAction Add(CS_JobAction entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobAction> AddList(IList<CS_JobAction> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobAction Update(CS_JobAction entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobAction> UpdateList(IList<CS_JobAction> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobAction> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobAction> ListAll(System.Linq.Expressions.Expression<Func<CS_JobAction, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobAction> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobAction, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobAction, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobAction Get(System.Linq.Expressions.Expression<Func<CS_JobAction, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}