using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ResourceAllocationDetailsRepository
	{
		private IRepository<CS_ResourceAllocationDetails> _repository {get;set;}
		public IRepository<CS_ResourceAllocationDetails> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ResourceAllocationDetailsRepository(IRepository<CS_ResourceAllocationDetails> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_ResourceAllocationDetails Add(CS_ResourceAllocationDetails entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_ResourceAllocationDetails> AddList(IList<CS_ResourceAllocationDetails> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_ResourceAllocationDetails Update(CS_ResourceAllocationDetails entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_ResourceAllocationDetails> UpdateList(IList<CS_ResourceAllocationDetails> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_ResourceAllocationDetails> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_ResourceAllocationDetails> ListAll(System.Linq.Expressions.Expression<Func<CS_ResourceAllocationDetails, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_ResourceAllocationDetails> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_ResourceAllocationDetails, bool>> where, System.Linq.Expressions.Expression<Func<CS_ResourceAllocationDetails, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_ResourceAllocationDetails Get(System.Linq.Expressions.Expression<Func<CS_ResourceAllocationDetails, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}