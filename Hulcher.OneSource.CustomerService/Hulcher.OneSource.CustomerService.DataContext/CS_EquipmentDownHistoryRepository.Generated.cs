using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentDownHistoryRepository
	{
		private IRepository<CS_EquipmentDownHistory> _repository {get;set;}
		public IRepository<CS_EquipmentDownHistory> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentDownHistoryRepository(IRepository<CS_EquipmentDownHistory> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentDownHistory Add(CS_EquipmentDownHistory entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentDownHistory> AddList(IList<CS_EquipmentDownHistory> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentDownHistory Update(CS_EquipmentDownHistory entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentDownHistory> UpdateList(IList<CS_EquipmentDownHistory> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentDownHistory> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentDownHistory> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentDownHistory, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentDownHistory> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentDownHistory, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentDownHistory, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentDownHistory Get(System.Linq.Expressions.Expression<Func<CS_EquipmentDownHistory, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}