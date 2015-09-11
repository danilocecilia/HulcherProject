using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ZipCodeRepository
	{
		private IRepository<CS_ZipCode> _repository {get;set;}
		public IRepository<CS_ZipCode> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ZipCodeRepository(IRepository<CS_ZipCode> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_ZipCode Add(CS_ZipCode entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_ZipCode> AddList(IList<CS_ZipCode> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_ZipCode Update(CS_ZipCode entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_ZipCode> UpdateList(IList<CS_ZipCode> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_ZipCode> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_ZipCode> ListAll(System.Linq.Expressions.Expression<Func<CS_ZipCode, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_ZipCode> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_ZipCode, bool>> where, System.Linq.Expressions.Expression<Func<CS_ZipCode, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_ZipCode Get(System.Linq.Expressions.Expression<Func<CS_ZipCode, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}