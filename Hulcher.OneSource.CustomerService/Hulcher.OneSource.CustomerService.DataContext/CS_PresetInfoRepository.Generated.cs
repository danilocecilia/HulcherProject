using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_PresetInfoRepository
	{
		private IRepository<CS_PresetInfo> _repository {get;set;}
		public IRepository<CS_PresetInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_PresetInfoRepository(IRepository<CS_PresetInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_PresetInfo Add(CS_PresetInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_PresetInfo> AddList(IList<CS_PresetInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_PresetInfo Update(CS_PresetInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_PresetInfo> UpdateList(IList<CS_PresetInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_PresetInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_PresetInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_PresetInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_PresetInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_PresetInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_PresetInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_PresetInfo Get(System.Linq.Expressions.Expression<Func<CS_PresetInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}