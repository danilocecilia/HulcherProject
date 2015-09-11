using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_LostJobInfoRepository
	{
		private IRepository<CS_LostJobInfo> _repository {get;set;}
		public IRepository<CS_LostJobInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_LostJobInfoRepository(IRepository<CS_LostJobInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_LostJobInfo Add(CS_LostJobInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_LostJobInfo> AddList(IList<CS_LostJobInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_LostJobInfo Update(CS_LostJobInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_LostJobInfo> UpdateList(IList<CS_LostJobInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_LostJobInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_LostJobInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_LostJobInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_LostJobInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_LostJobInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_LostJobInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_LostJobInfo Get(System.Linq.Expressions.Expression<Func<CS_LostJobInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}