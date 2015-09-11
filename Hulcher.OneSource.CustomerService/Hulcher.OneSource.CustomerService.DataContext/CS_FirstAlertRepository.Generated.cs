using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertRepository
	{
		private IRepository<CS_FirstAlert> _repository {get;set;}
		public IRepository<CS_FirstAlert> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertRepository(IRepository<CS_FirstAlert> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlert Add(CS_FirstAlert entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlert> AddList(IList<CS_FirstAlert> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlert Update(CS_FirstAlert entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlert> UpdateList(IList<CS_FirstAlert> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlert> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlert> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlert, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlert> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlert, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlert, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlert Get(System.Linq.Expressions.Expression<Func<CS_FirstAlert, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}