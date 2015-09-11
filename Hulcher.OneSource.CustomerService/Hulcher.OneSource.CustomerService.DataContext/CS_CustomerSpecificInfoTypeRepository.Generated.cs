using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerSpecificInfoTypeRepository
	{
		private IRepository<CS_CustomerSpecificInfoType> _repository {get;set;}
		public IRepository<CS_CustomerSpecificInfoType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CustomerSpecificInfoTypeRepository(IRepository<CS_CustomerSpecificInfoType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CustomerSpecificInfoType Add(CS_CustomerSpecificInfoType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CustomerSpecificInfoType> AddList(IList<CS_CustomerSpecificInfoType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CustomerSpecificInfoType Update(CS_CustomerSpecificInfoType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CustomerSpecificInfoType> UpdateList(IList<CS_CustomerSpecificInfoType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CustomerSpecificInfoType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CustomerSpecificInfoType> ListAll(System.Linq.Expressions.Expression<Func<CS_CustomerSpecificInfoType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CustomerSpecificInfoType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CustomerSpecificInfoType, bool>> where, System.Linq.Expressions.Expression<Func<CS_CustomerSpecificInfoType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CustomerSpecificInfoType Get(System.Linq.Expressions.Expression<Func<CS_CustomerSpecificInfoType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}