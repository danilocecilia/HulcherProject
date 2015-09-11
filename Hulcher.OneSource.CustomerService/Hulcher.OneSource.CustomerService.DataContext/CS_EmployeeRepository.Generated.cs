using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeRepository
	{
		private IRepository<CS_Employee> _repository {get;set;}
		public IRepository<CS_Employee> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EmployeeRepository(IRepository<CS_Employee> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Employee Add(CS_Employee entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Employee> AddList(IList<CS_Employee> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Employee Update(CS_Employee entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Employee> UpdateList(IList<CS_Employee> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Employee> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Employee> ListAll(System.Linq.Expressions.Expression<Func<CS_Employee, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Employee> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Employee, bool>> where, System.Linq.Expressions.Expression<Func<CS_Employee, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Employee Get(System.Linq.Expressions.Expression<Func<CS_Employee, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}