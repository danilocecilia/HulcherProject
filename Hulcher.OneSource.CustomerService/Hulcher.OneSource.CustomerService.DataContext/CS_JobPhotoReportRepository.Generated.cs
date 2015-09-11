using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_JobPhotoReportRepository
	{
		private IRepository<CS_JobPhotoReport> _repository {get;set;}
		public IRepository<CS_JobPhotoReport> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_JobPhotoReportRepository(IRepository<CS_JobPhotoReport> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_JobPhotoReport Add(CS_JobPhotoReport entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_JobPhotoReport> AddList(IList<CS_JobPhotoReport> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_JobPhotoReport Update(CS_JobPhotoReport entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_JobPhotoReport> UpdateList(IList<CS_JobPhotoReport> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_JobPhotoReport> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_JobPhotoReport> ListAll(System.Linq.Expressions.Expression<Func<CS_JobPhotoReport, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_JobPhotoReport> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_JobPhotoReport, bool>> where, System.Linq.Expressions.Expression<Func<CS_JobPhotoReport, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_JobPhotoReport Get(System.Linq.Expressions.Expression<Func<CS_JobPhotoReport, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}