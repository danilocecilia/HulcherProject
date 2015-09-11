using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallTypeRepository
	{
		private IRepository<CS_CallType> _repository {get;set;}
		public IRepository<CS_CallType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallTypeRepository(IRepository<CS_CallType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallType Add(CS_CallType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallType> AddList(IList<CS_CallType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallType Update(CS_CallType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallType> UpdateList(IList<CS_CallType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallType> ListAll(System.Linq.Expressions.Expression<Func<CS_CallType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallType, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallType Get(System.Linq.Expressions.Expression<Func<CS_CallType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}