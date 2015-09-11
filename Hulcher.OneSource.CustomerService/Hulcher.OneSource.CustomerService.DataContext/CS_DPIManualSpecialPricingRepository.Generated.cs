using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DPIManualSpecialPricingRepository
	{
		private IRepository<CS_DPIManualSpecialPricing> _repository {get;set;}
		public IRepository<CS_DPIManualSpecialPricing> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DPIManualSpecialPricingRepository(IRepository<CS_DPIManualSpecialPricing> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_DPIManualSpecialPricing Add(CS_DPIManualSpecialPricing entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_DPIManualSpecialPricing> AddList(IList<CS_DPIManualSpecialPricing> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_DPIManualSpecialPricing Update(CS_DPIManualSpecialPricing entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_DPIManualSpecialPricing> UpdateList(IList<CS_DPIManualSpecialPricing> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_DPIManualSpecialPricing> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_DPIManualSpecialPricing> ListAll(System.Linq.Expressions.Expression<Func<CS_DPIManualSpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_DPIManualSpecialPricing> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_DPIManualSpecialPricing, bool>> where, System.Linq.Expressions.Expression<Func<CS_DPIManualSpecialPricing, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_DPIManualSpecialPricing Get(System.Linq.Expressions.Expression<Func<CS_DPIManualSpecialPricing, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}