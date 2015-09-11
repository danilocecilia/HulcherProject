using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeOffCallHistoryRepository
	{
		private IRepository<CS_EmployeeOffCallHistory> _repository {get;set;}
		public IRepository<CS_EmployeeOffCallHistory> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EmployeeOffCallHistoryRepository(IRepository<CS_EmployeeOffCallHistory> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EmployeeOffCallHistory Add(CS_EmployeeOffCallHistory entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EmployeeOffCallHistory> AddList(IList<CS_EmployeeOffCallHistory> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EmployeeOffCallHistory Update(CS_EmployeeOffCallHistory entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EmployeeOffCallHistory> UpdateList(IList<CS_EmployeeOffCallHistory> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EmployeeOffCallHistory> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EmployeeOffCallHistory> ListAll(System.Linq.Expressions.Expression<Func<CS_EmployeeOffCallHistory, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EmployeeOffCallHistory> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EmployeeOffCallHistory, bool>> where, System.Linq.Expressions.Expression<Func<CS_EmployeeOffCallHistory, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EmployeeOffCallHistory Get(System.Linq.Expressions.Expression<Func<CS_EmployeeOffCallHistory, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}