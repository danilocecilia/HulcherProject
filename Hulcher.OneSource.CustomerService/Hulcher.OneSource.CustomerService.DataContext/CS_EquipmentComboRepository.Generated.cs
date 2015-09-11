using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentComboRepository
	{
		private IRepository<CS_EquipmentCombo> _repository {get;set;}
		public IRepository<CS_EquipmentCombo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentComboRepository(IRepository<CS_EquipmentCombo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentCombo Add(CS_EquipmentCombo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentCombo> AddList(IList<CS_EquipmentCombo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentCombo Update(CS_EquipmentCombo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentCombo> UpdateList(IList<CS_EquipmentCombo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentCombo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentCombo> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentCombo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentCombo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentCombo, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentCombo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentCombo Get(System.Linq.Expressions.Expression<Func<CS_EquipmentCombo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}