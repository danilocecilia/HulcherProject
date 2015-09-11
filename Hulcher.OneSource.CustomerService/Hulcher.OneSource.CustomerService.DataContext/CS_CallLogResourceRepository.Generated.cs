using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CallLogResourceRepository
	{
		private IRepository<CS_CallLogResource> _repository {get;set;}
		public IRepository<CS_CallLogResource> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CallLogResourceRepository(IRepository<CS_CallLogResource> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_CallLogResource Add(CS_CallLogResource entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_CallLogResource> AddList(IList<CS_CallLogResource> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_CallLogResource Update(CS_CallLogResource entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_CallLogResource> UpdateList(IList<CS_CallLogResource> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_CallLogResource> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_CallLogResource> ListAll(System.Linq.Expressions.Expression<Func<CS_CallLogResource, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_CallLogResource> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_CallLogResource, bool>> where, System.Linq.Expressions.Expression<Func<CS_CallLogResource, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_CallLogResource Get(System.Linq.Expressions.Expression<Func<CS_CallLogResource, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}