using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ContactRepository
	{
		private IRepository<CS_Contact> _repository {get;set;}
		public IRepository<CS_Contact> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ContactRepository(IRepository<CS_Contact> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Contact Add(CS_Contact entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Contact> AddList(IList<CS_Contact> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Contact Update(CS_Contact entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Contact> UpdateList(IList<CS_Contact> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Contact> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Contact> ListAll(System.Linq.Expressions.Expression<Func<CS_Contact, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Contact> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Contact, bool>> where, System.Linq.Expressions.Expression<Func<CS_Contact, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Contact Get(System.Linq.Expressions.Expression<Func<CS_Contact, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}