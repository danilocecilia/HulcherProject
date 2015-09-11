using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class vw_ProjectsForPurchasingRepository
	{
		private IRepository<vw_ProjectsForPurchasing> _repository {get;set;}
		public IRepository<vw_ProjectsForPurchasing> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public vw_ProjectsForPurchasingRepository(IRepository<vw_ProjectsForPurchasing> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public vw_ProjectsForPurchasing Add(vw_ProjectsForPurchasing entity)
		{
			return Repository.Add(entity);
		}

		public IList<vw_ProjectsForPurchasing> AddList(IList<vw_ProjectsForPurchasing> entity)
		{
			return Repository.AddList(entity);
		}

		public vw_ProjectsForPurchasing Update(vw_ProjectsForPurchasing entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<vw_ProjectsForPurchasing> UpdateList(IList<vw_ProjectsForPurchasing> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<vw_ProjectsForPurchasing> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<vw_ProjectsForPurchasing> ListAll(System.Linq.Expressions.Expression<Func<vw_ProjectsForPurchasing, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<vw_ProjectsForPurchasing> ListAll<TKey>(System.Linq.Expressions.Expression<Func<vw_ProjectsForPurchasing, bool>> where, System.Linq.Expressions.Expression<Func<vw_ProjectsForPurchasing, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public vw_ProjectsForPurchasing Get(System.Linq.Expressions.Expression<Func<vw_ProjectsForPurchasing, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}