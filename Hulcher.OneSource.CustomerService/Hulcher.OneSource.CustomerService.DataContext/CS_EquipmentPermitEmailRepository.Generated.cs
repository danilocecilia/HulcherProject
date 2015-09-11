using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentPermitEmailRepository
	{
		private IRepository<CS_EquipmentPermitEmail> _repository {get;set;}
		public IRepository<CS_EquipmentPermitEmail> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentPermitEmailRepository(IRepository<CS_EquipmentPermitEmail> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EquipmentPermitEmail Add(CS_EquipmentPermitEmail entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EquipmentPermitEmail> AddList(IList<CS_EquipmentPermitEmail> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EquipmentPermitEmail Update(CS_EquipmentPermitEmail entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EquipmentPermitEmail> UpdateList(IList<CS_EquipmentPermitEmail> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EquipmentPermitEmail> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EquipmentPermitEmail> ListAll(System.Linq.Expressions.Expression<Func<CS_EquipmentPermitEmail, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EquipmentPermitEmail> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EquipmentPermitEmail, bool>> where, System.Linq.Expressions.Expression<Func<CS_EquipmentPermitEmail, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EquipmentPermitEmail Get(System.Linq.Expressions.Expression<Func<CS_EquipmentPermitEmail, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}