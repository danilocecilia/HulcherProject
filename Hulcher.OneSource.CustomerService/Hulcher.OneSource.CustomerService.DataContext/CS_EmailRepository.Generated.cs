using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmailRepository
	{
		private IRepository<CS_Email> _repository {get;set;}
		public IRepository<CS_Email> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EmailRepository(IRepository<CS_Email> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Email Add(CS_Email entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Email> AddList(IList<CS_Email> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Email Update(CS_Email entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Email> UpdateList(IList<CS_Email> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Email> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Email> ListAll(System.Linq.Expressions.Expression<Func<CS_Email, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Email> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Email, bool>> where, System.Linq.Expressions.Expression<Func<CS_Email, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Email Get(System.Linq.Expressions.Expression<Func<CS_Email, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}