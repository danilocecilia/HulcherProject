using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallLogRepository
	{
		private IRepository<CS_CallLog> _repository {get;set;}
		public IRepository<CS_CallLog> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallLogRepository(IRepository<CS_CallLog> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallLog Add(CS_CallLog entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallLog> AddList(IList<CS_CallLog> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallLog Update(CS_CallLog entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallLog> UpdateList(IList<CS_CallLog> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallLog> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallLog> ListAll(System.Linq.Expressions.Expression<Func<CS_CallLog, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallLog> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallLog, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallLog, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallLog Get(System.Linq.Expressions.Expression<Func<CS_CallLog, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}