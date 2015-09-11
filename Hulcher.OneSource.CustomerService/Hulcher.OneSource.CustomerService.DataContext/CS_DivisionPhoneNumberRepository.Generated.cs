using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DivisionPhoneNumberRepository
	{
		private IRepository<CS_DivisionPhoneNumber> _repository {get;set;}
		public IRepository<CS_DivisionPhoneNumber> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DivisionPhoneNumberRepository(IRepository<CS_DivisionPhoneNumber> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_DivisionPhoneNumber Add(CS_DivisionPhoneNumber entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_DivisionPhoneNumber> AddList(IList<CS_DivisionPhoneNumber> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_DivisionPhoneNumber Update(CS_DivisionPhoneNumber entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_DivisionPhoneNumber> UpdateList(IList<CS_DivisionPhoneNumber> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_DivisionPhoneNumber> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_DivisionPhoneNumber> ListAll(System.Linq.Expressions.Expression<Func<CS_DivisionPhoneNumber, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_DivisionPhoneNumber> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_DivisionPhoneNumber, bool>> where, System.Linq.Expressions.Expression<Func<CS_DivisionPhoneNumber, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_DivisionPhoneNumber Get(System.Linq.Expressions.Expression<Func<CS_DivisionPhoneNumber, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}