using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertDivisionRepository
	{
		private IRepository<CS_FirstAlertDivision> _repository {get;set;}
		public IRepository<CS_FirstAlertDivision> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertDivisionRepository(IRepository<CS_FirstAlertDivision> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertDivision Add(CS_FirstAlertDivision entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertDivision> AddList(IList<CS_FirstAlertDivision> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertDivision Update(CS_FirstAlertDivision entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertDivision> UpdateList(IList<CS_FirstAlertDivision> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertDivision> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertDivision> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertDivision, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertDivision> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertDivision, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertDivision, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertDivision Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertDivision, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}