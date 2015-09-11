using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_RouteRepository
	{
		private IRepository<CS_Route> _repository {get;set;}
		public IRepository<CS_Route> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_RouteRepository(IRepository<CS_Route> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Route Add(CS_Route entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Route> AddList(IList<CS_Route> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Route Update(CS_Route entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Route> UpdateList(IList<CS_Route> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Route> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Route> ListAll(System.Linq.Expressions.Expression<Func<CS_Route, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Route> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Route, bool>> where, System.Linq.Expressions.Expression<Func<CS_Route, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Route Get(System.Linq.Expressions.Expression<Func<CS_Route, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}