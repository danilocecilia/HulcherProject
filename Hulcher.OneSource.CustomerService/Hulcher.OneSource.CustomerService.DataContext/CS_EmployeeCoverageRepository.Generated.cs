using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeCoverageRepository
	{
		private IRepository<CS_EmployeeCoverage> _repository {get;set;}
		public IRepository<CS_EmployeeCoverage> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EmployeeCoverageRepository(IRepository<CS_EmployeeCoverage> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EmployeeCoverage Add(CS_EmployeeCoverage entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EmployeeCoverage> AddList(IList<CS_EmployeeCoverage> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EmployeeCoverage Update(CS_EmployeeCoverage entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EmployeeCoverage> UpdateList(IList<CS_EmployeeCoverage> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EmployeeCoverage> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EmployeeCoverage> ListAll(System.Linq.Expressions.Expression<Func<CS_EmployeeCoverage, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EmployeeCoverage> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EmployeeCoverage, bool>> where, System.Linq.Expressions.Expression<Func<CS_EmployeeCoverage, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EmployeeCoverage Get(System.Linq.Expressions.Expression<Func<CS_EmployeeCoverage, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}