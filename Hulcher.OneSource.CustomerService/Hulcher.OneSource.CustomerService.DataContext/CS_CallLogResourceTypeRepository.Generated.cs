using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallLogResourceTypeRepository
	{
		private IRepository<CS_CallLogResourceType> _repository {get;set;}
		public IRepository<CS_CallLogResourceType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallLogResourceTypeRepository(IRepository<CS_CallLogResourceType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallLogResourceType Add(CS_CallLogResourceType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallLogResourceType> AddList(IList<CS_CallLogResourceType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallLogResourceType Update(CS_CallLogResourceType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallLogResourceType> UpdateList(IList<CS_CallLogResourceType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallLogResourceType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallLogResourceType> ListAll(System.Linq.Expressions.Expression<Func<CS_CallLogResourceType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallLogResourceType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallLogResourceType, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallLogResourceType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallLogResourceType Get(System.Linq.Expressions.Expression<Func<CS_CallLogResourceType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}