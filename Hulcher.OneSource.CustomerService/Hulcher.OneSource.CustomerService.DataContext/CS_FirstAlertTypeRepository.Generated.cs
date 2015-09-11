using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertTypeRepository
	{
		private IRepository<CS_FirstAlertType> _repository {get;set;}
		public IRepository<CS_FirstAlertType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertTypeRepository(IRepository<CS_FirstAlertType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertType Add(CS_FirstAlertType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertType> AddList(IList<CS_FirstAlertType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertType Update(CS_FirstAlertType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertType> UpdateList(IList<CS_FirstAlertType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertType> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertType, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertType Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}