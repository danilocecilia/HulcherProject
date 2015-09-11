using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentTypeRepository
	{
		private IRepository<CS_EquipmentType> _repository {get;set;}
		public IRepository<CS_EquipmentType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentTypeRepository(IRepository<CS_EquipmentType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentType Add(CS_EquipmentType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentType> AddList(IList<CS_EquipmentType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentType Update(CS_EquipmentType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentType> UpdateList(IList<CS_EquipmentType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentType> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentType, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentType Get(System.Linq.Expressions.Expression<Func<CS_EquipmentType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}