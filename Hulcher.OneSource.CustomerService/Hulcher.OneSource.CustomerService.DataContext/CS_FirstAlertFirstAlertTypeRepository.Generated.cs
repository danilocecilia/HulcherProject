using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertFirstAlertTypeRepository
	{
		private IRepository<CS_FirstAlertFirstAlertType> _repository {get;set;}
		public IRepository<CS_FirstAlertFirstAlertType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertFirstAlertTypeRepository(IRepository<CS_FirstAlertFirstAlertType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertFirstAlertType Add(CS_FirstAlertFirstAlertType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertFirstAlertType> AddList(IList<CS_FirstAlertFirstAlertType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertFirstAlertType Update(CS_FirstAlertFirstAlertType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertFirstAlertType> UpdateList(IList<CS_FirstAlertFirstAlertType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertFirstAlertType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertFirstAlertType> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertFirstAlertType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertFirstAlertType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertFirstAlertType, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertFirstAlertType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertFirstAlertType Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertFirstAlertType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}