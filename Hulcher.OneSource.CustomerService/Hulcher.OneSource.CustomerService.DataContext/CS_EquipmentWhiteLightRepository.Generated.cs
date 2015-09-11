using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentWhiteLightRepository
	{
		private IRepository<CS_EquipmentWhiteLight> _repository {get;set;}
		public IRepository<CS_EquipmentWhiteLight> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentWhiteLightRepository(IRepository<CS_EquipmentWhiteLight> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentWhiteLight Add(CS_EquipmentWhiteLight entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentWhiteLight> AddList(IList<CS_EquipmentWhiteLight> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentWhiteLight Update(CS_EquipmentWhiteLight entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentWhiteLight> UpdateList(IList<CS_EquipmentWhiteLight> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentWhiteLight> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentWhiteLight> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentWhiteLight, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentWhiteLight> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentWhiteLight, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentWhiteLight, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentWhiteLight Get(System.Linq.Expressions.Expression<Func<CS_EquipmentWhiteLight, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}