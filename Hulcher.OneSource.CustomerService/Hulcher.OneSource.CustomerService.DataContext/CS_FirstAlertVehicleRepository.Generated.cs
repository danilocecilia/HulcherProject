using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_FirstAlertVehicleRepository
	{
		private IRepository<CS_FirstAlertVehicle> _repository {get;set;}
		public IRepository<CS_FirstAlertVehicle> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_FirstAlertVehicleRepository(IRepository<CS_FirstAlertVehicle> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_FirstAlertVehicle Add(CS_FirstAlertVehicle entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_FirstAlertVehicle> AddList(IList<CS_FirstAlertVehicle> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_FirstAlertVehicle Update(CS_FirstAlertVehicle entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_FirstAlertVehicle> UpdateList(IList<CS_FirstAlertVehicle> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_FirstAlertVehicle> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_FirstAlertVehicle> ListAll(System.Linq.Expressions.Expression<Func<CS_FirstAlertVehicle, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_FirstAlertVehicle> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_FirstAlertVehicle, bool>> where, System.Linq.Expressions.Expression<Func<CS_FirstAlertVehicle, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_FirstAlertVehicle Get(System.Linq.Expressions.Expression<Func<CS_FirstAlertVehicle, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}