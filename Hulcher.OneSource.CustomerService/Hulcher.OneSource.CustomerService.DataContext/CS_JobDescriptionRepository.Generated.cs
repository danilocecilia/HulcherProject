using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobDescriptionRepository
	{
		private IRepository<CS_JobDescription> _repository {get;set;}
		public IRepository<CS_JobDescription> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobDescriptionRepository(IRepository<CS_JobDescription> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobDescription Add(CS_JobDescription entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobDescription> AddList(IList<CS_JobDescription> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobDescription Update(CS_JobDescription entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobDescription> UpdateList(IList<CS_JobDescription> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobDescription> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobDescription> ListAll(System.Linq.Expressions.Expression<Func<CS_JobDescription, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobDescription> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobDescription, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobDescription, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobDescription Get(System.Linq.Expressions.Expression<Func<CS_JobDescription, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}