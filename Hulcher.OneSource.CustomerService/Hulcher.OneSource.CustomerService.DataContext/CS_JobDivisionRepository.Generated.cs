using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobDivisionRepository
	{
		private IRepository<CS_JobDivision> _repository {get;set;}
		public IRepository<CS_JobDivision> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobDivisionRepository(IRepository<CS_JobDivision> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobDivision Add(CS_JobDivision entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobDivision> AddList(IList<CS_JobDivision> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobDivision Update(CS_JobDivision entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobDivision> UpdateList(IList<CS_JobDivision> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobDivision> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobDivision> ListAll(System.Linq.Expressions.Expression<Func<CS_JobDivision, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobDivision> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobDivision, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobDivision, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobDivision Get(System.Linq.Expressions.Expression<Func<CS_JobDivision, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}