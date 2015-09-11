using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PriceTypeRepository
	{
		private IRepository<CS_PriceType> _repository {get;set;}
		public IRepository<CS_PriceType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PriceTypeRepository(IRepository<CS_PriceType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PriceType Add(CS_PriceType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PriceType> AddList(IList<CS_PriceType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PriceType Update(CS_PriceType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PriceType> UpdateList(IList<CS_PriceType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PriceType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PriceType> ListAll(System.Linq.Expressions.Expression<Func<CS_PriceType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PriceType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PriceType, bool>> where, System.Linq.Expressions.Expression<Func<CS_PriceType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PriceType Get(System.Linq.Expressions.Expression<Func<CS_PriceType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}