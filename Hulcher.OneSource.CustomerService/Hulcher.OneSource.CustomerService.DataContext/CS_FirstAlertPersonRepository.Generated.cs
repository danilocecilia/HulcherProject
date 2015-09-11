using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertPersonRepository
	{
		private IRepository<CS_FirstAlertPerson> _repository {get;set;}
		public IRepository<CS_FirstAlertPerson> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertPersonRepository(IRepository<CS_FirstAlertPerson> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertPerson Add(CS_FirstAlertPerson entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertPerson> AddList(IList<CS_FirstAlertPerson> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertPerson Update(CS_FirstAlertPerson entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertPerson> UpdateList(IList<CS_FirstAlertPerson> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertPerson> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertPerson> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertPerson, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertPerson> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertPerson, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertPerson, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertPerson Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertPerson, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}