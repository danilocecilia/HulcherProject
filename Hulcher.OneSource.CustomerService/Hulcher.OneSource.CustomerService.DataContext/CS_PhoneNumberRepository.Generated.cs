using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PhoneNumberRepository
	{
		private IRepository<CS_PhoneNumber> _repository {get;set;}
		public IRepository<CS_PhoneNumber> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PhoneNumberRepository(IRepository<CS_PhoneNumber> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PhoneNumber Add(CS_PhoneNumber entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PhoneNumber> AddList(IList<CS_PhoneNumber> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PhoneNumber Update(CS_PhoneNumber entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PhoneNumber> UpdateList(IList<CS_PhoneNumber> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PhoneNumber> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PhoneNumber> ListAll(System.Linq.Expressions.Expression<Func<CS_PhoneNumber, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PhoneNumber> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PhoneNumber, bool>> where, System.Linq.Expressions.Expression<Func<CS_PhoneNumber, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PhoneNumber Get(System.Linq.Expressions.Expression<Func<CS_PhoneNumber, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}