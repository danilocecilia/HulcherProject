using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_Customer_LoadRepository
	{
		private IRepository<CS_Customer_Load> _repository {get;set;}
		public IRepository<CS_Customer_Load> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_Customer_LoadRepository(IRepository<CS_Customer_Load> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Customer_Load Add(CS_Customer_Load entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Customer_Load> AddList(IList<CS_Customer_Load> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Customer_Load Update(CS_Customer_Load entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Customer_Load> UpdateList(IList<CS_Customer_Load> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Customer_Load> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Customer_Load> ListAll(System.Linq.Expressions.Expression<Func<CS_Customer_Load, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Customer_Load> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Customer_Load, bool>> where, System.Linq.Expressions.Expression<Func<CS_Customer_Load, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Customer_Load Get(System.Linq.Expressions.Expression<Func<CS_Customer_Load, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}