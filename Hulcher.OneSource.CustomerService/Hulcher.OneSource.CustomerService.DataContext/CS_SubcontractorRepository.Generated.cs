using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_SubcontractorRepository
	{
		private IRepository<CS_Subcontractor> _repository {get;set;}
		public IRepository<CS_Subcontractor> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_SubcontractorRepository(IRepository<CS_Subcontractor> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Subcontractor Add(CS_Subcontractor entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Subcontractor> AddList(IList<CS_Subcontractor> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Subcontractor Update(CS_Subcontractor entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Subcontractor> UpdateList(IList<CS_Subcontractor> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Subcontractor> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Subcontractor> ListAll(System.Linq.Expressions.Expression<Func<CS_Subcontractor, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Subcontractor> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Subcontractor, bool>> where, System.Linq.Expressions.Expression<Func<CS_Subcontractor, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Subcontractor Get(System.Linq.Expressions.Expression<Func<CS_Subcontractor, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}