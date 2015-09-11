using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_LocationInfoRepository
	{
		private IRepository<CS_LocationInfo> _repository {get;set;}
		public IRepository<CS_LocationInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_LocationInfoRepository(IRepository<CS_LocationInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_LocationInfo Add(CS_LocationInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_LocationInfo> AddList(IList<CS_LocationInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_LocationInfo Update(CS_LocationInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_LocationInfo> UpdateList(IList<CS_LocationInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_LocationInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_LocationInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_LocationInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_LocationInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_LocationInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_LocationInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_LocationInfo Get(System.Linq.Expressions.Expression<Func<CS_LocationInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}