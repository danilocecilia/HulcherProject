using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PrimaryCallType_CallTypeRepository
	{
		private IRepository<CS_PrimaryCallType_CallType> _repository {get;set;}
		public IRepository<CS_PrimaryCallType_CallType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PrimaryCallType_CallTypeRepository(IRepository<CS_PrimaryCallType_CallType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PrimaryCallType_CallType Add(CS_PrimaryCallType_CallType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PrimaryCallType_CallType> AddList(IList<CS_PrimaryCallType_CallType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PrimaryCallType_CallType Update(CS_PrimaryCallType_CallType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PrimaryCallType_CallType> UpdateList(IList<CS_PrimaryCallType_CallType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PrimaryCallType_CallType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PrimaryCallType_CallType> ListAll(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType_CallType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PrimaryCallType_CallType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType_CallType, bool>> where, System.Linq.Expressions.Expression<Func<CS_PrimaryCallType_CallType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PrimaryCallType_CallType Get(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType_CallType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}