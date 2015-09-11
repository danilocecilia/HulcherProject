using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class vw_ProjectsForSLRepository
	{
		private IRepository<vw_ProjectsForSL> _repository {get;set;}
		public IRepository<vw_ProjectsForSL> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public vw_ProjectsForSLRepository(IRepository<vw_ProjectsForSL> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public vw_ProjectsForSL Add(vw_ProjectsForSL entity)
		{
			return Repository.Add(entity);
		}

		public IList<vw_ProjectsForSL> AddList(IList<vw_ProjectsForSL> entity)
		{
			return Repository.AddList(entity);
		}

		public vw_ProjectsForSL Update(vw_ProjectsForSL entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<vw_ProjectsForSL> UpdateList(IList<vw_ProjectsForSL> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<vw_ProjectsForSL> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<vw_ProjectsForSL> ListAll(System.Linq.Expressions.Expression<Func<vw_ProjectsForSL, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<vw_ProjectsForSL> ListAll<TKey>(System.Linq.Expressions.Expression<Func<vw_ProjectsForSL, bool>> where, System.Linq.Expressions.Expression<Func<vw_ProjectsForSL, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public vw_ProjectsForSL Get(System.Linq.Expressions.Expression<Func<vw_ProjectsForSL, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}