using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_LocalDivisionRepository
	{
		private IRepository<CS_LocalDivision> _repository {get;set;}
		public IRepository<CS_LocalDivision> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_LocalDivisionRepository(IRepository<CS_LocalDivision> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_LocalDivision Add(CS_LocalDivision entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_LocalDivision> AddList(IList<CS_LocalDivision> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_LocalDivision Update(CS_LocalDivision entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_LocalDivision> UpdateList(IList<CS_LocalDivision> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_LocalDivision> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_LocalDivision> ListAll(System.Linq.Expressions.Expression<Func<CS_LocalDivision, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_LocalDivision> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_LocalDivision, bool>> where, System.Linq.Expressions.Expression<Func<CS_LocalDivision, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_LocalDivision Get(System.Linq.Expressions.Expression<Func<CS_LocalDivision, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}