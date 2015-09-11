using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobPermitTypeRepository
	{
		private IRepository<CS_JobPermitType> _repository {get;set;}
		public IRepository<CS_JobPermitType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobPermitTypeRepository(IRepository<CS_JobPermitType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobPermitType Add(CS_JobPermitType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobPermitType> AddList(IList<CS_JobPermitType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobPermitType Update(CS_JobPermitType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobPermitType> UpdateList(IList<CS_JobPermitType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobPermitType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobPermitType> ListAll(System.Linq.Expressions.Expression<Func<CS_JobPermitType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobPermitType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobPermitType, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobPermitType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobPermitType Get(System.Linq.Expressions.Expression<Func<CS_JobPermitType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}