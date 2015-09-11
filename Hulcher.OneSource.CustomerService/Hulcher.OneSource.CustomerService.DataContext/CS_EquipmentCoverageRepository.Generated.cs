using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentCoverageRepository
	{
		private IRepository<CS_EquipmentCoverage> _repository {get;set;}
		public IRepository<CS_EquipmentCoverage> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentCoverageRepository(IRepository<CS_EquipmentCoverage> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentCoverage Add(CS_EquipmentCoverage entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentCoverage> AddList(IList<CS_EquipmentCoverage> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentCoverage Update(CS_EquipmentCoverage entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentCoverage> UpdateList(IList<CS_EquipmentCoverage> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentCoverage> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentCoverage> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentCoverage, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentCoverage> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentCoverage, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentCoverage, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentCoverage Get(System.Linq.Expressions.Expression<Func<CS_EquipmentCoverage, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}