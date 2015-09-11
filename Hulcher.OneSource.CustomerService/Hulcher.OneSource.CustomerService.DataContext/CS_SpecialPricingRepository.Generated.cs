using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_SpecialPricingRepository
	{
		private IRepository<CS_SpecialPricing> _repository {get;set;}
		public IRepository<CS_SpecialPricing> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_SpecialPricingRepository(IRepository<CS_SpecialPricing> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_SpecialPricing Add(CS_SpecialPricing entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_SpecialPricing> AddList(IList<CS_SpecialPricing> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_SpecialPricing Update(CS_SpecialPricing entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_SpecialPricing> UpdateList(IList<CS_SpecialPricing> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_SpecialPricing> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_SpecialPricing> ListAll(System.Linq.Expressions.Expression<Func<CS_SpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_SpecialPricing> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_SpecialPricing, bool>> where, System.Linq.Expressions.Expression<Func<CS_SpecialPricing, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_SpecialPricing Get(System.Linq.Expressions.Expression<Func<CS_SpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}