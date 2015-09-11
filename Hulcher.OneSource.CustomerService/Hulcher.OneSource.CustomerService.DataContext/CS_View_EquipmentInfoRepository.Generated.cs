using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_EquipmentInfoRepository
	{
		private IRepository<CS_View_EquipmentInfo> _repository {get;set;}
		public IRepository<CS_View_EquipmentInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_EquipmentInfoRepository(IRepository<CS_View_EquipmentInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_EquipmentInfo Add(CS_View_EquipmentInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_EquipmentInfo> AddList(IList<CS_View_EquipmentInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_EquipmentInfo Update(CS_View_EquipmentInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_EquipmentInfo> UpdateList(IList<CS_View_EquipmentInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_EquipmentInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_EquipmentInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_View_EquipmentInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_EquipmentInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_EquipmentInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_EquipmentInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_EquipmentInfo Get(System.Linq.Expressions.Expression<Func<CS_View_EquipmentInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}