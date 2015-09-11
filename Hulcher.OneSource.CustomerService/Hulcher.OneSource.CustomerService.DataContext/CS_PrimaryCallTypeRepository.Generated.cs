using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PrimaryCallTypeRepository
	{
		private IRepository<CS_PrimaryCallType> _repository {get;set;}
		public IRepository<CS_PrimaryCallType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PrimaryCallTypeRepository(IRepository<CS_PrimaryCallType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PrimaryCallType Add(CS_PrimaryCallType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PrimaryCallType> AddList(IList<CS_PrimaryCallType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PrimaryCallType Update(CS_PrimaryCallType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PrimaryCallType> UpdateList(IList<CS_PrimaryCallType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PrimaryCallType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PrimaryCallType> ListAll(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PrimaryCallType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType, bool>> where, System.Linq.Expressions.Expression<Func<CS_PrimaryCallType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PrimaryCallType Get(System.Linq.Expressions.Expression<Func<CS_PrimaryCallType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}