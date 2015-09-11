using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;

// This file is auto generated and will be overwritten as soon as the template is executed
// Do not modify this file...
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CompetitorRepository
	{
		private IRepository<CS_Competitor> _repository {get;set;}
		public IRepository<CS_Competitor> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public CS_CompetitorRepository(IRepository<CS_Competitor> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}

		public CS_Competitor Add(CS_Competitor entity)
		{
			return Repository.Add(entity);
		}

		public IList<CS_Competitor> AddList(IList<CS_Competitor> entity)
		{
			return Repository.AddList(entity);
		}

		public CS_Competitor Update(CS_Competitor entity)
		{
			return Repository.Update(entity);
		}
		
		public IList<CS_Competitor> UpdateList(IList<CS_Competitor> entity)
		{
			return Repository.UpdateList(entity);
		}

		public IList<CS_Competitor> ListAll(params string[] includeList)
		{
			return Repository.ListAll(includeList);
		}

		public IList<CS_Competitor> ListAll(System.Linq.Expressions.Expression<Func<CS_Competitor, bool>> where, params string[] includeList)
		{
			return Repository.ListAll(where, includeList);
		}

		public IList<CS_Competitor> ListAll<TKey>(System.Linq.Expressions.Expression<Func<CS_Competitor, bool>> where, System.Linq.Expressions.Expression<Func<CS_Competitor, TKey>> orderby, bool ascending, params string[] includeList)
		{
			return Repository.ListAll<TKey>(where, orderby, ascending, includeList);
		}

		public CS_Competitor Get(System.Linq.Expressions.Expression<Func<CS_Competitor, bool>> where, params string[] includeList)
		{
			return Repository.Get(where, includeList);
		}

		public void ExecuteSql(ObjectContext c, string sql)
		{
			Repository.ExecuteSql(c,sql);
		}
	}
}