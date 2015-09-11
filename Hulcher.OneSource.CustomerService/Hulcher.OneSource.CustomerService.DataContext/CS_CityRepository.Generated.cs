using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CityRepository
	{
		private IRepository<CS_City> _repository {get;set;}
		public IRepository<CS_City> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CityRepository(IRepository<CS_City> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_City Add(CS_City entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_City> AddList(IList<CS_City> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_City Update(CS_City entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_City> UpdateList(IList<CS_City> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_City> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_City> ListAll(System.Linq.Expressions.Expression<Func<CS_City, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_City> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_City, bool>> where, System.Linq.Expressions.Expression<Func<CS_City, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_City Get(System.Linq.Expressions.Expression<Func<CS_City, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}