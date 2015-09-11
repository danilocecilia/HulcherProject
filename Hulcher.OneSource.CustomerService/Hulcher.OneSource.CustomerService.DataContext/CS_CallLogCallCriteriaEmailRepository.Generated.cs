using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallLogCallCriteriaEmailRepository
	{
		private IRepository<CS_CallLogCallCriteriaEmail> _repository {get;set;}
		public IRepository<CS_CallLogCallCriteriaEmail> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallLogCallCriteriaEmailRepository(IRepository<CS_CallLogCallCriteriaEmail> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallLogCallCriteriaEmail Add(CS_CallLogCallCriteriaEmail entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallLogCallCriteriaEmail> AddList(IList<CS_CallLogCallCriteriaEmail> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallLogCallCriteriaEmail Update(CS_CallLogCallCriteriaEmail entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallLogCallCriteriaEmail> UpdateList(IList<CS_CallLogCallCriteriaEmail> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallLogCallCriteriaEmail> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallLogCallCriteriaEmail> ListAll(System.Linq.Expressions.Expression<Func<CS_CallLogCallCriteriaEmail, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallLogCallCriteriaEmail> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallLogCallCriteriaEmail, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallLogCallCriteriaEmail, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallLogCallCriteriaEmail Get(System.Linq.Expressions.Expression<Func<CS_CallLogCallCriteriaEmail, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}