using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_SettingsRepository
	{
		private IRepository<CS_Settings> _repository {get;set;}
		public IRepository<CS_Settings> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_SettingsRepository(IRepository<CS_Settings> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Settings Add(CS_Settings entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Settings> AddList(IList<CS_Settings> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Settings Update(CS_Settings entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Settings> UpdateList(IList<CS_Settings> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Settings> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Settings> ListAll(System.Linq.Expressions.Expression<Func<CS_Settings, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Settings> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Settings, bool>> where, System.Linq.Expressions.Expression<Func<CS_Settings, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Settings Get(System.Linq.Expressions.Expression<Func<CS_Settings, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}