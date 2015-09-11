using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DPISpecialPricingRepository
	{
		private IRepository<CS_DPISpecialPricing> _repository {get;set;}
		public IRepository<CS_DPISpecialPricing> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DPISpecialPricingRepository(IRepository<CS_DPISpecialPricing> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_DPISpecialPricing Add(CS_DPISpecialPricing entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_DPISpecialPricing> AddList(IList<CS_DPISpecialPricing> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_DPISpecialPricing Update(CS_DPISpecialPricing entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_DPISpecialPricing> UpdateList(IList<CS_DPISpecialPricing> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_DPISpecialPricing> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_DPISpecialPricing> ListAll(System.Linq.Expressions.Expression<Func<CS_DPISpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_DPISpecialPricing> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_DPISpecialPricing, bool>> where, System.Linq.Expressions.Expression<Func<CS_DPISpecialPricing, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_DPISpecialPricing Get(System.Linq.Expressions.Expression<Func<CS_DPISpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}