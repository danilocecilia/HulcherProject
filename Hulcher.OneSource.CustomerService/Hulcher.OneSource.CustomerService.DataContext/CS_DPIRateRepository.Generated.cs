using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DPIRateRepository
	{
		private IRepository<CS_DPIRate> _repository {get;set;}
		public IRepository<CS_DPIRate> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DPIRateRepository(IRepository<CS_DPIRate> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_DPIRate Add(CS_DPIRate entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_DPIRate> AddList(IList<CS_DPIRate> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_DPIRate Update(CS_DPIRate entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_DPIRate> UpdateList(IList<CS_DPIRate> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_DPIRate> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_DPIRate> ListAll(System.Linq.Expressions.Expression<Func<CS_DPIRate, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_DPIRate> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_DPIRate, bool>> where, System.Linq.Expressions.Expression<Func<CS_DPIRate, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_DPIRate Get(System.Linq.Expressions.Expression<Func<CS_DPIRate, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}