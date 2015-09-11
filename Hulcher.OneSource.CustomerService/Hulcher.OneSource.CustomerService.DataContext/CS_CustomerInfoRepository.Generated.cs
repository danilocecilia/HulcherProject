using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerInfoRepository
	{
		private IRepository<CS_CustomerInfo> _repository {get;set;}
		public IRepository<CS_CustomerInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CustomerInfoRepository(IRepository<CS_CustomerInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CustomerInfo Add(CS_CustomerInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CustomerInfo> AddList(IList<CS_CustomerInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CustomerInfo Update(CS_CustomerInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CustomerInfo> UpdateList(IList<CS_CustomerInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CustomerInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CustomerInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_CustomerInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CustomerInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CustomerInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_CustomerInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CustomerInfo Get(System.Linq.Expressions.Expression<Func<CS_CustomerInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}