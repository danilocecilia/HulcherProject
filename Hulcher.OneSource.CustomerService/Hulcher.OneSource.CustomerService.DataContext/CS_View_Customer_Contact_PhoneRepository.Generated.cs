using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_Customer_Contact_PhoneRepository
	{
		private IRepository<CS_View_Customer_Contact_Phone> _repository {get;set;}
		public IRepository<CS_View_Customer_Contact_Phone> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_Customer_Contact_PhoneRepository(IRepository<CS_View_Customer_Contact_Phone> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_Customer_Contact_Phone Add(CS_View_Customer_Contact_Phone entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_Customer_Contact_Phone> AddList(IList<CS_View_Customer_Contact_Phone> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_Customer_Contact_Phone Update(CS_View_Customer_Contact_Phone entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_Customer_Contact_Phone> UpdateList(IList<CS_View_Customer_Contact_Phone> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_Customer_Contact_Phone> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_Customer_Contact_Phone> ListAll(System.Linq.Expressions.Expression<Func<CS_View_Customer_Contact_Phone, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_Customer_Contact_Phone> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_Customer_Contact_Phone, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_Customer_Contact_Phone, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_Customer_Contact_Phone Get(System.Linq.Expressions.Expression<Func<CS_View_Customer_Contact_Phone, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}