using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ResourceRepository
	{
		private IRepository<CS_Resource> _repository {get;set;}
		public IRepository<CS_Resource> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_ResourceRepository(IRepository<CS_Resource> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Resource Add(CS_Resource entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Resource> AddList(IList<CS_Resource> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Resource Update(CS_Resource entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Resource> UpdateList(IList<CS_Resource> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Resource> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Resource> ListAll(System.Linq.Expressions.Expression<Func<CS_Resource, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Resource> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Resource, bool>> where, System.Linq.Expressions.Expression<Func<CS_Resource, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Resource Get(System.Linq.Expressions.Expression<Func<CS_Resource, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}