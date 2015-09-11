using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeEmergencyContactRepository
	{
		private IRepository<CS_EmployeeEmergencyContact> _repository {get;set;}
		public IRepository<CS_EmployeeEmergencyContact> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_EmployeeEmergencyContactRepository(IRepository<CS_EmployeeEmergencyContact> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_EmployeeEmergencyContact Add(CS_EmployeeEmergencyContact entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_EmployeeEmergencyContact> AddList(IList<CS_EmployeeEmergencyContact> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_EmployeeEmergencyContact Update(CS_EmployeeEmergencyContact entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_EmployeeEmergencyContact> UpdateList(IList<CS_EmployeeEmergencyContact> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_EmployeeEmergencyContact> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_EmployeeEmergencyContact> ListAll(System.Linq.Expressions.Expression<Func<CS_EmployeeEmergencyContact, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_EmployeeEmergencyContact> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_EmployeeEmergencyContact, bool>> where, System.Linq.Expressions.Expression<Func<CS_EmployeeEmergencyContact, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_EmployeeEmergencyContact Get(System.Linq.Expressions.Expression<Func<CS_EmployeeEmergencyContact, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}