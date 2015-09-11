using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerContractRepository
	{
		private IRepository<CS_CustomerContract> _repository {get;set;}
		public IRepository<CS_CustomerContract> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CustomerContractRepository(IRepository<CS_CustomerContract> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CustomerContract Add(CS_CustomerContract entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CustomerContract> AddList(IList<CS_CustomerContract> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CustomerContract Update(CS_CustomerContract entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CustomerContract> UpdateList(IList<CS_CustomerContract> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CustomerContract> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CustomerContract> ListAll(System.Linq.Expressions.Expression<Func<CS_CustomerContract, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CustomerContract> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CustomerContract, bool>> where, System.Linq.Expressions.Expression<Func<CS_CustomerContract, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CustomerContract Get(System.Linq.Expressions.Expression<Func<CS_CustomerContract, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}