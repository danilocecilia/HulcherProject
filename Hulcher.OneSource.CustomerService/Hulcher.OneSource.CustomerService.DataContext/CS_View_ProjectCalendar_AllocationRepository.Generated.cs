using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_ProjectCalendar_AllocationRepository
	{
		private IRepository<CS_View_ProjectCalendar_Allocation> _repository {get;set;}
		public IRepository<CS_View_ProjectCalendar_Allocation> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_ProjectCalendar_AllocationRepository(IRepository<CS_View_ProjectCalendar_Allocation> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_ProjectCalendar_Allocation Add(CS_View_ProjectCalendar_Allocation entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_ProjectCalendar_Allocation> AddList(IList<CS_View_ProjectCalendar_Allocation> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_ProjectCalendar_Allocation Update(CS_View_ProjectCalendar_Allocation entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_ProjectCalendar_Allocation> UpdateList(IList<CS_View_ProjectCalendar_Allocation> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_ProjectCalendar_Allocation> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_ProjectCalendar_Allocation> ListAll(System.Linq.Expressions.Expression<Func<CS_View_ProjectCalendar_Allocation, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_ProjectCalendar_Allocation> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_ProjectCalendar_Allocation, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_ProjectCalendar_Allocation, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_ProjectCalendar_Allocation Get(System.Linq.Expressions.Expression<Func<CS_View_ProjectCalendar_Allocation, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}