using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_Job_LocalEquipmentTypeRepository
	{
		private IRepository<CS_Job_LocalEquipmentType> _repository {get;set;}
		public IRepository<CS_Job_LocalEquipmentType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_Job_LocalEquipmentTypeRepository(IRepository<CS_Job_LocalEquipmentType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Job_LocalEquipmentType Add(CS_Job_LocalEquipmentType entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Job_LocalEquipmentType> AddList(IList<CS_Job_LocalEquipmentType> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Job_LocalEquipmentType Update(CS_Job_LocalEquipmentType entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Job_LocalEquipmentType> UpdateList(IList<CS_Job_LocalEquipmentType> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Job_LocalEquipmentType> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Job_LocalEquipmentType> ListAll(System.Linq.Expressions.Expression<Func<CS_Job_LocalEquipmentType, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Job_LocalEquipmentType> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Job_LocalEquipmentType, bool>> where, System.Linq.Expressions.Expression<Func<CS_Job_LocalEquipmentType, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Job_LocalEquipmentType Get(System.Linq.Expressions.Expression<Func<CS_Job_LocalEquipmentType, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}