using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentPermitRepository
	{
		private IRepository<CS_EquipmentPermit> _repository {get;set;}
		public IRepository<CS_EquipmentPermit> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentPermitRepository(IRepository<CS_EquipmentPermit> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentPermit Add(CS_EquipmentPermit entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentPermit> AddList(IList<CS_EquipmentPermit> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentPermit Update(CS_EquipmentPermit entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentPermit> UpdateList(IList<CS_EquipmentPermit> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentPermit> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentPermit> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentPermit, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentPermit> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentPermit, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentPermit, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentPermit Get(System.Linq.Expressions.Expression<Func<CS_EquipmentPermit, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}