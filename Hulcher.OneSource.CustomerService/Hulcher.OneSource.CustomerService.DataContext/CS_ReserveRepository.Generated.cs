using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ReserveRepository
	{
		private IRepository<CS_Reserve> _repository {get;set;}
		public IRepository<CS_Reserve> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ReserveRepository(IRepository<CS_Reserve> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Reserve Add(CS_Reserve entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Reserve> AddList(IList<CS_Reserve> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Reserve Update(CS_Reserve entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Reserve> UpdateList(IList<CS_Reserve> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Reserve> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Reserve> ListAll(System.Linq.Expressions.Expression<Func<CS_Reserve, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Reserve> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Reserve, bool>> where, System.Linq.Expressions.Expression<Func<CS_Reserve, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Reserve Get(System.Linq.Expressions.Expression<Func<CS_Reserve, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}