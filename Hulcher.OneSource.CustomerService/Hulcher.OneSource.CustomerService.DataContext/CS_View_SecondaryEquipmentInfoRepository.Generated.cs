using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_SecondaryEquipmentInfoRepository
	{
		private IRepository<CS_View_SecondaryEquipmentInfo> _repository {get;set;}
		public IRepository<CS_View_SecondaryEquipmentInfo> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_SecondaryEquipmentInfoRepository(IRepository<CS_View_SecondaryEquipmentInfo> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_SecondaryEquipmentInfo Add(CS_View_SecondaryEquipmentInfo entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_SecondaryEquipmentInfo> AddList(IList<CS_View_SecondaryEquipmentInfo> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_SecondaryEquipmentInfo Update(CS_View_SecondaryEquipmentInfo entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_SecondaryEquipmentInfo> UpdateList(IList<CS_View_SecondaryEquipmentInfo> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_SecondaryEquipmentInfo> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_SecondaryEquipmentInfo> ListAll(System.Linq.Expressions.Expression<Func<CS_View_SecondaryEquipmentInfo, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_SecondaryEquipmentInfo> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_SecondaryEquipmentInfo, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_SecondaryEquipmentInfo, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_SecondaryEquipmentInfo Get(System.Linq.Expressions.Expression<Func<CS_View_SecondaryEquipmentInfo, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}