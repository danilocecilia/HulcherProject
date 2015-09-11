using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CountryRepository
	{
		private IRepository<CS_Country> _repository {get;set;}
		public IRepository<CS_Country> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CountryRepository(IRepository<CS_Country> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Country Add(CS_Country entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Country> AddList(IList<CS_Country> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Country Update(CS_Country entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Country> UpdateList(IList<CS_Country> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Country> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Country> ListAll(System.Linq.Expressions.Expression<Func<CS_Country, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Country> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Country, bool>> where, System.Linq.Expressions.Expression<Func<CS_Country, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Country Get(System.Linq.Expressions.Expression<Func<CS_Country, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}