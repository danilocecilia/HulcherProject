using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_LostJobReasonRepository
	{
		private IRepository<CS_LostJobReason> _repository {get;set;}
		public IRepository<CS_LostJobReason> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_LostJobReasonRepository(IRepository<CS_LostJobReason> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_LostJobReason Add(CS_LostJobReason entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_LostJobReason> AddList(IList<CS_LostJobReason> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_LostJobReason Update(CS_LostJobReason entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_LostJobReason> UpdateList(IList<CS_LostJobReason> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_LostJobReason> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_LostJobReason> ListAll(System.Linq.Expressions.Expression<Func<CS_LostJobReason, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_LostJobReason> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_LostJobReason, bool>> where, System.Linq.Expressions.Expression<Func<CS_LostJobReason, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_LostJobReason Get(System.Linq.Expressions.Expression<Func<CS_LostJobReason, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}