using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_Employee_LoadRepository
	{
		private IRepository<CS_Employee_Load> _repository {get;set;}
		public IRepository<CS_Employee_Load> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_Employee_LoadRepository(IRepository<CS_Employee_Load> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Employee_Load Add(CS_Employee_Load entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Employee_Load> AddList(IList<CS_Employee_Load> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Employee_Load Update(CS_Employee_Load entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Employee_Load> UpdateList(IList<CS_Employee_Load> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Employee_Load> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Employee_Load> ListAll(System.Linq.Expressions.Expression<Func<CS_Employee_Load, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Employee_Load> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Employee_Load, bool>> where, System.Linq.Expressions.Expression<Func<CS_Employee_Load, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Employee_Load Get(System.Linq.Expressions.Expression<Func<CS_Employee_Load, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}