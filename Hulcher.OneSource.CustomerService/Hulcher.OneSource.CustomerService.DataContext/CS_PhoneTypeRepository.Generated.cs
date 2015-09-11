using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PhoneTypeRepository
	{
		private IRepository<CS_PhoneType> _repository {get;set;}
		public IRepository<CS_PhoneType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PhoneTypeRepository(IRepository<CS_PhoneType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PhoneType Add(CS_PhoneType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PhoneType> AddList(IList<CS_PhoneType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PhoneType Update(CS_PhoneType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PhoneType> UpdateList(IList<CS_PhoneType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PhoneType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PhoneType> ListAll(System.Linq.Expressions.Expression<Func<CS_PhoneType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PhoneType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PhoneType, bool>> where, System.Linq.Expressions.Expression<Func<CS_PhoneType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PhoneType Get(System.Linq.Expressions.Expression<Func<CS_PhoneType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}