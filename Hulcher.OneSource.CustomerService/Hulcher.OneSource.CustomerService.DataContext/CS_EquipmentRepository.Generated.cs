using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentRepository
	{
		private IRepository<CS_Equipment> _repository {get;set;}
		public IRepository<CS_Equipment> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EquipmentRepository(IRepository<CS_Equipment> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Equipment Add(CS_Equipment entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Equipment> AddList(IList<CS_Equipment> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Equipment Update(CS_Equipment entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Equipment> UpdateList(IList<CS_Equipment> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Equipment> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Equipment> ListAll(System.Linq.Expressions.Expression<Func<CS_Equipment, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Equipment> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Equipment, bool>> where, System.Linq.Expressions.Expression<Func<CS_Equipment, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Equipment Get(System.Linq.Expressions.Expression<Func<CS_Equipment, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}