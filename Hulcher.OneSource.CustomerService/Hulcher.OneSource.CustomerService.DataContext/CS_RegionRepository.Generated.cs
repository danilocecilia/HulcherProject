using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_RegionRepository
	{
		private IRepository<CS_Region> _repository {get;set;}
		public IRepository<CS_Region> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_RegionRepository(IRepository<CS_Region> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Region Add(CS_Region entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Region> AddList(IList<CS_Region> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Region Update(CS_Region entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Region> UpdateList(IList<CS_Region> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Region> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Region> ListAll(System.Linq.Expressions.Expression<Func<CS_Region, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Region> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Region, bool>> where, System.Linq.Expressions.Expression<Func<CS_Region, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Region Get(System.Linq.Expressions.Expression<Func<CS_Region, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}