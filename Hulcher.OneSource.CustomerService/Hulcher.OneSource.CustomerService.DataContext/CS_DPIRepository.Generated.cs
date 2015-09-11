using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DPIRepository
	{
		private IRepository<CS_DPI> _repository {get;set;}
		public IRepository<CS_DPI> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_DPIRepository(IRepository<CS_DPI> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_DPI Add(CS_DPI entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_DPI> AddList(IList<CS_DPI> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_DPI Update(CS_DPI entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_DPI> UpdateList(IList<CS_DPI> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_DPI> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_DPI> ListAll(System.Linq.Expressions.Expression<Func<CS_DPI, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_DPI> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_DPI, bool>> where, System.Linq.Expressions.Expression<Func<CS_DPI, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_DPI Get(System.Linq.Expressions.Expression<Func<CS_DPI, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}