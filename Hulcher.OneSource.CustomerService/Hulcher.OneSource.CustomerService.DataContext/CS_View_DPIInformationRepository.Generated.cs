using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_View_DPIInformationRepository
	{
		private IRepository<CS_View_DPIInformation> _repository {get;set;}
		public IRepository<CS_View_DPIInformation> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_View_DPIInformationRepository(IRepository<CS_View_DPIInformation> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_View_DPIInformation Add(CS_View_DPIInformation entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_View_DPIInformation> AddList(IList<CS_View_DPIInformation> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_View_DPIInformation Update(CS_View_DPIInformation entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_View_DPIInformation> UpdateList(IList<CS_View_DPIInformation> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_View_DPIInformation> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_View_DPIInformation> ListAll(System.Linq.Expressions.Expression<Func<CS_View_DPIInformation, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_View_DPIInformation> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_View_DPIInformation, bool>> where, System.Linq.Expressions.Expression<Func<CS_View_DPIInformation, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_View_DPIInformation Get(System.Linq.Expressions.Expression<Func<CS_View_DPIInformation, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}