using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_FirstAlertReportRepository
	{
		private IRepository<CS_View_FirstAlertReport> _repository {get;set;}
		public IRepository<CS_View_FirstAlertReport> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_FirstAlertReportRepository(IRepository<CS_View_FirstAlertReport> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_FirstAlertReport Add(CS_View_FirstAlertReport entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_FirstAlertReport> AddList(IList<CS_View_FirstAlertReport> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_FirstAlertReport Update(CS_View_FirstAlertReport entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_FirstAlertReport> UpdateList(IList<CS_View_FirstAlertReport> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_FirstAlertReport> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_FirstAlertReport> ListAll(System.Linq.Expressions.Expression<Func<CS_View_FirstAlertReport, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_FirstAlertReport> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_FirstAlertReport, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_FirstAlertReport, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_FirstAlertReport Get(System.Linq.Expressions.Expression<Func<CS_View_FirstAlertReport, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}