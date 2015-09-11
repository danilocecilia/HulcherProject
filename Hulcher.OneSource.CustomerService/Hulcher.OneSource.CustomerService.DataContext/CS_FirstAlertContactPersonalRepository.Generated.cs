using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertContactPersonalRepository
	{
		private IRepository<CS_FirstAlertContactPersonal> _repository {get;set;}
		public IRepository<CS_FirstAlertContactPersonal> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertContactPersonalRepository(IRepository<CS_FirstAlertContactPersonal> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertContactPersonal Add(CS_FirstAlertContactPersonal entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertContactPersonal> AddList(IList<CS_FirstAlertContactPersonal> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertContactPersonal Update(CS_FirstAlertContactPersonal entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertContactPersonal> UpdateList(IList<CS_FirstAlertContactPersonal> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertContactPersonal> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertContactPersonal> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertContactPersonal, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertContactPersonal> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertContactPersonal, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertContactPersonal, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertContactPersonal Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertContactPersonal, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}